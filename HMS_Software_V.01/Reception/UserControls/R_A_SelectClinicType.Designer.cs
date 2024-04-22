namespace HMS_Software_V._01.Reception.UserControls
{
    partial class R_A_SelectClinicType
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RASCT_ClincType_lbl = new System.Windows.Forms.Label();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.RDTC_availability = new System.Windows.Forms.Label();
            this.materialCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RASCT_ClincType_lbl
            // 
            this.RASCT_ClincType_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RASCT_ClincType_lbl.AutoSize = true;
            this.RASCT_ClincType_lbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RASCT_ClincType_lbl.Location = new System.Drawing.Point(30, 36);
            this.RASCT_ClincType_lbl.Name = "RASCT_ClincType_lbl";
            this.RASCT_ClincType_lbl.Size = new System.Drawing.Size(108, 22);
            this.RASCT_ClincType_lbl.TabIndex = 2;
            this.RASCT_ClincType_lbl.Text = "Clinc Type";
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.RDTC_availability);
            this.materialCard1.Controls.Add(this.RASCT_ClincType_lbl);
            this.materialCard1.Depth = 0;
            this.materialCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(0, 0);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(437, 98);
            this.materialCard1.TabIndex = 1;
            // 
            // RDTC_availability
            // 
            this.RDTC_availability.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RDTC_availability.AutoSize = true;
            this.RDTC_availability.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.RDTC_availability.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDTC_availability.Location = new System.Drawing.Point(328, 32);
            this.RDTC_availability.Name = "RDTC_availability";
            this.RDTC_availability.Padding = new System.Windows.Forms.Padding(5);
            this.RDTC_availability.Size = new System.Drawing.Size(87, 29);
            this.RDTC_availability.TabIndex = 6;
            this.RDTC_availability.Text = "Available";
            // 
            // R_A_SelectClinicType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialCard1);
            this.MinimumSize = new System.Drawing.Size(437, 98);
            this.Name = "R_A_SelectClinicType";
            this.Size = new System.Drawing.Size(437, 98);
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label RASCT_ClincType_lbl;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        public System.Windows.Forms.Label RDTC_availability;
    }
}
