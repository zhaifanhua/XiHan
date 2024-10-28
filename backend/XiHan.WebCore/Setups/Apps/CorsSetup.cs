﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CorsSetup
// Guid:8015c072-e6b5-4a52-aec5-6bd747cfe0a2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-29 上午 01:25:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using XiHan.Infrastructures.Apps.Configs;

namespace XiHan.WebCore.Setups.Apps;

/// <summary>
/// CorsSetup
/// </summary>
public static class CorsSetup
{
    /// <summary>
    /// 跨源资源共享应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseCorsSetup(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var isEnabledCors = AppSettings.Cors.IsEnabled.GetValue();
        if (!isEnabledCors) return app;
        // 策略名称
        var policyName = AppSettings.Cors.PolicyName.GetValue();
        // 对 UseCors 的调用必须放在 UseRouting 之后，但在 UseAuthorization 之前
        _ = app.UseCors(policyName);
        return app;
    }
}