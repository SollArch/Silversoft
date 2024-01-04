using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO.Post.Like;

namespace Business.Abstract;

public interface ILikeService
{
    IResult Like(LikeDto likeDto);
    IResult Unlike(Like like);
}