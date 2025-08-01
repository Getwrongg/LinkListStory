using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkListStory
{
    public class StoryNode
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public List<int> NextIds { get; set; } = new List<int>();
    }
}
