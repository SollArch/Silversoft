
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Blog : IEntity
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int LikeCount { get; set; }
    }
} 