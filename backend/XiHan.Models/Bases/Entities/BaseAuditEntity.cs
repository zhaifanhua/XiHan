﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseAuditEntity
// Guid:515de7d1-a2fa-4d72-8389-d427403ec6f0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:28:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entities;

/// <summary>
/// 审核基类，含主键，新增，修改，删除
/// </summary>
public abstract class BaseAuditEntity : BaseDeleteEntity
{
    /// <summary>
    /// 审核用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核用户主键")]
    public virtual long? AuditedId { get; set; }

    /// <summary>
    /// 审核用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "审核用户名称")]
    public virtual string? AuditedBy { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核时间")]
    public virtual DateTime? AuditedTime { get; set; }
}