using System;

namespace Business.Rules.Abstract;

public interface IBlogImageRules
{  
    public void CheckIfBlogImageNotExists(Guid id);
    public void CheckIfBlogHasImage(Guid blogId);
    public void CheckIfTheAuthorIsTheOneWhoAddedTheImage(Guid blogId, Guid addingMemberId);
}