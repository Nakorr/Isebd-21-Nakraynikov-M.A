using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsLab
{
    public class depo<T> : IEnumerator<T>, IEnumerable<T>, IComparable<depo<T>> where T : class, Iteplohod
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
        /// Текущий элемент для вывода через IEnumerator (будет обращаться по своему индексу к ключу словаря, по которму будет возвращаться запись)
        /// </summary>
        private int _currentIndex;
        public int GetKey
        {
            get
            {
                return _places.Keys.ToList()[_currentIndex];
            }
        }
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
            _currentIndex = -1;
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
            if (d._places.ContainsValue(teplohod))
            {
                throw new depoAlreadyHaveException();
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
            foreach (var tep in _places)
            {
                tep.Value.DrawTransport(g);
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
        /// <summary>
        /// Метод интерфейса IEnumerator для получения текущего элемента
        /// </summary>
        public T Current
        {
            get
            {
                return _places[_places.Keys.ToList()[_currentIndex]];
            }
        }
        /// <summary>
        /// Метод интерфейса IEnumerator для получения текущего элемента
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        /// <summary>
        /// Метод интерфейса IEnumerator, вызываемый при удалении объекта
        /// </summary>
        public void Dispose()
        {
            _places.Clear();
        }
        /// <summary>
        /// Метод интерфейса IEnumerator для перехода к следующему элементу или началу коллекции
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (_currentIndex + 1 >= _places.Count)
            {
                Reset();
                return false;
            }
            _currentIndex++;
            return true;
        }
        /// <summary>
        /// Метод интерфейса IEnumerator для сброса и возврата к началу коллекции
        /// </summary>
        public void Reset()
        {
            _currentIndex = -1;
        }
        /// <summary>
        /// Метод интерфейса IEnumerable
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }
        /// <summary>
        /// Метод интерфейса IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Метод интерфейса IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(depo<T> other)
        {
            if (_places.Count > other._places.Count)
            {
                return -1;
            }
            else if (_places.Count < other._places.Count)
            {
                return 1;
            }
            else if (_places.Count > 0)
            {
                var thisKeys = _places.Keys.ToList();
                var otherKeys = other._places.Keys.ToList();
                for (int i = 0; i < _places.Count; ++i)
                {
                    if (_places[thisKeys[i]] is Lokomotiv && other._places[thisKeys[i]] is
                   LokomotivTep)
                    {
                        return 1;
                    }
                    if (_places[thisKeys[i]] is LokomotivTep && other._places[thisKeys[i]] is
                    Lokomotiv)
                    {
                        return -1;
                    }
                    if (_places[thisKeys[i]] is Lokomotiv && other._places[thisKeys[i]] is Lokomotiv)
                    {
                        return (_places[thisKeys[i]] is
                       Lokomotiv).CompareTo(other._places[thisKeys[i]] is Lokomotiv);
                    }
                    if (_places[thisKeys[i]] is LokomotivTep && other._places[thisKeys[i]] is
                    LokomotivTep)
                    {
                        return (_places[thisKeys[i]] is
                       LokomotivTep).CompareTo(other._places[thisKeys[i]] is LokomotivTep);
                    }
                }
            }
            return 0;
        }
    }
}

