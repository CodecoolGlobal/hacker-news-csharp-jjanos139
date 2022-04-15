using System;

namespace Codecool.HackerNewsClient.Models
{
    public class Newest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateOnly TimeAgo { get; set; }
    }
}
