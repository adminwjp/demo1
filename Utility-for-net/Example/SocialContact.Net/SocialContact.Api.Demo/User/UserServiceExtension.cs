//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using Tunynet.Caching;
using Tunynet.CMS;
using Tunynet.Common.Configuration;
using Tunynet.Events;
using Tunynet.FileStore;
using Tunynet.Imaging;
using Tunynet.Settings;
using Tunynet.Utilities;

namespace Tunynet.Common
{
    /// <summary>
    /// 扩展用户业务逻辑
    /// </summary>
    public static class UserServiceExtension
    {
        #region 头像处理

        //Avatar文件扩展名
        private static readonly string AvatarFileExtension = "jpg";

        /// <summary>
        ///  Avatar存储目录名
        /// </summary>
        public static readonly string AvatarDirectory = "Avatars";

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户ID</param>
        /// <param name="postedFile">上传的二进制头像文件</param>
        public static void UploadOriginalAvatar(long userId, Stream postedFile)
        {
            if (postedFile == null)
                return;
            Image image = Image.FromStream(postedFile);

            UserProfileSettings userProfileSettings = SettingManager<UserProfileSettings>.Get();

            postedFile = ImageProcessor.Resize(postedFile, userProfileSettings.OriginalAvatarWidth, userProfileSettings.OriginalAvatarHeight, ResizeMethod.Crop);

            string relativePath = GetAvatarRelativePath(userId);

            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            var user = UserService.GetUser(userId);
            storeProvider.AddOrUpdateFile(relativePath, GetAvatarFileName(userId, AvatarSizeType.Original), postedFile);
            postedFile.Dispose();
            //1、如果原图超过一定尺寸（可以配置宽高像素值）则原图保留前先缩小（原图如果太大，裁剪时不方便操作）再保存
        }

        /// <summary>
        /// 上传封面图
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户ID</param>
        /// <param name="postedFile">上传的二进制头像文件</param>
        public static void UploadOriginalCover(long userId, Stream postedFile)
        {
            var postedFiles = postedFile;
            if (postedFile == null)
                return;
            Image image = Image.FromStream(postedFile);

            UserProfileSettings userProfileSettings = SettingManager<UserProfileSettings>.Get();
            string relativePath = GetAvatarRelativePath(userId);

            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            var user = GlobalHelper.MianUnitWork.FindSingle<User>(it=>it.UserId== userId);
            UserService.UpdateCover(user, 1);

            postedFile = ImageProcessor.Resize(postedFile, userProfileSettings.CoverWidth, userProfileSettings.CoverHeight, ResizeMethod.Crop);
            storeProvider.AddOrUpdateFile(relativePath, GetCoverFileName(userId, AvatarSizeType.Big), postedFile);
            postedFile.Dispose();

            //裁剪手机端图片
            postedFiles = ImageProcessor.Resize(postedFiles, userProfileSettings.BigCoverWidth, userProfileSettings.BigCoverHeight, ResizeMethod.Crop);
            storeProvider.AddOrUpdateFile(relativePath, GetCoverFileName(userId), postedFiles);
            postedFiles.Dispose();
            //1、如果原图超过一定尺寸（可以配置宽高像素值）则原图保留前先缩小（原图如果太大，裁剪时不方便操作）再保存
        }

