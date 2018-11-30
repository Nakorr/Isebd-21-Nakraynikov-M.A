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
        /// Объект от класса-парковки
        /// </summary>
        depo<Iteplohod> depo;

        public FormTeplohod()
        {
            InitializeComponent();
            depo = new depo<Iteplohod>(20, pictureBoxTake.Width, pictureBoxTake.Height);
            Draw();
        }

        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxTeplohod.Width, pictureBoxTeplohod.Height);
            Graphics gr = Graphics.FromImage(bmp);
            depo.Draw(gr);
            pictureBoxTeplohod.Image = bmp;
        }
        /// <summary>

        /// <summary>
        /// Обработка нажатия кнопки "Припарковать локоматив"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusLokomativ_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var teplohod = new Lokomotiv(100, 1000, dialog.Color);
                int place = depo + teplohod;
                Draw();
            }
        }

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusTep_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ColorDialog dialogDop = new ColorDialog();
                if (dialogDop.ShowDialog() == DialogResult.OK)
                {
                    var teplohod = new LokomotivTep(100, 1000, dialog.Color, dialogDop.Color, true, true);
                    int place = depo + teplohod;
                    Draw();
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
            if (maskedTextBox.Text != "")
            {
                var teplohod = depo - Convert.ToInt32(maskedTextBox.Text);
                if (teplohod != null)
                {
                    Bitmap bmp = new Bitmap(pictureBoxTake.Width, pictureBoxTake.Height);
                    Graphics gr = Graphics.FromImage(bmp);
                     teplohod.SetPosition(5, 5, pictureBoxTake.Width, pictureBoxTake.Height);
                    teplohod.DrawTransport(gr);
                    pictureBoxTake.Image = bmp;
                }
                else
                {
                    Bitmap bmp = new Bitmap(pictureBoxTake.Width, pictureBoxTake.Height);
                    pictureBoxTake.Image = bmp;
                }
                    Draw();
            }
        }
    }
}
