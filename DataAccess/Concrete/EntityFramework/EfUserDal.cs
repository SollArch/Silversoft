using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.DTO.Get;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DatabaseContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(Guid userId)
        {
            using var context = new DatabaseContext();
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims
                    on operationClaim.OperationClaimId equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == userId
                select new OperationClaim
                {
                    OperationClaimId = operationClaim.OperationClaimId,
                    OperationClaimName = operationClaim.OperationClaimName
                };
            return result.ToList();
        }

        public UserGetDto GetUserDtoByEmail(string email)
        {
            using var context = new DatabaseContext();
            var result = from user in context.Users
                join solve in context.UserPoints
                    on user.UserId equals solve.UserId
                where user.Email == email
                select new UserGetDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    StudentNumber = user.StudentNumber,
                    Point = context.UserPoints.Where(x => x.UserId == user.UserId).Sum(x => x.Point)
                };
            if (result.FirstOrDefault() != null) return result.FirstOrDefault();
            
            {
                var user = Get(u => u.Email == email);
                return new UserGetDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    StudentNumber = user.StudentNumber,
                    Point = context.UserPoints.Where(x => x.UserId == user.UserId).Sum(x => x.Point)
                };
            }

        }
    }
}