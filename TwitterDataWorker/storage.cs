using Facebook;
using Spring.Social.LinkedIn.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterDataWorker
{
    public class storage
    {
    }
    public class LinkedIn_Data
    {
        public string userId { get; set; }  //user.Id - responsible for id
        public string Token { get; set; }   //_accessToken
        public string TokenSecret { get; set; } //_accessTokenSecret
        public string FirstName{ get; set; }
        public string Lastname { get; set; }
        public string Summary { get; set; }
        public string Industry { get; set; }
        public string profileUrl { get; set; }
        public string headLine { get; set; }
        public int numFirstDeg { get; set; }
        public int numSecondDeg { get; set; }
        public IList<LinkedInProfile> connectData { get; set; }
    }

    //public class Connections_Data
    //{
    //    public string userId { get; set; }  //user.Id - responsible for id
    //    public string FirstName { get; set; }
    //    public string Lastname { get; set; }
    //    public string Summary { get; set; }
    //    public string Industry { get; set; }
    //    public string profileUrl { get; set; }
    //    public string headLine { get; set; }
    //}
    public class Twitter_Data
    {
        public string userId { get; set; }  //user.Id - responsible for id
        public string ScreenName { get; set; }  //user.ScreenName
        public string Token { get; set; }   //_accessToken
        public string TokenSecret { get; set; } //_accessTokenSecret
        public string isVerified { get; set; }
        public string Name { get; set; }
        public string profImgUrl { get; set; }
        public int number_of_friends { get; set; }
        public int number_of_followers { get; set; }
        public List<Friend_Data> friendData { get; set; }
        public List<Follower_Data> followData { get; set; }
    }

    public class Friend_Data
    {
        public string userID { get; set; }
        public string name { get; set; }
        public string screenName { get; set; }
    }

    public class Follower_Data
    {
        public string userID { get; set; }
        public string name { get; set; }
        public string screenName { get; set; }
    }

    public class Facebook_Data
    {
        public string id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string facebookName { get; set; }
        public string gender{ get; set; }
        public string locale{ get; set; }
        public string userName { get; set; }
        //public Facebook_Group_Data[] groups { get; set; }
        public JsonArray groups { get; set; }
    }

    public class Facebook_Group_Data
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}