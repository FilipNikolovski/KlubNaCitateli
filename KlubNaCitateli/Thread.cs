using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubNaCitateli
{
    public class Thread
    {
        public int IdForumTopic { get; set; }
        public string TopicName { get; set; }
        public int NumThreads { get; set; }
        public int NumPosts { get; set; }
        public string ThreadName{get; set;}
        public int ThreadNumPosts{get;set;}
        public int IdThread{get; set;}


        public Thread(int id, string name, int threads, int posts)
        {
            IdForumTopic = id;
            TopicName = name;
            NumThreads = threads;
            NumPosts = posts;
            ThreadName = "";
            ThreadNumPosts = 0;
            IdThread = 0;
        }








    }
}