using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete.EntityFramework;

public class EfCloudinaryConnectionDal : EfEntityRepositoryBase<CloudinaryConnection,DatabaseContext>,ICloudinaryConnectionDal
{
    
}