        /// <summary>
        /// 根据用户自己选择的尺寸及位置进行头像裁剪
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户Id</param>
        /// <param name="srcWidth">需裁剪的宽度</param>
        /// <param name="srcHeight">需裁剪的高度</param>
        /// <param name="srcX">需裁剪的左上角点坐标</param>
        /// <param name="srcY">需裁剪的左上角点坐标</param>
        public static void CropAvatar(long userId, float srcWidth, float srcHeight, float srcX, float srcY)
        {
            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            IStoreFile iStoreFile = storeProvider.GetFile(GetAvatarRelativePath(userId), GetAvatarFileName(userId, AvatarSizeType.Original));
            if (iStoreFile == null)
                return;
            User user = GetFullUser(userId);
            if (user == null)
                return;
            bool isFirst = true;
            string avatarRelativePath = GetAvatarRelativePath(userId).Replace(Path.DirectorySeparatorChar, '/');
            avatarRelativePath = avatarRelativePath.Substring(AvatarDirectory.Length + 1);
            //user.Avatar = avatarRelativePath + "/avatar_" + userId;

            UserService.UpdateAvatar(user, user.HasAvatar);

            UserProfileSettings userProfileSettings = SettingManager<UserProfileSettings>.Get();

            using (Stream fileStream = iStoreFile.OpenReadStream())
            {
                Stream bigImage = ImageProcessor.Crop(fileStream, new Rectangle((int)srcX, (int)srcY, (int)srcWidth, (int)srcHeight), userProfileSettings.AvatarWidth, userProfileSettings.AvatarHeight);

                Stream smallImage = ImageProcessor.Resize(bigImage, userProfileSettings.SmallAvatarWidth, userProfileSettings.SmallAvatarHeight, ResizeMethod.KeepAspectRatio);

                storeProvider.AddOrUpdateFile(GetAvatarRelativePath(userId), GetAvatarFileNameForEdit(userId, AvatarSizeType.Big), bigImage);

                storeProvider.AddOrUpdateFile(GetAvatarRelativePath(userId), GetAvatarFileNameForEdit(userId, AvatarSizeType.Small), smallImage);

                bigImage.Dispose();
                smallImage.Dispose();
            }

            //触发用户更新头像事件
            EventBus<User, CropAvatarEventArgs>.Instance().OnAfter(user, new CropAvatarEventArgs(isFirst));
        }

        /// <summary>
        /// 等比例缩放头像(手机端使用)
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId"></param>
        /// <param name="postedFile"></param>
        public static void ResizeAvatar(long userId, Stream postedFile)
        {
            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            IStoreFile iStoreFile = storeProvider.GetFile(GetAvatarRelativePath(userId), GetAvatarFileName(userId, AvatarSizeType.Original));
            if (iStoreFile == null)
                return;
            User user = GetFullUser(userId);
            if (user == null)
                return;
            bool isFirst = true;
            string avatarRelativePath = GetAvatarRelativePath(userId).Replace(Path.DirectorySeparatorChar, '/');
            avatarRelativePath = avatarRelativePath.Substring(AvatarDirectory.Length + 1);
            //user.Avatar = avatarRelativePath + "/avatar_" + userId;

            UserService.UpdateAvatar(user, user.HasAvatar);

            UserProfileSettings userProfileSettings = SettingManager<UserProfileSettings>.Get();

            using (postedFile)
            {
                Stream smallImage = ImageProcessor.Resize(postedFile, userProfileSettings.SmallAvatarWidth, userProfileSettings.SmallAvatarHeight, ResizeMethod.KeepAspectRatio);
                Stream bigImage = ImageProcessor.Resize(postedFile, userProfileSettings.AvatarWidth, userProfileSettings.AvatarHeight, ResizeMethod.KeepAspectRatio);
                storeProvider.AddOrUpdateFile(GetAvatarRelativePath(userId), GetAvatarFileNameForEdit(userId, AvatarSizeType.Small), smallImage);
                storeProvider.AddOrUpdateFile(GetAvatarRelativePath(userId), GetAvatarFileNameForEdit(userId, AvatarSizeType.Big), bigImage);

                smallImage.Dispose();
            }

            //触发用户更新头像事件
            EventBus<User, CropAvatarEventArgs>.Instance().OnAfter(user, new CropAvatarEventArgs(isFirst));
        }

        /// <summary>
        /// 删除用户头像
        /// </summary>
        public static void DeleteAvatar(long userId)
        {
            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();

            //删除文件系统的头像使用以下代码
            storeProvider.DeleteFolder(GetAvatarRelativePath(userId));
            var user = GlobalHelper.MianUnitWork.FindSingle<User>(userId);
            user.HasAvatar = 0;
            UserService.UpdateAvatar(user, user.HasAvatar);
        }

