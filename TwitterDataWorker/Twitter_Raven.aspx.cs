using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TweetSharp;
using System.Web.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using TwitterDataWorker;
using Raven.Client;
using Raven.Client.Connection;
using Raven.Client.Document;
using System.Diagnostics;
using System.Net;
using System.IO;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections;

namespace TwitterDataWorker
{
       public partial class Twitter_noSQL : System.Web.UI.Page
    {
        private string _consumerKey = WebConfigurationManager.AppSettings["_consumerKey"];
        private string _consumerSecret = WebConfigurationManager.AppSettings["_consumerSecret"];
        private string _accessToken = "";
        private string _accessTokenSecret = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //step 1: authenticate twitter client, return twitter user via consumer and token info
            TwitterUser User = authenticate(_consumerKey, _consumerSecret,
                _accessToken, _accessTokenSecret);

            if (!IsPostBack)
            {

                //step 2: init twitter data
                //note: should i have a differnet method seperate fo rthis
                //how would this method work (static or nonstatic - pass obj as param?)
                var exampleData = new Twitter_Data
                {
                    userId = (User.Id).ToString(),
                    ScreenName = User.ScreenName,
                    Token = _accessToken,
                    TokenSecret = _accessTokenSecret,
                    isVerified = User.IsVerified.Value.ToString(),
                    Name = User.Name,
                    profImgUrl = User.ProfileImageUrl,
                };


                //check if this should be done in global.asax file for efficiency
                //session factory design pattern is needed for this
                var documentStore = new DocumentStore
                {
                    Url = "http://localhost:8081",  //subject to change
                    DefaultDatabase = "Twitter_User_Info",
                    //ConnectionStringName = "RavenDB",
                    //insert connection string name?
                };
                documentStore.Initialize();

                //create new product
                using (var session = documentStore.OpenSession())
                {
                    session.Store(exampleData);
                    session.SaveChanges();
                };

                //step 4: load example product, show data 2 user (need better implementation 4 this)
                using (var session = documentStore.OpenSession())
                {
                    var p = session.Load<Twitter_Data>("Twitter_Datas/1");
                    InfoList.Items.Add(p.userId.ToString());
                    InfoList.Items.Add(p.Name);
                    InfoList.Items.Add(p.ScreenName);
                    InfoList.Items.Add(p.Token);
                    InfoList.Items.Add(p.TokenSecret);
                    InfoList.Items.Add(p.isVerified);
                    InfoList.Items.Add(p.profImgUrl);
                    session.SaveChanges();
                }
            
            }
            else //note: need to fix postback to clear ite
            {
                var documentStore = new DocumentStore
                {
                    Url = "http://localhost:8081",  //subject to change
                    DefaultDatabase = "Twitter_User_Info",
                };
                documentStore.Initialize();
                
                using (var session = documentStore.OpenSession())
                {
                    var p = session.Load<Twitter_Data>("Twitter_Datas/1");
                    InfoList.Items.Add(p.userId.ToString());
                    InfoList.Items.Add(p.Name);
                    InfoList.Items.Add(p.ScreenName);
                    InfoList.Items.Add(p.Token);
                    InfoList.Items.Add(p.TokenSecret);
                    InfoList.Items.Add(p.isVerified);
                    InfoList.Items.Add(p.profImgUrl);
                    session.SaveChanges();
                };

            
            }
        }

        public static TwitterUser authenticate(string _consumerKey, string _consumerSecret,
            string _accessToken, string _accessTokenSecret)
        {
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret); //create a twitter service
            service.AuthenticateWith(_accessToken, _accessTokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());
            return user;
        }

        //protected void TextBox2_TextChanged(object sender, EventArgs e)
        //{

        //}
    }


}
