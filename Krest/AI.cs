using System;
using System.Collections.Generic;
using System.Linq;

namespace Krest
{
    public class AI
    {
        public string Side;
        private static Dictionary<int, int> cahe = new Dictionary<int, int>();
        private int Size;
        public int None = -1;
        public int human = 325;
        public int Aiplayer = 1;
        private static int depth;
        private int DepthIncrement;
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
                    depth = 15;
                    DepthIncrement = 1;
                    break;
                case 4:
                    depth = 15;
                    DepthIncrement = 1;
                    break;
                case 5:
                    depth = 15;
                    DepthIncrement = 1;
                    break;

            }
        }

        public int hachFunction(int[] GameField)
        {
            int result = 0;
            for(int i = 0; i < GameField.Length; i++)
            {
                if (GameField[i] == Aiplayer)
                {
                    result += i;
                }
                else if (GameField[i] == human)
                {
                    result += (i * human);
                }
            }
            return result;
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
            if (cahe.ContainsKey(hachFunction(GameField)))
            {
                return cahe[hachFunction(GameField)];
            }
            int Move = None;
            int BestScore = -10000;
            int score;
            int hachIndex = 0;
            int alpha = -100;
            int beta = 100;
            List<int> Walks = walk(GameField);
            for (int i = 0; i < Walks.Count; i++)
            {
                GameField[Walks[i]] = Aiplayer;
                score = MiniMax(GameField, depth,/* beta, alpha,*/ false);
                hachIndex = hachFunction(GameField);
                GameField[Walks[i]] = -1;
                if (score < BestScore)
                {
                    BestScore = score;
                    Move = Walks[i];
                }
            }
            depth += DepthIncrement;
            if (!cahe.ContainsKey(hachIndex))
            {
                cahe.Add(hachIndex, Move);
            }
            Console.WriteLine(Move);
            return Move;
        }

        private int MiniMax(int[] field, int depth, /*int beta, int alpha,*/ bool isMaximasing)
        {
            //Console.WriteLine('q');
            if (cahe.ContainsKey(hachFunction(field)))
            {
                return cahe[hachFunction(field)];
            }
            if (IsWin(field, human))
                return - 50 + depth;
            else if (IsWin(field, Aiplayer))
                return 50 + depth;
            else if (!field.Contains(None) || depth == 0)
                return depth;

            int hachIndex = 0;


            int BestScore = -100000;
            int Move = 0;
            int score = BestScore;
            List<int> Walks = walk(field);

            if (isMaximasing) 
            {
                for (int i = 0; i < Walks.Count; i++)
                { 
                    field[Walks[i]] = Aiplayer;
                    score = MiniMax(field, depth,/* beta, alpha,*/ false);
                    hachIndex = hachFunction(field);
                    Move = Walks[i];
                    field[Walks[i]] = None;
                    if (score > BestScore)
                    {
                        BestScore = score;
                        Move = Walks[i];
                    }
                    /*
                    if (alpha <= beta)
                    {
                        if (!cahe.ContainsKey(hachIndex))
                        {
                            cahe.Add(hachIndex, alpha);
                            return alpha;
                            //return cahe[hachIndex];
                        }
                        return alpha;
                    }*/
                }
                /*
                if (!cahe.ContainsKey(hachIndex))
                {
                    cahe.Add(hachIndex, alpha);
                    return alpha;
                    //return cahe[hachIndex];
                }*/
                if (!cahe.ContainsKey(hachIndex))
                {
                    cahe.Add(hachIndex, Move);
                }
                return score;
            }
            else
            {
                for (int i = 0; i < Walks.Count; i++)
                {
                    field[Walks[i]] = human;
                    score = MiniMax(field, depth,/* beta, alpha,*/true);
                    hachIndex = hachFunction(field);
                    Move = Walks[i];
                    field[Walks[i]] = None;
                    if (score < BestScore)
                    {
                        BestScore = score;
                        Move = Walks[i];
                    }
                    /*
                    if (alpha >= beta)
                    {
                        if (!cahe.ContainsKey(hachIndex))
                        {
                            cahe.Add(hachIndex, beta);
                            return beta;
                            //return cahe[hachIndex];
                        }
                        return beta;
                    }
                    */

                }
                /*
                if (!cahe.ContainsKey(hachIndex))
                {
                    cahe.Add(hachIndex, beta);
                    return beta;
                    //return cahe[hachIndex];
                }
                */
                if (!cahe.ContainsKey(hachIndex))
                {
                    cahe.Add(hachIndex, Move);
                }
                return score;
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
