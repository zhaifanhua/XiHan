﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysRole
// Guid:26b82e42-87a7-477b-af80-59456336a22b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:42:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统角色表
/// </summary>
/// <remarks>记录新增，修改，删除信息</remarks>
[SugarTable, SystemTable]
public class SysRole : BaseDeleteEntity
{
    /// <summary>
    /// 父级角色
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 角色编码
    /// 如：admin
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 角色排序
    /// </summary>
    [SugarColumn]
    public int SortOrder { get; set; }

    /// <summary>
    /// 用户个数
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public int UserCount { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? Description { get; set; }
}