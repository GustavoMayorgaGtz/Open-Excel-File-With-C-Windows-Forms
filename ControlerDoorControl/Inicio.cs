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
                Names.Clear();
                TagUsers.Clear();
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

                    int valor = 1;
                    while (valor <= cellsLenght)
                    {
                        if (!string.IsNullOrEmpty(sl.GetCellValueAsString(valor, 1)))
                        {
                            Names.Add(sl.GetCellValueAsString(valor, 1));
                            TagUsers.Add(sl.GetCellValueAsString(valor, 2));
                        }
                        valor++;
                    }
                    /******Check if Names and TagUsers Lists contains a correctly data ******/
                    if (cellsLenght > 0 && (Names[0].Contains("Alumno") || Names[0].Contains("Nombre")) && (TagUsers[0].Contains("Numero")|| TagUsers[0].Contains("Control")))
                    {
                        MessageBox.Show("Formato Correcto");
                        setValue.Enabled = true;
                        setValue.Visible = Visible;
                    }
                    else
                    {
                        MessageBox.Show("No existe la tabla Alumno o la tabla Control || El formato no es correcto");
                        setValue.Enabled = false;
                        setValue.Visible =  false;
                    }
                    /*******insert column and data in dataGridView1**********/
                    int countColumn = dataGridView1.Columns.Count;
                  
                    if (countColumn == 2)
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("Nombres_Registrados", "Nombre");
                        dataGridView1.Columns.Add("Control", "No. Control");
                        DataGridViewColumn column1 = dataGridView1.Columns[0];
                        column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        DataGridViewColumn column2 = dataGridView1.Columns[1];
                        column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if (countColumn == 0)
                    {
                        dataGridView1.Columns.Add("Nombres_Registrados", "Nombre");
                        dataGridView1.Columns.Add("Control", "No. Control");
                        DataGridViewColumn column1 = dataGridView1.Columns[0];
                        column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        DataGridViewColumn column2 = dataGridView1.Columns[1];
                        column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if (countColumn == 1)
                    {
                        dataGridView1.Columns.Add("Control", "No. Control");
                        DataGridViewColumn column2 = dataGridView1.Columns[1];
                        column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                   
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

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayuda ayuda = new Ayuda();
            ayuda.Show();
            this.Close();
        }

        private void sintaxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sintaxis sintaxis = new Sintaxis();
            sintaxis.Show();
            this.Close();

        }
      
    }
}
