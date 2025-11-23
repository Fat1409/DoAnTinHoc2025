namespace DocGhiCSV
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.docfile_btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_updateHeight = new System.Windows.Forms.Button();
            this.btn_balance = new System.Windows.Forms.Button();
            this.btn_nodeHeight = new System.Windows.Forms.Button();
            this.btn_drawTree = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Handle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Variant_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inventory_quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Image_src = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // docfile_btn
            // 
            this.docfile_btn.Location = new System.Drawing.Point(80, 40);
            this.docfile_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.docfile_btn.Name = "docfile_btn";
            this.docfile_btn.Size = new System.Drawing.Size(196, 73);
            this.docfile_btn.TabIndex = 0;
            this.docfile_btn.Text = "Đọc File";
            this.docfile_btn.UseVisualStyleBackColor = true;
            this.docfile_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Product_type,
            this.Vendor,
            this.Tags,
            this.Handle,
            this.Variant_title,
            this.Sku,
            this.Price,
            this.Inventory_quantity,
            this.Image_src,
            this.Seri});
            this.dataGridView1.Location = new System.Drawing.Point(1, 161);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1583, 571);
            this.dataGridView1.TabIndex = 1;
            // 
            // btn_updateHeight
            // 
            this.btn_updateHeight.Location = new System.Drawing.Point(586, 12);
            this.btn_updateHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_updateHeight.Name = "btn_updateHeight";
            this.btn_updateHeight.Size = new System.Drawing.Size(148, 63);
            this.btn_updateHeight.TabIndex = 4;
            this.btn_updateHeight.Text = "Cập nhật chiều cao";
            this.btn_updateHeight.UseVisualStyleBackColor = true;
            this.btn_updateHeight.Click += new System.EventHandler(this.btn_updateHeight_Click);
            // 
            // btn_balance
            // 
            this.btn_balance.Location = new System.Drawing.Point(586, 90);
            this.btn_balance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_balance.Name = "btn_balance";
            this.btn_balance.Size = new System.Drawing.Size(148, 63);
            this.btn_balance.TabIndex = 5;
            this.btn_balance.Text = "Cân bằng cây";
            this.btn_balance.UseVisualStyleBackColor = true;
            this.btn_balance.Click += new System.EventHandler(this.btn_balance_Click);
            // 
            // btn_nodeHeight
            // 
            this.btn_nodeHeight.Location = new System.Drawing.Point(772, 40);
            this.btn_nodeHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_nodeHeight.Name = "btn_nodeHeight";
            this.btn_nodeHeight.Size = new System.Drawing.Size(148, 63);
            this.btn_nodeHeight.TabIndex = 6;
            this.btn_nodeHeight.Text = "chiều cao";
            this.btn_nodeHeight.UseVisualStyleBackColor = true;
            this.btn_nodeHeight.Click += new System.EventHandler(this.btn_nodeHeight_Click);
            // 
            // btn_drawTree
            // 
            this.btn_drawTree.Location = new System.Drawing.Point(1174, 40);
            this.btn_drawTree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_drawTree.Name = "btn_drawTree";
            this.btn_drawTree.Size = new System.Drawing.Size(148, 63);
            this.btn_drawTree.TabIndex = 7;
            this.btn_drawTree.Text = "Vẽ Cây";
            this.btn_drawTree.UseVisualStyleBackColor = true;
            this.btn_drawTree.Click += new System.EventHandler(this.btn_drawTree_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(970, 40);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 63);
            this.button1.TabIndex = 8;
            this.button1.Text = "Nhập số";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_NhapSo);
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.MinimumWidth = 10;
            this.Title.Name = "Title";
            this.Title.Width = 200;
            // 
            // Product_type
            // 
            this.Product_type.HeaderText = "Product_type";
            this.Product_type.MinimumWidth = 10;
            this.Product_type.Name = "Product_type";
            this.Product_type.Width = 200;
            // 
            // Vendor
            // 
            this.Vendor.HeaderText = "Vendor";
            this.Vendor.MinimumWidth = 10;
            this.Vendor.Name = "Vendor";
            this.Vendor.Width = 200;
            // 
            // Tags
            // 
            this.Tags.HeaderText = "Tags";
            this.Tags.MinimumWidth = 10;
            this.Tags.Name = "Tags";
            this.Tags.Width = 200;
            // 
            // Handle
            // 
            this.Handle.HeaderText = "Handle";
            this.Handle.MinimumWidth = 10;
            this.Handle.Name = "Handle";
            this.Handle.Width = 200;
            // 
            // Variant_title
            // 
            this.Variant_title.HeaderText = "Variant_title";
            this.Variant_title.MinimumWidth = 10;
            this.Variant_title.Name = "Variant_title";
            this.Variant_title.Width = 200;
            // 
            // Sku
            // 
            this.Sku.HeaderText = "Sku";
            this.Sku.MinimumWidth = 10;
            this.Sku.Name = "Sku";
            this.Sku.Width = 200;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 10;
            this.Price.Name = "Price";
            this.Price.Width = 200;
            // 
            // Inventory_quantity
            // 
            this.Inventory_quantity.HeaderText = "Inventory_quantity";
            this.Inventory_quantity.MinimumWidth = 10;
            this.Inventory_quantity.Name = "Inventory_quantity";
            this.Inventory_quantity.Width = 200;
            // 
            // Image_src
            // 
            this.Image_src.HeaderText = "Image_src";
            this.Image_src.MinimumWidth = 10;
            this.Image_src.Name = "Image_src";
            this.Image_src.Width = 200;
            // 
            // Seri
            // 
            this.Seri.HeaderText = "Seri";
            this.Seri.MinimumWidth = 10;
            this.Seri.Name = "Seri";
            this.Seri.Width = 200;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1814, 735);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_drawTree);
            this.Controls.Add(this.btn_nodeHeight);
            this.Controls.Add(this.btn_balance);
            this.Controls.Add(this.btn_updateHeight);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.docfile_btn);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button docfile_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tags;
        private System.Windows.Forms.DataGridViewTextBoxColumn Handle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variant_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sku;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inventory_quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Image_src;
        private System.Windows.Forms.Button btn_updateHeight;
        private System.Windows.Forms.Button btn_balance;
        private System.Windows.Forms.Button btn_nodeHeight;
        private System.Windows.Forms.Button btn_drawTree;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seri;
       
    }
}