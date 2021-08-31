using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Utility.Wpf.Extensions;

namespace Tool.Ctrls
{

    /// <summary>
    /// DockerToolControl.xaml 的交互逻辑
    /// </summary>
    public partial class DockerToolControl : UserControl
    {
        
        public List<ContainerEntity> ContainerEntities { get; set; }
        public ObservableCollection<ImageEntity> ImageEntities { get; set; }
        public DockerToolControl()
        {
            InitializeComponent();
         
        }

        public void InitialData()
        {
            this.Dispatcher.BeginInvoke(new Action(() => {
                DockerHelper.StartDocker();
                var containerEntity = DockerCmdHelper.GetContainers<ContainerEntity>();
                var imageEntitiey = DockerCmdHelper.GetImages();
                //没用 不能 自动绑定
                this.ContainerEntities = containerEntity; //new ObservableCollection<DockerUtils.ContainerEntity>(containerEntity);
                this.ImageEntities = new ObservableCollection<ImageEntity>(imageEntitiey);//该方式 数据 后台 硬编码 绑定 没结果显示
                this.DataGridContainer.ItemsSource = containerEntity;
                this.DataGridImage.ItemsSource = imageEntitiey;
            }));
        }

        private void StartImage_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                DockerHelper.RunCmd(DockerFlag.Start, new List<string>() { id }, false);
            }
        }

        private void RemoveImage_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                DockerHelper.RunCmd(DockerFlag.Rmi, new List<string>() { id },false);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                DockerHelper.RunCmd(DockerFlag.Start, new List<string>() { id });
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
           if(sender is Button button && button.Tag is string id)
            {
                DockerHelper.RunCmd(DockerFlag.Rm,new List<string>() { id});
            }
        }
        private string[] GetContainerIds()
        {
            var objs = this.DataGridContainer.GetCheckIds();
            return GetIds(objs, true);
        }
        private string[] GetImageIds()
        {
            var objs = this.DataGridImage.GetCheckIds();
            return GetIds(objs,false);
        }
        private string[] GetIds(object[] objs,bool isContainer)
        {
            List<string> ids = new List<string>();
            foreach (var item in objs)
            {
                if (isContainer && item is ContainerEntity container)
                  {
                    ids.Add(container.ContainerId);
                }
                else if (item is ImageEntity image)
                {
                    ids.Add(image.ImageID);
                }
            }
            return ids.ToArray();
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.DataGridContainer.CheckBoxClick(sender);
        }

        private void CheckBoxImage_Click(object sender, RoutedEventArgs e)
        {
            this.DataGridImage.CheckBoxClick(sender);
        }

        private void StartDocker(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => { DockerHelper.StartDocker(); }));
        }

        private void StopDocker(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => { DockerHelper.StopDocker(); }));
        }

        private void RefreshDocker(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => {
                var containerEntity = DockerCmdHelper.GetContainers<ContainerEntity>();
                var imageEntitiey = DockerCmdHelper.GetImages();
                this.DataGridContainer.ItemsSource = containerEntity;
                this.DataGridImage.ItemsSource = imageEntitiey;
            }));
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            List<string> ids = new List<string>(GetContainerIds()) ;
            ids.AddRange(GetImageIds());
            if (ids.Count == 0)
            {
                MessageBox.Show("未选中一行无法启动", "提示");
            }
            else
            {
                DockerHelper.RunCmd(DockerFlag.Start,ids);
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            List<string> ids = new List<string>(GetContainerIds());
            ids.AddRange(GetImageIds());
            if (ids.Count == 0)
            {
                MessageBox.Show("未选中一行无法停止", "提示");
            }
            else
            {
                DockerHelper.RunCmd(DockerFlag.Stop, ids);
            }
        }

        private void RemoveContainer(object sender, RoutedEventArgs e)
        {
            List<string> ids = new List<string>(GetContainerIds());
            if (ids.Count == 0)
            {
                MessageBox.Show("未选中一行无法移除", "提示");
            }
            else
            {
                DockerHelper.RunCmd(DockerFlag.Rm, ids);
            }
        }

        private void RemoveImage(object sender, RoutedEventArgs e)
        {
            List<string> ids = new List<string>(GetImageIds());
            if (ids.Count == 0)
            {
                MessageBox.Show("未选中一行无法移除", "提示");
            }
            else
            {
                DockerHelper.RunCmd(DockerFlag.Rmi, ids);
            }
        }
    }
    public class ContainerModel :ContainerEntity, INotifyPropertyChanged
    {
        private string _containerId;//容器ID
        private string _image;//镜像
        private string _command;//命令
        private string _created;//创建时间
        private string _status;//状态
        private string _ports;//端口
        public new string ContainerId
        {
            get
            {
                return this._containerId;
            }
            set
            {
                if (this._containerId == value) return;
                this._containerId = value;
                OnPropertyChanged("ContainerId");
            }
        }
        public new string Image
        {
            get
            {
                return this._image;
            }
            set
            {
                if (this._image == value) return;
                this._image = value;
                OnPropertyChanged("Image");
            }
        }
        public new string Command
        {
            get
            {
                return this._command;
            }
            set
            {
                if (this._command == value) return;
                this._command = value;
                OnPropertyChanged("Command");
            }
        }
        public new string Created
        {
            get
            {
                return this._created;
            }
            set
            {
                if (this._created == value) return;
                this._created = value;
                OnPropertyChanged("Created");
            }
        }
        public new string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                this._status = value;
                OnPropertyChanged("Status");
            }
        }
        public new string Ports
        {
            get
            {
                return this._ports;
            }
            set
            {
                if (this._ports == value) return;
                this._ports = value;
                OnPropertyChanged("Ports");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
