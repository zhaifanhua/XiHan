﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogCommentService
// Guid:1bf19ecb-ac1e-34ed-80c2-81b54781ebdd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:11:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogCommentService
/// </summary>
public interface IBlogCommentService : IBaseService<PostComment>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostComment> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogComments"></param>
    /// <returns></returns>
    Task<bool> InitBlogCommentAsync(List<PostComment> blogComments);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogComment"></param>
    /// <returns></returns>
    Task<bool> CreateBlogCommentAsync(PostComment blogComment);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogCommentAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogComment"></param>
    /// <returns></returns>
    Task<PostComment> ModifyBlogCommentAsync(PostComment blogComment);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostComment> FindBlogCommentAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<PostComment>> QueryBlogCommentAsync();
}