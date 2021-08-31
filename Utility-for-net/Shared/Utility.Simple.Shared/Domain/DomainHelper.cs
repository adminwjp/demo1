using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utility.Attributes;
using Utility.Domain.Repositories;
using Utility.Domain.Uow;
using Utility.Helpers;

namespace Utility.Domain
{

    /// <summary>
    /// simple impl
    /// </summary>
    public class DomainHelper
    {
        public static void Execute(object obj,bool read=true,bool start=true)
        {
            var type = obj.GetType();
            if (AttributeHelper.Exists<TranstationAttribute>(type.GetCustomAttributes(false)))
            {
                object rep = null;
                IUnitWork unitwork = null;
                foreach (var item in type.GetProperties(BindingFlags.Public|
                    BindingFlags.NonPublic| BindingFlags.Instance))
                {
                    if (TypeHelper.IsICollection(item.PropertyType, typeof(IRepository<>)))
                    {
                        rep = item.GetValue(type,null);
                    }
                    if (TypeHelper.IsICollection(item.PropertyType, typeof(IUnitWork)))
                    {
                        unitwork = (IUnitWork)item.GetValue(type, null);
                    }
                    
                }
                if (rep != null)
                {
                    if (unitwork == null)
                    {
                        unitwork=(IUnitWork)rep.GetType().GetProperty("UnitWork").GetValue(rep, null);
                    }
                    TransactionManager tran =read?unitwork.ReadTransaction:unitwork.WriteTransaction;
                    if (start)
                    {
                        tran.Begin();
                    }
                    else
                    {
                        if (tran.HasTransaction())
                        {
                            if (read)
                            {
                                tran.Dispose();
                            }
                            else
                            {
                                tran.Commit();
                            }
                        }
                    }
                    
                }
            }
        }
      
    }
}
