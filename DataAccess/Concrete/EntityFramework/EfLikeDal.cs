using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfLikeDal : EfEntityRepositoryBase<Like,DatabaseContext>, ILikeDal
{
    
}