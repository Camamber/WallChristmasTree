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
using System.Drawing.Drawing2D;

namespace WallChristmasTree
{
    class Balls
    {
        Random rnd = new Random();
        Bitmap bImage;
        Bitmap face;
        Bitmap ball;
        UserObject userobj;
        public Balls(Profiles profile)
        {
            using (WebClient wc = new WebClient())
            {
                userobj = JsonConvert.DeserializeObject<UserObject>(wc.DownloadString(String.Format("https://api.vk.com/method/users.get?user_id={1}&fields=photo_200&access_token={0}&v=5.60", Resources.token, profile.id)));
                MemoryStream ms = new MemoryStream(wc.DownloadData(userobj.response[0].photo_200));
                face = CropToCircle(new Bitmap(Image.FromStream(ms), 920, 920), Color.Transparent);
            }
            if (profile.id == 88589595)
                ball = new Bitmap(@"source\3.png");
            else
            {
                string path = String.Format(@"source\{0}.png", rnd.Next(1, 6));
                ball = new Bitmap(path);
            }
           
            if (face != null && ball != null)
            {
                bImage = CreateBall();
            }        
        }

        public Balls()
        {
            Image Tmpimg = Image.FromFile(@"C:\Users\Egerb\Desktop\mega super ruly\pB0RrwxZlRo.jpg");
            face = CropToCircle(new Bitmap(Tmpimg, 920, 920), Color.Transparent);
            face.Save("face.png", ImageFormat.Png);
            ball = new Bitmap(@"C:\Users\Egerb\Desktop\mega super ruly\1.png");
            bImage = CreateBall();
            string g = SaveImage;
            
        }

        Bitmap CreateBall()
        {
            Bitmap res = (Bitmap)ball.Clone();

            for (int i = 22; i < face.Width+22; i++)
            {
                for (int j = 829; j < face.Height + 829; j++)
                {
                    Color clr = face.GetPixel(i - 22, j - 829);
                    if (clr.A !=0)
                    {
                        int R = convSoftLight(face.GetPixel(i - 22, j - 829).R, ball.GetPixel(i, j).R);
                        int G = convSoftLight(face.GetPixel(i - 22, j - 829).G, ball.GetPixel(i, j).G);
                        int B = convSoftLight(face.GetPixel(i - 22, j - 829).B, ball.GetPixel(i, j).B);
                        res.SetPixel(i, j, Color.FromArgb(R, G, B));
                   
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

        public static Bitmap CropToCircle(Image srcImage, Color backGround)
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            Graphics g = Graphics.FromImage(dstImage);
            using (Brush br = new SolidBrush(backGround))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, dstImage.Width, dstImage.Height);
            g.SetClip(path);
            g.DrawImage(srcImage, 0, 0);

            return dstImage;
        }

        public string SaveImage
        {
            get
            {
                bImage.Save("tmp.png", ImageFormat.Png);
                return "tmp.png";
            }
        }
    }
}
