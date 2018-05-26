using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmGenerator
{
    public enum MySudokuRules { NakedSingle, HiddenSingle, LockedCandidatesPointing, LockedCandidatesClaiming, LockedPair,LockedTriple, HiddenPair, Other, NotValid, XWing }
    public static class MySudokuHumanSolver
    {
        public static int[]Rows=new int[]{
            1,1,1,1,1,1,1,1,1,
            2,2,2,2,2,2,2,2,2,
            3,3,3,3,3,3,3,3,3,
            4,4,4,4,4,4,4,4,4,
            5,5,5,5,5,5,5,5,5,
            6,6,6,6,6,6,6,6,6,
            7,7,7,7,7,7,7,7,7,
            8,8,8,8,8,8,8,8,8,
            9,9,9,9,9,9,9,9,9
        };
        public static int[] Cols = new int[]{
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
            1,2,3,4,5,6,7,8,9,
        };
        public static int[] Blocks = new int[]{
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

        public static bool onSameRow(int index1, int index2)
        {
            return index1 / 9 == index2 / 9;
        }
        public static bool onSameColumn(int index1, int index2)
        {
            return index1 % 9 == index2 % 9;
        }
        public static bool onSameBlock(int index1, int index2)
        {
            return (index1 / 9 / 3 == index2 / 9 / 3) && (index1 % 9 / 3 == index2 % 9 / 3);
        }

        public static MySudokuPuzzle PutValue(MySudokuPuzzle input, int cellIndex, int value)
        {
            foreach (int peerIndex in MySudokuPuzzle.Peers[cellIndex])
            {
                input.Cells[peerIndex].candidates = (input.Cells[peerIndex].candidates.Except(new List<int> { value })).ToList<int>();
            }
            input.Cells[cellIndex].value = value;
            input.Cells[cellIndex].isFixed = true;
            return input;
        }

        public static MySudokuPuzzle SolveStep(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = SolveStepUsingNakedSingle(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            result= SolveStepUsingHiddenSingle(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedCandidatePointing(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedCandidateClaiming(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedPair(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedTriple(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            result = SolveStepUsingHiddenPair(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            result = SolveStepUsingXWing(input, out step);
            if (step.rule != MySudokuRules.NotValid)
                return result;
            return input;
               
        }

        public static MySudokuPuzzle SolveStepUsingXWing(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - X-Wing /////////
            foreach (MyCell cell in result.Cells.FindAll(c=>!c.isFixed))
            {
                foreach (int candidate in cell.candidates)
                {
                    List<MyCell> rowPeerExceptCellContainingThisCandidate = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && Rows[cell.index] == Rows[c.index] && c.candidates.Contains(candidate));
                    if (rowPeerExceptCellContainingThisCandidate.Count == 1)
                    {
                        // we have the first row that contains candidate and this candidate only appears in two cells of the row
                        // lets check for two other cells in other row
                        List<MyCell> columnPeerExceptCellContainingThisCandidate = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && Cols[cell.index] == Cols[c.index] && c.candidates.Contains(candidate));
                        if (columnPeerExceptCellContainingThisCandidate.Count > 0)
                        {
                            foreach (MyCell cell2 in columnPeerExceptCellContainingThisCandidate)
                            {
                                List<MyCell> rowPeerForColumnPeerCell = result.Cells.FindAll(c => !c.isFixed && c.index != cell2.index && Rows[c.index] == Rows[cell2.index] && c.candidates.Contains(candidate));
                                if (rowPeerForColumnPeerCell.Count == 1)
                                    if (Cols[rowPeerForColumnPeerCell[0].index] == Cols[rowPeerExceptCellContainingThisCandidate[0].index])
                                    {
                                        // we have found 4 cells containing x wing 
                                        // let's check for affected cells ..
                                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && c.index != cell2.index && Cols[c.index] == Cols[cell.index] && Cols[c.index] == Cols[cell2.index] && c.candidates.Contains(candidate));
                                        affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && c.index != rowPeerExceptCellContainingThisCandidate[0].index && c.index != rowPeerForColumnPeerCell[0].index && Cols[c.index] == Cols[rowPeerExceptCellContainingThisCandidate[0].index] && Cols[c.index] == Cols[rowPeerForColumnPeerCell[0].index] && c.candidates.Contains(candidate)));
                                        if (affectedCells.Count > 0)
                                        {
                                            // X WING IS FOUND 
                                            List<int> indexesAffected = (
                                                from c in affectedCells
                                                select c.index).ToList<int>();
                                            foreach (MyCell affectedCell in affectedCells)
                                            {
                                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                                            }
                                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate }, new List<int> { cell.index, rowPeerExceptCellContainingThisCandidate[0].index, cell2.index, rowPeerForColumnPeerCell[0].index }, indexesAffected, MySudokuRules.XWing);
                                            return result;
                                        }
                                    }
                            }
                        }
                    }
                    /////////////
                    List<MyCell> columnPeerExceptCellContainingThisCandidate2 = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && Cols[cell.index] == Cols[c.index] && c.candidates.Contains(candidate));
                    if (columnPeerExceptCellContainingThisCandidate2.Count == 1)
                    {
                        // we have the first column that contains candidate and this candidate only appears in two cells of the column
                        // lets check for two other cells ib other column
                        List<MyCell> rowPeerExceptCellContainingThisCandidate2 = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && Rows[cell.index] == Rows[c.index] && c.candidates.Contains(candidate));
                        if (rowPeerExceptCellContainingThisCandidate2.Count > 0)
                        {
                            foreach (MyCell cell2 in rowPeerExceptCellContainingThisCandidate2)
                            {
                                List<MyCell> columnPeerForRowPeerCell = result.Cells.FindAll(c => !c.isFixed && c.index != cell2.index && Cols[c.index] == Cols[cell2.index] && c.candidates.Contains(candidate));
                                if(columnPeerForRowPeerCell.Count==1)
                                    if (Rows[columnPeerForRowPeerCell[0].index] == Rows[columnPeerExceptCellContainingThisCandidate2[0].index])
                                    {
                                        // we have found 4 cells containing x wing 
                                        // let's check for affected cells ..
                                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && c.index != cell2.index && Rows[c.index] == Rows[cell.index] && Rows[c.index] == Rows[cell2.index] && c.candidates.Contains(candidate));
                                        affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && c.index != columnPeerExceptCellContainingThisCandidate2[0].index && c.index != columnPeerForRowPeerCell[0].index && Rows[c.index] == Rows[columnPeerExceptCellContainingThisCandidate2[0].index] && Rows[c.index] == Rows[columnPeerForRowPeerCell[0].index] && c.candidates.Contains(candidate)));
                                        if (affectedCells.Count > 0)
                                        {
                                            // X WING IS FOUND 
                                            List<int> indexesAffected = (
                                                from c in affectedCells
                                                select c.index).ToList<int>();
                                            foreach (MyCell affectedCell in affectedCells)
                                            {
                                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                                            }
                                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate }, new List<int> { cell.index, columnPeerExceptCellContainingThisCandidate2[0].index, cell2.index, columnPeerForRowPeerCell[0].index }, indexesAffected, MySudokuRules.XWing);
                                            return result;
                                        }
                                    }
                            }
                        }
                    }
                }
            }
            ///////// END   - X-Wing /////////
            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }

        public static MySudokuPuzzle SolveStepUsingHiddenPair(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - Hidden Pair /////////
            foreach (MyCell cell in result.Cells.FindAll(c=>!c.isFixed))
            {
                List<MyCell> rowPeersExceptCell = result.Cells.FindAll(c=>!c.isFixed && Rows[c.index]==Rows[cell.index] && c.index !=cell.index);
                List<MyCell> columnPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && Cols[c.index] == Cols[cell.index] && c.index != cell.index);
                List<MyCell> blockPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && Blocks[c.index] == Blocks[cell.index] && c.index != cell.index);
                // iterate over all candidates to see if there are candidates that can be used as candidate 1 in Hidden Pair

                foreach (int candidate in cell.candidates)
                {
                    List<MyCell> rowPeersCellsContainingCandidate1 = rowPeersExceptCell.FindAll(c => c.candidates.Contains(candidate));
                    List<MyCell> columnPeersCellsContainingCandidate1 = columnPeersExceptCell.FindAll(c => c.candidates.Contains(candidate));
                    List<MyCell> blockPeersCellsContainingCandidate1 = blockPeersExceptCell.FindAll(c => c.candidates.Contains(candidate));
                    if (rowPeersCellsContainingCandidate1.Count == 1)
                    {
                        // we have found candidate 1
                        foreach (int candidate2 in cell.candidates.Except(new List<int> { candidate }).ToList<int>())
                        {
                            // check to see if the cell containing candidate 1 also contains candidate 2
                            if (rowPeersCellsContainingCandidate1[0].candidates.Contains(candidate2))
                            {
                                // check to see if the other row peers except cell 1 and cell 2 doesn't contains this candidate
                                if (!rowPeersExceptCell.Except(rowPeersCellsContainingCandidate1).ToList<MyCell>().FindAll(c => c.candidates.Contains(candidate2)).Any())
                                {
                                    // no other cells in the row contains candidate2
                                    // we have found two cells that contains two candidates and no other cells contains these candidates
                                    bool isAffected = false;
                                    List<int> candidatesToRemoveFromCell = new List<int>();
                                    List<int> candidatesToRemoveFromCell2 = new List<int>();
                                    foreach (int cand in rowPeersCellsContainingCandidate1[0].candidates)
                                    {
                                        if (cand != candidate && cand != candidate2)
                                        {
                                            candidatesToRemoveFromCell.Add(cand);
                                            isAffected = true;
                                        }
                                    }
                                    if (candidatesToRemoveFromCell.Count>0)
                                    {
                                        foreach (int candToRemove in candidatesToRemoveFromCell)
                                        {
                                            result.Cells[rowPeersCellsContainingCandidate1[0].index].candidates.Remove(candToRemove);
                                        }
                                    }
                                    foreach (int cand in cell.candidates)
                                    {
                                        if (cand != candidate && cand != candidate2)
                                        {
                                            candidatesToRemoveFromCell2.Add(cand);
                                            isAffected = true;
                                        }
                                    }
                                    if (candidatesToRemoveFromCell2.Count > 0)
                                    {
                                        foreach (int candToRemove in candidatesToRemoveFromCell2)
                                        {
                                            result.Cells[cell.index].candidates.Remove(candToRemove);
                                        }
                                    }
                                    if (isAffected)
                                    {
                                        step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate, candidate2 }, new List<int> { cell.index, rowPeersCellsContainingCandidate1[0].index }, new List<int> { cell.index, rowPeersCellsContainingCandidate1[0].index }, MySudokuRules.HiddenPair);
                                        return result;
                                    }
                                }
                            }

                        }
                    }
                    if (columnPeersCellsContainingCandidate1.Count == 1)
                    {
                        // we have found candidate 1
                        foreach (int candidate2 in cell.candidates.Except(new List<int> { candidate }).ToList<int>())
                        {
                            // check to see if the cell containing candidate 1 also contains candidate 2
                            if (columnPeersCellsContainingCandidate1[0].candidates.Contains(candidate2))
                            {
                                // check to see if the other column peers except cell 1 and cell 2 doesn't contains this candidate
                                if (!columnPeersExceptCell.Except(columnPeersCellsContainingCandidate1).ToList<MyCell>().FindAll(c => c.candidates.Contains(candidate2)).Any())
                                {
                                    // no other cells in the column contains candidate2
                                    // we have found two cells that contains two candidates and no other cells contains these candidates
                                    bool isAffected = false;
                                    List<int> candidatesToRemoveFromCell = new List<int>();
                                    List<int> candidatesToRemoveFromCell2 = new List<int>();
                                    foreach (int cand in columnPeersCellsContainingCandidate1[0].candidates)
                                    {
                                        if (cand != candidate && cand != candidate2)
                                        {
                                            candidatesToRemoveFromCell.Add(cand);
                                            isAffected = true;
                                        }
                                    }
                                    if (candidatesToRemoveFromCell.Count > 0)
                                    {
                                        foreach (int candToRemove in candidatesToRemoveFromCell)
                                        {
                                            result.Cells[columnPeersCellsContainingCandidate1[0].index].candidates.Remove(candToRemove);
                                        }
                                    }
                                    foreach (int cand in cell.candidates)
                                    {
                                        if (cand != candidate && cand != candidate2)
                                        {
                                            candidatesToRemoveFromCell2.Add(cand);
                                            isAffected = true;
                                        }
                                    }
                                    if (candidatesToRemoveFromCell2.Count > 0)
                                    {
                                        foreach (int candToRemove in candidatesToRemoveFromCell2)
                                        {
                                            result.Cells[cell.index].candidates.Remove(candToRemove);
                                        }
                                    }
                                    if (isAffected)
                                    {
                                        step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate, candidate2 }, new List<int> { cell.index, columnPeersCellsContainingCandidate1[0].index }, new List<int> { cell.index, columnPeersCellsContainingCandidate1[0].index }, MySudokuRules.HiddenPair);
                                        return result;
                                    }
                                }
                            }

                        }
                    }
                    if (blockPeersCellsContainingCandidate1.Count == 1)
                    {
                        // we have found candidate 1
                        foreach (int candidate2 in cell.candidates.Except(new List<int> { candidate }).ToList<int>())
                        {
                            // check to see if the cell containing candidate 1 also contains candidate 2
                            if (blockPeersCellsContainingCandidate1[0].candidates.Contains(candidate2))
                            {
                                // check to see if the other block peers except cell 1 and cell 2 doesn't contains this candidate
                                if (!blockPeersExceptCell.Except(blockPeersCellsContainingCandidate1).ToList<MyCell>().FindAll(c => c.candidates.Contains(candidate2)).Any())
                                {
                                    // no other cells in the block contains candidate2
                                    // we have found two cells that contains two candidates and no other cells contains these candidates
                                    bool isAffected = false;
                                    List<int> candidatesToRemoveFromCell = new List<int>();
                                    List<int> candidatesToRemoveFromCell2 = new List<int>();
                                    foreach (int cand in blockPeersCellsContainingCandidate1[0].candidates)
                                    {
                                        if (cand != candidate && cand != candidate2)
                                        {
                                            candidatesToRemoveFromCell.Add(cand);
                                            isAffected = true;
                                        }
                                    }
                                    if (candidatesToRemoveFromCell.Count > 0)
                                    {
                                        foreach (int candToRemove in candidatesToRemoveFromCell)
                                        {
                                            result.Cells[blockPeersCellsContainingCandidate1[0].index].candidates.Remove(candToRemove);
                                        }
                                    }
                                    foreach (int cand in cell.candidates)
                                    {
                                        if (cand != candidate && cand != candidate2)
                                        {
                                            candidatesToRemoveFromCell2.Add(cand);
                                            isAffected = true;
                                        }
                                    }
                                    if (candidatesToRemoveFromCell2.Count > 0)
                                    {
                                        foreach (int candToRemove in candidatesToRemoveFromCell2)
                                        {
                                            result.Cells[cell.index].candidates.Remove(candToRemove);
                                        }
                                    }
                                    if (isAffected)
                                    {
                                        step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate, candidate2 }, new List<int> { cell.index, blockPeersCellsContainingCandidate1[0].index }, new List<int> { cell.index, blockPeersCellsContainingCandidate1[0].index }, MySudokuRules.HiddenPair);
                                        return result;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            ///////// END   - Locked Candidate /////////
            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }

        public static MySudokuPuzzle SolveStepUsingLockedTriple(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - Locked Candidate /////////
            foreach (MyCell cell in result.Cells.FindAll(c => !c.isFixed))
            {
                // find row peers with candidates that union with candidates in cell equal 3 in length
                List<MyCell> rowPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && Rows[c.index] == Rows[cell.index] && c.index != cell.index && (cell.candidates.Union(c.candidates).ToList<int>()).Count == 3);
                List<MyCell> columnPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && Cols[c.index] == Cols[cell.index] && c.index != cell.index && (cell.candidates.Union(c.candidates).ToList<int>()).Count == 3);
                List<MyCell> blockPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && Blocks[c.index] == Blocks[cell.index] && c.index != cell.index && (cell.candidates.Union(c.candidates).ToList<int>()).Count == 3);
                if (rowPeersExceptCell.Count > 0)
                {
                    // iterate over row peers to find another cell that implement the rules of Locked Triples
                    foreach (MyCell cell2 in rowPeersExceptCell)
                    {
                        List<int> unionCandidatesBetweenCellAndCell2 = cell.candidates.Union(cell2.candidates).ToList<int>();
                        List<MyCell> rowPeersExceptCellAndCell2 = (rowPeersExceptCell.Except(new List<MyCell> { cell2 })).ToList<MyCell>();
                        if (rowPeersExceptCellAndCell2.Count > 0)
                        {
                            foreach (MyCell cell3 in rowPeersExceptCellAndCell2)
                            {
                                List<int> unionCandidatesBetweenCellAndCell3 = cell.candidates.Union(cell3.candidates).ToList<int>();
                                List<int> unionCandidatesBetweenCell2AndCell3 = cell2.candidates.Union(cell3.candidates).ToList<int>();
                                if (unionCandidatesBetweenCellAndCell2.Count == 3 && unionCandidatesBetweenCellAndCell3.Count == 3 && unionCandidatesBetweenCell2AndCell3.Count == 3)
                                {
                                    if ((unionCandidatesBetweenCellAndCell2.Intersect(unionCandidatesBetweenCellAndCell3).Intersect(unionCandidatesBetweenCell2AndCell3)).ToList<int>().Count == 3)
                                    {
                                        // we have three cells that satisfy the LOCKED TRIPLE conditions
                                        // let's check the afftected cells .
                                        
                                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && Rows[c.index]==Rows[cell.index] && c.index!=cell.index && c.index!=cell2.index && c.index!=cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any());
                                        if ((Blocks[cell.index] == Blocks[cell2.index]) && (Blocks[cell2.index] == Blocks[cell3.index]))
                                        {
                                            // These three cells are on the same block also ..
                                            affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && Blocks[c.index] == Blocks[cell.index] && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any()));
                                        }
                                        if (affectedCells.Count > 0)
                                        {
                                            // WE HAVE FOUND ROW LOCKED TRIPLE
                                            foreach (MyCell affectedCell in affectedCells)
                                            {
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[0]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[0]);
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[1]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[1]);
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[2]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[2]);
                                            }
                                            List<int> indexesAffected = (
                                                from c in affectedCells
                                                select c.index).ToList<int>();
                                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, unionCandidatesBetweenCellAndCell2, new List<int> { cell.index, cell2.index, cell3.index }, indexesAffected, MySudokuRules.LockedTriple);
                                            return result;
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
                if (columnPeersExceptCell.Count > 0)
                {
                    // iterate over column peers to find another cell that implement the rules of Locked Triples
                    foreach (MyCell cell2 in columnPeersExceptCell)
                    {
                        List<int> unionCandidatesBetweenCellAndCell2 = cell.candidates.Union(cell2.candidates).ToList<int>();
                        List<MyCell> columnPeersExceptCellAndCell2 = (columnPeersExceptCell.Except(new List<MyCell> { cell2 })).ToList<MyCell>();
                        if (columnPeersExceptCellAndCell2.Count > 0)
                        {
                            foreach (MyCell cell3 in columnPeersExceptCellAndCell2)
                            {
                                List<int> unionCandidatesBetweenCellAndCell3 = cell.candidates.Union(cell3.candidates).ToList<int>();
                                List<int> unionCandidatesBetweenCell2AndCell3 = cell2.candidates.Union(cell3.candidates).ToList<int>();
                                if (unionCandidatesBetweenCellAndCell2.Count == 3 && unionCandidatesBetweenCellAndCell3.Count == 3 && unionCandidatesBetweenCell2AndCell3.Count == 3)
                                {
                                    if ((unionCandidatesBetweenCellAndCell2.Intersect(unionCandidatesBetweenCellAndCell3).Intersect(unionCandidatesBetweenCell2AndCell3)).ToList<int>().Count == 3)
                                    {
                                        // we have three cells that satisfy the LOCKED TRIPLE conditions
                                        // let's check the afftected cells .

                                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && Cols[c.index] == Cols[cell.index] && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any());
                                        if ((Blocks[cell.index] == Blocks[cell2.index]) && (Blocks[cell2.index] == Blocks[cell3.index]))
                                        {
                                            // These three cells are on the same block also ..
                                            affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && Blocks[c.index] == Blocks[cell.index] && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any()));
                                        }
                                        if (affectedCells.Count > 0)
                                        {
                                            // WE HAVE FOUND COLUMN LOCKED TRIPLE
                                            foreach (MyCell affectedCell in affectedCells)
                                            {
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[0]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[0]);
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[1]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[1]);
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[2]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[2]);
                                            }
                                            List<int> indexesAffected = (
                                                from c in affectedCells
                                                select c.index).ToList<int>();
                                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, unionCandidatesBetweenCellAndCell2, new List<int> { cell.index, cell2.index, cell3.index }, indexesAffected, MySudokuRules.LockedTriple);
                                            return result;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                if (blockPeersExceptCell.Count > 0)
                {
                    // iterate over block peers to find another cell that implement the rules of Locked Triples
                    foreach (MyCell cell2 in blockPeersExceptCell)
                    {
                        List<int> unionCandidatesBetweenCellAndCell2 = cell.candidates.Union(cell2.candidates).ToList<int>();
                        List<MyCell> blockPeersExceptCellAndCell2 = (blockPeersExceptCell.Except(new List<MyCell> { cell2 })).ToList<MyCell>();
                        if (blockPeersExceptCellAndCell2.Count > 0)
                        {
                            foreach (MyCell cell3 in blockPeersExceptCellAndCell2)
                            {
                                List<int> unionCandidatesBetweenCellAndCell3 = cell.candidates.Union(cell3.candidates).ToList<int>();
                                List<int> unionCandidatesBetweenCell2AndCell3 = cell2.candidates.Union(cell3.candidates).ToList<int>();
                                if (unionCandidatesBetweenCellAndCell2.Count == 3 && unionCandidatesBetweenCellAndCell3.Count == 3 && unionCandidatesBetweenCell2AndCell3.Count == 3)
                                {
                                    if ((unionCandidatesBetweenCellAndCell2.Intersect(unionCandidatesBetweenCellAndCell3).Intersect(unionCandidatesBetweenCell2AndCell3)).ToList<int>().Count == 3)
                                    {
                                        // we have three cells that satisfy the LOCKED TRIPLE conditions
                                        // let's check the afftected cells .

                                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && Blocks[c.index] == Blocks[cell.index] && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any());
                                        
                                        if (affectedCells.Count > 0)
                                        {
                                            // WE HAVE FOUND COLUMN LOCKED TRIPLE
                                            foreach (MyCell affectedCell in affectedCells)
                                            {
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[0]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[0]);
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[1]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[1]);
                                                if (affectedCell.candidates.Contains(unionCandidatesBetweenCellAndCell2[2]))
                                                    result.Cells[affectedCell.index].candidates.Remove(unionCandidatesBetweenCellAndCell2[2]);
                                            }
                                            List<int> indexesAffected = (
                                                from c in affectedCells
                                                select c.index).ToList<int>();
                                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, unionCandidatesBetweenCellAndCell2, new List<int> { cell.index, cell2.index, cell3.index }, indexesAffected, MySudokuRules.LockedTriple);
                                            return result;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            ///////// END   - Locked Candidate /////////
            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }

        public static MySudokuPuzzle SolveStepUsingLockedCandidatePointing(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - Locked Candidate /////////
            foreach (MyCell cell in result.Cells.FindAll(c=>!c.isFixed))
            {
                //// get the list of all cells in the same block as the cell
                int blockIndex = Blocks[cell.index];
                List<MyCell> blockCells = result.Cells.FindAll(c => Blocks[c.index] == blockIndex && !c.isFixed);
                //// iterate over all candidates in the cell
                foreach (int candidate in cell.candidates)
                {
                    //// get the block cells that contains this candidate
                    List<MyCell> cellsContainingLockedCandidate = blockCells.FindAll(c => c.candidates.Contains(candidate) && c.index!=cell.index);
                    bool rowLockedCandidateFound = true;
                    
                    //// check to see if the locked candidate on the same row
                    foreach (MyCell c in cellsContainingLockedCandidate)
                    {
                        if (!onSameRow(cell.index, c.index))
                            rowLockedCandidateFound = false;
                    }
                    if (rowLockedCandidateFound) // we have found a cells containing candidate on the same row
                    {
                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && Rows[c.index] == Rows[cell.index] && Blocks[c.index] != Blocks[cell.index] && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND ROW LOCKED CANDIDATE
                            foreach (MyCell affectedCell in affectedCells)
                            {
                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                            }
                            List<int> indexesRuleAppliedTo = (
                                from c in cellsContainingLockedCandidate
                                select c.index).ToList<int>();
                            indexesRuleAppliedTo.Add(cell.index);
                            List<int> indexesAffected = (
                                from c in affectedCells
                                select c.index).ToList<int>();
                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate }, indexesRuleAppliedTo, indexesAffected, MySudokuRules.LockedCandidatesPointing);
                            return result;
                        }
                    }

                    bool columnLockedCandidateFound = true;

                    //// check to see if the locked candidate on the same column
                    foreach (MyCell c in cellsContainingLockedCandidate)
                    {
                        if (!onSameColumn(cell.index, c.index))
                            columnLockedCandidateFound = false;
                    }
                    if (columnLockedCandidateFound) // we have found a cells containing candidate on the same column
                    {
                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && Cols[c.index] == Cols[cell.index] && Blocks[c.index] != Blocks[cell.index] && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND COLUMN LOCKED CANDIDATE
                            foreach (MyCell affectedCell in affectedCells)
                            {
                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                            }
                            List<int> indexesRuleAppliedTo = (
                                from c in cellsContainingLockedCandidate
                                select c.index).ToList<int>();
                            indexesRuleAppliedTo.Add(cell.index);
                            List<int> indexesAffected = (
                                from c in affectedCells
                                select c.index).ToList<int>();
                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates,new List<int>{candidate}, indexesRuleAppliedTo, indexesAffected, MySudokuRules.LockedCandidatesPointing);
                            return result;
                        }
                    }
                }
            }
            ///////// END   - Locked Candidate /////////
            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }

        public static MySudokuPuzzle SolveStepUsingLockedCandidateClaiming(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - Locked Candidate /////////
            foreach (MyCell cell in result.Cells.FindAll(c => !c.isFixed))
            {
                //// get the list of all cells in the same row as the cell
                int rowIndex = Rows[cell.index];
                //// get the list of all cells in the same column as the cell
                int colIndex = Cols[cell.index];
                List<MyCell> rowCells = result.Cells.FindAll(c => Rows[c.index] == rowIndex && !c.isFixed);
                List<MyCell> colCells = result.Cells.FindAll(c => Cols[c.index] == colIndex && !c.isFixed);
                //// iterate over all candidates in the cell
                foreach (int candidate in cell.candidates)
                {
                    //// get the row cells that contains this candidate
                    List<MyCell> cellsContainingLockedCandidate = rowCells.FindAll(c => c.candidates.Contains(candidate) && c.index != cell.index);
                    //// get the row cells that contains this candidate
                    List<MyCell> cellsContainingLockedCandidate2 = colCells.FindAll(c => c.candidates.Contains(candidate) && c.index != cell.index);
                    bool blockLockedCandidateFound = true;

                    //// check to see if the locked candidate on the same block
                    foreach (MyCell c in cellsContainingLockedCandidate)
                    {
                        if (!onSameBlock(cell.index, c.index))
                            blockLockedCandidateFound = false;
                    }
                    if (blockLockedCandidateFound) // we have found a cells containing candidate on the same block
                    {
                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && Blocks[c.index] == Blocks[cell.index] && Rows[c.index] != Rows[cell.index] && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND BLOCK LOCKED CANDIDATE
                            foreach (MyCell affectedCell in affectedCells)
                            {
                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                            }
                            List<int> indexesRuleAppliedTo = (
                                from c in cellsContainingLockedCandidate
                                select c.index).ToList<int>();
                            indexesRuleAppliedTo.Add(cell.index);
                            List<int> indexesAffected = (
                                from c in affectedCells
                                select c.index).ToList<int>();
                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate }, indexesRuleAppliedTo, indexesAffected, MySudokuRules.LockedCandidatesClaiming);
                            return result;
                        }
                    }

                    blockLockedCandidateFound = true;
                    //// check to see if the locked candidate on the same block
                    foreach (MyCell c in cellsContainingLockedCandidate2)
                    {
                        if (!onSameBlock(cell.index, c.index))
                            blockLockedCandidateFound = false;
                    }
                    if (blockLockedCandidateFound) // we have found a cells containing candidate on the same block
                    {
                        List<MyCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && Blocks[c.index] == Blocks[cell.index] && Cols[c.index] != Cols[cell.index] && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND BLOCK LOCKED CANDIDATE
                            foreach (MyCell affectedCell in affectedCells)
                            {
                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                            }
                            List<int> indexesRuleAppliedTo = (
                                from c in cellsContainingLockedCandidate2
                                select c.index).ToList<int>();
                            indexesRuleAppliedTo.Add(cell.index);
                            List<int> indexesAffected = (
                                from c in affectedCells
                                select c.index).ToList<int>();
                            step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates, new List<int> { candidate }, indexesRuleAppliedTo, indexesAffected, MySudokuRules.LockedCandidatesClaiming);
                            return result;
                        }
                    }

                }
            }
            ///////// END   - Locked Candidate /////////
            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }

        public static MySudokuPuzzle SolveStepUsingLockedPair(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - Locked Pair /////////
            List<MyCell> cellsWithTwoCandidates = result.Cells.FindAll(c => !c.isFixed && c.candidates.Count == 2);
            foreach (MyCell cell in cellsWithTwoCandidates)
            {
                List<MyCell> cellsEqualinCandidates = (cellsWithTwoCandidates.Except(new List<MyCell>{cell})).ToList<MyCell>().FindAll(c => cell.isEqualInCandidates(c) && MySudokuPuzzle.IsPeer(cell.index,c.index));
                if(cellsEqualinCandidates.Count>0)
                {
                    int index1 = cell.index;
                    int index2 = cellsEqualinCandidates[0].index;
                    List<int> affectedCells=new List<int>();
                    if (index1 / 9 == index2 / 9) // the Locked Pair is in the same Row
                    {
                        affectedCells.AddRange((
                    from rowpeerindex in MySudokuPuzzle.Peers[index1]
                    where rowpeerindex / 9 == index1 / 9
                    && !result.Cells[rowpeerindex].isFixed
                    && rowpeerindex != index1 && rowpeerindex != index2
                    && result.Cells[rowpeerindex].candidates.Intersect(cell.candidates).Any()
                    select rowpeerindex).ToList<int>());
                    }
                    if(index1%9==index2%9)
                    {
                        affectedCells.AddRange((
                    from colpeerindex in MySudokuPuzzle.Peers[index1]
                    where colpeerindex % 9 == cell.index % 9
                    && !result.Cells[colpeerindex].isFixed
                    && colpeerindex != index1 && colpeerindex != index2
                    && result.Cells[colpeerindex].candidates.Intersect(cell.candidates).Any()
                    select colpeerindex).ToList<int>());
                    }
                    if (index1 / 9 / 3 == index2 / 9 / 3 && index1 % 9 / 3 == index2 % 9 / 3)
                    {
                        affectedCells.AddRange((
                    from blockpeerindex in MySudokuPuzzle.Peers[index1]
                    where blockpeerindex / 9 / 3 == cell.index / 9 / 3 && blockpeerindex % 9 / 3 == cell.index % 9 / 3
                    && !result.Cells[blockpeerindex].isFixed 
                    && blockpeerindex != index1 && blockpeerindex != index2
                    && result.Cells[blockpeerindex].candidates.Intersect(cell.candidates).Any()
                    select blockpeerindex).ToList<int>());
                    }

                    if (affectedCells.Count > 0)
                    {
                        foreach (int affectedIndex in affectedCells)
                        {
                            foreach (int cand in cell.candidates)
                            {
                                result.Cells[affectedIndex].candidates.Remove(cand);    
                            }
                            
                        }
                        step = new MySudokuSolverStep(MySudokuStepType.RemoveCandidates,cell.candidates, new List<int> { cell.index, cellsEqualinCandidates[0].index }, affectedCells, MySudokuRules.LockedPair);
                        return result;
                    }
                }
            }
            ///////// END   - Locked Pair /////////
            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }

        public static MySudokuPuzzle SolveStepUsingHiddenSingle(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - Hidden Single /////////
            foreach (MyCell cell in result.Cells.FindAll(c => !c.isFixed))
            {

                List<int> RowPeers = (
                    from rowpeerindex in MySudokuPuzzle.Peers[cell.index]
                    where rowpeerindex / 9 == cell.index / 9
                    && !result.Cells[rowpeerindex].isFixed
                    select rowpeerindex).ToList<int>();
                List<int> ColPeers = (
                    from colpeerindex in MySudokuPuzzle.Peers[cell.index]
                    where colpeerindex % 9 == cell.index % 9
                    && !result.Cells[colpeerindex].isFixed
                    select colpeerindex).ToList<int>();
                List<int> BlockPeers = (
                    from blockpeerindex in MySudokuPuzzle.Peers[cell.index]
                    where blockpeerindex / 9 / 3 == cell.index / 9 / 3 && blockpeerindex % 9 / 3 == cell.index % 9 / 3
                    && !result.Cells[blockpeerindex].isFixed
                    select blockpeerindex).ToList<int>();

                RowPeers.Add(cell.index);
                ColPeers.Add(cell.index);
                BlockPeers.Add(cell.index);
                for (int i = 1; i <= 9; i++)
                {
                    List<int> rowpeerswithvalue = (
                        from rp in RowPeers
                        where result.Cells[rp].candidates.Contains(i)
                        select rp).ToList<int>();
                    List<int> colpeerswithvalue = (
                        from cp in ColPeers
                        where result.Cells[cp].candidates.Contains(i)
                        select cp).ToList<int>();
                    List<int> blockpeerswithvalue = (
                        from bp in BlockPeers
                        where result.Cells[bp].candidates.Contains(i)
                        select bp).ToList<int>();
                    if (rowpeerswithvalue.Count == 1)
                    {
                        step = new MySudokuSolverStep(MySudokuStepType.PutValue, rowpeerswithvalue[0], i, MySudokuRules.HiddenSingle);
                        return PutValue(result, rowpeerswithvalue[0], i);
                    }
                    else if (colpeerswithvalue.Count == 1)
                    {
                        step = new MySudokuSolverStep(MySudokuStepType.PutValue, colpeerswithvalue[0], i, MySudokuRules.HiddenSingle);
                        return PutValue(result, colpeerswithvalue[0], i);
                    }
                    else if (blockpeerswithvalue.Count == 1)
                    {
                        step = new MySudokuSolverStep(MySudokuStepType.PutValue, blockpeerswithvalue[0], i, MySudokuRules.HiddenSingle);
                        return PutValue(result, blockpeerswithvalue[0], i);
                    }
                }
            }
            ///////// END - Hidden Single /////////

            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }

        public static MySudokuPuzzle SolveStepUsingNakedSingle(MySudokuPuzzle input, out MySudokuSolverStep step)
        {
            MySudokuPuzzle result = input;

            ///////// START - Naked Single  /////////
            foreach (MyCell cell in result.Cells.FindAll(c => !c.isFixed && c.value == 0 && c.candidates.Count == 1))
            {
                step = new MySudokuSolverStep(MySudokuStepType.PutValue, cell.index, cell.candidates[0], MySudokuRules.NakedSingle);
                return PutValue(result, cell.index, cell.candidates[0]);

            }
            ///////// END - Naked Single    /////////

            step = new MySudokuSolverStep(MySudokuStepType.PutValue, -1, -1, MySudokuRules.NotValid);
            return result;
        }
        public static MySudokuPuzzle GenerateSpecificRate(MySudokuPuzzle fullGridSeed, int rate)
        {
            MySudokuPuzzle initialPuzzle = MySudokuPuzzle.GenerateSudokuPuzzle(fullGridSeed);
            MySudokuPuzzle tempPuzzle=initialPuzzle.Clone() as MySudokuPuzzle;
            MySudokuPuzzle tempPuzzleCopy=tempPuzzle.Clone() as MySudokuPuzzle;
            MySudokuSolverStep step;
            int currentRate=0;
            bool isFinished = false;
            while (currentRate != 4 || !isFinished)
            {
                isFinished = false;
                currentRate = 0;
                do
                {
                    tempPuzzle = MySudokuHumanSolver.SolveStep(tempPuzzle, out step);
                    Console.WriteLine(step.ToString());
                    if (step.rule == MySudokuRules.XWing)
                        currentRate = 4;
                } while (step.rule != MySudokuRules.NotValid);
                if (tempPuzzle.isSolved())
                    isFinished = true;
                if (currentRate != 4 || !isFinished)
                {
                    tempPuzzle = MySudokuPuzzle.GenerateSudokuPuzzle(fullGridSeed);
                    tempPuzzleCopy = tempPuzzle.Clone() as MySudokuPuzzle;
                }
            }
            return tempPuzzleCopy;
        }
        public static void SolveStepByStep(MySudokuPuzzle puzzle)
        {
            MySudokuPuzzle tempPuzzle = puzzle.Clone() as MySudokuPuzzle;
            MySudokuSolverStep step;
            do
            {
                tempPuzzle = MySudokuHumanSolver.SolveStep(tempPuzzle, out step);
                Console.WriteLine(step.ToString());
            } while (step.rule != MySudokuRules.NotValid);
        }
        public static MySudokuPuzzle Solve(MySudokuPuzzle puzzle)
        {
            MySudokuPuzzle tempPuzzle = puzzle.Clone() as MySudokuPuzzle;
            MySudokuSolverStep step;
            do
            {
                tempPuzzle = MySudokuHumanSolver.SolveStep(tempPuzzle, out step);
                //Console.WriteLine(step.ToString());
            } while (step.rule != MySudokuRules.NotValid);
            return tempPuzzle;
        }
        public static int ratePuzzle(MySudokuPuzzle puzzle, out int weight)
        {
            MySudokuPuzzle tempPuzzle = puzzle.Clone() as MySudokuPuzzle;
            MySudokuSolverStep step;
            int rate = 0;
            weight = 0;
            do
            {
                tempPuzzle = MySudokuHumanSolver.SolveStep(tempPuzzle, out step);
                if (step.rule == MySudokuRules.NakedSingle && rate < 1)
                    rate = 1;
                if (step.rule == MySudokuRules.HiddenSingle && rate < 1)
                    rate = 1;
                if (step.rule == MySudokuRules.LockedCandidatesClaiming && rate < 2)
                    rate = 2; 
                if (step.rule == MySudokuRules.LockedCandidatesPointing && rate < 2)
                    rate = 2;
                if (step.rule == MySudokuRules.LockedPair && rate < 2)
                    rate = 2;
                if (step.rule == MySudokuRules.HiddenPair && rate < 2)
                    rate = 2;
                if (step.rule == MySudokuRules.LockedTriple && rate < 2)
                    rate = 2;
                if (step.rule == MySudokuRules.XWing && rate < 3)
                    rate = 3;
                switch (step.rule)
                {
                    case MySudokuRules.NakedSingle: weight += 4;
                        break;
                    case MySudokuRules.HiddenSingle:weight += 14;
                        break;
                    case MySudokuRules.LockedCandidatesPointing:weight += 50;
                        break;
                    case MySudokuRules.LockedCandidatesClaiming:weight += 50;
                        break;
                    case MySudokuRules.LockedPair:weight += 40;
                        break;
                    case MySudokuRules.LockedTriple:weight += 60;
                        break;
                    case MySudokuRules.HiddenPair:weight += 70;
                        break;
                    case MySudokuRules.Other:
                        break;
                    case MySudokuRules.NotValid:
                        break;
                    case MySudokuRules.XWing:weight += 140;
                        break;
                    default:
                        break;
                }
                //Console.WriteLine(step.ToString());
            } while (step.rule != MySudokuRules.NotValid);
            if (!tempPuzzle.isSolved())
                rate = 4;
            if (rate == 3)
            {
                //Console.WriteLine("XWing is FOUND");
                //Console.WriteLine(puzzle.toStringList());
                //Console.ReadLine();
            }
            return rate;
        }
    }

    public enum MySudokuStepType { PutValue, RemoveCandidates }

    public class MySudokuSolverStep
    {
        public int index;
        public List<int> indexesRuleAppliedTo;
        public MySudokuRules rule;
        public int value;
        public List<int> indexesAffected;
        public MySudokuStepType type;
        public List<int> values;

        public MySudokuSolverStep(MySudokuStepType type, int index, int value, MySudokuRules rule)
        {
            this.index = index;
            this.value = value;
            this.rule = rule;
            this.type = type;
        }

        public MySudokuSolverStep(MySudokuStepType type,List<int> values, List<int> indexesRuleAppliedTo, List<int> indexesAffected, MySudokuRules rule)
        {
            this.indexesRuleAppliedTo = indexesRuleAppliedTo;
            this.indexesAffected = indexesAffected;
            this.rule = rule;
            this.type = type;
            this.values = values;
        }

        public override string ToString()
        {
            if (this.type == MySudokuStepType.PutValue)
                return String.Format("Step:({0}) in index ({1}) put value ({2})",
                    this.rule.ToString(),
                    coords(this.index),
                    this.value);
            StringBuilder sb = new StringBuilder();
            sb.Append(" values: ");
            foreach (int v in this.values)
            {
                sb.Append(v);
                sb.Append(" ");
            }
            sb.Append(" in indexes: ");
            foreach (int indexRuleAppliedTo in this.indexesRuleAppliedTo)
            {
                sb.Append(coords(indexRuleAppliedTo));
                sb.Append(" ");
            }
            sb.Append(" => ([");
            foreach (int index in this.indexesAffected)
            {
                sb.Append(coords(index));
                sb.Append(" ");
            }
            sb.Append("])");
            return String.Format("Step:({0}) {1}",
                    this.rule.ToString(),
                    sb.ToString()
                    );
        }
        public string coords(int index)
        {
            if (index == -1)
                return "r-1c-1";
            return "r" + MySudokuHumanSolver.Rows[index] + "c" + MySudokuHumanSolver.Cols[index];
        }

        
    }
}
