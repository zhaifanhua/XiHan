﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserCollectCategoryService
// Guid:8d79d8c4-0bab-4ab2-8559-70eee3c88d04
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// 用户收藏分类表
/// </summary>
public class UserCollectCategoryService : BaseService<UserCollectCategory>, IUserCollectCategoryService
{
    private readonly IUserCollectCategoryRepository _IUserCollectCategoryRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iUserCollectCategoryRepository"></param>
    public UserCollectCategoryService(IUserCollectCategoryRepository iUserCollectCategoryRepository)
    {
        _IUserCollectCategoryRepository = iUserCollectCategoryRepository;
        base._IBaseRepository = iUserCollectCategoryRepository;
    }
}