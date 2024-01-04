using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Blog : IEntity
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        
        
    }
} 