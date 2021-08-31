using MediatR;
using NHibernate;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Caching;
using Utility.Cache;
using Utility.Domain.Uow;
using Utility.Pool;

namespace Core
{
    public class GlobalHelper
    {
        private static ConnectionPool readConnectionPool ;
        private static ConnectionPool writeConnectionPool;
        private static ObjectPool<IUnitWork> unitWorkPool;
        public static ICacheContent Cache { get; private set; }

        public static IMediator Mediator { get; private set; }
        public static IUnitWork UnitWork { get; set; }
        public static Microsoft.Extensions.Logging.ILoggerFactory LoggerFactory { get; private set; }
        public readonly static IDictionary<Type, RealTimeCacheHelper> RealTimeCaches = new ConcurrentDictionary<Type, RealTimeCacheHelper>();

        public static ISession GetSession()
        {
            return null;
        }
        public static Guid GetIdByUnitWork()
        {
            return Guid.NewGuid();
        }

        public static IUnitWork GetUnitWork()
        {
            return null;
        }
        public static DbConnection GetConnection(bool read=true)
        {
            return null;
        }

        public static void ReturnUnitWork(Guid id)
        {
        }
        public static void ReturnUnitWork(IUnitWork unitWork)
        {
        }
        
        public static void TestUnitWorkTemplate() {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {

            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }

        }
        public static void TestConnectionTemplate()
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {

            }
            finally
            {
                GlobalHelper.ReturnConnection(connection,false);
            }

        }
        public static void ReturnConnection(DbConnection connection,bool read=true)
        {

        }
        public static T Get<T>(object id) where T:class
        {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                return unitWork.FindSingle<T>(id);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }
    }
}
