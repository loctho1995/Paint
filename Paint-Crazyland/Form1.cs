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
            Brush, Pencil, Eraser, Marquee, Zoom, Fill, Text, Shape, LeftColor, RightColor, ColorPicker, None
        }

        public Tools CurrentTool
        { get; set; }

        Color m_leftColor, m_rightColor;
        bool m_isMouseDown, m_isMouseUp, m_allowResieWorkSpace;
        Bitmap m_bmWorkSpace, m_bmTemp;
        Pen m_leftPen, m_rightPen;
        Point m_mouseLocation, m_mouseFixedLocation;
        List<Point> m_listMouseLocation;
        Graphics m_gpTemp;
        MouseButtons m_mouseButton;
        int m_grid = 15; //gioi han size khi re chuot vao resize workspace

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.AutoScroll = true;

            m_workSpace.Paint += m_workSpace_Paint;
            m_workSpace.MouseMove += m_workSpace_MouseMove;
            m_workSpace.MouseDown += m_workSpace_MouseDown;
            m_workSpace.MouseUp += m_workSpace_MouseUp;
            m_workSpace.MouseLeave += m_workSpace_MouseLeave;

            m_leftColor = m_btLeftColor.BackColor;
            m_rightColor = m_btRightColor.BackColor;

            CurrentTool = Tools.None;
            ActiveTool();

            m_bmWorkSpace = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            m_bmTemp = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            m_gpTemp = Graphics.FromImage(m_bmWorkSpace);
            m_gpTemp.FillRectangle(Brushes.White, new Rectangle(0, 0, m_bmWorkSpace.Width, m_bmWorkSpace.Height));
            m_listMouseLocation = new List<Point>();

            PensChanged();
        }

        void m_workSpaceResized()
        {
            //Bitmap newbm = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            //Graphics gp = Graphics.FromImage(newbm);
            //gp.FillRectangle(Brushes.White, new Rectangle(0, 0, newbm.Width, newbm.Height));

            //int width = m_workSpace.Width > m_bmWorkSpace.Width ? m_bmWorkSpace.Width : newbm.Width;
            //int height = m_workSpace.Height > m_bmWorkSpace.Height ? m_bmWorkSpace.Height : newbm.Height;

            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {
            //        newbm.SetPixel(i, j, m_bmWorkSpace.GetPixel(i, j));
            //    }
            //}

            //BitmapData bmData = m_bmWorkSpace.LockBits(new Rectangle(0, 0, m_bmWorkSpace.Width, m_bmWorkSpace.Height), ImageLockMode.ReadWrite, m_bmWorkSpace.PixelFormat);
            //IntPtr ptr = bmData.Scan0; //address of the first line
            //int bytes = Math.Abs(bmData.Stride) * m_bmWorkSpace.Height;
            //byte[] rgbValues = new byte[bytes];
            //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            //int depth = Bitmap.GetPixelFormatSize(m_bmWorkSpace.PixelFormat);

            //for (int i = 0; i < height; i++)
            //{
            //    for (int j = 0; j < width; j++)
            //    {
            //        //newbm.SetPixel(j, i, m_bmWorkSpace.GetPixel(j, i));
            //        int k = (i * width + j) * depth / 8;
            //        newbm.SetPixel(j, i, Color.FromArgb(rgbValues[k], rgbValues[k + 1], rgbValues[k + 2], rgbValues[k + 3]));              
            //    }
            //}

            //m_bmWorkSpace.UnlockBits(bmData);

            //m_bmWorkSpace = new Bitmap(newbm, newbm.Width, newbm.Height);
        }

        void PensChanged()
        {
            m_rightPen = new Pen(new SolidBrush(m_rightColor), int.Parse(m_tbSize.Text));
            m_leftPen = new Pen(new SolidBrush(m_leftColor), int.Parse(m_tbSize.Text));
            this.Invalidate();
        }

        void ButtonDefaulChosen()
        {
            m_btBrush.IsChosen = false;
            m_btColorPicker.IsChosen = false;
            m_btEraser.IsChosen = false;
            m_btFill.IsChosen = false;
            m_btLeftColor.IsChosen = false;
            m_btMarquee.IsChosen = false;
            m_btPencil.IsChosen = false;
            m_btRightColor.IsChosen = false;
            m_btShape.IsChosen = false;
            m_btText.IsChosen = false;
            m_btZoom.IsChosen = false;
        }

        void m_workSpace_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        void m_workSpace_MouseUp(object sender, MouseEventArgs e)
        {
            switch (CurrentTool)
            {
                case Tools.Brush:
                    break;
                case Tools.Pencil:
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
                    break;
                case Tools.Shape:
                    switch (m_cbShapes.SelectedItem.ToString())
	                {
                        case "Line":
                            m_gpTemp.DrawImage(m_bmTemp, new Point(0, 0));
                            break;

                        case "Polygon":
                            break;

                        case "Ellipse":
                            break;

                        case "Rectangle":
                            break;

                        default:
                            break;
	                }
                    break;
                case Tools.LeftColor:
                    break;
                case Tools.RightColor:
                    break;
                case Tools.ColorPicker:
                    break;
                case Tools.None:
                    break;
                default:
                    break;
            }

            if (m_allowResieWorkSpace)
                m_workSpaceResized();

            m_listMouseLocation.Clear();
            m_mouseLocation = e.Location;
            m_isMouseDown = false;
            m_isMouseUp = true;
            m_allowResieWorkSpace = false;
            m_workSpace.Invalidate();
            this.Cursor = Cursors.Default;
            this.Invalidate();
        }

        void m_workSpace_MouseDown(object sender, MouseEventArgs e)
        {
            m_isMouseUp = false;
            m_isMouseDown = true;

            m_mouseButton = e.Button;
            m_mouseLocation = e.Location;
            m_mouseFixedLocation = e.Location;

            if (e.Location.X >= m_workSpace.Width - m_grid && e.Location.Y >= m_workSpace.Height - m_grid)
            {
                m_allowResieWorkSpace = true;
            }

            m_workSpace.Invalidate();
        }

        void m_workSpace_MouseMove(object sender, MouseEventArgs e)
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
                    m_workSpace.Size = new Size(e.Location.X, e.Location.Y);

                //label1.Text = e.Location.ToString();
                this.Invalidate();
            }
            #endregion

            else if (m_isMouseDown)
            {
                label1.Text = m_listMouseLocation.Count.ToString();
                m_listMouseLocation.Add(e.Location);
                m_workSpace.Invalidate();
            }

        }

        void m_workSpace_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            m_gpTemp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            switch (CurrentTool)
            {
                case Tools.Brush:
                    #region -BRUSH-
                    #endregion
                    break;

                case Tools.Pencil:
                    #region -PENCIL-

                    if (m_isMouseDown && m_listMouseLocation.Count >= 2)
                    {
                        if (m_mouseButton == System.Windows.Forms.MouseButtons.Left)
                            m_gpTemp.DrawLines(m_leftPen, m_listMouseLocation.ToArray());
                        else
                            m_gpTemp.DrawLines(m_rightPen, m_listMouseLocation.ToArray());
                    }

                    if(m_isMouseUp)
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
                    #endregion
                    break;

                case Tools.Marquee:
                    #region -MARQUEE-
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
                    #endregion
                    break;

                case Tools.Shape:
                    #region -SHAPE-
                    try
                    {
                        switch (m_cbShapes.SelectedItem.ToString())
                        {
                            case "Line":
                                Graphics gp = Graphics.FromImage(m_bmTemp);
                                gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                gp.Clear(Color.Transparent);
                                gp.DrawLine(m_leftPen, m_mouseFixedLocation, m_mouseLocation);
                                break;

                            case "Polygon":
                                break;

                            case "Ellipse":
                                break;

                            case "Rectangle":
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

                case Tools.LeftColor:
                    #region -LEFTCOLOR-
                    #endregion
                    break;

                case Tools.RightColor:
                    #region -RIGHTCOLOR-
                    #endregion
                    break;

                case Tools.ColorPicker:
                    #region -COLORPICKER-
                    #endregion
                    break;

                default:
                    break;
            }

            
            e.Graphics.DrawImage(m_bmWorkSpace, new Point(0, 0));

            if(CurrentTool == Tools.Shape)
                e.Graphics.DrawImage(m_bmTemp, new Point(0, 0));
        }

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
            ActiveTool();
        }

        private void m_btPencil_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Pencil;
            ButtonDefaulChosen();
            m_btPencil.IsChosen = true;

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

        private void m_workSpace_MouseClick(object sender, MouseEventArgs e)
        {
            if (CurrentTool == Tools.ColorPicker)
            {
                m_btColorPickerShow.BackColor = m_bmWorkSpace.GetPixel(e.X, e.Y);
                this.Invalidate();
            }
        }

        //cho phep an/hien cac option can thiet
        void ActiveTool()
        {
            m_btColorPickerShow.Visible = false;
            m_lbColorPicker.Visible = false;
            m_lbShapes.Visible = false;
            m_cbShapes.Visible = false;

            switch (CurrentTool)
            {
                case Tools.Brush:
                    break;
                case Tools.Pencil:
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
                    break;

                case Tools.Shape:
                    m_lbShapes.Visible = true;
                    m_cbShapes.Visible = true;
                    break;

                case Tools.LeftColor:
                    break;

                case Tools.RightColor:
                    break;

                case Tools.ColorPicker:
                    m_btColorPickerShow.Visible = true;
                    m_lbColorPicker.Visible = true;
                    break;

                default:
                    break;
            }
        }

        private void m_cbShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Untitle";
            saveFileDialog1.Filter = "Bitmap(*.bmp)|*.Bmp|JPEG(*.JPEG)|*.jpg|GIF(*.Gif)|*.Gif|PNG(*.PNG)|*.PNG";

            Bitmap bmSave = new Bitmap(m_workSpace.Width, m_workSpace.Height);
            Graphics gp = Graphics.FromImage(bmSave);
            gp.DrawImage(m_bmWorkSpace, new Rectangle(0, 0, bmSave.Width, bmSave.Height), new Rectangle(0, 0, bmSave.Width, bmSave.Height), GraphicsUnit.Pixel);

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
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
        }
    }
}
