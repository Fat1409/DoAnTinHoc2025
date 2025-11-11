using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace DocGhiCSV
{
    public partial class Form1 : Form
    {
        private AVLTree<double> avlPrice = new AVLTree<double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo các cột của DataGridView
            dataGridView1.AutoGenerateColumns = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.Title = "Chọn file CSV cần đọc";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadCsvToGrid(filePath);
            }
        }

        private void LoadCsvToGrid(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath)
                                .Where(l => !string.IsNullOrWhiteSpace(l))
                                .ToList();

                if (lines.Count <= 1)
                {
                    MessageBox.Show("File CSV không có dữ liệu!");
                    return;
                }

                dataGridView1.Rows.Clear();
                avlPrice.Clear();

                foreach (var line in lines.Skip(1))
                {
                    var values = ParseCsvLine(line).Select(v => CleanValue(v)).ToArray();

                    // Đảm bảo có đủ 10 cột
                    while (values.Length < 10)
                        values = values.Concat(new string[] { "" }).ToArray();

                    dataGridView1.Rows.Add(values.Take(10).ToArray());

                    // Lấy giá từ cột thứ 8 (Price)
                    int priceIndex = 7;
                    if (priceIndex < values.Length && double.TryParse(values[priceIndex], out double price))
                    {
                        avlPrice.Insert(price, values);
                    }
                }

                MessageBox.Show("Đọc file CSV và hiển thị thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file CSV: " + ex.Message);
            }
        }

        private string[] ParseCsvLine(string line)
        {
            var parts = new List<string>();
            bool inQuotes = false;
            string current = "";

            foreach (char c in line)
            {
                if (c == '\"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    parts.Add(current);
                    current = "";
                }
                else
                {
                    current += c;
                }
            }

            parts.Add(current);
            return parts.ToArray();
        }

        private string CleanValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            value = value.Trim();
            if (value.StartsWith("\"") && value.EndsWith("\""))
                value = value.Substring(1, value.Length - 2);

            while (value.Contains("  "))
                value = value.Replace("  ", " ");

            return value.Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        private void btn_nodeHeight_Click(object sender, EventArgs e)
        {
            int height = avlPrice.GetTreeHeight();
            MessageBox.Show("Chiều cao cây AVL hiện tại: " + height);
        }

        private void btn_balance_Click(object sender, EventArgs e)
        {
            int balance = avlPrice.GetRootBalance();
            MessageBox.Show("Hệ số cân bằng tại nút gốc: " + balance);
        }

        private void btn_updateHeight_Click(object sender, EventArgs e)
        {
            avlPrice.RecalculateHeights();
            MessageBox.Show("Đã cập nhật lại chiều cao của toàn bộ cây AVL!");
        }

        private void btn_drawTree_Click(object sender, EventArgs e)
        {
            string treeText = avlPrice.PrintTree();

            Form treeForm = new Form();
            treeForm.Text = "Cấu trúc cây AVL";
            treeForm.Size = new Size(700, 900);
            treeForm.StartPosition = FormStartPosition.CenterParent;

            TextBox txt = new TextBox();
            txt.Multiline = true;
            txt.ScrollBars = ScrollBars.Both;
            txt.Font = new Font("Consolas", 10);
            txt.Dock = DockStyle.Fill;
            txt.Text = treeText;
            txt.ReadOnly = true;

            treeForm.Controls.Add(txt);
            treeForm.Show();
        }
    }
}