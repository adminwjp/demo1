﻿using System;

namespace Utility.Application.Services.Dtos
{
    /// <summary>
    /// IEntityDto  interface:
    /// primary key base class information.
    /// </summary>
    /// <typeparam name="Key">primary key type</typeparam>
    public interface IEntityDto<Key>
    {

        /// <summary>
        /// primary key
        /// </summary>

        Key Id { get; set; }
    }
    /// <summary>
    /// EntityDto:IEntityDto interface implement.
    /// primary key base class information.
    /// remote not support generic type .
    /// </summary>
    /// <typeparam name="Key">primary key type </typeparam>
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]//remote webservice(interfacer not support) must need, wcf not need
#endif
    public class EntityDto<Key>:IEntityDto<Key>
    {
        private Key _id;//primary key

        /// <summary>
        /// no param entity dto  constractor 
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// have param entity dto  constractor 
        /// </summary>
        /// <param name="id">primary key</param>
        public EntityDto(Key id)
        {
            this._id = id;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Key Id { get { return this._id; } set { Set(ref _id, value, "Id"); } }

        /// <summary>
        /// 设置属性值 wpf 使用时直接继承 viewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="propertyName">属性名称 wpf 有效</param>

        protected virtual void Set<T>(ref T oldValue, T newValue, string propertyName)
        {
            oldValue = newValue;
        }
    }
}
