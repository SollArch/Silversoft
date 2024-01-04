using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.DTO.Get;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Concrete.EntityFramework;

public class EfCtfDal : EfEntityRepositoryBase<Ctf,DatabaseContext>,ICtfDal
{
    public List<CtfGetDto> GetAllWithDto(Expression<Func<Ctf, bool>> filter = null)
    {
        using var context = new DatabaseContext();
        Func<string, int> calculateHintCount = hint =>
            string.IsNullOrEmpty(hint) ? 0 : hint.Contains(',') ? hint.Split(',').Length : 1;
        var result = from ctf in filter == null ? context.Ctfs : context.Ctfs.Where(filter)
            select new CtfGetDto
            {
                Id = ctf.Id,
                Title = ctf.Title,
                Question = ctf.Question,
                Point = ctf.Point,
                SolvabilityLimit = ctf.SolvabilityLimit,
                NumberOfSolve = ctf.NumberOfSolve,
                HintCount = calculateHintCount(ctf.Hint),
                IsActive = ctf.IsActive
            };
        return result.ToList();
    }
}