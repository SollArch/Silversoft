namespace Entities.Concrete
{
    public class Blog
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int LikeCount { get; set; }
    }
}