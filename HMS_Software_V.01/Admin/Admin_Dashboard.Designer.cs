namespace HMS_Software_V._01.Admin
{
    partial class Admin_Dashboard
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
            this.sidePanel = new System.Windows.Forms.Panel();
            this.sidePanel_FlowLP = new System.Windows.Forms.FlowLayoutPanel();
            this.admin_mainPanel = new System.Windows.Forms.Panel();
            this.admin_home_btn = new System.Windows.Forms.Button();
            this.admin_doctors_btn = new System.Windows.Forms.Button();
            this.admin_nurse_btn = new System.Windows.Forms.Button();
            this.admin_home1 = new HMS_Software_V._01.Admin.Admin_UserConotrols.Admin_home();
            this.sidePanel.SuspendLayout();
            this.sidePanel_FlowLP.SuspendLayout();
            this.admin_mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(179)))), ((int)(((byte)(255)))));
            this.sidePanel.Controls.Add(this.sidePanel_FlowLP);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Margin = new System.Windows.Forms.Padding(0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(70, 537);
            this.sidePanel.TabIndex = 0;
            // 
            // sidePanel_FlowLP
            // 
            this.sidePanel_FlowLP.Controls.Add(this.admin_home_btn);
            this.sidePanel_FlowLP.Controls.Add(this.admin_doctors_btn);
            this.sidePanel_FlowLP.Controls.Add(this.admin_nurse_btn);
            this.sidePanel_FlowLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel_FlowLP.Location = new System.Drawing.Point(0, 0);
            this.sidePanel_FlowLP.Name = "sidePanel_FlowLP";
            this.sidePanel_FlowLP.Size = new System.Drawing.Size(70, 537);
            this.sidePanel_FlowLP.TabIndex = 0;
            // 
            // admin_mainPanel
            // 
            this.admin_mainPanel.Controls.Add(this.admin_home1);
            this.admin_mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.admin_mainPanel.Location = new System.Drawing.Point(70, 0);
            this.admin_mainPanel.Name = "admin_mainPanel";
            this.admin_mainPanel.Size = new System.Drawing.Size(938, 537);
            this.admin_mainPanel.TabIndex = 1;
            // 
            // admin_home_btn
            // 
            this.admin_home_btn.FlatAppearance.BorderSize = 0;
            this.admin_home_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(110)))), ((int)(((byte)(255)))));
            this.admin_home_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(110)))), ((int)(((byte)(255)))));
            this.admin_home_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.admin_home_btn.Image = global::HMS_Software_V._01.Properties.Resources.Home_icon;
            this.admin_home_btn.Location = new System.Drawing.Point(0, 130);
            this.admin_home_btn.Margin = new System.Windows.Forms.Padding(0, 130, 0, 20);
            this.admin_home_btn.Name = "admin_home_btn";
            this.admin_home_btn.Size = new System.Drawing.Size(70, 53);
            this.admin_home_btn.TabIndex = 0;
            this.admin_home_btn.UseVisualStyleBackColor = true;
            // 
            // admin_doctors_btn
            // 
            this.admin_doctors_btn.FlatAppearance.BorderSize = 0;
            this.admin_doctors_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(110)))), ((int)(((byte)(255)))));
            this.admin_doctors_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(110)))), ((int)(((byte)(255)))));
            this.admin_doctors_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.admin_doctors_btn.Image = global::HMS_Software_V._01.Properties.Resources.Doctor_icon;
            this.admin_doctors_btn.Location = new System.Drawing.Point(0, 203);
            this.admin_doctors_btn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.admin_doctors_btn.Name = "admin_doctors_btn";
            this.admin_doctors_btn.Size = new System.Drawing.Size(70, 53);
            this.admin_doctors_btn.TabIndex = 1;
            this.admin_doctors_btn.UseVisualStyleBackColor = true;
            // 
            // admin_nurse_btn
            // 
            this.admin_nurse_btn.FlatAppearance.BorderSize = 0;
            this.admin_nurse_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(110)))), ((int)(((byte)(255)))));
            this.admin_nurse_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(110)))), ((int)(((byte)(255)))));
            this.admin_nurse_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.admin_nurse_btn.Image = global::HMS_Software_V._01.Properties.Resources.Home_icon;
            this.admin_nurse_btn.Location = new System.Drawing.Point(0, 276);
            this.admin_nurse_btn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.admin_nurse_btn.Name = "admin_nurse_btn";
            this.admin_nurse_btn.Size = new System.Drawing.Size(70, 53);
            this.admin_nurse_btn.TabIndex = 2;
            this.admin_nurse_btn.UseVisualStyleBackColor = true;
            // 
            // admin_home1
            // 
            this.admin_home1.BackColor = System.Drawing.Color.White;
            this.admin_home1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.admin_home1.Location = new System.Drawing.Point(0, 0);
            this.admin_home1.Name = "admin_home1";
            this.admin_home1.Size = new System.Drawing.Size(938, 537);
            this.admin_home1.TabIndex = 0;
            // 
            // Admin_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1008, 537);
            this.Controls.Add(this.admin_mainPanel);
            this.Controls.Add(this.sidePanel);
            this.MinimumSize = new System.Drawing.Size(1024, 576);
            this.Name = "Admin_Dashboard";
            this.Text = "Admin_Dashboard";
            this.sidePanel.ResumeLayout(false);
            this.sidePanel_FlowLP.ResumeLayout(false);
            this.admin_mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.FlowLayoutPanel sidePanel_FlowLP;
        private System.Windows.Forms.Button admin_home_btn;
        private System.Windows.Forms.Button admin_doctors_btn;
        private System.Windows.Forms.Button admin_nurse_btn;
        private System.Windows.Forms.Panel admin_mainPanel;
        private Admin_UserConotrols.Admin_home admin_home1;
    }
}