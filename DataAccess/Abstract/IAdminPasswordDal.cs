using Core.DataAccess;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IAdminPasswordDal : IEntityRepository<AdminPassword>
{
    
}