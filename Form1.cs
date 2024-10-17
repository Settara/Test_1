using System;
using System.Drawing;
using System.Windows.Forms;


namespace Test_1
{
    public partial class MainForm : Form
    {
        private float[,] H;
        private float[,] proection;
        private int cenX;
        private int cenY;
        private Graphics _graphics;

        public MainForm()
            => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cenX = Size.Width / 2;
            cenY = Size.Height / 2;
            SetDefaultPosition();
            //кабинетное проецирование относительно центра правосторонней системы координат
            float[,] p =
            {
                { 1, 0, 0, 0},
                { 0, -1, 0, 0},
                { -(float)(Math.Cos(Math.PI/4))/2, (float)(Math.Cos(Math.PI/4))/2, 0, 0},
                { cenX, cenY, 0, 1}
            };
            proection = p;
            DrawH();
        }

        //умножение матриц
        private float[,] Mult(float[,] X, float[,] Y)
        {
            float[,] result = new float[X.GetLength(0), Y.GetLength(1)];
            for (int i = 0; i < X.GetLength(0); i++)
                for (int j = 0; j < Y.GetLength(1); j++)
                    for (int k = 0; k < Y.GetLength(0); k++)
                        result[i, j] += X[i, k] * Y[k, j];
            return result;
        }

