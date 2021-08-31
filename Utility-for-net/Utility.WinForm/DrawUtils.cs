#if true//NET20 ||  NET30 ||  NET35 ||  NET40 ||  NET45 ||  NET451 ||  NET452 ||  NET46 ||  NET461 ||  NET462 ||  NET47 ||  NET471 ||  NET472 ||  NET48
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.WinForm
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    ///  公共类
    /// </summary>
    public static class DrawUtils
    {
        public struct FontEx
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public bool NotPass { get; set; }
        }
        public static Font GetFont(Font font,string str,int width,int height,Func<Font,FontEx> func,int margin)
        {
            Font currentFont = font;
            int flag = 0;
            while (true)
            {
                FontEx fontEx = func(currentFont);
                if (fontEx.NotPass) break;
                if (width - fontEx.Width > margin && height - fontEx.Height > margin)
                {
                    if (flag == -1) break;
                    currentFont = new Font(currentFont.FontFamily, currentFont.Size + 1, currentFont.Style);
                    flag = 1;
                }
                else
                {
                    if (flag == 1|| currentFont.Size - 1<1) break;
                    currentFont = new Font(currentFont.FontFamily, currentFont.Size - 1, currentFont.Style);
                    flag = -1;
                }
            }
            return currentFont;
        }
        /// <summary>
        ///绘字符串字体
        /// </summary>
        public static Font Font = new Font("宋体",10);
        /// <summary>
        ///鼠标悬浮 数据 区域显示矩形
        /// </summary>
        public static RectangleF Border = new RectangleF(0,0,5,5);
        /// <summary>
        /// 绘图绘数据 展示方式:分显 绘图区域高度太小 忽略即rectangle.Height lt 1 ,无数据 忽略即data==null||datas.Length == 0
        /// <para>数据不一致 忽略 即displays==null|| displays.Length != datas.Length</para>
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="rectangle">绘图区域</param>
        /// <param name="datas">1-n 组数据 </param>
        /// <param name="displays">1-n 组数据 每组是否显示 </param>
        /// <param name="mousePoint">鼠标所在位置 </param>
        /// <param name="scrollBar">滚动条<see cref="System.Windows.Forms.ScrollBar"/></param>
        /// <param name="hscrollValue">滚动条滚动的位置数据 </param>
        /// <param name="isShowZeroLine">是否显示0行，默认显示 </param>
        /// <param name="showType">展示方式 1：分显 ;否则全显 默认分显</param>
        public static void DrawGrid(Graphics graphics, RectangleF rectangle,float[][] datas,bool[] displays,PointF? mousePoint,
            System.Windows.Forms.ScrollBar scrollBar,float hscrollValue=0.0f,
            bool isShowZeroLine=true, int showType=1)
        {
            if (showType == 1)
            {
                if (rectangle.Height < 1)
                {
                    return;//绘图区域高度太小 忽略
                }
                else if (datas==null||datas.Length == 0)
                {
                    return;//无数据 忽略
                }
                else if(displays==null|| displays.Length != datas.Length)
                {
                    return;//数据不一致 忽略
                }
                graphics.DrawRectangles(Pens.Black,new RectangleF[1] { rectangle });//绘制绘图矩形区域
                int length = datas.Length;//数据组个数
                float avgHeight = rectangle.Height / (float)length;//每组数据高度
                RectangleF[] rectangles =new RectangleF[length];//声明所有组数据区域
                //初始化所有组数据区域
                for (int i = 0; i < length; i++)
                {
                    rectangles[i] = new RectangleF(rectangle.Left,rectangle.Top+i*avgHeight,rectangle.Width,avgHeight);//初始化每组数据的区域
                }
                //绘数据
                for (int i = 0; i < length; i++)
                {
                    //该组数据是否显示
                    if (displays[i])
                    {

                    }
                }
            }
        }
        /// <summary>
        ///多组数据一个矩形区域 绘图绘数据  绘图区域高度太小 忽略即rectangle.Height lt 1 ,无数据 忽略即data==null||datas.Length lt 2
        ///<para>datas 数据为0 忽略</para>
        ///<para>datas 多组 数据 每组数据个数一致</para>
        /// </summary>
        /// <param name="graphics">绘图对象<see cref="Graphics"/></param>
        /// <param name="rectangle">绘图区域<see cref="RectangleF"/></param>
        /// <param name="datas">一组数据</param>
        /// <param name="colors">颜色<see cref="Color"/> </param>
        /// <param name="mousePoint">鼠标所在位置<see cref="PointF"/> </param>
        /// <param name="scrollBar">滚动条 无效<see cref="System.Windows.Forms.ScrollBar"/></param>
        /// <param name="zoomOut">放大倍数<see cref="float"/> 默认1.0f</param>
        /// <param name="zoomIn">放小倍数<see cref="float"/>默认1.0f</param>
        /// <param name="yCount">y轴分n份<see cref="float"/>默认10</param>
        /// <param name="xCount">x轴分n份<see cref="float"/>默认10</param>
        public static void DrawMulitData(Graphics graphics, RectangleF rectangle, float[][] datas, Color[] colors, PointF? mousePoint,
            System.Windows.Forms.ScrollBar scrollBar = null, float zoomOut = 1.0f, float zoomIn = 1.0f, int yCount = 10, int xCount = 10)
        {
            if (rectangle.Height < 1)
            {
                return;//绘图区域高度太小 忽略
            }
            DrawGrid(graphics, rectangle, yCount, xCount);
            if (datas == null || datas.Length < 2)
            {
                return;//无数据 忽略
            }
            float[] maxAndMins = new float[datas.Length * 2];
            for (int i = 0; i < datas.Length; i++)
            {
                Array.Copy(GetMaxAndMin(datas[i]),0, maxAndMins,i*2, 2);
            }
            float[] maxAndMin = GetMaxAndMin(maxAndMins);
            if (maxAndMin[0] == 0 && maxAndMin[1] == 0)
            {
                return; //数据为0 忽略
            }
            bool f = maxAndMin[1] < 0;
            maxAndMin[0] = Math.Abs(maxAndMin[0]);
            maxAndMin[1] = Math.Abs(maxAndMin[1]);
            var avg = maxAndMin[0] > maxAndMin[1] ? maxAndMin[0] : maxAndMin[1];
            int length = datas.Length;//数据组个数
            float avgWidth = rectangle.Width / (float)datas[0].Length;//每组数据宽度
            float avgHeight = rectangle.Height / avg / (f ? 2 : 1);//每组数据高度
            float avgW = f ? avg : 0;
            for (int i = 0; i < length; i++)
            {
                Pen pen = new Pen(colors[i]);
                for (int j = 0; j < datas[i][j]; j++)
                {
                    float x1 = rectangle.Left + avgWidth * j, x2 = rectangle.Left + avgWidth * (j + 1),
                                        y1 = (datas[i][j] + avgW) * avgHeight, y2 = (datas[i][j + 1] + avgW) * avgHeight;
                    graphics.DrawLine(pen, x1, y1, x2, y2);
                    DrawStringAndBorder(graphics, rectangle, colors[i], i, datas[i][j], avgWidth, mousePoint, x1, y1, x2, y2);
                }
                
            }
        }
        /// <summary>
        ///每组数据一个矩形区域 绘图绘数据  绘图区域高度太小 忽略即rectangle.Height lt 1 ,无数据 忽略即data==null||datas.Length gt 1
        ///<para>datas 数据为0 忽略</para>
        /// </summary>
        /// <param name="graphics">绘图对象<see cref="Graphics"/></param>
        /// <param name="rectangle">绘图区域<see cref="RectangleF"/></param>
        /// <param name="index">位置即通道</param>
        /// <param name="datas">一组数据</param>
        /// <param name="color">颜色<see cref="Color"/> </param>
        /// <param name="mousePoint">鼠标所在位置<see cref="PointF"/> </param>
        /// <param name="scrollBar">滚动条 无效<see cref="System.Windows.Forms.ScrollBar"/></param>
        /// <param name="zoomOut">放大倍数<see cref="float"/> 默认1.0f</param>
        /// <param name="zoomIn">放小倍数<see cref="float"/>默认1.0f</param>
        /// <param name="yCount">y轴分n份<see cref="float"/>默认10</param>
        /// <param name="xCount">x轴分n份<see cref="float"/>默认10</param>
        public static void DrawOneData(Graphics graphics, RectangleF rectangle,int index, float[] datas,Color color, PointF? mousePoint,
            System.Windows.Forms.ScrollBar scrollBar=null,float zoomOut=1.0f,float zoomIn=1.0f,int yCount=10,int xCount=10)
        {
            if (rectangle.Height < 1)
            {
                return;//绘图区域高度太小 忽略
            }
            DrawGrid(graphics,rectangle,yCount,xCount);
            if (datas == null || datas.Length > 1)
            {
                return;//无数据 忽略
            }
            float[] maxAndMin = GetMaxAndMin(datas);
            if (maxAndMin[0] == 0 && maxAndMin[1] == 0)
            {
                return; //数据为0 忽略
            }
            bool f = maxAndMin[1] < 0;
            maxAndMin[0] = Math.Abs(maxAndMin[0]);
            maxAndMin[1] = Math.Abs(maxAndMin[1]);
            var avg = maxAndMin[0] > maxAndMin[1] ? maxAndMin[0] : maxAndMin[1];
            int length = datas.Length;//数据组个数
            float avgWidth = rectangle.Width / (float)length;//每组数据宽度
            float avgHeight = rectangle.Height / avg/(f?2:1);//每组数据高度
            float avgW = f ? avg : 0;
            Pen pen = new Pen(color);
            for (int i = 0; i < length; i++)
            {
                float x1= rectangle.Left+avgWidth*i,x2= rectangle.Left + avgWidth * (i+1),
                    y1 = (datas[i] + avgW) * avgHeight,y2=(datas[i+1] + avgW) *avgHeight;
                graphics.DrawLine(pen, x1,y1,x2,y2);
                DrawStringAndBorder(graphics,rectangle,color, index, datas[i],avgWidth,mousePoint,x1,y1,x2,y2);
            }
        }
        /// <summary>
        ///绘制字符串
        /// </summary>
        /// <param name="graphics">绘图对象<see cref="Graphics"/></param>
        /// <param name="rectangle">绘图区域<see cref="RectangleF"/></param>
        /// <param name="color">颜色<see cref="Color"/> </param>
        /// <param name="index">位置即通道</param>
        /// <param name="data">数据</param>
        /// <param name="avgWidth">同比宽度 ，即鼠标所在位置范围</param>
        /// <param name="mousePoint">鼠标所在位置<see cref="PointF"/> </param>
        /// <param name="x1">开始位置x </param>
        /// <param name="y1">开始位置y</param>
        /// <param name="x2">下个位置x</param>
        /// <param name="y2">下个位置y</param>
        public static void DrawStringAndBorder(Graphics graphics,RectangleF rectangle,Color color,int index,float data,float avgWidth, PointF? mousePoint,
            float x1,float y1,float x2,float y2)
        {
            if (mousePoint.HasValue)
            {
                string str = "通道" + index + "y:" + data.ToString();
                SizeF sizeF = GetStringSizeF(graphics,str);
                if (mousePoint.Value.X - avgWidth <= x1 || mousePoint.Value.X + avgWidth >= x1)
                {
                    x1 = rectangle.Right - x1 - sizeF.Width > 0 ? x1 : rectangle.Right - sizeF.Width;
                    graphics.DrawString(str, Font, new SolidBrush(color), x1, y1);

                    RectangleF border = Border;
                    graphics.DrawRectangle(new Pen(color), border.Left,border.Top,border.Width,border.Height);
                    graphics.FillRectangle(new SolidBrush(color), border.Left, border.Top, border.Width, border.Height);
                }
                else if (mousePoint.Value.X - avgWidth <= x2 || mousePoint.Value.X + avgWidth >= x2)
                {
                    x2 = rectangle.Right - x2 - sizeF.Width > 0 ? x2 : rectangle.Right - sizeF.Width;
                    graphics.DrawString(str, Font, new SolidBrush(color), x2, y2);
                    RectangleF border = Border;
                    graphics.FillRectangle(new SolidBrush(color), border.Left, border.Top, border.Width, border.Height);
                }
            }
        }
        /// <summary>
        /// 获取字符串字体大小
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static SizeF GetStringSizeF(Graphics graphics,string str)
        {
            return graphics.MeasureString(str, Font);
        }
        /// <summary>
        /// 获取最大值最小值 
        /// </summary>
        /// <param name="datas">数据</param>
        /// <returns>float[0] max float[1] min  </returns>
        public static float[] GetMaxAndMin(float[] datas)
        {
            float[] fs = new float[] { datas[0], datas[0] };
            foreach (var item in datas)
            {
                if (fs[0] < item)
                {
                    fs[0] = item;
                }
                if (fs[1] > item)
                {
                    fs[1] = item;
                }
            }
            return fs;
        }
        /// <summary>
        ///绘制网格 
        /// </summary>
        /// <param name="graphics">绘图对象<see cref="Graphics"/></param>
        /// <param name="rectangle">绘图区域<see cref="RectangleF"/></param>
        /// <param name="yCount">y轴分n份<see cref="float"/>默认10</param>
        /// <param name="xCount">x轴分n份<see cref="float"/>默认10</param>
        public static void DrawGrid(Graphics graphics, RectangleF rectangle, int yCount = 10, int xCount = 10)
        {
            graphics.DrawRectangle(Pens.Black, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);//绘制绘图矩形区域
            float avgHeiht = rectangle.Height / yCount,avgWidth=rectangle.Width/xCount;
            Pen pen = new Pen(Color.Black);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 2, 2 };
            for (int i = 1; i < yCount-1; i++)
            {
                graphics.DrawLine(pen, new PointF(rectangle.X+avgWidth*i, rectangle.Y), new PointF(rectangle.X + avgWidth * i, rectangle.Y + rectangle.Height));
            }
            for (int i = 1; i < xCount-1; i++)
            {
                graphics.DrawLine(pen, new PointF(rectangle.X, rectangle.Y + avgHeiht * i), new PointF(rectangle.X + rectangle.Width, rectangle.Y + avgHeiht * i));
            }
        }
    }
}
#endif