        /// <summary>
        /// 获取直连URL
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="user"></param>
        /// <param name="enableClientCaching"></param>
        /// <param name="avatarSizeType"></param>
        /// <returns></returns>
        public static string GetCoverDirectlyUrl(IUser user, bool enableClientCaching = true, AvatarSizeType avatarSizeType = AvatarSizeType.Small)
        {
            string url = string.Empty;

            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            string directlyRootUrl = storeProvider.DirectlyRootUrl;
            if (!string.IsNullOrEmpty(storeProvider.DirectlyRootUrl) && user.HasCover == 1)
            {
                url += storeProvider.DirectlyRootUrl;
            }
            else
            {
                url += WebUtility.ResolveUrl("~/Uploads");  //本机存储时仅允许用~/Uploads/
            }

            if (user == null)
            {
                url += "/" + AvatarDirectory + "/usercover";
            }
            else
            {
                if (user.HasCover == 1)
                {
                    url += "/" + GetAvatarRelativePath(user.UserId).Replace(Path.DirectorySeparatorChar, '/') + "/cover_" + user.UserId;
                }
                else
                {
                    url += "/" + AvatarDirectory + "/usercover";
                }
            }
            switch (avatarSizeType)
            {
                case AvatarSizeType.Original:
                case AvatarSizeType.Big:
                case AvatarSizeType.Medium:
                    url += "_big." + AvatarFileExtension;
                    break;

                case AvatarSizeType.Small:
                case AvatarSizeType.Micro:
                    url += "." + AvatarFileExtension;
                    break;

                default:
                    url += "." + AvatarFileExtension;
                    break;
            }
            if (!enableClientCaching)
            {
                url += "?lq=" + DateTime.Now.Ticks;
            }
            return url;
        }

        /// <summary>
        /// 获取直连URL
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="user"></param>
        /// <param name="avatarSizeType"></param>
        /// <param name="enableClientCaching"></param>
        /// <returns></returns>
        public static string GetAvatarDirectlyUrl(IUser user, AvatarSizeType avatarSizeType, bool enableClientCaching = true)
        {
            string url = string.Empty;

            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            string directlyRootUrl = storeProvider.DirectlyRootUrl;
            if (!string.IsNullOrEmpty(storeProvider.DirectlyRootUrl))
            {
                url += storeProvider.DirectlyRootUrl;
            }
            else
            {
                url += WebUtility.ResolveUrl("~/Uploads");  //本机存储时仅允许用~/Uploads/
            }

            if (user == null)
            {
                url += "/" + AvatarDirectory + "/anonymous";
            }
            else
            {
                if (user.HasAvatar == 1)
                {
                    url += "/" + GetAvatarRelativePath(user.UserId).Replace(Path.DirectorySeparatorChar, '/') + "/avatar_" + user.UserId;
                }
                else
                {
                    url += "/" + AvatarDirectory + "/anonymous";
                }
            }

            switch (avatarSizeType)
            {
                case AvatarSizeType.Original:
                    url += "_original." + AvatarFileExtension;
                    break;

                case AvatarSizeType.Big:
                case AvatarSizeType.Medium:
                    url += "_big." + AvatarFileExtension;
                    break;

                case AvatarSizeType.Small:
                case AvatarSizeType.Micro:
                    url += "." + AvatarFileExtension;
                    break;

                default:
                    url = string.Empty;
                    break;
            }
            if (!enableClientCaching)
            {
                url += "?lq=" + DateTime.Now.Ticks;
            }
            return url;
        }

