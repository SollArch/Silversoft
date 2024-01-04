using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO.Get;

namespace DataAccess.Abstract;

public interface IBlogDal : IEntityRepository<Blog>
{
    List<BlogDetailDto> GetAllBlogDetails(Expression<Func<Blog, bool>> filter = null);
    BlogDetailDto GetBlogDetails(Expression<Func<Blog, bool>> filter);
    
}