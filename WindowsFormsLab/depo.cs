using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsLab
{
    public class depo<T> where T : class, Iteplohod
    {/// <summary>
     /// Массив объектов, которые храним
     /// </summary>
        private T[] _places;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int PictureWidth { get; set; }
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int PictureHeight { get; set; }
        /// <summary>
        /// Размер парковочного места (ширина)
        /// </summary>
        private int _placeSizeWidth = 210;
        /// <summary>
        /// Размер парковочного места (высота)
        /// </summary>
        private int _placeSizeHeight = 60;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sizes">Количество мест в депо</param>
        /// <param name="pictureWidth">Рамзер депо - ширина</param>
        /// <param name="pictureHeight">Рамзер депо - высота</param>
        public depo(int sizes, int pictureWidth, int pictureHeight)
        {
            _places = new T[sizes];
            PictureWidth = pictureWidth;
            PictureHeight = pictureHeight;
            for (int i = 0; i < _places.Length; i++)
            {
                _places[i] = null;
            }
        }
        /// <summary>
        /// Перегрузка оператора сложения
        /// Логика действия: в депо добавляется локоматив
        /// </summary>
        /// <param name="d">depo</param>
        /// <param name="teplohod">Добавляемый локоматив</param>
        /// <returns></returns>
        public static int operator +(depo<T> d, T teplohod)
        {
            for (int i = 0; i < d._places.Length; i++)
            {
                if (d.CheckFreePlace(i))
                {
                    d._places[i] = teplohod;
                    d._places[i].SetPosition(5 + i / 5 * d._placeSizeWidth + 5,
                     i % 5 * d._placeSizeHeight + 15, d.PictureWidth,
                    d.PictureHeight);
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: с депо забираем локоматив
        /// </summary>
        /// <param name="d">Депо</param>
        /// <param name="index">Индекс места, с которого пытаемся извлечь объект</param>
        /// <returns></returns>
        public static T operator -(depo<T> d, int index)
        {
            if (index < 0 || index > d._places.Length)
            {
                return null;
            }
            if (!d.CheckFreePlace(index))
            {
                T teplohod = d._places[index];
                d._places[index] = null;
                return teplohod;
            }
            return null;
        }
        /// <summary>
        /// Метод проверки заполнености парковочного места (ячейки массива)
        /// </summary>
        /// <param name="index">Номер парковочного места (порядковый номер в массиве)</param>
        /// <returns></returns>
        private bool CheckFreePlace(int index)
        {
            return _places[index] == null;
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Length; i++)
            {
                if (!CheckFreePlace(i))
                {//если место не пустое
                    _places[i].DrawTransport(g);
                }
            }
        }
        /// <summary>
        /// Метод отрисовки разметки места в депо
        /// </summary>
        /// <param name="g"></param>
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            //границы депо
            g.DrawRectangle(pen, 0, 0, (_places.Length / 5) * _placeSizeWidth, 480);
            for (int i = 0; i < _places.Length / 5; i++)
            {//отрисовываем, по 5 мест на линии
                for (int j = 1; j < 6; ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth, j * _placeSizeHeight + 9, i * _placeSizeWidth + 210, j * _placeSizeHeight + 9);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i * _placeSizeWidth, 309);
            }
        }
    }
}

