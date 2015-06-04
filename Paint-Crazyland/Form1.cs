using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Crazyland
{
    public partial class Form1 : Form
    {
        public enum Tools
        {
            Brush, Eraser, Marquee, Zoom, Fill, Text, Shape, LeftColor, RightColor, ColorPicker, None
        }

        public Tools CurrentTool
        { get; set; }

        Color   m_leftColor, 
                m_rightColor;

        bool    m_isMouseDown, 
                m_isMouseUp, 
                m_isKeyDown, // true khi Keydown
                m_isMouseUpMarqueeShow, // cho phep ve anh Demo len khi Lan dau xac dinh marquee
                m_isMarqueeChosing, // = true khi dang duoc ve len
                m_isMarqueeChosen, // khi marquee da dc xac dinh va cho phep nhap giu chuot de di chuyen vung chon
                m_isMarqueeFinish, // = true khi mouse up va luc nay ve duoc vung chon 
                m_isFistTimeZoom = true, // = true khi chon zoom (CurrentTool != zoom) de save bitmap lai
                m_allowResieWorkSpace,
                m_textClicked,// Khi Text Tool duoc chon thi bien nay se cho biet la da nhap vao workspace luc nay se cho phep go text
                m_allowDrawText,//sau khi ket thuc TextTool cho phep ve string len
                m_allowErase,//cho phep tay trang 1 vung
                m_allowDrawPolygon, // cho phep ve Polygon 
                m_isEdit, //neu co chinh sua thi mang gia tri true va nguoc lai
                m_firstSave; //true khi nguoi dung chua luu lan dau tien

        Bitmap  m_bmWorkSpace, 
                m_bmTemp, //dung de demo cac shape
                m_bmMarquee, // dung de xac dinh vung Marquee
                m_bmSave; // bimap save kich co ban dau cua workspace (sau khi resize)

        Keys m_keys;
        Rectangle m_rectMarquee; 

        Pen m_leftPen, m_rightPen;
        Point m_mouseLocation, m_mouseFixedLocation;
        Point[] m_points;
        List<Point> m_listMouseLocation, //danh sach cac vi tri cua mouse (torng mouse move)
                    m_listPointFill, //danh sach cac vi tri fill 
                    m_listCallBackPoint; //danh sach cac vi tri can xet lai trong fill
        Graphics m_gpTemp;
        MouseButtons m_mouseButton;
        int m_grid = 15; //gioi han size khi re chuot vao resize workspace
        int[,] m_arrayCheckFill; // kiem tra nhung vi tri da fill

        string m_saveFile; //duong dan file duoc mo hay file da luu
        int m_fileFilterIndex; // index filter cua open dialog

        Stack<Bitmap> m_undo;
        Stack<Bitmap> m_redo;
        //Stack<Bitmap> m_copy;

        public Form1()
        {
            InitializeComponent();

            saveFileDialog1.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.JPEG)|*.jpg|GIF(*.Gif)|*.Gif|PNG(*.PNG)|*.PNG";
            saveFileDialog1.FileName = "Untitle";
            openFileDialog1.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.JPEG)|*.jpg|GIF(*.gif)|*.gif|PNG(*.PNG)|*.PNG";
            openFileDialog1.FileName = "Untitle";

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.AutoScroll = true;
            m_cbShapes.SelectedIndex = 0;

            m_workSpace.Paint += m_workSpace_Paint;
            m_workSpace.MouseMove += m_workSpace_MouseMove;
            m_workSpace.MouseDown += m_workSpace_MouseDown;
            m_workSpace.MouseUp += m_workSpace_MouseUp;
            m_workSpace.MouseLeave += m_workSpace_MouseLeave;

            m_leftColor = m_btLeftColor.BackColor;
            m_rightColor = m_btRightColor.BackColor;

            CurrentTool = Tools.None;           

            m_bmWorkSpace = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            m_bmTemp = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            m_bmSave = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
            m_gpTemp.FillRectangle(Brushes.White, new Rectangle(0, 0, m_bmWorkSpace.Width, m_bmWorkSpace.Height));
            m_listMouseLocation = new List<Point>();
            m_listPointFill = new List<Point>();
            m_listCallBackPoint = new List<Point>();
            m_points = new Point[1];

            this.KeyPreview = true;
            this.KeyDown += (o, e) =>
                {
                    m_isKeyDown = true;
                    m_keys = e.KeyCode;
                };

            ActiveTool();
            PensChanged();
            m_isEdit = false;
            lblSaveStt.Text = "already";
            m_firstSave = true;
            //MessageBox.Show(m_isEdit +"");

            m_undo = new Stack<Bitmap> { };
            m_redo = new Stack<Bitmap> { };
            //m_copy = new Stack<Bitmap> { };

            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
        }

        void PensChanged()
        {
            try
            {
                m_rightPen = new Pen(new SolidBrush(m_rightColor), int.Parse(m_tbSize.Text));
                m_leftPen = new Pen(new SolidBrush(m_leftColor), int.Parse(m_tbSize.Text));
                this.Invalidate();
            }
            catch
            {

            }
        }

        void ButtonDefaulChosen()
        {
            m_btBrush.IsChosen = false;
            m_btColorPicker.IsChosen = false;
            m_btEraser.IsChosen = false;
            m_btFill.IsChosen = false;
            m_btLeftColor.IsChosen = false;
            m_btMarquee.IsChosen = false;
            m_btRightColor.IsChosen = false;
            m_btShape.IsChosen = false;
            m_btText.IsChosen = false;
            m_btZoom.IsChosen = false;
        }

        void Zoom()
        {
            try
            {
                if (float.Parse(m_tbZoomValue.Text) < 100)
                {
                    m_tbZoomValue.Text = "100";
                    m_tbZoom.Value = 0;
                }
                float val = (float.Parse(m_tbZoomValue.Text)) / 100;
                m_lbZoom.Text = val.ToString();
                m_bmWorkSpace = new Bitmap(m_bmSave, (int)(m_bmSave.Width * val), (int)(m_bmSave.Height * val));
                m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
                m_workSpace.Size = m_bmWorkSpace.Size;
                m_workSpace.Invalidate();
            }
            catch
            {
                //MessageBox.Show("Có lỗi, xin kiểm tra lại thông số");
            }
        }

        //cho phep an/hien cac option can thiet
        void ActiveTool()
        {
            m_btColorPickerShow.Visible = false;
            m_lbColorPicker.Visible = false;
            m_lbShapes.Visible = false;
            m_cbShapes.Visible = false;
            m_btFont.Visible = false;
            m_tbSides.Visible = false;
            m_lbSize.Visible = false;
            m_tbSize.Visible = false;
            m_lbSides.Visible = false;
            m_lbZoom.Visible = false;
            m_tbZoom.Visible = false;
            m_tbZoomValue.Visible = false;

            if (CurrentTool != Tools.Zoom)
                m_isFistTimeZoom = true;

            if (CurrentTool != Tools.Marquee)
            {
                if (m_isMarqueeFinish)
                {
                    m_gpTemp.DrawImage(m_bmMarquee, m_rectMarquee);

                    m_rectMarquee = Rectangle.Empty;
                    m_bmMarquee = null;

                    m_isMarqueeFinish = false;
                    m_isMarqueeChosing = false;
                    m_isMarqueeChosen = false;
                    m_isMouseUpMarqueeShow = false;
                    m_workSpace.Invalidate();
                }
            }

            switch (CurrentTool)
            {
                case Tools.Brush:
                    m_lbSize.Visible = true;
                    m_tbSize.Visible = true;
                    break;

                case Tools.Eraser:
                    m_lbSize.Visible = true;
                    m_tbSize.Visible = true;
                    break;

                case Tools.Marquee:
                    break;

                case Tools.Zoom:
                    m_lbZoom.Visible = true;
                    m_tbZoom.Visible = true;
                    m_tbZoomValue.Visible = true;

                    m_lbZoom.Location = m_lbSize.Location;
                    m_tbZoomValue.Location = new Point(m_lbZoom.Location.X + m_lbZoom.Width, m_tbZoomValue.Location.Y);
                    m_tbZoom.Location = new Point(m_tbZoomValue.Location.X + m_tbZoomValue.Width, m_tbZoom.Location.Y);

                    if (m_isFistTimeZoom)
                    {
                        m_isFistTimeZoom = false;
                        m_bmSave = new Bitmap(m_bmWorkSpace, m_bmSave.Size);
                    }
                    break;

                case Tools.Fill:
                    break;

                case Tools.Text:
                    m_btFont.Visible = true;
                    m_btFont.Location = m_lbSize.Location;
                    break;

                case Tools.Shape:
                    m_lbShapes.Visible = true;
                    m_cbShapes.Visible = true;
                    m_tbSides.Visible = true;
                    m_lbSides.Visible = true;

                    m_lbShapes.Location = m_lbSize.Location;
                    m_cbShapes.Location = new Point(m_lbShapes.Location.X + m_lbShapes.Width, m_cbShapes.Location.Y);
                    m_lbSides.Location = new Point(m_cbShapes.Location.X + m_cbShapes.Width, m_lbSides.Location.Y);
                    m_tbSides.Location = new Point(m_lbSides.Location.X + m_lbSides.Width, m_tbSides.Location.Y);
                    break;

                case Tools.ColorPicker:
                    m_btColorPickerShow.Visible = true;
                    m_lbColorPicker.Visible = true;

                    m_lbColorPicker.Location = m_lbSize.Location;
                    m_btColorPickerShow.Location = new Point(m_lbColorPicker.Location.X + m_lbColorPicker.Width, m_btColorPickerShow.Location.Y);
                    break;

                default:
                    break;
            }
        }

        void ChangeTextBoxSize()
        {
            int padding = 5;
            int numLines = m_tbTextTool.GetLineFromCharIndex(m_tbTextTool.TextLength) + 1;
            int border = m_tbTextTool.Height - m_tbTextTool.ClientRectangle.Height;


            m_tbTextTool.Height = m_tbTextTool.Font.Height * numLines + padding + border;

            if (m_tbTextTool.Font.Style != FontStyle.Bold && m_tbTextTool.Font.Style != FontStyle.Regular)
                m_tbTextTool.Width = TextRenderer.MeasureText(m_tbTextTool.Text, m_tbTextTool.Font).Width + padding + border + (int)m_tbTextTool.Font.Size;
            else
                m_tbTextTool.Width = TextRenderer.MeasureText(m_tbTextTool.Text, m_tbTextTool.Font).Width + padding + border;
        }

        //ham lay pixel tu bmWorkspace
        #region -FILL TOOL-
        private Color GetPixel(int x, int y, byte[] bytes, int width)
        {
            if (x * 4 + y * width * 4 + 3 > bytes.Length || x < 0 || y < 0)
                return Color.Transparent;

            Color color = Color.FromArgb(bytes[x * 4 + y * width * 4 + 3], bytes[x * 4 + y * width * 4 + 2], bytes[x * 4 + y * width * 4 + 1], bytes[x  * 4 + y * width * 4]);
            return color;
        }

        private void SetPixel(int x, int y, byte[] bytes, int width, Color color)
        {
            if (x * 4 + y * width * 4 + 3 > bytes.Length)
                return;
            //cu moi diem anh cach nhau 4 don vi
            bytes[x * 4 + y * width * 4] = color.B;
            bytes[x * 4 + y * width * 4 + 1] = color.G;
            bytes[x * 4 + y * width * 4 + 2] = color.R;
            bytes[x * 4 + y * width * 4 + 3] = color.A;
            
            /* test
            bytes[x * 4 + y * width * 4] = 77; // color.A;
            bytes[x * 4 + y * width * 4 + 1] = 77; // color.R;
            bytes[x * 4 + y * width * 4 + 2] = 77; // color.G;
            bytes[x * 4 + y * width * 4 + 3] = 77; // color.B;
             */
        }

        private bool ColorEquals(Color color1, Color color2)
        {
            //MessageBox.Show("Color A:" + color1.ToString() + "\nColor B: " + color2.ToString());

            if (color1.A == color2.A && color1.B == color2.B && color1.R == color2.R && color1.G == color2.G)
                return true;

            return false;
        }

        //kiem tra mau xung quanh cua pixel co trung voi color cho truoc khong roi nhet vao list
        private void CheckAroundPixel(int x, int y, byte[] bytes, Color color, int width, List<Point> list)
        {
            //kiem tra diem ben trai
            if (ColorEquals(GetPixel(x - 1, y, bytes, width), color))
            {
                list.Add(new Point(x - 1, y));
            }

            //kiem tra diem ben phai
            if (ColorEquals(GetPixel(x + 1, y, bytes, width), color))
            {
                list.Add(new Point(x + 1, y));
            }

            //kiem tra diem ben tren
            if (ColorEquals(GetPixel(x, y - 1, bytes, width), color))
            {
                list.Add(new Point(x, y - 1));
            }

            //kiem tra diem ben duoi
            if (ColorEquals(GetPixel(x, y + 1, bytes, width), color))
            {
                list.Add(new Point(x, y + 1));
            }
        }

        private void FillColor(int x, int y, byte[] bytes, Color color, int width, int height, Color colorFill)
        {
            FloodFill(x, y, bytes, width, height, colorFill);
        }

        private void FloodFill(int x, int y, byte[] bytes, int width, int height, Color colorFill)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(x, y));

            while (points.Count > 0)
            {
                Point newPos = points[points.Count - 1];
                int px = newPos.X, py = newPos.Y;
                points.RemoveAt(points.Count - 1);

                Console.WriteLine(points.Count.ToString());

                bool reachLeft = false, reachRight = false;

                while (ColorEquals(Color.White, GetPixel(px, py, bytes, width)) && py > 0)
                {
                    //Console.WriteLine("s");
                    py--;
                }

                py++;

                while (ColorEquals(Color.White, GetPixel(px, py, bytes, width)) && py < height)
                {
                    SetPixel(px, py, bytes, width, colorFill);

                    if (px > 0)
                    {
                        if (ColorEquals(Color.White, GetPixel(px - 1, py, bytes, width)))
                        {
                            if (!reachLeft)
                            {
                                reachLeft = true;
                                points.Add(new Point(px - 1, py));
                            }
                        }
                        else if (reachLeft)
                            reachLeft = false;
                    }

                    if (px < width)
                    {
                        if (ColorEquals(Color.White, GetPixel(px + 1, py, bytes, width)))
                        {
                            if (!reachRight)
                            {
                                reachRight = true;
                                points.Add(new Point(px + 1, py));
                            }
                        }
                        else if (reachRight)
                            reachRight = false;
                    }

                    py++;
                }
            }
        }

        #endregion

        #region -BUTTONS CLICK-
        private void m_btMarquee_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Marquee;
            ButtonDefaulChosen();
            m_btMarquee.IsChosen = true;
            ActiveTool();
        }

        private void m_btEraser_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Eraser;
            ButtonDefaulChosen();
            m_btEraser.IsChosen = true;
            ActiveTool();
        }

        private void m_btFill_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Fill;
            ButtonDefaulChosen();
            m_btFill.IsChosen = true;
            ActiveTool();
        }

        private void m_btBrush_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Brush;
            ButtonDefaulChosen();
            m_btBrush.IsChosen = true;

            try
            {
                m_leftPen = new Pen(new SolidBrush(m_leftColor), int.Parse(m_tbSize.Text));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }

            ActiveTool();
            this.Invalidate();
        }

        private void m_btText_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Text;
            ButtonDefaulChosen();
            m_btText.IsChosen = true;
            ActiveTool();
        }

        private void m_btColorPicker_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.ColorPicker;            
            ButtonDefaulChosen();
            m_btColorPicker.IsChosen = true;
            ActiveTool();
        }

        private void m_btShape_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Shape;
            ButtonDefaulChosen();
            m_btShape.IsChosen = true;
            ActiveTool();
        }

        private void m_btZoom_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Zoom;
            ButtonDefaulChosen();
            m_btZoom.IsChosen = true;
            ActiveTool();
        }

        private void m_btLeftColor_Click(object sender, EventArgs e)
        {
            //CurrentTool = Tools.LeftColor;

            if(colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_btLeftColor.BackColor = colorDialog1.Color;
                m_leftColor = colorDialog1.Color;
                ActiveTool();
                PensChanged();
            }
        }

        private void m_btRightColor_Click(object sender, EventArgs e)
        {
            //CurrentTool = Tools.RightColor;

            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_btRightColor.BackColor = colorDialog1.Color;
                m_rightColor = colorDialog1.Color;
                ActiveTool();
                PensChanged();
            }            
        }

        #endregion

        #region - ANOTHER EVENTS-

        private void m_workSpace_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void m_workSpace_MouseUp(object sender, MouseEventArgs e)
        {
            m_allowErase = false;
            m_allowDrawPolygon = false;

            switch (CurrentTool)
            {
                case Tools.Brush:
                    break;

                case Tools.Eraser:
                    break;
                case Tools.Marquee:
                    if (m_isMarqueeFinish)
                    {
                        Point realPoint = new Point(m_rectMarquee.Location.X + (m_mouseLocation.X - m_mouseFixedLocation.X),
                                                            m_rectMarquee.Location.Y + (m_mouseLocation.Y - m_mouseFixedLocation.Y));

                        m_rectMarquee = new Rectangle(realPoint, m_rectMarquee.Size);
                        m_mouseLocation = m_mouseFixedLocation = e.Location;
                    }

                    if (m_isMarqueeChosing)
                    {
                        if (m_mouseLocation.X > m_mouseFixedLocation.X && m_mouseLocation.Y >= m_mouseFixedLocation.Y)
                        {
                            m_rectMarquee = new Rectangle(m_mouseFixedLocation,
                                                new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                            Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y)));
                        }
                        else if (m_mouseLocation.X > m_mouseFixedLocation.X && m_mouseLocation.Y <= m_mouseFixedLocation.Y)
                        {
                            m_rectMarquee = new Rectangle(new Point(m_mouseFixedLocation.X, m_mouseLocation.Y),
                                                 new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                             Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y)));
                        }
                        else if (m_mouseLocation.X < m_mouseFixedLocation.X && m_mouseLocation.Y >= m_mouseFixedLocation.Y)
                        {
                            m_rectMarquee = new Rectangle(new Point(m_mouseLocation.X, m_mouseFixedLocation.Y),
                                                new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                            Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y)));
                        }
                        else
                        {
                            m_rectMarquee = new Rectangle(new Point(m_mouseLocation.X, m_mouseLocation.Y),
                                                new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                            Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y)));
                        }

                        m_isMouseUpMarqueeShow = true;
                        m_isMarqueeChosing = false;
                        m_isMarqueeFinish = true;

                        if (m_rectMarquee.Width != 0 && m_rectMarquee.Height != 0)
                        {
                            m_bmMarquee = m_bmWorkSpace.Clone(m_rectMarquee, m_bmWorkSpace.PixelFormat);
                            m_gpTemp.FillRectangle(Brushes.White, m_rectMarquee);
                        }
                    }

                    break;

                case Tools.Shape:
                    #region -SHAPE-
                    try
                    {
                        switch (m_cbShapes.SelectedItem.ToString())
                        {
                            case "Line":
                                m_gpTemp.DrawImage(m_bmTemp, new Point(0, 0));
                                break;

                            case "Polygon":
                                m_gpTemp.DrawPolygon(m_leftPen, m_points);
                                break;

                            case "Ellipse":
                                m_gpTemp.DrawImage(m_bmTemp, new Point(0, 0));
                                break;

                            default:
                                break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Bạn chưa chọn loại hình vẽ");
                    }
                    #endregion
                    break;

                default:
                    break;

            }

            if (m_allowResieWorkSpace)
            {
                //resize
                m_isFistTimeZoom = true; // sau khi resize cho = true de luu lai bitmap
                int width = m_workSpace.Width > m_bmWorkSpace.Width ? m_bmWorkSpace.Width : m_workSpace.Width;
                int heigth = m_workSpace.Height > m_bmWorkSpace.Height ? m_bmWorkSpace.Height : m_workSpace.Height;

                Bitmap bmtemp = m_bmWorkSpace.Clone(new Rectangle(0, 0, width, heigth), m_bmWorkSpace.PixelFormat);
                m_gpTemp.Clear(Color.White);
                m_bmWorkSpace = new Bitmap(m_workSpace.Width, m_workSpace.Height);
                m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
                m_gpTemp.DrawImage(bmtemp, new Point(0, 0));
                m_bmSave = new Bitmap(m_bmWorkSpace);
            }


            m_listMouseLocation.Clear();
            m_mouseLocation = e.Location;
            m_isMouseDown = false;
            m_isMouseUp = true;
            m_allowResieWorkSpace = false;
            m_workSpace.Invalidate();
            this.Cursor = Cursors.Default;

        }

        private void m_workSpace_MouseDown(object sender, MouseEventArgs e)//
        {
            m_isMouseUp = false;
            m_isMouseDown = true;

            m_mouseButton = e.Button;
            m_mouseLocation = e.Location;
            m_mouseFixedLocation = e.Location;

            if (m_rectMarquee.Contains(e.Location))
            {
                m_isMarqueeChosen = true;
                m_isMouseUpMarqueeShow = false;
            }
            else
            {
                if (m_isMarqueeFinish)
                {
                    #region undo,redo
                    m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                    if (m_undo.Count != 0)
                        undoToolStripMenuItem.Enabled = true;
                    if (m_redo.Count != 0)
                    {
                        for (int i = 0; i < m_redo.Count; i++)
                            m_redo.Pop();
                        redoToolStripMenuItem.Enabled = false;
                    }
                    #endregion

                    m_gpTemp.DrawImage(m_bmMarquee, m_rectMarquee);

                    m_rectMarquee = Rectangle.Empty;
                    m_bmMarquee = null;
                }
                else
                    m_isMarqueeChosing = true;

                m_isMouseUpMarqueeShow = false;
                m_isMarqueeChosen = false;
                m_isMarqueeFinish = false;
            }

            if (e.Location.X >= m_workSpace.Width - m_grid && e.Location.Y >= m_workSpace.Height - m_grid)
            {
                m_allowResieWorkSpace = true;
            }

            switch (CurrentTool)
            {
                case Tools.Eraser:
                    m_allowErase = true;
                    #region undo,redo
                    m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                    if (m_undo.Count != 0)
                        undoToolStripMenuItem.Enabled = true;
                    if (m_redo.Count != 0)
                    {
                        for (int i = 0; i < m_redo.Count; i++)
                            m_redo.Pop();
                        redoToolStripMenuItem.Enabled = false;
                    }
                    #endregion
                    break;

                case Tools.Marquee:
                    break;

                case Tools.Brush:
                    #region undo,redo
                    m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                    if (m_undo.Count != 0)
                        undoToolStripMenuItem.Enabled = true;
                    if (m_redo.Count != 0)
                    {
                        for (int i = 0; i < m_redo.Count; i++)
                            m_redo.Pop();
                        redoToolStripMenuItem.Enabled = false;
                    }
                    #endregion
                    break;

                case Tools.Shape:
                    #region undo,redo
                    m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                    if (m_undo.Count != 0)
                        undoToolStripMenuItem.Enabled = true;
                    if (m_redo.Count != 0)
                    {
                        for (int i = 0; i < m_redo.Count; i++)
                            m_redo.Pop();
                        redoToolStripMenuItem.Enabled = false;
                    }
                    #endregion
                    break;

                case Tools.Fill:
                    #region undo,redo
                    m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                    if (m_undo.Count != 0)
                        undoToolStripMenuItem.Enabled = true;
                    if (m_redo.Count != 0)
                    {
                        for (int i = 0; i < m_redo.Count; i++)
                            m_redo.Pop();
                        redoToolStripMenuItem.Enabled = false;
                    }
                    #endregion

                    if (CurrentTool == Tools.Fill)
                    {
                        BitmapData bmData = m_bmWorkSpace.LockBits(new Rectangle(0,0,m_bmWorkSpace.Width, m_bmWorkSpace.Height), ImageLockMode.ReadWrite, m_bmWorkSpace.PixelFormat);
                        IntPtr intptr = bmData.Scan0;
                        int length = m_bmWorkSpace.Height * Math.Abs(bmData.Stride);
                        byte[] bytes = new byte[length];
                        System.Runtime.InteropServices.Marshal.Copy(intptr, bytes, 0, length);

                        m_arrayCheckFill = new int[m_bmWorkSpace.Width, m_bmWorkSpace.Height];

                        FillColor(e.Location.X, e.Location.Y, bytes,
                                    GetPixel(e.Location.X, e.Location.Y, bytes, bmData.Stride / 4),
                                    m_bmWorkSpace.Width, m_bmWorkSpace.Height, m_leftColor);

                        /*
                        bytes[i] = 0; // B
                        bytes[i + 1] = 0; // G
                        bytes[i + 2] = 255; // R
                        bytes[i + 3] = 255; // A
                         * 
                        //for (int i = 0; i < m_bmWorkSpace.Height; i++)
                        //{
                        //    for (int j = 0; j < m_bmWorkSpace.Width * 4; j += 4)
                        //    { 
                        //        bytes[j + i * m_bmWorkSpace.Width * 4] = 255;
                        //        bytes[j + i * m_bmWorkSpace.Width * 4 + 1] = 0;
                        //        bytes[j + i * m_bmWorkSpace.Width * 4 + 2] = 0;
                        //        bytes[j + i * m_bmWorkSpace.Width * 4 + 3] = 255;
                        //    }
                        //}
                        */

                        System.Runtime.InteropServices.Marshal.Copy(bytes, 0, intptr, length);

                        m_bmWorkSpace.UnlockBits(bmData);
                        m_workSpace.Invalidate();
                        m_isEdit = true;
                        lblSaveStt.Text = "not yet";
                        //MessageBox.Show(m_isEdit + "" + "\nMouse down");
                    }
                    break;
                default:
                    break;
            }

            m_workSpace.Invalidate();
        }                                                                                                              

        private void m_workSpace_MouseMove(object sender, MouseEventArgs e)
        {
            m_mouseLocation = e.Location;

            //re vao  vung cho phep resize workspace
            if (e.Location.X >= m_workSpace.Width - m_grid && e.Location.Y >= m_workSpace.Height - m_grid)
                this.Cursor = Cursors.SizeNWSE;
            else
                this.Cursor = Cursors.Default;

            #region -resize workspace-
            if (m_allowResieWorkSpace)
            {
                if (e.Location.X - m_workSpace.Location.X > 0 && e.Location.Y - m_workSpace.Location.Y > 0)
                {
                    m_workSpace.Size = new Size(e.Location.X, e.Location.Y);
                    //m_bmWorkSpace = new Bitmap(m_bmWorkSpace, m_workSpace.Size);
                }

                //label1.Text = e.Location.ToString();
                this.Invalidate();
            }
            #endregion

            else if (m_isMouseDown)
            {
                //label1.Text = m_listMouseLocation.Count.ToString();
                m_listMouseLocation.Add(e.Location);
                m_workSpace.Invalidate();
            }


            switch (CurrentTool)
            {
                case Tools.Brush:
                    break;

                case Tools.Eraser:
                    m_workSpace.Invalidate();
                    break;

                case Tools.Marquee:

                    break;

                case Tools.Zoom:
                    break;
                case Tools.Fill:
                    break;
                case Tools.Text:
                    break;
                case Tools.Shape:
                    if (m_isMouseDown)
                        m_allowDrawPolygon = true;
                    break;

                default:
                    break;
            }
        }

        private void m_workSpace_MouseClick(object sender, MouseEventArgs e)
        {
            switch (CurrentTool)
            {
                case Tools.Brush:
                    break;

                case Tools.Eraser:

                    break;

                case Tools.Marquee:
                    break;
                case Tools.Zoom:
                    break;
                case Tools.Fill:
                    break;

                case Tools.Text:

                    #region -Text-
                    if (!m_textClicked)
                    {
                        m_textClicked = true;
                        m_tbTextTool.Location = new Point(e.Location.X + m_workSpace.Location.X, e.Location.Y + m_workSpace.Location.Y);
                        m_tbTextTool.Visible = true;
                        m_allowDrawText = false;
                        m_tbTextTool.Focus();
                    }
                    else
                    {
                        #region undo,redo
                        m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                        if (m_undo.Count != 0)
                            undoToolStripMenuItem.Enabled = true;
                        if (m_redo.Count != 0)
                        {
                            for (int i = 0; i < m_redo.Count; i++)
                                m_redo.Pop();
                            redoToolStripMenuItem.Enabled = false;
                        }
                        #endregion
                        m_textClicked = false;
                        m_tbTextTool.Visible = false;
                        m_allowDrawText = true;
                        this.Invalidate();
                    }
                    m_isEdit = true;
                    lblSaveStt.Text = "not yet";
                    //MessageBox.Show(m_isEdit + "\nMouseclick");
                    #endregion

                    break;

                case Tools.Shape:
                    break;

                case Tools.ColorPicker:
                    m_btColorPickerShow.BackColor = m_bmWorkSpace.GetPixel(e.X, e.Y);
                    m_btLeftColor.BackColor = m_btColorPickerShow.BackColor;
                    PensChanged();
                    this.Invalidate();
                    break;

                case Tools.None:
                    break;

                default:
                    break;
            }
        }

        private void m_workSpace_Paint(object sender, PaintEventArgs e)
        {
            //MessageBox.Show("ASdasda");
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            m_gpTemp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Graphics gp = Graphics.FromImage(m_bmTemp);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gp.Clear(Color.Transparent);

            int size = int.Parse(m_tbSize.Text);

            switch (CurrentTool)
            {
                case Tools.Brush:
                    #region -BRUSH-

                    if (m_isMouseDown && m_listMouseLocation.Count >= 2)
                    {
                        if (m_mouseButton == System.Windows.Forms.MouseButtons.Left)
                            m_gpTemp.DrawLines(m_leftPen, m_listMouseLocation.ToArray());
                        else
                            m_gpTemp.DrawLines(m_rightPen, m_listMouseLocation.ToArray());
                    }

                    if (m_isMouseUp && m_allowResieWorkSpace)
                    {
                        for (int i = 0; i < m_leftPen.Width; i++)
                        {
                            for (int j = 0; j < m_leftPen.Width; j++)
                            {
                                if (m_mouseButton == System.Windows.Forms.MouseButtons.Left)
                                {
                                    try
                                    {
                                        if (m_mouseLocation.X - (int)m_leftPen.Width / 2 + i >= 0)
                                            m_bmWorkSpace.SetPixel(m_mouseLocation.X - (int)m_leftPen.Width / 2 + i,
                                                                        m_mouseLocation.Y - (int)m_leftPen.Width / 2 + j,
                                                                            m_leftColor);
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        if (m_mouseLocation.X - (int)m_rightPen.Width / 2 + i >= 0)
                                            m_bmWorkSpace.SetPixel(m_mouseLocation.X - (int)m_rightPen.Width / 2 + i,
                                                                        m_mouseLocation.Y - (int)m_rightPen.Width / 2 + j,
                                                                            m_rightColor);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }

                    #endregion
                    break;

                case Tools.Eraser:
                    #region -ERASER-
                    if (m_allowErase)
                    {
                        m_gpTemp.FillRectangle(new SolidBrush(Color.White),
                                                    new Rectangle(m_mouseLocation.X - size / 2, m_mouseLocation.Y - size / 2, size, size));
                    }

                    gp.FillRectangle(Brushes.White, new Rectangle(m_mouseLocation.X - size / 2, m_mouseLocation.Y - size / 2, size, size));
                    gp.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(m_mouseLocation.X - size / 2, m_mouseLocation.Y - size / 2, size, size));
                    #endregion
                    break;

                case Tools.Marquee:
                    #region -MARQUEE-
                    Pen pen = new Pen(Brushes.Black, 1);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    if (m_isMarqueeChosing)
                    {
                        Point[] points = new Point[]
                        {
                            m_mouseFixedLocation,
                            new Point(m_mouseFixedLocation.X, m_mouseLocation.Y),
                            m_mouseLocation,
                            new Point(m_mouseLocation.X, m_mouseFixedLocation.Y)
                        };

                        gp.DrawPolygon(pen, points);
                    }

                    if (m_isMouseUpMarqueeShow)
                    {
                        gp.DrawImage(m_bmMarquee, m_rectMarquee.Location);
                        gp.DrawRectangle(pen, m_rectMarquee);
                    }
                    else
                    {
                        if (m_isMarqueeChosen)
                        {
                            Point realPoint = new Point(m_rectMarquee.Location.X + (m_mouseLocation.X - m_mouseFixedLocation.X),
                                                            m_rectMarquee.Location.Y + (m_mouseLocation.Y - m_mouseFixedLocation.Y));
                            gp.DrawImage(m_bmMarquee, realPoint);
                            gp.DrawRectangle(pen, new Rectangle(new Point(realPoint.X, realPoint.Y), m_rectMarquee.Size));
                        }
                    }

                    #endregion
                    break;

                case Tools.Zoom:
                    #region -ZOOM-
                    #endregion
                    break;

                case Tools.Fill:
                    #region -FILL-
                    #endregion
                    break;

                case Tools.Text:
                    #region -TEXT-
                    if (m_allowDrawText)
                    {
                        m_gpTemp.DrawString(m_tbTextTool.Text, m_tbTextTool.Font, new SolidBrush(m_leftColor),
                                                new PointF(m_tbTextTool.Location.X - m_workSpace.Location.X, m_tbTextTool.Location.Y - m_workSpace.Location.Y));
                        m_tbTextTool.Text = "";
                    }
                    #endregion
                    break;

                case Tools.Shape:
                    #region -SHAPE-
                    try
                    {
                        switch (m_cbShapes.SelectedItem.ToString())
                        {
                            case "Line":
                                #region -LINE-
                                gp.DrawLine(m_leftPen, m_mouseFixedLocation, m_mouseLocation);
                                #endregion
                                break;

                            case "Polygon":
                                #region -POLYGONS-
                                if (m_allowDrawPolygon)
                                {
                                    switch (m_tbSides.Text)
                                    {
                                        case "3":
                                            #region -3 SIDES-
                                            m_points = new Point[] 
                                                { 
                                                    new Point((m_mouseFixedLocation.X + ((m_mouseLocation.X - m_mouseFixedLocation.X)) / 2), m_mouseFixedLocation.Y),
                                                    new Point(m_mouseFixedLocation.X, m_mouseLocation.Y), 
                                                    new Point(m_mouseLocation.X, m_mouseLocation.Y)    
                                                };

                                            gp.DrawPolygon(m_leftPen, m_points);
                                            #endregion
                                            break;

                                        case "4":
                                            #region -4 SIDES-
                                            m_points = new Point[] 
                                                { 
                                                    m_mouseFixedLocation,
                                                    new Point(m_mouseLocation.X, m_mouseFixedLocation.Y), 
                                                    m_mouseLocation,
                                                    new Point(m_mouseFixedLocation.X, m_mouseLocation.Y)    
                                                };

                                            gp.DrawPolygon(m_leftPen, m_points);
                                            #endregion
                                            break;

                                        case "5":
                                            #region -5 SIDES-
                                            m_points = new Point[]
                                            {
                                                new Point(m_mouseFixedLocation.X + (m_mouseLocation.X - m_mouseFixedLocation.X) /                                                               2, m_mouseFixedLocation.Y),
                                                new Point(m_mouseFixedLocation.X, m_mouseFixedLocation.Y + (m_mouseLocation.Y -                                                                 m_mouseFixedLocation.Y) / 2),
                                                new Point(m_mouseFixedLocation.X + (m_mouseLocation.X - m_mouseFixedLocation.X) /                                                               3 , m_mouseLocation.Y),
                                                new Point(m_mouseFixedLocation.X + (m_mouseLocation.X - m_mouseFixedLocation.X) *                                                           2 / 3 , m_mouseLocation.Y),
                                                new Point(m_mouseLocation.X, m_mouseFixedLocation.Y + (m_mouseLocation.Y -                                                                  m_mouseFixedLocation.Y) / 2)
                                            };

                                            gp.DrawPolygon(m_leftPen, m_points);
                                            #endregion
                                            break;

                                        case "6":
                                            #region
                                            m_points = new Point[]
                                            {
                                                new Point(m_mouseFixedLocation.X + (m_mouseLocation.X - m_mouseFixedLocation.X) / 2, m_mouseFixedLocation.Y),
                                                new Point(m_mouseFixedLocation.X, m_mouseFixedLocation.Y + (m_mouseLocation.Y - m_mouseFixedLocation.Y) / 3),
                                                new Point(m_mouseFixedLocation.X, m_mouseFixedLocation.Y + (m_mouseLocation.Y - m_mouseFixedLocation.Y) * 2 / 3),
                                                new Point(m_mouseFixedLocation.X + (m_mouseLocation.X - m_mouseFixedLocation.X) / 2, m_mouseLocation.Y),                                                
                                                new Point(m_mouseLocation.X, m_mouseFixedLocation.Y + (m_mouseLocation.Y - m_mouseFixedLocation.Y) * 2 / 3),
                                                new Point(m_mouseLocation.X, m_mouseFixedLocation.Y + (m_mouseLocation.Y - m_mouseFixedLocation.Y) / 3)
                                            };

                                            gp.DrawPolygon(m_leftPen, m_points);
                                            #endregion
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                #endregion
                                break;

                            case "Ellipse":
                                #region -ECLIPSE-
                                if (m_mouseLocation.X > m_mouseFixedLocation.X && m_mouseLocation.Y >= m_mouseFixedLocation.Y)
                                {
                                    gp.DrawEllipse(m_leftPen, new Rectangle(m_mouseFixedLocation,
                                                        new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                                    Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y))));
                                }
                                else if (m_mouseLocation.X > m_mouseFixedLocation.X && m_mouseLocation.Y <= m_mouseFixedLocation.Y)
                                {
                                    gp.DrawEllipse(m_leftPen, new Rectangle(new Point(m_mouseFixedLocation.X, m_mouseLocation.Y),
                                                        new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                                    Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y))));
                                }
                                else if (m_mouseLocation.X < m_mouseFixedLocation.X && m_mouseLocation.Y >= m_mouseFixedLocation.Y)
                                {
                                    gp.DrawEllipse(m_leftPen, new Rectangle(new Point(m_mouseLocation.X, m_mouseFixedLocation.Y),
                                                        new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                                    Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y))));
                                }
                                else
                                {
                                    gp.DrawEllipse(m_leftPen, new Rectangle(new Point(m_mouseLocation.X, m_mouseLocation.Y),
                                                        new Size(Math.Abs(m_mouseLocation.X - m_mouseFixedLocation.X),
                                                                    Math.Abs(m_mouseLocation.Y - m_mouseFixedLocation.Y))));
                                }
                                #endregion
                                break;

                            default:
                                break;
                        };
                    }
                    catch
                    {

                    }

                    #endregion
                    break;

                default:
                    break;
            }

            e.Graphics.DrawImage(m_bmWorkSpace, new Point(0, 0));
            m_isEdit = true;
            lblSaveStt.Text = "not yet";
            //MessageBox.Show(m_isEdit + "\nPaint");

            if (((CurrentTool == Tools.Shape && m_allowDrawPolygon) || CurrentTool == Tools.Eraser || CurrentTool == Tools.Marquee))
                e.Graphics.DrawImage(m_bmTemp, new Point(0, 0));
        }

        private void m_tbSize_TextChanged(object sender, EventArgs e)
        {
            if(m_tbSize.Text != "")
                PensChanged();
        }

        private void m_tbSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void m_cbShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_firstSave)
            {
                saveImageAsToolStripMenuItem_Click(sender, e);
                m_firstSave = false;
                m_isEdit = false;
                lblSaveStt.Text = "already";
            }
            else
            {
                switch (m_fileFilterIndex)
                {
                    case 1:
                        m_bmWorkSpace.Save(m_saveFile, ImageFormat.Bmp);
                        break;

                    case 2:
                        m_bmWorkSpace.Save(m_saveFile, ImageFormat.Jpeg);
                        break;

                    case 3:
                        m_bmWorkSpace.Save(m_saveFile, ImageFormat.Gif);
                        break;

                    case 4:
                    default:
                        m_bmWorkSpace.Save(m_saveFile, ImageFormat.Png);
                        break;
                }

                m_isEdit = false;
                lblSaveStt.Text = "already";
            }
        }

        private void m_tbTextTool_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChangeTextBoxSize();
        }

        private void m_btFont_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_tbTextTool.Font = fontDialog1.Font;
                ChangeTextBoxSize();
            }
        }

        private void m_btColorPickerShow_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = m_btColorPickerShow.BackColor;
            colorDialog1.ShowDialog();
        }

        private void m_tbZoom_Scroll(object sender, EventArgs e)
        {
            m_tbZoomValue.Text = (m_tbZoom.Value + 100).ToString();
            Zoom();
        }

        private void m_tbZoomValue_TextChanged(object sender, EventArgs e)
        {
            Zoom();
        }

        private void m_workSpace_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_isEdit)
            {
                System.Windows.Forms.DialogResult tmp = MessageBox.Show("Do you want to save it before quit?", "", MessageBoxButtons.YesNoCancel);
                if (tmp == System.Windows.Forms.DialogResult.Yes)
                {
                    saveImageToolStripMenuItem_Click(sender, e);
                    this.Close();
                }
                else if (tmp == System.Windows.Forms.DialogResult.No)
                {
                    this.Close();
                }
                else
                {

                }
            }
            else
                this.Close();
        }

        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmSave = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            Graphics gp = Graphics.FromImage(bmSave);
            gp.DrawImage(m_bmWorkSpace, new Rectangle(0, 0, bmSave.Width, bmSave.Height), new Rectangle(0, 0, bmSave.Width, bmSave.Height), GraphicsUnit.Pixel);
            //gp.DrawImage(m_bmWorkSpace, 0, 0);
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(saveFileDialog1.FileName);
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bmSave.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                        break;

                    case 2:
                        bmSave.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                        break;

                    case 3:
                        bmSave.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                        break;

                    case 4:
                    default:
                        bmSave.Save(saveFileDialog1.FileName, ImageFormat.Png);
                        break;
                }
            }
            m_saveFile = saveFileDialog1.FileName;
            m_fileFilterIndex = saveFileDialog1.FilterIndex;
            m_isEdit = false;
            lblSaveStt.Text = "already";
            m_firstSave = false;
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_isEdit)
            {
                System.Windows.Forms.DialogResult tmp = MessageBox.Show("Do you want to save it before open file?", "", MessageBoxButtons.YesNoCancel);
                if (tmp == System.Windows.Forms.DialogResult.Yes)
                {
                    saveImageToolStripMenuItem_Click(sender, e);
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_saveFile = openFileDialog1.FileName;
                        m_fileFilterIndex = openFileDialog1.FilterIndex;
                        Bitmap bmopen = new Bitmap(m_saveFile);
                        m_bmWorkSpace = new Bitmap(bmopen);
                        m_workSpace.Size = new Size(bmopen.Size.Width, bmopen.Size.Height);
                        m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
                        //Graphics gp = Graphics.FromImage(m_bmWorkSpace);
                        Graphics im = m_workSpace.CreateGraphics();
                        //gp.DrawImage(bmopen, 0, 0);
                        im.DrawImage(m_bmWorkSpace, 0, 0);
                        m_firstSave = false;
                        m_isEdit = false;
                        lblSaveStt.Text = "already";
                    }
                }
                else if (tmp == System.Windows.Forms.DialogResult.No)
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_saveFile = openFileDialog1.FileName;
                        m_fileFilterIndex = openFileDialog1.FilterIndex;
                        Bitmap bmopen = new Bitmap(m_saveFile);
                        m_bmWorkSpace = new Bitmap(bmopen);
                        m_workSpace.Size = new Size(bmopen.Size.Width, bmopen.Size.Height);
                        //Graphics gp = Graphics.FromImage(m_bmWorkSpace);
                        m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
                        Graphics im = m_workSpace.CreateGraphics();
                        //gp.DrawImage(bmopen, 0, 0);
                        im.DrawImage(m_bmWorkSpace, 0, 0);
                        m_firstSave = false;
                        m_isEdit = false;
                        lblSaveStt.Text = "already";
                    }
                }
                else
                {

                }
            }
            else
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_saveFile = openFileDialog1.FileName;
                    m_fileFilterIndex = openFileDialog1.FilterIndex;
                    Bitmap bmopen = new Bitmap(m_saveFile);
                    m_bmWorkSpace = new Bitmap(bmopen);
                    m_workSpace.Size = new Size(bmopen.Size.Width, bmopen.Size.Height);
                    m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
                    //Graphics gp = Graphics.FromImage(m_bmWorkSpace);
                    Graphics im = m_workSpace.CreateGraphics();
                    //gp.DrawImage(bmopen, 0, 0);
                    im.DrawImage(m_bmWorkSpace, 0, 0);
                    m_firstSave = false;
                    m_isEdit = false;
                    lblSaveStt.Text = "already";
                }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_isEdit)
            {
                System.Windows.Forms.DialogResult tmp = MessageBox.Show("Do you want to save it before create new?", "", MessageBoxButtons.YesNoCancel);
                if (tmp == System.Windows.Forms.DialogResult.Yes)
                {
                    saveImageToolStripMenuItem_Click(sender, e);
                    Graphics im = m_workSpace.CreateGraphics();
                    m_gpTemp.Clear(Color.White);
                    im.Clear(Color.White);
                    m_firstSave = true;
                    m_isEdit = false;
                    lblSaveStt.Text = "already";
                    m_saveFile = "";
                }
                else if (tmp == System.Windows.Forms.DialogResult.No)
                {
                    Graphics im = m_workSpace.CreateGraphics();
                    m_gpTemp.Clear(Color.White);
                    im.Clear(Color.White);
                    m_firstSave = true;
                    m_isEdit = false;
                    lblSaveStt.Text = "already";
                    m_saveFile = "";
                }
                else
                {

                }
            }
            else
                {
                    Graphics im = m_workSpace.CreateGraphics();
                    m_gpTemp.Clear(Color.White);
                    im.Clear(Color.White);
                    m_firstSave = true;
                    m_isEdit = false;
                    lblSaveStt.Text = "already";
                    m_saveFile = "";
                }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_undo.Count != 0)
            {
                //neu dang co text box
                if (m_textClicked)
                {
                    m_tbTextTool.Text = "";
                    m_textClicked = false;
                    m_tbTextTool.Visible = false;
                    m_allowDrawText = false;
                    this.Invalidate();
                }
                //neu dang cat
                if (m_isMarqueeFinish)
                {
                    m_rectMarquee = Rectangle.Empty;
                    m_bmMarquee = null;
                    m_isMouseUpMarqueeShow = false;
                    m_isMarqueeChosen = false;
                    m_isMarqueeChosing = false;
                    m_isMarqueeFinish = false;
                }

                //m_bmWorkSpace = new Bitmap(m_bmWorkSpace, m_workSpace.Size);
                m_redo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                redoToolStripMenuItem.Enabled = true;
                Graphics im = m_workSpace.CreateGraphics();
                Bitmap undo = new Bitmap(m_undo.Pop());
                //Bitmap undo = new Bitmap("C:/Users/thinh_000/Desktop/dfadfsdfsdfs.PNG");
                //m_bmWorkSpace = new Bitmap(undo, undo.Size);
                m_workSpace.Size = new Size(undo.Size.Width, undo.Size.Height);

                int width = m_workSpace.Width > m_bmWorkSpace.Width ? m_bmWorkSpace.Width : m_workSpace.Width;
                int heigth = m_workSpace.Height > m_bmWorkSpace.Height ? m_bmWorkSpace.Height : m_workSpace.Height;

                Bitmap bmtemp = m_bmWorkSpace.Clone(new Rectangle(0, 0, width, heigth), m_bmWorkSpace.PixelFormat);
                m_bmWorkSpace = new Bitmap(m_workSpace.Width, m_workSpace.Height);
                m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
                m_gpTemp.Clear(Color.White);
                //m_gpTemp.DrawImage(bmtemp, new Point(0, 0));

                m_gpTemp.DrawImage(undo, 0, 0);
                im.DrawImage(m_bmWorkSpace, 0, 0);

                if (m_undo.Count == 0)
                    undoToolStripMenuItem.Enabled = false;

                m_isEdit = true;
                lblSaveStt.Text = "not yet";
            }
            else
                undoToolStripMenuItem.Enabled = false;
            
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_redo.Count != 0)
            {
                //m_bmWorkSpace = new Bitmap(m_bmWorkSpace, m_workSpace.Size);
                m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
                undoToolStripMenuItem.Enabled = true;
                Graphics im = m_workSpace.CreateGraphics();
                Bitmap redo = new Bitmap(m_redo.Pop());
                //m_bmWorkSpace = new Bitmap(redo, redo.Size);
                m_workSpace.Size = new Size(redo.Size.Width, redo.Size.Height);

                int width = m_workSpace.Width > m_bmWorkSpace.Width ? m_bmWorkSpace.Width : m_workSpace.Width;
                int heigth = m_workSpace.Height > m_bmWorkSpace.Height ? m_bmWorkSpace.Height : m_workSpace.Height;

                Bitmap bmtemp = m_bmWorkSpace.Clone(new Rectangle(0, 0, width, heigth), m_bmWorkSpace.PixelFormat);
                m_bmWorkSpace = new Bitmap(m_workSpace.Width, m_workSpace.Height);
                m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
                m_gpTemp.Clear(Color.White);
                //m_gpTemp.DrawImage(bmtemp, new Point(0, 0));

                m_gpTemp.DrawImage(redo, 0, 0);
                im.DrawImage(m_bmWorkSpace, 0, 0);
                //this.Invalidate();
                if (m_redo.Count == 0)
                    redoToolStripMenuItem.Enabled = false;

                m_isEdit = true;
                lblSaveStt.Text = "not yet";
            }
            else
                redoToolStripMenuItem.Enabled = false;
        }

        private void m_tbZoom_MouseDown(object sender, MouseEventArgs e)
        {
            #region undo,redo
            m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
            if (m_undo.Count != 0)
                undoToolStripMenuItem.Enabled = true;
            if (m_redo.Count != 0)
            {
                for (int i = 0; i < m_redo.Count; i++)
                    m_redo.Pop();
                redoToolStripMenuItem.Enabled = false;
            }
            #endregion
        }

        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolBoxToolStripMenuItem.Checked)
                m_toolBar.Show();
            else
                m_toolBar.Hide();

        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked)
                m_statusBar.Show();
            else
                m_statusBar.Hide();
        }

        private void leftToRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region undo,redo
            m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
            if (m_undo.Count != 0)
                undoToolStripMenuItem.Enabled = true;
            if (m_redo.Count != 0)
            {
                for (int i = 0; i < m_redo.Count; i++)
                    m_redo.Pop();
                redoToolStripMenuItem.Enabled = false;
            }
            #endregion
            m_bmWorkSpace.RotateFlip(RotateFlipType.Rotate90FlipNone);
            m_workSpace.Size = new Size(m_bmWorkSpace.Size.Width, m_bmWorkSpace.Size.Height);
            Graphics im = m_workSpace.CreateGraphics();
            im.DrawImage(m_bmWorkSpace, 0, 0);
            m_isEdit = true;
            lblSaveStt.Text = "not yet";
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region undo,redo
            m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
            if (m_undo.Count != 0)
                undoToolStripMenuItem.Enabled = true;
            if (m_redo.Count != 0)
            {
                for (int i = 0; i < m_redo.Count; i++)
                    m_redo.Pop();
                redoToolStripMenuItem.Enabled = false;
            }
            #endregion
            m_bmWorkSpace.RotateFlip(RotateFlipType.Rotate90FlipXY);
            m_workSpace.Size = new Size(m_bmWorkSpace.Size.Width, m_bmWorkSpace.Size.Height);
            Graphics im = m_workSpace.CreateGraphics();
            im.DrawImage(m_bmWorkSpace, 0, 0);
            m_isEdit = true;
            lblSaveStt.Text = "not yet";
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            #region undo,redo
            m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
            if (m_undo.Count != 0)
                undoToolStripMenuItem.Enabled = true;
            if (m_redo.Count != 0)
            {
                for (int i = 0; i < m_redo.Count; i++)
                    m_redo.Pop();
                redoToolStripMenuItem.Enabled = false;
            }
            #endregion
            m_bmWorkSpace.RotateFlip(RotateFlipType.Rotate180FlipNone);
            m_workSpace.Size = new Size(m_bmWorkSpace.Size.Width, m_bmWorkSpace.Size.Height);
            Graphics im = m_workSpace.CreateGraphics();
            im.DrawImage(m_bmWorkSpace, 0, 0);
            m_isEdit = true;
            lblSaveStt.Text = "not yet";
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region undo,redo
            m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
            if (m_undo.Count != 0)
                undoToolStripMenuItem.Enabled = true;
            if (m_redo.Count != 0)
            {
                for (int i = 0; i < m_redo.Count; i++)
                    m_redo.Pop();
                redoToolStripMenuItem.Enabled = false;
            }
            #endregion
            for(int i = 0; i < m_bmWorkSpace.Width; i++)
                for (int j = 0; j < m_bmWorkSpace.Height; j++)
                {
                    Color pixelColor = m_bmWorkSpace.GetPixel(i, j);
                    int grayScale = (int)(pixelColor.R * 0.3086) + (int)(pixelColor.G * 0.6094) + (int)(pixelColor.B * 0.0820);
                    Color newColor = Color.FromArgb(grayScale,grayScale,grayScale);
                    m_bmWorkSpace.SetPixel(i, j, newColor);
                }

            Graphics im = m_workSpace.CreateGraphics();
            im.DrawImage(m_bmWorkSpace, 0, 0);
            m_isEdit = true;
            lblSaveStt.Text = "not yet";
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region undo,redo
            m_undo.Push(new Bitmap(m_bmWorkSpace, m_workSpace.Size));
            if (m_undo.Count != 0)
                undoToolStripMenuItem.Enabled = true;
            if (m_redo.Count != 0)
            {
                for (int i = 0; i < m_redo.Count; i++)
                    m_redo.Pop();
                redoToolStripMenuItem.Enabled = false;
            }
            #endregion
            for (int i = 0; i < m_bmWorkSpace.Width; i++)
                for (int j = 0; j < m_bmWorkSpace.Height; j++)
                {
                    Color pixelColor = m_bmWorkSpace.GetPixel(i, j);
                    Color newColor = Color.FromArgb(255-pixelColor.R, 255-pixelColor.G, 255-pixelColor.B);
                    m_bmWorkSpace.SetPixel(i, j, newColor);
                }

            Graphics im = m_workSpace.CreateGraphics();
            im.DrawImage(m_bmWorkSpace, 0, 0);
            m_isEdit = true;
            lblSaveStt.Text = "not yet";
        }
    }
}
