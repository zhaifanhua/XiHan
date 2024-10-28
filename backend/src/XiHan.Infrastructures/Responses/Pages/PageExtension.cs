﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageExtension
// Guid:a345ade2-5c23-474d-b6b5-ea29490d57b0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-02 上午 01:03:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Pages;

/// <summary>
/// 分页拓展
/// </summary>
public static class PageExtension
{
    /// <summary>
    /// 获取List的分页后的数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static List<TEntity> ToPageList<TEntity>(this IEnumerable<TEntity> entities, int currentIndex, int pageSize,
        int defaultFirstIndex = 1) where TEntity : class, new()
    {
        return entities.Skip((currentIndex - defaultFirstIndex) * pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// 获取List分页后的数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static List<TEntity> ToPageList<TEntity>(this IEnumerable<TEntity> entities, PageDto pageDto,
        int defaultFirstIndex = 1) where TEntity : class, new()
    {
        return entities.Skip((pageDto.CurrentIndex - defaultFirstIndex) * pageDto.PageSize).Take(pageDto.PageSize)
            .ToList();
    }

    /// <summary>
    /// IQueryable数据进行分页(还未在数据库中查询)
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static List<TEntity> ToPageList<TEntity>(this IQueryable<TEntity> entities, int currentIndex, int pageSize,
        int defaultFirstIndex = 1) where TEntity : class, new()
    {
        return [.. entities.Skip((currentIndex - defaultFirstIndex) * pageSize).Take(pageSize)];
    }

    /// <summary>
    /// IQueryable数据进行分页(还未在数据库中查询)
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    public static List<TEntity> ToPageList<TEntity>(this IQueryable<TEntity> entities, PageDto pageDto,
        int defaultFirstIndex = 1) where TEntity : class, new()
    {
        return [.. entities.Skip((pageDto.CurrentIndex - defaultFirstIndex) * pageDto.PageSize).Take(pageDto.PageSize)
];
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据，无Data数据(还未在数据库中查询)
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDto<TEntity>(this IQueryable<TEntity> entities, int currentIndex,
        int pageSize) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(currentIndex, pageSize, entities.Count())
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据，无Data数据(还未在数据库中查询)
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDto<TEntity>(this IQueryable<TEntity> entities, PageDto pageDto)
        where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(pageDto.CurrentIndex, pageDto.PageSize, entities.Count())
        };
        return pageDataDto;
    }

    /// <summary>
    /// 获取Dto数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    /// <returns>分页后的Dto结果</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IList<TEntity> entities, int currentIndex,
        int pageSize, int defaultFirstIndex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(currentIndex, pageSize, entities.Count),
            Data = entities.ToPageList(currentIndex, pageSize, defaultFirstIndex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 获取Dto数据
    /// </summary>
    /// <typeparam name="TEntity">数据源类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    /// <returns>分页后的Dto结果</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IList<TEntity> entities, PageDto pageDto,
        int defaultFirstIndex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(pageDto.CurrentIndex, pageDto.PageSize, entities.Count),
            Data = entities.ToPageList(pageDto, defaultFirstIndex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据(还未在数据库中查询)
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="currentIndex">当前页标</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IQueryable<TEntity> entities, int currentIndex,
        int pageSize, int defaultFirstIndex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(currentIndex, pageSize, entities.Count()),
            Data = entities.ToPageList(currentIndex, pageSize, defaultFirstIndex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 处理IQueryable数据后分页数据(还未在数据库中查询)
    /// 推荐针对部分列的增改
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <param name="pageDto">分页传入实体</param>
    /// <param name="defaultFirstIndex">默认起始下标</param>
    /// <returns>分页后的List数据</returns>
    public static PageDataDto<TEntity> ToPageDataDto<TEntity>(this IQueryable<TEntity> entities, PageDto pageDto,
        int defaultFirstIndex = 1) where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(pageDto.CurrentIndex, pageDto.PageSize, entities.Count()),
            Data = entities.ToPageList(pageDto, defaultFirstIndex)
        };
        return pageDataDto;
    }

    /// <summary>
    /// 获取全部信息，该信息被分页器包裹
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <returns>分页后的所有数据</returns>
    public static PageDataDto<TEntity> ToAllPageDataDto<TEntity>(this IEnumerable<TEntity> entities)
        where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(),
            Data = entities.ToList()
        };
        pageDataDto.PageInfo.CurrentIndex = 1;
        pageDataDto.PageInfo.TotalCount = pageDataDto.Data.Count;
        pageDataDto.PageInfo.PageSize = pageDataDto.PageInfo.TotalCount;
        pageDataDto.PageInfo.PageCount = 1;
        return pageDataDto;
    }

    /// <summary>
    /// 获取全部信息，该信息被分页器包裹 [IQueryable]
    /// </summary>
    /// <typeparam name="TEntity">数据类型</typeparam>
    /// <param name="entities">数据源</param>
    /// <returns>分页后的所有数据</returns>
    public static PageDataDto<TEntity> ToAllPageDataDto<TEntity>(this IQueryable<TEntity> entities)
        where TEntity : class, new()
    {
        PageDataDto<TEntity> pageDataDto = new()
        {
            PageInfo = new PageInfoDto(),
            Data = [.. entities]
        };
        pageDataDto.PageInfo.CurrentIndex = 1;
        pageDataDto.PageInfo.TotalCount = pageDataDto.Data.Count;
        pageDataDto.PageInfo.PageSize = pageDataDto.PageInfo.TotalCount;
        pageDataDto.PageInfo.PageCount = 1;
        return pageDataDto;
    }
}