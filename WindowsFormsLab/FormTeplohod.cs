using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsLab.Teplohod;

namespace WindowsFormsLab
{
    public partial class FormTeplohod : Form
    {
        private Teplohod teplohod;
        public FormTeplohod()
        {
            InitializeComponent();
        }

        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxTeplohod.Width, pictureBoxTeplohod.Height);
            Graphics gr = Graphics.FromImage(bmp);
            teplohod.DrawTeplohod(gr);
            pictureBoxTeplohod.Image = bmp;
        }
        /// <summary>

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            teplohod = new Teplohod (rnd.Next(100, 300), rnd.Next(1000, 2000), Color.Green,
           Color.Red);
            teplohod.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100), pictureBoxTeplohod.Width,
           pictureBoxTeplohod.Height);
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопок управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    teplohod.MoveTransport(Direction.Up);
                    break;
                case "buttonDown":
                    teplohod.MoveTransport(Direction.Down);
                    break;
                case "buttonLeft":
                    teplohod.MoveTransport(Direction.Left);
                    break;
                case "buttonRight":
                    teplohod.MoveTransport(Direction.Right);
                    break;
            }
            Draw();
        }
    }
}
