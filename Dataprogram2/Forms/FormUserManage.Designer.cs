using System.Windows.Forms;

namespace Dataprogram2.Forms
{
    partial class FormUserManage
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
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();

            // 
            // dgvUsers
            // 
            this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(20, 20);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(360, 200);
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(100, 240);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(120, 20);

            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(100, 270);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(120, 20);

            // 
            // lblUsername
            // 
            this.lblUsername.Text = "用户名：";
            this.lblUsername.Location = new System.Drawing.Point(30, 240);

            // 
            // lblPassword
            // 
            this.lblPassword.Text = "密码：";
            this.lblPassword.Location = new System.Drawing.Point(30, 270);

            // 
            // btnAddUser
            // 
            this.btnAddUser.Text = "添加用户";
            this.btnAddUser.Location = new System.Drawing.Point(240, 240);
            this.btnAddUser.Size = new System.Drawing.Size(100, 25);
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);

            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Text = "删除用户";
            this.btnDeleteUser.Location = new System.Drawing.Point(240, 270);
            this.btnDeleteUser.Size = new System.Drawing.Size(100, 25);
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

            // 
            // btnRefresh
            // 
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Location = new System.Drawing.Point(150, 310);
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // 
            // FormUserManagement
            // 
            this.ClientSize = new System.Drawing.Size(400, 360);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnRefresh);
            this.Text = "用户管理";
        }

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
    }
}
