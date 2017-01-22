using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace LilPartsMaker
{
    public partial class Form1 : Form
    {
        WebsiteMaker wm;
        private string csvFile;
        private string xlsFile;
        private string saveLocation;
        public Form1()
        {
            InitializeComponent();
        }

        private void generateButtonPress(object sender, EventArgs e)//generate button
        {
            if ((xlsFile == null || xlsFile.Equals("")) && (csvFile == null || csvFile.Equals("")) || saveLocation == null || saveLocation.Equals(""))
            {
                MessageBox.Show("The csv/xls file has not been selected or the save location hasn't been selected,");
                return;
            }
            if (xlsFile == null || xlsFile.Equals(""))
            {
                wm = new WebsiteMaker(csvFile, saveLocation, false);
            }
            else
            {
                wm = new WebsiteMaker(xlsFile, saveLocation, true);//xls overrides csv
            }

            if (wm.init() != 1)
            {
                MessageBox.Show("Website failed to generate");
                return;
            }
            wm.makeCarAndCategoryPages();
            wm.makeFlatCategoryPages();
            wm.makeIndividualPages();
            wm.makeAllPage();
            wm.makeSqlCode();
            MessageBox.Show("Website successfully generated");
        }

        private void browseCSV(object sender, EventArgs e)//browseCsv
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV Files (.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                csvFile = openFileDialog1.FileName;
                tb_csvFile.Text = csvFile;
            }

        }

        private void tb_csvFile_TextChanged(object sender, EventArgs e)
        {
            csvFile = tb_csvFile.Text;
        }


        private void onXLSTextChanged(object sender, EventArgs e)//xlstextBox
        {
            xlsFile = textBox1.Text;
        }

        private void browseXLS(object sender, EventArgs e)//xls browse
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XLS Files (.xls)|*.xls|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                xlsFile = openFileDialog.FileName;
                textBox1.Text = xlsFile;
            }
        }

        private void browseSaveLocation(object sender, EventArgs e)//save location
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                saveLocation = fbd.SelectedPath;
                tb_saveLocation.Text = saveLocation;
            }
        }

        private void tb_saveLocation_TextChanged(object sender, EventArgs e)//save location
        {
            saveLocation = tb_saveLocation.Text;
        }
    }
}
