using System.Windows.Forms;
using System.Drawing;


namespace Dataprogram2.Forms
{
    partial class FormCheckIn
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
            this.dgvToday = new System.Windows.Forms.DataGridView();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();

            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLibrary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFloor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOut = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvToday)).BeginInit();
            this.SuspendLayout();

            this.dgvToday.AllowUserToAddRows = false;
            this.dgvToday.AllowUserToDeleteRows = false;
            this.dgvToday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToday.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colID, this.colLibrary, this.colFloor, this.colSeat,
                this.colStart, this.colEnd, this.colStatus, this.colIn, this.colOut
            });
            this.dgvToday.Location = new System.Drawing.Point(20, 20);
            this.dgvToday.MultiSelect = false;
            this.dgvToday.ReadOnly = true;
            this.dgvToday.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvToday.Size = new System.Drawing.Size(850, 300);

            this.colID.HeaderText = "预约ID";
            this.colID.Visible = false;
            this.colLibrary.HeaderText = "图书馆";
            this.colFloor.HeaderText = "楼层";
            this.colSeat.HeaderText = "座位号";
            this.colStart.HeaderText = "开始";
            this.colEnd.HeaderText = "结束";
            this.colStatus.HeaderText = "签到状态";
            this.colIn.HeaderText = "签到时间";
            this.colOut.HeaderText = "签退时间";

            this.btnCheckIn.Text = "签到";
            this.btnCheckIn.Location = new System.Drawing.Point(200, 340);
            this.btnCheckIn.Size = new System.Drawing.Size(100, 35);
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);

            this.btnCheckOut.Text = "签退";
            this.btnCheckOut.Location = new System.Drawing.Point(350, 340);
            this.btnCheckOut.Size = new System.Drawing.Size(100, 35);
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);

            this.lblStatus.Location = new System.Drawing.Point(20, 390);
            this.lblStatus.Size = new System.Drawing.Size(850, 25);
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            this.ClientSize = new System.Drawing.Size(900, 430);
            this.Controls.Add(this.dgvToday);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.btnCheckOut);
            this.Controls.Add(this.lblStatus);
            this.Text = "签到与签退";
            this.Load += new System.EventHandler(this.FormCheckIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToday)).EndInit();
            this.ResumeLayout(false);
        }

        private DataGridView dgvToday;
        private Button btnCheckIn;
        private Button btnCheckOut;
        private Label lblStatus;

        private DataGridViewTextBoxColumn colID, colLibrary, colFloor, colSeat,
            colStart, colEnd, colStatus, colIn, colOut;
    }
}
