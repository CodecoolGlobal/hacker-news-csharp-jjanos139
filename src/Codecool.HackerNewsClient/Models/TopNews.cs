using System;

namespace Codecool.HackerNewsClient.Models
{
    public class TopNews
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string URL { get; set; }
        public int TimeAgo { get; set; }
    }
}
