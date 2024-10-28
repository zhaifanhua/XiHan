﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CRootAuthorityDto
// Guid:e063ddee-794e-4927-9617-5f0cc77815b9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-15 下午 06:07:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// CRootAuthorityDto
/// </summary>
public class CRootAuthorityDto
{
    /// <summary>
    /// 父级权限
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(10, ErrorMessage = "{0}不能多于{1}个字")]
    public string AuthName { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(10, ErrorMessage = "{0}不能多于{1}个字")]
    public string AuthType { get; set; } = string.Empty;

    /// <summary>
    /// 权限描述
    /// </summary>
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字")]
    public string? Description { get; set; }
}