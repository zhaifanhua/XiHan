﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictTypeService
// Guid:3f7ca12f-9a86-4e07-b304-1a0f951a133c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-12 下午 04:32:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Dicts.Dtos;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Dicts.Logic;

/// <summary>
/// 系统字典服务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysDictDataService"></param>
[AppService(ServiceType = typeof(ISysDictTypeService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysDictTypeService(ISysDictDataService sysDictDataService) : BaseService<SysDictType>, ISysDictTypeService
{
    private readonly ISysDictDataService _sysDictDataService = sysDictDataService;

    /// <summary>
    /// 校验字典是否唯一
    /// </summary>
    /// <param name="sysDictType"></param>
    /// <returns></returns>
    private async Task<bool> CheckDictTypeUnique(SysDictType sysDictType)
    {
        var isUnique = await IsAnyAsync(f => f.TypeCode == sysDictType.TypeCode && f.TypeName == sysDictType.TypeName);
        return isUnique ? throw new CustomException($"已存在字典【{sysDictType.TypeName}】!") : isUnique;
    }

    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="dictTypeCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateDictType(SysDictTypeCDto dictTypeCDto)
    {
        var sysDictType = dictTypeCDto.Adapt<SysDictType>();

        _ = await CheckDictTypeUnique(sysDictType);

        return await AddReturnIdAsync(sysDictType);
    }

    /// <summary>
    /// 批量删除字典
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteDictTypeByIds(long[] dictIds)
    {
        List<SysDictType> sysDictTypeList = await QueryAsync(s => dictIds.Contains(s.BaseId));

        // 系统内置字典
        var isOfficialCount = sysDictTypeList.Where(s => s.IsOfficial).ToList().Count;
        if (isOfficialCount > 0) throw new CustomException($"存在系统内置字典，不能删除！");

        // 已分配字典
        List<string> typeCodes = sysDictTypeList.Select(s => s.TypeCode).ToList();
        List<SysDictData> sysDictDataList = await _sysDictDataService.QueryAsync(f => typeCodes.Contains(f.TypeCode));
        if (sysDictDataList.Count == 0) return await RemoveAsync(s => dictIds.Contains(s.BaseId));

        foreach (var sysDictData in sysDictDataList)
        {
            var sysDictType = sysDictTypeList.First(s => s.TypeCode == sysDictData.TypeCode);
            throw new CustomException($"字典【{sysDictType.TypeName}】已分配字典项【{sysDictData.Label}】,不能删除！");
        }

        return await RemoveAsync(s => dictIds.Contains(s.BaseId));
    }

    /// <summary>
    /// 修改字典
    /// </summary>
    /// <param name="dictTypeMDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyDictType(SysDictTypeMDto dictTypeMDto)
    {
        var sysDictType = dictTypeMDto.Adapt<SysDictType>();

        var oldDictType = await FindAsync(x => x.BaseId == sysDictType.BaseId);
        if (sysDictType.TypeCode != oldDictType.TypeCode || sysDictType.TypeName != oldDictType.TypeName)
            _ = await CheckDictTypeUnique(sysDictType);

        return await UpdateAsync(sysDictType);
    }

    /// <summary>
    /// 查询字典
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    public async Task<SysDictType> GetDictTypeById(long dictId)
    {
        return await FindAsync(f => f.BaseId == dictId);
    }

    /// <summary>
    /// 查询字典列表(所有)
    /// </summary>
    /// <returns></returns>
    public async Task<List<SysDictType>> GetDictTypeList()
    {
        return await QueryAllAsync();
    }

    /// <summary>
    /// 查询字典列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysDictType>> GetDictTypeList(SysDictTypeWDto whereDto)
    {
        Expressionable<SysDictType> whereExpression = Expressionable.Create<SysDictType>();
        _ = whereExpression.AndIF(whereDto.TypeCode.IsNotEmptyOrNull(), u => u.TypeCode == whereDto.TypeCode);
        _ = whereExpression.AndIF(whereDto.TypeName.IsNotEmptyOrNull(), u => u.TypeName.Contains(whereDto.TypeName!));
        _ = whereExpression.AndIF(whereDto.IsEnable != null, u => u.IsEnable == whereDto.IsEnable);
        _ = whereExpression.AndIF(whereDto.IsOfficial != null, u => u.IsOfficial == whereDto.IsOfficial);

        return await QueryAsync(whereExpression.ToExpression(), o => o.TypeCode);
    }

    /// <summary>
    /// 查询字典列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysDictType>> GetDictTypePageList(PageWhereDto<SysDictTypeWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        Expressionable<SysDictType> whereExpression = Expressionable.Create<SysDictType>();
        _ = whereExpression.AndIF(whereDto.TypeCode.IsNotEmptyOrNull(), u => u.TypeCode == whereDto.TypeCode);
        _ = whereExpression.AndIF(whereDto.TypeName.IsNotEmptyOrNull(), u => u.TypeName.Contains(whereDto.TypeName!));
        _ = whereExpression.AndIF(whereDto.IsEnable != null, u => u.IsEnable == whereDto.IsEnable);
        _ = whereExpression.AndIF(whereDto.IsOfficial != null, u => u.IsOfficial == whereDto.IsOfficial);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.TypeCode, pageWhere.IsAsc);
    }
}