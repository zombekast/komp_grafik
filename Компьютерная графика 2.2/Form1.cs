using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Компьютерная_графика_2._2
{
    public partial class Form1 : Form
    {
        int x0, x1, y0, y1, a, b, c = 0; // Объявляем глобальные переменные
        private List<Point> points = new List<Point>();
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap bm = new Bitmap(8000, 6000);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // При нажатии на кнопку мыши записываются координаты начальной точки
        {
            x0 = e.X;
            y0 = e.Y;
            c = 1;
            //pictureBox.MouseMove += pictureBox_MouseMove;
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (c == 1)
            {
                int x, y;
                Bitmap bm = new Bitmap(8000, 6000);//Создаёт пустой
                this.bm = bm; //Присваивает основному свойства пустого
                pictureBox.Image = this.bm;
                pictureBox.Image = this.bm;
                x = e.X;
                y = e.Y;
                DrawRectangle(x0, y0, x, y);
            }

        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // При отпускании кнопки мыши записывается конечная точки и рисуются необходимые элементы
        {
            Bitmap bm = new Bitmap(8000, 6000);//Создаёт пустой
            this.bm = bm; //Присваивает основному свойства пустого
            pictureBox.Image = this.bm;
            pictureBox.Image = this.bm;
            x1 = e.X;
            y1 = e.Y;
            DrawRectangle(x0, y0, x1, y1); // Функция рисования прямоугольника
            //a = Math.Abs(x0 - x1) / 2;
            //b = Math.Abs(y0 - y1) / 2;
            a = (int)((Math.Abs(x0 + 1 - x1)) / 2.0);
            b = (int)((Math.Abs(y0 + 1 - y1)) / 2.0);
            DrawEllips();
        }
        void pixel4(int x, int y) // Рисование пикселя для первого квадранта, и, симметрично, для остальных
        {
            bm.SetPixel(x + x0 + a, y + y0 + b, Color.Red);
            bm.SetPixel(x + x0 + a, -y + y0 + b, Color.Red);
            bm.SetPixel(-x + x0 + a + 1, -y + y0 + b, Color.Red);
            bm.SetPixel(-x + x0 + a + 1, y + y0 + b, Color.Red);
            pictureBox.Image = bm;
        }
        void DrawEllips()
        {
            {
                pictureBox.MouseMove -= pictureBox_MouseMove;
                int x = 0; // Компонента x
                int y = b; // Компонента y
                int ap = a * a;// a^2, a - большая полуось
                int bp = b * b; // b^2, b - малая полуось
                int delta = 4 * bp * ((x + 1) * (x + 1)) + ap * ((2 * y - 1) * (2 * y - 1)) - 4 * ap * bp; // Функция координат точки (x+1, y-1/2)
                while (ap * (2 * y - 1) > 2 * bp * (x + 1)) // Первая часть дуги
                {
                    pixel4(x, y);
                    if (delta < 0) // Переход по горизонтали
                    {
                        x++;
                        delta += 4 * bp * (2 * x + 3);
                    }
                    else // Переход по диагонали
                    {
                        x++;
                        delta = delta - 8 * ap * (y - 1) + 4 * bp * (2 * x + 3);
                        y--;
                    }
                }
                delta = bp * ((2 * x + 1) * (2 * x + 1)) + 4 * ap * ((y + 1) * (y + 1)) - 4 * ap * bp; // Функция координат точки (x+1/2, y-1)
                while (y + 1 != 0) // Вторая часть дуги, если не выполняется условие первого цикла, значит выполняется a^2(2y - 1) <= 2b^2(x + 1)
                {
                    pixel4(x, y);
                    if (delta < 0) // Переход по вертикали
                    {
                        y--;
                        delta += 4 * ap * (2 * y + 3);
                    }
                    else // Переход по диагонали
                    {
                        y--;
                        delta = delta - 8 * bp * (x + 1) + 4 * ap * (2 * y + 3);
                        x++;
                    }
                }
            }
        }

        private void DrawRectangle(int xa, int ya, int xb, int yb) 
        {
            x1 = Math.Max(xa, xb); // Сортировка x и y 
            x0 = Math.Min(xa, xb);
            y1 = Math.Max(ya, yb);
            y0 = Math.Min(ya, yb);
            for (int x = x0; x <= x1; x++) // Рисование прямых линий по y
            {
                bm.SetPixel(x, y0, Color.Black);
                bm.SetPixel(x, y1, Color.Black);
            }
            for (int y = y0; y <= y1; y++) // Рисование прямых линий по x
            {
                bm.SetPixel(x0, y, Color.Black);
                bm.SetPixel(x1, y, Color.Black);
            }
            pictureBox.Image = bm;
        }

    }
}
