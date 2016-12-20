using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace WallChristmasTree
{
    class Balls
    {
        Bitmap bImage;
        Bitmap face;
        UserObject userobj;
        public Balls(Profiles profile)
        {
            using(WebClient wc = new WebClient())
            {
                userobj = JsonConvert.DeserializeObject<UserObject>(wc.DownloadString(String.Format("https://api.vk.com/method/users.get?users_id={1}&access_token={0}&v=5.60", Resources.token, profile.id)));
                MemoryStream ms = new MemoryStream(wc.DownloadData(userobj.response[0].photo_200));
                face = new Bitmap(ms);
            }              
        }

        void CreateBall()
        {

        }

        public string Image
        {
            get
            {
                bImage.Save("tmp.png", ImageFormat.Png);
                return "tmp.png";
            }
        }
    }
}
