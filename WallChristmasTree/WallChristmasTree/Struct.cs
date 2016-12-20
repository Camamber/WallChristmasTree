using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallChristmasTree
{
    public class Resources
    {
       public const string token = "a3073a9dcbc855d50cad6edaad8b09447376819d6841ec768ea85b14ce15590fb4cd439d491a36fd1480b";
    }



    /// <summary>
    /// Likes.Get
    /// </summary>
    public class Profiles
    {
        public string type { get; set; }
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class ResponseLikes
    {
        public int count { get; set; }
        public List<Profiles> items { get; set; }
    }

    public class SerialProfiel
    {
        public List<Profiles> items { get; set; }
    }

    public class LikeObject
    {
        public ResponseLikes response { get; set; }
    }



    /// <summary>
    /// photo.GetUploadServer
    /// </summary>
    public class UpServerResponse
    {
        public string upload_url { get; set; }
        public int album_id { get; set; }
        public int user_id { get; set; }
    }

    public class UploadServer
    {
        public UpServerResponse response { get; set; }
    }



    /// <summary>
    /// Post request upload photo
    /// </summary>
    public class UploadedPhoto
    {
        public int server { get; set; }
        public string photos_list { get; set; }
        public int aid { get; set; }
        public string hash { get; set; }
    }



    /// <summary>
    /// photo.save
    /// </summary>
    public class ResponseInf
    {
        public int id { get; set; }
        public int album_id { get; set; }
        public int owner_id { get; set; }
        public string photo_75 { get; set; }
        public string photo_130 { get; set; }
        public string photo_604 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string text { get; set; }
        public int date { get; set; }
    }

    public class PhotoInformation
    {
        public List<ResponseInf> response { get; set; }
    }


    /// <summary>
    /// User.Get
    /// </summary>
    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo_200 { get; set; }
    }

    public class UserObject
    {
        public List<User> response { get; set; }
    }
}
