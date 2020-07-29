namespace insa
{
    partial class Form6
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
            this.TEXTBOX = new System.Windows.Forms.TextBox();
            this.intbtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // TEXTBOX
            // 
            this.TEXTBOX.Location = new System.Drawing.Point(12, 39);
            this.TEXTBOX.Name = "TEXTBOX";
            this.TEXTBOX.Size = new System.Drawing.Size(302, 21);
            this.TEXTBOX.TabIndex = 34;
            this.TEXTBOX.TextChanged += new System.EventHandler(this.btnchk_TextChanged);
            // 
            // intbtn
            // 
            this.intbtn.Location = new System.Drawing.Point(334, 39);
            this.intbtn.Name = "intbtn";
            this.intbtn.Size = new System.Drawing.Size(75, 23);
            this.intbtn.TabIndex = 29;
            this.intbtn.Text = "등록";
            this.intbtn.UseVisualStyleBackColor = true;
            this.intbtn.Click += new System.EventHandler(this.intbtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(397, 435);
            this.dataGridView1.TabIndex = 36;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 528);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.TEXTBOX);
            this.Controls.Add(this.intbtn);
            this.Name = "Form6";
            this.Text = "Form6";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TEXTBOX;
        private System.Windows.Forms.Button intbtn;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}