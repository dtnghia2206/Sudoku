using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuTest
{
    public partial class Form1 : Form
    {
        Sudoku S;
        const int firstDigit    = 7;
        const int secondDigit   = 8;
        
        public Form1()
        {
            InitializeComponent();
            S = new Sudoku();
            showData();
        }

        private void solveBtn_Click(object sender, EventArgs e)
        {
            // Get the puzzle
            getData();

            // Solve the puzzle
            if (!S.SolveGame())
            {
                System.Windows.Forms.MessageBox.Show("No Available Solution!");
            };

            // Apply the result
            showData();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            var allTextBoxes = this.GetChildControls<TextBox>();
            foreach (TextBox tb in allTextBoxes)
            {
                tb.Text = "";
            };
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            getData();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string text = Path.GetFullPath(saveFileDialog1.FileName);
                string output = "";

                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(text);

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        output += S.arrayData[i, j].ToString();
                    }
                    streamWriter.Write(output);
                    output = "";
                }

                streamWriter.Close();
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            string text = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                text = File.ReadAllText(openFileDialog.FileName);
            }

            char[] position = text.ToCharArray();
            for (int i = 0; i < 81; i++)
            {
                S.arrayData[i / 9, i % 9] = (int)Char.GetNumericValue(position[i]);
            }

            showData();
        }

        private void showData()
        {
            var allTextBoxes = this.GetChildControls<TextBox>();
            foreach (TextBox tb in allTextBoxes)
            {
                char[] position = tb.Name.ToCharArray();
                // textBox01 -> get 0 & 1 -> 0 * 10 + 1 = 1
                int i = (int)Char.GetNumericValue(position[firstDigit]) * 10 + (int)Char.GetNumericValue(position[secondDigit]);   
                // array start with 0 -> i = 10 -> arrayData[1,0] -> first value of the 2nd row
                if (S.arrayData[(i - 1) / 9, (i - 1) % 9].ToString().Equals("0"))
                {
                    tb.Text = "";
                }
                else
                {
                    tb.Text = S.arrayData[(i - 1) / 9, (i - 1) % 9].ToString();
                };
            };
        }
        private void getData()
        {
            string temp = "0";
            var allTextBoxes = this.GetChildControls<TextBox>();
            foreach (TextBox tb in allTextBoxes)
            {
                char[] position = tb.Name.ToCharArray();
                int i = (int)Char.GetNumericValue(position[firstDigit]) * 10 + (int)Char.GetNumericValue(position[secondDigit]);
                if (tb.Text.Equals(""))
                {
                    //Add number 0 to the string to detect empty position
                    S.arrayData[(i - 1) / 9, (i - 1) % 9] = (int)Char.GetNumericValue(temp.ToCharArray()[0]);
                }
                else
                {
                    char[] content = tb.Text.ToCharArray();
                    //Add number on user interface to the array
                    S.arrayData[(i - 1) / 9, (i - 1) % 9] = (int)Char.GetNumericValue(content[0]);
                };
            };
        }
    }
}
