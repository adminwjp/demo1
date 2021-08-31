using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;
using static Tool.PlatformHelper;

namespace Tool
{
    public partial class TooPlatControl : UserControl//: Form
    {
        public TooPlatControl()
        {
            InitializeComponent();
            Init();
            this.Resize += TooPlatControl_Resize;
        }

        private void TooPlatControl_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this,()=> {
                int maxWidth = this.cbo_max_net.Right - this.lbl_support_platform.Left;
                int space = this.cbo_min_net.Left - lbl_min_net.Right;
                int defaultHeight = this.cbo_max_net.Height;
                //int num = 9 + 2;
                int avgHeight =(this.Height -defaultHeight * 11-5*8)/2;
                int margin = (this.Width - maxWidth)/2;

                this.lbl_support_platform.Left = margin;
                chk_net.Left = this.lbl_support_platform.Right + space;
                chk_core.Left = chk_net.Right + space * 3;
                chk_standard.Left = chk_core.Right + space * 3;
                lbl_support_platform.Top = chk_net.Top = chk_core.Top = chk_standard.Top = avgHeight;

                lbl_min_net.Left = Avg(lbl_support_platform.Width - lbl_min_net.Width) / 2 + margin;
                cbo_min_net.Left= lbl_min_net.Right + space;
                lbl_max_net.Left = cbo_min_net.Right + space;
                cbo_max_net.Left = lbl_max_net.Right + space;
                lbl_min_net.Top = cbo_min_net.Top = lbl_max_net.Top = cbo_max_net.Top = chk_standard.Bottom+5;

                lbl_min_standard.Left = Avg(lbl_support_platform.Width - lbl_min_standard.Width) / 2 + margin;
                cbo_min_standard.Left = lbl_min_standard.Right + space;
                lbl_max_standard.Left = cbo_min_standard.Right + space;
                cbo_max_standard.Left = lbl_max_standard.Right + space;
                lbl_min_standard.Top = cbo_min_standard.Top = lbl_max_standard.Top = cbo_max_standard.Top = cbo_max_net.Bottom+5;

                lbl_min_core.Left = Avg(lbl_support_platform.Width - lbl_min_core.Width) / 2 + margin;
                cbo_min_core.Left = lbl_min_core.Right + space;
                lbl_max_core.Left = cbo_min_core.Right + space;
                cbo_max_core.Left = lbl_max_core.Right + space;
                lbl_min_core.Top = cbo_min_core.Top = lbl_max_core.Top = cbo_max_core.Top = cbo_max_standard.Bottom + 5;

                lbl_select.Left = Avg(lbl_support_platform.Width - lbl_select.Width) / 2 + margin;
                rad_btn_or.Left = lbl_select.Right + space;
                rad_btn_and.Left = rad_btn_or.Right + space*3;
                lbl_select.Top = rad_btn_or.Top = rad_btn_and.Top = cbo_max_core.Bottom + 5;

                lbl_compile.Left = Avg(lbl_support_platform.Width - lbl_compile.Width) / 2 + margin;
                chk_code_compile.Left = lbl_compile.Right + space ;
                lbl_compile.Top = chk_code_compile.Top = rad_btn_and.Bottom + 5;

                lbl_input_way.Left = Avg(lbl_support_platform.Width - lbl_input_way.Width) / 2 + margin;
                chk_input_way.Left = lbl_input_way.Right + space;
                lbl_input_way.Top = chk_input_way.Top = chk_code_compile.Bottom + 5;

                btn_ok.Left = lbl_support_platform.Width + margin+space;
                btn_ok.Top =  chk_input_way.Bottom + 5;

                rtxt_result.Left = lbl_support_platform.Width + margin + space;
                rtxt_result.Top = btn_ok.Bottom + 5;
                rtxt_result.Width = maxWidth - lbl_support_platform.Width - space;
            });
        }
        int Avg(int width)
        {
            return width == 0 ? 0 : width / 2;
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this._platformCodition.Or = this.rad_btn_or.Checked;
            StringBuilder builder = new StringBuilder(100);
            this._platformCodition.Net.Enable = this.chk_net.Checked;
            if (this._platformCodition.Net.Enable)
            {
                this._platformCodition.Net.Min = (int)this.cbo_min_net.SelectedValue;
                this._platformCodition.Net.Max = (int)this.cbo_max_net.SelectedValue;
            }
            this._platformCodition.Core.Enable = this.chk_core.Checked;
            if (this._platformCodition.Core.Enable)
            {
                this._platformCodition.Core.Min = (int)this.cbo_min_core.SelectedValue;
                this._platformCodition.Core.Max = (int)this.cbo_max_core.SelectedValue;
            }
            this._platformCodition.Standard.Enable = this.chk_standard.Checked;
            if (this._platformCodition.Standard.Enable)
            {
                this._platformCodition.Standard.Min = (int)this.cbo_min_standard.SelectedValue;
                this._platformCodition.Standard.Max = (int)this.cbo_max_standard.SelectedValue;
            }
            this._platformCodition.CodeCompile = this.chk_code_compile.Checked;
            this._platformCodition.InputWay = this.chk_input_way.Checked;
            string result = PlatformHelper.GetCodition(this._platform, this._platformCodition);
            this.rtxt_result.Text = result;
        }
      
        readonly Platform _platform = PlatformHelper.GetPlatform();
        readonly PlatformCodition _platformCodition = new PlatformCodition();
        void Init()
        {
            //bug 引用同一个对象出现错误 下拉选框 值一起改变
            this.InitComboxData(this.cbo_min_net,this._platform.Net);
            this.InitComboxData(this.cbo_max_net, new List<PlatformCategory>(this._platform.Net));
            this.InitComboxData(this.cbo_min_core, this._platform.Core);
            this.InitComboxData(this.cbo_max_core, new List<PlatformCategory>(this._platform.Core));
            this.InitComboxData(this.cbo_min_standard, this._platform.Standard);
            this.InitComboxData(this.cbo_max_standard, new List<PlatformCategory>(this._platform.Standard));
        }

        void InitComboxData(ComboBox comboBox,List<PlatformCategory> categories)
        {
            comboBox.DataSource = categories;
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Id";
        }
    }
   
}
