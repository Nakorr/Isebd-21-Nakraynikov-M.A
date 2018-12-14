using System;
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
        public FormTeplohod()
        {
            InitializeComponent();
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
                    var tep = depos[listBox.SelectedIndex] -
                   Convert.ToInt32(maskedTextBox.Text);
                    if (tep != null)
                    {
                        Bitmap bmp = new Bitmap(pictureBoxTake.Width,
                       pictureBoxTake.Height);
                        Graphics gr = Graphics.FromImage(bmp);
                        tep.SetPosition(5, 5, pictureBoxTake.Width,
                       pictureBoxTake.Height);
                        tep.DrawTransport(gr);
                        pictureBoxTake.Image = bmp;
                    }
                    else
                    {
                        Bitmap bmp = new Bitmap(pictureBoxTake.Width,
                       pictureBoxTake.Height);
                        pictureBoxTake.Image = bmp;
                    }
                    Draw();
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
        /// <summary>
        /// Обработка нажатия кнопки "Добавить автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
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
        /// <param name="car"></param>
        private void AddTep(Iteplohod car)
        {
            if (car != null && listBox.SelectedIndex > -1)
            {
                int place = depos[listBox.SelectedIndex] + car;
                if (place > -1)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("Вагон не удалось поставить");
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
                if (depos.SaveData(saveFileDialog.FileName))
                {
                    MessageBox.Show("Сохранение прошло успешно", "Результат",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не сохранилось", "Результат", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
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
                if (depos.LoadData(openFileDialog.FileName))
                {
                    MessageBox.Show("Загрузили", "Результат", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не загрузили", "Результат", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
                Draw();
            }
        }
    }
}
