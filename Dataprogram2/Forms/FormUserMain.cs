using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dataprogram2.Forms
{
    private int _userId;
    public partial class FormUserMain: Form
    {
        public FormUserMain()
        {
            InitializeComponent();
            _userId = userId;
        }
        private void FormUserMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"欢迎，用户编号：{_userId}";
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            FormUserReservation res = new FormUserReservation(_userId);
            res.ShowDialog();
        }

        private void btnMyRes_Click(object sender, EventArgs e)
        {
            FormMyReservations myRes = new FormMyReservations(_userId);
            myRes.ShowDialog();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            FormCheckIn check = new FormCheckIn(_userId);
            check.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Restart();  // 返回登录界面
        }
    }
}