        /// <summary>
        /// 获取直连URL(手机端)
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId"></param>
        /// <param name="avatarSizeType"></param>
        /// <returns></returns>
        public static string GetAvatarDirectlyUrl(long userId, AvatarSizeType avatarSizeType = AvatarSizeType.Small)
        {
            string url = string.Empty;
            var user = GetFullUser(userId);

            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            string directlyRootUrl = storeProvider.DirectlyRootUrl;
            if (!string.IsNullOrEmpty(storeProvider.DirectlyRootUrl))
            {
                url += storeProvider.DirectlyRootUrl;
            }
            else
            {
                url += WebUtility.ResolveUrl("~/Uploads");  //本机存储时仅允许用~/Uploads/
            }

            if (user == null)
            {
                url = string.Empty;
            }
            else
            {
                if (user.HasAvatar == 1)
                {
                    url += "/" + GetAvatarRelativePath(user.UserId).Replace(Path.DirectorySeparatorChar, '/') + "/avatar_" + user.UserId;

                    switch (avatarSizeType)
                    {
                        case AvatarSizeType.Original:
                            url += "_original." + AvatarFileExtension;
                            break;

                        case AvatarSizeType.Big:
                        case AvatarSizeType.Medium:
                            url += "_big." + AvatarFileExtension;
                            break;

                        case AvatarSizeType.Small:
                        case AvatarSizeType.Micro:
                            url += "." + AvatarFileExtension;
                            break;

                        default:
                            url = string.Empty;
                            break;
                    }
                }
                else
                {
                    url = string.Empty;
                }
            }

            return url;
        }

