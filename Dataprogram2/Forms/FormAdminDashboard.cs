using LibrarySeatSystem;
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
    public partial class FormAdminDashboard: Form
    {
        private int _userId;
        public FormAdminDashboard(int userId)
        {
            InitializeComponent();
            _userId = _userId;
        }
        private void btnUserMgmt_Click(object sender, EventArgs e)
        {
            FormUserManage userForm = new FormUserManage();
            userForm.ShowDialog();
        }

        private void btnSeatMgmt_Click(object sender, EventArgs e)
        {
            FormSeatManagement seatForm = new FormSeatManagement();
            seatForm.ShowDialog();
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            FormReservationStats statsForm = new FormReservationStats();
            statsForm.ShowDialog();
        }

        private void btnCheckinReport_Click(object sender, EventArgs e)
        {
            FormCheckInReport reportForm = new FormCheckInReport();
            reportForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // 返回登录窗体
        }
    }
}