        //отрисовка осей
        private void DrawAxis()
        {
            _graphics = CreateGraphics();
            _graphics.Clear(Color.White);
            float[,] Axis =
            {
                { 0, 0, 0, 1},
                { 500, 0, 0, 1},
                { 0, 400, 0, 1},
                { 0, 0, 500, 1},
                { 490, 5, 0, 1},
                { 490, -5, 0, 1},
                { 5, 390, 0, 1},
                { -5, 390, 0, 1},
                { 12, 0, 495, 1},
                { -10, 0, 480, 1}
            };
            Axis = Mult(Axis, proection);
            #region X
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[1, 0], Axis[1, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[1, 0], Axis[1, 1], Axis[4, 0], Axis[4, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[1, 0], Axis[1, 1], Axis[5, 0], Axis[5, 1]);
            #endregion
            #region Y
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[2, 0], Axis[2, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[2, 0], Axis[2, 1], Axis[6, 0], Axis[6, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[2, 0], Axis[2, 1], Axis[7, 0], Axis[7, 1]);
            #endregion
            #region H
            _graphics.DrawLine(Pens.Gray, Axis[0, 0], Axis[0, 1], Axis[3, 0], Axis[3, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[3, 0], Axis[3, 1], Axis[8, 0], Axis[8, 1]);
            _graphics.DrawLine(Pens.Gray, Axis[3, 0], Axis[3, 1], Axis[9, 0], Axis[9, 1]);
            #endregion
        }

        //начальные значения H
        private void SetDefaultPosition()
        {
            float[,] DefH=
            {
                { 0, 0, 0, 1 },      // A - 0, нижний левый угол
                { 0, 60, 0, 1 },     // B - 1, верхний левый угол
                { 40, 60, 0, 1 },    // C - 2, верхний правый угол
                { 40, 0, 0, 1 },     // D - 3, нижний правый угол
                { 0, 30, 0, 1 },     // E - 4, середина левой вертикальной линии
                { 40, 30, 0, 1 },    // F - 5, середина правой вертикальной линии

                // Трёхмерные точки
                { 0, 0, 10, 1 },     // A' - 6, нижний левый угол (трёхмерная точка)
                { 0, 60, 10, 1 },    // B' - 7, верхний левый угол
                { 40, 60, 10, 1 },   // C' - 8, верхний правый угол
                { 40, 0, 10, 1 },    // D' - 9, нижний правый угол
                { 0, 30, 10, 1 },    // E' - 10, середина левой вертикальной линии
                { 40, 30, 10, 1 }    // F' - 11, середина правой вертикальной линии
            };
            H = DefH;
        }

        //отрисовка проекции буквы
        private void DrawH()
        {
            _graphics = CreateGraphics();
            DrawAxis();
            float[,] matrixDraw = Mult(H, proection);
            
             //рисуем левую вертикальную линию
             _graphics.DrawLine(Pens.Gold, matrixDraw[0, 0], matrixDraw[0, 1], matrixDraw[1, 0], matrixDraw[1, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[6, 0], matrixDraw[6, 1], matrixDraw[7, 0], matrixDraw[7, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[0, 0], matrixDraw[0, 1], matrixDraw[6, 0], matrixDraw[6, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[7, 0], matrixDraw[7, 1], matrixDraw[1, 0], matrixDraw[1, 1]);


             //рисуем правую вертикальную линию
             _graphics.DrawLine(Pens.Gold, matrixDraw[2, 0], matrixDraw[2, 1], matrixDraw[3, 0], matrixDraw[3, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[8, 0], matrixDraw[8, 1], matrixDraw[9, 0], matrixDraw[9, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[2, 0], matrixDraw[2, 1], matrixDraw[8, 0], matrixDraw[8, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[9, 0], matrixDraw[9, 1], matrixDraw[3, 0], matrixDraw[3, 1]);

             //рисуем горизонтальную линию в середине
             _graphics.DrawLine(Pens.Gold, matrixDraw[4, 0], matrixDraw[4, 1], matrixDraw[5, 0], matrixDraw[5, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[10, 0], matrixDraw[10, 1], matrixDraw[11, 0], matrixDraw[11, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[4, 0], matrixDraw[4, 1], matrixDraw[10, 0], matrixDraw[10, 1]);
             _graphics.DrawLine(Pens.Gold, matrixDraw[5, 0], matrixDraw[5, 1], matrixDraw[11, 0], matrixDraw[11, 1]);

        }

        //поместить буквы начального размера в центр системы координат
        private void buttonDeffaultPosition_Click(object sender, EventArgs e)
        {
            SetDefaultPosition();
            DrawH();
        }

        //движение вдоль OX в положительном направлении
        private void MovePlusX_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { toMove, 0, 0, 1}
            };
            H= Mult(H, Move);
            DrawH();
        }

        //движение вдоль OX в отрицательном направлении
        private void MoveMinusX_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { -toMove, 0, 0, 1}
            };
            H= Mult(H, Move);
            DrawH();
        }

        //движение вдоль OY в положительном направлении
        private void MovePlusY_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, toMove, 0, 1}
            };
            H= Mult(H, Move);
            DrawH();
        }

        //движение вдоль OY в отрицательном направлении
        private void MoveMinusY_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, -toMove, 0, 1}
            };
            H= Mult(H, Move);
            DrawH();
        }

        //движение вдоль OHв положительном направлении
        private void MovePlusZ_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, toMove, 1}
            };
            H= Mult(H, Move);
            DrawH();
        }

        //движение вдоль OHв отрицательном направлении
        private void MoveMinusZ_Click(object sender, EventArgs e)
        {
            int toMove = Convert.ToInt32(MoveTextBox.Text);
            float[,] Move =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, -toMove, 1}
            };
            H= Mult(H, Move);
            DrawH();
        }

        //вращение вокруг OX вправо
        private void RotateRightX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { 1, 0, 0, 0},
                { 0, (float)(Math.Cos(angle)), (float)(Math.Sin(angle)), 0},
                { 0, -(float)(Math.Sin(angle)), (float)(Math.Cos(angle)), 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Rotate);
            DrawH();
        }

        //вращение вокруг OX влево
        private void RotateLeftX_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { 1, 0, 0, 0},
                { 0, (float)Math.Cos(angle), -((float)(Math.Sin(angle))), 0},
                { 0, ((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Rotate);
            DrawH();
        }

        //вращение вокруг OY вправо
        private void RotateRightY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), 0, ((float)(Math.Sin(angle))), 0},
                { 0, 1, 0, 0},
                { -((float)(Math.Sin(angle))), 0, ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Rotate);
            DrawH();
        }

        //вращение вокруг OY влево
        private void RotateLeftY_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), 0, -((float)(Math.Sin(angle))), 0},
                { 0, 1, 0, 0},
                { ((float)(Math.Sin(angle))), 0, ((float)(Math.Cos(angle))), 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Rotate);
            DrawH();
        }

        //вращение вокруг OH вправо
        private void RotateRightZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), -((float)(Math.Sin(angle))), 0, 0},
                { ((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Rotate);
            DrawH();
        }

        //вращение вокруг OH влево
        private void RotateLeftZ_Click(object sender, EventArgs e)
        {
            int toRotate = Convert.ToInt32(RotateTextBox.Text);
            //перевод в радианы
            float angle = (float)(toRotate * Math.PI / 180);
            float[,] Rotate =
            {
                { ((float)(Math.Cos(angle))), ((float)(Math.Sin(angle))), 0, 0},
                { -((float)(Math.Sin(angle))), ((float)(Math.Cos(angle))), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Rotate);
            DrawH();
        }

        //отражение относительно плоскости XY
        private void MirrorXY_Click(object sender, EventArgs e)
        {
            float[,] Mirror =
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, -1, 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Mirror);
            DrawH();
        }

        //отражение относительно плоскости XZ
        private void MirrorXZ_Click(object sender, EventArgs e)
        {
            float[,] Mirror =
            {
                { 1, 0, 0, 0},
                { 0, -1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Mirror);
            DrawH();
        }

        //отражение относительно плоскости YZ
        private void MirrorYZ_Click(object sender, EventArgs e)
        {
            float[,] Mirror =
            {
                { -1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Mirror);
            DrawH();
        }

        //растяжение
        private void Stretch_Click(object sender, EventArgs e)
        {
            float[,] Stretch =
            {
                { 2, 0, 0, 0},
                { 0, 2, 0, 0},
                { 0, 0, 2, 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Stretch);
            DrawH();
        }

        //сжатие
        private void Clench_Click(object sender, EventArgs e)
        {
            float[,] Clench =
            {
                { (float)(0.5), 0, 0, 0},
                { 0, (float)(0.5), 0, 0},
                { 0, 0, (float)(0.5), 0},
                { 0, 0, 0, 1}
            };
            H= Mult(H, Clench);
            DrawH();
        }

        //анимация вращения вокруг геометрического центра в одной плоскости с одновременным перемещением вдоль оси OX.
        private void taskOX_Click(object sender, EventArgs e)
        {
            int way = 35;
            int count = 0;
            int coef = 3;
            float angle = (float)(10 * Math.PI / 180);
            float deltaX = 0.5f;  // шаг перемещения вдоль оси OX

            float[,] Dynamic =
            {
                { 1, 0, 0, 0},
                { 0, (float)(Math.Cos(angle)), (float)(Math.Sin(angle)), 0},
                { 0, -(float)(Math.Sin(angle)), (float)(Math.Cos(angle)), 0},
                { 0, 0, 0, 1}
            };

            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler((o, ev) =>
            {
                count++;

                // Перемещение вдоль оси OX
                Dynamic[3, 0] += deltaX;

                // Применение вращения и перемещения
                H= Mult(H, Dynamic);
                DrawH();

                if (count % 3 == 0)
                    timer.Interval += coef;

                if (count == way)
                {
                    Timer t = o as Timer;
                    t.Stop();
                }
            });
            timer.Start();
        }
        //анимация вращения вокруг геометрического центрав одной плоскости с одновременным перемещением вдоль оси OY.
        private void taskOY_Click(object sender, EventArgs e)
        {
            int way = 35;
            int count = 0;
            int coef = 3;
            float angle = (float)(10 * Math.PI / 180);
            float deltaY = 0.5f;  // шаг перемещения вдоль оси OY

            float[,] Dynamic =
            {
                { (float)(Math.Cos(angle)), 0, -(float)(Math.Sin(angle)), 0},
                { 0, 1, 0, 0},
                { (float)(Math.Sin(angle)), 0, (float)(Math.Cos(angle)), 0},
                { 0, 0, 0, 1}
            };

            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler((o, ev) =>
            {
                count++;

                // Перемещение вдоль оси OY
                Dynamic[3, 1] += deltaY;

                // Применение вращения и перемещения
                H= Mult(H, Dynamic);
                DrawH();

                if (count % 3 == 0)
                    timer.Interval += coef;

                if (count == way)
                {
                    Timer t = o as Timer;
                    t.Stop();
                }
            });
            timer.Start();
        }

        //анимация вращения вокруг геометрического центрав одной плоскости с одновременным перемещением вдоль оси OZ.
        private void taskOZ_Click(object sender, EventArgs e)
        {
            int way = 35;
            int count = 0;
            int coef = 3;
            float angle = (float)(10 * Math.PI / 180);
            float deltaZ= 0.5f;  // шаг перемещения вдоль оси OZ

            float[,] Dynamic =
            {
                { (float)(Math.Cos(angle)), (float)(Math.Sin(angle)), 0, 0},
                { -(float)(Math.Sin(angle)), (float)(Math.Cos(angle)), 0, 0},
                { 0, 0, 1, 0},
                { 0, 0, 0, 1}
            };

            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler((o, ev) =>
            {
                count++;

                // Перемещение вдоль оси OZ
                Dynamic[3, 2] += deltaZ;

                // Применение вращения и перемещения
                H= Mult(H, Dynamic);
                DrawH();

                if (count % 3 == 0)
                    timer.Interval += coef;

                if (count == way)
                {
                    Timer t = o as Timer;
                    t.Stop();
                }
            });
            timer.Start();
        }
    }

}
