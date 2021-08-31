using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool.Controls;
using Utility.WinForm;

namespace Tool
{
    public partial class MainForm : Form
    {
        private readonly Timer _timer = new Timer();
        public MainForm()
        {
            InitializeComponent();
            this.StartPosition=FormStartPosition.CenterScreen;
            this.Resize += MainForm_Resize;
            this.FormClosed += MainForm_FormClosed;
            this.Time = $"当前时间为:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}";
            InitTimer();
            this.tabPageConsole.Resize += TabPageConsole_Resize;
        }

        private void TabPageConsole_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this.tabPageConsole, () => {
                int height = (this.tabPageConsole.Height - 30) / 2;
                int width = this.tabPageConsole.Width - 20;
                this.groupBoxLog.Location = new Point(this.tabPageConsole.Left + 2, this.tabPageConsole.Top + 2);
                this.groupBoxLog.Width = width - 10;
                this.groupBoxLog.Height = height;
                richTextBoxLog.Location = new Point(groupBoxLog.Location.X + 2, groupBoxLog.Location.Y + 2);
                this.richTextBoxLog.Width = width - 20;
                this.richTextBoxLog.Height = height - 3;

                this.groupBoxTotalLog.Location = new Point(this.tabPageConsole.Left + 2, this.groupBoxLog.Bottom + 2);
                this.groupBoxTotalLog.Width = width - 10;
                this.groupBoxTotalLog.Height = height;
                //richTextBoxTotalLog.Location = new Point(groupBoxTotalLog.Location.X + 2, groupBoxTotalLog.Location.Y + 2);
                richTextBoxTotalLog.Left = richTextBoxLog.Left;
                richTextBoxTotalLog.Width = width - 20;
                richTextBoxTotalLog.Height = height - 3;

            });
        }

        /// <summary>
        /// 底部显示时间
        /// </summary>
        protected string Time
        {
            get
            {
                return this.toolStripStatusLabelTime.Text;
            }
            set
            {
                Action action = () => {
                    this.toolStripStatusLabelTime.Text = value;
                };
                action();
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._timer.Stop();
        }

        private void InitTimer()
        {
            this._timer.Interval = 1000;
            this._timer.Enabled = true;
            this._timer.Tick += Timer_Tick;
            this._timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Time = $"当前时间为:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}";
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this, () =>
            {
                this.treeView.Width = this.Width / (this.Width > 1000 ? 10 : 6);
                this.treeView.Location = new Point(0, this.menuStrip.Height);
                this.treeView.Height = this.Height - this.menuStrip.Height - this.statusStrip.Height;
                this.tabControl.Location = new Point(this.treeView.Width, this.menuStrip.Height);
                this.tabControl.Width = this.Width - this.treeView.Width;
                this.tabControl.Height = this.Height - this.menuStrip.Height - this.statusStrip.Height - 50;

            });
        }
        void CreateTab<T>(string text,Action<T> action=null) where T:UserControl,new()
        {
            WinFormComponent.CreateTab<T>(tabControl, text, action);
        }
        private void net平台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab<TooPlatControl>("Net平台工具");
            //  tooPlatForm.StartPosition = FormStartPosition.CenterParent;
            //  tooPlatForm.ShowDialog();
            //tooPlatForm.StartPosition = FormStartPosition.CenterScreen;
            //tooPlatForm.Show();
        }

        private void 字符串转义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab<StrToolControl>("字符串转义工具");
            //strToolForm.StartPosition = FormStartPosition.CenterParent;
            //strToolForm.ShowDialog();
            //strToolForm.StartPosition = FormStartPosition.CenterScreen;
            //strToolForm.Show();
        }

        private void 爬虫ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeView.AllowDrop = true;
            bool exists = false;
            foreach (TreeNode item in this.treeView.Nodes)
            {
                if(item.Text== "QQ头像")
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                this.treeView.Nodes.Add(new TreeNode("QQ头像"));
            }
        }
        /// <summary>
        /// 为爬虫属性菜单 时 才可以触发此事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var data = e.Data.GetData(DataFormats.FileDrop);
                var str = data.ToString();
                if (str.EndsWith(".dll"))
                {

                }
                else
                {
                    MessageBox.Show($"树形拖动只支持后缀为dll的文件");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"树形拖动异常:{ex.Message}");
            }
        }
        /// <summary>
        /// 只有触发该事件才能触发拖动完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void 爬虫测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Crawl.FiveOneJobSpider().Collect(null);
            //Crawl.DatabaseManger.CreateTable();
            //var response=  new Crawl.QQBodyCollect(new Utility.HttpClientFactory()).Collect(new System.Collections.Hashtable() { ["NeedHtml"] =true});
            //Utility.FileUtils.WriteFile($"{Crawl.AddressManger.SaveDiretory}\\{Crawl.AddressManger.QQBodyAddress}\\{Utility.DateUtils.DateString()}.html", response.Data.ToString());
        }

        private void docker工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab<DockerToolControl>("Docker工具",(it)=> { it.InitGrid(); });
            //DockerToolControl dockerToolForm = new DockerToolControl();
            //dockerToolForm.StartPosition = FormStartPosition.CenterParent;
            //dockerToolForm.ShowDialog();
        }

        private void winForm工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinFormToolForm winFormToolForm = new WinFormToolForm();
            winFormToolForm.StartPosition = FormStartPosition.CenterParent;
            winFormToolForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetOut(new RichTextBoxWriter(this.richTextBoxLog));
        }

        private void rabbitMQ工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab<RabbitMQControl>("RabbitMQ工具");
        }

        private void 工具ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolForm toolForm = new ToolForm();
            //toolForm.Parent = this;
            toolForm.StartPosition = FormStartPosition.CenterScreen;
            toolForm.ShowDialog();
        }

        private void 实体工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab<Tool.ToolControl>("实体工具");
        }
    }
}
