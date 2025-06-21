using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySeatSystem
{
    public partial class FormReservationStats : Form
    {
        private string connectionString = @"Data Source=.;Initial Catalog=LibrarySeatDB;Integrated Security=True";

        public FormReservationStats()
        {
            InitializeComponent();
            cmbGroupBy.SelectedIndex = 0;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string selected = cmbGroupBy.SelectedItem.ToString();
            string query = "";

            if (selected == "按用户")
            {
                query = @"
                    SELECT u.Username AS 用户名, COUNT(*) AS 预约次数
                    FROM Reservation r
                    JOIN [User] u ON r.UserID = u.UserID
                    GROUP BY u.Username
                    ORDER BY 预约次数 DESC";
            }
            else if (selected == "按楼层")
            {
                query = @"
                    SELECT f.FloorNumber AS 楼层, COUNT(*) AS 预约次数
                    FROM Reservation r
                    JOIN Seat s ON r.SeatID = s.SeatID
                    JOIN Floor f ON s.FloorID = f.FloorID
                    GROUP BY f.FloorNumber
                    ORDER BY 预约次数 DESC";
            }
            else if (selected == "按日期")
            {
                query = @"
                    SELECT CONVERT(date, StartTime) AS 预约日期, COUNT(*) AS 预约次数
                    FROM Reservation
                    GROUP BY CONVERT(date, StartTime)
                    ORDER BY 预约日期 DESC";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvStats.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message);
            }
        }
    }
}
