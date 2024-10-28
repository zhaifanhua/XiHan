﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SwaggerSetup
// Guid:40c713e1-7cdf-42da-9e08-84d1bff1489f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-29 上午 01:03:07
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebCore.Setups.Apps;

/// <summary>
/// SwaggerSetup
/// </summary>
public static class SwaggerSetup
{
    /// <summary>
    /// Swagger应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        try
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(options =>
            {
                // 路由前缀
                var routePrefix = AppSettings.Swagger.RoutePrefix.GetValue();
                // 性能分析开关
                var isEnabledMiniprofiler = AppSettings.Miniprofiler.IsEnabled.GetValue();
                // 需要暴露的分组
                string[] publishGroup = AppSettings.Swagger.PublishGroup.GetSection();

                // 根据分组遍历展示
                typeof(ApiGroupNameEnum).GetFields().Skip(1).ToList().ForEach(group =>
                {
                    // 获取枚举值上的特性
                    if (publishGroup.All(pGroup =>
                            !string.Equals(pGroup, group.Name, StringComparison.CurrentCultureIgnoreCase))) return;

                    var info = group.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>()
                        .FirstOrDefault();
                    // 切换分组操作,参数一是使用的哪个json文件,参数二是个名字
                    options.SwaggerEndpoint($"/swagger/{group.Name}/swagger.json", info?.Title);
                });

                // 性能分析
                if (isEnabledMiniprofiler)
                {
                    // 入口程序集
                    var entryAssembly = App.EntryAssembly;
                    // 将swagger首页，设置成自定义的页面，写法：{ 项目名.index.html}
                    options.IndexStream = () =>
                        entryAssembly.GetManifestResourceStream($"{entryAssembly.GetName().Name}.index.html");
                    options.HeadContent =
                        @"<style>.opblock-summary-description{font-weight: bold;text-align: right;}</style>";
                }

                // Api页面标题
                options.DocumentTitle = $"曦寒|接口文档";
                // Api文档仅展开标记
                // List：列表式(展开子类)，默认值;
                // Full：完全展开;
                // None：列表式(不展开子类)
                options.DocExpansion(DocExpansion.None);
                // 模型的默认扩展深度，设置为 -1 完全隐藏模型
                options.DefaultModelsExpandDepth(-1);
                // Api前缀设置
                options.RoutePrefix = routePrefix;
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
        }

        return app;
    }
}