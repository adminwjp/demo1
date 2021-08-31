using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;
using Utility.WinForm.Ctrls;
using Utility.Attributes;
using Utility.Helpers;
using Utility.IO;

namespace Tool
{
    public partial class DockerToolControl :UserControl//: Form
    {
        public DockerToolControl()
        {
            InitializeComponent();
            this.Resize += DockerToolForm_Resize;
            this.tabPageList.Resize += TabPageList_Resize;
            this.tabPageOperator.Resize += TabPageOperator_Resize;
        }

        private void TabPageOperator_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this.tabPageOperator,()=> {

                int height = (this.tabPageOperator.Height - 60) / 2;
                int width = this.tabPageOperator.Width - 250;
                labelCmd.Left = 20;
                labelCmd.Top = 20;
                richTextBoxCmd.Left = labelCmd.Right + 20;
                richTextBoxCmd.Top = labelCmd.Top;
                richTextBoxCmd.Height = height;
                richTextBoxCmd.Width = width;

                labelRes.Left = 20;
                labelRes.Top = richTextBoxCmd.Bottom + 20;
                richTextBoxRes.Left = labelRes.Right + 20;
                richTextBoxRes.Top = labelRes.Top;
                richTextBoxRes.Height = height;
                richTextBoxRes.Width = width;

                int btnWidth = this.buttonPull.Width;
                int btnMargin = (250 - btnWidth) / 2;
                buttonSearch.Location = new Point(labelCmd.Right + 20 + width + btnMargin, -20);
                //buttonSearch.Left = labelCmd.Right +20+ width + btnMargin;
                //buttonSearch.Top = labelCmd.Top;

                buttonPull.Left = buttonSearch.Left;
                buttonPull.Top = buttonSearch.Bottom + 20;

                buttonSearch.Left = buttonPull.Left;
                buttonSearch.Top = buttonPull.Bottom + 20;

                buttonRunCmd.Left = buttonSearch.Left;
                buttonRunCmd.Top = buttonSearch.Bottom + 20;

                buttonSave.Left = buttonRunCmd.Left;
                buttonSave.Top = buttonRunCmd.Bottom + 20;
            });
        }

        private void TabPageList_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this, () => {
                ResizeList();
            });
        }

        private void DockerToolForm_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this, () =>
            {
                tabControl.Location = new Point(0, this.menuStrip.Height);
                tabControl.Width = this.Width;
                tabControl.Height = this.Height - this.menuStrip.Height;
               // ResizeList();
            });
           
        }
        void ResizeList()
        {
            var height = this.tabPageList.Height - 60;
            this.labelImage.Location = new Point((this.tabPageList.Width - this.labelImage.Width) / 2, this.tabPageList.Top + (30 - this.labelImage.Height) / 2);
            this.dataGridViewImage.Width = this.tabPageList.Width-20;
            this.dataGridViewImage.Height = height / 2;
            this.dataGridViewImage.Location = new Point(this.tabPageList.Left+5, this.tabPageList.Top + 30);

            this.labelContainer.Location = new Point((this.tabPageList.Width - this.labelContainer.Width) / 2, this.tabPageList.Top + height / 2 + 30 + (30 - this.labelContainer.Height) / 2);
            this.dataGridViewContainer.Width = this.tabPageList.Width-20;
            this.dataGridViewContainer.Height = height - height / 2;
            this.dataGridViewContainer.Location = new Point(this.tabPageList.Left + 5, this.tabPageList.Top + 60 + height / 2);
        }
        /// <summary>
        /// 获取所有容器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 获取所有容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinFormHelper.Set(this, () =>
            {
                var result = DockerCmdHelper.GetContainers<ContainerEntity>();
                this.dataGridViewContainer.DataGridView.DataSource = result;
            });


        }

        private void DockerToolForm_Load(object sender, EventArgs e)
        {
            //InitGrid();
        }
        private readonly  static Type ContainerType=typeof(ContainerEntity);
        private readonly static Type ImageType = typeof(ImageEntity);
        public static readonly List<GridEntity> ContainerGridEntities = new List<GridEntity>();
        public static readonly List<GridEntity> ImageGridEntities = new List<GridEntity>();
        public void InitGrid()
        {
            if (ContainerGridEntities.Count == 0)
            {
                foreach (var item in ContainerType.GetProperties())
                {
                    if(item.Name== "ContainerId")
                    {
                        ContainerGridEntities.Insert(0,new GridEntity()
                        {
                            Name = item.Name,
                            Text ="全选",
                            GridFlag = GridFlag.CheckBox
                        });
                    }
                    ContainerGridEntities.Add(new GridEntity() { 
                        Name=item.Name,
                        Readonly = true,
                        Text =item.GetCustomAttribute<HeaderAttribute>().Name
                    });
                }
                this.dataGridViewContainer.GridEntities.AddRange(ContainerGridEntities);
            }
            this.dataGridViewContainer.OnHeaderChangeEvent?.Invoke();
            this.dataGridViewContainer.DataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dataGridViewContainer.DataGridView.MultiSelect = true;
            this.dataGridViewContainer.DataGridView.CellContentClick += DataGridView_CellContentClick;
            this.dataGridViewContainer.DataGridView.CellClick += DataGridView_CellClick;
            if (ImageGridEntities.Count == 0)
            {
                foreach (var item in ImageType.GetProperties())
                {
                    if (item.Name == "ImageID")
                    {
                        ImageGridEntities.Insert(0,new GridEntity()
                        {
                            Name = item.Name,
                            Text = "全选",
                            GridFlag= GridFlag.CheckBox
                        });
                    }
                    ImageGridEntities.Add(new GridEntity()
                    {
                        Name = item.Name,
                        Readonly = true,
                        Text = item.GetCustomAttribute<HeaderAttribute>().Name
                    });
                }
                this.dataGridViewImage.GridEntities.AddRange(ImageGridEntities);
            }
            this.dataGridViewImage.OnHeaderChangeEvent?.Invoke();
            this.dataGridViewImage.DataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dataGridViewImage.DataGridView.MultiSelect = true;
            this.dataGridViewImage.DataGridView.CellContentClick += DataGridView_CellContentClick;
            this.dataGridViewImage.DataGridView.CellClick += DataGridView_CellClick;
        }
        bool _all = false;
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                if (sender is DataGridView gridView)
                {
                    _all = !_all; 
                    gridView.CurrentCell=null;//选中的单元格 失效bug  清除光标选中
                     var flag = _all;
                    for (int i = 0; i < gridView.Rows.Count; i++)
                    {
                        gridView.Rows[i].Cells[0].Value = flag;
                    }
                }
            }
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                if (sender is DataGridView gridView)
                {
                    if (gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value is bool flag)
                    {
                        gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !flag;
                    }
                    else
                    {
                        gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                    }
                }
            }
        }
        //Task _imageTask;
        private void 获取所有镜像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(_imageTask!=null&& !_imageTask.IsCompleted)
            //{
            //    MessageBox.Show("获取所有镜像任务正在进行中!");
            //}
            //_imageTask = Task.Factory.StartNew(() =>
           // {
                WinFormHelper.Set(this, () =>
                {
                    var result = DockerCmdHelper.GetImages();
                    this.dataGridViewImage.DataGridView.DataSource = result;
                });
            //});
        }

        private void 删除镜像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCmd(DockerFlag.Rmi,false);
        }
        /// <summary>
        /// 有时会卡 bug
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="container"></param>
        private void RunCmd(DockerFlag flag,bool container=true)
        {
            WinFormHelper.Set(this, () => {
                var ids = GetIds(container);
                DockerHelper.RunCmd(flag,ids,container);
            });
        }
        private void 启动镜像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCmd(DockerFlag.Start,false);
        }

        private List<string> GetIds(bool container=true)
        {
            List<string> _ids = new List<string>();
            var rows = container?this.dataGridViewContainer.DataGridView.Rows: this.dataGridViewImage.DataGridView.Rows;
            foreach (DataGridViewRow item in rows)
            {
                if(item.Cells[0].Value is bool flag)
                {
                    if(flag)
                    {
                        _ids.Add(item.Cells[container ? 1 : 3].Value?.ToString());
                    }
                }
            }
            return _ids; 
        }
        private void 停止镜像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCmd(DockerFlag.Stop,false);
        }

        private void 删除容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCmd(DockerFlag.Rm);
        }

        private void 启动容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCmd(DockerFlag.Start);
        }

        private void 停止容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCmd(DockerFlag.Stop);
        }

        private void 停止DockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinFormHelper.Set(this, () => {
                DockerHelper.StopDocker();
            });
        }

        private void 启动DockerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WinFormHelper.Set(this, () => {
                DockerHelper.StartDocker();
            });
        }

        private void 获取所有镜像IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂无实现!");
        }

        private void 搜索镜像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂无实现!");
        }

        private void 拉取镜像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂无实现!");
        }

        private void 获取所有容器IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂无实现!");
        }

        private void 运行容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂无实现!");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var cmd = this.richTextBoxCmd.Text;
            if (string.IsNullOrEmpty(cmd))
            {
                MessageBox.Show("搜索镜像命令不能为空!");
                return;
            }
            cmd = RegexHelper.GetValue(cmd, "(docker\\s*search\\s*)?(.*?)", 2, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            WinFormHelper.Set(this.richTextBoxRes, DockerCmdHelper.Run(cmd,DockerFlag.Search));
        }

        private void buttonPull_Click(object sender, EventArgs e)
        {
            var cmd = this.richTextBoxCmd.Text;
            if (string.IsNullOrEmpty(cmd))
            {
                MessageBox.Show("拉取镜像命令不能为空!");
                return;
            }
            cmd = RegexHelper.GetValue(cmd, "docker\\s*pull\\s*(.*?)", 1, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            WinFormHelper.Set(this.richTextBoxRes, DockerCmdHelper.Run(cmd, DockerFlag.Pull));
        }

        private void buttonRunCmd_Click(object sender, EventArgs e)
        {
            var cmd = this.richTextBoxCmd.Text;
            if (string.IsNullOrEmpty(cmd))
            {
                MessageBox.Show("执行命令不能为空!");
                return;
            }
           var matches= RegexHelper.Matches(cmd, "(.*?)[\r|\n]");
            string[] cmds = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                cmds[i] = matches[i].Groups[0].Value;
            }
            WinFormHelper.Set(this.richTextBoxRes, string.Join("\r\n", DockerCmdHelper.Run(cmds, DockerFlag.Run).Select(it=>it)));
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var cmd = this.richTextBoxCmd.Text;
            if (string.IsNullOrEmpty(cmd))
            {
                MessageBox.Show("执行命令不能为空!");
                return;
            }
            FileDialog dialog = new SaveFileDialog();
            if(dialog.ShowDialog()== DialogResult.OK)
            {
                var file = dialog.FileName;
                FileHelper.WriteFile(file, cmd);
            }
        }
    }
}
