﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CRootRoleDto
// Guid:15e96d08-47b5-44a7-a8ea-fa34723dff54
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-20 上午 12:23:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// CRootRoleDto
/// </summary>
public class CRootRoleDto
{
    /// <summary>
    /// 父级角色
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(10, ErrorMessage = "{0}不能多于{1}个字")]
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 角色描述
    /// </summary>
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字")]
    public string? Description { get; set; }
}