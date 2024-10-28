﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysMenu
// Guid:9add3fb4-8c8d-4f8c-92d2-ec35ee45fc20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-08-04 下午 01:23:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统菜单表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysMenu : BaseModifyEntity
{
    /// <summary>
    /// 父级菜单
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 菜单编码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 菜单名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 菜单图标
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// 路由地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// 路由参数
    ///</summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? Query { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? ComponentPath { get; set; }

    /// <summary>
    /// 菜单排序
    /// </summary>
    [SugarColumn]
    public int SortOrder { get; set; }

    /// <summary>
    /// 是否为外部链接
    ///</summary>
    [SugarColumn]
    public bool IsLink { get; set; }

    /// <summary>
    /// 是否缓存
    ///</summary>
    [SugarColumn]
    public bool IsCache { get; set; }

    /// <summary>
    /// 是否显示
    ///</summary>
    [SugarColumn]
    public bool IsShow { get; set; }

    /// <summary>
    /// 菜单描述
    /// </summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? Description { get; set; }
}