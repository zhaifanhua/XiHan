﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserService
// Guid:2f0d94cc-ae27-4504-94bf-cc835ad307f9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 02:04:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Consts;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Roles;
using XiHan.Services.Syses.Users.Dtos;
using XiHan.Utils.Encryptions;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Users.Logic;

/// <summary>
/// 系统用户服务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
[AppService(ServiceType = typeof(ISysUserService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysUserService(ISysUserRoleService sysUserRoleService, ISysRoleService sysRoleService) : BaseService<SysUser>, ISysUserService
{
    private readonly ISysUserRoleService _sysUserRoleService = sysUserRoleService;
    private readonly ISysRoleService _sysRoleService = sysRoleService;

    /// <summary>
    /// 校验账户是否唯一
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    private async Task<bool> GetAccountUnique(SysUser sysUser)
    {
        var isUnique = await IsAnyAsync(u => u.Account == sysUser.Account);
        return isUnique ? throw new CustomException($"账户【{sysUser.Account}】已存在！") : isUnique;
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateUser(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();

        _ = await GetAccountUnique(sysUser);

        var encryptPasswod = Md5HashEncryptionHelper.Encrypt(DesEncryptionHelper.Encrypt(GlobalConst.DefaultPassword));
        sysUser.Password = encryptPasswod;
        var userId = await AddReturnIdAsync(sysUser);

        // 新增用户角色信息
        sysUser.BaseId = userId;
        return userId;
    }

    /// <summary>
    /// 删除用户(假删除)
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUser(SysUser sysUser)
    {
        var user = await FindAsync(u => u.BaseId == sysUser.BaseId);
        // 删除用户角色
        _ = await _sysUserRoleService.DeleteUserRoleByUserId(sysUser.BaseId);
        return await RemoveAsync(user);
    }

    /// <summary>
    /// 修改用户账号信息
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserAccountInfo(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();
        return await UpdateAsync(s => new SysUser()
        {
            Account = sysUser.Account,
            NickName = sysUser.NickName,
            AvatarPath = sysUser.AvatarPath,
            Signature = sysUser.Signature
        }, f => f.BaseId == sysUser.BaseId);
    }

    /// <summary>
    /// 修改用户基本信息
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserBaseInfo(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();
        return await UpdateAsync(s => new SysUser()
        {
            RealName = sysUser.RealName,
            Gender = sysUser.Gender,
            Email = sysUser.Email,
            Phone = sysUser.Phone,
            Birthday = sysUser.Birthday,
            Address = sysUser.Address
        }, f => f.BaseId == sysUser.BaseId);
    }

    ///// <summary>
    ///// 修改用户角色
    ///// </summary>
    ///// <param name="userCDto"></param>
    ///// <returns></returns>
    //public async Task<bool> ModifyUserRole(SysUserCDto userCDto)
    //{
    //    var sysUser = userCDto.Adapt<SysUser>();
    //    var roleIds = await _sysRoleService.GetUserRoleIdsByUserId(sysUser.BaseId);
    //    var diffArr1 = roleIds.Where(id => !sysUser.SysRoleIds.Contains(id)).ToList();
    //    var diffArr2 = sysUser.SysRoleIds.Where(id => !roleIds.Contains(id)).ToList();

    //    //if (!diffArr1.Any() && !diffArr2.Any()) return true;
    //    // 删除用户与角色关联并重新关联用户与角色
    //    return await _sysUserRoleService.DeleteUserRoleByUserId(sysUser.BaseId) && await _sysUserRoleService.CreateUserRole(sysUser);
    //}

    /// <summary>
    /// 更改用户状态
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserStatus(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();
        return await UpdateAsync(s => new SysUser()
        {
        }, f => f.BaseId == sysUser.BaseId);
    }

    /// <summary>
    /// 更改用户密码
    /// </summary>
    /// <param name="userPwdMDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserPassword(SysUserPwdMDto userPwdMDto)
    {
        return await IsAnyAsync(u => !u.IsDeleted && u.BaseId == userPwdMDto.BaseId && u.Password ==
            Md5HashEncryptionHelper.Encrypt(DesEncryptionHelper.Encrypt(userPwdMDto.OldPassword)))
            ? await UpdateAsync(s => new SysUser
            {
                Password = Md5HashEncryptionHelper.Encrypt(DesEncryptionHelper.Encrypt(userPwdMDto.NewPassword))
            }, f => f.BaseId == userPwdMDto.BaseId)
            : throw new CustomException("重置密码出错，旧密码有误！");
    }

    /// <summary>
    /// 查询用户信息(根据用户主键)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserById(long userId)
    {
        var sysUser = await FindAsync(u => u.BaseId == userId);
        //sysUser.SysRoles = await _sysRoleService.GetUserRolesByUserId(userId);
        //sysUser.SysRoleIds = sysUser.SysRoles.Select(x => x.BaseId).ToList();

        return sysUser;
    }

    /// <summary>
    /// 查询用户信息(登录获取Token使用)
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByAccount(string account)
    {
        return await FindAsync(u => u.Account == account);
    }

    /// <summary>
    /// 查询用户信息(登录获取Token使用)
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByEmail(string email)
    {
        return await FindAsync(u => u.Email == email);
    }

    /// <summary>
    /// 查询用户列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysUser>> GetUserList(PageWhereDto<SysUserWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        Expressionable<SysUser> whereExpression = Expressionable.Create<SysUser>();
        _ = whereExpression.AndIF(whereDto.Account.IsNotEmptyOrNull(), u => u.Account.Contains(whereDto.Account!));
        _ = whereExpression.AndIF(whereDto.NickName.IsNotEmptyOrNull(), u => u.NickName.Contains(whereDto.NickName!));
        _ = whereExpression.AndIF(whereDto.RealName.IsNotEmptyOrNull(), u => u.RealName.Contains(whereDto.RealName!));
        _ = whereExpression.AndIF(whereDto.Gender != null, u => u.Gender == whereDto.Gender);
        _ = whereExpression.AndIF(whereDto.Email.IsNotEmptyOrNull(), u => u.Email.Contains(whereDto.Email!));
        _ = whereExpression.AndIF(whereDto.Phone.IsNotEmptyOrNull(), u => u.Phone.Contains(whereDto.Phone!));

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}