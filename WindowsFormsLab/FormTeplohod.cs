using System;
using NLog;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsLab
{
    public partial class FormTeplohod : Form
    {
        /// <summary>
        /// Объект от класса-уровней
        /// </summary>
        LevelDepo depos;
        private const int countLevel = 5;
        /// <summary>
        /// Форма для добавления
        /// </summary>
        FormTepConfig form;
        /// <summary>
        /// Логгер
        /// </summary>
        private Logger logger;
        public FormTeplohod()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            depos = new LevelDepo(countLevel, pictureBoxTeplohod.Width, pictureBoxTeplohod.Height);
            //заполнение listBox
            for (int i = 0; i < countLevel; i++)
            {
                listBox.Items.Add("Уровень " + (i + 1));
            }
            listBox.SelectedIndex = 0;
        }

        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            if (listBox.SelectedIndex > -1)
            {//если выбран один из пуктов в listBox (при старте программы ни один пунктне будет выбран и может возникнуть ошибка, если мы попытаемся обратиться к элементу listBox)
                Bitmap bmp = new Bitmap(pictureBoxTeplohod.Width, pictureBoxTeplohod.Height);
                Graphics gr = Graphics.FromImage(bmp);
                depos[listBox.SelectedIndex].Draw(gr);
                pictureBoxTeplohod.Image = bmp;
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Забрать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///   
        private void Take_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                if (maskedTextBox.Text != "")
                {
                    try
                    {
                        var tep = depos[listBox.SelectedIndex] -
                   Convert.ToInt32(maskedTextBox.Text);
                        Bitmap bmp = new Bitmap(pictureBoxTake.Width,
                       pictureBoxTake.Height);
                        Graphics gr = Graphics.FromImage(bmp);
                        tep.SetPosition(5, 5, pictureBoxTake.Width,
                       pictureBoxTake.Height);
                        tep.DrawTransport(gr);
                        pictureBoxTake.Image = bmp;
                        logger.Info("Изъят вагон " + tep.ToString() + " с места " + maskedTextBox.Text);
                        Draw();
                    }
                    catch (depoNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message, "Не найдено", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                        Bitmap bmp = new Bitmap(pictureBoxTake.Width,
                       pictureBoxTake.Height);
                        pictureBoxTake.Image = bmp;
                    }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message, "Неизвестная ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                }
            }
        }
        /// <summary>
        /// Метод обработки выбора элемента на listBoxs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }
        /// Обработка нажатия кнопки "Добавить вагон"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetTep_Click(object sender, EventArgs e)
        {
            form = new FormTepConfig();
            form.AddEvent(AddTep);
            form.Show();
        }
        /// <summary>
        /// Метод добавления вагона
        /// </summary>
        /// <param name="tep"></param>
        private void AddTep(Iteplohod tep)
        {
            if (tep != null && listBox.SelectedIndex > -1)
            {
                try
                {
                    int place = depos[listBox.SelectedIndex] + tep;
                    logger.Info("Добавлен вагон " + tep.ToString() + " на место " + place);
                    Draw();
                }
                catch (depoOverflowException ex)
                {
                    MessageBox.Show(ex.Message, "Переполнение", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
                catch (depoAlreadyHaveException ex)
                {
                    MessageBox.Show(ex.Message, "Дублирование", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Обработка нажатия пункта меню "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    depos.SaveData(saveFileDialog.FileName);
                    MessageBox.Show("Сохранение прошло успешно", "Результат",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Сохранено в файл " + saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка при сохранении",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Обработка нажатия пункта меню "Загрузить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    depos.LoadData(openFileDialog.FileName);
                    MessageBox.Show("Загрузили", "Результат", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    logger.Info("Загружено из файла " + openFileDialog.FileName);
                }
                catch (depoOccupiedPlaceException ex)
                {
                    MessageBox.Show(ex.Message, "Занятое место", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка при сохранении",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Draw();
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Сортировка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSort_Click(object sender, EventArgs e)
        {
            depos.Sort();
            Draw();
            logger.Info("Сортировка уровней");
        }
    }
}
