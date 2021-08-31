using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Utility.Wpf.ViewModels
{
    /// <summary>
    /// ��ѡ�� ѡ�� ��
    /// </summary>
    public interface IIsSelectedViewModel: INotifyPropertyChanged
    {

        /// <summary>
        /// ��ѡ�� ѡ�� ��
        /// </summary>
        bool IsSelected { get; set; }
        ///// <summary>
        ///// ��ֹ ˫���ʱ�Ķ���Ϊnull ���� ���ݲ�����
        ///// </summary>
        //void CreateByNullInstance();
        /// <summary>
        ///  ��ѡ�� ѡ�л� ȡ��  �� ���� ȫѡ ��ѡ�� �Ƿ� ѡ��
        /// </summary>
        event Action<IIsSelectedViewModel> AllSelectEvent;
    }
}
