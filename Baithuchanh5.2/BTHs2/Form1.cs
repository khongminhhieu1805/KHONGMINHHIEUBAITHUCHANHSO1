
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTHs2
{
    public partial class Form1 : Form
    {
        
        private List<MedicalService> allServices = new List<MedicalService>();

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            allServices = new List<MedicalService>
            {
                new MedicalService("Khám tổng quát", 150000, "Khám bệnh"),
                new MedicalService("Khám chuyên khoa", 250000, "Khám bệnh"),
                new MedicalService("Xét nghiệm máu", 120000, "Xét nghiệm"),
                new MedicalService("Xét nghiệm sinh hóa", 180000, "Xét nghiệm"),
                new MedicalService("Chụp X-Quang lồng ngực", 200000, "Chụp X-Quang"),
                new MedicalService("Chụp X-Quang khớp gối", 250000, "Chụp X-Quang"),
                new MedicalService("Vắc-xin Cúm", 350000, "Vắc-xin"),
                new MedicalService("Vắc-xin Viêm gan B", 280000, "Vắc-xin")
            };

            cboCategory.Items.AddRange(new object[] { "Khám bệnh", "Xét nghiệm", "Chụp X-Quang", "Vắc-xin" });
            cboCategory.SelectedIndex = 0;
        }

        
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loaiChon = cboCategory.SelectedItem.ToString();
            lstAvailableServices.Items.Clear();

            var dsLoc = allServices.Where(s => s.Category == loaiChon);
            foreach (var item in dsLoc)
            {
                lstAvailableServices.Items.Add(item);
            }
        }

        
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstAvailableServices.SelectedItem is MedicalService item)
            {
                lstSelectedServices.Items.Add(item);
                TinhTien();
            }
        }

        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelectedServices.SelectedIndex != -1)
            {
                lstSelectedServices.Items.RemoveAt(lstSelectedServices.SelectedIndex);
                TinhTien();
            }
        }

        
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lstSelectedServices.Items.Clear();
            TinhTien();
        }

        
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            TinhTien();
        }

      
        private void TinhTien()
        {
            decimal tienGoc = 0;
            foreach (MedicalService item in lstSelectedServices.Items)
            {
                tienGoc += item.Price;
            }

            if (!decimal.TryParse(txtDiscount.Text.Trim(), out decimal phanTramGiam) || phanTramGiam < 0)
            {
                phanTramGiam = 0;
            }
            if (phanTramGiam > 100) phanTramGiam = 100;

            decimal thanhTien = tienGoc * (1 - (phanTramGiam / 100));

            txtRawTotal.Text = tienGoc.ToString("N0") + " đ";
            txtFinalTotal.Text = thanhTien.ToString("N0") + " đ";
        }
    }
}