using System;
using System.Collections.Generic;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Event : IEntity
    {
        public Guid EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}