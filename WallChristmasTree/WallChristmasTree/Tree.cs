using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallChristmasTree
{
    class Tree
    {
        Bitmap bImage;
        public Tree(Profiles user)
        {

        }

        public string Image
        {
            get
            {
                bImage.Save("Tree.png", ImageFormat.Png);
                return "Tree.png";
            }
        }
    }
}
