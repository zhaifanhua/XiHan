﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RRootRoleMenuDto
// Guid:1eb8ee90-47b5-45d0-afa1-674518ef0360
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-06 上午 02:24:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// RRootRoleMenuDto
/// </summary>
public class RRootRoleMenuDto : BaseResultFieldDto
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public Guid MenuId { get; set; }

    /// <summary>
    /// 系统角色
    /// </summary>
    public Guid RoleId { get; set; }
}