﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogFriendlyLink
// Guid:6580a424-f371-43eb-aafb-5ed17facb1aa
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:49:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Blogs;

/// <summary>
/// 博客友情链接表
/// </summary>
/// <remarks>记录新增，修改，删除，审核，状态信息</remarks>
[SugarTable]
public class BlogFriendlyLink : BaseEntity
{
    /// <summary>
    /// 友链名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(Length = 256)]
    public string AvatarPath { get; set; } = "/Images/BlogFriendlyLink/Avatar/default.png";

    /// <summary>
    /// 友链地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 友链描述
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Description { get; set; }
}