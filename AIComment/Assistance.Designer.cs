namespace WinFormsApp1
{
    partial class frmTools
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTools));
            timer1 = new System.Windows.Forms.Timer(components);
            imageList1 = new ImageList(components);
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            ShowToolStripMenuItem = new ToolStripMenuItem();
            QuitToolStripMenuItem = new ToolStripMenuItem();
            timer2 = new System.Windows.Forms.Timer(components);
            button1 = new Button();
            button2 = new Button();
            checkBox1 = new CheckBox();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ShowToolStripMenuItem, QuitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(117, 64);
            // 
            // ShowToolStripMenuItem
            // 
            ShowToolStripMenuItem.Name = "ShowToolStripMenuItem";
            ShowToolStripMenuItem.Size = new Size(116, 30);
            ShowToolStripMenuItem.Text = "显示";
            // 
            // QuitToolStripMenuItem
            // 
            QuitToolStripMenuItem.Name = "QuitToolStripMenuItem";
            QuitToolStripMenuItem.Size = new Size(116, 30);
            QuitToolStripMenuItem.Text = "退出";
            QuitToolStripMenuItem.Click += QuitToolStripMenuItem_Click;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 600000;
            timer2.Tick += timer2_Tick;
            // 
            // button1
            // 
            button1.Location = new Point(249, 34);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 1;
            button1.Text = "执行";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_2;
            // 
            // button2
            // 
            button2.Location = new Point(249, 97);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 2;
            button2.Text = "退出";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Exist_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(43, 64);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(126, 28);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "执行AI评论";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // frmTools
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 173);
            Controls.Add(checkBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmTools";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AI评论";
            TopMost = true;
            FormClosing += frmTools_FormClosing;
            Load += frmTools_Load;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ImageList imageList1;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ShowToolStripMenuItem;
        private ToolStripMenuItem QuitToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
    }
}
