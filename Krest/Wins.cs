using System.Collections.Generic;

namespace Krest
{
    class WinCombinations
    {
        private List<List<int>> wins;
        public WinCombinations(int Size)
        {
            switch (Size)
            {
                case 3:
                    this.wins = new List<List<int>>() {
                                new List<int>(){ 0, 1, 2 },
                                new List<int>(){ 3, 4, 5 },
                                new List<int>(){ 6, 7, 8 },
                                new List<int>(){ 0, 3, 6 },
                                new List<int>(){ 1, 4, 7 },
                                new List<int>(){ 2, 5, 8 },
                                new List<int>(){ 0, 4, 8 },
                                new List<int>(){ 2, 4, 6 }
                    };
                    break;
                case 4:
                    this.wins = new List<List<int>>(){
                                new List<int>(){ 0, 1, 2, 3 },
                                new List<int>(){ 4, 5, 6, 7 },
                                new List<int>(){ 8, 9, 10, 11 },
                                new List<int>(){ 12, 13, 14, 15 },
                                new List<int>(){ 0, 4, 8, 12 },
                                new List<int>(){ 1, 5, 9, 13 },
                                new List<int>(){ 2, 6, 10, 14 },
                                new List<int>(){ 3, 7, 11, 15 },
                                new List<int>(){ 0, 5, 10, 15 },
                                new List<int>(){ 3, 6, 9, 12 }
                    };
                    break;
                case 5:
                    this.wins = new List<List<int>>(){
                                new List<int>(){ 0, 1, 2, 3, 4 },
                                new List<int>(){ 5, 6, 7, 8, 9 },
                                new List<int>(){ 10, 11, 12, 13, 14 },
                                new List<int>(){ 15, 16, 17, 18, 19 },
                                new List<int>(){ 20, 21, 22, 23, 24 },
                                new List<int>(){ 0, 5, 10, 15, 20 },
                                new List<int>(){ 1, 6, 11, 16, 21 },
                                new List<int>(){ 2, 7, 12, 17, 22 },
                                new List<int>(){ 3, 8, 13, 18, 23 },
                                new List<int>(){ 4, 9, 14, 19, 24 },
                                new List<int>(){ 0, 6, 12, 18, 24 },
                                new List<int>(){ 4, 8, 12, 16, 20 }
                    };
                    break;
            }
        }
        public List<List<int>> GetWins()
        {
            return wins;
        }
    }
}
