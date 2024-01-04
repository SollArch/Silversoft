using System;
using Entities.Concrete;

namespace Business.Rules.Abstract;

public interface IBlogRules
{
    public void CheckIfBlogExists(Guid id);
    public void SetNonUpdateableFields(Blog blog);
    public void CheckIfBlogAlreadyActive(Blog blog);
    public void CheckIfUpdateUserIsAuthor(Guid authorId, Guid authenticatedUserId);
}