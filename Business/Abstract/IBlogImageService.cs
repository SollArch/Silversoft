using System;
using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO.Post.BlogImage;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface IBlogImageService
{
    IResult Add(BlogImageAddDto blogImageAddDto);
    IResult Delete(BlogImage blogImage);
    IDataResult<BlogImage> GetByBlogId(Guid id);
    IDataResult<BlogImage> GetById(Guid id);
}