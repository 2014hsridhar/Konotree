using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using TweetSharp;
using System.Web.UI.WebControls;

namespace TwitterApplication1
{
    public partial class Default : System.Web.UI.Page
    {

        private string _consumerKey = WebConfigurationManager.AppSettings["_consumerKey"];
        private string _consumerSecret = WebConfigurationManager.AppSettings["_consumerSecret"];
        //private TwitterLDataContext db;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAuthenticate_Click(object sender, EventArgs e)
        {
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret); //create a twitter service
            OAuthRequestToken requestToken = service.GetRequestToken("http://127.0.0.1:52316/authorizeCallback.aspx");    //request token: specify callback URL

            // Step 2 - Redirect to the OAuth Authorization URL
            //Uri uri = service.GetAuthorizationUri(requestToken);
            Uri uri = service.GetAuthenticationUrl(requestToken);
            //Label1.Text = uri.AbsoluteUri.ToString();
            Response.Redirect(uri.ToString());
        }

    }
}