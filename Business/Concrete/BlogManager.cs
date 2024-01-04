using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Rules.Abstract;
using Business.ValidationRules.FluentValdation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO.Get;
using Entities.DTO.Post.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete;

public class BlogManager : IBlogService
{
    private readonly IBlogDal _blogDal;
    private readonly IBlogRules _blogRules;

    public BlogManager(IBlogDal blogDal, IBlogRules blogRules)
    {
        _blogDal = blogDal;
        _blogRules = blogRules;
        ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    [SecuredOperation("admin,student")]
    [ValidationAspect(typeof(BlogValidator))]
    [CacheRemoveAspect("IBlogService.Get")]
    public IResult Add(Blog blog)
    {
        blog.AuthorId = JwtHelper.GetAuthenticatedUserId();
        blog.IsActive = false;
        blog.Id = Guid.NewGuid();
        _blogDal.Add(blog);
        return new SuccessResult(Messages.BlogAdded);
    }

    [SecuredOperation("admin,student")]
    [ValidationAspect(typeof(BlogValidator))]
    [CacheRemoveAspect("IBlogService.Get")]
    public IResult Update(Blog blog)
    {
        var authenticatedUserId =JwtHelper.GetAuthenticatedUserId();
        _blogRules.CheckIfUpdateUserIsAuthor(blog.AuthorId, authenticatedUserId);
        _blogRules.CheckIfBlogExists(blog.Id);
        _blogRules.SetNonUpdateableFields(blog);
        _blogDal.Update(blog);
        return new SuccessResult(Messages.BlogUpdated);
    }

    [SecuredOperation("admin,student")]
    [ValidationAspect(typeof(BlogValidator))]
    [CacheRemoveAspect("IBlogService.Get")]
    public IResult Delete(BlogDeleteDto blogDeleteDto)
    {
        var authenticatedUserId = JwtHelper.GetAuthenticatedUserId();
        var blog = _blogDal.Get(b => b.Id == blogDeleteDto.Id);
        _blogRules.CheckIfUpdateUserIsAuthor(blog.AuthorId, authenticatedUserId);
        _blogRules.CheckIfBlogExists(blogDeleteDto.Id);
        _blogDal.Delete(blog);
        return new SuccessResult(Messages.BlogDeleted);
    }

    [CacheAspect]
    public IDataResult<List<BlogDetailDto>> GetAllActive()
    {
        return new SuccessDataResult<List<BlogDetailDto>>(_blogDal.GetAllBlogDetails(b => b.IsActive == true));
    }

    [SecuredOperation("admin")]
    [CacheAspect]
    public IDataResult<List<BlogDetailDto>> GetAllNotActive()
    {
        return new SuccessDataResult<List<BlogDetailDto>>(_blogDal.GetAllBlogDetails(b => b.IsActive == false));
    }

    [CacheAspect]
    public IDataResult<BlogDetailDto> GetById(Guid id)
    {
        return new SuccessDataResult<BlogDetailDto>(_blogDal.GetBlogDetails(b => b.Id.Equals(id)));
    }

    [CacheAspect]
    public IDataResult<List<BlogDetailDto>> GetByAuthorId(Guid authorId)
    {
        return new SuccessDataResult<List<BlogDetailDto>>(_blogDal.GetAllBlogDetails(b => b.AuthorId == authorId));
    }

    [CacheAspect]
    public IDataResult<List<BlogDetailDto>> GetByTitle(string title)
    {
        return new SuccessDataResult<List<BlogDetailDto>>(_blogDal.GetAllBlogDetails(b => b.Title.ToLower().Contains(title.ToLower())));
    }
    
}