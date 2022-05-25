using System;
using System.Windows.Forms;
using SpreadsheetLight;
using System.Collections.Generic;

namespace ControlerDoorControl
{
    public partial class Inicio : Form
    {
        private readonly List<String> Names = new List<string>();
        private readonly List<String> TagUsers = new List<string>();
        
       
        public Inicio()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            /*******esta seccion funciona para archivos de texto *****/
            //OpenFileDialog openFile = new OpenFileDialog();
            //if(openFile.ShowDialog() == DialogResult.OK)
            //{
            //    openFile.InitialDirectory = "c:\\";
            //    openFile.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            //    openFile.FilterIndex = 2;
            //    openFile.RestoreDirectory = true;
            //    string direccion = openFile.FileName;
            //    using (FileStream stream = File.OpenRead(direccion))
            //    {
            //        int totalBytes = (int)stream.Length;
            //        byte[] bytes = new byte[totalBytes];
            //        int bytesRead = 0;

            //        while (bytesRead < totalBytes)
            //        {
            //            int len = stream.Read(bytes, bytesRead, totalBytes);
            //            bytesRead += len;
            //        }

            //        string text = Encoding.UTF8.GetString(bytes);
            //        lbl_excel_response.Text = text;
            //        Console.WriteLine(text);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No se selecciono archivo");
            //}

            /**************************************************************************/
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    openFile.InitialDirectory = "c:\\";
                    openFile.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                    openFile.FilterIndex = 2;
                    openFile.RestoreDirectory = true;
                    string direccion = openFile.FileName;
                    SLDocument sl = new SLDocument(direccion);
                    var cells = sl.GetCells();
                    int cellsLenght = cells.Count;

                    Console.WriteLine("Celdas");
                    Console.WriteLine(cells.Count);

                    lbl_excel_response.Text = cells.Count.ToString();
                    int valor = 1;
                    Console.WriteLine("Comenzando....");
                    while (valor <= cellsLenght)
                    {
                        if (!string.IsNullOrEmpty(sl.GetCellValueAsString(valor, 1)))
                        {
                            Console.WriteLine(sl.GetCellValueAsString(valor, 1));
                            Console.WriteLine(sl.GetCellValueAsString(valor, 2));
                            Names.Add(sl.GetCellValueAsString(valor, 1));
                            TagUsers.Add(sl.GetCellValueAsString(valor, 2));
                        }
                        valor++;
                    }
                    dataGridView1.Columns.Add("Control", "No. Control");
                    foreach (string item in Names)
                    {

                        dataGridView1.Rows.Add(item);
                    }
                    int iterator = 0;
                    foreach (var item in TagUsers)
                    {
                        dataGridView1.Rows[iterator].Cells["Control"].Value = item;
                        iterator++;
                    }
                }
                else
                {

                }
                dataGridView1.ReadOnly = true;
                TagUsers.AsReadOnly();
                Names.AsReadOnly();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }
    }
}
