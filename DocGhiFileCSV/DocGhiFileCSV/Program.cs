using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = @"D:\DOANTINHOC\gymshark_products.csv";
        string outputPath = @"D:\DOANTINHOC\gymshark_products_cleaned.csv";

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = null,
            HeaderValidated = null,
            BadDataFound = null
        };

        // Đọc dữ liệu gốc
        List<dynamic> records;
        using (var reader = new StreamReader(inputPath))
        using (var csv = new CsvReader(reader, config))
        {
            records = csv.GetRecords<dynamic>().ToList();
        }

        Console.WriteLine($"✅ Đã đọc {records.Count} dòng dữ liệu.");

        // --- Chuẩn bị ---
        var random = new Random();
        var cleaned = new List<Dictionary<string, object>>();

        // Bản đồ số -> size
        var sizeMapping = new Dictionary<int, string>
        {
            {0, "Extra Small"},
            {1, "Small"},
            {2, "Medium"},
            {3, "Large"},
            {4, "Extra Large"},
            {5, "XXL"}
        };
        string defaultSize = "Medium";

        // Các giá trị coi như thiếu
        string[] missingTokens = { "", "n/a", "none", "null", "undefined", "default title", "nan" };

        // --- Làm sạch từng dòng ---
        foreach (var item in records)
        {
            var row = (IDictionary<string, object>)item;
            var cleanRow = new Dictionary<string, object>();

            foreach (var kv in row)
            {
                string key = kv.Key;
                string value = kv.Value?.ToString()?.Trim() ?? "";
                string lowerKey = key.ToLower();
                bool isMissing = missingTokens.Contains(value.ToLower());

                // Nếu dữ liệu bị thiếu
                if (isMissing)
                {
                    // Xử lý từng loại cột
                    if (lowerKey.Contains("variant_title") || lowerKey.Contains("variant") || lowerKey.Contains("option"))
                        value = defaultSize;

                    else if (lowerKey.Contains("price") || lowerKey.Contains("cost") || lowerKey.Contains("amount"))
                        value = random.Next(20, 500).ToString();

                    else if (lowerKey.Contains("inventory_quantity") || lowerKey.Contains("stock"))
                        value = random.Next(5, 200).ToString();

                    else if (lowerKey.Contains("rating") || lowerKey.Contains("review"))
                        value = (random.NextDouble() * 2 + 3).ToString("0.0");

                    else if (lowerKey.Contains("name") || lowerKey.Contains("title"))
                        value = "Sample Product";

                    else if (lowerKey.Contains("category"))
                        value = "General Category";

                    else if (lowerKey.Contains("description") || lowerKey.Contains("detail"))
                        value = "Auto-generated description";

                    else if (lowerKey.Contains("vendor"))
                        value = "Default Vendor";

                    else if (lowerKey.Contains("sku"))
                        value = "SKU-" + random.Next(1000, 9999);

                    else
                        // Thay Unknown bằng giá trị chuỗi tự động
                        value = "AutoFilled";
                }
                else
                {
                    // --- Dữ liệu không trống ---
                    // Xử lý các cột variant / option có giá trị là số
                    if (lowerKey.Contains("variant_title") || lowerKey.Contains("variant") || lowerKey.Contains("option"))
                    {
                        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                        {
                            int rounded = (int)Math.Round(num);
                            value = sizeMapping.ContainsKey(rounded) ? sizeMapping[rounded] : defaultSize;
                        }
                        else
                        {
                            string lowerVal = value.ToLower();
                            if (!(lowerVal.Contains("small") || lowerVal.Contains("medium") || lowerVal.Contains("large") || lowerVal.Contains("xl")))
                                value = defaultSize;
                        }
                    }

                    // Nếu là cột số mà bằng 0 → đổi thành > 0
                    else if (lowerKey.Contains("price") || lowerKey.Contains("cost") || lowerKey.Contains("amount") ||
                             lowerKey.Contains("inventory_quantity") || lowerKey.Contains("stock"))
                    {
                        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double numVal))
                        {
                            if (numVal <= 0)
                                value = random.Next(5, 200).ToString();
                        }
                        else
                        {
                            value = random.Next(5, 200).ToString();
                        }
                    }
                }

                cleanRow[key] = value;
            }

            cleaned.Add(cleanRow);
        }

        // --- Ghi dữ liệu ra file mới ---
        string finalPath = outputPath;
        var headers = cleaned.First().Keys.ToList();

        try
        {
            using (var writer = new StreamWriter(outputPath, false))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                foreach (var h in headers) csv.WriteField(h);
                csv.NextRecord();

                foreach (var row in cleaned)
                {
                    foreach (var h in headers)
                        csv.WriteField(row[h]);
                    csv.NextRecord();
                }
            }
        }
        catch (IOException)
        {
            // Nếu file đang mở, ghi sang file mới
            finalPath = Path.Combine(Path.GetDirectoryName(outputPath)!,
                Path.GetFileNameWithoutExtension(outputPath) + "_new" + Path.GetExtension(outputPath));

            using (var writer = new StreamWriter(finalPath, false))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                foreach (var h in headers) csv.WriteField(h);
                csv.NextRecord();

                foreach (var row in cleaned)
                {
                    foreach (var h in headers)
                        csv.WriteField(row[h]);
                    csv.NextRecord();
                }
            }

            Console.WriteLine("⚠️ File gốc đang mở — đã lưu ra file mới: " + finalPath);
        }

        Console.WriteLine("\n✅ ĐÃ LÀM SẠCH DỮ LIỆU THÀNH CÔNG!");
        Console.WriteLine("📁 File kết quả: " + finalPath);
    }
}
