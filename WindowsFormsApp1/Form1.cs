using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        string winDir = System.Environment.GetEnvironmentVariable("windir");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadChart1();
            loadChart2();
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
        }

        DataTable data = new DataTable();

        private void openCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int size = -1;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;

                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }

                data.Clear();
                data.Columns.Clear();

                if (file != "")
                {

                    string delimiter = ",";

                    TextFieldParser tfp = new TextFieldParser(file);

                    tfp.SetDelimiters(delimiter);

                    // Skip The Column Names
                    if (!tfp.EndOfData)
                    {
                        string[] fields = tfp.ReadFields();

                        List<Type> fieldTypes = new List<Type>() { typeof(double), typeof(double), typeof(double),
                        typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double),
                        typeof(double), typeof(double), typeof(double), typeof(string), typeof(string)};

                        for (int i = 0; i < fields.Count(); i++)
                        {
                            data.Columns.Add(fields[i], fieldTypes[i]);
                            System.Diagnostics.Debug.WriteLine(fields[i]);
                        }
                        
                    }

                    while (!tfp.EndOfData)
                        data.Rows.Add(tfp.ReadFields());

                    tfp.Close();

                }

            }

            /*
            foreach (DataRow row in data.Rows)
            {
                row["Hook Load"] = Convert.ToDouble(row["Hook Load"]);
            }
            */

            //Console.WriteLine(size); // <-- Shows file size in debugging mode.
            //Console.WriteLine(result); // <-- For debugging use.

            //System.Diagnostics.Debug.WriteLine(data.Columns["Hook Load"].DataType);

            loadChart1();
            loadChart2();
            dataGridView1.DataSource = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                loadChart2((string)comboBox1.SelectedItem, (string)comboBox2.SelectedItem);
            }
            else
            {
                loadChart2();
            }
        }


        ArrayList PasonArray = new ArrayList();


        private void PopulatePasonArray()
        {
            //fill Pason Array with correct data!!!

            //BHAPasonIdealData.BHASpecificPasonIdealData BHAinstance;

            //PasonArray = BHAinstance.PasonData;
        }

        private void etcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = data;

            List<Type> fieldTypes = new List<Type>() { typeof(double), typeof(double), typeof(double),
                typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double),
                typeof(double), typeof(double), typeof(double), typeof(string), typeof(string)};

            
             //* Unfinished, as PasonRawData fields do not match PasonCU836 CSV column headers
            
            dataTable.Clear();

            foreach (BHAPasonIdealData.PasonRawData pason in PasonArray)
            {
                dataTable.Rows.Add(pason.Bit_Depth, pason.Hole_Depth, pason.RatePenetration, pason.WeightonBit,
                    0, pason.HookLoad, pason.RPM, pason.Torque, pason.StandPipePressure, 0, 0, 0, 0, pason.Flow, pason.Date, pason.Time);
            }


            loadChart1();
            loadChart2();
            dataGridView1.DataSource = data;

            return;
        }
    }
}
