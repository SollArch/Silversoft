using System;
using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO.Get;
using Entities.DTO.Post.Blog;

namespace Business.Abstract;

public interface IBlogService
{
    IResult Add(Blog blog);
    IResult Update(Blog blog);
    IResult Delete(BlogDeleteDto blog);
    IDataResult<List<BlogDetailDto>> GetAllActive();
    IDataResult<List<BlogDetailDto>> GetAllNotActive();
    IDataResult<BlogDetailDto> GetById(Guid id);
    IDataResult<List<BlogDetailDto>> GetByAuthorId(Guid authorId);
    IDataResult<List<BlogDetailDto>> GetByTitle(string title);
}