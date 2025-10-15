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
            Fetch = new Button();
            sAppId = new TextBox();
            UserId = new TextBox();
            Environment = new ComboBox();
            sAppSecret = new TextBox();
            accessToken = new TextBox();
            corporationId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            UserAccess = new Button();
            TxtHash = new TextBox();
            TxtSQL = new TextBox();
            TxtBase64 = new TextBox();
            textBox1 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            lbl_msg = new Label();
            imageList1 = new ImageList(components);
            button1 = new Button();
            UserOpen = new Button();
            corpName = new TextBox();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            ShowToolStripMenuItem = new ToolStripMenuItem();
            QuitToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Fetch
            // 
            Fetch.Location = new Point(575, 106);
            Fetch.Margin = new Padding(5, 4, 5, 4);
            Fetch.Name = "Fetch";
            Fetch.Size = new Size(177, 32);
            Fetch.TabIndex = 0;
            Fetch.Text = "获取管理员链接";
            Fetch.UseVisualStyleBackColor = true;
            Fetch.Click += Fetch_Click;
            // 
            // sAppId
            // 
            sAppId.Location = new Point(162, 97);
            sAppId.Margin = new Padding(5, 4, 5, 4);
            sAppId.Multiline = true;
            sAppId.Name = "sAppId";
            sAppId.Size = new Size(399, 55);
            sAppId.TabIndex = 1;
            sAppId.TextChanged += sAppId_TextChanged;
            sAppId.KeyDown += sAppId_KeyUp;
            // 
            // UserId
            // 
            UserId.Location = new Point(162, 204);
            UserId.Margin = new Padding(5, 4, 5, 4);
            UserId.Name = "UserId";
            UserId.Size = new Size(399, 30);
            UserId.TabIndex = 2;
            UserId.TextChanged += UserId_TextChanged;
            // 
            // Environment
            // 
            Environment.FormattingEnabled = true;
            Environment.Items.AddRange(new object[] { "https://app.ekuaibao.com", "https://dd2.hosecloud.com", "https://wx2.ekuaibao.com" });
            Environment.Location = new Point(162, 13);
            Environment.Margin = new Padding(5, 4, 5, 4);
            Environment.Name = "Environment";
            Environment.Size = new Size(399, 32);
            Environment.TabIndex = 3;
            Environment.Text = "https://app.ekuaibao.com";
            // 
            // sAppSecret
            // 
            sAppSecret.Location = new Point(162, 163);
            sAppSecret.Margin = new Padding(5, 4, 5, 4);
            sAppSecret.Name = "sAppSecret";
            sAppSecret.ReadOnly = true;
            sAppSecret.Size = new Size(399, 30);
            sAppSecret.TabIndex = 4;
            sAppSecret.TextChanged += sAppSecret_TextChanged;
            // 
            // accessToken
            // 
            accessToken.Location = new Point(162, 245);
            accessToken.Margin = new Padding(5, 4, 5, 4);
            accessToken.Name = "accessToken";
            accessToken.ReadOnly = true;
            accessToken.Size = new Size(399, 30);
            accessToken.TabIndex = 6;
            // 
            // corporationId
            // 
            corporationId.Location = new Point(162, 56);
            corporationId.Margin = new Padding(5, 4, 5, 4);
            corporationId.Name = "corporationId";
            corporationId.Size = new Size(399, 30);
            corporationId.TabIndex = 7;
            corporationId.TextChanged += corporationId_TextChanged;
            corporationId.KeyDown += corporationId_KeyDown;
            corporationId.Leave += corporationId_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 13);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(46, 24);
            label1.TabIndex = 9;
            label1.Text = "环境";
            label1.DoubleClick += label1_DoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 97);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(75, 24);
            label2.TabIndex = 10;
            label2.Text = "appKey";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 163);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(113, 24);
            label3.TabIndex = 11;
            label3.Text = "appSecurity";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 204);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(64, 24);
            label4.TabIndex = 12;
            label4.Text = "用户名";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(42, 245);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(116, 24);
            label5.TabIndex = 13;
            label5.Text = "accessToken";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(42, 56);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(63, 24);
            label6.TabIndex = 14;
            label6.Text = "企业Id";
            // 
            // UserAccess
            // 
            UserAccess.Enabled = false;
            UserAccess.Location = new Point(575, 204);
            UserAccess.Margin = new Padding(5, 4, 5, 4);
            UserAccess.Name = "UserAccess";
            UserAccess.Size = new Size(177, 32);
            UserAccess.TabIndex = 16;
            UserAccess.Text = "授权人员";
            UserAccess.UseVisualStyleBackColor = true;
            UserAccess.Click += button1_Click;
            // 
            // TxtHash
            // 
            TxtHash.Location = new Point(42, 302);
            TxtHash.Margin = new Padding(5, 4, 5, 4);
            TxtHash.Name = "TxtHash";
            TxtHash.Size = new Size(837, 30);
            TxtHash.TabIndex = 17;
            TxtHash.TextChanged += TxtHash_TextChanged;
            // 
            // TxtSQL
            // 
            TxtSQL.Location = new Point(42, 343);
            TxtSQL.Margin = new Padding(5, 4, 5, 4);
            TxtSQL.Multiline = true;
            TxtSQL.Name = "TxtSQL";
            TxtSQL.Size = new Size(838, 60);
            TxtSQL.TabIndex = 18;
            TxtSQL.TextChanged += textBox2_TextChanged;
            // 
            // TxtBase64
            // 
            TxtBase64.Location = new Point(42, 414);
            TxtBase64.Margin = new Padding(5, 4, 5, 4);
            TxtBase64.Multiline = true;
            TxtBase64.Name = "TxtBase64";
            TxtBase64.ReadOnly = true;
            TxtBase64.Size = new Size(837, 75);
            TxtBase64.TabIndex = 19;
            TxtBase64.TextChanged += textBox1_TextChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(561, 245);
            textBox1.Margin = new Padding(5, 4, 5, 4);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(369, 30);
            textBox1.TabIndex = 20;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // lbl_msg
            // 
            lbl_msg.AutoSize = true;
            lbl_msg.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lbl_msg.Location = new Point(575, 13);
            lbl_msg.Name = "lbl_msg";
            lbl_msg.Size = new Size(219, 30);
            lbl_msg.TabIndex = 23;
            lbl_msg.Text = "✔已经复制到粘贴板";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // button1
            // 
            button1.Location = new Point(762, 106);
            button1.Margin = new Padding(5, 4, 5, 4);
            button1.Name = "button1";
            button1.Size = new Size(168, 32);
            button1.TabIndex = 25;
            button1.Text = "跳转";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // UserOpen
            // 
            UserOpen.Enabled = false;
            UserOpen.Location = new Point(770, 204);
            UserOpen.Margin = new Padding(5, 4, 5, 4);
            UserOpen.Name = "UserOpen";
            UserOpen.Size = new Size(160, 32);
            UserOpen.TabIndex = 26;
            UserOpen.Text = "跳转";
            UserOpen.UseVisualStyleBackColor = true;
            UserOpen.Click += button2_Click;
            // 
            // corpName
            // 
            corpName.Location = new Point(575, 56);
            corpName.Name = "corpName";
            corpName.ReadOnly = true;
            corpName.Size = new Size(355, 30);
            corpName.TabIndex = 27;
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
            // frmTools
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 296);
            Controls.Add(corpName);
            Controls.Add(UserOpen);
            Controls.Add(button1);
            Controls.Add(lbl_msg);
            Controls.Add(textBox1);
            Controls.Add(TxtBase64);
            Controls.Add(TxtSQL);
            Controls.Add(TxtHash);
            Controls.Add(UserAccess);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(corporationId);
            Controls.Add(accessToken);
            Controls.Add(sAppSecret);
            Controls.Add(Environment);
            Controls.Add(UserId);
            Controls.Add(sAppId);
            Controls.Add(Fetch);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmTools";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "授权链接";
            TopMost = true;
            FormClosing += frmTools_FormClosing;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Fetch;
        private TextBox sAppId;
        private TextBox UserId;
        private ComboBox Environment;
        private TextBox sAppSecret;
        private TextBox accessToken;
        private TextBox corporationId;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button UserAccess;
        private TextBox TxtHash;
        private TextBox TxtSQL;
        private TextBox TxtBase64;
        private TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private Label lbl_msg;
        private ImageList imageList1;
        private Button button1;
        private Button UserOpen;
        private TextBox corpName;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ShowToolStripMenuItem;
        private ToolStripMenuItem QuitToolStripMenuItem;
    }
}
