using System;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Rules;

public class BlogRules : IBlogRules
{
    private readonly IBlogDal _blogDal;

    public BlogRules(IBlogDal blogDal)
    {
        _blogDal = blogDal;
    }

    public void CheckIfBlogExists(Guid id)
    {
        var blog = _blogDal.Get(b => b.Id == id);
        if (blog == null)
        {
            throw new BusinessException(Messages.BlogNotFound);
        }
    }

    public void SetNonUpdateableFields(Blog blog)
    {
        var blogToBeUpdated = _blogDal.Get(b => b.Id == blog.Id);
        blog.IsActive = false;
        blog.AuthorId = blogToBeUpdated.AuthorId; 
    }

    public void CheckIfBlogAlreadyActive(Blog blog)
    {
        if(blog.IsActive)
            throw new BusinessException(Messages.BlogAlreadyActive);
    }

    public void CheckIfUpdateUserIsAuthor(Guid authorId, Guid authenticatedUserId)
    {
        if (authorId != authenticatedUserId)
        {
            throw new BusinessException(Messages.AuthorizationDenied);
        }
    }
}