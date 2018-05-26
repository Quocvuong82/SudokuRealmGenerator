using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmGenerator
{
    public class ClockSudokuPuzzle : ICloneable
    {
        public List<ClockCell> Cells;
        //public const int Length = 9;
        //public const int BoxSize = 3;

        public ClockSudokuPuzzle(string input)
        {
            if (input.Length != 72)
            {
                Console.WriteLine("input not ok");
                return;
            }
            Cells = new List<ClockCell>();
            int index = 0;
            foreach (char ch in input)
            {
                if (Char.IsDigit(ch) && ch != '0')
                    Cells.Add(new ClockCell(index, Int32.Parse(ch.ToString()), new List<int>(), true));
                else if (ch == 'A')
                    Cells.Add(new ClockCell(index, 10, new List<int>(), true));
                else if (ch == 'B')
                    Cells.Add(new ClockCell(index, 11, new List<int>(), true));
                else if (ch == 'C')
                    Cells.Add(new ClockCell(index, 12, new List<int>(), true));
                else
                    Cells.Add(new ClockCell(index, 0, new List<int>(), false));
                index++;
            }
            if (Peers == null)
                GeneratePeersArray();
            GenerateInitialCandidates();
        }

        public ClockSudokuPuzzle()
        {
            Cells = new List<ClockCell>();
        }

        private void GenerateInitialCandidates()
        {
            foreach (ClockCell cell in this.Cells)
            {
                bool isFoundInPeers = false;
                for (int i = 1; i <= 12; i++)
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
            int index11 = (index1 % 2 == 0 ? index1 : index1 - 1);
            int index22 = (index2 % 2 == 0 ? index2 : index2 - 1);
            return (index1 != index2) && (      // not the same index
                (index1 / 12 == index2 / 12) || // same row
                (index1 % 6 == index2 % 6) ||   // same column
                (index11 % 12 == index22 % 12)  // same block
                );
        }

        public static Dictionary<int, List<int>> Peers;

        private void GeneratePeersArray()
        {
            Peers = new Dictionary<int, List<int>>();
            List<int> temp;
            foreach (ClockCell cell in this.Cells)
            {
                temp = new List<int>();
                foreach (ClockCell cell2 in this.Cells)
                {
                    if (IsPeer(cell.index, cell2.index))
                        temp.Add(cell2.index);
                }
                Peers.Add(cell.index, temp);
            }
        }

        public object Clone()
        {
            var clone = new ClockSudokuPuzzle();
            foreach (ClockCell cell in this.Cells)
            {
                clone.Cells.Add((ClockCell)cell.Clone());
            }

            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ClockCell cell in this.Cells)
            {
                //if (cell.index % 27 == 0)
                //    sb.AppendLine();
                //if (cell.index % 9 == 0)
                //    sb.AppendLine();
                //if (cell.index % 3 == 0)
                //    sb.Append("  |  ");
                sb.Append(cell);

            }

            return sb.ToString();
        }

        public string toStringList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ClockCell cell in this.Cells)
            {
                sb.Append(cell.stringValue.ToString());
            }
            return sb.ToString();
        }

        public bool isEqual(ClockSudokuPuzzle puzzle)
        {
            foreach (ClockCell cell in this.Cells)
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

        public ClockSudokuPuzzle ApplyConstraints(int cellIndex, int value)
        {
            ClockSudokuPuzzle puzzle = (ClockSudokuPuzzle)this.Clone();
            puzzle.Cells[cellIndex].value = value;
            if (value <= 9)
                puzzle.Cells[cellIndex].stringValue = value.ToString();
            else if (value == 10)
                puzzle.Cells[cellIndex].stringValue = "A";
            else if (value == 10)
                puzzle.Cells[cellIndex].stringValue = "B";
            else 
                puzzle.Cells[cellIndex].stringValue = "C";
            foreach (int peerIndex in Peers[cellIndex])
            {
                var newCandidates = puzzle.Cells[peerIndex].candidates.Except(new List<int> { value });
                puzzle.Cells[peerIndex].candidates = newCandidates.ToList<int>();
                if (!newCandidates.Any())
                    return null;
                puzzle.Cells[cellIndex].candidates = new List<int>(Enumerable.Repeat(0, 12));
            }
            return puzzle;
        }

        public ClockSudokuPuzzle PlaceValue(int cellIndex, int value)
        {
            if (!this.Cells[cellIndex].candidates.Contains(value))
                return null;

            var puzzle = ApplyConstraints(cellIndex, value);
            if (puzzle == null)
                return null;
            return puzzle;

        }

        public static ClockSudokuPuzzle RandomGrid()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 72; i++)
            {
                sb.Append('0');
            }
            ClockSudokuPuzzle puzzle = new ClockSudokuPuzzle(sb.ToString());

            var rand = new Random();

            while (true)
            {
                int[] UnsolvedCellIndexes = puzzle.Cells
                    .Select((cands, index) => new { cands, index })     //Project to a new sequence of candidates and index (an anonymous type behaving like a tuple)
                    .Where(t => t.cands.candidates.Count >= 2)                    //Filter to cells with at least 2 candidates
                    .Select(u => u.index)                               //Project the tuple to only the index
                    .ToArray();

                int cellIndex = UnsolvedCellIndexes[rand.Next(UnsolvedCellIndexes.Length)];
                int candidateValue = puzzle.Cells[cellIndex].candidates[rand.Next(puzzle.Cells[cellIndex].candidates.Count)];

                ClockSudokuPuzzle workingPuzzle = puzzle.PlaceValue(cellIndex, candidateValue);
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

        public static ClockSudokuPuzzle RandomGrid(string seed)
        {
            ClockSudokuPuzzle puzzle = new ClockSudokuPuzzle(seed);

            var rand = new Random();

            while (true)
            {
                int[] UnsolvedCellIndexes = puzzle.Cells
                    .Select((cands, index) => new { cands, index })     //Project to a new sequence of candidates and index (an anonymous type behaving like a tuple)
                    .Where(t => t.cands.candidates.Count >= 2)                    //Filter to cells with at least 2 candidates
                    .Select(u => u.index)                               //Project the tuple to only the index
                    .ToArray();

                int cellIndex = UnsolvedCellIndexes[rand.Next(UnsolvedCellIndexes.Length)];
                int candidateValue = puzzle.Cells[cellIndex].candidates[rand.Next(puzzle.Cells[cellIndex].candidates.Count)];

                ClockSudokuPuzzle workingPuzzle = puzzle.PlaceValue(cellIndex, candidateValue);
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

        public ClockSudokuPuzzle SolveUsingGuess(Func<ClockSudokuPuzzle, bool> SolutionFunc = null)
        {
            if (this.Cells.All(mycell => mycell.value != 0))
                return (SolutionFunc != null && SolutionFunc(this)) ? null : this;

            int ActiveCell = FindWorkingCell();
            foreach (int guess in this.Cells[ActiveCell].candidates)
            {
                ClockSudokuPuzzle puzzle;
                if ((puzzle = PlaceValue(ActiveCell, guess)) != null)
                    if ((puzzle = puzzle.SolveUsingGuess(SolutionFunc)) != null)
                        return puzzle;
            }
            return null;
        }
        public static List<ClockSudokuPuzzle> MultiSolve(ClockSudokuPuzzle input, int MaximumSolutions = -1)
        {
            var Solutions = new List<ClockSudokuPuzzle>();
            input.SolveUsingGuess(p =>
            {
                Solutions.Add(p);
                return Solutions.Count() < MaximumSolutions || MaximumSolutions == -1;
            });
            return Solutions;
        }
        public int FindWorkingCell()
        {
            int minCandidates = (from cell in this.Cells where !cell.isFixed && cell.value == 0 select cell).Min(cell => cell.candidates.Count);

            foreach (ClockCell cell in this.Cells)
            {
                if (cell.isFixed || cell.value != 0)
                    continue;
                if (cell.candidates.Count == minCandidates)
                    return cell.index;
            }
            return -1;
        }
        public static ClockSudokuPuzzle GenerateSudokuPuzzle2(ClockSudokuPuzzle fullGrid)
        {
            ClockSudokuPuzzle puzzle = new ClockSudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            while (count < 71)
            {
                /////// perform digging holes ///////
                string stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed
                                     select cell.index).ToList<int>();
                int randomCellIndex = rand.Next(Indexes.Count);
                StringBuilder sb = new StringBuilder(stringpuzzle);
                sb[Indexes[randomCellIndex]] = '0';
                string newstringpuzzle = sb.ToString();
                //Console.WriteLine(newstringpuzzle);
                ClockSudokuPuzzle temppuzzle = new ClockSudokuPuzzle(newstringpuzzle);
                var solutions = ClockSudokuPuzzle.MultiSolve(temppuzzle);
                if (solutions.Count == 1)
                {
                    puzzle = temppuzzle;
                    //if (temppuzzle.isEqual(puzzle))
                    //    break;
                }
                ////////////////////////////////////
                count++;
            }

            return puzzle;
        }
        public static ClockSudokuPuzzle GenerateSudokuPuzzle(ClockSudokuPuzzle fullGrid)
        {
            ClockSudokuPuzzle puzzle = new ClockSudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new ClockSudokuPuzzle(stringpuzzle);
            while (!Continue && puzzle.NumberOfEmptyCells() <= 41)
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
                    ClockSudokuPuzzle temppuzzle = new ClockSudokuPuzzle(newstringpuzzle);
                    var solutions = ClockSudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = ClockSudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
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
                    puzzle = new ClockSudokuPuzzle(newstringpuzzle2);
                }
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }
        public int NumberOfEmptyCells()
        {
            return this.toStringList().ToList().FindAll(ch => ch == '0').Count;
        }
        public bool isSolved()
        {
            if (this.toStringList().IndexOf('0') == -1)
                return true;
            else
                return false;
        }
    }
}
