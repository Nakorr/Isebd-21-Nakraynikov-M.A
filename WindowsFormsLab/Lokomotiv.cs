using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsLab
{
    public class Lokomotiv : Transport
    {
        /// <summary>
        /// Ширина отрисовки 
        /// </summary>
        protected const int tepWidth = 100;
        /// <summary>
        /// Высота отрисовки 
        /// </summary>
        protected const int tepHeight = 60;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес автомобиля</param>
        /// <param name="mainColor">Основной цвет кузова</param>
        public Lokomotiv(int maxSpeed, float weight, Color mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="info">Информация по объекту</param>
        public Lokomotiv(string info)
        {
            string[] strs = info.Split(';');
            if (strs.Length == 3)
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
            }
        }
        public override void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - tepWidth)
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
                    if (_startPosY + step < _pictureHeight - tepHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        public override void DrawTransport(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            //границы 
            g.DrawEllipse(pen, _startPosX, _startPosY, 20, 20);
            g.DrawEllipse(pen, _startPosX, _startPosY + 30, 20, 20);
            g.DrawEllipse(pen, _startPosX + 70, _startPosY, 20, 20);
            g.DrawEllipse(pen, _startPosX + 70, _startPosY + 30, 20, 20);
            g.DrawEllipse(pen, _startPosX + 23, _startPosY + 30, 20, 20);
            g.DrawEllipse(pen, _startPosX + 46, _startPosY + 30, 20, 20);
            g.DrawRectangle(pen, _startPosX - 1, _startPosY + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX + 80, _startPosY + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX - 10, _startPosY + 30, 110, 8);
            g.DrawRectangle(pen, _startPosX + 10, _startPosY - 1, 70, 52);
            //Вагон
            Brush br = new SolidBrush(MainColor);
            g.FillRectangle(br, _startPosX, _startPosY + 10, 10, 30);
            g.FillRectangle(br, _startPosX + 80, _startPosY + 10, 10, 30);
            g.FillRectangle(br, _startPosX + 10, _startPosY, 70, 40);
            //Колеса
            Brush brBlack = new SolidBrush(Color.Black);
            g.FillRectangle(brBlack, _startPosX - 10, _startPosY + 30, 110, 8);
            g.FillEllipse(brBlack, _startPosX + 70, _startPosY + 30, 20, 20);
            g.FillEllipse(brBlack, _startPosX + 46, _startPosY + 30, 20, 20);
            g.FillEllipse(brBlack, _startPosX + 23, _startPosY + 30, 20, 20);
            g.FillEllipse(brBlack, _startPosX, _startPosY + 30, 20, 20);
        }
        public override string ToString()
        {
            return MaxSpeed + ";" + Weight + ";" + MainColor.Name;
        }
    }
}