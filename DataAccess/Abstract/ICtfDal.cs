using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO.Get;

namespace DataAccess.Abstract;

public interface ICtfDal : IEntityRepository<Ctf>
{
    public List<CtfGetDto> GetAllWithDto(Expression<Func<Ctf, bool>> filter = null);
}