using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Raven.Client;
using Raven.Client.Connection;
using Raven.Client.Document;

using Spring.Json;
using Spring.Social.OAuth1;
using Spring.Social.LinkedIn.Api;
using Spring.Social.LinkedIn.Connect;
using System.Threading.Tasks;
using Spring.Social.LinkedIn.Api.Impl;
using System.Collections.Specialized;

namespace TwitterDataWorker
{
    public partial class LinkedIn : System.Web.UI.Page
    {
        private const string LinkedInApiKey = "77n05i85cakept";
        private const string LinkedInApiSecret = "sM8dlQFnbgWtAZC7";
        private const string LinkedInUserToken = "c96bc95f-100e-4919-9fce-b7fb8d8eb385";
        private const string LinkedInUserSecret = "08e5f363-89bf-4c08-af5b-b5153e2fbaef";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var documentStore = new DocumentStore
                {
                    Url = "http://localhost:8080",  //subject to change
                    DefaultDatabase = "LinkedIn_User_Infor",
                    //ConnectionStringName = "RavenDB",
                    //insert connection string name?
                };
                documentStore.Initialize();

                try
                {
                    ILinkedIn linkedIn = new LinkedInTemplate(LinkedInApiKey, LinkedInApiSecret, LinkedInUserToken, LinkedInUserSecret);
                    LinkedInServiceProvider provider = new LinkedInServiceProvider(LinkedInApiKey, LinkedInApiSecret);
                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("scope", "r_basicprofile r_emailaddress  r_fullprofile r_network r_contactinfo"); //note: need to check how these params are done
                    ILinkedIn access = provider.GetApi(LinkedInUserToken, LinkedInUserSecret);

                    LinkedInProfile profile = access.ProfileOperations.GetUserProfileAsync().Result;
                    LinkedInProfiles profiles = access.ConnectionOperations.GetConnectionsAsync().Result;
                    NetworkStatistics networkCon = access.ConnectionOperations.GetNetworkStatisticsAsync().Result;

                    LinkedIn_Data data = new LinkedIn_Data
                    {
                        userId = profile.ID,
                        Token = LinkedInUserToken,
                        TokenSecret = LinkedInUserSecret,
                        FirstName = profile.FirstName,
                        Lastname = profile.LastName,
                        Summary = profile.Summary,
                        Industry = profile.Industry,
                        profileUrl = profile.PublicProfileUrl,
                        headLine = profile.Headline,
                        numFirstDeg = networkCon.FirstDegreeCount,
                        numSecondDeg = networkCon.SecondDegreeCount,
                        connectData = profiles.Profiles
                    };

                    using (var session = documentStore.OpenSession())
                    {
                        session.Store(data);
                        session.SaveChanges();
                    };
                }
                catch (AggregateException ae)
                {
                    ae.Handle(ex =>
                    {
                        // TODO: Update after error handler implementation
                        if (ex is Spring.Rest.Client.HttpResponseException)
                        {
                            Response.Write(ex.Message);
                            Response.Write(((Spring.Rest.Client.HttpResponseException)ex).GetResponseBodyAsString());
                            return true;
                        }
                        return false;
                    });
                }


            }
        }
    }
}