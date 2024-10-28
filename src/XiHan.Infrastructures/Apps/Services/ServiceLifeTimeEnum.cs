﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceLifeTimeEnum
// Guid:a690c369-797c-4713-a4a3-5ea80782de34
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-20 下午 03:16:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructures.Apps.Services;

/// <summary>
/// 服务生命周期
/// </summary>
public enum ServiceLifeTimeEnum
{
    /// <summary>
    /// 单例
    /// </summary>
    [Description("单例")] Singleton,

    /// <summary>
    /// 作用域
    /// </summary>
    [Description("作用域")] Scoped,

    /// <summary>
    /// 瞬时
    /// </summary>
    [Description("瞬时")] Transient
}