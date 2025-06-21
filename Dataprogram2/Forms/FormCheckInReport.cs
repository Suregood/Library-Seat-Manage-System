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

namespace Dataprogram2.Forms
{
    public partial class FormCheckInReport: Form
    {
        private string connectionString = "Server=.;Database=LibrarySeatDB;Trusted_Connection=True;";

        public FormCheckInReport()
        {
            InitializeComponent();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            string groupBy = cmbGroupBy.SelectedItem?.ToString();
            if (groupBy == null)
            {
                MessageBox.Show("请选择分组方式！");
                return;
            }

            DateTime start = dtpFrom.Value.Date;
            DateTime end = dtpTo.Value.Date.AddDays(1); // 包含当天

            string sql = "";

            if (groupBy == "全部记录")
            {
                sql = @"
            SELECT u.Username AS 用户名,
                   s.SeatNumber AS 座位号,
                   r.StartTime AS 开始时间,
                   r.EndTime AS 结束时间,
                   c.CheckInTime AS 签到时间,
                   c.CheckOutTime AS 签退时间,
                   c.CheckInStatus AS 状态
            FROM Reservation r
            JOIN [User] u ON r.UserID = u.UserID
            JOIN Seat s ON r.SeatID = s.SeatID
            LEFT JOIN CheckIn c ON r.ReservationID = c.ReservationID
            WHERE r.StartTime BETWEEN @Start AND @End
            ORDER BY r.StartTime DESC";
            }
            else if (groupBy == "按用户")
            {
                sql = @"
            SELECT u.Username AS 用户名,
                   COUNT(*) AS 预约次数,
                   SUM(CASE WHEN c.CheckInStatus = '已签到' THEN 1 ELSE 0 END) AS 已签到次数,
                   CAST(
                        CAST(SUM(CASE WHEN c.CheckInStatus = '已签到' THEN 1 ELSE 0 END) AS FLOAT) /
                        COUNT(*) * 100 AS DECIMAL(5,2)
                   ) AS 签到率
            FROM Reservation r
            JOIN [User] u ON r.UserID = u.UserID
            LEFT JOIN CheckIn c ON r.ReservationID = c.ReservationID
            WHERE r.StartTime BETWEEN @Start AND @End
            GROUP BY u.Username
            ORDER BY 签到率 DESC";
            }
            else if (groupBy == "按楼层")
            {
                sql = @"
            SELECT f.FloorNumber AS 楼层号,
                   COUNT(*) AS 预约次数,
                   SUM(CASE WHEN c.CheckInStatus = '已签到' THEN 1 ELSE 0 END) AS 已签到次数,
                   CAST(
                        CAST(SUM(CASE WHEN c.CheckInStatus = '已签到' THEN 1 ELSE 0 END) AS FLOAT) /
                        COUNT(*) * 100 AS DECIMAL(5,2)
                   ) AS 签到率
            FROM Reservation r
            JOIN Seat s ON r.SeatID = s.SeatID
            JOIN Floor f ON s.FloorID = f.FloorID
            LEFT JOIN CheckIn c ON r.ReservationID = c.ReservationID
            WHERE r.StartTime BETWEEN @Start AND @End
            GROUP BY f.FloorNumber
            ORDER BY 签到率 DESC";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Start", start);
                cmd.Parameters.AddWithValue("@End", end);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvCheckIn.DataSource = dt;
            }
        }

    }
}
