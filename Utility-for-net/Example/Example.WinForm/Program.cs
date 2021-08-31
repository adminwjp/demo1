using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;

namespace Example.WinForm
{

    public static  class Program
    {

        //垃圾玩意儿 没心情了 重启?
        //可视化 怎么没有 了 坑 编译通过 vs 显示 语法错误
        [STAThread]
        public static void Main(string[] args)
        {
            ApplicationHelper.Start();
            //Application.Run(new MainForm());
            Application.Run(new Tool.MainForm());
        }
    }
}
