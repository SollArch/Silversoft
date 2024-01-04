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
            var userDto = (from user in context.Users
                where user.Email == email
                select new UserGetDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    StudentNumber = user.StudentNumber,
                    Point = user.Point
                }).FirstOrDefault();

            if (userDto == null) return null;
            userDto.Point += context.UserPoints
                .Where(up => up.UserId == userDto.UserId)
                .Sum(up => up.Point);
                
            userDto.Point += context.CtfSolves
                .Where(solve => solve.UserId == userDto.UserId)
                .Join(context.Ctfs,
                    solve => solve.CtfId,
                    ctf => ctf.Id,
                    (solve, ctf) => ctf.Point)
                .Sum();

            return userDto;
        }
    }
}