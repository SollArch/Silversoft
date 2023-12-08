using System.Collections.Generic;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Event : IEntity
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Links { get; set; }
    }
}