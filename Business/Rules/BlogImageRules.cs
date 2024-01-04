using System;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;

namespace Business.Rules;

public class BlogImageRules : IBlogImageRules
{
    private readonly IBlogImageDal _blogImageDal;
    private readonly IBlogDal _blogDal;

    public BlogImageRules(IBlogImageDal blogImageDal, IBlogDal blogDal)
    {
        _blogImageDal = blogImageDal;
        _blogDal = blogDal;
    }

    public void CheckIfBlogImageNotExists(Guid id)
    {
        if (_blogImageDal.Get(i => i.Id == id) == null)
        {
            throw new BusinessException(Messages.BlogImageNotFound);
        }
    }

    public void CheckIfBlogHasImage(Guid blogId)
    {
        if (_blogImageDal.Get(i => i.BlogId == blogId) != null)
        {
            throw new BusinessException(Messages.BlogHasImageAlreadyExist);
        }
    }

    public void CheckIfTheAuthorIsTheOneWhoAddedTheImage(Guid blogId, Guid addingMemberId)
    {
        var blog = _blogDal.Get(b => b.Id == blogId);
        if (blog.AuthorId != addingMemberId)
        {
            throw new BusinessException(Messages.BlogImageNotAddedByTheAuthor);
        }
    }
    
}