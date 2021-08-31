using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Utility.Model
{
    public class ListModel
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public Type IdType { get; set; }
        /// <summary>dialog 宽 </summary>
        public int Width { get; set; }
        /// <summary>dialog 高 </summary>
        public int Height { get; set; }
        public List<ColumnModel> Columns { get; set; } = new List<ColumnModel>();
        public int Flag { get; set; }
        public class ColumnModel
        {
            public string Name { get; set; }
            public string Header { get; set; }
            public string DisplayMemberPath { get; set; }
            public string SelectedValuePath { get; set; }
            public bool Required { get; set; }
            public string StringFormat { get; set; }
            public ColumnEditFlag Flag { get; set; }
            public int MaxLength { get; set; }
            public ColumnType ColumnType { get; set; }
            public object Items { get; set; }
            public bool SingleItems { get; set; } //[1,2,3]
            public string Key { get; set; }//数据源 key
        }
        public enum ColumnType
        {
            None = 0,
            TextBox = 1,
            Combox = 2,
            TextBoxNumber = 3
        }
        public enum ColumnEditFlag
        {
            /// <summary>可以 编辑</summary>
            Edit,
            /// <summary>添加 时 可用,其他情况 隐藏 </summary>
            Hiddern,
            /// <summary> 除添加 时 禁用</summary>
            Disabled
        }
    }

    public interface IIsSelected
    {
        bool IsSelected { get; set; }
        object Id { get; set; }
    }
    public class DataListDataSource
    {
        public Action<bool> IsAllSelectedEvent { get; set; }
        public bool? IsAllSelected { get; set; } = false;
        public List<IIsSelected> DataList { get; set; }
        public IIsSelected GetSelect()
        {
            IIsSelected obj = null;//只能 选中 一行 进行 编辑
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.IsSelected)
                    {
                        if (obj != null)
                        {
                            return null;
                        }
                        obj = item;
                    }
                }
            }
            return obj;
        }
        public IIsSelected GetSelect(object id)
        {
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    object temp = item.Id;
                    if (Comparer.Default.Compare(temp, id) == 0)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public bool SetSelected(object id,bool selected)
        {
            if (DataList != null && DataList.Count > 0)
            {
                bool all = true;
                foreach (var item in DataList)
                {
                    object temp = item.Id;
                    if (Comparer.Default.Compare(temp, id) == 0)
                    {
                        item.IsSelected = selected;
                    }
                    if (selected&&!item.IsSelected)
                    {
                        all = false;//要么都选

                    }
                    else if (!selected && item.IsSelected)
                    {
                        all = false;//要么都没选
                    }
                }
                if (all)
                {
                    IsAllSelectedEvent?.Invoke(selected);
                }
                return all;
            }
            return false;
        }
     

        public IEnumerable<IIsSelected> GetMulSelect()
        {
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.IsSelected)
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                yield break;
            }
        }
        public IEnumerable<object> GetMulSelectId()
        {
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.IsSelected)
                    {
                        yield return item.Id;
                    }
                }
            }
            else
            {
                yield break;
            }
        }
    }
}
