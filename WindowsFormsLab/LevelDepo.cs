using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLab
{
    public class LevelDepo
    {
        /// <summary>
        /// Список с уровнями депо
        /// </summary>
        List<depo<Iteplohod>> deposStages;
        /// <summary>
        /// Сколько мест на каждом уровне
        /// </summary>
        private const int countPlaces = 20;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countStages">Количество уровеней депо</param>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        public LevelDepo(int countStages, int pictureWidth, int pictureHeight)
        {
            deposStages = new List<depo<Iteplohod>>();
            for (int i = 0; i < countStages; ++i)
            {
                deposStages.Add(new depo<Iteplohod>(countPlaces, pictureWidth, pictureHeight));
            }
        }
        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public depo<Iteplohod> this[int ind]
        {
            get
            {
                if (ind > -1 && ind < deposStages.Count)
                {
                    return deposStages[ind];
                }
                return null;
            }
        }
    }
}