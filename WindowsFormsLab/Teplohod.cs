using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsLab
{
    class Teplohod
    {
       
        /// <summary>
        /// Левая координата отрисовки теплохода 
        /// </summary>
        private float _startPosX;
        /// <summary>
        /// Правая кооридната отрисовки теплохода
        /// </summary>
        private float _startPosY;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int _pictureWidth;
        //Высота окна отрисовки
        private int _pictureHeight;
        /// <summary>
        /// Ширина отрисовки теплохода
        /// </summary>
        private const int TeplohodWidth = 100;
        /// <summary>
        /// Высота отрисовки теплохода
        /// </summary>
        private const int TeplohodHeight = 60;
        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public int MaxSpeed { private set; get; }
        /// <summary>
        /// Вес теплохода
        /// </summary>
        public float Weight { private set; get; }
        /// <summary>
        /// Основной цвет вагона
        /// </summary>
        public Color MainColor { private set; get; }
        /// <summary>
        /// Дополнительный цвет
        /// </summary>
        public Color DopColor { private set; get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес теплохода</param>
        /// <param name="mainColor">Основной цвет вагона</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        public Teplohod(int maxSpeed, float weight, Color mainColor, Color dopColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;

        }
        /// <summary>
        /// Установка позиции теплохода
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина картинки</param>
        /// <param name="height">Высота картинки</param>
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        /// <summary>
        /// Изменение направления пермещения
        /// </summary>
        /// <param name="direction">Направление</param>
        public void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - TeplohodWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                //влево
                case Direction.Left:
                    if (_startPosX - step > 0)
                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - TeplohodHeight)
                    {
                        _startPosY += step;
                    }
                    break;

            }
        }
        public void DrawTeplohod(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            // теперь отрисуем основной кузов 
            //границы 
            g.DrawEllipse(pen, _startPosX, _startPosY, 20, 20);
            g.DrawEllipse(pen, _startPosX, _startPosY + 30, 20, 20);
            g.DrawEllipse(pen, _startPosX + 70, _startPosY, 20, 20);
            g.DrawEllipse(pen, _startPosX + 70, _startPosY + 30, 20, 20);
            g.DrawEllipse(pen, _startPosX + 23, _startPosY + 30, 20, 20);
            g.DrawEllipse(pen, _startPosX + 46, _startPosY + 30, 20, 20);
            g.DrawRectangle(pen, _startPosX - 1, _startPosY + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX + 80, _startPosY + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX + 65, _startPosY - 10, 8, 10);
            g.DrawRectangle(pen, _startPosX - 10, _startPosY + 30, 110, 8);
            g.DrawRectangle(pen, _startPosX + 10, _startPosY - 1, 70, 52);
            //g.DrawLine(pen, _startPosX + 69, _startPosY - 10, 4, 15);
            //Знаки предупреждения
            Brush brYellow = new SolidBrush(Color.Yellow);
            g.FillEllipse(brYellow, _startPosX, _startPosY, 20, 20);
            g.FillEllipse(brYellow, _startPosX + 70, _startPosY, 20, 20);
            //Вагон
            Brush br = new SolidBrush(MainColor);
            g.FillRectangle(br, _startPosX, _startPosY + 10, 10, 30);
            g.FillRectangle(br, _startPosX + 80, _startPosY + 10, 10, 30);
            g.FillRectangle(br, _startPosX + 10, _startPosY, 70, 40);
            //Колеса
            Brush brBlack = new SolidBrush(Color.Black);
            g.FillRectangle(brBlack, _startPosX + 65, _startPosY - 10, 8, 10);
            g.FillRectangle(brBlack, _startPosX - 10, _startPosY + 30, 110, 8);
            g.FillEllipse(brBlack, _startPosX + 70, _startPosY + 30, 20, 20);
            g.FillEllipse(brBlack, _startPosX + 46, _startPosY + 30, 20, 20);
            g.FillEllipse(brBlack, _startPosX + 23, _startPosY + 30, 20, 20);
            g.FillEllipse(brBlack, _startPosX, _startPosY + 30, 20, 20);
            //Мостик
            br = new SolidBrush(DopColor);
            g.FillRectangle(br, _startPosX, _startPosY + 30, 90, 8);
        }
    }
}



