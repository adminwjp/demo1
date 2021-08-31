using System;

namespace Utility.Ioc
{
    public interface IScopeIocManager : IDisposable
    {
        /// <summary>
        /// get object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(string name = null);
    }
    /// <summary>
    /// ioc manager
    /// </summary>
    public interface IIocManager
    {
        /// <summary>
        /// add transitent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ImplT"></typeparam>
        void AddTransient<T, ImplT>(string name=null) where ImplT : class;

        /// <summary>
        /// add scope
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ImplT"></typeparam>
        void AddScoped<T, ImplT>(string name = null) where ImplT : class;


        IScopeIocManager CreateScope();

        /// <summary>
        /// add single instanc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ImplT"></typeparam>
        void SingleInstance<T, ImplT>(string name = null) where ImplT : class;

        /// <summary>
        /// get object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(string name=null);
    }
}
