using System;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IBlogActivateService
{
    IResult Activate(Guid blogId);
    IResult Deactivate(Guid blogId);
}