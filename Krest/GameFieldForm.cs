using System;
using System.Drawing;
using System.Windows.Forms;

namespace Krest
{
    public partial class GameFieldForm : Form
    {
        private int MachineIndex = 1;
        private GameConfig GameConfig;
        private int[] GameField;
        private Button[] buttons;
        private AI ai;
        private Player text = Player.X;

        public GameFieldForm(GameConfig Config)
        {
            GameConfig = Config;
            GameField = new int[(int)Math.Pow(Config.Size, 2)];
            buttons = new Button[(int)Math.Pow(Config.Size, 2)];
            ai = new AI(Config.Player, Config.Size);
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void GameFieldForm_Load(object sender, EventArgs e)
        {
            button1.Text = "Назад";
            Button button;
            int index = 0;
            for (int j = 0; j < GameConfig.Size; j++)
            {
                for (int i = 0; i < GameConfig.Size; i++)
                {
                    button = new Button();
                    button.TabIndex = index;
                    button.Width = 180 / GameConfig.Size;
                    button.Height = 180 / GameConfig.Size;
                    button.Location = new Point((button.Width / 2) + (button.Width * i), (button.Height / 2) + (button.Height * j));
                    button.Font = new Font("Microsoft Sans Serif", 100 / GameConfig.Size);
                    switch (GameConfig.GameMode)
                    {
                        case GameMode.ComputerXHuman:
                            button.Click += ButtonClickMachine;
                            break;
                        case GameMode.HumanXHuman:
                            button.Click += ButtonClickHuman;
                            break;
                        case GameMode.ComputerXComputer:
                            break;
                    }
                        
                    this.Controls.Add(button);

                    buttons[index] = button;

                    GameField[index] = -1;

                    index++;
                }
            }
            if (GameConfig.Player == Player.O && GameConfig.GameMode == GameMode.ComputerXHuman)
            {
                MachineMove(ai);
            }
            else if (GameConfig.GameMode == GameMode.ComputerXComputer)
            {
                ButtonClickComputer();
            }

        }
        private void isEnd()
        {
            if(ai.IsWin(GameField, ai.human))
            {
                MessageBoxButtons Mbutton = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("End", "Крестики победили", Mbutton);
            }
            else if (ai.IsWin(GameField, ai.Aiplayer))
            {
                MessageBoxButtons Mbutton = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("End", "Нолики победили", Mbutton);
            }
        }

        private void ButtonClickMachine(object sender, EventArgs e)
        {
            HumanMove(sender, ai.human, GameConfig.Player.ToString());
            isEnd();
            MachineMove(ai);
            isEnd();

        }
        private void MachineMove(AI ai)
        {
            int Move = ai.AiMove(GameField);
            if (Move < 0)
            {/*
                MessageBoxButtons Mbutton = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("End", "Ничья", Mbutton);
                */
            }
            else
            {
                buttons[Move].Text = ai.Side;
                buttons[Move].Enabled = false;
                GameField[Move] = MachineIndex;
            }
        }
        private void HumanMove(object sender, int player, string text)
        {
            Button PlayerClick = (sender as Button);
            PlayerClick.Text = text;
            PlayerClick.Enabled = false;
            GameField[PlayerClick.TabIndex] = player;
        }
        private void ButtonClickHuman(object sender, EventArgs e)
        {
            HumanMove(sender, ai.human, text.ToString());
            isEnd();
            text = swap(text);
        }
        private Player swap(Player str)
        {
            if (str == Player.X)
                return Player.O;
            else
                return Player.X;
        }
        private void ButtonClickComputer()
        {
            for (int i = 0; i < GameField.Length; i++)
            {
                int Move = ai.AiMove(GameField);
                if (Move < 0)
                {
                    MessageBoxButtons Mbutton = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show("End", "Ничья", Mbutton);
                }
                else
                {
                    buttons[Move].Text = text.ToString();
                    buttons[Move].Enabled = false;
                    GameField[Move] = MachineIndex;
                    text = swap(text);
                    GameField = reverseField(GameField);
                }
            }
        }
        private int[] reverseField(int[] field)
        {
            for(int i = 0; i < field.Length; i++)
            {
                if (field[i] == 0)
                    field[i] = 1;
                else if (field[i] == 1)
                    field[i] = 0;
            }
            return field;
        }
    }
}
