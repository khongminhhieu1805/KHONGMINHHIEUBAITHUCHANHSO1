using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTHso3
{
    public partial class Form1 : Form
    {
        // 1. Khởi tạo danh sách động để đồng bộ với DataGridView
        private BindingList<Product> products = new BindingList<Product>();
        private BindingSource bindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        // Hai hàm này giữ nguyên để không bị lỗi màn hình Design
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        // Khi mở Form: Nạp danh mục và 3 sản phẩm mẫu
        private void Form1_Load(object sender, EventArgs e)
        {
            cboCategory.Items.AddRange(new object[] { "Điện tử", "Phụ kiện", "Gia dụng", "Văn phòng phẩm" });
            cboCategory.SelectedIndex = 0;

            products.Add(new Product { ProductId = "SP01", ProductName = "Chuột Logitech G102", UnitPrice = 400000, Quantity = 10, Category = "Phụ kiện" });
            products.Add(new Product { ProductId = "SP02", ProductName = "Bàn phím cơ Akko", UnitPrice = 1200000, Quantity = 5, Category = "Phụ kiện" });
            products.Add(new Product { ProductId = "SP03", ProductName = "Tai nghe Gaming", UnitPrice = 650000, Quantity = 8, Category = "Phụ kiện" });

            bindingSource.DataSource = products;
            dgvProducts.DataSource = bindingSource;

            dgvProducts.Columns["ProductId"].HeaderText = "Mã SP";
            dgvProducts.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";
            dgvProducts.Columns["UnitPrice"].HeaderText = "Đơn Giá (đ)";
            dgvProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            dgvProducts.Columns["Quantity"].HeaderText = "Số Lượng";
            dgvProducts.Columns["Category"].HeaderText = "Danh Mục";
        }

        // Nút THÊM
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            string name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Mã và tên sản phẩm không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (products.Any(p => p.ProductId.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || !int.TryParse(txtQuantity.Text.Trim(), out int qty))
            {
                MessageBox.Show("Đơn giá và số lượng phải là số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            products.Add(new Product
            {
                ProductId = id,
                ProductName = name,
                UnitPrice = price,
                Quantity = qty,
                Category = cboCategory.SelectedItem.ToString()
            });

            LamTrang();
        }

        // Nút SỬA
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            var product = products.FirstOrDefault(p => p.ProductId.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa hoặc nhập đúng Mã SP!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || !int.TryParse(txtQuantity.Text.Trim(), out int qty))
            {
                MessageBox.Show("Đơn giá và số lượng phải là số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            product.ProductName = txtName.Text.Trim();
            product.UnitPrice = price;
            product.Quantity = qty;
            product.Category = cboCategory.SelectedItem.ToString();

            bindingSource.ResetBindings(false);
            MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Nút XÓA
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trên bảng!", "Thông báo");
                return;
            }

            var product = (Product)dgvProducts.CurrentRow.DataBoundItem;
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm '{product.ProductName}'?",
                                                 "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                products.Remove(product);
                LamTrang();
            }
        }

        // Nút LÀM MỚI
        private void btnClear_Click(object sender, EventArgs e)
        {
            LamTrang();
            bindingSource.DataSource = products;
        }

        // Nút TÌM KIẾM
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            var filtered = products.Where(p => p.ProductName.ToLower().Contains(keyword)).ToList();
            bindingSource.DataSource = new BindingList<Product>(filtered);
        }

        // Nhấp vào dòng trên DataGridView -> Đẩy dữ liệu lên các ô nhập
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProducts.Rows[e.RowIndex].DataBoundItem is Product p)
            {
                txtId.Text = p.ProductId;
                txtName.Text = p.ProductName;
                txtPrice.Text = p.UnitPrice.ToString();
                txtQuantity.Text = p.Quantity.ToString();
                cboCategory.SelectedItem = p.Category;
            }
        }

        // Hàm xóa trắng các ô nhập liệu
        private void LamTrang()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtSearch.Clear();
            cboCategory.SelectedIndex = 0;
            txtId.Focus();
        }
    }
}