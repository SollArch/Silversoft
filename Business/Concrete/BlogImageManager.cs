using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO.Post.BlogImage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete;

public class BlogImageManager : IBlogImageService
{
    private readonly IBlogImageDal _blogImageDal;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IBlogService _blogService;
    private readonly IBlogImageRules _blogImageRules;
    private readonly IBlogRules _blogRules;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BlogImageManager(IBlogImageDal blogImageDal, IBlogImageRules blogImageRules, ICloudinaryService cloudinaryService, IBlogService blogService, IBlogRules blogRules)
    {
        _blogImageDal = blogImageDal;
        _blogImageRules = blogImageRules;
        _cloudinaryService = cloudinaryService;
        _blogService = blogService;
        _blogRules = blogRules;
        _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    [SecuredOperation("admin,student")]
    [CacheRemoveAspect("IBlogImageService.Get,IBlogService.Get")]
    public IResult Add(BlogImageAddDto blogImageAddDto)
    {
        var requestedUserId = JwtHelper.GetAuthenticatedUserId();
        _blogRules.CheckIfBlogExists(blogImageAddDto.BlogId);
        _blogImageRules.CheckIfBlogHasImage(blogImageAddDto.BlogId);
        _blogImageRules.CheckIfTheAuthorIsTheOneWhoAddedTheImage(blogImageAddDto.BlogId, requestedUserId);
        var blog = _blogService.GetById(blogImageAddDto.BlogId).Data;
        var uploadResult = _cloudinaryService.Upload(blogImageAddDto.FormFile,blog.Title);
        Guid.Parse(uploadResult.Data);
        var blogImage = new BlogImage
        {
            ImagePath= uploadResult.Message, 
            Id = Guid.Parse(uploadResult.Data),
            BlogId = blogImageAddDto.BlogId
        };
          _blogImageDal.Add(blogImage);
        return new SuccessResult(Messages.BlogImageAdded);
    }

    [SecuredOperation("admin,student")]
    [CacheRemoveAspect("IBlogImageService.Get,IBlogService.Get")]
    public IResult Delete(BlogImage blogImage)
    {
        var requestedUserId = JwtHelper.GetAuthenticatedUserId();
        _blogImageRules.CheckIfTheAuthorIsTheOneWhoAddedTheImage(blogImage.BlogId, requestedUserId);
        _blogRules.CheckIfBlogExists(blogImage.BlogId);
        _blogImageRules.CheckIfBlogImageNotExists(blogImage.Id);
        var destroyResult = _cloudinaryService.Destroy(blogImage.Id.ToString());
        _blogImageDal.Delete(blogImage);
        return destroyResult;
    }
    
    [CacheAspect]
    public IDataResult<BlogImage> GetByBlogId(Guid id)
    {
        var result = _blogImageDal.Get(b => b.BlogId == id);
        return new SuccessDataResult<BlogImage>(result);
    }

    [CacheAspect]
    public IDataResult<BlogImage> GetById(Guid id)
    {
        var result = _blogImageDal.Get(b => b.Id == id);
        return new SuccessDataResult<BlogImage>(result);
    }
    
}