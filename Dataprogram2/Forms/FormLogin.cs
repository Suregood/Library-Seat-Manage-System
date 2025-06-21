using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;

namespace Dataprogram2.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            string user = txtUsername.Text.Trim();
            string pwd = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pwd))
            {
                lblMessage.Text = "请输入用户名和密码";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = @"
                 SELECT u.UserID, r.RoleName
                   FROM [User] u
                   JOIN User_Role ur ON u.UserID=ur.UserID
                   JOIN Role r ON ur.RoleID=r.RoleID
                  WHERE u.Username=@u AND u.Password=@p";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", user);
                cmd.Parameters.AddWithValue("@p", pwd);

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    int userId = rd.GetInt32(0);
                    string role = rd.GetString(1);

                    this.Hide();
                    if (role == "管理员")
                    {
                        new FormAdminDashboard(userId).Show();
                    }
                    else
                    {
                        new FormUserMain(userId).Show();
                    }
                }
                else
                {
                    lblMessage.Text = "用户名或密码错误";
                }
            }
        }
        
    }
 }
