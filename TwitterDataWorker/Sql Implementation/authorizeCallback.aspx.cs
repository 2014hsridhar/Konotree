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
//using TwitterUsersInfo.DbAccessObjects;
using TwitterDataWorker;
using Raven.Client;
//using Raven.Client.Embedded;
using Raven.Client.Connection;

namespace TwitterApplication1
{
    public partial class AuthorizeCallback : System.Web.UI.Page
    {
        //private string _consumerKey = WebConfigurationManager.AppSettings["_consumerKey"];
        //private string _consumerSecret = WebConfigurationManager.AppSettings["_consumerSecret"];
        //private string _accessToken = "2646280183-pWOqDO2EqyUjEB6K8lUbf5VLnrPA61pyz6UgO9v";
        //private string _accessTokenSecret = "IDJlUHf1FZOrYsQUzydzvcFFzJar7Gyv6mQWMSwjjo8cP";
        //private TwitterInfoDataContext db;
        //private TwitterUsersInfo table;
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!IsPostBack)
        //        {
        //            //        //check if these things are available - authorization token, authorization verify arg
        //            //        //should be on top in URL
        //            //        if (Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verification"] != null)
        //            //        {
        //            //            //get user request token and verify, from url
        //            //            OAuthRequestToken requestToken = new OAuthRequestToken();
        //            //            requestToken.Token = Request.QueryString["oauth_token"];
        //            //            string a = Request.QueryString["oauth_verifier"];


        //            TwitterService service = new TwitterService(_consumerKey, _consumerSecret); //create a twitter service
        //            //            //call service authentication, request access token and secret, from user
        //            service.AuthenticateWith(_accessToken, _accessTokenSecret);
        //            //make a new service- verify credentials
        //            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());
        //            //access properties of choice, store in session state
        //            string id = (user.Id).ToString();
        //            Session["Id"] = user.Id;
        //            Session["ScreenName"] = user.ScreenName;
        //            Session["ProfileImageUrl"] = user.ProfileImageUrl;
        //            Label1.Text = String.Format("Screen Name is {0} and user name is {1}", user.ScreenName, user.Name);

        //            db = new TwitterInfoDataContext();
        //            //check if user is in database
        //            var checkUserVerify = from row in db.TwitterUsersInfos
        //                                  where row.ID == user.Id
        //                                  select user.Id;

        //            getTweetsTimeLine((int)(user.Id), user.ScreenName);

        //            //if user isn't in database - save info in database
        //            if (checkUserVerify.Count() == 0)
        //            {
        //                table = new TwitterUsersInfo();
        //                table.ID = user.Id;
        //                table.IsVerified = user.IsVerified.Value.ToString();
        //                table.Name = user.Name;
        //                table.ProfileImageUrl = user.ProfileImageUrl;
        //                table.ScreenName = user.ScreenName;
        //                table.Token = _accessToken;
        //                table.TokenSecret = _accessTokenSecret;

        //                db.TwitterUsersInfos.InsertOnSubmit(table);
        //                db.SubmitChanges();

        //            }

        //            var query = from unique_user in db.TwitterUsersInfos
        //                        select unique_user;

        //            GridView1.DataSource = query;
        //            GridView1.DataBind();
        //        }
        //        else
        //        {   //do something if session screen name is not null - let user know that they're on twitter
        //            //if (Session["ScreenName"] != null)

        //        }
        //    }
        //    catch (Exception err) { Console.WriteLine(err.ToString()); }
        //}

        //private void getTweetsTimeLine(int userId, string userScreenName)
        //{
        //    //go back to database - get token, secret for user
        //    TwitterService service = new TwitterService(_consumerKey, _consumerSecret); //create a twitter service
        //    service.AuthenticateWith(_accessToken, _accessTokenSecret);
        //    db = new TwitterInfoDataContext();
        //    //just want 1 unique user id
        //    var userTimeLine_TokenSecret = from userTl_TokenSecret in db.TwitterUsersInfos
        //                                   where userTl_TokenSecret.ID == userId
        //                                   select new { userTl_TokenSecret.Token, userTl_TokenSecret.TokenSecret };

        //    ////send test tweet
        //    //SendTweetOptions options = new SendTweetOptions();
        //    //options.Status = "Sending 1st tweet with Twitter API and TweetSharp #TwitterDevelopers";
        //    //service.SendTweet(options);

        //    //get user timeline (list)
        //    var vals = new TweetSharp.ListTweetsOnUserTimelineOptions();
        //    vals.ScreenName = userScreenName;
        //    vals.Count = 5;
        //    IEnumerable<TwitterStatus> tweets = service.ListTweetsOnUserTimeline(vals);

        //    GridView2.DataSource = buildTweetLine(tweets);  //return data table
        //    GridView2.DataBind();
        //}

        ////    private string ConvertUrlsToLinks(string msg)
        ////    {
        ////        return "";
        ////        //Lookup at minute 29
        ////    }

        //private DataTable buildTweetLine(IEnumerable<TwitterStatus> args)
        //{
        //    DataTable tbTimeLine = new DataTable("UserTimeLine");
        //    tbTimeLine.Columns.Add("ScreenName", typeof(string));
        //    tbTimeLine.Columns.Add("TwitterText", typeof(string));
        //    tbTimeLine.Columns.Add("ImageUrl", typeof(string));
        //    tbTimeLine.Columns.Add("UserName", typeof(string));
        //    tbTimeLine.Columns.Add("CreatedDate", typeof(DateTime));
        //    //tbTimeLine.Columns.Add("Minutesago", typeof(string));
        //    //tbTimeLine.Columns.Add("Secondsago", typeof(double));
        //    foreach (var item in args)
        //    {
        //        tbTimeLine.Rows.Add(item.User.ScreenName, item.TextDecoded,
        //        item.User.ProfileImageUrl, item.User.Name, item.CreatedDate.AddHours(-4));
        //    }
        //    return tbTimeLine;

        //}

        ////tweet search - simpel string (need 2 implement one for multiple querys)
        //private void tweetSearch()
        //{

        //}


        ////    //send what has been clicked to twitter
        ////    protected void Button1_Click(object sender, EventArgs e)
        ////    {
        ////        try
        ////        {
        ////            TwitterService service = new TwitterService(_consumerKey, _consumerSecret); //create a twitter service
        ////            db = new TwitterLDataContext();

        ////            var currentUser = from _currentUser in db.TwitterUsers
        ////                                           where _currentUser.ID.toString() = Session["Id"].ToString();
        ////                                           select _currentUser; 

        ////            foreach(var _currentUser in currentUser) {
        ////                service.AuthenticateWith(_currentUser.Token, _currentUser.TokenSecret);
        ////            }

        ////            TwitterStatus status = service.SendTweet(HttpUtility.HtmlEncode(txtUpdateStatus.Text));
        ////            if(status != null)
        ////            {
        ////                txtUpdateStatus.Text = "";
        ////                getTweetsTimeLine(int.Parse(Session["Id"].ToString()));
        ////            }
        ////        }
        ////        catch (Exception err)
        ////        {
        ////            lblText.Text = err.Message;
        ////        }
        ////    }
        ////}
    }
}