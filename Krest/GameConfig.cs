
using System;

namespace Krest
{
    public enum FieldSize { X3, X4, X5 };
    public enum Player { X, O };
    public enum GameMode { ComputerXHuman, HumanXHuman, ComputerXComputer };

    public class GameConfig
    {
        public int Size;
        public Player Player;
        public GameMode GameMode;

        public GameConfig(int size = 3, Player player = Player.X, GameMode gameMode = GameMode.ComputerXHuman)
        {
            this.Size = size;
            this.Player = player;
            this.GameMode = gameMode;
        }
        /*
        public GameConfig(null, null, null)
        {
            this.Size = 3;
            this.Player = Player.X;
            this.GameMode = GameMode.ComputerXHuman;
        }*/
    }
}
