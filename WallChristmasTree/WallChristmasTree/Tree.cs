using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace WallChristmasTree
{
    class Tree
    {
        Point xy;
        Bitmap bImage;

        public Tree(Profiles user)
        {
            string[] tmp;
            if (user.id == 88589595)
                xy = new Point(745, 518);
            else
            {
                using (StreamReader sr = new StreamReader("point.txt"))
                {
                    tmp = sr.ReadToEnd().Split('\n');

                    xy = new Point(int.Parse(tmp[0].Split(',')[0]), int.Parse(tmp[1].Split(',')[0]));

                }
                using (StreamWriter sw = new StreamWriter("point.txt"))
                {
                    string s = "";
                    for (int i = 1; i < tmp.Length; i++)
                    {
                        s += tmp[0] + "\n";
                    }
                    sw.Write(s);
                }
            }
            bImage = CreateTree();
        }

        Bitmap CreateTree()
        {
            Bitmap bmp = new Bitmap("Tree.png");
            Bitmap ball = new Bitmap(Image.FromFile("tmpT.png"), 137, 250);
            for (int i = xy.X; i < xy.X + 137; i++)
            {
                for (int j = xy.Y; j < xy.Y + 250; j++)
                {
                    Color clr = ball.GetPixel(i - xy.X, j - xy.Y);
                    if (clr.A != 0)
                    {
                        bmp.SetPixel(i, j, clr);
                    }
                }
            }
            return bmp;
        }

        public string ImageT
        {
            get
            {
                bImage.Save("Tree.png", ImageFormat.Png);
                return "Tree.png";
            }
        }
    }
}