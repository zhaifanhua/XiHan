﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentPollService
// Guid:33c38140-d18e-4566-9bc0-3778fc44d069
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCommentPollService
/// </summary>
public class BlogCommentPollService : BaseService<CommentPoll>, IBlogCommentPollService
{
    private readonly IBlogCommentPollRepository _IBlogCommentPollRepository;
    private readonly IBlogCommentService _IBlogCommentService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogCommentPollRepository"></param>
    /// <param name="iBlogCommentService"></param>
    public BlogCommentPollService(IBlogCommentPollRepository iBlogCommentPollRepository,
        IBlogCommentService iBlogCommentService)
    {
        base._IBaseRepository = iBlogCommentPollRepository;
        _IBlogCommentPollRepository = iBlogCommentPollRepository;
        _IBlogCommentService = iBlogCommentService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<CommentPoll> IsExistenceAsync(Guid guid)
    {
        var blogCommentPoll = await _IBlogCommentPollRepository.FindAsync(e => e.BaseId == guid && !e.SoftDelete);
        if (blogCommentPoll == null)
            throw new ApplicationException("博客文章评论点赞不存在");
        return blogCommentPoll;
    }

    /// <summary>
    /// 初始化博客评论点赞
    /// </summary>
    /// <param name="blogCommentPolls"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogCommentPollAsync(List<CommentPoll> blogCommentPolls)
    {
        blogCommentPolls.ForEach(blogCommentPoll =>
        {
            blogCommentPoll.SoftDelete = false;
        });
        var result = await _IBlogCommentPollRepository.CreateBatchAsync(blogCommentPolls);
        return result;
    }

    /// <summary>
    /// 新增博客评论点赞
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    public async Task<bool> CreateBlogCommentPollAsync(CommentPoll blogCommentPoll)
    {
        await _IBlogCommentService.IsExistenceAsync(blogCommentPoll.CommentId);
        blogCommentPoll.SoftDelete = false;
        var result = await _IBlogCommentPollRepository.CreateAsync(blogCommentPoll);
        return result;
    }

    /// <summary>
    /// 删除博客评论点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogCommentPollAsync(Guid guid, Guid deleteId)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        blogCommentPoll.SoftDelete = true;
        blogCommentPoll.DeleteId = deleteId;
        blogCommentPoll.DeleteTime = DateTime.Now;
        return await _IBlogCommentPollRepository.UpdateAsync(blogCommentPoll);
    }

    /// <summary>
    /// 修改博客评论点赞
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    public async Task<CommentPoll> ModifyBlogCommentPollAsync(CommentPoll blogCommentPoll)
    {
        await IsExistenceAsync(blogCommentPoll.BaseId);
        await _IBlogCommentService.IsExistenceAsync(blogCommentPoll.CommentId);
        var result = await _IBlogCommentPollRepository.UpdateAsync(blogCommentPoll);
        if (result)
        {
            blogCommentPoll = await _IBlogCommentPollRepository.FindAsync(blogCommentPoll.BaseId);
        }

        return blogCommentPoll;
    }

    /// <summary>
    /// 查找博客评论点赞
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<CommentPoll> FindBlogCommentPollAsync(Guid guid)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        return blogCommentPoll;
    }

    /// <summary>
    /// 查询博客评论点赞
    /// </summary>
    /// <returns></returns>
    public async Task<List<CommentPoll>> QueryBlogCommentPollAsync()
    {
        var blogCommentPoll = from blogtag in await _IBlogCommentPollRepository.QueryListAsync(e => !e.SoftDelete)
                              orderby blogtag.CreateTime descending
                              select blogtag;
        return blogCommentPoll.ToList();
    }
}