using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace DocGhiFileCSV
{
    public class Product
    {
        [Name("title")]
        public string Title { get; set; }

        [Name("product_type")]
        public string ProductType { get; set; }

        [Name("vendor")]
        public string Vendor { get; set; }

        [Name("tags")]
        public string Tags { get; set; }

        [Name("handle")]
        public string Handle { get; set; }

        [Name("variant_title")]
        public string VariantTitle { get; set; }

        [Name("sku")]
        public string SKU { get; set; }

        [Name("price")]
        public string Price { get; set; }

        [Name("inventory_quantity")]
        public string InventoryQuantity { get; set; }

        [Name("image_src")]
        public string ImageSrc { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string filePath = @"D:\DOANTINHOC\gymshark_products.csv";

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true,
                    TrimOptions = TrimOptions.Trim,
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    BadDataFound = null, // ✅ Bỏ qua dòng lỗi
                    Delimiter = ","
                };

                List<Product> products;

                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                using (var csv = new CsvReader(reader, config))
                {
                    products = new List<Product>(csv.GetRecords<Product>());
                }

                Console.WriteLine($"✅ Đọc file CSV thành công! Tổng số sản phẩm: {products.Count}\n");
                Console.WriteLine(new string('=', 120));

                // 🧾 In toàn bộ thông tin từng sản phẩm
                foreach (var p in products)
                {
                    Console.WriteLine($"📦 Title: {p.Title}");
                    Console.WriteLine($"🏷️  Product Type: {p.ProductType}");
                    Console.WriteLine($"🏭 Vendor: {p.Vendor}");
                    Console.WriteLine($"🏷️  Tags: {p.Tags}");
                    Console.WriteLine($"🔗 Handle: {p.Handle}");
                    Console.WriteLine($"🧩 Variant Title: {p.VariantTitle}");
                    Console.WriteLine($"🆔 SKU: {p.SKU}");
                    Console.WriteLine($"💰 Price: {p.Price}");
                    Console.WriteLine($"📦 Inventory Quantity: {p.InventoryQuantity}");
                    Console.WriteLine($"🖼️ Image: {p.ImageSrc}");
                    Console.WriteLine(new string('-', 120));
                }

                Console.WriteLine("\n✅ Hiển thị toàn bộ dữ liệu CSV hoàn tất!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
