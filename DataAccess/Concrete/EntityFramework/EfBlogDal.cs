using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.DTO.Get;

namespace DataAccess.Concrete.EntityFramework;

public class EfBlogDal : EfEntityRepositoryBase<Blog, DatabaseContext>, IBlogDal
{
    public List<BlogDetailDto> GetAllBlogDetails(Expression<Func<Blog, bool>> filter = null)
    {
        using var context = new DatabaseContext();

        var filteredBlogs = filter == null ? context.Blogs : context.Blogs.Where(filter);

        var result = from blog in filteredBlogs
            join user in context.Users on blog.AuthorId equals user.UserId
            join blogImage in context.BlogImages on blog.Id equals blogImage.BlogId into images
            from blogImage in images.DefaultIfEmpty() // Left join
            join like in context.Likes on blog.Id equals like.BlogId into likes
            from like in likes.DefaultIfEmpty() // Left join
            let likeCount = context.Likes.Count(l => l.BlogId == blog.Id)
            let likers = context.Likes
                .Where(l => l.BlogId == blog.Id)
                .Select(l =>
                    context.Users.FirstOrDefault(u => u.UserId == l.UserId).UserName) // UserId'yi UserName'e çevir
                .ToList()
            select new BlogDetailDto
            {
                Id = blog.Id,
                Content = blog.Content,
                LikeCount = likeCount,
                Likers = likers,
                Title = blog.Title,
                AuthorUserName = user.UserName,
                ImageUrl = blogImage.ImagePath,
                IsActive = blog.IsActive
            };

        return result.ToList();
    }


    public BlogDetailDto GetBlogDetails(Expression<Func<Blog, bool>> filter)
    {
        using var context = new DatabaseContext();
        var result = from blog in context.Blogs.Where(filter)
            join user in context.Users on blog.AuthorId equals user.UserId
            join blogImage in context.BlogImages on blog.Id equals blogImage.BlogId into images
            from blogImage in images.DefaultIfEmpty() // Left join
            join like in context.Likes on blog.Id equals like.BlogId into likes
            from like in likes.DefaultIfEmpty() // Left join
            let likeCount = context.Likes.Count(l => l.BlogId == blog.Id)
            let likers = context.Likes
                .Where(l => l.BlogId == blog.Id)
                .Select(l =>
                    context.Users.FirstOrDefault(u => u.UserId == l.UserId).UserName) // UserId'yi UserName'e çevir
                .ToList()
            select new BlogDetailDto
            {
                Id = blog.Id,
                Content = blog.Content,
                LikeCount = (from likeCount in context.Likes where likeCount.BlogId == blog.Id select likeCount)
                    .Count(),
                Likers =
                    (from likeUser in context.Users where likeUser.UserId == blog.AuthorId select likeUser.UserName)
                    .ToList(),
                Title = blog.Title,
                AuthorUserName = user.UserName,
                ImageUrl = blogImage.ImagePath,
                IsActive = blog.IsActive
                };
        return result.SingleOrDefault();
    }
}