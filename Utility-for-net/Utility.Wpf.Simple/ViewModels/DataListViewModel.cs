using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Utility.Wpf.ViewModels
{
    /// <summary>
    /// 选中 事件
    /// </summary>
    public enum CheckFlag
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 全选
        /// </summary>
        AllCheck,
        /// <summary>
        /// 单选
        /// </summary>
        Check
    }
    /// <summary>
    /// 统一 数据源
    /// </summary>
    public class DataListViewModel: AbstractNotifyPropertyChanged
    {
        private bool? _isAllSelected=false;//全选 复选框
        private readonly object ObjLock = new object();
        /// <summary>
        /// id
        /// </summary>
        public virtual object Id { get; set; }//id
        /// <summary>
        /// 全选 复选框 触发 更新 datagrid 复选框
        /// </summary>
        public virtual bool? IsAllSelected { get { return _isAllSelected; } set {
                if (_isAllSelected != value)
                {
                    try
                    {
                       // Monitor.Enter(ObjLock);
                        if(Flag== CheckFlag.None)
                        {
                            Flag = CheckFlag.AllCheck;
                        }
                        if (Flag == CheckFlag.Check && value.HasValue)
                        {
                            if (DataList != null && DataList.Count > 0)
                            {
                                int c = 0;
                                if (DataList[0] is IIsSelectedViewModel)
                                {
                                    foreach (var item in DataList)
                                    {
                                        if (item is IIsSelectedViewModel isSelectedViewModel)
                                        {
                                            if (isSelectedViewModel.IsSelected)
                                            {
                                                c += 1;
                                            }
                                        }
                                    }
                                }
                                Set(ref _isAllSelected, c == DataList.Count, "IsAllSelected");
                            }
                        }
                        else
                        {
                            Set(ref _isAllSelected, value, "IsAllSelected");
                        }
                     

                        //全选 时 触发 单选(不触发全选)
                        if (Flag== CheckFlag.AllCheck && value.HasValue)
                        {
                            if (DataList != null && DataList.Count > 0)
                            {
                                if (DataList[0] is IIsSelectedViewModel)
                                {
                                    foreach (var item in DataList)
                                    {
                                        if (item is IIsSelectedViewModel isSelectedViewModel)
                                        {
                                            isSelectedViewModel.IsSelected = value.Value;
                                        }
                                    }
                                }
                            }
                            Flag = CheckFlag.None;
                        }
                       
                    }
                    finally
                    {
                      //  Monitor.Exit(ObjLock);
                    }
                }
                
            }
        }
        private ObservableCollection<object> _dataList;//datagrid 数据集
        private object query;
        private object form;
        /// <summary>
        /// 查询
        /// </summary>
        public virtual object Query { get => query; set => SetProperty(ref query, value, "Query"); }
        /// <summary>
        /// 表单
        /// </summary>
        public virtual object Form { get => form; set => SetProperty(ref form, value, "Form"); }
        /// <summary>
        /// 复选框 触发 时(不是全选复选框 触发)
        /// </summary>
        public virtual CheckFlag Flag { get; set; }
        /// <summary>
        /// datagrid 数据集
        /// </summary>
        public virtual ObservableCollection<object> DataList
        {
            get { return this._dataList; }
            set { Set(ref _dataList, value, "DataList"); }
        }
        /// <summary>
        /// datagrid 选中 数据项
        /// </summary>
        /// <returns></returns>
        public virtual IIsSelectedViewModel GetSelect()
        {
            IIsSelectedViewModel obj = null;//只能 选中 一行 进行 编辑
            if (DataList != null && DataList.Count > 0)
            {
                if (DataList[0] is IIsSelectedViewModel)
                {
                    foreach (var item in DataList)
                    {
                        if (item is IIsSelectedViewModel isSelectedViewModel&&isSelectedViewModel.IsSelected)
                        {
                            if (obj != null)
                            {
                                return null;
                            }
                            obj = isSelectedViewModel;
                        }
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// 根据 id datagrid 选中 数据项
        /// </summary>
        /// <param name="idName">id 属性 名称</param>
        /// <param name="id">id 属性 值</param>
        /// <returns></returns>
        public virtual object GetSelect(string idName,object id)
        {
            bool isValue = id.GetType().IsValueType;
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (isValue &&item.GetType().GetProperty(idName)?.GetValue(item).ToString() == id.ToString())
                    {
                        return item;
                    }
                    //1==1 false
                    else if (object.ReferenceEquals(item.GetType().GetProperty(idName)?.GetValue(item), id))
                    {
                        return item;
                    }
                }
            }
            return null;
        }
    }

    /// <summary>
    /// 统一 数据源
    /// </summary>
    public class DataListViewModel<T, Key> : DataListViewModel
       where T : class, IIsSelectedViewModel
    {
        private ObservableCollection<T> _dataList;//datagrid 数据集
        private T query;
        private T form;
        private Key id;

        /// <summary>
        /// 查询
        /// </summary>
        public new T Query { get => query; set { SetProperty(ref query, value, "Query"); base.Query = value; } }
        /// <summary>
        /// 表单
        /// </summary>
        public new T Form { get => form; set { SetProperty(ref query, value, "Form"); base.Form = value; } }

        /// <summary>
        /// 表格表单
        /// </summary>
        public virtual T TableForm { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public new Key Id { get => id; set { id = value; ; base.Id = value; } }//id
        /// <summary>
        /// datagrid 数据集
        /// </summary>
        public new ObservableCollection<T> DataList
        {
            get { return this._dataList; }
            set
            {
                Set(ref _dataList, value, "DataList"); if (_dataList != null)
                {
                    foreach (var item in _dataList)
                    {
                        base.DataList = base.DataList ?? new ObservableCollection<object>();
                        base.DataList?.Clear();
                        base.DataList.Add(item);
                    }
                } }
        }
 
        /// <summary>
        /// 根据 id datagrid 选中 数据项
        /// </summary>
        /// <param name="idName">id 属性 名称</param>
        /// <param name="id">id 属性 值</param>
        /// <returns></returns>
        public new T GetSelect(string idName, object id)
        {
            var obj = base.GetSelect(idName, id);
            return obj==null? default(T):(T)obj;
        }

        /// <summary>
        /// 根据  datagrid 选中 数据项
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetSelectItems()
        {
            if (DataList == null || DataList.Count == 0)
            {
                yield return null;

                foreach (var item in DataList)
                {
                    if (item.IsSelected)
                    {
                        yield return item;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 统一 数据源
    /// </summary>
    public class DataListViewModel<Create, Update, QueryInput, QueryOutput, Key> :
        DataListViewModel<QueryOutput, Key>
        where Create : class
        where Update : class
        where QueryInput : class
        where QueryOutput : class, IIsSelectedViewModel
    {

     
        private ObservableCollection<QueryOutput> _dataList;//datagrid 数据集
        private QueryInput query;
        private Create form;
        private Update updateForm;
        private QueryOutput previewForm;

        /// <summary>
        /// 查询
        /// </summary>
        public new  QueryInput Query { get => query; set => SetProperty(ref query, value, "Query"); }
        /// <summary>
        /// 表单
        /// </summary>
        public new  Create Form { get => form; set => SetProperty(ref form, value, "Form"); }
        /// <summary>
        ///编辑 表单
        /// </summary>
        public virtual Update UpdateForm { get => updateForm; set => SetProperty(ref updateForm, value, "UpdateForm"); }

        /// <summary>
        ///预览 或 删除 表单
        /// </summary>
        public virtual QueryOutput PreviewForm { get => previewForm; set => SetProperty(ref previewForm, value, "PreviewForm"); }

    }
}
