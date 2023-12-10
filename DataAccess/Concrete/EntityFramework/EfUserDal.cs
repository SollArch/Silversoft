using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User,DatabaseContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(int userId)
        {
            using (var context = new DatabaseContext())
            {
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
        }
    }
}