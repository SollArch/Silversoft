using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfBlogImageDal : EfEntityRepositoryBase<BlogImage,DatabaseContext>, IBlogImageDal
{
    
}