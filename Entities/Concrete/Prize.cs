using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Prize : IEntity
    {
        public int PrizeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}