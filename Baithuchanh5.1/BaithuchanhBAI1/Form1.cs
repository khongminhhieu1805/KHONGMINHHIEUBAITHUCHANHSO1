using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaithuchanhBAI1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
            epCheck.Clear();
            bool hopLe = true;

          
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                epCheck.SetError(txtUsername, "Vui lòng nhập tên đăng nhập!");
                hopLe = false;
            }

            
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                epCheck.SetError(txtPassword, "Vui lòng nhập mật khẩu!");
                hopLe = false;
            }

            
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                epCheck.SetError(txtConfirmPassword, "Mật khẩu xác nhận không khớp!");
                hopLe = false;
            }

          
            DateTime homNay = DateTime.Today;
            int tuoi = homNay.Year - dtpBirthDate.Value.Year;
            if (dtpBirthDate.Value.Date > homNay.AddYears(-tuoi)) tuoi--;

            if (tuoi < 18)
            {
                epCheck.SetError(dtpBirthDate, "Yêu cầu phải đủ từ 18 tuổi trở lên!");
                hopLe = false;
            }

            
            if (!chkTerms.Checked)
            {
                epCheck.SetError(chkTerms, "Bạn phải đồng ý với điều khoản dịch vụ!");
                hopLe = false;
            }

            
            if (hopLe)
            {
                string gioiTinh = rbMale.Checked ? "Nam" : "Nữ";
                MessageBox.Show($"Đăng ký thành công!\nTài khoản: {txtUsername.Text}\nGiới tính: {gioiTinh}",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            epCheck.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            dtpBirthDate.Value = DateTime.Today;
            rbMale.Checked = true;
            chkTerms.Checked = false;
            txtUsername.Focus();
        }
    }
}
