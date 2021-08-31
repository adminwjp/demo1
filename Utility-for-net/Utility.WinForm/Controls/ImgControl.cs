using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;

namespace Controls
{
    public partial class ImgControl : UserControl
    {
        public ImgControl()
        {
            InitializeComponent();
            this.Paint += ImgControl_Paint;
        }
        public Margin CustomMargin { get; set; } = new Margin() { Top = 30, Bottom = 50, Left =70, Right = 20 };
        public float Max { get; set; } = 10;
        public float Min { get; set; } = -1;
        public string Unit { get; set; } = "mm/s";
        public string Title { get; set; } = "标题";
        public string StrCheck { get; set; } = "通道1";
        string _right = "√";
        private void ImgControl_Paint(object sender, PaintEventArgs e)
        {
            int height = this.Height - CustomMargin.Top - CustomMargin.Bottom;
            CustomMargin.Right +=50;
            int avgCheck = (height-2*8) / 8;
            avgCheck = avgCheck > 20 ? 20 : avgCheck;
            Size checkSize = Size.Empty;
            Font checkFont = DrawUtils.GetFont(Font, StrCheck, 50-avgCheck, 20, (it) =>
            {
                checkSize = TextRenderer.MeasureText(StrCheck, it);
                return new DrawUtils.FontEx() { Width = checkSize.Width + 10, Height = checkSize.Height + 10, NotPass = it.Size < 1 };
            }, 5);
            int avg = (height - avgCheck * 8) / 8;
            //for (int i = 0; i < 8; i++)
            //{
            //    TextBoxRenderer.DrawTextBox(e.Graphics, new Rectangle(Width - CustomMargin.Right + 50 - 5 - checkSize.Width - avgCheck, CustomMargin.Top+(avgCheck + avg)*i, avgCheck, avgCheck),
            // System.Windows.Forms.VisualStyles.TextBoxState.Selected);
            //    TextRenderer.DrawText(e.Graphics, Unit, checkFont, new Point(Width - CustomMargin.Right + 50 - 5 - checkSize.Width, (int)(CustomMargin.Top + 
            //        (avgCheck + (avgCheck + avg) * i)/2 - (float)checkSize.Height / 2)), Color.Black);
            //}
            float y1 = this.Height - CustomMargin.Bottom;
            e.Graphics.DrawLine(Pens.Black,CustomMargin.Left,CustomMargin.Top,CustomMargin.Left,y1);//y
            e.Graphics.DrawLine(Pens.Black, CustomMargin.Left, y1, this.Width - CustomMargin.Right,y1 );//x
            //y
            string maxStr = Max.ToString("0.0000"), minStr = Min.ToString("0.0000");
            string yStr = maxStr.Length > minStr.Length ? maxStr : minStr;
            yStr = $"{yStr}{Unit}";
           
            Size titleSize = Size.Empty;
            Font titleFont = DrawUtils.GetFont(Font, Title, CustomMargin.Left, height, (it) =>
            {
                titleSize = TextRenderer.MeasureText(yStr, it);
                return new DrawUtils.FontEx() { Width = titleSize.Width + 10, Height = titleSize.Height+10, NotPass = it.Size<1};
            }, 5);
            TextRenderer.DrawText(e.Graphics, Title, titleFont, new Point((Width- titleSize.Width)/2, (int)(CustomMargin.Top - (float)titleSize.Height / 2)), Color.Black);
            Size ySize = Size.Empty;
            Font yTempFont = DrawUtils.GetFont(Font, yStr, CustomMargin.Left, height, (it) =>
            {
                ySize = TextRenderer.MeasureText(yStr, it);
                return new DrawUtils.FontEx() {Width=ySize.Width+10,Height=(ySize.Height + 2)*10,NotPass = it.Size < 1 };
            }, 5);
            //x
            float time = 1.0f;
            float[] tiemFs = new float[10];
            string xStr = string.Empty;
            for (int i = 1; i < 11; i++)
            {
                tiemFs[i-1] = time/10 * i;
                string temp = tiemFs[i-1].ToString();
                xStr = temp.Length > xStr.Length ? temp : xStr;
            }
            Size xSize = Size.Empty;
            Font xTempFont = DrawUtils.GetFont(Font, xStr,Width- CustomMargin.Left-CustomMargin.Right, height, (it) =>
            {
                xSize = TextRenderer.MeasureText(xStr, it);
                return new DrawUtils.FontEx() { Width = (xSize.Width + 2)*10, Height = xSize.Height + 5, NotPass = it.Size < 1 };
            }, 5);
            yTempFont = xTempFont = yTempFont.Size > xTempFont.Size ? xTempFont : yTempFont;
            float avgHeight = (float)height / 10;
            float avgWidth =(float)(this.Width - CustomMargin.Left - CustomMargin.Right) / 10;
            float avgVal = (Max - Min)/10;
            for (int i = 0; i < 11; i++)
            {
                var y = y1;
                var x = (float)CustomMargin.Left;
                if(i==0)
                {
                    y = y1;
                    x = CustomMargin.Left;
                }
                else if (i == 10)
                {
                    y = CustomMargin.Top;
                    x = this.Width-CustomMargin.Right;
                }
                else
                {
                    y = y1 - avgHeight * i;
                    x = CustomMargin.Left + avgWidth * i;
                }
                //y
                e.Graphics.DrawLine(Pens.Black, CustomMargin.Left,y, CustomMargin.Left - 5, y);
                yStr = (Min + avgVal * i).ToString("0.0000");
                Size yStrSize = TextRenderer.MeasureText(yStr, yTempFont);
                TextRenderer.DrawText(e.Graphics, (Min + avgVal * i).ToString("0.0000"), yTempFont, new Point(CustomMargin.Left-5-yStrSize.Width,(int)(y - (float)yStrSize.Height / 2)), Color.Black);
                //x
                e.Graphics.DrawLine(Pens.Black, x, this.Height-CustomMargin.Bottom, x, this.Height - CustomMargin.Bottom+5);
                xStr = i == 0 ? "0" : tiemFs[i - 1].ToString();
                Size xStrSize =TextRenderer.MeasureText(xStr, xTempFont);
                TextRenderer.DrawText(e.Graphics, xStr, xTempFont, new Point((int)(x - (float)xStrSize.Width / 2), this.Height - CustomMargin.Bottom + 5), Color.Black);
            }
            Size yUnitStrSize = TextRenderer.MeasureText(Unit, yTempFont);
            TextRenderer.DrawText(e.Graphics, Unit, yTempFont, new Point(15, (int)(Height-CustomMargin.Bottom-height/2 - (float)yUnitStrSize.Height / 2)), Color.Black);
        }
    }
    public class Margin
    {
        public  int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public static implicit operator MarginF(Margin margin)
        {
            return new MarginF() { Top = margin.Top, Left = margin.Left, Right = margin.Right, Bottom = margin.Bottom };
        }
    }
    public class MarginF
    {
        public float Top { get; set; }
        public float Left { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
        public static implicit operator Margin(MarginF marginF)
        {
            return new Margin() { Top=(int)marginF.Top, Left = (int)marginF.Left, Right = (int)marginF.Right, Bottom = (int)marginF.Bottom };
        }
    }
}
