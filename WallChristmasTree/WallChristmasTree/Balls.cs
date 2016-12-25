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
        Bitmap ball;
        UserObject userobj;
        public Balls(Profiles profile)
        {
            using(WebClient wc = new WebClient())
            {
                userobj = JsonConvert.DeserializeObject<UserObject>(wc.DownloadString(String.Format("https://api.vk.com/method/users.get?users_id={1}&access_token={0}&v=5.60", Resources.token, profile.id)));
                MemoryStream ms = new MemoryStream(wc.DownloadData(userobj.response[0].photo_200));
                face = new Bitmap(ms);
            }              
            if(face!=null&&ball!=null)
            {
                CreateBall();
            }
        }

        Bitmap CreateBall()
        {
            Bitmap res = (Bitmap)ball.Clone();

            for (int i = 28; i < face.Width+28; i++)
            {
                for (int j = 833; j < face.Height+833; j++)
                {
                    Color clr = face.GetPixel(i, j);
                    if (clr.A ==0)
                    {
                        int R = convSoftLight(face.GetPixel(i, j).R, ball.GetPixel(i, j).R);
                        int G = convSoftLight(face.GetPixel(i, j).G, ball.GetPixel(i, j).G);
                        int B = convSoftLight(face.GetPixel(i, j).B, ball.GetPixel(i, j).B);
                        bImage.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                }
            }
            return res;
        }

        int convSoftLight(int A, int B)
        {

            float a = (float)A / 255;
            float b = (float)B / 255;
            float result = 0;

            if (b < 0.5)
                result = (float)(2 * a * b + Math.Pow(a, 2) * (1 - 2 * b));
            else
                result = (float)(2 * a * (1 - b) + Math.Sqrt(a) * (2 * b - 1));

            return (int)(255 * result);
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
