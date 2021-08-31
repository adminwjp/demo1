#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections.Generic;
using System.Data;
#if !(NET20 || NET30)
using System.Linq;
#endif
using System.Text;
using Utility.Helpers;
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
#endif

namespace Utility.Database
{

    public class DataModify
    {
        protected readonly List<IObservable<object>> Subscribes = new List<IObservable<object>>();
        protected readonly List<IObservable<object>> NoSubscribes = new List<IObservable<object>>();
        public void Subscribe(IObservable<object> observer)
        {
            if (NoSubscribes.Contains(observer))
            {
                NoSubscribes.Remove(observer);
            }
            if (!Subscribes.Contains(observer))
            {
                Subscribes.Add(observer);
            }
        }

        public void NoSubscribe(IObservable<object> observer)
        {
            if (Subscribes.Contains(observer))
            {
                Subscribes.Remove(observer);
                NoSubscribes.Add(observer);
            }
        }


    }

    public class DataModifyObservable : IObservable<object>
    {
        public IDisposable Subscribe(IObserver<object> observer)
        {
            throw new NotImplementedException();
        }
    }

    public class DataModifyObserver : IObserver<object>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(object value)
        {
            throw new NotImplementedException();
        }
    }

    public enum SqliteFKFlag
    {
        None=0x0,
        Cascade=0x1,
        SetNull=0x2,
        NoAction=0x3,
        Restrict=0x4,
        SetDefault=0x5
    }
    public struct SqliteDateFormatConstant
    {
        public const string YYYY_MM_DD="YYYY-MM-DD";//2010-12-30
        public const string YYYY_MM_DD_HH_MM = "YYYY-MM-DD HH:MM";//2010-12-30 12:10
        public const string YYYY_MM_DD_HH_MM_SS_SSS = "YYYY-MM-DD HH:MM:SS.SSS";//2010-12-30 12:10:04.100
        public const string MM_DD_YYYY_HH_MM = "MM-DD-YYYY HH:MM";//30-12-2010 12:10
        public const string HH_MM = "HH:MM";//12:10
        public const string YYYY_MM_DDTHH_MM = "YYYY-MM-DDTHH:MM";//2010-12-30 12:10
        public const string HH_MM_SS = "HH:MM:SS";//12:10:04
        public const string Now = "now";//2010-12-30
        public const string YYYYMMDDHHMMSS = "YYYYMMDD HHMMSS";//20101230 121004
    }
    public struct SqliteModifierFormatConstant
    {

    }





  

  



}
#endif