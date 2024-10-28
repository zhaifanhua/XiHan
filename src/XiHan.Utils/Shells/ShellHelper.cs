﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ShellHelper
// Guid:11a08ee1-6099-4d00-9545-a177bf8a8393
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-09 上午 05:25:38
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Diagnostics;

namespace XiHan.Utils.Shells;

/// <summary>
/// ShellHelper
/// </summary>
public static class ShellHelper
{
    /// <summary>
    /// Unix 系统命令
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static string Bash(string command)
    {
        var output = string.Empty;
        var escapedArgs = command.Replace(@"""", @"\""");

        ProcessStartInfo info = new()
        {
            FileName = @"/bin/bash",
            // /bin/bash -c 后面接命令 ，而 /bin/bash 后面接执行的脚本
            Arguments = $"""
                         -c "{escapedArgs}"
                         """,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(info);
        if (process == null) return output;

        output = process.StandardOutput.ReadToEnd();
        return output;
    }

    /// <summary>
    /// Windows 系统命令
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Cmd(string fileName, string args)
    {
        var output = string.Empty;

        ProcessStartInfo info = new()
        {
            FileName = fileName,
            Arguments = args,
            RedirectStandardOutput = true,
            // 指定是否使用操作系统的外壳程序来启动进程。如果设置为 false，则使用 CreateProcess 函数直接启动进程
            UseShellExecute = false,
            // 指定是否在启动进程时创建一个新的窗口
            CreateNoWindow = true
        };

        using var process = Process.Start(info);
        if (process == null) return output;

        output = process.StandardOutput.ReadToEnd();
        return output;
    }
}