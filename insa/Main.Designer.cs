namespace insa
{
    partial class Main
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("인사코드 관리");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("인사기초정보", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("인사기본사항");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("인사기록 조회(통합)");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("인사기록관리", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("인사발령대장 관리");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("인사발령 등록");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("인사발령 조회");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("인사변동 관리", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("재직증명서");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("경력증명서");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("영문증명서");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("제증명서 발급대장 조회");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("제증명서 발급", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("부서별 인원현황");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("현황 및 통계", new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "노드1";
            treeNode1.Text = "인사코드 관리";
            treeNode2.Name = "노드0";
            treeNode2.Text = "인사기초정보";
            treeNode3.Name = "노드5";
            treeNode3.Text = "인사기본사항";
            treeNode4.Name = "노드30";
            treeNode4.Text = "인사기록 조회(통합)";
            treeNode5.Name = "노드4";
            treeNode5.Text = "인사기록관리";
            treeNode6.Name = "노드10";
            treeNode6.Text = "인사발령대장 관리";
            treeNode7.Name = "노드11";
            treeNode7.Text = "인사발령 등록";
            treeNode8.Name = "노드12";
            treeNode8.Text = "인사발령 조회";
            treeNode9.Name = "노드9";
            treeNode9.Text = "인사변동 관리";
            treeNode10.Name = "노드15";
            treeNode10.Text = "재직증명서";
            treeNode11.Name = "노드16";
            treeNode11.Text = "경력증명서";
            treeNode12.Name = "노드17";
            treeNode12.Text = "영문증명서";
            treeNode13.Name = "노드31";
            treeNode13.Text = "제증명서 발급대장 조회";
            treeNode14.Name = "노드14";
            treeNode14.Text = "제증명서 발급";
            treeNode15.Name = "노드19";
            treeNode15.Text = "부서별 인원현황";
            treeNode16.Name = "노드18";
            treeNode16.Text = "현황 및 통계";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode5,
            treeNode9,
            treeNode14,
            treeNode16});
            this.treeView1.Size = new System.Drawing.Size(191, 707);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(223, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1051, 707);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 757);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.treeView1);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}