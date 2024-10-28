﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPermissionMenuService
// Guid:5d8dcf77-de3e-4986-aadd-9b3635ba0146
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-11 下午 04:47:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Permissions.Logic;

/// <summary>
/// 系统权限菜单服务(为某权限分派菜单)
/// </summary>
[AppService(ServiceType = typeof(ISysPermissionMenuService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysPermissionMenuService : BaseService<SysPermissionMenuTieup>, ISysPermissionMenuService;