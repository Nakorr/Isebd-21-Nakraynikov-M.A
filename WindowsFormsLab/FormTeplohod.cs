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

        /// <summary>
        /// Обработка нажатия кнопки "Припарковать локоматив"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusLokomativ_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var tep = new Lokomotiv(100, 1000, dialog.Color);
                    int place = depos[listBox.SelectedIndex] + tep;
                    if (place == -1)
                    {
                        MessageBox.Show("Нет свободных мест", "Ошибка",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Draw();
                }
            }
        }

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusTep_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        var tep = new LokomotivTep(100, 1000, dialog.Color, dialogDop.Color, true, true);
                        int place = depos[listBox.SelectedIndex] + tep;
                        if (place == -1)
                        {
                            MessageBox.Show("Нет свободных мест", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Draw();
                    }
                }
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
    }
}
