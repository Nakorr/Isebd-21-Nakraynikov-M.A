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
        private Dictionary<int, T> _places;
        /// <summary>
        /// Максимальное количество мест на парковке
        /// </summary>
        private int _maxCount;
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
            _maxCount = sizes;
            _places = new Dictionary<int, T>();
            PictureWidth = pictureWidth;
            PictureHeight = pictureHeight;
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
            if (d._places.Count == d._maxCount)
            {
                throw new depoOverflowException();
            }
            for (int i = 0; i < d._maxCount; i++)
            {
                if (d.CheckFreePlace(i))
                {
                    d._places.Add(i,teplohod);
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
            if (!d.CheckFreePlace(index))
            {
                T teplohod = d._places[index];
                d._places.Remove(index);
                return teplohod;
            }
            throw new depoNotFoundException(index);
        }
        /// <summary>
        /// Метод проверки заполнености парковочного места (ячейки массива)
        /// </summary>
        /// <param name="index">Номер парковочного места (порядковый номер в массиве)</param>
        /// <returns></returns>
        private bool CheckFreePlace(int index)
        {
            return !_places.ContainsKey(index);
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            var keys = _places.Keys.ToList();
            for (int i = 0; i < _places.Count; i++)
            {
                _places[keys[i]].DrawTransport(g);
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
            g.DrawRectangle(pen, 0, 0, (_maxCount / 5) * _placeSizeWidth, 480);
            for (int i = 0; i < _maxCount / 5; i++)
            {//отрисовываем, по 5 мест на линии
                for (int j = 1; j < 6; ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth, j * _placeSizeHeight + 9, i * _placeSizeWidth + 210, j * _placeSizeHeight + 9);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i * _placeSizeWidth, 309);
            }
        }
        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public T this[int ind]
        {
            get
            {
                if (_places.ContainsKey(ind))
                {
                    return _places[ind];
                }
                throw new depoNotFoundException(ind);
            }
            set
            {
                if (CheckFreePlace(ind))
                {
                    _places.Add(ind, value);
                    _places[ind].SetPosition(5 + ind / 5 * _placeSizeWidth + 5, ind % 5 *
                   _placeSizeHeight + 15, PictureWidth, PictureHeight);
                }
                else
                {
                    throw new depoOccupiedPlaceException(ind);
                }
            }
        }
    }
}

