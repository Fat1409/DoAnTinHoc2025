using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DocGhiCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Có thể để trống hoặc hiển thị hướng dẫn
        }

        // ====== NÚT ĐỌC FILE CSV ======
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.Title = "Chọn file CSV cần đọc";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadAndCleanCsv(filePath);
            }
        }

        // ====== HÀM ĐỌC & LÀM SẠCH DỮ LIỆU ======
        private void LoadAndCleanCsv(string filePath)
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

                foreach (var line in lines.Skip(1)) // bỏ dòng header
                {
                    var rawValues = ParseCsvLine(line);
                    var cleanedValues = rawValues.Select(v => CleanValue(v)).ToArray();

                    if (cleanedValues.All(string.IsNullOrWhiteSpace)) continue;

                    // đảm bảo đủ 10 cột
                    if (cleanedValues.Length < 10)
                        cleanedValues = cleanedValues.Concat(Enumerable.Repeat("", 10 - cleanedValues.Length)).ToArray();

                    // 🔹 Ép kiểu cột F (index 5) thành chuỗi
                    cleanedValues[5] = ForceStringForSize(cleanedValues[5]);

                    dataGridView1.Rows.Add(cleanedValues.Take(10).ToArray());
                }

                MessageBox.Show("Đọc & làm sạch CSV thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý file CSV: " + ex.Message);
            }
        }

        // ====== HÀM PARSE DÒNG CSV ======
        private string[] ParseCsvLine(string line)
        {
            var parts = new List<string>();
            bool inQuotes = false;
            string current = "";

            foreach (char c in line)
            {
                if (c == '\"')
                    inQuotes = !inQuotes;
                else if (c == ',' && !inQuotes)
                {
                    parts.Add(current);
                    current = "";
                }
                else
                    current += c;
            }

            parts.Add(current);
            return parts.ToArray();
        }

        // ====== HÀM LÀM SẠCH TỪNG GIÁ TRỊ ======
        private string CleanValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            value = value.Trim();

            // Bỏ ngoặc kép
            if (value.StartsWith("\"") && value.EndsWith("\""))
                value = value.Substring(1, value.Length - 2);

            // Xóa khoảng trắng kép
            while (value.Contains("  "))
                value = value.Replace("  ", " ");

            // Xóa ký tự đặc biệt
            value = value.Replace("\r", "").Replace("\n", "").Replace("\t", "");

            return value;
        }

        // ====== HÀM ÉP CỘT F THÀNH CHUỖI ======
        private string ForceStringForSize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            value = value.Trim();

            // Nếu là số → giữ nguyên định dạng
            if (double.TryParse(value, out double number))
            {
                // Nếu là số nguyên thì hiển thị "3", còn nếu số thực thì hiển thị "3.5"
                if (Math.Abs(number % 1) < 0.0001)
                    return number.ToString("0");
                else
                    return number.ToString("0.##");
            }

            // Nếu là chữ → viết hoa chữ cái đầu
            return Capitalize(value);
        }

        private string Capitalize(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }

        // ====== NÚT LƯU FILE CSV ======
        //private void SaveToCsv(string filePath)
        //{
        //    try
        //    {
        //        using (StreamWriter writer = new StreamWriter(filePath))
        //        {
        //            // Ghi header
        //            string[] headers = dataGridView1.Columns
        //                .Cast<DataGridViewColumn>()
        //                .Select(c => c.HeaderText)
        //                .ToArray();
        //            writer.WriteLine(string.Join(",", headers));

        //            // Ghi từng dòng
        //            foreach (DataGridViewRow row in dataGridView1.Rows)
        //            {
        //                if (!row.IsNewRow)
        //                {
        //                    string[] cells = row.Cells
        //                        .Cast<DataGridViewCell>()
        //                        .Select(c => (c.Value?.ToString() ?? "").Replace(",", " ")) // tránh lỗi dấu phẩy
        //                        .ToArray();

        //                    writer.WriteLine(string.Join(",", cells));
        //                }
        //            }
        //        }

        //        MessageBox.Show("Đã lưu file CSV thành công!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi lưu CSV: " + ex.Message);
        //    }
        //}

        // ====== THÊM NÚT LƯU FILE ======
        //private void buttonSave_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveDialog = new SaveFileDialog();
        //    saveDialog.Filter = "CSV Files (*.csv)|*.csv";
        //    saveDialog.Title = "Lưu file CSV đã chỉnh sửa";

        //    if (saveDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        SaveToCsv(saveDialog.FileName);
        //    }
        //}
    }
}
