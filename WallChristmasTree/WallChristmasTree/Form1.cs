using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace WallChristmasTree
{
    
    public partial class Form1 : Form
    {
        Post wallPost;
        SerialProfiel worked = new SerialProfiel();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wallPost = new Post("1065", "63026589");
            if (File.Exists("Liked.json"))
            {
                using (StreamReader sr = new StreamReader("Liked.json"))
                    worked = JsonConvert.DeserializeObject<SerialProfiel>(sr.ReadToEnd());
            }
            else
            {
                worked.items = new List<Profiles>();
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Start();
            //timer1_Tick(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<int> ls = new List<int>();
            foreach (Profiles user2 in worked.items)
                ls.Add(user2.id);
            List<Profiles> likes = wallPost.GetLikes();
            foreach (Profiles user in likes)
            {

                if (!ls.Contains(user.id))
                {
                    Balls ball = new Balls(user);
                    wallPost.CreateComment(user, ball);
                    if (likes.Count < 25)
                    {
                        Tree tree = new Tree(user);
                        wallPost.AddPhoto(tree);
                    }
                    else if (user.id == 88589595)
                    {
                        Tree tree = new Tree(user);
                        wallPost.AddPhoto(tree);

                    }
                    //Редачим Ёлку


                    worked.items.Add(user);
                    Serialize();
                    break;
                }
            }          
        }
        

        void Serialize()
        {
            using (StreamWriter sr = new StreamWriter("Liked.json"))
                sr.Write(JsonConvert.SerializeObject(worked));

        }
    }
}
