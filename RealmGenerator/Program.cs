using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace RealmGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Generate Full Samurai Grids
                 //GenerateFullSamurai();
            //GenerateClockSudokuRated3();
            
            #endregion

            #region Generate Games rated 3
            //SamuraiSudokuPuzzle sampuz = SamuraiSudokuPuzzle.GenerateSudokuPuzzleBySolver("514962378---637851492692387514---215439786378541926---984267513987253461---346912875451796283---879546321236814795---521783649865429137896452698137123675849251763124958749138652734198375264------391568247------------768412935------------425973681------614598273689514298637293714586147329576814857263914325876314592186937452---932861745479852631---657423981532146798---148957263761489325---281645379325671849---765139428948325167---493782156");
            //Console.WriteLine(sampuz.toStringList());
            //SamuraiSudokuHumanSolver.SolveStepByStep(new SamuraiSudokuPuzzle("090000160---010000840050000000---000000000000030708---004201007010098003---080003201370002901---000469000000000000---030000000900025300400000000000000001000670000007960000000090050008004003------020030000------------000006000------------000085690------090000004800000000000600002000000000046000000009000000000905028006008000---003060080480060000---000000701700500000---020000000069800007---009000500000410002---060007004002007030---002050600"));
            //MySudokuHumanSolver.SolveStepByStep(new MySudokuPuzzle("003070000000900250060008070706001043900000500300086090050102000087000000100000000"));

            #endregion
            //string str = "941352786---465218397357846921---213975486628917345---789346521134298657---396752814785634192---154683279269175834---872491653593461278539641527938412789563147928134765876523419682537869142------321965784------------785214369------------694873152------176538942756813792456432691857321496385721985742136498275461983347169528---382547169568423719---657819342219875463---149236578824317695---924178635653984271---768953214791256384---531624897";
            //int weight=0;
            //string str = "003005000200000904000700010000000000108400000600003507050090000070100006002000400";
            //MySudokuHumanSolver.SolveStepByStep(new MySudokuPuzzle(str));
            //var solutions = MySudokuPuzzle.MultiSolve(new MySudokuPuzzle(str));
            //Console.WriteLine(solutions.Count);
            //SamuraiSudokuPuzzle solved = SamuraiSudokuPuzzle.GenerateSudokuPuzzleBySolver(str);
            //do
            //{
                //solved = s.GenerateSudokuPuzzleBySolver(str);
                //Console.WriteLine(solved.toStringList());
                //Console.Read();
            //} while (SamuraiSudokuHumanSolver.ratePuzzle(solved, out weight) != 3);
            ///Console.WriteLine(solved);
            //GenerateStandardSudokuRated3();  
            //GenerateFullClock();
            //using (System.IO.StreamReader file = new System.IO.StreamReader(@"RandomSamuraiFullGrids.txt"))
            //{
            //    while (!file.EndOfStream)
            //        InsertSamuraiFilledGrids(file.ReadLine());
            //}
            //GenerateClockSudokuRated3();
            //ClockSudokuPuzzle p = new ClockSudokuPuzzle("B07C0230946070004010B8200000000000000040085020798006B020000500023C900700");
            //ClockSudokuHumanSolver.SolveStepByStep(p);
            //GenerateFullSamurai();
            //GenerateSamuraiSudokuRated3FINAL();
            //MySudokuPuzzle p = new MySudokuPuzzle("001078093200009000700006080003080920002090006000000000000004519407000832009000467");
            //MySudokuHumanSolver.SolveStepByStep(p);
            //SamuraiSudokuPuzzle s = new SamuraiSudokuPuzzle("001078093---050002030200009000---003000090700006080---709060000003080920---600070008002090006---008000073000000000---000000000000004500020800094020407000000000000008006009000007800310006080------100003020------------000002086------------006090500------004030001000009000080087004603001050000610000007900200030008540070040000---010600000026000400---860001490030016007---900000000000000000---180004000003070140---000005000002000006---406000100");
            //int weight = 0;
            //Console.WriteLine(SamuraiSudokuHumanSolver.ratePuzzle(s, out weight));
            //MySudokuPuzzle p = new MySudokuPuzzle("001078093200009000700006080003080920002090006000000000000004500407000000009000007");
            //MySudokuHumanSolver.SolveStepByStep(p);
        }
        static bool RunWithTimeout(ThreadStart threadStart, TimeSpan timeout)
        {
            Thread workerThread = new Thread(threadStart);

            workerThread.Start();

            bool finished = workerThread.Join(timeout);
            if (!finished)
                workerThread.Abort();

            return finished;
        }

        static void LongRunningOperation(int ss)
        {
            //Thread.Sleep(5000);
            
        }
        public void generatepuzzles()
        {
            MySudokuPuzzle emptypuzzle = new MySudokuPuzzle("000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"RandomGrids.txt", true))
            {
                while (true)
                {
                    MySudokuPuzzle initialpuzzle = new MySudokuPuzzle("000000000000000000000000000000000000000000000000000000000000000000000000000000000");
                    Thread workerThread = new Thread(() => { initialpuzzle = MySudokuPuzzle.RandomGrid(); });
                    workerThread.Start();
                    bool finished = workerThread.Join(5000);
                    if (!finished)
                        workerThread.Abort();
                    Console.WriteLine(initialpuzzle.toStringList());
                    if (!initialpuzzle.isEqual(emptypuzzle))
                    {
                        //Console.WriteLine("NOT GOOD");
                        file.WriteLine(initialpuzzle.toStringList());
                        file.Flush();
                    }
                    //ch = (char)Console.Read();
                }
            }
        }

        public static void generatepuzzles2()
        {
            MySudokuPuzzle emptypuzzle = new MySudokuPuzzle("000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            int weight = 0;
                while (true)
                {
                    MySudokuPuzzle initialpuzzle = new MySudokuPuzzle("000000000000000000000000000000000000000000000000000000000000000000000000000000000");
                    //Thread workerThread = new Thread(() => { initialpuzzle = MySudokuPuzzle.GenerateSudokuPuzzle(new MySudokuPuzzle("173264859942851367865379124354682791298417635716935248489123576637598412521746983")); });
                    Thread workerThread = new Thread(() => { initialpuzzle = MySudokuPuzzle.GenerateSudokuPuzzle(new MySudokuPuzzle("000000000936428175724153896142937568687245319593816427479381652315692784268574931")); });
                    workerThread.Start();
                    bool finished = workerThread.Join(1000000);
                    if (!finished)
                        workerThread.Abort();
                    Console.WriteLine(initialpuzzle.toStringList());
                    if (!initialpuzzle.isEqual(emptypuzzle))
                    {
                        Console.WriteLine(initialpuzzle.toStringList());
                        
                    }
                    if(MySudokuHumanSolver.ratePuzzle(initialpuzzle,out weight)==3)
                        Console.ReadLine();
                }
            
        }

        public static void generatesamuraipuzzles()
        {
            SamuraiSudokuPuzzle emptypuzzle = new SamuraiSudokuPuzzle(SamuraiSudokuPuzzle.GetEmptySamurai());
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"RandomSamuraiGrids.txt", true))
            {
                while (true)
                {
                    SamuraiSudokuPuzzle initialpuzzle = new SamuraiSudokuPuzzle(SamuraiSudokuPuzzle.GetEmptySamurai());
                    Thread workerThread = new Thread(() => { initialpuzzle = SamuraiSudokuPuzzle.RandomGrid(); });
                    workerThread.Start();
                    bool finished = workerThread.Join(10000);
                    if (!finished)
                        workerThread.Abort();
                    Console.WriteLine(initialpuzzle.toStringList());
                    if (!initialpuzzle.isEqual(emptypuzzle))
                    {
                        //Console.WriteLine("NOT GOOD");
                        file.WriteLine(initialpuzzle.toStringList());
                        file.Flush();
                    }
                    //ch = (char)Console.Read();
                }
            }
        }

        public static string GetConnectionString()
        {
            return "Data Source=.\\SQLEXPRESS;AttachDbFilename='E:\\Softech Design\\Project Naseem Sudoku\\Sudoku Generator\\RealmGenerator\\RealmGenerator\\SudokuGridsDB.mdf';Integrated Security=True;User Instance=True";
        }
        public static void InsertStandardFilledGrids(string _filledGrid)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Insert Into StandardFilledGrids Values(@Filled)";
            command.Parameters.Add("@Filled", SqlDbType.NVarChar);
            conn.Open();
            {
                command.Parameters["@Filled"].Value = _filledGrid;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        public static void InsertStandardSudoku(string _puzzle,int _filledId,int _numOfFilled,int _rate,int _weight)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Insert Into StandardSudoku Values(@Puzzle,@FilledID,@NumOfFilled,@Rate,@Weight)";
            command.Parameters.Add("@Puzzle", SqlDbType.NVarChar);
            command.Parameters.Add("@FilledID", SqlDbType.Int);
            command.Parameters.Add("@NumOfFilled", SqlDbType.Int);
            command.Parameters.Add("@Rate", SqlDbType.Int);
            command.Parameters.Add("@Weight", SqlDbType.Int);
            conn.Open();
            {
                command.Parameters["@Puzzle"].Value = _puzzle;
                command.Parameters["@FilledID"].Value = _filledId;
                command.Parameters["@NumOfFilled"].Value = _numOfFilled;
                command.Parameters["@Rate"].Value = _rate;
                command.Parameters["@Weight"].Value = _weight;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        public static void InsertClockFilledGrids(string _filledGrid)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Insert Into ClockFilledGrids Values(@Filled)";
            command.Parameters.Add("@Filled", SqlDbType.NVarChar);
            conn.Open();
            {
                command.Parameters["@Filled"].Value = _filledGrid;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        public static void InsertClockSudoku(string _puzzle, int _filledId, int _numOfFilled, int _rate, int _weight)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Insert Into ClockSudoku Values(@Puzzle,@FilledID,@NumOfFilled,@Rate,@Weight)";
            command.Parameters.Add("@Puzzle", SqlDbType.NVarChar);
            command.Parameters.Add("@FilledID", SqlDbType.Int);
            command.Parameters.Add("@NumOfFilled", SqlDbType.Int);
            command.Parameters.Add("@Rate", SqlDbType.Int);
            command.Parameters.Add("@Weight", SqlDbType.Int);
            conn.Open();
            {
                command.Parameters["@Puzzle"].Value = _puzzle;
                command.Parameters["@FilledID"].Value = _filledId;
                command.Parameters["@NumOfFilled"].Value = _numOfFilled;
                command.Parameters["@Rate"].Value = _rate;
                command.Parameters["@Weight"].Value = _weight;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        static void GenerateStandardSudokuRated3()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM STANDARDFILLEDGRIDS where id>10";
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            List<List<int>> BlocksRelation = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                BlocksRelation.Add(new List<int>());
            }
            BlocksRelation[0].AddRange(new List<int> { 5, 6, 8, 9 });
            BlocksRelation[1].AddRange(new List<int> { 4, 6, 7, 9 });
            BlocksRelation[2].AddRange(new List<int> { 4, 5, 7, 8 });
            BlocksRelation[3].AddRange(new List<int> { 2, 3, 8, 9 });
            BlocksRelation[4].AddRange(new List<int> { 1, 3, 7, 9 });
            BlocksRelation[5].AddRange(new List<int> { 1, 2, 7, 8 });
            BlocksRelation[6].AddRange(new List<int> { 2, 3, 5, 6 });
            BlocksRelation[7].AddRange(new List<int> { 1, 3, 4, 6 });
            BlocksRelation[8].AddRange(new List<int> { 1, 2, 4, 5 });

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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
            while (reader.Read())
            {
                List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
                string p2;
                int id = Convert.ToInt32(reader["ID"]);
                string initialPuzzleString = reader["Grid"].ToString();
                Random rng = new Random();
                bool isFound = false;
                do
                {
                    do
                    {
                        int n = list.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rng.Next(n + 1);
                            char value = list[k];
                            list[k] = list[n];
                            list[n] = value;
                        }

                        StringBuilder sb = new StringBuilder(initialPuzzleString);
                        char randomNumber = list[0];
                        for (int i = 0; i < 81; i++)
                        {
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                        }

                        int randomRow = int.Parse(list[1].ToString());
                        for (int i = 0; i < 81; i++)
                        {
                            if (Rows[i] == randomRow)
                                sb[i] = '0';
                        }
                        int randomCol = int.Parse(list[1].ToString());
                        for (int i = 0; i < 81; i++)
                        {
                            if (Cols[i] == randomCol)
                                sb[i] = '0';
                        }
                        //int randomRow2 = int.Parse(list[2].ToString());
                        //for (int i = 0; i < 81; i++)
                        //{
                        //    if (Rows[i] == randomRow2)
                        //        sb[i] = '0';
                        //}
                        int randomCol2 = int.Parse(list[2].ToString());
                        for (int i = 0; i < 81; i++)
                        {
                            if (Cols[i] == randomCol2)
                                sb[i] = '0';
                        }
                        char randomNumber2 = list[4];
                        int count = 0;
                        for (int i = 0; i < 81; i++)
                        {
                            if (sb[i] == randomNumber2)
                            {
                                if (count < 2)
                                    count++;
                                else
                                    sb[i] = '0';
                            }
                        }
                        p2 = sb.ToString();
                        MySudokuPuzzle t = new MySudokuPuzzle(p2);
                        Console.WriteLine(t);
                        //Console.ReadLine();
                        solutions = MySudokuPuzzle.MultiSolve(t);
                    } while (solutions.Count != 1);
                    int weight2 = 0;
                    Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                    int rate = 0;
                    int counter = 0;
                    MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                    do
                    {
                        generatedPuzzle = MySudokuPuzzle.GenerateSudokuPuzzle(new MySudokuPuzzle(p2));
                        int weight = 0;
                        rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                        Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                        Console.WriteLine(generatedPuzzle.toStringList());
                        counter++;
                    } while (rate != 3 && counter < 10);
                    if (rate == 3)
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"GeneratedXWingGrids.txt", true))
                        {
                            file.WriteLine(generatedPuzzle.toStringList());
                            file.Flush();
                        }
                        isFound = true;
                    }
                    else
                    {
                        isFound = false;
                    }
                } while (!isFound);

            }
        }
        
        public static void InsertSamuraiFilledGrids(string _filledGrid)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Insert Into SamuraiFilledGrids Values(@Filled)";
            command.Parameters.Add("@Filled", SqlDbType.NVarChar);
            conn.Open();
            {
                command.Parameters["@Filled"].Value = _filledGrid;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        static void GenerateFullClock()
        {
            ClockSudokuPuzzle emptypuzzle = new ClockSudokuPuzzle("000000000000000000000000000000000000000000000000000000000000000000000000");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"RandomClockFullGrids.txt", true))
            {
                while (true)
                {
                    ClockSudokuPuzzle initialpuzzle = new ClockSudokuPuzzle("000000000000000000000000000000000000000000000000000000000000000000000000");
                    Thread workerThread = new Thread(() => { initialpuzzle = ClockSudokuPuzzle.RandomGrid(); });
                    workerThread.Start();
                    bool finished = workerThread.Join(5000);
                    if (!finished)
                        workerThread.Abort();
                    Console.WriteLine(initialpuzzle.toStringList());
                    if (!initialpuzzle.isEqual(emptypuzzle))
                    {
                        //Console.WriteLine("NOT GOOD");
                        file.WriteLine(initialpuzzle.toStringList());
                        file.Flush();
                    }
                    //ch = (char)Console.Read();
                }
            }
        }
        static void GenerateClockSudokuRated3()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM CLOCKFILLEDGRIDS where id>17";
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            list.Add('A');
            list.Add('B');
            list.Add('C');
            List<char> list6 = new List<char>();
            list6.Add('1');
            list6.Add('2');
            list6.Add('3');
            list6.Add('4');
            list6.Add('5');
            list6.Add('6');
            int[]Rows=new int[]{
            1,1,1,1,1,1,1,1,1,1,1,1,
            2,2,2,2,2,2,2,2,2,2,2,2,
            3,3,3,3,3,3,3,3,3,3,3,3,
            4,4,4,4,4,4,4,4,4,4,4,4,
            5,5,5,5,5,5,5,5,5,5,5,5,
            6,6,6,6,6,6,6,6,6,6,6,6
            };
            int[] Cols = new int[]{
            1,2,3,4,5,6,1,2,3,4,5,6,
            1,2,3,4,5,6,1,2,3,4,5,6,
            1,2,3,4,5,6,1,2,3,4,5,6,
            1,2,3,4,5,6,1,2,3,4,5,6,
            1,2,3,4,5,6,1,2,3,4,5,6,
            1,2,3,4,5,6,1,2,3,4,5,6
            };
            int[] Blocks = new int[]{
            1,1,2,2,3,3,4,4,5,5,6,6,
            1,1,2,2,3,3,4,4,5,5,6,6,
            1,1,2,2,3,3,4,4,5,5,6,6,
            1,1,2,2,3,3,4,4,5,5,6,6,
            1,1,2,2,3,3,4,4,5,5,6,6,
            1,1,2,2,3,3,4,4,5,5,6,6
        };
            while (reader.Read())
            {
                List<ClockSudokuPuzzle> solutions = new List<ClockSudokuPuzzle>();
                string p2;
                int id = Convert.ToInt32(reader["ID"]);
                string initialPuzzleString = reader["Grid"].ToString();
                Random rng = new Random();
                bool isFound = false;
                do
                {
                    do
                    {
                        int n = list.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rng.Next(n + 1);
                            char value = list[k];
                            list[k] = list[n];
                            list[n] = value;
                        }

                        n = list6.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rng.Next(n + 1);
                            char value = list6[k];
                            list6[k] = list6[n];
                            list6[n] = value;
                        }


                        StringBuilder sb = new StringBuilder(initialPuzzleString);
                        char randomNumber = list[0];
                        for (int i = 0; i < 72; i++)
                        {
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                        }

                        //int randomRow = int.Parse(list6[1].ToString());
                        //for (int i = 0; i < 72; i++)
                        //{
                        //    if (Rows[i] == randomRow)
                        //        sb[i] = '0';
                        //}
                        int randomCol = int.Parse(list6[1].ToString());
                        for (int i = 0; i < 72; i++)
                        {
                            if (Cols[i] == randomCol)
                                sb[i] = '0';
                        }
                        int randomRow2 = int.Parse(list6[2].ToString());
                        for (int i = 0; i < 72; i++)
                        {
                            if (Rows[i] == randomRow2)
                                sb[i] = '0';
                        }
                        //int randomCol2 = int.Parse(list6[2].ToString());
                        //for (int i = 0; i < 72; i++)
                        //{
                        //    if (Cols[i] == randomCol2)
                        //        sb[i] = '0';
                        //}
                        char randomNumber2 = list[4];
                        int count = 0;
                        for (int i = 0; i < 72; i++)
                        {
                            if (sb[i] == randomNumber2)
                            {
                                if (count < 2)
                                    count++;
                                else
                                    sb[i] = '0';
                            }
                        }
                        p2 = sb.ToString();
                        ClockSudokuPuzzle t = new ClockSudokuPuzzle(p2);
                        Console.WriteLine(t);
                        //Console.ReadLine();
                        solutions = ClockSudokuPuzzle.MultiSolve(t);
                    } while (solutions.Count != 1);
                    int weight2 = 0;
                    Console.WriteLine(ClockSudokuHumanSolver.ratePuzzle(new ClockSudokuPuzzle(p2), out weight2));

                    int rate = 0;
                    int counter = 0;
                    ClockSudokuPuzzle generatedPuzzle = new ClockSudokuPuzzle();
                    do
                    {
                        generatedPuzzle = ClockSudokuPuzzle.GenerateSudokuPuzzle(new ClockSudokuPuzzle(p2));
                        int weight = 0;
                        rate = ClockSudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                        Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                        Console.WriteLine(generatedPuzzle.toStringList());
                        counter++;
                    } while (rate != 3 && counter < 10);
                    if (rate == 3)
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"GeneratedClockXWingGrids.txt", true))
                        {
                            file.WriteLine(generatedPuzzle.toStringList());
                            file.Flush();
                        }
                        isFound = true;
                    }
                    else
                    {
                        isFound = false;
                    }
                } while (!isFound);

            }
        }
        static void GenerateFullSamurai()
        {
            SamuraiSudokuPuzzle emptypuzzle = new SamuraiSudokuPuzzle(SamuraiSudokuPuzzle.GetEmptySamurai());
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"RandomSamuraiFullGrids.txt", true))
            {
                while (true)
                {
                    SamuraiSudokuPuzzle initialpuzzle = new SamuraiSudokuPuzzle(SamuraiSudokuPuzzle.GetEmptySamurai());
                    Thread workerThread = new Thread(() => { initialpuzzle = SamuraiSudokuPuzzle.RandomGrid(); });
                    workerThread.Start();
                    bool finished = workerThread.Join(5000);
                    if (!finished)
                        workerThread.Abort();
                    Console.WriteLine(initialpuzzle.toStringList());
                    if (!initialpuzzle.isEqual(emptypuzzle))
                    {
                        //Console.WriteLine("NOT GOOD");
                        file.WriteLine(initialpuzzle.toStringList());
                        file.Flush();
                    }
                    //ch = (char)Console.Read();
                }
            }
        }
        static void GenerateSamuraiSudokuRated3()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM SAMURAIFILLEDGRIDS where id=1";
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            
             #region Rows Array
        int[][] Rows ={
        new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},
        new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},
        new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},
        new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},
        new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},
        new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},
        new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17,51},new int[] {17,51},new int[] {17,51},
        new int[] {51},new int[] {51},new int[] {51},
        new int[] {27,51},new int[] {27,51},new int[] {27,51},new int[] {27},new int[] {27},new int[] {27},new int[] {27},new int[] {27},new int[] {27},
        new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18,52},new int[] {18,52},new int[] {18,52},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {28,52},new int[] {28,52},new int[] {28,52},new int[] {28},new int[] {28},new int[] {28},new int[] {28},new int[] {28},new int[] {28},
        new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19,53},new int[] {19,53},new int[] {19,53},
        new int[] {53},new int[] {53},new int[] {53},
        new int[] {29,53},new int[] {29,53},new int[] {29,53},new int[] {29},new int[] {29},new int[] {29},new int[] {29},new int[] {29},new int[] {29},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31,57},new int[] {31,57},new int[] {31,57},
        new int[] {57},new int[] {57},new int[] {57},
        new int[] {41,57},new int[] {41,57},new int[] {41,57},new int[] {41},new int[] {41},new int[] {41},new int[] {41},new int[] {41},new int[] {41},
        new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32,58},new int[] {32,58},new int[] {32,58},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {42,58},new int[] {42,58},new int[] {42,58},new int[] {42},new int[] {42},new int[] {42},new int[] {42},new int[] {42},new int[] {42},
        new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33,59},new int[] {33,59},new int[] {33,59},
        new int[] {59},new int[] {59},new int[] {59},
        new int[] {43,59},new int[] {43,59},new int[] {43,59},new int[] {43},new int[] {43},new int[] {43},new int[] {43},new int[] {43},new int[] {43},
        new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},
        new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},
        new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},
        new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},
        new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},
        new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49}          
                                    };
        #endregion

             #region Columns Array
        int[][] Cols ={
        ////// row 1
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 2
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 3
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 4
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 5
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 6
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 7
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 8
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 9
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 10
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 11
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 12
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 13
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 14
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 15
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 16
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 17
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 18
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 19
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 20
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 21
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49}
                                    };
        #endregion

             #region Blocks Array
        int[][] Blocks ={
        ////// row 1
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 2
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 3
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 4
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 5
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 6
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 7
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 8
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 9
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 10
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 11
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 12
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 13
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},
        ////// row 14
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},
        ////// row 15
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},                              
        ////// row 16
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 17
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 18
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 19
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49},
        ////// row 20
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49},
        ////// row 21
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49}
        
              
                                      };
        #endregion
            while (reader.Read())
            {
                List<SamuraiSudokuPuzzle> solutions = new List<SamuraiSudokuPuzzle>();
                string p2;
                int id = Convert.ToInt32(reader["ID"]);
                string initialPuzzleString = reader["Grid"].ToString();
                Random rng = new Random();
                bool isFound = false;
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder(initialPuzzleString);
                // LEFT TOP
                //int index = 0;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                // RIGHT TOP
                //int index = 0;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 12; j < 21; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                // LEFT BOTTOM
                //int index = 252;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                // RIGHT BOTTOM
                //int index = 252;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 12; j < 21; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                // MIDDLE
                //int index = 132;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                //Console.WriteLine(sb.ToString());
                string lefttop = "000570000000100600095200004060400901002090000000000000080020509400060802100800400";
                string righttop = "050900037200500090000000000600400008020005400070000000860000020400008706300700004";
                string leftbottom = "204030080000090023000007004000000000906080010030016007840001000600070009700009036";
                string rightbottom = "200510300000000000000790042502600030000070090900000000105320906307160000000000100";
                sb2 = new StringBuilder(initialPuzzleString);
                //int c = 0;
                //int index = 132;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb2[index + j] = middle[c];
                //        c++;
                //    }
                //    index += 21;
                //}
                int c = 0;
                int index = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sb2[index + j] = lefttop[c];
                        c++;
                    }
                    index += 21;
                }
                index = 0;
                c = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 12; j < 21; j++)
                    {
                        sb2[index + j] = righttop[c];
                        c++;
                    }
                    index += 21;
                }
                index = 252;
                c = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sb2[index + j] = leftbottom[c];
                        c++;
                    }
                    index += 21;
                }
                index = 252;
                c = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 12; j < 21; j++)
                    {
                        sb2[index + j] = rightbottom[c];
                        c++;
                    }
                    index += 21;
                }
                
                StringBuilder middlesb = new StringBuilder();
                index = 132;
                c = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        middlesb.Append(sb2[index + j]);
                        c++;
                    }
                    index += 21;
                }
                //string result = GenerateMiddleStandardSudokuRated4(middlesb.ToString());
                //Console.WriteLine(result.ToString());
                string middle = "009000860000010400400050000108560004300000100006190073080000200023040000000007000";
                c = 0;
                index = 132;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sb2[index + j] = middle[c];
                        c++;
                    }
                    index += 21;
                }
                //Console.WriteLine(middlesb.ToString());
                //Console.WriteLine("AFTER ELIMINATION :");
                
                //Console.WriteLine(middlesb.ToString());
                //MySudokuPuzzle pp = MySudokuPuzzle.GenerateSudokuPuzzle(new MySudokuPuzzle(middlesb.ToString()));
                
                //middle = pp.toStringList();
                //middle = "519000867832006495467000312008500000000400080200190000781000249623000758954000631";
                //Console.WriteLine(pp.toStringList());
                //Console.ReadLine();
                
                //Console.WriteLine(sb2.ToString());
                
                //string result = GenerateMiddleStandardSudokuRated3(middlesb.ToString());
                //Console.WriteLine(result.ToString());
                //middle = "009024067002710000400009300170060904390470106000000000001605040000941050000000000";
                //c = 0;
                //index = 132;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb2[index + j] = middle[c];
                //        c++;
                //    }
                //    index += 21;
                //}
                Console.WriteLine(sb2.ToString());
                Console.ReadLine();
                int weight=0;
                SamuraiSudokuHumanSolver.SolveStepByStep(new SamuraiSudokuPuzzle(sb2.ToString()));
                Console.WriteLine(SamuraiSudokuHumanSolver.ratePuzzle(new SamuraiSudokuPuzzle(sb2.ToString()), out weight));
            }
        }
        #region Final Generation
        static void GenerateSamuraiSudokuRated3FINAL()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM SAMURAIFILLEDGRIDS where id=1";
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');

            #region Rows Array
            int[][] Rows ={
        new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},
        new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},
        new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},
        new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},
        new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},
        new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},
        new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17,51},new int[] {17,51},new int[] {17,51},
        new int[] {51},new int[] {51},new int[] {51},
        new int[] {27,51},new int[] {27,51},new int[] {27,51},new int[] {27},new int[] {27},new int[] {27},new int[] {27},new int[] {27},new int[] {27},
        new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18,52},new int[] {18,52},new int[] {18,52},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {28,52},new int[] {28,52},new int[] {28,52},new int[] {28},new int[] {28},new int[] {28},new int[] {28},new int[] {28},new int[] {28},
        new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19,53},new int[] {19,53},new int[] {19,53},
        new int[] {53},new int[] {53},new int[] {53},
        new int[] {29,53},new int[] {29,53},new int[] {29,53},new int[] {29},new int[] {29},new int[] {29},new int[] {29},new int[] {29},new int[] {29},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31,57},new int[] {31,57},new int[] {31,57},
        new int[] {57},new int[] {57},new int[] {57},
        new int[] {41,57},new int[] {41,57},new int[] {41,57},new int[] {41},new int[] {41},new int[] {41},new int[] {41},new int[] {41},new int[] {41},
        new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32,58},new int[] {32,58},new int[] {32,58},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {42,58},new int[] {42,58},new int[] {42,58},new int[] {42},new int[] {42},new int[] {42},new int[] {42},new int[] {42},new int[] {42},
        new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33,59},new int[] {33,59},new int[] {33,59},
        new int[] {59},new int[] {59},new int[] {59},
        new int[] {43,59},new int[] {43,59},new int[] {43,59},new int[] {43},new int[] {43},new int[] {43},new int[] {43},new int[] {43},new int[] {43},
        new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},
        new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},
        new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},
        new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},
        new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},
        new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49}          
                                    };
            #endregion

            #region Columns Array
            int[][] Cols ={
        ////// row 1
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 2
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 3
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 4
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 5
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 6
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 7
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 8
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 9
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 10
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 11
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 12
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 13
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 14
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 15
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 16
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 17
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 18
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 19
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 20
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 21
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49}
                                    };
            #endregion

            #region Blocks Array
            int[][] Blocks ={
        ////// row 1
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 2
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 3
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 4
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 5
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 6
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 7
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 8
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 9
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 10
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 11
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 12
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 13
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},
        ////// row 14
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},
        ////// row 15
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},                              
        ////// row 16
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 17
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 18
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 19
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49},
        ////// row 20
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49},
        ////// row 21
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49}
        
              
                                      };
            #endregion
            while (reader.Read())
            {
                List<SamuraiSudokuPuzzle> solutions = new List<SamuraiSudokuPuzzle>();
                int id = Convert.ToInt32(reader["ID"]);
                string initialPuzzleString = reader["Grid"].ToString();
                Random rng = new Random();
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder(initialPuzzleString);
                //LEFT TOP
                //int index = 0;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                //string lefttopfull = sb.ToString();

                //MySudokuPuzzle LeftTopPuzzle = GenerateStandardSudokuRated3LeftTop(lefttopfull);

                //Console.WriteLine("*******************");
                //Console.WriteLine(LeftTopPuzzle.toStringList());
                //Console.ReadLine();
                // RIGHT TOP
                //sb.Clear();
                //index = 0;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 12; j < 21; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                //string righttopfull = sb.ToString();
                //MySudokuPuzzle RightTopPuzzle = GenerateStandardSudokuRated3RightTop(righttopfull);

                //Console.WriteLine("*******************");
                //Console.WriteLine(RightTopPuzzle.toStringList());
                //Console.ReadLine();
                // LEFT BOTTOM
                //sb.Clear();
                //index = 252;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                //string leftbottomfull = sb.ToString();
                //MySudokuPuzzle LeftBottomPuzzle = GenerateStandardSudokuRated3LeftBottom(leftbottomfull);

                //Console.WriteLine("*******************");
                //Console.WriteLine(LeftBottomPuzzle.toStringList());
                //Console.ReadLine();
                // RIGHT BOTTOM
                //sb.Clear();
                //index = 252;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 12; j < 21; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                //string rightbottomfull = sb.ToString();
                //MySudokuPuzzle RightBottomPuzzle = GenerateStandardSudokuRated3RightBottom(rightbottomfull);

                //Console.WriteLine("*******************");
                //Console.WriteLine(RightBottomPuzzle.toStringList());
                //Console.ReadLine();
                // MIDDLE
                //sb.Clear();
                //int index = 132;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb.Append(initialPuzzleString[index + j]);
                //    }
                //    index += 21;
                //}
                //string middlefull = sb.ToString();
                //MySudokuPuzzle MiddlePuzzle = new MySudokuPuzzle(GenerateMiddleStandardSudokuRated4(middlefull));

                //Console.WriteLine("*******************");
                //Console.WriteLine(MiddlePuzzle.toStringList());
                //Console.ReadLine();
                ////Console.WriteLine(sb.ToString());
                //string middle = MiddlePuzzle.toStringList();
                string middle = "010320000002010005060850010170000004005070180000000000000600200600040700054280000";
                string lefttop = "001078093200009000700006080003080920002090006000000000000004519407000832009000467";
                //string lefttopfinal = MySudokuPuzzle.GenerateFinalSudokuPuzzleLeftTop(lefttop).toStringList();
                string righttop = "050002030003000090709060000600070008008000073000000000867094020495008006312006080";
                //string righttopfinal = MySudokuPuzzle.GenerateFinalSudokuPuzzleRightTop(righttop).toStringList();
                string leftbottom = "004030781087004623000007954070040000026000400030016007000000000003070140002000006";
                string rightbottom = "249000080758000610631008540010600000860001490900000000180004000000005000406000100";
                //string rightbottomfinal = MySudokuPuzzle.GenerateFinalSudokuPuzzleRightBottom(rightbottom).toStringList();
                sb2 = new StringBuilder(initialPuzzleString);
                //int c = 0;
                //int index = 132;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb2[index + j] = middle[c];
                //        c++;
                //    }
                //    index += 21;
                //}
                int c = 0;
                int index = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sb2[index + j] = lefttop[c];
                        c++;
                    }
                    index += 21;
                }
                index = 0;
                c = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 12; j < 21; j++)
                    {
                        sb2[index + j] = righttop[c];
                        c++;
                    }
                    index += 21;
                }
                index = 252;
                c = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sb2[index + j] = leftbottom[c];
                        c++;
                    }
                    index += 21;
                }
                index = 252;
                c = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 12; j < 21; j++)
                    {
                        sb2[index + j] = rightbottom[c];
                        c++;
                    }
                    index += 21;
                }

                //StringBuilder middlesb = new StringBuilder();
                //index = 132;
                //c = 0;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        middlesb.Append(sb2[index + j]);
                //        c++;
                //    }
                //    index += 21;
                //}
                //string result = GenerateMiddleStandardSudokuRated4(middlesb.ToString());
                //Console.WriteLine(result.ToString());
                //string middle = "009000860000010400400050000108560004300000100006190073080000200023040000000007000";
                c = 0;
                index = 132;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sb2[index + j] = middle[c];
                        c++;
                    }
                    index += 21;
                }
                //Console.WriteLine(middlesb.ToString());
                //Console.WriteLine("AFTER ELIMINATION :");

                //Console.WriteLine(middlesb.ToString());
                //MySudokuPuzzle pp = MySudokuPuzzle.GenerateSudokuPuzzle(new MySudokuPuzzle(middlesb.ToString()));

                //middle = pp.toStringList();
                //middle = "519000867832006495467000312008500000000400080200190000781000249623000758954000631";
                //Console.WriteLine(pp.toStringList());
                //Console.ReadLine();

                Console.WriteLine(sb2.ToString());

                //string result = GenerateMiddleStandardSudokuRated3(middlesb.ToString());
                //Console.WriteLine(result.ToString());
                //middle = "009024067002710000400009300170060904390470106000000000001605040000941050000000000";
                //c = 0;
                //index = 132;
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        sb2[index + j] = middle[c];
                //        c++;
                //    }
                //    index += 21;
                //}
                Console.WriteLine(sb2.ToString());
                Console.ReadLine();
                int weight = 0;
                SamuraiSudokuHumanSolver.SolveStepByStep(new SamuraiSudokuPuzzle(sb2.ToString()));
                Console.WriteLine(SamuraiSudokuHumanSolver.ratePuzzle(new SamuraiSudokuPuzzle(sb2.ToString()), out weight));
            }
        }
        public static MySudokuPuzzle GenerateStandardSudokuRated3LeftTop(string seed)
        {

            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            List<char> list2 = new List<char>();
            list2.Add('1');
            list2.Add('2');
            list2.Add('3');
            list2.Add('4');
            list2.Add('5');
            list2.Add('6');
            List<List<int>> BlocksRelation = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                BlocksRelation.Add(new List<int>());
            }
            BlocksRelation[0].AddRange(new List<int> { 5, 6, 8, 9 });
            BlocksRelation[1].AddRange(new List<int> { 4, 6, 7, 9 });
            BlocksRelation[2].AddRange(new List<int> { 4, 5, 7, 8 });
            BlocksRelation[3].AddRange(new List<int> { 2, 3, 8, 9 });
            BlocksRelation[4].AddRange(new List<int> { 1, 3, 7, 9 });
            BlocksRelation[5].AddRange(new List<int> { 1, 2, 7, 8 });
            BlocksRelation[6].AddRange(new List<int> { 2, 3, 5, 6 });
            BlocksRelation[7].AddRange(new List<int> { 1, 3, 4, 6 });
            BlocksRelation[8].AddRange(new List<int> { 1, 2, 4, 5 });

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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

            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    n = list2.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2[k];
                        list2[k] = list2[n];
                        list2[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 9)
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                    }

                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 9)
                            if (sb[i] == randomNumber2)
                            {
                                if (count < 2)
                                    count++;
                                else
                                    sb[i] = '0';
                            }
                    }

                    int randomRow = int.Parse(list2[0].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow)
                            sb[i] = '0';
                    }
                    int randomCol = int.Parse(list2[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol)
                            sb[i] = '0';
                    }
                    //int randomRow2 = int.Parse(list[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow2)
                    //        sb[i] = '0';
                    //}
                    int randomCol2 = int.Parse(list2[2].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol2)
                            sb[i] = '0';
                    }

                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateSudokuPuzzleForLeftTop(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 3 && counter < 10);
                if (rate == 3)
                {
                    return generatedPuzzle;
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return null;
        }
        static MySudokuPuzzle GenerateStandardSudokuRated3RightTop(string seed)
        {

            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            List<char> list2forRows = new List<char>();
            list2forRows.Add('1');
            list2forRows.Add('2');
            list2forRows.Add('3');
            list2forRows.Add('4');
            list2forRows.Add('5');
            list2forRows.Add('6');
            List<char> list2forCols = new List<char>();
            list2forCols.Add('4');
            list2forCols.Add('5');
            list2forCols.Add('6');
            list2forCols.Add('7');
            list2forCols.Add('8');
            list2forCols.Add('9');
            List<List<int>> BlocksRelation = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                BlocksRelation.Add(new List<int>());
            }
            BlocksRelation[0].AddRange(new List<int> { 5, 6, 8, 9 });
            BlocksRelation[1].AddRange(new List<int> { 4, 6, 7, 9 });
            BlocksRelation[2].AddRange(new List<int> { 4, 5, 7, 8 });
            BlocksRelation[3].AddRange(new List<int> { 2, 3, 8, 9 });
            BlocksRelation[4].AddRange(new List<int> { 1, 3, 7, 9 });
            BlocksRelation[5].AddRange(new List<int> { 1, 2, 7, 8 });
            BlocksRelation[6].AddRange(new List<int> { 2, 3, 5, 6 });
            BlocksRelation[7].AddRange(new List<int> { 1, 3, 4, 6 });
            BlocksRelation[8].AddRange(new List<int> { 1, 2, 4, 5 });

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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

            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    n = list2forRows.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forRows[k];
                        list2forRows[k] = list2forRows[n];
                        list2forRows[n] = value;
                    }

                    n = list2forCols.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forCols[k];
                        list2forCols[k] = list2forCols[n];
                        list2forCols[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 7)
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                    }

                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 7)
                            if (sb[i] == randomNumber2)
                            {
                                if (count < 2)
                                    count++;
                                else
                                    sb[i] = '0';
                            }
                    }
                    

                    int randomRow = int.Parse(list2forRows[0].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow)
                            sb[i] = '0';
                    }
                    int randomCol = int.Parse(list2forCols[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol)
                            sb[i] = '0';
                    }
                    //int randomRow2 = int.Parse(list[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow2)
                    //        sb[i] = '0';
                    //}
                    int randomCol2 = int.Parse(list2forCols[2].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol2)
                            sb[i] = '0';
                    }

                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateSudokuPuzzleForRightTop(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 3 && counter < 10);
                if (rate == 3)
                {
                    return generatedPuzzle;
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return null;
        }
        static MySudokuPuzzle GenerateStandardSudokuRated3LeftBottom(string seed)
        {

            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            List<char> list2forRows = new List<char>();
            list2forRows.Add('4');
            list2forRows.Add('5');
            list2forRows.Add('6');
            list2forRows.Add('7');
            list2forRows.Add('8');
            list2forRows.Add('9');
            List<char> list2forCols = new List<char>();
            list2forCols.Add('1');
            list2forCols.Add('2');
            list2forCols.Add('3');
            list2forCols.Add('4');
            list2forCols.Add('5');
            list2forCols.Add('6');
            List<List<int>> BlocksRelation = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                BlocksRelation.Add(new List<int>());
            }
            BlocksRelation[0].AddRange(new List<int> { 5, 6, 8, 9 });
            BlocksRelation[1].AddRange(new List<int> { 4, 6, 7, 9 });
            BlocksRelation[2].AddRange(new List<int> { 4, 5, 7, 8 });
            BlocksRelation[3].AddRange(new List<int> { 2, 3, 8, 9 });
            BlocksRelation[4].AddRange(new List<int> { 1, 3, 7, 9 });
            BlocksRelation[5].AddRange(new List<int> { 1, 2, 7, 8 });
            BlocksRelation[6].AddRange(new List<int> { 2, 3, 5, 6 });
            BlocksRelation[7].AddRange(new List<int> { 1, 3, 4, 6 });
            BlocksRelation[8].AddRange(new List<int> { 1, 2, 4, 5 });

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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

            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    n = list2forRows.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forRows[k];
                        list2forRows[k] = list2forRows[n];
                        list2forRows[n] = value;
                    }

                    n = list2forCols.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forCols[k];
                        list2forCols[k] = list2forCols[n];
                        list2forCols[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 3)
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                    }

                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 3)
                            if (sb[i] == randomNumber2)
                            {
                                //if (count < 2)
                                //    count++;
                                //else
                                    sb[i] = '0';
                            }
                    }

                    char randomNumber3 = list[3];
                    count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 3)
                            if (sb[i] == randomNumber3)
                            {
                                if (count < 2)
                                    count++;
                                else
                                    sb[i] = '0';
                            }
                    }

                    int randomRow = int.Parse(list2forRows[0].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow)
                            sb[i] = '0';
                    }
                    int randomCol = int.Parse(list2forCols[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol)
                            sb[i] = '0';
                    }
                    //int randomRow2 = int.Parse(list2forRows[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow2)
                    //        sb[i] = '0';
                    //}
                    int randomCol2 = int.Parse(list2forCols[2].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol2)
                            sb[i] = '0';
                    }

                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateSudokuPuzzleForLeftBottom(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 3 && counter < 10);
                if (rate == 3)
                {
                    return generatedPuzzle;
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return null;
        }
        static MySudokuPuzzle GenerateStandardSudokuRated3RightBottom(string seed)
        {

            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            List<char> list2forRows = new List<char>();
            list2forRows.Add('4');
            list2forRows.Add('5');
            list2forRows.Add('6');
            list2forRows.Add('7');
            list2forRows.Add('8');
            list2forRows.Add('9');
            List<char> list2forCols = new List<char>();
            list2forCols.Add('4');
            list2forCols.Add('5');
            list2forCols.Add('6');
            list2forCols.Add('7');
            list2forCols.Add('8');
            list2forCols.Add('9');
            List<List<int>> BlocksRelation = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                BlocksRelation.Add(new List<int>());
            }
            BlocksRelation[0].AddRange(new List<int> { 5, 6, 8, 9 });
            BlocksRelation[1].AddRange(new List<int> { 4, 6, 7, 9 });
            BlocksRelation[2].AddRange(new List<int> { 4, 5, 7, 8 });
            BlocksRelation[3].AddRange(new List<int> { 2, 3, 8, 9 });
            BlocksRelation[4].AddRange(new List<int> { 1, 3, 7, 9 });
            BlocksRelation[5].AddRange(new List<int> { 1, 2, 7, 8 });
            BlocksRelation[6].AddRange(new List<int> { 2, 3, 5, 6 });
            BlocksRelation[7].AddRange(new List<int> { 1, 3, 4, 6 });
            BlocksRelation[8].AddRange(new List<int> { 1, 2, 4, 5 });

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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

            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    n = list2forRows.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forRows[k];
                        list2forRows[k] = list2forRows[n];
                        list2forRows[n] = value;
                    }

                    n = list2forCols.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forCols[k];
                        list2forCols[k] = list2forCols[n];
                        list2forCols[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 1)
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                    }

                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 1)
                            if (sb[i] == randomNumber2)
                            {
                                //if (count < 2)
                                //    count++;
                                //else
                                sb[i] = '0';
                            }
                    }

                    char randomNumber3 = list[3];
                    count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (Blocks[i] != 1)
                            if (sb[i] == randomNumber3)
                            {
                                if (count < 2)
                                    count++;
                                else
                                    sb[i] = '0';
                            }
                    }

                    //int randomRow = int.Parse(list2forRows[0].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow)
                    //        sb[i] = '0';
                    //}
                    int randomCol = int.Parse(list2forCols[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol)
                            sb[i] = '0';
                    }
                    //int randomRow2 = int.Parse(list2forRows[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow2)
                    //        sb[i] = '0';
                    //}
                    int randomCol2 = int.Parse(list2forCols[2].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol2)
                            sb[i] = '0';
                    }

                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateSudokuPuzzleForRightBottom(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 3 && counter < 10);
                if (rate == 3)
                {
                    return generatedPuzzle;
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return null;
        }
        static MySudokuPuzzle GenerateStandardSudokuRated3Middle(string seed)
        {

            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            List<char> list2forRows = new List<char>();
            list2forRows.Add('1');
            list2forRows.Add('2');
            list2forRows.Add('3');
            list2forRows.Add('4');
            list2forRows.Add('5');
            list2forRows.Add('6');
            list2forRows.Add('7');
            list2forRows.Add('8');
            list2forRows.Add('9');
            List<char> list2forCols = new List<char>();
            list2forCols.Add('1');
            list2forCols.Add('2');
            list2forCols.Add('3');
            list2forCols.Add('4');
            list2forCols.Add('5');
            list2forCols.Add('6');
            list2forCols.Add('7');
            list2forCols.Add('8');
            list2forCols.Add('9');
            List<List<int>> BlocksRelation = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                BlocksRelation.Add(new List<int>());
            }
            BlocksRelation[0].AddRange(new List<int> { 5, 6, 8, 9 });
            BlocksRelation[1].AddRange(new List<int> { 4, 6, 7, 9 });
            BlocksRelation[2].AddRange(new List<int> { 4, 5, 7, 8 });
            BlocksRelation[3].AddRange(new List<int> { 2, 3, 8, 9 });
            BlocksRelation[4].AddRange(new List<int> { 1, 3, 7, 9 });
            BlocksRelation[5].AddRange(new List<int> { 1, 2, 7, 8 });
            BlocksRelation[6].AddRange(new List<int> { 2, 3, 5, 6 });
            BlocksRelation[7].AddRange(new List<int> { 1, 3, 4, 6 });
            BlocksRelation[8].AddRange(new List<int> { 1, 2, 4, 5 });

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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

            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    n = list2forRows.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forRows[k];
                        list2forRows[k] = list2forRows[n];
                        list2forRows[n] = value;
                    }

                    n = list2forCols.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list2forCols[k];
                        list2forCols[k] = list2forCols[n];
                        list2forCols[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                       
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                    }

                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                            if (sb[i] == randomNumber2)
                            {
                                if (count < 2)
                                    count++;
                                else
                                sb[i] = '0';
                            }
                    }

                    //char randomNumber3 = list[3];
                    //count = 0;
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Blocks[i] != 1)
                    //        if (sb[i] == randomNumber3)
                    //        {
                    //            if (count < 2)
                    //                count++;
                    //            else
                    //                sb[i] = '0';
                    //        }
                    //}

                    int randomRow = int.Parse(list2forRows[0].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow)
                            sb[i] = '0';
                    }
                    int randomCol = int.Parse(list2forCols[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol)
                            sb[i] = '0';
                    }
                    //int randomRow2 = int.Parse(list2forRows[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow2)
                    //        sb[i] = '0';
                    //}
                    int randomCol2 = int.Parse(list2forCols[2].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol2)
                            sb[i] = '0';
                    }

                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateSudokuPuzzleForMiddle(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 3 && counter < 10);
                if (rate == 3)
                {
                    return generatedPuzzle;
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return null;
        }
        #endregion
        
        static string GenerateStandardSudokuRated3(string seed)
        {
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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
                List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
                string p2;
                //int id = Convert.ToInt32(reader["ID"]);
                string initialPuzzleString = seed;
                Random rng = new Random();
                bool isFound = false;
                do
                {
                    do
                    {
                        int n = list.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rng.Next(n + 1);
                            char value = list[k];
                            list[k] = list[n];
                            list[n] = value;
                        }

                        StringBuilder sb = new StringBuilder(initialPuzzleString);
                        char randomNumber = list[0];
                        for (int i = 0; i < 81; i++)
                        {
                            if (sb[i] == randomNumber)
                                sb[i] = '0';
                        }

                        int randomRow = int.Parse(list[1].ToString());
                        for (int i = 0; i < 81; i++)
                        {
                            if (Rows[i] == randomRow)
                                sb[i] = '0';
                        }
                        int randomCol = int.Parse(list[1].ToString());
                        for (int i = 0; i < 81; i++)
                        {
                            if (Cols[i] == randomCol)
                                sb[i] = '0';
                        }
                        //int randomRow2 = int.Parse(list[2].ToString());
                        //for (int i = 0; i < 81; i++)
                        //{
                        //    if (Rows[i] == randomRow2)
                        //        sb[i] = '0';
                        //}
                        int randomCol2 = int.Parse(list[2].ToString());
                        for (int i = 0; i < 81; i++)
                        {
                            if (Cols[i] == randomCol2)
                                sb[i] = '0';
                        }
                        char randomNumber2 = list[4];
                        int count = 0;
                        for (int i = 0; i < 81; i++)
                        {
                            if (sb[i] == randomNumber2)
                            {
                                if (count < 2)
                                    count++;
                                else
                                    sb[i] = '0';
                            }
                        }
                        p2 = sb.ToString();
                        MySudokuPuzzle t = new MySudokuPuzzle(p2);
                        Console.WriteLine(t);
                        //Console.ReadLine();
                        solutions = MySudokuPuzzle.MultiSolve(t);
                    } while (solutions.Count != 1);
                    int weight2 = 0;
                    Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                    int rate = 0;
                    int counter = 0;
                    MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                    do
                    {
                        generatedPuzzle = MySudokuPuzzle.GenerateFinalSudokuPuzzle(new MySudokuPuzzle(p2));
                        int weight = 0;
                        rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                        Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                        Console.WriteLine(generatedPuzzle.toStringList());
                        counter++;
                    } while (rate != 3 && counter < 10);
                    if (rate == 3)
                    {
                        return generatedPuzzle.toStringList();
                    }
                    else
                    {
                        isFound = false;
                    }
                } while (!isFound);

                return "";
        }
        static string GenerateStandardSudokuRated4(string seed)
        {
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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
            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            //int id = Convert.ToInt32(reader["ID"]);
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                        if (sb[i] == randomNumber)
                            sb[i] = '0';
                    }

                    int randomRow = int.Parse(list[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow)
                            sb[i] = '0';
                    }
                    int randomCol = int.Parse(list[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol)
                            sb[i] = '0';
                    }
                    //int randomRow2 = int.Parse(list[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow2)
                    //        sb[i] = '0';
                    //}
                    int randomCol2 = int.Parse(list[2].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol2)
                            sb[i] = '0';
                    }
                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (sb[i] == randomNumber2)
                        {
                            if (count < 2)
                                count++;
                            else
                                sb[i] = '0';
                        }
                    }
                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateFinalSudokuPuzzle(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate < 4 && counter < 10);
                if (rate == 4)
                {
                    return generatedPuzzle.toStringList();
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return "";
        }
        static string GenerateMiddleStandardSudokuRated3(string seed)
        {
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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
            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            //int id = Convert.ToInt32(reader["ID"]);
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                        if (sb[i] == randomNumber)
                            sb[i] = '0';
                    }

                    int randomRow = int.Parse(list[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow)
                            sb[i] = '0';
                    }
                    //int randomCol = int.Parse(list[1].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Cols[i] == randomCol)
                    //        sb[i] = '0';
                    //}
                    int randomRow2 = int.Parse(list[2].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow2)
                            sb[i] = '0';
                    }
                    //int randomCol2 = int.Parse(list[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Cols[i] == randomCol2)
                    //        sb[i] = '0';
                    //}
                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (sb[i] == randomNumber2)
                        {
                            if (count < 2)
                                count++;
                            else
                                sb[i] = '0';
                        }
                    }
                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateSudokuPuzzle(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 3 && counter < 10);
                if (rate == 3)
                {
                    return generatedPuzzle.toStringList();
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return "";
        }
        static string GenerateMiddleStandardSudokuRated4(string seed)
        {
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');

            int[] Rows = new int[]{
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
            int[] Cols = new int[]{
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
            List<MySudokuPuzzle> solutions = new List<MySudokuPuzzle>();
            string p2;
            //int id = Convert.ToInt32(reader["ID"]);
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                    char randomNumber = list[0];
                    for (int i = 0; i < 81; i++)
                    {
                        if (sb[i] == randomNumber)
                            sb[i] = '0';
                    }

                    int randomRow = int.Parse(list[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Rows[i] == randomRow)
                            sb[i] = '0';
                    }
                    int randomCol = int.Parse(list[1].ToString());
                    for (int i = 0; i < 81; i++)
                    {
                        if (Cols[i] == randomCol)
                            sb[i] = '0';
                    }
                    //int randomRow2 = int.Parse(list[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Rows[i] == randomRow2)
                    //        sb[i] = '0';
                    //}
                    //int randomCol2 = int.Parse(list[2].ToString());
                    //for (int i = 0; i < 81; i++)
                    //{
                    //    if (Cols[i] == randomCol2)
                    //        sb[i] = '0';
                    //}
                    char randomNumber2 = list[4];
                    int count = 0;
                    for (int i = 0; i < 81; i++)
                    {
                        if (sb[i] == randomNumber2)
                        {
                            if (count < 2)
                                count++;
                            else
                                sb[i] = '0';
                        }
                    }
                    p2 = sb.ToString();
                    MySudokuPuzzle t = new MySudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = MySudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(MySudokuHumanSolver.ratePuzzle(new MySudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                MySudokuPuzzle generatedPuzzle = new MySudokuPuzzle();
                do
                {
                    generatedPuzzle = MySudokuPuzzle.GenerateFinalSudokuPuzzle(new MySudokuPuzzle(p2));
                    int weight = 0;
                    rate = MySudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 4 && counter < 10);
                if (rate == 4)
                {
                    return generatedPuzzle.toStringList();
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return "";
        }
        static string GenerateSamuraiSudokuRated3NEW(string seed)
        {
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');

            #region Rows Array
            int[][] Rows ={
        new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},new int[] {11},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},new int[] {21},
        new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},new int[] {12},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},new int[] {22},
        new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},new int[] {23},
        new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},new int[] {14},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},new int[] {24},
        new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},new int[] {15},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},new int[] {25},
        new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},new int[] {26},
        new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17},new int[] {17,51},new int[] {17,51},new int[] {17,51},
        new int[] {51},new int[] {51},new int[] {51},
        new int[] {27,51},new int[] {27,51},new int[] {27,51},new int[] {27},new int[] {27},new int[] {27},new int[] {27},new int[] {27},new int[] {27},
        new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18},new int[] {18,52},new int[] {18,52},new int[] {18,52},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {28,52},new int[] {28,52},new int[] {28,52},new int[] {28},new int[] {28},new int[] {28},new int[] {28},new int[] {28},new int[] {28},
        new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19},new int[] {19,53},new int[] {19,53},new int[] {19,53},
        new int[] {53},new int[] {53},new int[] {53},
        new int[] {29,53},new int[] {29,53},new int[] {29,53},new int[] {29},new int[] {29},new int[] {29},new int[] {29},new int[] {29},new int[] {29},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},new int[] {54},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},new int[] {55},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31},new int[] {31,57},new int[] {31,57},new int[] {31,57},
        new int[] {57},new int[] {57},new int[] {57},
        new int[] {41,57},new int[] {41,57},new int[] {41,57},new int[] {41},new int[] {41},new int[] {41},new int[] {41},new int[] {41},new int[] {41},
        new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32},new int[] {32,58},new int[] {32,58},new int[] {32,58},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {42,58},new int[] {42,58},new int[] {42,58},new int[] {42},new int[] {42},new int[] {42},new int[] {42},new int[] {42},new int[] {42},
        new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33},new int[] {33,59},new int[] {33,59},new int[] {33,59},
        new int[] {59},new int[] {59},new int[] {59},
        new int[] {43,59},new int[] {43,59},new int[] {43,59},new int[] {43},new int[] {43},new int[] {43},new int[] {43},new int[] {43},new int[] {43},
        new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},new int[] {34},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},new int[] {44},
        new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},new int[] {35},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},new int[] {45},
        new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},new int[] {46},
        new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},new int[] {37},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},new int[] {47},
        new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},new int[] {38},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},new int[] {48},
        new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49},new int[] {49}          
                                    };
            #endregion

            #region Columns Array
            int[][] Cols ={
        ////// row 1
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 2
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 3
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 4
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 5
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 6
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17},new int[] {18},new int[] {19},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {22},new int[] {23},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 7
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 8
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 9
        new int[] {11},new int[] {12},new int[] {13},new int[] {14},new int[] {15},new int[] {16},new int[] {17,51},new int[] {18,52},new int[] {19,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {21,57},new int[] {22,58},new int[] {23,59},new int[] {24},new int[] {25},new int[] {26},new int[] {27},new int[] {28},new int[] {29},
        ////// row 10
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 11
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 12
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {51},new int[] {52},new int[] {53},new int[] {54},new int[] {55},new int[] {56},new int[] {57},new int[] {58},new int[] {59},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 13
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 14
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 15
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37,51},new int[] {38,52},new int[] {39,53},
        new int[] {54},new int[] {55},new int[] {56},
        new int[] {41,57},new int[] {42,58},new int[] {43,59},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 16
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 17
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 18
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 19
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 20
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49},
        ////// row 21
        new int[] {31},new int[] {32},new int[] {33},new int[] {34},new int[] {35},new int[] {36},new int[] {37},new int[] {38},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {41},new int[] {42},new int[] {43},new int[] {44},new int[] {45},new int[] {46},new int[] {47},new int[] {48},new int[] {49}
                                    };
            #endregion

            #region Blocks Array
            int[][] Blocks ={
        ////// row 1
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 2
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 3
        new int[] {11},new int[] {11},new int[] {11},new int[] {12},new int[] {12},new int[] {12},new int[] {13},new int[] {13},new int[] {13},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {21},new int[] {21},new int[] {21},new int[] {22},new int[] {22},new int[] {22},new int[] {23},new int[] {23},new int[] {23},
        ////// row 4
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 5
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 6
        new int[] {14},new int[] {14},new int[] {14},new int[] {15},new int[] {15},new int[] {15},new int[] {16},new int[] {16},new int[] {16},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {24},new int[] {24},new int[] {24},new int[] {25},new int[] {25},new int[] {25},new int[] {26},new int[] {26},new int[] {26},
        ////// row 7
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 8
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 9
        new int[] {17},new int[] {17},new int[] {17},new int[] {18},new int[] {18},new int[] {18},new int[] {19,51},new int[] {19,51},new int[] {19,51},
        new int[] {52},new int[] {52},new int[] {52},
        new int[] {27,53},new int[] {27,53},new int[] {27,53},new int[] {28},new int[] {28},new int[] {28},new int[] {29},new int[] {29},new int[] {29},
        ////// row 10
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 11
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 12
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {54},new int[] {54},new int[] {54},new int[] {55},new int[] {55},new int[] {55},new int[] {56},new int[] {56},new int[] {56},
        new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},new int[] {-1},
        ////// row 13
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},
        ////// row 14
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},
        ////// row 15
        new int[] {31},new int[] {31},new int[] {31},new int[] {32},new int[] {32},new int[] {32},new int[] {33,57},new int[] {33,57},new int[] {33,57},
        new int[] {58},new int[] {58},new int[] {58},
        new int[] {41,59},new int[] {41,59},new int[] {41,59},new int[] {42},new int[] {42},new int[] {42},new int[] {43},new int[] {43},new int[] {43},                              
        ////// row 16
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 17
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 18
        new int[] {34},new int[] {34},new int[] {34},new int[] {35},new int[] {35},new int[] {35},new int[] {36},new int[] {36},new int[] {36},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {44},new int[] {44},new int[] {44},new int[] {45},new int[] {45},new int[] {45},new int[] {46},new int[] {46},new int[] {46},
        ////// row 19
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49},
        ////// row 20
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49},
        ////// row 21
        new int[] {37},new int[] {37},new int[] {37},new int[] {38},new int[] {38},new int[] {38},new int[] {39},new int[] {39},new int[] {39},
        new int[] {-1},new int[] {-1},new int[] {-1},
        new int[] {47},new int[] {47},new int[] {47},new int[] {48},new int[] {48},new int[] {48},new int[] {49},new int[] {49},new int[] {49}
        
              
                                      };
            #endregion
            List<SamuraiSudokuPuzzle> solutions = new List<SamuraiSudokuPuzzle>();
            string p2;
            //int id = Convert.ToInt32(reader["ID"]);
            string initialPuzzleString = seed;
            Random rng = new Random();
            bool isFound = false;
            do
            {
                do
                {
                    int n = list.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        char value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                    }

                    StringBuilder sb = new StringBuilder(initialPuzzleString);
                   
                    
                    p2 = sb.ToString();
                    SamuraiSudokuPuzzle t = new SamuraiSudokuPuzzle(p2);
                    Console.WriteLine(t);
                    //Console.ReadLine();
                    solutions = SamuraiSudokuPuzzle.MultiSolve(t);
                } while (solutions.Count != 1);
                int weight2 = 0;
                Console.WriteLine(SamuraiSudokuHumanSolver.ratePuzzle(new SamuraiSudokuPuzzle(p2), out weight2));

                int rate = 0;
                int counter = 0;
                SamuraiSudokuPuzzle generatedPuzzle = new SamuraiSudokuPuzzle();
                do
                {
                    generatedPuzzle = SamuraiSudokuPuzzle.GenerateSudokuPuzzle2(new SamuraiSudokuPuzzle(p2));
                    int weight = 0;
                    rate = SamuraiSudokuHumanSolver.ratePuzzle(generatedPuzzle, out weight);
                    Console.WriteLine(String.Format("Rate = {0} , Weight= {1}", rate, weight));
                    Console.WriteLine(generatedPuzzle.toStringList());
                    counter++;
                } while (rate != 3 && counter < 10);
                if (rate == 3)
                {
                    return generatedPuzzle.toStringList();
                }
                else
                {
                    isFound = false;
                }
            } while (!isFound);

            return "";
        }
    }
}
