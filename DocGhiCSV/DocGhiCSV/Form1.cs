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
        private AVLTree<string> avlSeri = new AVLTree<string>();
        private List<string[]> allData = new List<string[]>();
        private string currentTreeType = "Price"; // Mặc định là Price

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                avlSeri.Clear();
                allData.Clear();

                foreach (var line in lines.Skip(1))
                {
                    var values = ParseCsvLine(line).Select(v => CleanValue(v)).ToArray();

                    // Đảm bảo có đủ 11 cột
                    while (values.Length < 11)
                        values = values.Concat(new string[] { "" }).ToArray();

                    dataGridView1.Rows.Add(values.Take(11).ToArray());
                    allData.Add(values);
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
            int height = currentTreeType == "Price" ? avlPrice.GetTreeHeight() : avlSeri.GetTreeHeight();
            MessageBox.Show($"Chiều cao cây AVL ({currentTreeType}): {height}");
        }

        private void btn_balance_Click(object sender, EventArgs e)
        {
            int balance = currentTreeType == "Price" ? avlPrice.GetRootBalance() : avlSeri.GetRootBalance();
            MessageBox.Show($"Hệ số cân bằng tại nút gốc ({currentTreeType}): {balance}");
        }

        private void btn_updateHeight_Click(object sender, EventArgs e)
        {
            if (currentTreeType == "Price")
                avlPrice.RecalculateHeights();
            else
                avlSeri.RecalculateHeights();

            MessageBox.Show($"Đã cập nhật lại chiều cao của cây AVL ({currentTreeType})!");
        }

        private void button1_Click_NhapSo(object sender, EventArgs e)
        {
            if (allData.Count == 0)
            {
                MessageBox.Show("Vui lòng đọc file CSV trước!");
                return;
            }

            Form selectForm = new Form();
            selectForm.Text = "Chọn sản phẩm và loại cây AVL";
            selectForm.Size = new Size(900, 700);
            selectForm.StartPosition = FormStartPosition.CenterParent;

            // Radio buttons để chọn loại cây
            GroupBox treeTypeGroup = new GroupBox();
            treeTypeGroup.Text = "Chọn loại cây AVL";
            treeTypeGroup.Location = new Point(10, 10);
            treeTypeGroup.Size = new Size(860, 60);

            RadioButton rbPrice = new RadioButton();
            rbPrice.Text = "Cây AVL theo Price";
            rbPrice.Location = new Point(20, 25);
            rbPrice.Checked = true;
            rbPrice.AutoSize = true;

            RadioButton rbSeri = new RadioButton();
            rbSeri.Text = "Cây AVL theo Seri";
            rbSeri.Location = new Point(250, 25);
            rbSeri.AutoSize = true;

            treeTypeGroup.Controls.Add(rbPrice);
            treeTypeGroup.Controls.Add(rbSeri);

            CheckedListBox checkedListBox = new CheckedListBox();
            checkedListBox.Location = new Point(10, 130);
            checkedListBox.Size = new Size(860, 450);
            checkedListBox.CheckOnClick = true;

            int priceIndex = 7;
            int seriIndex = 10;

            for (int i = 0; i < allData.Count; i++)
            {
                var row = allData[i];
                string title = row.Length > 0 ? row[0] : "N/A";
                string price = row.Length > priceIndex ? row[priceIndex] : "N/A";
                string seri = row.Length > seriIndex ? row[seriIndex] : "N/A";
                string displayText = $"[{i + 1}] {title} - Giá: {price} - Seri: {seri}";
                checkedListBox.Items.Add(displayText);
            }

            Label lblInstruction = new Label();
            lblInstruction.Text = "Chọn từ 1 đến 10 sản phẩm (tick vào checkbox):";
            lblInstruction.Location = new Point(10, 80);
            lblInstruction.Size = new Size(860, 30);
            lblInstruction.Font = new Font("Arial", 10, FontStyle.Bold);

            Button btnConfirm = new Button();
            btnConfirm.Text = "Thêm vào cây AVL";
            btnConfirm.Location = new Point(300, 600);
            btnConfirm.Size = new Size(150, 40);
            btnConfirm.Click += (s, evt) =>
            {
                var selectedIndices = checkedListBox.CheckedIndices;

                if (selectedIndices.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất 1 sản phẩm!");
                    return;
                }

                if (selectedIndices.Count > 10)
                {
                    MessageBox.Show("Chỉ được chọn tối đa 10 sản phẩm!");
                    return;
                }

                bool usePrice = rbPrice.Checked;
                currentTreeType = usePrice ? "Price" : "Seri";

                if (usePrice)
                {
                    avlPrice.Clear();
                    int inserted = 0;

                    foreach (int index in selectedIndices)
                    {
                        var values = allData[index];
                        if (priceIndex < values.Length && double.TryParse(values[priceIndex], out double price))
                        {
                            avlPrice.Insert(price, values);
                            inserted++;
                        }
                    }

                    MessageBox.Show($"Đã thêm {inserted} sản phẩm vào cây AVL (Price)!");
                }
                else
                {
                    avlSeri.Clear();
                    int inserted = 0;

                    foreach (int index in selectedIndices)
                    {
                        var values = allData[index];
                        if (seriIndex < values.Length && !string.IsNullOrWhiteSpace(values[seriIndex]))
                        {
                            avlSeri.Insert(values[seriIndex], values);
                            inserted++;
                        }
                    }

                    MessageBox.Show($"Đã thêm {inserted} sản phẩm vào cây AVL (Seri)!");
                }

                selectForm.Close();
            };

            Button btnCancel = new Button();
            btnCancel.Text = "Hủy";
            btnCancel.Location = new Point(470, 600);
            btnCancel.Size = new Size(100, 40);
            btnCancel.Click += (s, evt) => selectForm.Close();

            selectForm.Controls.Add(treeTypeGroup);
            selectForm.Controls.Add(lblInstruction);
            selectForm.Controls.Add(checkedListBox);
            selectForm.Controls.Add(btnConfirm);
            selectForm.Controls.Add(btnCancel);

            selectForm.ShowDialog();
        }

        private void btn_drawTree_Click(object sender, EventArgs e)
        {
            List<dynamic> nodeInfos;
            string treeTitle;

            if (currentTreeType == "Price")
            {
                var priceNodes = avlPrice.GetNodeInfos();
                if (priceNodes.Count == 0)
                {
                    MessageBox.Show("Cây AVL (Price) rỗng! Vui lòng thêm dữ liệu trước.");
                    return;
                }
                nodeInfos = priceNodes.Cast<dynamic>().ToList();
                treeTitle = "Cây AVL - Price";
            }
            else
            {
                var seriNodes = avlSeri.GetNodeInfos();
                if (seriNodes.Count == 0)
                {
                    MessageBox.Show("Cây AVL (Seri) rỗng! Vui lòng thêm dữ liệu trước.");
                    return;
                }
                nodeInfos = seriNodes.Cast<dynamic>().ToList();
                treeTitle = "Cây AVL - Seri";
            }

            Form treeForm = new Form();
            treeForm.Text = treeTitle;
            treeForm.Size = new Size(1200, 700);
            treeForm.StartPosition = FormStartPosition.CenterParent;

            Panel drawPanel = new Panel();
            drawPanel.Dock = DockStyle.Fill;
            drawPanel.AutoScroll = true;
            drawPanel.BackColor = Color.White;

            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            int maxDepth = nodeInfos.Max(n => n.Depth);
            int canvasWidth = nodeInfos.Count * 100 + 100;
            int canvasHeight = (maxDepth + 1) * 120 + 100;

            Bitmap bmp = new Bitmap(canvasWidth, canvasHeight);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int nodeRadius = 30;
            Font font = new Font("Arial", 9, FontStyle.Bold);
            Font levelFont = new Font("Arial", 8);
            Brush textBrush = Brushes.Black;
            Pen linePen = new Pen(Color.Gray, 2);

            Dictionary<int, Point> nodePositions = new Dictionary<int, Point>();
            int xSpacing = canvasWidth / (nodeInfos.Count + 1);
            int ySpacing = 100;

            for (int i = 0; i < nodeInfos.Count; i++)
            {
                var node = nodeInfos[i];
                int x = (i + 1) * xSpacing;
                int y = node.Depth * ySpacing + 60;
                nodePositions[node.Index] = new Point(x, y);
            }

            foreach (var node in nodeInfos)
            {
                if (node.ParentIndex >= 0 && nodePositions.ContainsKey(node.ParentIndex))
                {
                    Point childPos = nodePositions[node.Index];
                    Point parentPos = nodePositions[node.ParentIndex];
                    g.DrawLine(linePen, parentPos.X, parentPos.Y, childPos.X, childPos.Y);
                }
            }

            foreach (var node in nodeInfos)
            {
                Point pos = nodePositions[node.Index];

                g.FillEllipse(Brushes.LightBlue, pos.X - nodeRadius, pos.Y - nodeRadius,
                             nodeRadius * 2, nodeRadius * 2);
                g.DrawEllipse(Pens.Black, pos.X - nodeRadius, pos.Y - nodeRadius,
                             nodeRadius * 2, nodeRadius * 2);

                string keyText = currentTreeType == "Price"
                    ? ((double)node.Key).ToString("F2")
                    : node.Key.ToString();

                SizeF textSize = g.MeasureString(keyText, font);
                g.DrawString(keyText, font, textBrush,
                           pos.X - textSize.Width / 2, pos.Y - textSize.Height / 2);

                string levelText = $"Tầng {node.Depth}";
                SizeF levelSize = g.MeasureString(levelText, levelFont);
                g.DrawString(levelText, levelFont, Brushes.Red,
                           pos.X - levelSize.Width / 2, pos.Y + nodeRadius + 5);
            }

            pictureBox.Image = bmp;
            drawPanel.Controls.Add(pictureBox);
            treeForm.Controls.Add(drawPanel);

            int treeHeight = currentTreeType == "Price" ? avlPrice.GetTreeHeight() : avlSeri.GetTreeHeight();

            Label infoLabel = new Label();
            infoLabel.Text = $"Loại: {currentTreeType} | Tổng số node: {nodeInfos.Count} | Chiều cao cây: {treeHeight}";
            infoLabel.Dock = DockStyle.Top;
            infoLabel.Height = 35;
            infoLabel.TextAlign = ContentAlignment.MiddleCenter;
            infoLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            infoLabel.BackColor = Color.LightYellow;
            treeForm.Controls.Add(infoLabel);

            treeForm.Show();
        }
    }
}