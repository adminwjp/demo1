using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.Helpers;

namespace Tool
{
    public partial class ToolControl : UserControl
    {
        public ToolControl()
        {
            InitializeComponent();
            OriginalStr = "{\"row\":[\"\t\"],\"end\":[\"\r\",\"\n\"]}";
        }
        string originalStr, currentStr,parseFormat;
        public virtual string OriginalStr
        {
            get
            {
                return this.rtxt_original.Text;
            }
            set
            {
                Action action = () =>
                {
                    this.rtxt_original.Text = value;
                    this.originalStr = value;
                };
                if (this.rtxt_original.InvokeRequired) this.rtxt_original.Invoke(action);
                else action();
            }
        }
        public virtual string CurrentStr
        {
            get
            {
                return this.rtxt_current.Text;
            }
            set
            {
                Action action = () =>
                {
                    this.rtxt_current.Text = value;
                    this.currentStr = value;
                };
                if (this.rtxt_current.InvokeRequired) this.rtxt_current.Invoke(action);
                else action();
            }
        }
        const string N = "\n";
        const string SStart = "/// <summary>";
        const string SEnd = "/// </summary>";
        const string PStart = "/// <para>";
        const string PEnd = "</para>";
        //格式 不同 不好解析 只能 特定 规则下解析
        private void Btn_parse_Click(object sender, EventArgs e)
        {
           // Dictionary<string, List<string>> datas = Utility.JsonUtils.Instance.ToObject<Dictionary<string, List<string>>>(this.ParseFormat);
            StringBuilder stringBuilder = new StringBuilder();
            char[] chars = this.OriginalStr.ToCharArray();
            int count = 0;
            var c = '\n';
            if (this.txt_parseFormat.Text == " ")
            {
                c = ' ';
            }
            List<int> indexs = new List<int>();
            for (int i = 0; i < chars.Length; i++)
            {
               
                if (chars[i] == '\t')
                {
                    if (indexs.Count >= 1)
                    {
                        count = 0;
                        for (int j = 0; j < indexs.Count - 1; j++)
                        {
                            chars[indexs[j]] = ' ';
                        }
                        indexs.Clear();
                    }
                    count++;
                }
                if (count != 4)
                {
                    if (chars[i] == c)
                    {
                        chars[i] = ' ';
                    }
                }
                else
                {
                    if (chars[i] == c)
                    {
                        indexs.Add(i);
                    }
                }
            }
            //string str = new string(chars);
            //MessageBox.Show($"\"{new String(chars).Replace("\t", "\\t").Replace("\r", "\\r").Replace("\n", "\\n")}\"");
            string[] rows = (this.OriginalStr//= new String(chars)
                ).Split(c);
            for (int i = 1; i < rows.Length; i++)
            {
                try
                {
                    string[] cells = rows[i].Split('\t');
                    for (int j = 0; j < cells.Length; j++)
                    {
                        if (j == 0 )
                        {
                            stringBuilder.Append(SStart).Append(N);
                            if(!RegexHelper.IsMatch(cells[j], RegexHelper.Letter))
                            {
                                continue;
                            }
                            if (c == '\n')
                            {
                                stringBuilder.Append($"///{cells[j]}").Append(N);
                                continue;
                            }
                        }
                        else if (j == 1 )
                        {
                            if ( c == ' '|| RegexHelper.IsMatch(cells[j], RegexHelper.Letter))
                            {
                                stringBuilder.Append($"///{cells[j]}").Append(N);
                                stringBuilder.Append(PStart).Append($"{cells[0]}").Append(PEnd).Append(N);
                            }
                            continue;
                        }
                        stringBuilder.Append(PStart).Append($"{cells[j]}").Append(PEnd).Append(N);
                    }
                    
                    if (c == '\n')
                    {
                        if (cells.Length > 0&&RegexHelper.IsMatch(cells[0], RegexHelper.Letter))
                        {
                            stringBuilder.Append(SEnd).Append(N).Append(cells[0]).Append(",").Append(N);
                        }
                        else if (cells.Length > 1 && RegexHelper.IsMatch(cells[1], RegexHelper.Letter))
                            stringBuilder.Append(SEnd).Append(N).Append(cells[1]).Append(",").Append(N);
                    }
                    else
                    {
                        if (cells.Length > 1)
                            stringBuilder.Append(SEnd).Append(N).Append(cells[1]).Append(",").Append(N);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            this.CurrentStr = stringBuilder.ToString();
        }

        public virtual string ParseFormat
        {
            get
            {
                return this.txt_parseFormat.Text;
            }
            set
            {
                Action action = () =>
                {
                    this.txt_parseFormat.Text = value;
                    this.parseFormat = value;
                };
                if (this.txt_parseFormat.InvokeRequired) this.txt_parseFormat.Invoke(action);
                else action();
            }
        }
        private void Btn_parseString_Click(object sender, EventArgs e)
        {
            this.CurrentStr = $"\"{this.OriginalStr.Replace("\t", "\\t").Replace("\r", "\\r").Replace("\n", "\\n")}\"";
        }
        
    }
}
