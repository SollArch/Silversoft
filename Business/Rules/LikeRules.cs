using System;
using Business.Abstract;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO.Get;

namespace Business.Rules;

public class LikeRules : ILikeRules
{
    private readonly ILikeDal _likeDal;
    private readonly IBlogService _blogService;
    public LikeRules(ILikeDal likeDal, IBlogService blogService)
    {
        _likeDal = likeDal;
        _blogService = blogService;
    }

    public void CheckIfLikeExists(Guid likeId)
    {
        var like = _likeDal.Get(l => l.LikeId == likeId);
        if (like == null)
        {
            throw new Exception(Messages.LikeNotFound);
        }
    }

    public void CheckIfUserLikedBefore(Like like)
    {
        var result = _likeDal.Get(l => l.UserId == like.UserId && l.BlogId == like.BlogId);
        if (result != null)
        {
            throw new Exception(Messages.UserLikedBefore);
        }
    }

    public void CheckIfBlogActive(Guid blogId)
    {
        var blogResult = _blogService.GetById(blogId);
        CheckIfBlogExists(blogResult.Data);
        if (!blogResult.Data.IsActive)
        {
            throw new BusinessException(Messages.BlogNotActive);
        }
    }
    private void CheckIfBlogExists(BlogDetailDto blog)
    {
        if (blog == null)
        {
            throw new BusinessException(Messages.BlogNotFound);
        }
    }
    
    public void CheckIfLikedUserIsAuthUser(Guid likedUserId, Guid authUserId)
    {
        if (likedUserId != authUserId)
        {
            throw new BusinessException(Messages.AuthorizationDenied);
        }
    }
}