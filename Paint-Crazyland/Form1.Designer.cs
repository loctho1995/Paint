﻿namespace Paint_Crazyland
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolBar = new System.Windows.Forms.Panel();
            this.m_btLeftColor = new Paint_Crazyland.ButtonFlat();
            this.m_btRightColor = new Paint_Crazyland.ButtonFlat();
            this.m_btZoom = new Paint_Crazyland.ButtonFlat();
            this.m_btShape = new Paint_Crazyland.ButtonFlat();
            this.m_btColorPicker = new Paint_Crazyland.ButtonFlat();
            this.m_btText = new Paint_Crazyland.ButtonFlat();
            this.m_btPencil = new Paint_Crazyland.ButtonFlat();
            this.m_btBrush = new Paint_Crazyland.ButtonFlat();
            this.m_btFill = new Paint_Crazyland.ButtonFlat();
            this.m_btEraser = new Paint_Crazyland.ButtonFlat();
            this.m_btMarquee = new Paint_Crazyland.ButtonFlat();
            this.m_propertiesBar = new System.Windows.Forms.Panel();
            this.m_lbShapes = new System.Windows.Forms.Label();
            this.m_cbShapes = new System.Windows.Forms.ComboBox();
            this.m_lbColorPicker = new System.Windows.Forms.Label();
            this.m_btColorPickerShow = new Paint_Crazyland.ButtonFlat();
            this.m_tbSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.m_workSpace = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.m_toolBar.SuspendLayout();
            this.m_propertiesBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_workSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.viewToolStripMenuItem1,
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripMenuItem,
            this.rotationToolStripMenuItem,
            this.convertToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.viewToolStripMenuItem.Text = "Image";
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.sizeToolStripMenuItem.Text = "Size";
            // 
            // rotationToolStripMenuItem
            // 
            this.rotationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToRightToolStripMenuItem,
            this.rightToLeftToolStripMenuItem,
            this.toolStripMenuItem2});
            this.rotationToolStripMenuItem.Name = "rotationToolStripMenuItem";
            this.rotationToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.rotationToolStripMenuItem.Text = "Rotation";
            // 
            // leftToRightToolStripMenuItem
            // 
            this.leftToRightToolStripMenuItem.Name = "leftToRightToolStripMenuItem";
            this.leftToRightToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.leftToRightToolStripMenuItem.Text = "90° Left to Right";
            // 
            // rightToLeftToolStripMenuItem
            // 
            this.rightToLeftToolStripMenuItem.Name = "rightToLeftToolStripMenuItem";
            this.rightToLeftToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.rightToLeftToolStripMenuItem.Text = "90° Right to Left";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem2.Text = "180 °";
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayScaleToolStripMenuItem,
            this.filerToolStripMenuItem,
            this.invertToolStripMenuItem});
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.convertToolStripMenuItem.Text = "Convert";
            // 
            // grayScaleToolStripMenuItem
            // 
            this.grayScaleToolStripMenuItem.Name = "grayScaleToolStripMenuItem";
            this.grayScaleToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.grayScaleToolStripMenuItem.Text = "Gray Scale";
            // 
            // filerToolStripMenuItem
            // 
            this.filerToolStripMenuItem.Name = "filerToolStripMenuItem";
            this.filerToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.filerToolStripMenuItem.Text = "Filer";
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.invertToolStripMenuItem.Text = "Invert";
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBoxToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // toolBoxToolStripMenuItem
            // 
            this.toolBoxToolStripMenuItem.CheckOnClick = true;
            this.toolBoxToolStripMenuItem.Name = "toolBoxToolStripMenuItem";
            this.toolBoxToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.toolBoxToolStripMenuItem.Text = "ToolBox";
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.statusBarToolStripMenuItem.Text = "Status Bar";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // m_toolBar
            // 
            this.m_toolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_toolBar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_toolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_toolBar.Controls.Add(this.m_btLeftColor);
            this.m_toolBar.Controls.Add(this.m_btRightColor);
            this.m_toolBar.Controls.Add(this.m_btZoom);
            this.m_toolBar.Controls.Add(this.m_btShape);
            this.m_toolBar.Controls.Add(this.m_btColorPicker);
            this.m_toolBar.Controls.Add(this.m_btText);
            this.m_toolBar.Controls.Add(this.m_btPencil);
            this.m_toolBar.Controls.Add(this.m_btBrush);
            this.m_toolBar.Controls.Add(this.m_btFill);
            this.m_toolBar.Controls.Add(this.m_btEraser);
            this.m_toolBar.Controls.Add(this.m_btMarquee);
            this.m_toolBar.Location = new System.Drawing.Point(0, 65);
            this.m_toolBar.Name = "m_toolBar";
            this.m_toolBar.Size = new System.Drawing.Size(115, 391);
            this.m_toolBar.TabIndex = 1;
            // 
            // m_btLeftColor
            // 
            this.m_btLeftColor.AlphaGlow = 40F;
            this.m_btLeftColor.BackColor = System.Drawing.Color.Black;
            this.m_btLeftColor.ButtonImage = null;
            this.m_btLeftColor.ButtonText = "";
            this.m_btLeftColor.DeltaAlphaGlow = 2F;
            this.m_btLeftColor.DeltaDistance = 2F;
            this.m_btLeftColor.HaveEffects = false;
            this.m_btLeftColor.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btLeftColor.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btLeftColor.IsChosen = false;
            this.m_btLeftColor.Location = new System.Drawing.Point(60, 230);
            this.m_btLeftColor.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btLeftColor.Name = "m_btLeftColor";
            this.m_btLeftColor.SaveChanged = false;
            this.m_btLeftColor.ShadownDistance = 6;
            this.m_btLeftColor.Size = new System.Drawing.Size(36, 35);
            this.m_btLeftColor.TabIndex = 10;
            this.m_btLeftColor.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btLeftColor.TextColor = System.Drawing.Color.White;
            this.m_btLeftColor.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btLeftColor.Click += new System.EventHandler(this.m_btLeftColor_Click);
            // 
            // m_btRightColor
            // 
            this.m_btRightColor.AlphaGlow = 40F;
            this.m_btRightColor.BackColor = System.Drawing.Color.White;
            this.m_btRightColor.ButtonImage = null;
            this.m_btRightColor.ButtonText = "";
            this.m_btRightColor.DeltaAlphaGlow = 2F;
            this.m_btRightColor.DeltaDistance = 2F;
            this.m_btRightColor.HaveEffects = false;
            this.m_btRightColor.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btRightColor.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btRightColor.IsChosen = false;
            this.m_btRightColor.Location = new System.Drawing.Point(73, 244);
            this.m_btRightColor.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btRightColor.Name = "m_btRightColor";
            this.m_btRightColor.SaveChanged = false;
            this.m_btRightColor.ShadownDistance = 6;
            this.m_btRightColor.Size = new System.Drawing.Size(37, 36);
            this.m_btRightColor.TabIndex = 9;
            this.m_btRightColor.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btRightColor.TextColor = System.Drawing.Color.White;
            this.m_btRightColor.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btRightColor.Click += new System.EventHandler(this.m_btRightColor_Click);
            // 
            // m_btZoom
            // 
            this.m_btZoom.AlphaGlow = 40F;
            this.m_btZoom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btZoom.ButtonImage = global::Paint_Crazyland.Properties.Resources.zoom_tool;
            this.m_btZoom.ButtonText = "";
            this.m_btZoom.DeltaAlphaGlow = 2F;
            this.m_btZoom.DeltaDistance = 2F;
            this.m_btZoom.HaveEffects = false;
            this.m_btZoom.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btZoom.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btZoom.IsChosen = false;
            this.m_btZoom.Location = new System.Drawing.Point(4, 230);
            this.m_btZoom.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btZoom.Name = "m_btZoom";
            this.m_btZoom.SaveChanged = false;
            this.m_btZoom.ShadownDistance = 6;
            this.m_btZoom.Size = new System.Drawing.Size(50, 50);
            this.m_btZoom.TabIndex = 8;
            this.m_btZoom.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btZoom.TextColor = System.Drawing.Color.White;
            this.m_btZoom.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btZoom.Click += new System.EventHandler(this.m_btZoom_Click);
            // 
            // m_btShape
            // 
            this.m_btShape.AlphaGlow = 40F;
            this.m_btShape.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btShape.ButtonImage = global::Paint_Crazyland.Properties.Resources.rectangle_stroked;
            this.m_btShape.ButtonText = "";
            this.m_btShape.DeltaAlphaGlow = 2F;
            this.m_btShape.DeltaDistance = 2F;
            this.m_btShape.HaveEffects = false;
            this.m_btShape.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btShape.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btShape.IsChosen = false;
            this.m_btShape.Location = new System.Drawing.Point(60, 174);
            this.m_btShape.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btShape.Name = "m_btShape";
            this.m_btShape.SaveChanged = false;
            this.m_btShape.ShadownDistance = 6;
            this.m_btShape.Size = new System.Drawing.Size(50, 50);
            this.m_btShape.TabIndex = 6;
            this.m_btShape.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btShape.TextColor = System.Drawing.Color.White;
            this.m_btShape.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btShape.Click += new System.EventHandler(this.m_btShape_Click);
            // 
            // m_btColorPicker
            // 
            this.m_btColorPicker.AlphaGlow = 40F;
            this.m_btColorPicker.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btColorPicker.ButtonImage = global::Paint_Crazyland.Properties.Resources.colorpicker;
            this.m_btColorPicker.ButtonText = "";
            this.m_btColorPicker.DeltaAlphaGlow = 2F;
            this.m_btColorPicker.DeltaDistance = 2F;
            this.m_btColorPicker.HaveEffects = false;
            this.m_btColorPicker.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btColorPicker.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btColorPicker.IsChosen = false;
            this.m_btColorPicker.Location = new System.Drawing.Point(4, 174);
            this.m_btColorPicker.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btColorPicker.Name = "m_btColorPicker";
            this.m_btColorPicker.SaveChanged = false;
            this.m_btColorPicker.ShadownDistance = 6;
            this.m_btColorPicker.Size = new System.Drawing.Size(50, 50);
            this.m_btColorPicker.TabIndex = 2;
            this.m_btColorPicker.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btColorPicker.TextColor = System.Drawing.Color.White;
            this.m_btColorPicker.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btColorPicker.Click += new System.EventHandler(this.m_btColorPicker_Click);
            // 
            // m_btText
            // 
            this.m_btText.AlphaGlow = 40F;
            this.m_btText.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btText.ButtonImage = global::Paint_Crazyland.Properties.Resources.text_tool;
            this.m_btText.ButtonText = "";
            this.m_btText.DeltaAlphaGlow = 2F;
            this.m_btText.DeltaDistance = 2F;
            this.m_btText.HaveEffects = false;
            this.m_btText.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btText.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btText.IsChosen = false;
            this.m_btText.Location = new System.Drawing.Point(60, 118);
            this.m_btText.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btText.Name = "m_btText";
            this.m_btText.SaveChanged = false;
            this.m_btText.ShadownDistance = 6;
            this.m_btText.Size = new System.Drawing.Size(50, 50);
            this.m_btText.TabIndex = 5;
            this.m_btText.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btText.TextColor = System.Drawing.Color.White;
            this.m_btText.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btText.Click += new System.EventHandler(this.m_btText_Click);
            // 
            // m_btPencil
            // 
            this.m_btPencil.AlphaGlow = 40F;
            this.m_btPencil.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btPencil.ButtonImage = global::Paint_Crazyland.Properties.Resources.pencil_512;
            this.m_btPencil.ButtonText = "";
            this.m_btPencil.DeltaAlphaGlow = 2F;
            this.m_btPencil.DeltaDistance = 2F;
            this.m_btPencil.HaveEffects = false;
            this.m_btPencil.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btPencil.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btPencil.IsChosen = false;
            this.m_btPencil.Location = new System.Drawing.Point(4, 118);
            this.m_btPencil.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btPencil.Name = "m_btPencil";
            this.m_btPencil.SaveChanged = false;
            this.m_btPencil.ShadownDistance = 6;
            this.m_btPencil.Size = new System.Drawing.Size(50, 50);
            this.m_btPencil.TabIndex = 4;
            this.m_btPencil.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btPencil.TextColor = System.Drawing.Color.White;
            this.m_btPencil.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btPencil.Click += new System.EventHandler(this.m_btPencil_Click);
            // 
            // m_btBrush
            // 
            this.m_btBrush.AlphaGlow = 40F;
            this.m_btBrush.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btBrush.ButtonImage = global::Paint_Crazyland.Properties.Resources.brush_tool;
            this.m_btBrush.ButtonText = "";
            this.m_btBrush.DeltaAlphaGlow = 2F;
            this.m_btBrush.DeltaDistance = 2F;
            this.m_btBrush.HaveEffects = false;
            this.m_btBrush.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btBrush.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btBrush.IsChosen = false;
            this.m_btBrush.Location = new System.Drawing.Point(60, 62);
            this.m_btBrush.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btBrush.Name = "m_btBrush";
            this.m_btBrush.SaveChanged = false;
            this.m_btBrush.ShadownDistance = 6;
            this.m_btBrush.Size = new System.Drawing.Size(50, 50);
            this.m_btBrush.TabIndex = 3;
            this.m_btBrush.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btBrush.TextColor = System.Drawing.Color.White;
            this.m_btBrush.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btBrush.Click += new System.EventHandler(this.m_btBrush_Click);
            // 
            // m_btFill
            // 
            this.m_btFill.AlphaGlow = 40F;
            this.m_btFill.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btFill.ButtonImage = global::Paint_Crazyland.Properties.Resources.fill_tool;
            this.m_btFill.ButtonText = "";
            this.m_btFill.DeltaAlphaGlow = 2F;
            this.m_btFill.DeltaDistance = 2F;
            this.m_btFill.HaveEffects = false;
            this.m_btFill.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btFill.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btFill.IsChosen = false;
            this.m_btFill.Location = new System.Drawing.Point(4, 62);
            this.m_btFill.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btFill.Name = "m_btFill";
            this.m_btFill.SaveChanged = false;
            this.m_btFill.ShadownDistance = 6;
            this.m_btFill.Size = new System.Drawing.Size(50, 50);
            this.m_btFill.TabIndex = 2;
            this.m_btFill.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btFill.TextColor = System.Drawing.Color.White;
            this.m_btFill.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btFill.Click += new System.EventHandler(this.m_btFill_Click);
            // 
            // m_btEraser
            // 
            this.m_btEraser.AlphaGlow = 40F;
            this.m_btEraser.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btEraser.ButtonImage = global::Paint_Crazyland.Properties.Resources.eraser_tool;
            this.m_btEraser.ButtonText = "";
            this.m_btEraser.DeltaAlphaGlow = 2F;
            this.m_btEraser.DeltaDistance = 2F;
            this.m_btEraser.HaveEffects = false;
            this.m_btEraser.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btEraser.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btEraser.IsChosen = false;
            this.m_btEraser.Location = new System.Drawing.Point(60, 6);
            this.m_btEraser.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btEraser.Name = "m_btEraser";
            this.m_btEraser.SaveChanged = false;
            this.m_btEraser.ShadownDistance = 6;
            this.m_btEraser.Size = new System.Drawing.Size(50, 50);
            this.m_btEraser.TabIndex = 1;
            this.m_btEraser.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btEraser.TextColor = System.Drawing.Color.White;
            this.m_btEraser.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btEraser.Click += new System.EventHandler(this.m_btEraser_Click);
            // 
            // m_btMarquee
            // 
            this.m_btMarquee.AlphaGlow = 40F;
            this.m_btMarquee.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_btMarquee.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_btMarquee.ButtonImage")));
            this.m_btMarquee.ButtonText = "";
            this.m_btMarquee.DeltaAlphaGlow = 2F;
            this.m_btMarquee.DeltaDistance = 2F;
            this.m_btMarquee.HaveEffects = false;
            this.m_btMarquee.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btMarquee.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btMarquee.IsChosen = false;
            this.m_btMarquee.Location = new System.Drawing.Point(4, 6);
            this.m_btMarquee.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btMarquee.Name = "m_btMarquee";
            this.m_btMarquee.SaveChanged = false;
            this.m_btMarquee.ShadownDistance = 6;
            this.m_btMarquee.Size = new System.Drawing.Size(50, 50);
            this.m_btMarquee.TabIndex = 0;
            this.m_btMarquee.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btMarquee.TextColor = System.Drawing.Color.White;
            this.m_btMarquee.TextOrigin = new System.Drawing.Point(0, 0);
            this.m_btMarquee.Click += new System.EventHandler(this.m_btMarquee_Click);
            // 
            // m_propertiesBar
            // 
            this.m_propertiesBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_propertiesBar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.m_propertiesBar.Controls.Add(this.m_lbShapes);
            this.m_propertiesBar.Controls.Add(this.m_cbShapes);
            this.m_propertiesBar.Controls.Add(this.m_lbColorPicker);
            this.m_propertiesBar.Controls.Add(this.m_btColorPickerShow);
            this.m_propertiesBar.Controls.Add(this.m_tbSize);
            this.m_propertiesBar.Controls.Add(this.label1);
            this.m_propertiesBar.Location = new System.Drawing.Point(0, 27);
            this.m_propertiesBar.Name = "m_propertiesBar";
            this.m_propertiesBar.Size = new System.Drawing.Size(1000, 39);
            this.m_propertiesBar.TabIndex = 3;
            // 
            // m_lbShapes
            // 
            this.m_lbShapes.AutoSize = true;
            this.m_lbShapes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbShapes.Location = new System.Drawing.Point(266, 9);
            this.m_lbShapes.Name = "m_lbShapes";
            this.m_lbShapes.Size = new System.Drawing.Size(64, 20);
            this.m_lbShapes.TabIndex = 14;
            this.m_lbShapes.Text = "Shapes";
            // 
            // m_cbShapes
            // 
            this.m_cbShapes.FormattingEnabled = true;
            this.m_cbShapes.Items.AddRange(new object[] {
            "Line",
            "Polygon",
            "Ellipse",
            "Rectangle"});
            this.m_cbShapes.Location = new System.Drawing.Point(336, 8);
            this.m_cbShapes.Name = "m_cbShapes";
            this.m_cbShapes.Size = new System.Drawing.Size(121, 21);
            this.m_cbShapes.TabIndex = 13;
            this.m_cbShapes.SelectedIndexChanged += new System.EventHandler(this.m_cbShapes_SelectedIndexChanged);
            // 
            // m_lbColorPicker
            // 
            this.m_lbColorPicker.AutoSize = true;
            this.m_lbColorPicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbColorPicker.Location = new System.Drawing.Point(121, 9);
            this.m_lbColorPicker.Name = "m_lbColorPicker";
            this.m_lbColorPicker.Size = new System.Drawing.Size(93, 20);
            this.m_lbColorPicker.TabIndex = 12;
            this.m_lbColorPicker.Text = "Color Picker";
            // 
            // m_btColorPickerShow
            // 
            this.m_btColorPickerShow.AlphaGlow = 40F;
            this.m_btColorPickerShow.BackColor = System.Drawing.Color.Black;
            this.m_btColorPickerShow.ButtonImage = null;
            this.m_btColorPickerShow.ButtonText = "";
            this.m_btColorPickerShow.DeltaAlphaGlow = 2F;
            this.m_btColorPickerShow.DeltaDistance = 2F;
            this.m_btColorPickerShow.HaveEffects = false;
            this.m_btColorPickerShow.ImageOrigin = new System.Drawing.Point(0, 0);
            this.m_btColorPickerShow.ImageSize = new System.Drawing.Size(40, 40);
            this.m_btColorPickerShow.IsChosen = false;
            this.m_btColorPickerShow.Location = new System.Drawing.Point(220, 1);
            this.m_btColorPickerShow.MouseState = Paint_Crazyland.ButtonFlat.MouseStates.Leave;
            this.m_btColorPickerShow.Name = "m_btColorPickerShow";
            this.m_btColorPickerShow.SaveChanged = false;
            this.m_btColorPickerShow.ShadownDistance = 6;
            this.m_btColorPickerShow.Size = new System.Drawing.Size(36, 35);
            this.m_btColorPickerShow.TabIndex = 11;
            this.m_btColorPickerShow.TextAlignment = Paint_Crazyland.ButtonFlat.BTTextAlignment.Bot;
            this.m_btColorPickerShow.TextColor = System.Drawing.Color.White;
            this.m_btColorPickerShow.TextOrigin = new System.Drawing.Point(0, 0);
            // 
            // m_tbSize
            // 
            this.m_tbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbSize.Location = new System.Drawing.Point(55, 4);
            this.m_tbSize.Multiline = true;
            this.m_tbSize.Name = "m_tbSize";
            this.m_tbSize.Size = new System.Drawing.Size(60, 29);
            this.m_tbSize.TabIndex = 1;
            this.m_tbSize.Text = "3";
            this.m_tbSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_tbSize.TextChanged += new System.EventHandler(this.m_tbSize_TextChanged);
            this.m_tbSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_tbSize_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // m_workSpace
            // 
            this.m_workSpace.BackColor = System.Drawing.Color.White;
            this.m_workSpace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_workSpace.Location = new System.Drawing.Point(116, 67);
            this.m_workSpace.Name = "m_workSpace";
            this.m_workSpace.Size = new System.Drawing.Size(881, 389);
            this.m_workSpace.TabIndex = 5;
            this.m_workSpace.TabStop = false;
            this.m_workSpace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_workSpace_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.m_propertiesBar);
            this.Controls.Add(this.m_toolBar);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.m_workSpace);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.m_toolBar.ResumeLayout(false);
            this.m_propertiesBar.ResumeLayout(false);
            this.m_propertiesBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_workSpace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonTab ribbonTab2;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonButton ribbonButton2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem leftToRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem grayScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.Panel m_toolBar;
        private ButtonFlat m_btEraser;
        private ButtonFlat m_btMarquee;
        private ButtonFlat m_btText;
        private ButtonFlat m_btPencil;
        private ButtonFlat m_btBrush;
        private ButtonFlat m_btFill;
        private ButtonFlat m_btShape;
        private ButtonFlat m_btColorPicker;
        private System.Windows.Forms.Panel m_propertiesBar;
        private ButtonFlat m_btZoom;
        private ButtonFlat m_btRightColor;
        private ButtonFlat m_btLeftColor;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbSize;
        private ButtonFlat m_btColorPickerShow;
        private System.Windows.Forms.Label m_lbColorPicker;
        private System.Windows.Forms.PictureBox m_workSpace;
        private System.Windows.Forms.ComboBox m_cbShapes;
        private System.Windows.Forms.Label m_lbShapes;
    }
}
