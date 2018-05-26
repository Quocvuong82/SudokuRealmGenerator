using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmGenerator
{
    public class MySudokuPuzzle : ICloneable
    {
        public List<MyCell> Cells;
        public const int Length = 9;
        public const int BoxSize = 3;
        public static Int64 nums; 


        public MySudokuPuzzle(string input)
        {
            if (input.Length != 81)
            {
                Console.WriteLine("input not ok");
                return;
            }
            Cells = new List<MyCell>();
            int index=0;
            foreach (char ch in input)
            {
                if (Char.IsDigit(ch) && ch!='0')
                    Cells.Add(new MyCell(index, Int32.Parse(ch.ToString()), new List<int>(),true));
                else
                    Cells.Add(new MyCell(index, 0, new List<int>(), false));
                index++;
            }
            if (Peers == null)
                GeneratePeersArray();
            GenerateInitialCandidates();
        }

        public MySudokuPuzzle()
        {
            Cells = new List<MyCell>();
        }

        private void GenerateInitialCandidates()
        {
            foreach (MyCell cell in this.Cells)
            {
                bool isFoundInPeers=false;
                for (int i = 1; i<=9 ; i++)
                {
                    isFoundInPeers = false;
                    foreach (int peerIndex in Peers[cell.index])
                    {
                        if (Cells[peerIndex].value == i)
                        {
                            isFoundInPeers = true;
                        }
                    }
                    if (!isFoundInPeers)
                        cell.candidates.Add(i);
                }
            }
        }

        public static bool IsPeer(int index1, int index2)
        {
            return (index1 != index2) && (
                (index1 / Length == index2 / Length) || (index1 % Length == index2 % Length) || (index1 / Length / BoxSize == index2 / Length / BoxSize && index1 % Length / BoxSize == index2 % Length / BoxSize));
        }

        public static Dictionary<int,List<int>> Peers;

        private void GeneratePeersArray()
        {
            Peers = new Dictionary<int, List<int>>();
            List<int> temp;
            foreach (MyCell cell in this.Cells)
            {
                temp = new List<int>();
                foreach (MyCell cell2 in this.Cells)
                {
                    if (IsPeer(cell.index, cell2.index))
                        temp.Add(cell2.index);
                }
                Peers.Add(cell.index, temp);
            }
        }

        public MySudokuPuzzle ApplyConstraints(int cellIndex, int value)
        {
            MySudokuPuzzle puzzle = (MySudokuPuzzle)this.Clone();
            puzzle.Cells[cellIndex].value = value;
            foreach (int peerIndex in Peers[cellIndex])
            {

                var newCandidates = puzzle.Cells[peerIndex].candidates.Except(new List<int> { value });
                puzzle.Cells[peerIndex].candidates = newCandidates.ToList<int>();
                if (!newCandidates.Any())
                    return null;
                puzzle.Cells[cellIndex].candidates = new List<int>(Enumerable.Repeat(0, 9));

            }
            return puzzle;
        }

        public MySudokuPuzzle PlaceValue(int cellIndex, int value)
        {
            if (!this.Cells[cellIndex].candidates.Contains(value))
                return null;

            var puzzle = ApplyConstraints(cellIndex, value);
            if (puzzle == null)
                return null;
            return puzzle;

        }

        public static MySudokuPuzzle RandomGrid()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 81; i++)
            {
                sb.Append('0');
            }
            MySudokuPuzzle puzzle = new MySudokuPuzzle(sb.ToString());

            var rand = new Random();
            char ch = '#';
            while (true)
            {
                Console.Write(ch);
                int[] UnsolvedCellIndexes = puzzle.Cells
                    .Select((cands, index) => new { cands, index })     //Project to a new sequence of candidates and index (an anonymous type behaving like a tuple)
                    .Where(t => t.cands.candidates.Count >= 2)                    //Filter to cells with at least 2 candidates
                    .Select(u => u.index)                               //Project the tuple to only the index
                    .ToArray();

                int cellIndex = UnsolvedCellIndexes[rand.Next(UnsolvedCellIndexes.Length)];
                int candidateValue = puzzle.Cells[cellIndex].candidates[rand.Next(puzzle.Cells[cellIndex].candidates.Count)];

                MySudokuPuzzle workingPuzzle = puzzle.PlaceValue(cellIndex, candidateValue);
                if (workingPuzzle != null)
                {
                    var Solutions = MultiSolve(workingPuzzle, 2);
                    switch (Solutions.Count)
                    {
                        case 0: continue;
                        case 1:
                            //Console.WriteLine(Solutions[0].ToString());
                            return Solutions.Single();
                        default:
                            puzzle = workingPuzzle;
                            break;
                    }
                }
            }

        }

        public static MySudokuPuzzle RandomGrid(string seed)
        {
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 81; i++)
            //{
            //    sb.Append('0');
            //}
            MySudokuPuzzle puzzle = new MySudokuPuzzle(seed);

            var rand = new Random();
            char ch = '#';
            while (true)
            {
                //Console.Write(ch);
                int[] UnsolvedCellIndexes = puzzle.Cells
                    .Select((cands, index) => new { cands, index })     //Project to a new sequence of candidates and index (an anonymous type behaving like a tuple)
                    .Where(t => t.cands.candidates.Count >= 2)                    //Filter to cells with at least 2 candidates
                    .Select(u => u.index)                               //Project the tuple to only the index
                    .ToArray();

                int cellIndex = UnsolvedCellIndexes[rand.Next(UnsolvedCellIndexes.Length)];
                int candidateValue = puzzle.Cells[cellIndex].candidates[rand.Next(puzzle.Cells[cellIndex].candidates.Count)];

                MySudokuPuzzle workingPuzzle = puzzle.PlaceValue(cellIndex, candidateValue);
                if (workingPuzzle != null)
                {
                    var Solutions = MultiSolve(workingPuzzle, 2);
                    switch (Solutions.Count)
                    {
                        case 0: continue;
                        case 1:
                            //Console.WriteLine(Solutions[0].ToString());
                            return Solutions.Single();
                        default:
                            puzzle = workingPuzzle;
                            break;
                    }
                }
            }
        }

        public object Clone()
        {
            var clone = new MySudokuPuzzle();
            foreach (MyCell cell in this.Cells)
            {
                clone.Cells.Add((MyCell)cell.Clone());
            }

            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int maxWidth = 9;
            foreach (MyCell cell in this.Cells)
            {
                if (cell.index % 27 == 0)
                    sb.AppendLine();  
                if (cell.index % 9 == 0)
                        sb.AppendLine();
                    if (cell.index % 3 == 0)
                        sb.Append("  |  ");
                    sb.Append(cell);

            }

            return sb.ToString();
        }

        public MySudokuPuzzle SolveUsingGuess(Func<MySudokuPuzzle, bool> SolutionFunc = null)
        {
            if (this.Cells.All(mycell => mycell.value != 0))
                return (SolutionFunc != null && SolutionFunc(this)) ? null : this;
            
            int ActiveCell = FindWorkingCell();
            //Console.WriteLine(nums);
            foreach (int guess in this.Cells[ActiveCell].candidates)
            {
                MySudokuPuzzle puzzle;
                if ((puzzle = PlaceValue(ActiveCell, guess)) != null)
                    if ((puzzle = puzzle.SolveUsingGuess(SolutionFunc)) != null)
                    {
                        return puzzle;
                    }
            }
            return null;
        }

        public static List<MySudokuPuzzle> MultiSolve(MySudokuPuzzle input, int MaximumSolutions = -1)
        {
            
            var Solutions = new List<MySudokuPuzzle>();
            input.SolveUsingGuess(p =>
            {
                Solutions.Add(p);
                return Solutions.Count() < MaximumSolutions || MaximumSolutions == -1;
            });
            return Solutions;
        }
        public int FindWorkingCell()
        {
            int minCandidates = (from cell in this.Cells where !cell.isFixed && cell.value==0 select cell).Min(cell => cell.candidates.Count);
            
            foreach (MyCell cell in this.Cells)
            {
                if (cell.isFixed || cell.value != 0)
                    continue;
                if (cell.candidates.Count == minCandidates)
                    return cell.index;
            }
            return -1;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzleForMiddleSamurai(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue && puzzle.NumberOfEmptyCells()<=50)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    bool isOK = false;
                    switch (index)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 24:
                        case 25:
                        case 26:
                        case 54:
                        case 55:
                        case 56:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                        case 78:
                        case 79:
                        case 80:
                            isOK = false;
                            break;
                        default:
                            isOK = true;
                            break;
                    }

                    if (isOK)
                    {
                        StringBuilder sb = new StringBuilder(stringpuzzle);
                        sb[index] = '0';
                        string newstringpuzzle = sb.ToString();
                        MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                        var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                        if (solutions.Count == 1)
                        {
                            count++;
                            int weight = 0;
                            int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                            Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                                puzzle.NumberOfEmptyCells(),
                                currentRate.ToString()
                                ));
                            if (currentRate != 4)
                            {
                                possibleIndexes.Add(index);
                                possibleRates.Add(currentRate);
                            }
                        }
                    }
                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
            }
            
            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzle(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue && puzzle.NumberOfEmptyCells() <= 50)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzle2(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);

            // shuffle list 1..9
            //List<char> list = new List<char>();
            //list.Add('1');
            //list.Add('2');
            //list.Add('3');
            //list.Add('4');
            //list.Add('5');
            //list.Add('6');
            //list.Add('7');
            //list.Add('8');
            //list.Add('9');
            //Random rng = new Random();
            //int n = list.Count;
            //while (n > 1)
            //{
            //    n--;
            //    int k = rng.Next(n + 1);
            //    char value = list[k];
            //    list[k] = list[n];
            //    list[n] = value;
            //}

            //// dig all selected number from list
            //for (int i = 0; i < stringpuzzle.Length; i++)
            //{
            //    if (stringpuzzle[i] == list[0])
            //        sbb[i] = '0';
            //}
            //// dig another number with only one remaining
            //List<int> possible2 = new List<int>();
            //for (int i = 0; i < stringpuzzle.Length; i++)
            //{
            //    if (stringpuzzle[i] == list[1])
            //        possible2.Add(i);
            //}
            //int random2index = rand.Next(possible2.Count);
            //for (int i = 0; i < stringpuzzle.Length; i++)
            //{
            //    if (stringpuzzle[i] == list[1] && i != random2index)
            //        sbb[i] = '0';
            //}
            //stringpuzzle = sbb.ToString();
            // dig another number with only two remaining
            //List<int> possible3 = new List<int>();
            //for (int i = 0; i < stringpuzzle.Length; i++)
            //{
            //    if (stringpuzzle[i] == list[2])
            //        possible3.Add(i);
            //}
            //int random3index = rand.Next(possible3.Count);
            //possible3.Remove(random3index);
            //int random3index2 = rand.Next(possible3.Count);
            //possible3.Remove(random3index2);
            //int random3index3 = rand.Next(possible3.Count);
            //for (int i = 0; i < stringpuzzle.Length; i++)
            //{
            //    if (stringpuzzle[i] == list[2] && i != random3index && i != random3index2 && i != random3index3)
            //        sbb[i] = '0';
            //}
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue && puzzle.NumberOfEmptyCells() <= 40)
            {
                //Console.ReadLine();
                //Console.WriteLine(puzzle.toStringList());
                /////// perform digging holes ///////
                //Console.WriteLine(count);
                stringpuzzle = puzzle.toStringList();

                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed
                                     select cell.index).ToList<int>();
                //int MaxRate = 0;
                //int MaxIndex = 0;
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    //int randomCellIndex = rand.Next(Indexes.Count);
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    //sb[Indexes[randomCellIndex]] = '0';
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    //Console.WriteLine(newstringpuzzle);
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        //puzzle = temppuzzle;
                        //if (temppuzzle.isEqual(puzzle))
                        //    break;
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        //if (currentRate != 4 && currentRate > MaxRate)
                        //{
                        //    MaxRate = currentRate;
                        //    MaxIndex = index;
                        //    //puzzle = temppuzzle;
                        //    Continue = false;
                        //} 
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }
                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
                else
                {
                    //Console.WriteLine("FINAL => "+puzzle.toStringList());
                    //Console.WriteLine("FINAL Rate => " + MySudokuHumanSolver.ratePuzzle(puzzle));
                    //Console.ReadLine();
                }


                ////////////////////////////////////
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateFinalSudokuPuzzle(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        //if (currentRate != 4)
                        //{
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        //}
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
                else
                    Continue = true;
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzleForLeftTop(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            int[] Blocks = new int[]{
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
        };
            while (!Continue && puzzle.NumberOfEmptyCells() <= 50)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && Blocks[cell.index]!=9
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzleForRightTop(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            int[] Blocks = new int[]{
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
        };
            while (!Continue && puzzle.NumberOfEmptyCells() <= 50)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && Blocks[cell.index] != 7
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzleForLeftBottom(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            int[] Blocks = new int[]{
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
        };
            while (!Continue && puzzle.NumberOfEmptyCells() <= 50)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && Blocks[cell.index] != 3
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzleForRightBottom(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            int[] Blocks = new int[]{
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
        };
            while (!Continue && puzzle.NumberOfEmptyCells() <= 50)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && Blocks[cell.index] != 1
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateSudokuPuzzleForMiddle(MySudokuPuzzle fullGrid)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            int[] Blocks = new int[]{
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            1,1,1,2,2,2,3,3,3,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            4,4,4,5,5,5,6,6,6,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
            7,7,7,8,8,8,9,9,9,
        };
            while (!Continue && puzzle.NumberOfEmptyCells() <= 50)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateFinalSudokuPuzzleLeftTop(string lefttop)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(lefttop);
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && MySudokuHumanSolver.Blocks[cell.index] != 1 && MySudokuHumanSolver.Blocks[cell.index] != 2 && MySudokuHumanSolver.Blocks[cell.index]!=4 && MySudokuHumanSolver.Blocks[cell.index]!=5 && MySudokuHumanSolver.Blocks[cell.index]!=9 
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                        possibleIndexes.Add(index);
                        possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
                else
                    Continue = true;
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateFinalSudokuPuzzleRightTop(string righttop)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(righttop);
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && MySudokuHumanSolver.Blocks[cell.index] != 2 && MySudokuHumanSolver.Blocks[cell.index] != 3 && MySudokuHumanSolver.Blocks[cell.index] != 5 && MySudokuHumanSolver.Blocks[cell.index] != 6 && MySudokuHumanSolver.Blocks[cell.index] != 7
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
                else
                    Continue = true;
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public static MySudokuPuzzle GenerateFinalSudokuPuzzleRightBottom(string rightbottom)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(rightbottom);
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && MySudokuHumanSolver.Blocks[cell.index] != 1 
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                    }

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
                else
                    Continue = true;
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public string toStringList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MyCell cell in this.Cells)
            {
                sb.Append(cell.value.ToString());
            }
            return sb.ToString();
        }

        public bool isEqual(MySudokuPuzzle puzzle)
        {
            foreach (MyCell cell in this.Cells)
            {
                if ((cell.isFixed == puzzle.Cells[cell.index].isFixed) &&
                    (cell.value == puzzle.Cells[cell.index].value) &&
                    (cell.candidates.Count == puzzle.Cells[cell.index].candidates.Count))
                {
                    for (int i = 0; i < cell.candidates.Count; i++)
                    {
                        if (cell.candidates[i] != puzzle.Cells[cell.index].candidates[i])
                            return false;
                    }

                }
                else
                    return false;
            }
            return true;
        }
        public bool isSolved()
        {
            if (this.toStringList().IndexOf('0') == -1)
                return true;
            else
                return false;
        }
        public int NumberOfEmptyCells()
        {
            return this.toStringList().ToList().FindAll(ch => ch == '0').Count;
        }

        public static MySudokuPuzzle GenerateSudokuPuzzleBySolver(string fullGridString)
        {
            MySudokuPuzzle puzzle = new MySudokuPuzzle(fullGridString);
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = fullGridString;
            puzzle = new MySudokuPuzzle(stringpuzzle);
            while (!Continue)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    MySudokuPuzzle temppuzzle = new MySudokuPuzzle(newstringpuzzle);
                    MySudokuPuzzle solvedPuzzle = MySudokuHumanSolver.Solve(temppuzzle);
                    if (solvedPuzzle.toStringList() == fullGridString)
                    {
                        int weight = 0;
                        int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                        possibleIndexes.Add(index);
                        possibleRates.Add(currentRate);
                        }
                    }
                    //var solutions = MySudokuPuzzle.MultiSolve(temppuzzle);
                    //if (solutions.Count == 1)
                    //{
                    //    count++;
                    //    int weight = 0;
                    //    int currentRate = MySudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                    //    Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                    //        puzzle.NumberOfEmptyCells(),
                    //        currentRate.ToString()
                    //        ));
                    //    //if (currentRate != 4)
                    //    //{
                    //    possibleIndexes.Add(index);
                    //    possibleRates.Add(currentRate);
                    //    //}
                    //}

                }

                if (possibleIndexes.Count > 0)
                {
                    Continue = false;
                    int MaxRate = possibleRates.Max();
                    List<int> tempRates = new List<int>();
                    List<int> tempIndexes = new List<int>();
                    for (int i = 0; i < possibleRates.Count; i++)
                    {
                        if (possibleRates[i] == MaxRate)
                        {
                            tempRates.Add(possibleRates[i]);
                            tempIndexes.Add(possibleIndexes[i]);
                        }
                    }

                    int randomCellIndex = rand.Next(tempRates.Count);

                    StringBuilder sb2 = new StringBuilder(stringpuzzle);
                    sb2[tempIndexes[randomCellIndex]] = '0';
                    string newstringpuzzle2 = sb2.ToString();
                    puzzle = new MySudokuPuzzle(newstringpuzzle2);
                }
                else
                    Continue = true;
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
       
    }
}
