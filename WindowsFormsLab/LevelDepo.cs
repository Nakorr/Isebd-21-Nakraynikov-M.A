using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        /// Ширина окна отрисовки
        /// </summary>
        private int pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int pictureHeight;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countStages">Количество уровеней депо</param>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        public LevelDepo(int countStages, int pictureWidth, int pictureHeight)
        {
            deposStages = new List<depo<Iteplohod>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
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
        /// <summary>
        /// Сохранение информации по вагонам на депо в файл
        /// </summary>
        /// <param name="filename">Путь и имя файла</param>
        /// <returns></returns>
        public bool SaveData(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    //Записываем количество уровней
                    WriteToFile("CountLeveles:" + deposStages.Count +
                   Environment.NewLine, fs);
                    foreach (var level in deposStages)
                    {
                        //Начинаем уровень
                        WriteToFile("Level" + Environment.NewLine, fs);
                        for (int i = 0; i < countPlaces; i++)
                        {
                            var tep = level[i];
                            if (tep != null)
                            {
                                //если место не пустое
                                //Записываем тип вагона
                                if (tep.GetType().Name == "Lokomotiv")
                                {
                                    WriteToFile(i + ":Lokomotiv:", fs);
                                }
                                if (tep.GetType().Name == "LokomotivTep")
                                {
                                    WriteToFile(i + ":LokomotivTep:", fs);
                                }
                                //Записываемые параметры
                                WriteToFile(tep + Environment.NewLine, fs);
                            }
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Метод записи информации в файл
        /// </summary>
        /// <param name="text">Строка, которую следует записать</param>
        /// <param name="stream">Поток для записи</param>
        private void WriteToFile(string text, FileStream stream)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            stream.Write(info, 0, info.Length);
        }
        /// <summary>
        /// Загрузка информации по вагонам на депо из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool LoadData(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }
            string bufferTextFromFile = "";
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    byte[] b = new byte[fs.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (bs.Read(b, 0, b.Length) > 0)
                    {
                        bufferTextFromFile += temp.GetString(b);
                    }
                }
            }
            bufferTextFromFile = bufferTextFromFile.Replace("\r", "");
            var strs = bufferTextFromFile.Split('\n');
            if (strs[0].Contains("CountLeveles"))
            {
                //считываем количество уровней
                int count = Convert.ToInt32(strs[0].Split(':')[1]);
                if (deposStages != null)
                {
                    deposStages.Clear();
                }
                deposStages = new List<depo<Iteplohod>>(count);
            }
            else
            {
                //если нет такой записи, то это не те данные
                return false;
            }
            int counter = -1;
            Iteplohod tep = null;
            for (int i = 1; i < strs.Length; ++i)
            {
                //идем по считанным записям
                if (strs[i] == "Level")
                {
                    //начинаем новый уровень
                    counter++;
                    deposStages.Add(new depo<Iteplohod>(countPlaces, pictureWidth, pictureHeight));
                    continue;
                }
                if (string.IsNullOrEmpty(strs[i]))
                {
                    continue;
                }
                if (strs[i].Split(':')[1] == "Lokomotiv")
                {
                    tep = new Lokomotiv(strs[i].Split(':')[2]);
                }
                else if (strs[i].Split(':')[1] == "LokomotivTep")
                {
                    tep = new LokomotivTep(strs[i].Split(':')[2]);
                }
                deposStages[counter][Convert.ToInt32(strs[i].Split(':')[0])] = tep;
            }
            return true;
        }
    }
}