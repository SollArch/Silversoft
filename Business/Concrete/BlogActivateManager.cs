using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class BlogActivateManager : IBlogActivateService
{
    private readonly IBlogDal _blogDal;
    private readonly IBlogRules _blogRules;

    public BlogActivateManager(IBlogDal blogDal, IBlogRules blogRules)
    {
        _blogDal = blogDal;
        _blogRules = blogRules;
    }

    [SecuredOperation("admin")]
    [CacheRemoveAspect("IBlogService.Get")]
    public IResult Activate(Guid blogId)
    {
        var blog = _blogDal.Get(b => b.Id == blogId);
        _blogRules.CheckIfBlogAlreadyActive(blog);
        _blogRules.CheckIfBlogExists(blogId);
        blog.IsActive = true;
        _blogDal.Update(blog);
        return new SuccessResult(Messages.BlogActivated);
    }


    [SecuredOperation("admin")]
    [CacheRemoveAspect("IBlogService.Get")]
    public IResult Deactivate(Guid blogId)
    {
        var blog = _blogDal.Get(b => b.Id == blogId);
        _blogRules.CheckIfBlogExists(blogId);
        blog.IsActive = false;
        _blogDal.Update(blog);
        return new SuccessResult(Messages.BlogDeactivated);
    }
}