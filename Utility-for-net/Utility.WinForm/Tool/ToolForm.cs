using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
        }
        private readonly string str1 = "-1	系统繁忙，此时请开发者稍候再试\r\n0   请求成功\r\n40001   AppSecret错误或者AppSecret不属于这个公众号，请开发者确认AppSecret的正确性\r\n40002 请确保grant_type字段值为client_credential\r\n40164 调用接口的IP地址不在白名单中，请在接口IP白名单中进行设置。（小程序及小游戏调用不要求IP地址在白名单内。）";
        private readonly string str2 = "{\"kf_account\" : \"test1@test\",\"nickname\" : \"客服1\", \"password\" : \"pswmd5\",}";
        private void btn_parse_Click(object sender, EventArgs e)
        {
            string val = this.rtxb_expl.Text;
            if (string.IsNullOrEmpty(val))
            {
                MessageBox.Show("解析失败!", "提示:", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                return;
            }
            //示例1
            if (this.rb_one.Checked)
            {
                OneExpl(val);
            }
            //示例2
            else if (this.rb_two.Checked)
            {
                TwoExpl(val);
            }
            //示例3
            else if (this.rb_three.Checked)
            {

            }
        }
        /// <summary>
        /// 默认加载时显示示例1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolForm_Load(object sender, EventArgs e)
        {
            OneExpl(str1);
        }
        /// <summary>
        /// 示例1解析
        /// </summary>
        /// <param name="str"></param>
        void OneExpl(string str)
        {
            this.rtxb_expl.Text = str;
            str= str.Replace("\r\n", "\r"); 
            StringBuilder builder = new StringBuilder(str.Length+100);
            builder.Append("Dictionary<string,string> dictionary=new Dictionary<string,string>(){");
            foreach (var item in str.Split('\r'))
            {
                string st = Regex.Replace(item, "([\t ]+)", " ");
                string[] strs = st.Replace("\r", "").Replace("\n","").Split(' ');
                if (strs.Length == 2)
                {
                    builder.Append("[\"").Append(strs[0]).Append("\"]=").Append("\"").Append(strs[1]).Append("\",\r\n");
                }
                else
                {
                    MessageBox.Show($"解析失败!\r\n{st}","提示:", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                    return;
                }
            }
            string chars = builder.ToString();
            chars= chars.Remove(chars.LastIndexOf(','),1);
            this.rtxb_code.Text = chars+"};";
        }
        /// <summary>
        /// 示例2解析
        /// </summary>
        /// <param name="str"></param>
        void TwoExpl(string str)
        {
            this.rtxb_expl.Text = str;
            str = str.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            this.rtxb_code.Text= str.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Replace("\"","\\\"");
        }
        /// <summary>
        /// 示例1选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_one_CheckedChanged(object sender, EventArgs e)
        {
           this.RBOne(str1);
        }
        /// <summary>
        /// 示例1解析
        /// </summary>
        /// <param name="str"></param>
        void RBOne(string str)
        {
            if (this.rb_one.Checked)
            {
                this.rb_one.Checked = true;
                OneExpl(str1);
            }
            else
            {
                this.rb_one.Checked = false;
                this.Clear();
            }
        }
        void RadioButtoneEvent(object sender)
        {
            if (sender is RadioButton)
            {
                RadioButton radioButton = ((RadioButton)sender);
                if(object.ReferenceEquals(this.rb_one,radioButton))
                {
                    this.rb_one.Checked = !this.rb_one.Checked;
                    if (this.rb_one.Checked)
                    {
                        OneExpl(str1);
                    }
                }
                
            }
        }
        /// <summary>
        /// 取消事件,即清空文本框的值
        /// </summary>
        /// <param name="sender">点击对象</param>
        /// <param name="e">事件</param>
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Clear();
        }
        void Clear()
        {
            this.rtxb_expl.Text = string.Empty;
            this.rtxb_code.Text = string.Empty;
        }
        /// <summary>
        /// 示例2选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_two_CheckedChanged(object sender, EventArgs e)
        {
            RBTwo(str2);
        }
        /// <summary>
        /// 示例2解析
        /// </summary>
        /// <param name="str"></param>
        void RBTwo(string str)
        {
            if (this.rb_two.Checked)
            {
                this.rb_two.Checked = true;
                TwoExpl(str);
            }
            else
            {
                this.rb_two.Checked = false;
                this.Clear();
            }
        }
    }
}
