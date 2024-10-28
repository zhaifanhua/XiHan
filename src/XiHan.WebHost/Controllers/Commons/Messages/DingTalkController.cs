﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DingTalkController
// Guid:fccfeb28-624c-41cb-9c5c-0b0652648a6b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-12 下午 07:50:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Responses;
using XiHan.Services.Commons.Messages.DingTalkPush;
using XiHan.Subscriptions.Bots.DingTalk;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Commons.Messages;

/// <summary>
/// 钉钉消息推送管理
/// <code>包含：文本/链接/文档/任务卡片/卡片菜单</code>
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="dingTalkPushService"></param>
[AllowAnonymous]
[ApiGroup(ApiGroupNameEnum.Common)]
public class DingTalkController(IDingTalkPushService dingTalkPushService) : BaseApiController
{
    private readonly IDingTalkPushService _dingTalkPushService = dingTalkPushService;

    /// <summary>
    /// 文本
    /// </summary>
    /// <returns></returns>
    [HttpPost("Text")]
    public async Task<ApiResult> DingTalkToText()
    {
        DingTalkText text = new()
        {
            Content = "看万山红遍，层林尽染；漫江碧透，百舸争流。"
        };
        DingTalkAt dingTalkAt = new()
        {
            AtMobiles = ["1302873****"],
            IsAtAll = false
        };
        return await _dingTalkPushService.DingTalkToText(text, dingTalkAt);
    }

    /// <summary>
    /// 链接
    /// </summary>
    /// <returns></returns>
    [HttpPost("Link")]
    public async Task<ApiResult> DingTalkToLink()
    {
        DingTalkLink link = new()
        {
            Title = "时代在召唤",
            Text = "这个即将发布的新版本，创始人陈航(花名“无招”)称它为“红树林”。而在此之前，每当面临重大升级，产品经理们都会取一个应景的代号，这一次，为什么是“红树林”？",
            PicUrl = "https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png",
            MessageUrl = "https://www.dingtalk.com/"
        };

        return await _dingTalkPushService.DingTalkToLink(link);
    }

    /// <summary>
    /// 文档
    /// </summary>
    /// <returns></returns>
    [HttpPost("Markdown")]
    public async Task<ApiResult> DingTalkToMarkdown()
    {
        DingTalkMarkdown markdown = new()
        {
            Title = "长沙天气",
            Text = "#### 长沙天气 \n" +
                   "> 8度，西北风3级，空气优16，相对湿度100%\n\n" +
                   "> ![screenshot](https://gw.alipayobjects.com/zos/skylark-tools/public/files/84111bbeba74743d2771ed4f062d1f25.png)\n" +
                   "> ###### 15点03分发布 [天气](https://www.seniverse.com/) \n"
        };
        DingTalkAt dingTalkAt = new()
        {
            AtMobiles = ["1302873****"],
            IsAtAll = false
        };
        return await _dingTalkPushService.DingTalkToMarkdown(markdown, dingTalkAt);
    }

    /// <summary>
    /// 任务卡片(整体跳转)
    /// </summary>
    /// <returns></returns>
    [HttpPost("WholeActionCard")]
    public async Task<ApiResult> DingTalkToWholeActionCard()
    {
        DingTalkActionCard actionCard = new()
        {
            Title = "乔布斯 20 年前想打造一间苹果咖啡厅，而它正是 Apple Store 的前身",
            Text =
                "![screenshot](https://gw.alipayobjects.com/zos/skylark-tools/public/files/84111bbeba74743d2771ed4f062d1f25.png) " +
                "### 乔布斯 20 年前想打造的苹果咖啡厅 " +
                "Apple Store 的设计正从原来满满的科技感走向生活化，而其生活化的走向其实可以追溯到 20 年前苹果一个建立咖啡馆的计划",
            SingleTitle = "阅读全文",
            SingleUrl = "https://www.dingtalk.com/"
        };
        return await _dingTalkPushService.DingTalkToActionCard(actionCard);
    }

    /// <summary>
    /// 任务卡片(独立跳转)
    /// </summary>
    /// <returns></returns>
    [HttpPost("PartActionCard")]
    public async Task<ApiResult> DingTalkToPartActionCard()
    {
        DingTalkActionCard actionCard = new()
        {
            Title = "乔布斯 20 年前想打造一间苹果咖啡厅，而它正是 Apple Store 的前身",
            Text =
                "![screenshot](https://gw.alipayobjects.com/zos/skylark-tools/public/files/84111bbeba74743d2771ed4f062d1f25.png) " +
                "### 乔布斯 20 年前想打造的苹果咖啡厅 " +
                "Apple Store 的设计正从原来满满的科技感走向生活化，而其生活化的走向其实可以追溯到 20 年前苹果一个建立咖啡馆的计划",
            BtnOrientation = "1",
            Btns =
            [
                new()
                {
                    Title = "不错",
                    ActionUrl = "https://www.dingtalk.com/"
                },
                new()
                {
                    Title = "不感兴趣",
                    ActionUrl = "https://www.dingtalk.com/"
                }
            ]
        };
        return await _dingTalkPushService.DingTalkToActionCard(actionCard);
    }

    /// <summary>
    /// 卡片菜单
    /// </summary>
    /// <returns></returns>
    [HttpPost("FeedCard")]
    public async Task<ApiResult> DingTalkToFeedCard()
    {
        DingTalkFeedCard feedCard = new()
        {
            Links =
            [
                new()
                {
                    Title = "时代的火车向前开",
                    MessageUrl = "https://www.dingtalk.com/",
                    PicUrl = "https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png"
                },
                new()
                {
                    Title = "时代在召唤",
                    MessageUrl = "https://www.dingtalk.com/",
                    PicUrl = "https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png"
                }
            ]
        };
        return await _dingTalkPushService.DingTalkToFeedCard(feedCard);
    }
}