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
            wallPost = new Post("1047", "63026589");
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
            new Balls();
            //timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<Profiles> likes = wallPost.GetLikes();
            foreach(Profiles user in likes)
            {
                if(!worked.items.Contains(user))
                {
                    Balls ball = new Balls(user);
                    Tree tree = new Tree(user);
                    //Редачим Ёлку
                    wallPost.CreateComment(user, ball);
                    wallPost.AddPhoto(tree);
                    worked.items.Add(user);
                    Serialize();
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
