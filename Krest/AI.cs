using System;
using System.Collections.Generic;
using System.Linq;

namespace Krest
{
    public class AI
    {
        public string Side;
        private static Dictionary<string, int> cahe1 = new Dictionary<string, int>();
        private static Dictionary<string, int> cahe2 = new Dictionary<string, int>();
        private int Size;
        public int None = -1;
        public int human = 1000;
        public int Aiplayer = 1;
        private int depth;
        private int DepthIncrement;
        //private int BestScore = -10000;
        public AI(Player player, int Size)
        {
            if (player == Player.O)
                Side = "X";
            else
                Side = "O";
            this.Size = Size;
            switch(Size)
            {
                case 3:
                    this.depth = 15;
                    this.DepthIncrement = 1;
                    break;
                case 4:
                    this.depth = 10;
                    this.DepthIncrement = 1;
                    break;
                case 5:
                    this.depth = 15;
                    this.DepthIncrement = 1;
                    break;

            }
        }

        public string hachFunction(int[] GameField)
        {
            int result1 = 0;
            int result2 = 0;
            int count1 = 0;
            int count2 = 0;
            string tmp = this.Size.ToString() + this.Side + ':';
            for(int i = 0; i < GameField.Length; i++)
            {
                if (GameField[i] == Aiplayer)
                {
                    result1 = (i+1);
                    tmp += result1.ToString() + 'X';
                }
                else if (GameField[i] == human)
                {
                    result2 = ((i+1) * human);
                    tmp += result2.ToString() + 'O';
                }
            }
            return tmp;
        }

        public List<int> walk(int[] GameField)
        {
            List<int> tmp = new List<int>();
            for (int i = 0; i < GameField.Length; ++i)
                if (GameField[i] == -1)
                {
                    tmp.Add((int)i);
                }
            return tmp;
        }

        public int AiMove(int[] GameField)
        {
/*            if (cahe.ContainsKey(hachFunction(GameField)))
            {
                return cahe[hachFunction(GameField)];
            }*/
            int Move = None;
            int BestScore1 = -1000;
            int score;
            //string hachIndex = "";
            int alpha = -100;
            int beta = 100;
            List<int> Walks = walk(GameField);
            for (int i = 0; i < Walks.Count; i++)
            {
                GameField[Walks[i]] = Aiplayer;
                score = MiniMax(GameField, this.depth, beta, alpha, false);
                //hachIndex = hachFunction(GameField);
                GameField[Walks[i]] = -1;
                if (score > BestScore1)
                {
                    BestScore1 = score;
                    Move = Walks[i];
                }
            }
            if(Move == None)
            {
                Move = Walks[0];
            }

            depth += DepthIncrement;
/*            if (!cahe.ContainsKey(hachIndex))
            {
                cahe.Add(hachIndex, Move);
            }*/
            /*Console.WriteLine(Move);*/
            return Move;
        }

        private int MiniMax(int[] field, int depth, int beta, int alpha, bool isMaximasing)
        {
            //Console.WriteLine('q');


            List<int> Walks = walk(field);
            if (IsWin(field, human))
                return -100 + depth;
            else if (IsWin(field, Aiplayer))
                return 100 - depth;
            else if (Walks.Count == 0 || depth == 0)
                return 0;

            string hachIndex = "";
            int[] tmp = new int[field.Length];
            int BestScore2 = 0;

            //int Move = 0;
            int score = BestScore2;

            if (isMaximasing)
            {
                if (cahe1.ContainsKey(hachIndex))
                {
                    return cahe1[hachIndex];
                }
                BestScore2 = -1000;
                for (int i = 0; i < Walks.Count; i++)
                {
                    
                    field[Walks[i]] = Aiplayer;
                    score = MiniMax(field, depth-1, beta, alpha, false);
                    BestScore2 = Math.Max(BestScore2, score);
                    //field.CopyTo(tmp, 0);
                    hachIndex = hachFunction(field);
                    field[Walks[i]] = None;
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha)
                        break;
                }
                //hachIndex = hachFunction(field);
                if (!cahe1.ContainsKey(hachIndex))
                {
                    cahe1.Add(hachIndex, BestScore2);
                }
                return BestScore2;
            }
            else
            {
                if (cahe2.ContainsKey(hachIndex))
                {
                    return cahe2[hachIndex];
                }
                BestScore2 = 1000;
                for (int i = 0; i < Walks.Count; i++)
                {
                    
                    field[Walks[i]] = human;
                    score = MiniMax(field, depth-1, beta, alpha, true);
                    BestScore2 = Math.Min(BestScore2, score);
                    //field.CopyTo(tmp, 0);
                    hachIndex = hachFunction(field);
                    field[Walks[i]] = None;
                    beta = Math.Min(beta, score);
                    if (beta <= alpha)
                        break;
                }
                //hachIndex = hachFunction(field);
                if (!cahe2.ContainsKey(hachIndex))
                {
                    cahe2.Add(hachIndex, BestScore2);
                }
                return BestScore2;
            }
        }

        public bool IsWin(int[] field, int player)
        {
            List<int> intMap = new List<int>();
            List<List<int>> Wins = new List<List<int>>(new WinCombinations(Size).GetWins());

            for (int i = 0; i < field.Length; i++)
                if (field[i] == player)
                    intMap.Add(i);
            foreach (List<int> obj in Wins)
            {
                if (!obj.Except(intMap).Any())
                    return true;
            }
            return false;
        }
    }
}
