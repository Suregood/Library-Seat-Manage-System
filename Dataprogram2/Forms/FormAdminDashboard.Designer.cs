using System.Drawing;

namespace Dataprogram2.Forms
{
    partial class FormAdminDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnUserMgmt = new System.Windows.Forms.Button();
            this.btnSeatMgmt = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnCheckinReport = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();

            // 
            // label1
            // 
            this.label1.Text = "管理员面板";
            this.label1.Font = new Font("微软雅黑", 16F, FontStyle.Bold);
            this.label1.Location = new Point(140, 30);
            this.label1.Size = new Size(200, 40);

            // 
            // btnUserMgmt
            // 
            this.btnUserMgmt.Text = "用户管理";
            this.btnUserMgmt.Size = new Size(200, 40);
            this.btnUserMgmt.Location = new Point(100, 90);
            this.btnUserMgmt.Click += new System.EventHandler(this.btnUserMgmt_Click);
            // 
            // btnSeatMgmt
            // 
            this.btnSeatMgmt.Text = "座位与楼层管理";
            this.btnSeatMgmt.Size = new Size(200, 40);
            this.btnSeatMgmt.Location = new Point(100, 140);
            this.btnSeatMgmt.Click += new System.EventHandler(this.btnSeatMgmt_Click);
            // 
            // btnStats
            // 
            this.btnStats.Text = "预约统计";
            this.btnStats.Size = new Size(200, 40);
            this.btnStats.Location = new Point(100, 190);
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnCheckinReport
            // 
            this.btnCheckinReport.Text = "签到报表";
            this.btnCheckinReport.Size = new Size(200, 40);
            this.btnCheckinReport.Location = new Point(100, 240);
            this.btnCheckinReport.Click += new System.EventHandler(this.btnCheckinReport_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Text = "退出登录";
            this.btnLogout.Size = new Size(200, 40);
            this.btnLogout.Location = new Point(100, 290);
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // 
            // FormAdminDashboard
            // 
            this.ClientSize = new Size(420, 380);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUserMgmt);
            this.Controls.Add(this.btnSeatMgmt);
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.btnCheckinReport);
            this.Controls.Add(this.btnLogout);
            this.Text = "管理员面板";
        }

        private System.Windows.Forms.Button btnUserMgmt;
        private System.Windows.Forms.Button btnSeatMgmt;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Button btnCheckinReport;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label label1;
    }
}
