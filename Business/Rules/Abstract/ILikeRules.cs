using System;
using Entities.Concrete;

namespace Business.Rules.Abstract;

public interface ILikeRules
{
    public void CheckIfLikeExists(Guid likeId);
    public void CheckIfUserLikedBefore(Like like);
    public void CheckIfBlogActive(Guid blogId);
    public void CheckIfLikedUserIsAuthUser(Guid likedUserId, Guid authUserId);
}