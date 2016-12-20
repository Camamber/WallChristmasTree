using System.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WallChristmasTree
{
    class Post
    {
        string post_id, owner_id;
        public Post(string post_id, string owner_id)
        {
            this.post_id = post_id;
            this.owner_id = owner_id;
        }

        public List<Profiles> GetLikes()
        {
            List<Profiles> users = new List<Profiles>();
            using (WebClient wc = new WebClient())
            {
                LikeObject likes = JsonConvert.DeserializeObject<LikeObject>(wc.DownloadString(String.Format("https://api.vk.com/method/likes.getList?type=post&owner_id={0}&item_id={1}&extended=1&access_token={2}&v=5.60", owner_id, post_id, Resources.token)));
                users = likes.response.items;
            }
            return users;
        }

        bool SendComment(string msg, string attachment = "0")
        {
            using (WebClient wc = new WebClient())
            {
              string s = wc.DownloadString(String.Format("https://api.vk.com/method/wall.createComment?owner_id={0}&post_id={1}&messages={2}&attachments={3}&access_token={4}&v=5.60", owner_id, post_id, msg, attachment, Resources.token));
            }
            return true;
        }

        public void CreateComment(Profiles user, Balls ball)
        {
            using (WebClient wc = new WebClient())
            {
                UploadServer uploadServer = JsonConvert.DeserializeObject<UploadServer>(wc.DownloadString(String.Format("https://api.vk.com/method/photos.getUploadServer?album_id=239545490&access_token={0}&v=5.60", Resources.token)));
                UploadedPhoto uploadedPhoto = JsonConvert.DeserializeObject<UploadedPhoto>(Encoding.ASCII.GetString(wc.UploadFile(uploadServer.response.upload_url, ball.Image)));
                PhotoInformation image = JsonConvert.DeserializeObject<PhotoInformation>(wc.DownloadString(string.Format("https://api.vk.com/method/photos.save?album_id=239545490&photos_list={1}&server={2}&hash={3}&v=5.37&access_token={0}", Resources.token, uploadedPhoto.photos_list, uploadedPhoto.server, uploadedPhoto.hash)));
                string attachment = String.Format("photo{0}_{1}", image.response[0].owner_id, image.response[0].id);
                string messages = String.Format("*id{0} ({1}), Этот шарик я сделал специально для тебя, я старался(не забывай, я всего лишь машина). Теперь и ты есть на моей елочке. Надеюсь тебе понравилось😌. С наступающим тебя!!!🍊🍷🍸🎁🎅🎄🌟", user.id, user.first_name);
                SendComment(messages, attachment);
            }
        }

        public void EditPhoto(Tree tree)
        {
            using (WebClient wc = new WebClient())
            {
                UploadServer uploadServer = JsonConvert.DeserializeObject<UploadServer>(wc.DownloadString(String.Format("https://api.vk.com/method/photos.getUploadServer?album_id=239545490&access_token={0}&v=5.60", Resources.token)));
                UploadedPhoto uploadedPhoto = JsonConvert.DeserializeObject<UploadedPhoto>(Encoding.ASCII.GetString(wc.UploadFile(uploadServer.response.upload_url, tree.Image)));
                PhotoInformation image = JsonConvert.DeserializeObject<PhotoInformation>(wc.DownloadString(string.Format("https://api.vk.com/method/photos.save?album_id=239545490&photos_list={1}&server={2}&hash={3}&v=5.37&access_token={0}", Resources.token, uploadedPhoto.photos_list, uploadedPhoto.server, uploadedPhoto.hash)));
                string attachment = String.Format("photo{0}_{1}", image.response[0].owner_id, image.response[0].id);
                string messages = "";
                wc.DownloadString(String.Format("https://api.vk.com/method/wall.edit?owner_id={0}&post_id={1}&messages={2}&attachments={3}&access_token={4}&v=5.60", owner_id, post_id, messages, attachment, Resources.token));
            }
        }
    }
}