using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursovaya.Saper
{

    public partial class Form1 : Form
    {
        Random rnd = new Random();

       
        int[,] Arr = new int[9, 9]; // масив поля гри
        Button[,] arrB = new Button[9, 9]; // масив кнопок гри
        Boolean[,] existOf0 = new bool[9, 9]; // масив для перевірки "ввімкненості" кнопок
        Label label1 = new Label(); // лейбл з повідомленням 
        

        public Form1()
        {
            InitializeComponent();
            NewMap(); // створення карти для гри
                      

            //генерація площі з кнопками 
            button1.MouseDown += (object sender, MouseEventArgs e) => button1_Click(sender, e); // кнопка для встанновлення відміток


            // обробка масива для перевірки "ввімкненості" кнопок
            for (int m = 0; m < 9; m++)
            {
                for (int n = 0; n < 9; n++)
                { existOf0[m, n] = false; }
            }
            
            // відступи для координування кнопок
            int top = 70;
            int left = 30;

            // лейбл з повідомленням
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            label1.Text = "\x263a";
            label1.Size = new System.Drawing.Size(900, 40);
            label1.Font = new Font("Arial", 20, FontStyle.Regular);
            label1.Left = 120;
            label1.Top = 25;
            Controls.Add(label1);


            // генерація кнопок та запам'ятовування їх кординат. Розмальовування та візуалізація карти гри. 
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button button = new Button();
                    button.Height = 20;
                    button.Width = 20;
                    button.TabStop = false;

                    button.Top = top;
                    button.Name = Convert.ToString(i * 10 + j);
                    button.Left = left;

                    Panel panel = new Panel();
                    panel.Height = 20;
                    panel.Width = 20;

                    panel.Top = top;
                    panel.Name = Convert.ToString(i);
                    panel.Left = left;
                    panel.BorderStyle = BorderStyle.FixedSingle;

                    Label label = new Label();
                    label.Text = " ";
                    label.Font = new Font("Arial", 12, FontStyle.Bold);

                    switch (Arr[i,j])
                    {
                        case 0 :
                            break;
                        case 9 :
                            label.Text = "\u25CD";
                            break;
                        case 1:
                            label.Text = Arr[i, j].ToString();
                            label.ForeColor = System.Drawing.Color.Blue;

                            break;
                        case 2:
                            label.Text = Arr[i, j].ToString();
                            label.ForeColor = System.Drawing.Color.Green;
                            break;
                        case 3:
                            label.Text = Arr[i, j].ToString();
                            label.ForeColor = System.Drawing.Color.Red;
                            break;
                        case 4:
                            label.Text = Arr[i, j].ToString();
                            label.ForeColor = System.Drawing.Color.DarkBlue;
                            break;
                        default:
                            label.Text = Arr[i, j].ToString();
                            label.ForeColor = System.Drawing.Color.DarkGreen;
                            break;
                    }
                    label.AutoSize = false;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Dock = DockStyle.Fill;

                    Controls.Add(button);
                    Controls.Add(panel);
                    panel.Controls.Add(label);

                    left += button.Width + 1;

                    arrB[i, j] = button;
                    


                    int index1 = i, index2 = j;
                    arrB[i, j].MouseClick += (object sender, MouseEventArgs e) => ButtonOnClick(sender, e, index1, index2);
                    arrB[i,j].MouseDown += (object sender, MouseEventArgs e) => ButtonOnClickRigtht(sender, e, index1, index2);

                }

                top += 21;
                left = 30;

            }

        }
        // встанновлення міток за натиском правої кнопки миші
        public void ButtonOnClickRigtht(object sender, MouseEventArgs e, int i, int j)
        { if( e.Button == MouseButtons.Right)
            {
                arrB[i,j].BackColor = Color.Red;
            }
        
        }

        // видалення кнопок після натиску лівої кнопки миші 
        public void ButtonOnClick(object sender, MouseEventArgs e, int i, int j)
        {
            ;
            var button = (Button)sender;
            button.Visible = false;
            existOf0[i, j] = true;
            
           
            if (Arr[i, j] == 0)
            {
                CleanMap(i, j);

            }
            if (Arr[i, j] == 9)
            {
                BOOM();
            }
            else
            {
                int counter = 0;
                for (int m = 0; m < 9; m++)
                {
                    for (int n = 0; n < 9; n++)
                    {
                        if (arrB[m, n].Visible == true)
                        {
                            counter++;
                        }
                    }
                }

                if(counter <= 9)
                {
                    label1.Text += " ви виграли!";
                    label1.Font = new Font("Arial", 10, FontStyle.Regular);
                }
            }


            
        }

        // прибирання всіх кнопок при попаданні в бомбу та виведення повідомлення
        public void BOOM()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    arrB[i, j].Visible = false;
                }
            }

            label1.Text = "\x2639 ви програли ";
            label1.Font = new Font("Arial", 10, FontStyle.Regular);


        }

        // створення карти для гри 
        public void NewMap()
        {
            for (int i = 0; i < 9; i++) // матриця поля гри 
            {
                for (int j = 0; j < 9; j++)
                {
                    Arr[i, j] = 0;
                }

            }

            int k = 0, m = 0, n = 0;

            do
            {
                m = rnd.Next(0, 9); // присвоюемо рандомне значення для чисел m та n в діапазоні розміру матриці
                n = rnd.Next(0, 9);

                // перевірка та встановлення номерів 
                if (Arr[m, n] != 9) // перевіряємо чи не співпали координати 
                {
                    Arr[m, n] = 9; // встановлюємо бомбу в матриці
                    if (m - 1 >= 0)
                    {
                        if (Arr[m - 1, n] != 9)
                        {
                            Arr[m - 1, n]++;
                        }

                        if (n - 1 >= 0 && Arr[m - 1, n - 1] != 9)
                        {
                            Arr[m - 1, n - 1]++;
                        }
                        if (n + 1 <= 8 && Arr[m - 1, n + 1] != 9)
                        {
                            Arr[m - 1, n + 1]++;
                        }
                    }
                    if (m + 1 <= 8)
                    {

                        if (Arr[m + 1, n] != 9)
                        {
                            Arr[m + 1, n]++;
                        }



                        if (n + 1 <= 8 && Arr[m + 1, n + 1] != 9)
                        {
                            Arr[m + 1, n + 1]++;
                        }
                        if (n - 1 >= 0 && Arr[m + 1, n - 1] != 9)
                        {
                            Arr[m + 1, n - 1]++;
                        }
                    }


                    if (n - 1 >= 0 && Arr[m, n - 1] != 9)
                    {
                        Arr[m, n - 1]++;
                    }
                    if (n + 1 <= 8 && Arr[m, n + 1] != 9)
                    {
                        Arr[m, n + 1]++;

                    }



                    k++;
                }

            } while (k < 10);
        }

        // перезапуск гри 
        private void button1_Click(object sender, MouseEventArgs e)
        {
            Application.Restart();
        }

        //зачищення карти кнопками, що приховують послідовне розташування нулів
        public void CleanMap(int i, int j)
        {


            if (Arr[i++, j] == 0 && i >= 0 && i < 9)
            {
                arrB[i, j].Visible = false;

                ExistOfZ(i, j);

            }
            i--;
            if (Arr[i--, j] == 0 && i >= 0 && i < 9)
            {
                arrB[i, j].Visible = false;
                ExistOfZ(i, j);

            }
            i++;
            if (Arr[i, j++] == 0 && j < 9 && j >= 0)
            {
                arrB[i, j].Visible = false;
                ExistOfZ(i, j);

            }
            j--;
            if (Arr[i, j--] == 0 && j >= 0 && j < 9)
            {
                arrB[i, j].Visible = false;
                ExistOfZ(i, j);

            }
            j++;


        }

        // знаходження вже перевірених нулів 
        public void ExistOfZ(int i, int j)
        {
            if (existOf0[i, j] == false)
            {
                
                existOf0[i, j] = true;
                arrB[i, j].Visible = false;
                CleanMap(i, j);

            }
        }

    }
}