        /// <summary>
        /// 获取用户封面图
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户Id</param>
        /// <param name="avatarSizeType">头像尺寸类型</param>
        /// <returns></returns>
        public static IStoreFile GetCover(this UserService userService, long userId, AvatarSizeType avatarSizeType = AvatarSizeType.Small)
        {
            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            return storeProvider.GetFile(GetAvatarRelativePath(userId), GetCoverFileName(userId, avatarSizeType));
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户Id</param>
        /// <param name="avatarSizeType">头像尺寸类型</param>
        /// <returns></returns>
        public static IStoreFile GetAvatar(this UserService userService, long userId, AvatarSizeType avatarSizeType)
        {
            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            return storeProvider.GetFile(GetAvatarRelativePath(userId), GetAvatarFileName(userId, avatarSizeType));
        }

        /// <summary>
        /// 获取UserId头像存储的相对路径
        /// </summary>
        public static string GetAvatarRelativePath(long userId)
        {
            IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
            string idString = userId.ToString().PadLeft(15, '0');
            return storeProvider.JoinDirectory(AvatarDirectory, idString.Substring(0, 5), idString.Substring(5, 5), idString.Substring(10, 5));
        }

        /// <summary>
        /// 获取头像文件名称
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="avatarSizeType">头像尺寸类别</param>
        private static string GetAvatarFileNameForEdit(long userId, AvatarSizeType avatarSizeType)
        {
            string filename;
            switch (avatarSizeType)
            {
                case AvatarSizeType.Original:
                    filename = string.Format("avatar_{0}_original.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Big:
                    filename = string.Format("avatar_{0}_big.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Medium:
                    filename = string.Format("avatar_{0}_big.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Small:
                    filename = string.Format("avatar_{0}.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Micro:
                    filename = string.Format("avatar_{0}.{1}", userId, AvatarFileExtension);
                    break;

                default:
                    filename = string.Empty;
                    break;
            }
            return filename;
        }

        /// <summary>
        /// 获取头像文件名称
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="avatarSizeType">头像尺寸类别</param>
        private static string GetAvatarFileName(long userId, AvatarSizeType avatarSizeType)
        {
            string filename;
            switch (avatarSizeType)
            {
                case AvatarSizeType.Original:
                    filename = string.Format("avatar_{0}_original.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Big:
                    filename = string.Format("avatar_{0}_big.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Medium:
                    filename = string.Format("avatar_{0}_big.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Small:
                    filename = string.Format("avatar_{0}.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Micro:
                    filename = string.Format("avatar_{0}.{1}", userId, AvatarFileExtension);
                    break;

                default:
                    filename = string.Empty;
                    break;
            }
            return filename;
        }

        /// <summary>
        /// 获取封面图文件名称
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="avatarSizeType">头像尺寸类别</param>
        private static string GetCoverFileName(long userId, AvatarSizeType avatarSizeType = AvatarSizeType.Small)
        {
            string filename;
            switch (avatarSizeType)
            {
                case AvatarSizeType.Original:
                    filename = string.Format("cover_{0}_original.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Big:
                    filename = string.Format("cover_{0}_big.{1}", userId, AvatarFileExtension);
                    filename = string.Format("cover_{0}_big.{1}", userId, AvatarFileExtension);
                    break;

                case AvatarSizeType.Small:
                case AvatarSizeType.Micro:
                    filename = string.Format("cover_{0}.{1}", userId, AvatarFileExtension);
                    break;

                default:
                    filename = string.Empty;
                    break;
            }
            return filename;
            //return string.Format("cover_{0}.{1}", userId, AvatarFileExtension);
        }

        #endregion 头像处理

        /// <summary>
        /// 获取完整的用户实体
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户ID</param>
        public static User GetFullUser( long userId)
        {
            return UserService.GetUser(userId);
        }

        /// <summary>
        /// 获取完整的用户实体
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userName">用户名</param>
        public static User GetFullUser( string userName)
        {
            return UserService.GetUserByUserName(userName);
        }

        /// <summary>
        /// 获取前N个用户
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="topNumber">获取用户数</param>
        /// <param name="sortBy">排序字段</param>
        /// <returns></returns>
        public static IEnumerable<IUser> GetTopUsers( int topNumber, SortBy_User sortBy)
        {
            return UserService.GetTopUsers(topNumber, sortBy);
        }

        /// <summary>
        /// 根据排序条件分页显示用户
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="sortBy">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录</param>
        /// <returns>根据排序条件倒排序分页显示用户</returns>
        public static PagingDataSet<User> GetPagingUsers(  SortBy_User? sortBy, int pageIndex, int pageSize)
        {
            return UserService.GetPagingUsers(sortBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userQuery">查询用户条件</param>
        /// <param name="pageSize">页面显示条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static PagingDataSet<User> GetUsers(  UserQuery userQuery, int pageSize, int pageIndex)
        {
            return UserService.GetUsers(userQuery, pageSize, pageIndex);
        }

        /// <summary>
        /// 根据用户Id集合组装用户集合
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetFullUsers(this IEnumerable<long> userIds)
        {
            if (userIds == null || !userIds.Any())
                return new List<User>();
            return null;
            //return GlobalHelper.MianUnitWork.Find<User>(userIds);
        }

        /// <summary>
        /// 帐号邮箱通过验证
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户Id</param>
        public static void UserEmailVerified( long userId)
        {
            User user = UserService.GetUser(userId);
            if (user == null)
                return;
            user.IsEmailVerified = true;
            GlobalHelper.MianUnitWork.Update(user);

            EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(EventOperationType.Instance().UserEmailVerified()));
        }

        /// <summary>
        /// 解除符合解除管制标准的用户（永久管制的用户不会自动解除管制）
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId"></param>
        public static void NoModeratedUser( long userId)
        {
            User user = UserService.GetUser(userId);
            if (user == null)
                return;
            user.IsModerated = false;
            GlobalHelper.MianUnitWork.Update(user);
            EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(EventOperationType.Instance().AutoNoModeratedUser()));
        }

        ///// <summary>
        ///// 更换皮肤
        ///// </summary>
        ///// <param name="userService"></param>
        ///// <param name="userId">用户Id</param>
        ///// <param name="isUseCustomStyle">是否使用自定义皮肤</param>
        ///// <param name="themeAppearance">themeKey与appearanceKey用逗号关联</param>
        //public static void ChangeThemeAppearance(this UserService userService, long userId, bool isUseCustomStyle, string themeAppearance)
        //{
        //    UserRepository userRepository = userService.GetUserRepository();
        //    userRepository.ChangeThemeAppearance(userId, isUseCustomStyle, themeAppearance);
        //}

    

        /// <summary>
        /// 根据用户状态获取用户数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="status">用户账号状态(-1=已删除,1=已激活,0=未激活)</param>
        /// <param name="isBanned">是否封禁</param>
        /// <param name="isModerated">是否管制</param>
        public static Dictionary<UserManageableCountType, int> GetManageableCounts(UserStatus status, bool isBanned, bool isModerated)
        {
            return GetManageableCounts(status, isBanned, isModerated);
        }

        /// <summary>
        /// 获取用户评论数计数And贴子回复数计数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户ID</param>
        /// <param name="tenantTypeId">租户</param>
        /// <param name="isIgnoreAuditStatus">是否忽略审核,用户个人空间计数</param>
        /// <returns></returns>
        public static int GetUserCommentCount(long userId, string tenantTypeId, bool isIgnoreAuditStatus = false)
        {
            IKvStore kvStore = DIContainer.Resolve<IKvStore>();
            //待审核,需在审核,通过个数
            int pendingCount, againCount, successCount, returnCount = 0;
            if (isIgnoreAuditStatus)
                kvStore.TryGet<int>(KvKeys.Instance().UserCommentCount(userId, tenantTypeId, null), out returnCount);
            else
            {
                kvStore.TryGet<int>(KvKeys.Instance().UserCommentCount(userId, tenantTypeId, AuditStatus.Pending), out pendingCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserCommentCount(userId, tenantTypeId, AuditStatus.Again), out againCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserCommentCount(userId, tenantTypeId, AuditStatus.Success), out successCount);
                CalculateCount(pendingCount, againCount, successCount, ref returnCount);
            }
            return returnCount;
        }

        /// <summary>
        /// 获取用户发布贴子数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户ID</param>
        /// <param name="tenantTypeId">租户</param>
        /// <param name="isIgnoreAuditStatus">是否忽略审核,用户个人空间计数</param>
        /// <returns></returns>
        public static int GetUserThreadCount(long userId, string tenantTypeId, bool isIgnoreAuditStatus = false)
        {
            IKvStore kvStore = DIContainer.Resolve<IKvStore>();
            //待审核,需在审核,通过个数
            int pendingCount, againCount, successCount, returnCount = 0;
            if (isIgnoreAuditStatus)
                kvStore.TryGet<int>(KvKeys.Instance().UserThreadCount(userId, tenantTypeId, null), out returnCount);
            else
            {
                kvStore.TryGet<int>(KvKeys.Instance().UserThreadCount(userId, tenantTypeId, AuditStatus.Pending), out pendingCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserThreadCount(userId, tenantTypeId, AuditStatus.Again), out againCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserThreadCount(userId, tenantTypeId, AuditStatus.Success), out successCount);
                CalculateCount(pendingCount, againCount, successCount, ref returnCount);
            }
            return returnCount;
        }

        /// <summary>
        /// 获取用户发布文章数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">用户ID</param>
        /// <param name="contentModelKey">模型key</param>
        /// <param name="isIgnoreAuditStatus">是否忽略审核,用户个人空间计数</param>
        /// <returns></returns>
        public static int GetUserContentItemCount(long userId, string contentModelKey="", bool isIgnoreAuditStatus = false)
        {
            IKvStore kvStore = DIContainer.Resolve<IKvStore>();
            //待审核,需在审核,通过个数
            int pendingCount, againCount, successCount, returnCount = 0;
            int tempCount = 0;
            Action calculateCount = () =>
            {
                if (isIgnoreAuditStatus)
                {
                    kvStore.TryGet<int>(KvKeys.Instance().UserContentItemCount(userId, contentModelKey, null), out tempCount);
                    returnCount += tempCount;
                }
                else
                {
                    kvStore.TryGet<int>(KvKeys.Instance().UserContentItemCount(userId, contentModelKey, AuditStatus.Pending), out pendingCount);
                    kvStore.TryGet<int>(KvKeys.Instance().UserContentItemCount(userId, contentModelKey, AuditStatus.Again), out againCount);
                    kvStore.TryGet<int>(KvKeys.Instance().UserContentItemCount(userId, contentModelKey, AuditStatus.Success), out successCount);
                    CalculateCount(pendingCount, againCount, successCount, ref returnCount);
                }
            };

            if (!string.IsNullOrEmpty(contentModelKey))
            {
                calculateCount();
            }
            else
            {
                contentModelKey = ContentModelKeys.Instance().Article();
                calculateCount();
                contentModelKey = ContentModelKeys.Instance().Image();
                calculateCount();
                contentModelKey = ContentModelKeys.Instance().Video();
                calculateCount();
                contentModelKey = ContentModelKeys.Instance().Contribution();
                calculateCount();
            }

            return returnCount;
        }

        ///// <summary>
        ///// 删除用户所有计数
        ///// </summary>
        ///// <param name="kvStore"></param>
        ///// <param name="userId">用户ID</param>
        ///// <param name="tenantTypeId">租户</param>
        ///// <param name="isIgnoreAuditStatus">是否忽略审核,用户个人空间计数</param>
        ///// <returns></returns>
        //public static void DeleteUserCount(this UserService userService, long userId)
        //{
        //    //删除用户所有相关的kvstore里面的数据 目前包含计数
        //    IKvStore kvStore = DIContainer.Resolve<IKvStore>();
        //    var kvValues = kvStore.GetList(userId.ToString(), true);
        //    foreach (var item in kvValues)
        //    {
        //        kvStore.DeleteById(kvValues.Select(n => n.Id).ToArray());
        //    }
        //}

        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="pendingCount">待审核数</param>
        /// <param name="againCount">需在审核</param>
        /// <param name="successCount">通过审核数</param>
        /// <param name="returnCount">返回数</param>
        /// <returns></returns>
        public static void CalculateCount(int pendingCount, int againCount, int successCount, ref int returnCount)
        {
            SiteSettings siteSetting = SettingManager<SiteSettings>.Get();
            if (siteSetting.AuditStatus == PubliclyAuditStatus.Success)
                returnCount += successCount;
            else if (siteSetting.AuditStatus == PubliclyAuditStatus.Again_GreaterThanOrEqual)
                returnCount += againCount + successCount;
            else if (siteSetting.AuditStatus == PubliclyAuditStatus.Pending_GreaterThanOrEqual)
                returnCount += pendingCount + againCount + successCount;
        }

        #region 防灌水限制相关

        /// <summary>
        /// 用户是否可以发布新内容
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId"></param>
        /// <param name="tenantTypeId"></param>
        /// <returns></returns>
        public static bool CanCreateNewContent(this UserService userService, long userId, string tenantTypeId)
        {
            //未配置或配置0则不限制
            int createNewContentCycle = Convert.ToInt32(ConfigurationManager.AppSettings["CreateNewContentCycle"]);
            if (createNewContentCycle > 0)
            {
                ICacheService cacheService = DIContainer.Resolve<ICacheService>();
                DateTime? lastCreated = null;
                string cacheKey = $"ContentLastCreated-userId:{userId}-tenantTypeId:{tenantTypeId}";
                if (cacheService.TryGetValue(cacheKey, out lastCreated))
                {
                    return DateTime.Now - lastCreated >= new TimeSpan(0, 0, createNewContentCycle);
                }
            }

            return true;
        }

        /// <summary>
        /// 设置用户上次发布某个新内容的时间
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId"></param>
        /// <param name="tenantTypeId"></param>
        /// <param name="dateCreated"></param>
        public static void SetNewContentLastCreated(this UserService userService, long userId, string tenantTypeId, DateTime dateCreated)
        {
            //未配置或配置0则不限制
            int createNewContentCycle = Convert.ToInt32(ConfigurationManager.AppSettings["CreateNewContentCycle"]);
            if (createNewContentCycle > 0)
            {
                ICacheService cacheService = DIContainer.Resolve<ICacheService>();
                string cacheKey = $"ContentLastCreated-userId:{userId}-tenantTypeId:{tenantTypeId}";
                cacheService.Set(cacheKey, dateCreated, CachingExpirationType.SingleObject);
            }
        }

        #endregion


        /// <summary>
        /// 获取用户的回答数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">userId</param>
        /// <param name="isIgnoreAuditStatus">是否忽略审核</param>
        /// <returns></returns>
        public static int GetUserAnswerCount(long userId, bool isIgnoreAuditStatus = false)
        {
            IKvStore kvStore = DIContainer.Resolve<IKvStore>();
            //待审核,需在审核,通过个数
            int pendingCount, againCount, successCount, returnCount = 0;
            if (isIgnoreAuditStatus)
                kvStore.TryGet<int>(KvKeys.Instance().UserAskAnswerCount(userId, null), out returnCount);
            else
            {
                kvStore.TryGet<int>(KvKeys.Instance().UserAskAnswerCount(userId, AuditStatus.Pending), out pendingCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserAskAnswerCount(userId, AuditStatus.Again), out againCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserAskAnswerCount(userId, AuditStatus.Success), out successCount);
                Tunynet.Common.UserServiceExtension.CalculateCount(pendingCount, againCount, successCount, ref returnCount);

            }
            return returnCount;
        }

        /// <summary>
        /// 获取用户的发布的问题数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId"></param>
        /// <param name="isIgnoreAuditStatus">是否忽略审核</param>
        /// <returns></returns>
        public static int GetUserQuestionCount(long userId, bool isIgnoreAuditStatus = false)
        {
            IKvStore kvStore = DIContainer.Resolve<IKvStore>();
            //待审核,需在审核,通过个数
            int pendingCount, againCount, successCount, returnCount = 0;
            if (isIgnoreAuditStatus)
                kvStore.TryGet<int>(KvKeys.Instance().UserAskQuestionCount(userId, null), out returnCount);
            else
            {
                kvStore.TryGet<int>(KvKeys.Instance().UserAskQuestionCount(userId, AuditStatus.Pending), out pendingCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserAskQuestionCount(userId, AuditStatus.Again), out againCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserAskQuestionCount(userId, AuditStatus.Success), out successCount);
                Tunynet.Common.UserServiceExtension.CalculateCount(pendingCount, againCount, successCount, ref returnCount);
            }
            return returnCount;
        }

        /// <summary>
        /// 获取用户的活动计数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userId">userId</param>
        /// <param name="isIgnoreAuditStatus">是否忽略审核</param>
        /// <returns></returns>
        public static int GetUserEventCount(long userId, bool isIgnoreAuditStatus = false)
        {
            IKvStore kvStore = DIContainer.Resolve<IKvStore>();
            //待审核,需在审核,通过个数
            int pendingCount, againCount, successCount, returnCount = 0;
            if (isIgnoreAuditStatus)
                kvStore.TryGet<int>(KvKeys.Instance().UserEventCount(userId, null), out returnCount);
            else
            {
                kvStore.TryGet<int>(KvKeys.Instance().UserEventCount(userId, AuditStatus.Pending), out pendingCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserEventCount(userId, AuditStatus.Again), out againCount);
                kvStore.TryGet<int>(KvKeys.Instance().UserEventCount(userId, AuditStatus.Success), out successCount);
                Tunynet.Common.UserServiceExtension.CalculateCount(pendingCount, againCount, successCount, ref returnCount);
            }
            return returnCount;
        }

        /// <summary>
        /// 是否可以修改对外显示名称
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="user"></param>
        /// <param name="historyData"></param>
        /// <returns></returns>
        public static bool CanEditDisplayName(User user, User historyData)
        {
            var setting = SettingManager<UserSettings>.Get();
            int editDisplayNameCycle = Convert.ToInt32(ConfigurationManager.AppSettings["EditDisplayNameCycle"]);
            IKvStore kvStore = DIContainer.Resolve<IKvStore>();
            if (setting != null)
            {
                DateTime? lastModified = null;
                kvStore.TryGet(KvKeys.Instance().UserDisplayNameLastModified(user.UserId), out lastModified);
                if (lastModified.HasValue && editDisplayNameCycle > 0)
                {
                    //如果修改了对外显示的名称 需要判断上次修改的时间
                    if ((setting.DisplayNameType == DisplayNameType.UserNameFirst && user.UserName != historyData.DisplayName) || (setting.DisplayNameType == DisplayNameType.TrueNameFirst && user.TrueName != historyData.TrueName))
                    {
                        var days = DateTime.Now.Date - lastModified.Value.Date;
                        return days >= new TimeSpan(editDisplayNameCycle, 0, 0, 0);
                    }
                }
            }

            return true;
        }

    }
}