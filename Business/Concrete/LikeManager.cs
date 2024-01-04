using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Rules.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO.Post.Like;

namespace Business.Concrete;

public class LikeManager : ILikeService
{
    private readonly ILikeDal _likeDal;
    private readonly ILikeRules _likeRules;

    public LikeManager(ILikeDal likeDal, ILikeRules likeRules)
    {
        _likeDal = likeDal;
        _likeRules = likeRules;
    }

    [SecuredOperation("admin,student,member")]
    [CacheRemoveAspect("IBlogService.Get")]
    public IResult Like(LikeDto likeDto)
    {
        _likeRules.CheckIfBlogActive(likeDto.BlogId);
        var like = new Like
        {
            BlogId = likeDto.BlogId,
            UserId = JwtHelper.GetAuthenticatedUserId(),
            LikeId = Guid.NewGuid()
        };
        _likeRules.CheckIfUserLikedBefore(like);
        _likeDal.Add(like);
        return new SuccessResult();
    }

    [SecuredOperation("admin,student,member")]
    [CacheRemoveAspect("IBlogService.Get")]
    public IResult Unlike(Like like)
    {
        var userId = JwtHelper.GetAuthenticatedUserId();
        _likeRules.CheckIfLikedUserIsAuthUser(like.UserId, userId);
        _likeRules.CheckIfBlogActive(like.BlogId);
        _likeRules.CheckIfLikeExists(like.LikeId);
        _likeDal.Delete(like);
        return new SuccessResult();
    }
}