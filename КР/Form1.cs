using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace КР
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Очищаем предыдущий результат
            richTextBox1.Clear();

            // Получаем текст из текстового поля
            string searchTerm = textBox1.Text.Trim();

            // Загружаем XML файл
            XDocument doc = XDocument.Load("XMLFile1.xml");

            // Ищем соответствующий элемент в XML файле
            var results = doc.Descendants("material")
                             .Where(m => m.Element("name").Value.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();

            // Если есть результаты, отображаем информацию
            if (results.Any())
            {
                foreach (var result in results)
                {
                    string availability = result.Element("availability").Value;
                    string manufacturer = result.Element("manufacturer").Value;
                    string price = result.Element("price").Value;
                    string currency = result.Element("currency").Value;
                    string supply = result.Element("supply")?.Value ?? "700 кг"; // Проверяем наличие информации о поставке
                    string release = result.Element("release")?.Value ?? "373 кг"; // Проверяем наличие информации об отпуске

                    richTextBox1.AppendText($"Название: {searchTerm}\nНаличие: {availability}\nПроизводитель: {manufacturer}\nЦена: {price} {currency}\nПоставлено на склад: {supply}\nОтпущено со склада: {release}\n\n");
                }
            }
            else
            {
                // Если ничего не найдено, выводим сообщение
                richTextBox1.AppendText("Ничего не найдено.");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("XMLFile1.xml");

            if (checkBox1.Checked)
            {
                var materials = from material in doc.Descendants("material")
                                orderby int.Parse(material.Element("price").Value)
                                select new
                                {
                                    Name = material.Element("name").Value,
                                    Price = material.Element("price").Value + " " + material.Element("currency").Value
                                };

                string output = "";
                foreach (var material in materials)
                {
                    output += material.Name + "\n" + material.Price + "\n\n";
                }

                richTextBox1.Text = output;
            }
            else
            {
                richTextBox1.Clear();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("XMLFile1.xml");

            if (checkBox2.Checked)
            {
                var materials = from material in doc.Descendants("material")
                                orderby int.Parse(material.Element("price").Value) descending
                                select new
                                {
                                    Name = material.Element("name").Value,
                                    Price = material.Element("price").Value + " " + material.Element("currency").Value
                                };

                string output = "";
                foreach (var material in materials)
                {
                    output += material.Name + "\n" + material.Price + "\n\n";
                }

                richTextBox1.Text = output;
            }
            else
            {
                richTextBox1.Clear();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("XMLFile1.xml");

            if (checkBox3.Checked)
            {
                var materials = from material in doc.Descendants("material")
                                select new
                                {
                                    Name = material.Element("name").Value,
                                    Price = material.Element("price").Value + " " + material.Element("currency").Value,
                                    Info = material.Element("manufacturer").Value
                                };

                string output = "";
                foreach (var material in materials)
                {
                    output += material.Name + "\n" + material.Price + "\n" + material.Info + "\n\n";
                }

                richTextBox1.Text = output;
            }
            else
            {
                richTextBox1.Clear();
            }
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("XMLFile1.xml");

            List<XElement> materials = doc.Descendants("material").ToList();
            QuickSort(materials, 0, materials.Count - 1);

            string output = "";
            foreach (var material in materials)
            {
                output += material.Element("name").Value + "\n" +
                          material.Element("price").Value + " " +
                          material.Element("currency").Value + "\n\n";
            }

            richTextBox1.Text = output;
        }

        private void QuickSort(List<XElement> arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);

                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        private int Partition(List<XElement> arr, int low, int high)
        {
            string pivot = arr[high].Element("price").Value;

            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (int.Parse(arr[j].Element("price").Value) < int.Parse(pivot))
                {
                    i++;

                    XElement temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            XElement temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("XMLFile1.xml");

            List<XElement> materials = doc.Descendants("material").ToList();
            QuickSortDescending(materials, 0, materials.Count - 1);

            string output = "";
            foreach (var material in materials)
            {
                output += material.Element("name").Value + "\n" +
                          material.Element("price").Value + " " +
                          material.Element("currency").Value + "\n\n";
            }

            richTextBox1.Text = output;
        }

        private void QuickSortDescending(List<XElement> arr, int low, int high)
        {
            if (low < high)
            {
                int pi = PartitionDescending(arr, low, high);

                QuickSortDescending(arr, low, pi - 1);
                QuickSortDescending(arr, pi + 1, high);
            }
        }

        private int PartitionDescending(List<XElement> arr, int low, int high)
        {
            string pivot = arr[high].Element("price").Value;

            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (int.Parse(arr[j].Element("price").Value) > int.Parse(pivot))
                {
                    i++;

                    XElement temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            XElement temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("XMLFile1.xml");

            List<XElement> materials = doc.Descendants("material").ToList();
            QuickSortByName(materials, 0, materials.Count - 1);

            string output = "";
            foreach (var material in materials)
            {
                output += material.Element("name").Value + "\n" +
                          material.Element("price").Value + " " +
                          material.Element("currency").Value + "\n" +
                          material.Element("manufacturer").Value + "\n\n";
            }

            richTextBox1.Text = output;
        }

        private void QuickSortByName(List<XElement> arr, int low, int high)
        {
            if (low < high)
            {
                int pi = PartitionByName(arr, low, high);

                QuickSortByName(arr, low, pi - 1);
                QuickSortByName(arr, pi + 1, high);
            }
        }

        private int PartitionByName(List<XElement> arr, int low, int high)
        {
            string pivot = arr[high].Element("name").Value;

            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (String.Compare(arr[j].Element("name").Value, pivot) < 0)
                {
                    i++;

                    XElement temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            XElement temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }
    }
    }
    
        
    
