using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterDataWorker;
using TweetSharp;
using LinqToTwitter;
using Raven.Client;
using Raven.Client.Connection;
using Raven.Client.Document;
using System.Web.Configuration;
using System.Threading;

namespace TwitterDataWorker
{

    public partial class fullRaven : System.Web.UI.Page
    {
        const string accessToken = "2646280183-XFBmBcL8O0sUNl6eYgYigsoLVHN0x1yEf62XsPJ";
        const string accessTokenSecret = "NOBBmpBQqOhZZxYb4iBUD88038LKsOvImz1pZoi7udiNX";
        const string consumerKey = "cRL7ezgfmbzAuCdK9744LCObE";
        const string consumerSecret = "QuxFUBjvfxP2ikkkdH9dItH9KSjSO9lYfAjCfbBRvnalrEnUJA";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //step 1: authenticate twitter client, return twitter user via consumer and token info
                TwitterUser User = authenticate(consumerKey, consumerSecret,
                    accessToken, accessTokenSecret);
                //string twitterAccountToDisplay = User.ScreenName;
                const string twitterAccountToDisplay = "hari_srdhr"; //find another way to impleemt this (tweetsharp)

                //check if this should be done in global.asax file for efficiency
                //session factory design pattern is needed for this
                var documentStore = new DocumentStore
                {
                    Url = "http://localhost:8080",  //subject to change
                    DefaultDatabase = "Twitter_User_Info",
                    //ConnectionStringName = "RavenDB",
                    //insert connection string name?
                };
                documentStore.Initialize();


                var authorizer = new SingleUserAuthorizer
                {
                    //CredentialStore = new SingleUserInMemoryCredentialStore
                    CredentialStore = new SingleUserInMemoryCredentialStore
                    {
                        ConsumerKey = consumerKey,
                        ConsumerSecret = consumerSecret,
                        AccessToken= accessToken,
                        AccessTokenSecret= accessTokenSecret
                    }
                };

                // SECTION C: Generate the Twitter Context
                //Console.WriteLine("SECTION C: Generate the Twitter Context");
                var twitterCtx = new TwitterContext(authorizer);
                List<Friend_Data> friends = new List<Friend_Data>();

                //SEction A: Get a user's friends
                Friendship friendship;
                long cursor = -1;
                do
                {
                        friendship =
                            (from friend in twitterCtx.Friendship
                             where friend.Type == FriendshipType.FriendsList &&
                                   friend.ScreenName == twitterAccountToDisplay &&
                                   friend.Cursor == cursor
                             select friend)
                            .SingleOrDefault();

                    if (friendship != null &&
                        friendship.Users != null &&
                        friendship.CursorMovement != null)
                    {
                        cursor = friendship.CursorMovement.Next;

                        try
                        {
                            foreach (User ind in friendship.Users)
                            {
                                Response.Write(
                                "ID:" + ind.UserIDResponse + " ScreenName:" + ind.ScreenNameResponse + "Name: " + ind.Name + "<br>");
                                //Friend_Data temp = new Friend_Data
                                //{
                                //    userID = ind.UserIDResponse,
                                //    screenName = ind.ScreenNameResponse,
                                //    name = ind.Name,
                                //};
                                //friends.Add(temp);
                            }
                        }
                        catch (AggregateException aggregateException)
                        {
                            aggregateException.Handle(exception =>
                            {
                                Response.Write("Handled exception of type: " + exception.GetType() + "<br>");
                                return true;
                            });
                        }
                    }
                } while (cursor != 0);

                //follower data
                Friendship followers =
                        (from friend in twitterCtx.Friendship
                         where friend.Type == FriendshipType.FollowersList &&
                         friend.ScreenName == twitterAccountToDisplay
                         select friend).SingleOrDefault();

                Response.Write("Followers length is : " + followers.Users.Count());
                Response.Write("Friendship users length is: " + friendship.Users.Count());

                List<Follower_Data> follows = new List<Follower_Data>();
                foreach(User ind in followers.Users)
                {
                        Follower_Data temp = new Follower_Data
                        {
                            userID = ind.UserIDResponse,
                            screenName = ind.ScreenNameResponse,
                            name = ind.Name,
                        };
                    follows.Add(temp);
                }

                var exampleData = new Twitter_Data
                {
                    userId = (User.Id).ToString(),
                    ScreenName = User.ScreenName,
                    Token = accessToken,
                    TokenSecret = accessTokenSecret,
                    isVerified = User.IsVerified.Value.ToString(),
                    Name = User.Name,
                    profImgUrl = User.ProfileImageUrl,
                    followData = follows,
                    friendData = friends,
                    //number_of_friends = friends.Count,
                    //number_of_followers = follows.Count,
                };
                
                //create new product
                using (var session = documentStore.OpenSession())
                {
                    session.Store(exampleData);
                    session.SaveChanges();
                };

                ////step 4: load example product, show data 2 user (need better implementation 4 this)
                //using (var session = documentStore.OpenSession())
                //{
                //    var p = session.Load<Twitter_Data>("Twitter_Datas/641");
                //    InfoList.Items.Add(p.userId.ToString());
                //    InfoList.Items.Add(p.Name);
                //    InfoList.Items.Add(p.ScreenName);
                //    InfoList.Items.Add(p.Token);
                //    InfoList.Items.Add(p.TokenSecret);
                //    InfoList.Items.Add(p.isVerified);
                //    InfoList.Items.Add(p.profImgUrl);
                //    session.SaveChanges();
                //}
            
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
                    var p = session.Load<Twitter_Data>("Twitter_Datas/641");
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

    }
}