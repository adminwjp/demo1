#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Linq;
using TakeOutFoot.Activties;
using TakeOutFoot.Gifts;
using TakeOutFoot.Infrastructure;

namespace TakeOutFoot.Prizes
{
    /// <summary>
    /// 中奖 服务
    /// </summary>
    public class PrizeAppService
    {
        TakeOutFootDbContext dbContext;
        ActivtySettingAppService activtySettingAppService;
        ActivtyAppService activtyAppService;
        GiftAppService giftAppService;
        //IRepository<Prize> Repository;

        public PrizeAppService(TakeOutFootDbContext dbContext, ActivtySettingAppService activtySettingAppService, ActivtyAppService activtyAppService, 
            GiftAppService giftAppService)
        {
            this.dbContext = dbContext;
            this.activtySettingAppService = activtySettingAppService;
            this.activtyAppService = activtyAppService;
            this.giftAppService = giftAppService;
            //Repository = repository;
        }

        public Tuple<long?, long?, int>[] Cache { get; protected set; }
        /// <summary>
        /// 未实现 aop 实现
        /// </summary>
        /// <param name="tuples"></param>
        /// <returns></returns>
        public virtual bool CheckStock(Tuple<long?, long?, int>[] tuples)
        {
            return false;
        }
        /// <summary>
        /// 系统随机分配 给  用户 奖品
        /// 比如 优惠券
        /// </summary>
        /// <param name="userId">用户 id</param>
        /// <param name="activtyId">活动 id </param>
        /// <returns></returns>
        public virtual PrizeOutput GetPrize(string userId, long? activtyId)
        {
            Cache = null;
            //假设这里必中 物品
            //系统随机分配 给  用户 奖品
            Tuple<long?, long?, int>[] giftIds = null;// TestTakeOutFoot.GetRandom(dbContext,userId);//未实现 先写死  规则 自己去定义
            if (!CheckStock(giftIds))
            {
                
                return null;//优惠券 发送完了
            }
            //默认该 活动 有 15 种商品
            //库存 - 1
            // 3 5 7

            //库存 逻辑 处理 这里未实现 缓存实现 


            //库存 验证 使用 aop 缓存
            //更新 优惠券 库存
            giftAppService.UpdateStock(giftIds.Select(it=>new Tuple<long?,int>(it.Item1,-it.Item3)).ToArray());

            //更新 活动 设置 库存
            activtySettingAppService.UpdateStock(giftIds.Select(it => new Tuple<long?, int>(it.Item2, -it.Item3)).ToArray());

            //更新 活动 库存
            int total = 0;
            foreach (var item in giftIds)
            {
                total += item.Item3;
            }
            activtyAppService.UpdateStock(activtyId, -total);
            Cache = giftIds;
            //更新用户 优惠券 信息
            //默认 1-n 张
            List<Prize> prizes = new List<Prize>();
            foreach (var item in giftIds)
            {
                var prize = new Prize()
                {
                    CreationTime = DateTime.Now,
                    Account = userId,
                    UserId = "1",//xx //userId.Equals(TestTakeOutFoot.PlayerAccount1)
                    //? TestTakeOutFoot.PlayerId1 : TestTakeOutFoot.PlayerId2,
                    GiftId = item.Item1
                };
                //dotnet 执行
                prizes.Add(prize);//垃圾编译器 应输入; 报错不准确 重启vs 就好了
            }

            dbContext.Prizes.AddRange(prizes);
            dbContext.SaveChanges();
            //Repository.BatchInsert(prizes.ToArray());
            return null;
        }
    }
}
#endif