
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Ctf : IEntity
    {
        public int CtfId { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string QuestionImageUrl { get; set; }
        public string Answer { get; set; }
        public int Point { get; set; }
        public int NumberOfSolve { get; set; }
    }
}