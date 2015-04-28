using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Paint_Crazyland
{
    [DefaultEvent("Click")]
    public partial class ButtonFlat : UserControl
    {
        #region - ENUMS, ATTRIBUTES. PROPERTIES -
        public enum MouseStates
        {
            Hover, Down, Up, Enter, Leave
        }

        MouseStates m_mouseState = MouseStates.Leave;
        public MouseStates MouseState
        {
            get { return m_mouseState; }
            set { m_mouseState = value; }
        }

        public enum BTTextAlignment
        {
            Center, Top, Bot
        }
        BTTextAlignment m_textAlignment = BTTextAlignment.Bot;
        public BTTextAlignment TextAlignment
        {
            get { return m_textAlignment; }
            set { m_textAlignment = value; this.Invalidate(); }
        }

        Point m_textOrigin;
        public Point TextOrigin
        {
            get { return m_textOrigin; }
            set { m_textOrigin = value; this.Invalidate(); }
        }

        string m_text;
        public string ButtonText
        {
            get { return m_text; }
            set { m_text = value; this.Invalidate(); }
        }

        Color m_textColor;
        public Color TextColor
        {
            get { return m_textColor; }
            set { m_textColor = value; this.Invalidate(); }
        }

        //do lech cua anh khi ve len, mac dinh la 0,0 ve o giua
        Point m_imageOrigin;
        public Point ImageOrigin
        {
            get { return m_imageOrigin; }
            set { m_imageOrigin = value; this.Invalidate(); }
        }

        int m_shawdownDistance;
        public int ShadownDistance
        {
            get { return m_shawdownDistance; }
            set { m_shawdownDistance = value; this.Invalidate(); }
        }

        Image m_buttonImage;
        public Image ButtonImage
        {
            get { return m_buttonImage; }
            set { m_buttonImage = value; this.Invalidate(); }
        }

        Size m_imageSize;
        public Size ImageSize
        {
            get { return m_imageSize; }
            set { m_imageSize = value; this.Invalidate(); }
        }

        //do trong suot toi da, dung cho hieu ung sang cua button khi mouse hover, default = 20
        float m_alphaGlow;
        public float AlphaGlow
        {
            get { return m_alphaGlow; }
            set { m_alphaGlow = value; this.Invalidate(); }
        }

        //do bien thien alphaGlow theo thoi gian
        float m_deltaAlphaGlow;
        public float DeltaAlphaGlow
        {
            get { return m_deltaAlphaGlow; }
            set { m_deltaAlphaGlow = value; this.Invalidate(); }
        }

        //khoang distance thay doi dung cho khi mouse hover
        float m_deltaDistace;
        public float DeltaDistance
        {
            get { return m_deltaDistace; }
            set { m_deltaDistace = value; this.Invalidate(); }
        }

        bool m_saveChanged; // cho phep luu Location khi Event OnLocationChanged duoc goi
        public bool SaveChanged
        {
            get { return m_saveChanged; }
            set { m_saveChanged = value; this.Invalidate(); }
        }

        bool m_haveEffects;
        public bool HaveEffects
        {
            get { return m_haveEffects; }
            set { m_haveEffects = value; this.Invalidate(); }
        }

        bool m_isChosen;
        public bool IsChosen
        {
            get { return m_isChosen; }
            set { m_isChosen = value; this.Invalidate(); }
        }

        Timer m_timer;
        Color m_backColorSave; //luu mau backcolor de cho viec chuyen sau
        float m_deltaDistanceCount, // dung de tang distance cong vao position khi mouse hover
              m_alphaGlowValue; //gia tri hien tai cua alphaGlow
        Point m_locationSave; //dung de luu vi tri location truoc khi mouse hover
        Size m_sizeSave; // dung de luu size cua control truoc khi mouse hover, dung cho khi mouse leave va tinh toan khoang cach chinh xac
        bool m_sizeChanged, //kiem tra khi chayxong effect mouse hover thi khong thay doi location va size cua control nua
             m_firstTimeChanged, // dung de biet lan dau thay doi size, location de luu gia tri vao savelocation va savesize 
             m_mouseDowning; //neu nhu chuot dang o trang thai down thi khong xu ly nhung trang thai khac

        #endregion

        #region -INIT AND TIMER-
        public ButtonFlat()
        {
            #region -INIT-
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.BackColor = Color.FromArgb(44, 208, 136);


            m_shawdownDistance = 6;
            m_deltaDistace = 2;
            m_deltaDistanceCount = 0;
            m_alphaGlow = 40;
            m_deltaAlphaGlow = 2;
            m_alphaGlowValue = 0;
            m_textColor = Color.White;
            m_text = "Flat Button";
            m_imageOrigin = new Point(0, 0);
            m_textOrigin = new Point(0, 0);
            m_sizeChanged = false;
            m_firstTimeChanged = true;
            m_mouseDowning = false;
            m_haveEffects = true;

            m_timer = new Timer();
            m_timer.Interval = 50;
            m_timer.Tick += m_timer_Tick;
            m_timer.Enabled = true;
            m_timer.Stop();

            m_buttonImage = Paint_Crazyland.Properties.Resources.Marquee_Rectangle;
            m_imageSize = Paint_Crazyland.Properties.Resources.Marquee_Rectangle.Size;
            #endregion
        }


        void m_timer_Tick(object sender, EventArgs e)
        {
            switch (m_mouseState)
            {
                case MouseStates.Hover:
                    #region - MOUSE HOVER -
                    if (!m_haveEffects)
                        return;

                    if (m_deltaDistanceCount < m_shawdownDistance)
                    {
                        if (m_alphaGlowValue < m_alphaGlow)
                        {
                            m_alphaGlowValue += m_deltaAlphaGlow;
                        }

                        m_deltaDistanceCount += m_deltaDistace;
                        if (m_deltaDistanceCount > m_shawdownDistance)
                            m_deltaDistanceCount = m_shawdownDistance;

                        this.Size = new System.Drawing.Size(this.Size.Width + (int)m_deltaDistace,
                                                             this.Size.Height + (int)m_deltaDistace);

                        this.Location = new Point(this.Location.X - (int)m_deltaDistace, this.Location.Y);
                        this.Invalidate();
                    }
                    else
                    {
                        m_alphaGlowValue = m_alphaGlow;
                        m_sizeChanged = true;
                        this.Location = new Point(m_locationSave.X - (int)m_shawdownDistance,
                                                    m_locationSave.Y);

                        this.Size = new System.Drawing.Size(m_sizeSave.Width + (int)m_shawdownDistance,
                                                             m_sizeSave.Height + (int)m_shawdownDistance);

                        this.Invalidate();
                        m_timer.Stop();
                    }
                    #endregion
                    break;

                case MouseStates.Down:
                    break;

                case MouseStates.Up:
                    break;

                case MouseStates.Enter:
                    break;

                case MouseStates.Leave:
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region -DRAW EVENTS-
        protected override void OnBackColorChanged(EventArgs e)
        {
            if (BackColor != Color.Transparent)
                m_backColorSave = this.BackColor;

            base.OnBackColorChanged(e);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            if (m_saveChanged)
            {
                m_locationSave = this.Location;
                m_saveChanged = false;
            }

            base.OnLocationChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bm;
            if (m_buttonImage != null)
                bm = new Bitmap(m_buttonImage, m_imageSize);
            else
                bm = new Bitmap(this.Width, this.Height);

            switch (m_mouseState)
            {
                case MouseStates.Down:
                    #region - MOUSE DOWN -

                    this.BackColor = m_backColorSave;
                    // draw face
                    e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, this.Width, this.Height));

                    //ve icon len button
                    if (m_buttonImage != null)
                        e.Graphics.DrawImage(bm, new Point(this.Width / 2 - bm.Width / 2 + m_imageOrigin.X, this.Height / 2 - bm.Height / 2 + m_imageOrigin.Y));

                    //cho nen control toi lai
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), new Rectangle(0, 0, this.Width, this.Height));
                    #endregion
                    break;

                case MouseStates.Up:
                    #region - MOUSE UP -
                    m_alphaGlowValue = m_alphaGlow;
                    m_sizeChanged = true;

                    if (m_buttonImage != null)
                        e.Graphics.DrawImage(bm, new Point(this.Width / 2 - bm.Width / 2 + m_imageOrigin.X, this.Height / 2 - bm.Height / 2 + m_imageOrigin.Y));
                    #endregion
                    break;

                case MouseStates.Enter:
                    #region - MOUSE ENTER -
                    if (m_buttonImage != null)
                        e.Graphics.DrawImage(bm, new Point(this.Width / 2 - bm.Width / 2 + m_imageOrigin.X, this.Height / 2 - bm.Height / 2 + m_imageOrigin.Y));
                    #endregion
                    break;

                case MouseStates.Leave:
                    #region - MOUSE LEAVE -
                    //ve icon len button
                    if (m_buttonImage != null)
                        e.Graphics.DrawImage(bm, new Point(this.Width / 2 - bm.Width / 2 + m_imageOrigin.X, this.Height / 2 - bm.Height / 2 + m_imageOrigin.Y));
                    #endregion
                    break;

                case MouseStates.Hover:

                    #region - MOUSE HOVER -
                    if (!m_haveEffects)
                    {
                        //draw face
                        e.Graphics.FillRectangle(new SolidBrush(m_backColorSave), new Rectangle((int)m_deltaDistanceCount,
                                                    0, this.Width - (int)m_deltaDistanceCount, this.Height - (int)m_deltaDistanceCount));

                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb((int)m_alphaGlow, Color.White)), new Rectangle((int)m_deltaDistanceCount,
                                                    0, this.Width - (int)m_deltaDistanceCount, this.Height - (int)m_deltaDistanceCount));
                        //ve icon len button
                        if (m_buttonImage != null)
                            e.Graphics.DrawImage(bm, new Point(this.Width / 2 - bm.Width / 2 + m_imageOrigin.X,
                                                    this.Height / 2 - bm.Height / 2 - (int)m_deltaDistanceCount / 2 + m_imageOrigin.Y));
                        break;
                    }

                    e.Graphics.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(this.ClientRectangle.Width, this.ClientRectangle.Height),
                                                Color.FromArgb(60, 40, 40, 40), Color.FromArgb(120, 80, 80, 80)),
                                                new Rectangle(0, (int)m_deltaDistanceCount, this.Width - (int)m_deltaDistanceCount,
                                                                    this.Height - (int)m_deltaDistanceCount));
                    //draw face
                    e.Graphics.FillRectangle(new SolidBrush(m_backColorSave), new Rectangle((int)m_deltaDistanceCount,
                                                0, this.Width - (int)m_deltaDistanceCount, this.Height - (int)m_deltaDistanceCount));

                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb((int)m_alphaGlowValue, Color.White)), new Rectangle((int)m_deltaDistanceCount,
                                                0, this.Width - (int)m_deltaDistanceCount, this.Height - (int)m_deltaDistanceCount));

                    //ve icon len button
                    if (m_buttonImage != null)
                        e.Graphics.DrawImage(bm, new Point(this.Width / 2 - bm.Width / 2 + m_imageOrigin.X,
                                                this.Height / 2 - bm.Height / 2 - (int)m_deltaDistanceCount / 2 + m_imageOrigin.Y));
                    #endregion
                    break;

                default:
                    break;
            }

            switch (m_textAlignment)
            {
                case BTTextAlignment.Center:
                    SizeF textSize1 = e.Graphics.MeasureString(m_text, this.Font);
                    e.Graphics.DrawString(m_text, this.Font, new SolidBrush(m_textColor),
                                            new PointF(this.Width / 2 - textSize1.Width / 2 + m_textOrigin.X,
                                                        this.Height / 2 - textSize1.Height / 2 + m_textOrigin.Y));
                    break;

                case BTTextAlignment.Top:
                    break;

                case BTTextAlignment.Bot:
                    SizeF textSize = e.Graphics.MeasureString(m_text, this.Font);
                    e.Graphics.DrawString(m_text, this.Font, new SolidBrush(m_textColor),
                                            new PointF(this.Width / 2 - textSize.Width / 2 + m_textOrigin.X,
                                                        this.Height - textSize.Height - (int)m_deltaDistanceCount + m_textOrigin.Y));
                    break;
                default:
                    break;
            }

            if (m_isChosen)
            {
                this.BackColor = m_backColorSave;
                // draw face
                e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, this.Width, this.Height));

                //ve icon len button
                if (m_buttonImage != null)
                    e.Graphics.DrawImage(bm, new Point(this.Width / 2 - bm.Width / 2 + m_imageOrigin.X, this.Height / 2 - bm.Height / 2 + m_imageOrigin.Y));

                //cho nen control toi lai
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), new Rectangle(0, 0, this.Width, this.Height));
            }

            base.OnPaint(e);
        }
        #endregion

        #region -MOUSE EVENTS-
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!m_mouseDowning)
            {
                this.BackColor = Color.Transparent;
                m_mouseState = MouseStates.Hover;
                m_timer.Start();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.BackColor = Color.Transparent;
            m_mouseState = MouseStates.Enter;

            if (m_firstTimeChanged)
            {
                m_firstTimeChanged = false;
                m_sizeSave = this.Size;
                m_locationSave = this.Location;
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (!m_mouseDowning)
            {
                this.BackColor = Color.Transparent;
                m_mouseState = MouseStates.Hover;
                m_timer.Start();
            }

            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            m_mouseState = MouseStates.Leave;

            m_sizeChanged = true;
            m_firstTimeChanged = true;

            m_deltaDistanceCount = 0;
            m_alphaGlowValue = 0;

            this.BackColor = m_backColorSave;
            this.Location = m_locationSave;
            this.Size = m_sizeSave;

            this.Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            m_mouseDowning = true;
            m_mouseState = MouseStates.Down;
            this.Size = m_sizeSave;
            this.Location = m_locationSave;

            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            m_mouseState = MouseStates.Leave;
            m_mouseDowning = false;
            m_sizeChanged = true;
            m_firstTimeChanged = true;

            m_deltaDistanceCount = 0;
            m_alphaGlowValue = 0;

            this.Location = m_locationSave;
            this.Size = m_sizeSave;
            this.BackColor = m_backColorSave;

            this.Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
    }
}
