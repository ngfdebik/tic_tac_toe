using System;
using System.Windows.Forms;

namespace Krest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameFieldForm Game = new GameFieldForm(new GameConfig((int)comboBox1.SelectedItem + 3, (Player)comboBox2.SelectedItem, (GameMode)comboBox3.SelectedItem));
            Game.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Выберите размер игрового поля";
            comboBox1.Items.Add(FieldSize.X3);
            comboBox1.Items.Add(FieldSize.X4);
            comboBox1.Items.Add(FieldSize.X5);

            label2.Text = "Выберие сторону";
            comboBox2.Items.Add(Player.X);
            comboBox2.Items.Add(Player.O);

            label3.Text = "Выберите режим";
            comboBox3.Items.Add(GameMode.ComputerXHuman);
            comboBox3.Items.Add(GameMode.HumanXHuman);
            comboBox3.Items.Add(GameMode.ComputerXComputer);

        }
    }
}
