using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Timers;

namespace Японский_кроссворд
{
    public partial class Form1 : Form
    {
        Color Col = Color.Black;
        private int SIZE = 20;//Размер поля
        public bool Er = false;
        Graphics G;
        Rectangle Rect;
        IАnswer[] A;
        bool f;
        private List<List<StatusObject>> mapH = new List<List<StatusObject>>();
        private List<List<StatusObject>> People = new List<List<StatusObject>>();

        public Form1()
        {
            InitializeComponent();
            G = Field.CreateGraphics();
            Rect = new Rectangle(G);
            A = new IАnswer[] { new Victory(), new Loss(), new Error() };
            People_Full();
        }

        private bool Start()
        {
            mapH = new List<List<StatusObject>>();
            List<Automate> automatesHList = getAutomates(panel1, ' ');//список автоматов для строк
            List<Automate> automatesVList = getAutomates(panel2, '\n');//список автоматов для столбцов
            if (!Er)
            //список со списками состояний ячеек
            {
                List<List<StatusObject>> mapV = new List<List<StatusObject>>();//По номеру столбца и строки

                //цикл для заполнения клеток поля
                for (int x = 0; x < SIZE; x++)
                {
                    List<StatusObject> row = new List<StatusObject>();
                    for (int y = 0; y < SIZE; y++)
                        row.Add(new StatusObject().setNone());
                    mapH.Add(row);
                }
                for (int y = 0; y < SIZE; y++)
                {
                    List<StatusObject> coll = new List<StatusObject>();
                    for (int x = 0; x < SIZE; x++)
                        coll.Add(mapH[x][y]);
                    mapV.Add(coll);
                }

                bool notEnd = true;//переменная, которая определяет, есть ли ещё не заполненные ячейки
                while (notEnd)
                {
                    bool changed = false;//переменная, определяющая, изменена ли ячейка
                    notEnd = false;

                    for (int y = 0; y < mapH.Count; y++)
                    {
                        List<StatusObject> row = mapH[y];//Строка с ячейками
                        Automate automateH = automatesHList[y];//Строка с автоматами
                        for (int x = 0; x < mapH[y].Count; x++)
                        {
                            StatusObject cell = row[x];//Ячейка
                            if (!cell.isNone())//если ячейка установлена, то её пропускаем
                                continue;

                            List<StatusObject> coll = mapV[x];//Столбец с ячейками
                            Automate automateV = automatesVList[x];//Столбец с автоматами

                            cell.setWhite();//Устанавливаем ячейку в белый цвет
                            bool isWhite = automateH.IsValid(row) && automateV.IsValid(coll);//Проверяем строку и столбец на корректность
                            cell.setBalck();//Устанавливаем ячейку в черный цвет
                            bool isBlack = automateH.IsValid(row) && automateV.IsValid(coll);//Проверяем строку и столбец на корректность

                            if (isWhite && !isBlack)
                            {
                                cell.setWhite();
                                changed = true;//Изменили яейку
                            }
                            else if (!isWhite && isBlack)
                            {
                                cell.setBalck();
                                changed = true;//Изменили яейку
                            }
                            else if (isWhite)
                            {
                                cell.setNone();
                                notEnd = true;//Есть еще не тронутые ячейки
                            }
                            else
                            {
                                A[2].answer();
                                return false;
                            }
                        }
                        //System.Threading.Thread.Sleep(200);
                        //Вывод_на_экран();
                    }
                    if (notEnd && !changed)//Остались не тронутые ячейки, но за цикл не было произведено изменений
                    {

                        A[1].answer();
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("Некорректный ввод", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private List<Automate> getAutomates(Panel p, Char spliter)
        {
            Er = false;
            List<Automate> automates = new List<Automate>();
            try
            {
                for (int i = p.Controls.Count - 1; i >= 0; i--)
                {
                    String[] text = p.Controls[i].Text.Split(spliter);
                    List<int> row = new List<int>();
                    foreach (String s in text)
                    {
                        if (s.Length != 0)
                            row.Add(int.Parse(s));
                    }
                    automates.Add(new Automate(row, SIZE));//Создаем автомат
                }
                return automates;
            }
            catch
            {
                Er = true;
                return automates;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Bt_Решить_Click_1(object sender, EventArgs e)
        {
            f = Start();
            if (f) A[0].answer(); //interface
            //Вывод_на_экран();
        }

        private void Bt_Открыть_Click_1(object sender, EventArgs e)
        {
            Field.Image = null;
            mapH = new List<List<StatusObject>>();
            string[] file = new string[1];

            for (int i = 0; i < SIZE; i++)
            {
                panel1.Controls[i].Text = "";
                panel2.Controls[i].Text = "";
            }
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    file = File.ReadAllLines(openFileDialog1.FileName);

                    string[] S = file[0].Split();
                    if ((int.Parse(S[0]) <= SIZE) && (int.Parse(S[1]) <= SIZE))
                    {
                        int p = 1;
                        for (int i = 19; i > 19 - int.Parse(S[0]); i--)
                        {
                            panel1.Controls[i].Text = file[p++];
                        }
                        for (int i = 19; i > 19 - int.Parse(S[1]); i--)
                        {
                            string g = file[p++].Replace(" ", " \n");
                            panel2.Controls[i].Text = g;

                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Некорректный файл", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mapH = new List<List<StatusObject>>();
                for (int i = 0; i < SIZE; i++)
                {
                    panel1.Controls[i].Text = "";
                    panel2.Controls[i].Text = "";
                }
            }
        }

        private void Вывод_на_экран()
        {
            int y = 0, x;
            for (int i = 0; i < mapH.Count; i++)
            {
                x = 0;
                for (int j = 0; j < mapH[i].Count; j++)
                {
                    if (mapH[i][j].isWhite())
                    {
                        Rect.Draw(x, y, Color.Black);
                    }
                    else
                        Rect.Draw(x, y, Color.LightBlue);
                    x += Figure.Size_Сell;
                }
                y += Figure.Size_Сell;
            }
        }

        private void Field_MouseUp(object sender, MouseEventArgs e)
        {
            int x = e.X / Figure.Size_Сell;
            int y = e.Y / Figure.Size_Сell;
            if (Col == Color.Black)
            {
                People[y][x].setWhite();
            }
            else
            if (Col == Color.LightBlue)
            {
                People[y][x].setBalck();
            }
            else
                People[y][x].setNone();

            Rect.Draw(x * Figure.Size_Сell, y * Figure.Size_Сell, Col);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {   
            Col = Color.Black;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Col = Color.LightBlue;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Col = Color.Gray;
        }

        private void People_Full()
        {
            for (int x = 0; x < SIZE; x++)
            {
                List<StatusObject> row = new List<StatusObject>();
                for (int y = 0; y < SIZE; y++)
                    row.Add(new StatusObject().setNone());
                People.Add(row);
            }
        }

        private void Bt_Очистить_Click(object sender, EventArgs e)
        {
            People = new List<List<StatusObject>>();
            People_Full();
            Field.Image = Field.BackgroundImage;
        }

        private void Bt_Проверка_Click(object sender, EventArgs e)
        {
            mapH = new List<List<StatusObject>>();
            f = Start();
            bool r = true;
            for (int i = 0; (i < mapH.Count) && (r); i++)
            {
                for (int j = 0; j < mapH[i].Count; j++)
                {
                    if (mapH[i][j].isWhite())
                    {
                        if (!People[i][j].isWhite())
                        {
                            r = false;
                            break;
                        }
                    }
                    if (mapH[i][j].isBlack())
                    {
                        if (People[i][j].isWhite())
                        {
                            r = false;
                            break;
                        }
                    }
                }
            }
            if (r) MessageBox.Show("Кроссворд решен правильно!", "Конец", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                MessageBox.Show("Кроссворд решен не правильно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
          