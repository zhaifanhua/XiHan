﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserLoginService
// Guid:4fe56a1d-1a0f-415f-a38f-677ddafef307
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:04:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Syses;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// 用户登录
/// </summary>
public class UserLoginService : BaseService<SysLoginLog>, IUserLoginService
{
    private readonly IUserLoginRepository _IUserLoginRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iUserLoginRepository"></param>
    public UserLoginService(IUserLoginRepository iUserLoginRepository)
    {
        _IUserLoginRepository = iUserLoginRepository;
        base._IBaseRepository = iUserLoginRepository;
    }
}