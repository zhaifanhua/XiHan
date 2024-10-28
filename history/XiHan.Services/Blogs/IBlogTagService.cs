﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogTagService
// Guid:e8c119bb-b7ae-29f6-0db5-da415d8eaefb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:16:38
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogTagService
/// </summary>
public interface IBlogTagService : IBaseService<PostTag>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostTag> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogTags"></param>
    /// <returns></returns>
    Task<bool> InitBlogTagAsync(List<PostTag> blogTags);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogTag"></param>
    /// <returns></returns>
    Task<bool> CreateBlogTagAsync(PostTag blogTag);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogTagAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogTag"></param>
    /// <returns></returns>
    Task<PostTag> ModifyBlogTagAsync(PostTag blogTag);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostTag> FindBlogTagAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<PostTag>> QueryBlogTagAsync();
}