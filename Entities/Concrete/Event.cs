using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Links { get; set; }
    }
}