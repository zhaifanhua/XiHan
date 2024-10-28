﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobLogWDto
// Guid:3fcd7683-0372-44e9-a5be-273ae32aa0e7
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-21 下午 05:35:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Jobs.Dtos;

/// <summary>
/// SysJobLogWDto
/// </summary>
public class SysJobLogWDto
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public string? JobName { get; set; }

    /// <summary>
    /// 任务分组
    /// </summary>
    public string? JobGroup { get; set; }

    /// <summary>
    /// 执行是否成功(1正常 0失败)
    /// </summary>
    public bool? IsSuccess { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}