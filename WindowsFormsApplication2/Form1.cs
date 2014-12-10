using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        TextBox[] cells = new TextBox[16];
        Panel[] panels = new Panel[4];
        static Random r = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void text_changed(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Panel panel = textBox.Parent as Panel;

            if (textBox.Text != "")
            {
                rowColCheck(textBox);
                areaCheck(textBox, panel);
            }
        }

        public void drawTable(int tableSize)
        {
            int sayi = 0;
            int tsayi = 0;
            int size = Convert.ToInt32(Math.Sqrt(tableSize));

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    panels[sayi] = new Panel();
                    panels[sayi].Name = "panel" + (sayi + 1);
                    panels[sayi].Location = new System.Drawing.Point(j * size * 55 + 140, i * size * 55 + 20);
                    panels[sayi].Size = new System.Drawing.Size(size * 50, size * 50);
                    panels[sayi].BackColor = Color.RoyalBlue;

                    for (int ti = 0; ti < size; ti++)
                    {
                        for (int tj = 0; tj < size; tj++)
                        {
                            cells[tsayi] = new TextBox();
                            cells[tsayi].Name = "txt" + tsayi;
                            cells[tsayi].Width = 50;
                            cells[tsayi].Font = new System.Drawing.Font("Microsoft Sans Serif", 22F);
                            cells[tsayi].Location = new Point(tj * 50, ti * 50);
                            cells[tsayi].TextAlign = HorizontalAlignment.Center;
                            cells[tsayi].TextChanged += new EventHandler(text_changed);

                            panels[sayi].Controls.Add(cells[tsayi]);
                            tsayi++;
                        }
                    }

                    this.Controls.Add(panels[sayi]);
                    sayi++;

                }

            }

            randomData(tableSize);
        }

        // TO-DO having issue with creating random data.
        public void randomData(int tableSize)
        {
            int total = tableSize * tableSize;
            int count = 0;

            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    int rand1 = r.Next(0, total);
                    int rand2 = r.Next(1, tableSize + 1);

                    if (cells[rand1].Text == "")
                    {
                        if (count <= tableSize)
                        {
                            cells[rand1].Text = rand2.ToString();
                            cells[rand1].Enabled = false;
                        }
                    }
                count++;
                }
            }

        }

        public void clearTable()
        {
            foreach (Panel panel in panels)
            {
                // not sure if it is necessary
                foreach (TextBox cell in cells)
                {
                    this.Controls.Remove(cell);
                    //cell.Dispose();
                }
                this.Controls.Remove(panel);
            }

            cells = new TextBox[] { };
            panels = new Panel[] { };
        }

        public void areaCheck(TextBox changedCell, Panel changedCellParent)
        {
            foreach (TextBox cell in changedCellParent.Controls)
            {
                if (cell.Name != changedCell.Name)
                {
                    //string valui = txtElem.Name;
                    //Console.WriteLine(valui);

                    if (cell.Text == changedCell.Text)
                    {
                        cell.BackColor = Color.Purple;
                    }
                }
            }
        }

        public void rowColCheck(TextBox changedCell)
        {
            // initialize
            // cc stands for ChangedCell

            string ccVal = changedCell.Text;
            Panel ccPanel = changedCell.Parent as Panel;

            int ccParentX = ccPanel.Location.X;
            int ccX = changedCell.Location.X;
            int ccRow = ccParentX + ccX;

            int ccParentY = ccPanel.Location.Y;
            int ccY = changedCell.Location.Y;
            int ccCol = ccParentY + ccY;

            foreach (TextBox cell in cells)
            {
                if (cell.Name != changedCell.Name)
                {
                    Panel panel = cell.Parent as Panel;

                    int parentX = panel.Location.X;
                    int selfX = cell.Location.X;
                    int row = parentX + selfX;

                    int parentY = panel.Location.Y;
                    int selfY = cell.Location.Y;
                    int col = parentY + selfY;

                    string self = cell.Text;

                    if (ccVal == self)
                    {
                        if (ccCol == col)
                        {
                            cell.BackColor = Color.Red;
                        }

                        if (ccRow == row)
                        {
                            cell.BackColor = Color.Pink;
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearTable();

            Console.WriteLine("selected" + comboBox1.SelectedItem.GetType());
            string choice = comboBox1.SelectedItem.ToString();
            int boardSize = int.Parse(choice.Substring(0, 1));

            // set form size
            this.Size = new System.Drawing.Size(boardSize * 55 + 180, boardSize * 55 + 80);
            panel1.Height = this.Height;

            cells = new TextBox[boardSize * boardSize];
            panels = new Panel[boardSize];

            drawTable(boardSize);

        }
 

        //public void horizontalCheck(int row, int col, int area, int num, string name)
        //{
        //    foreach (TextBox neme in txt)
        //    {
        //        if (neme.Name != name)
        //        {
        //            int rowa = int.Parse(neme.Name.Substring(3, 1));
        //            int cola = int.Parse(neme.Name.Substring(4, 1));
        //            int areaa = row + col;
        //            int numa;

        //            if (neme.Text != "")
        //            {
        //                numa = int.Parse(neme.Text);
        //            }
        //            else
        //            {
        //                continue;
        //            }

        //            if (rowa == row && numa == num)
        //            {
        //                neme.BackColor = Color.Red;
        //            }

        //            if (cola == col && numa == num)
        //            {
        //                neme.BackColor = Color.RoyalBlue;
        //            }

        //            areaa = areaa / 4;
        //            area = area / 4;

        //            MessageBox.Show(areaa + " " + area);

        //            double num1 = Math.Round(2.1, 0);
        //            double num2 = Math.Round(2.3, 0);

        //            if (num1 == num2)
        //            {
        //                neme.BackColor = Color.SaddleBrown;
        //            }
        //        }
        //    }
        //}
    }
}
