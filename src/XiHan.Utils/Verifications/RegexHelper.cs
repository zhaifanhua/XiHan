﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RegexHelper
// Guid:351b39db-a1a2-4d26-94bb-96a924fba528
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-09 上午 01:16:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.RegularExpressions;

namespace XiHan.Utils.Verifications;

/// <summary>
/// 字符验证帮助类
/// </summary>
public static partial class RegexHelper
{
    #region 验证输入字符串是否与模式字符串匹配

    /// <summary>
    /// 验证输入字符串是否与模式字符串匹配，匹配返回 true
    /// </summary>
    /// <param name="input">输入的字符串</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="options">筛选条件</param>
    public static bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
    {
        return Regex.IsMatch(input, pattern, options);
    }

    #endregion

    #region 是否 Guid

    /// <summary>
    /// Guid 格式验证(a480500f-a181-4d3d-8ada-461f69eecfdd)
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsGuid(string checkValue)
    {
        return GuidRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否中国电话

    /// <summary>
    /// 电话号码(正确格式为：xxxxxxxxxx或xxxxxxxxxxxx)
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberTel(string checkValue)
    {
        return NumberTelRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否身份证

    /// <summary>
    /// 验证身份证是否有效
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberPeople(string checkValue)
    {
        switch (checkValue.Length)
        {
            case 18:
                {
                    var check = IsNumberPeople18(checkValue);
                    return check;
                }
            case 15:
                {
                    var check = IsNumberPeople15(checkValue);
                    return check;
                }
            default:
                return false;
        }
    }

    /// <summary>
    /// 身份证号(18位数字)
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberPeople18(string checkValue)
    {
        // 数字验证
        if (long.TryParse(checkValue.Remove(17), out var n) == false || n < Math.Pow(10, 16) ||
            long.TryParse(checkValue.Replace('x', '0').Replace('X', '0'), out _) == false)
            return false;
        // 省份验证
        const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (!address.Contains(checkValue.Remove(2), StringComparison.CurrentCulture)) return false;
        // 生日验证
        var birth = checkValue.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        if (!DateTime.TryParse(birth, out _)) return false;
        // 校验码验证
        var arrVerifyCode = "1,0,x,9,8,7,6,5,4,3,2".Split(',');
        var wi = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(',');
        var ai = checkValue.Remove(17).ToCharArray();
        var sum = 0;
        for (var i = 0; i < 17; i++) sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());

        _ = Math.DivRem(sum, 11, out var y);
        return arrVerifyCode[y].Equals(checkValue.Substring(17, 1), StringComparison.InvariantCultureIgnoreCase);
        // 符合GB11643-1999标准
    }

    /// <summary>
    /// 身份证号(15位数字)
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberPeople15(string checkValue)
    {
        // 数字验证
        if (long.TryParse(checkValue, out var n) == false || n < Math.Pow(10, 14)) return false;
        // 省份验证
        const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (!address.Contains(checkValue.Remove(2), StringComparison.CurrentCulture)) return false;
        // 生日验证
        var birth = checkValue.Substring(6, 6).Insert(4, "-").Insert(2, "-");
        return DateTime.TryParse(birth, out _);
    }

    #endregion

    #region 是否邮箱

    /// <summary>
    /// Email地址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsEmail(string checkValue)
    {
        return EmailRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否数字

    /// <summary>
    /// 数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber(string checkValue)
    {
        return NumberRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 是不是Int型
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsInt(string source)
    {
        return IntRegex().Match(source).Success && long.Parse(source) is <= 0x7fffffffL and >= -2147483648L;
    }

    /// <summary>
    /// 整数或者小数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberIntOrDouble(string checkValue)
    {
        return NumberIntOrDoubleRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// N位的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberSeveralN(string checkValue)
    {
        return NumberSeveralNRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 至少N位的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberSeveralAtLeastN(string checkValue)
    {
        return NumberSeveralAtLeastNRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// M至N位的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberSeveralMn(string checkValue)
    {
        return NumberSeveralMnRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 零和非零开头的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberBeginZeroOrNotZero(string checkValue)
    {
        return NumberBeginZeroOrNotZeroRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 2位小数的正实数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberPositiveRealTwoDouble(string checkValue)
    {
        return NumberPositiveRealTwoDoubleRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 有1-3位小数的正实数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberPositiveRealOneOrThreeDouble(string checkValue)
    {
        return NumberPositiveRealOneOrThreeDoubleRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 非零的正整数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberPositiveIntNotZero(string checkValue)
    {
        return NumberPositiveIntNotZeroRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 非零的负整数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberNegativeIntNotZero(string checkValue)
    {
        return NumberNegativeIntNotZeroRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否字母

    /// <summary>
    /// 字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsLetter(string checkValue)
    {
        return LetterRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 大写字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsLetterCapital(string checkValue)
    {
        return LetterCapitalRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 小写字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsLetterLower(string checkValue)
    {
        return LetterLowerRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否数字或英文字母

    /// <summary>
    /// 数字或英文字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberOrLetter(string checkValue)
    {
        return NumberOrLetterRegex().IsMatch(checkValue);
    }

    #endregion

    #region 字符串长度限定

    /// <summary>
    /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="begin">大于等于</param>
    /// <param name="end">小于等于</param>
    /// <returns></returns>
    public static bool IsLengthStr(string source, int begin, int end)
    {
        var length = LengthStrRegex().Replace(source, "OK").Length;
        return length > begin || length < end;
    }

    /// <summary>
    /// 长度为3的字符
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsCharThree(string checkValue)
    {
        return CharThreeRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否邮政编码

    /// <summary>
    /// 邮政编码 6个数字
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsPostCode(string source)
    {
        return PostCodeRegex().IsMatch(source);
    }

    #endregion

    #region 是否特殊字符

    /// <summary>
    /// 是否含有=，。：等特殊字符
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsCharSpecial(string checkValue)
    {
        return CharSpecialRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否汉字

    /// <summary>
    /// 包含汉字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsContainChinese(string checkValue)
    {
        return ContainChineseRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 全部汉字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsChinese(string checkValue)
    {
        return ChineseRegex().Matches(checkValue).Count == checkValue.Length;
    }

    #endregion

    #region 是否网址

    /// <summary>
    /// 是否网址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsUrl(string checkValue)
    {
        return UrlRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否日期

    /// <summary>
    /// 验证日期
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsDateTime(string checkValue)
    {
        try
        {
            return DateTime.TryParse(checkValue, out _);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 一年的12个月(正确格式为："01"～"09"和"1"～"12")
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsMonth(string checkValue)
    {
        return MonthRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 一月的31天(正确格式为："01"～"09"和"1"～"31")
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsDay(string checkValue)
    {
        return DayRegex().IsMatch(checkValue);
    }

    #endregion

    #region 是否 Ip 地址

    /// <summary>
    /// 是否 Ip 地址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsIpRegex(string checkValue)
    {
        return IpRegex().IsMatch(checkValue);
    }

    /// <summary>
    /// 是否 Ip 地址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsIp(string checkValue)
    {
        var result = false;
        try
        {
            var checkValueArg = checkValue.Split('.');
            if (string.Empty != checkValue && checkValue.Length < 16 && checkValueArg.Length == 4)
            {
                for (var i = 0; i < 4; i++)
                {
                    int intCheckValue = Convert.ToInt16(checkValueArg[i]);
                    if (intCheckValue <= 255) continue;

                    result = false;
                    return result;
                }

                result = true;
            }
        }
        catch
        {
            return result;
        }

        return result;
    }

    #endregion

    #region 是否 Cron 表达式

    /// <summary>
    /// 是否 Cron 表达式
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsCron(string checkValue)
    {
        return IsCron().IsMatch(checkValue);
    }

    #endregion

    [GeneratedRegex(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex GuidRegex();

    [GeneratedRegex(@"^(\d{3,4})\d{7,8}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberTelRegex();

    [GeneratedRegex(@"^[A-Za-z0-9](([\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^(-){0,1}\d+$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex IntRegex();

    [GeneratedRegex(@"^[0-9]*$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberRegex();

    [GeneratedRegex(@"^[0-9]+\.{0,1}[0-9]{0,2}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberIntOrDoubleRegex();

    [GeneratedRegex(@"^\d{n}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberSeveralNRegex();

    [GeneratedRegex(@"^\d{n,}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberSeveralAtLeastNRegex();

    [GeneratedRegex(@"^\d{m,n}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberSeveralMnRegex();

    [GeneratedRegex(@"^(0|[1-9] [0-9]*)$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberBeginZeroOrNotZeroRegex();

    [GeneratedRegex(@"^[0-9]+(.[0-9]{2})?$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberPositiveRealTwoDoubleRegex();

    [GeneratedRegex(@"^[0-9]+(.[0-9]{1,3})?$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberPositiveRealOneOrThreeDoubleRegex();

    [GeneratedRegex(@"^\+?[1-9][0-9]*$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberPositiveIntNotZeroRegex();

    [GeneratedRegex(@"^\-?[1-9][0-9]*$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberNegativeIntNotZeroRegex();

    [GeneratedRegex(@"^[A-Za-z]+$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex LetterRegex();

    [GeneratedRegex(@"^[A-Z]+$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex LetterCapitalRegex();

    [GeneratedRegex(@"^[a-z]+$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex LetterLowerRegex();

    [GeneratedRegex(@"^[A-Za-z0-9]+$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex NumberOrLetterRegex();

    [GeneratedRegex(@"[^\x00-\xff]", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex LengthStrRegex();

    [GeneratedRegex(@"^.{3}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex CharThreeRegex();

    [GeneratedRegex(@"^\d{6}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex PostCodeRegex();

    [GeneratedRegex(@"[^%&',;=?$\x22]+", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex CharSpecialRegex();

    [GeneratedRegex(@"^[\u4e00-\u9fa5]{0,}$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex ContainChineseRegex();

    [GeneratedRegex(@"[一-龥]", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex ChineseRegex();

    [GeneratedRegex(@"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\.-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%\./-~-]*)?$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex UrlRegex();

    [GeneratedRegex(@"^^(0?[1-9]|1[0-2])$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex MonthRegex();

    [GeneratedRegex(@"^((0?[1-9])|((1|2)[0-9])|30|31)$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex DayRegex();

    [GeneratedRegex(@"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex IpRegex();

    [GeneratedRegex(@"^\\s*($|#|\\w+\\s*=|(\\?|\\*|(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?(?:,(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?)*)\\s+(\\?|\\*|(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?(?:,(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?)*)\\s+(\\?|\\*|(?:[01]?\\d|2[0-3])(?:(?:-|\\/|\\,)(?:[01]?\\d|2[0-3]))?(?:,(?:[01]?\\d|2[0-3])(?:(?:-|\\/|\\,)(?:[01]?\\d|2[0-3]))?)*)\\s+(\\?|\\*|(?:0?[1-9]|[12]\\d|3[01])(?:(?:-|\\/|\\,)(?:0?[1-9]|[12]\\d|3[01]))?(?:,(?:0?[1-9]|[12]\\d|3[01])(?:(?:-|\\/|\\,)(?:0?[1-9]|[12]\\d|3[01]))?)*)\\s+(\\?|\\*|(?:[1-9]|1[012])(?:(?:-|\\/|\\,)(?:[1-9]|1[012]))?(?:L|W)?(?:,(?:[1-9]|1[012])(?:(?:-|\\/|\\,)(?:[1-9]|1[012]))?(?:L|W)?)*|\\?|\\*|(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?(?:,(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?)*)\\s+(\\?|\\*|(?:[0-6])(?:(?:-|\\/|\\,|#)(?:[0-6]))?(?:L)?(?:,(?:[0-6])(?:(?:-|\\/|\\,|#)(?:[0-6]))?(?:L)?)*|\\?|\\*|(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?(?:,(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?)*)(|\\s)+(\\?|\\*|(?:|\\d{4})(?:(?:-|\\/|\\,)(?:|\\d{4}))?(?:,(?:|\\d{4})(?:(?:-|\\/|\\,)(?:|\\d{4}))?)*))$", RegexOptions.IgnoreCase, "zh-CN")]
    private static partial Regex IsCron();
}