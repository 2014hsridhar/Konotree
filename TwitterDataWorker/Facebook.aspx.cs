using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Raven.Client;
using Raven.Client.Connection;
using Raven.Client.Document;

using Facebook;

namespace TwitterDataWorker
{
    public partial class Facebook : System.Web.UI.Page
    {
        private const string app_id = "1457269367858050";
        private const string app_secret = "5d7f63825c49a88832d3f11da7cbd1b5";
        private const string scope = "email, user_about_me ";
        private string _accessToken;

        protected void login_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1457269367858050",
                redirect_uri = "http://localhost:60590/Facebook.aspx",
                response_type = "code",
                scope = "public_profile,user_friends,user_groups,user_about_me,read_stream, read_friendlists"
            });
            Response.Redirect(loginUrl.AbsoluteUri);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (Session["AccessToken"] != null)
                {
                    // Retrieve user info, from database
                    //Else,  create a new FB Client with this accesstoken
                    //and extract data again.
                    GetUserData(Session["AccessToken"].ToString());
                }
                //check if FB redirect
                else if (Request.QueryString["code"] != null)
                {
                    string accessCode = Request.QueryString["code"];    //get access code
                    FacebookClient fb = new FacebookClient();
                    dynamic result = fb.Post("oauth/access_token", new
                    {
                        client_id = "1457269367858050",
                        client_secret = "5d7f63825c49a88832d3f11da7cbd1b5",
                        redirect_uri = "http://localhost:60590/Facebook.aspx",
                        code = accessCode
                    });

                    // Store the access token in the session
                    Session["AccessToken"] = result.access_token;
                }

               

            }
            else if (Request.QueryString["error"] != null)
            {
                string error = Request.QueryString["error"];
                string errorResponse = Request.QueryString["error_reason"];
                string errorDescription = Request.QueryString["error_description"];
                //do whatever here
                Status.Text = errorDescription;
            }
            else Status.Text = "Not Signed IN"; //user not connected - should ask resign
        }


        protected void logout_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            var logoutUrl = fb.GetLogoutUrl(new { access_token = _accessToken, next = "https://www.facebook.com/connect/login_success.html" });
            Session.Remove("AccessToken");
            Response.Redirect(logoutUrl.AbsoluteUri);
        }

        protected void aggregate_click(object sender, EventArgs e)
        {
            GetUserData(Session["AccessToken"].ToString());
        }

        private string getMessage(FacebookOAuthResult facebookOAuthResult)
        {
            string message = "";
            if (facebookOAuthResult != null)
            {
                if (facebookOAuthResult.IsSuccess)
                {
                    _accessToken = facebookOAuthResult.AccessToken;
                    var fb = new FacebookClient(facebookOAuthResult.AccessToken);

                    dynamic result = fb.Get("/me");
                    message = "Login successufl, Hello: " + result.name;
                }
                else message = facebookOAuthResult.ErrorDescription;
            }
            return message;
        }

        private void GetUserData(string accessToken)
        {
            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",  //subject to change
                DefaultDatabase = "Facebook_User_Info",
            };
            documentStore.Initialize();

            //GET: Graph API
            FacebookClient fb = new FacebookClient(accessToken);   //use access token
            dynamic me = fb.Get("me");
            dynamic userGroups = fb.Get("/me/groups");

            Facebook_Data data = new Facebook_Data
            {
                id = me.id,
                email = me.email,
                first_name = me.first_name,
                last_name = me.last_name,
                facebookName = me.name,
                gender = me.gender,
                locale = me.locale,
                userName = me.username,
                groups = userGroups.data
            };

            using (var session = documentStore.OpenSession())
            {
                session.Store(data);
                session.SaveChanges();
            };
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }

}
