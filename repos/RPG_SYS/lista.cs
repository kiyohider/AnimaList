using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RPG_SYS
{
    public partial class lista : Form
    {
        int X = 0;
        int Y = 0;
        string a1, a2, a3;
        public lista()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(panel1_MouseDown);
            this.MouseMove += new MouseEventHandler(panel1_MouseMove);
            a1 = assistir.Text;
            a2 = Assistindo.Text;
            a3 = assistido.Text;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0X2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        DataTable Table = new DataTable();



        private void lista_Load(object sender, EventArgs e)
        {

            Table.Columns.Add("Nome", typeof(string));
            Table.Columns.Add("Numero eps", typeof(int));
            Table.Columns.Add("Numero temps", typeof(int));
            Table.Columns.Add("Visto", typeof(int));
            Table.Columns.Add("Ano", typeof(int));
            Table.Columns.Add("Nota", typeof(int));
            Table.Columns.Add("Status", typeof(string));
            
            dataGridView1.DataSource = Table;


        }

        private void import_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                string[] values;


                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].ToString().Split('|');
                    string[] row = new string[values.Length];

                    for (int j = 0; j < values.Length; j++)
                    {
                        row[j] = values[j].Trim();
                    }
                    Table.Rows.Add(row);
                }
                dataGridView1.DataSource = Table;

            }


        }

        private void exit_Click(object sender, EventArgs e)
        {
            var Menu = new Menu();
            Menu.Show();
            this.Close();
        }

        private void expand_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void export_Click(object sender, EventArgs e)
        {
            TextWriter writer = new StreamWriter(openFileDialog1.FileName);
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (j == dataGridView1.Columns.Count - 1)
                    {
                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                    }
                    else
                    {
                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                }
                writer.WriteLine("");

            }
            writer.Close();
            MessageBox.Show("exported");

        }

        private void add_Click(object sender, EventArgs e)
        {
         
            
            if (assistir.Checked)
            {               
                Table.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text,textBox6.Text, textBox4.Text, textBox5.Text, assistir.Text );   
            }
            else if (Assistindo.Checked)
            {
                Table.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox6.Text, textBox4.Text, textBox5.Text, Assistindo.Text);
            }
            else if (assistido.Checked)
            {
                Table.Rows.Add(textBox1.Text, textBox2.Text, textBox3, textBox6.Text, textBox4.Text, textBox5.Text, assistido.Text);
            }


            dataGridView1.DataSource = Table;

            

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox5.Text = textBox5.Text.Remove(textBox5.Text.Length - 1);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum registro selecionado", "Atenção");
                return;
            }
            else
            {

                
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {         
                DataGridViewRowHeaderCell cell = dataGridView1.Rows[i].HeaderCell;
                cell.Value = (i + 1).ToString();
                dataGridView1.Rows[i].HeaderCell = cell;
                
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (dataGridView1.RowCount >= 2)
            //{

                
            //    if (dataGridView1.Rows[e.RowIndex].Cells[2].ToString() == dataGridView1.Rows[e.RowIndex].Cells[4].ToString())
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Assistido";
                    
            //    }
            //}

            if (e.Value != null && e.Value.Equals(a1))

            {

                dataGridView1.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;

            }
            if (e.Value != null && e.Value.Equals(a2))

            {

                dataGridView1.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Yellow;

            }
            if (e.Value != null && e.Value.Equals(a3))

            {

                dataGridView1.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Green;

            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            X = this.Left - MousePosition.X;
            Y = this.Top - MousePosition.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Left = X + MousePosition.X;
            this.Top = Y + MousePosition.Y;
        }

       
    }
}
