using System;
using Core.Entities.Abstract;

namespace Entities.Concrete;

public class CtfQuestionImage : IEntity
{
    public Guid Id { get; set; }
    public Guid CtfId { get; set; }
    public string ImagePath { get; set; }
}