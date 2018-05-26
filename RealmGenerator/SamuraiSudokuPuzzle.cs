using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmGenerator
{
    public class SamuraiSudokuPuzzle : ICloneable
    {
        public List<SamuraiCell> Cells;
        public const int Length = 9;
        public const int BoxSize = 3;
        public static Int64 nums;

        #region Rows Array
        public static int[][] Rows ={
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
        public static int[][] Cols ={
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
        public static int[][] Blocks ={
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

        public SamuraiSudokuPuzzle(string input)
        {
            if (input.Length != 441)
            {
                Console.WriteLine("input not ok");
                return;
            }
            Cells = new List<SamuraiCell>();
            int index = 0;
            foreach (char ch in input)
            {
                if (Char.IsDigit(ch) && ch != '0')
                    Cells.Add(new SamuraiCell(index, Int32.Parse(ch.ToString()), new List<int>(), true));
                else if(ch=='0')
                    Cells.Add(new SamuraiCell(index, 0, new List<int>(), false));
                else
                    Cells.Add(new SamuraiCell(index, -1, new List<int>(), true));
                index++;
            }
            if (Peers == null)
                GeneratePeersArray();
            GenerateInitialCandidates();
        }

        public SamuraiSudokuPuzzle()
        {
            Cells = new List<SamuraiCell>();
        }

        private void GenerateInitialCandidates()
        {
            foreach (SamuraiCell cell in this.Cells)
            {
                bool isFoundInPeers = false;
                for (int i = 1; i <= 9; i++)
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

        public static bool OnSameRow(int index1, int index2)
        {
            return Rows[index1].Intersect(Rows[index2]).Any();
        }

        public static bool OnSameColumn(int index1, int index2)
        {
            return Cols[index1].Intersect(Cols[index2]).Any();
        }

        public static bool OnSameBlock(int index1, int index2)
        {
            return Blocks[index1].Intersect(Blocks[index2]).Any();
        }

        public static bool IsPeer(int index1, int index2)
        {
            return (index1 != index2) && (
                (OnSameRow(index1, index2)) || (OnSameColumn(index1, index2)) || (OnSameBlock(index1, index2)));
        }

        public static Dictionary<int, List<int>> Peers;

        private void GeneratePeersArray()
        {
            Peers = new Dictionary<int, List<int>>();
            List<int> temp;
            foreach (SamuraiCell cell in this.Cells)
            {
                temp = new List<int>();
                foreach (SamuraiCell cell2 in this.Cells)
                {
                    if (IsPeer(cell.index, cell2.index)&&!cell.isExcept())
                        temp.Add(cell2.index);
                }
                Peers.Add(cell.index, temp);
            }
        }

        public SamuraiSudokuPuzzle ApplyConstraints(int cellIndex, int value)
        {
            SamuraiSudokuPuzzle puzzle = (SamuraiSudokuPuzzle)this.Clone();
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

        public SamuraiSudokuPuzzle PlaceValue(int cellIndex, int value)
        {
            if (!this.Cells[cellIndex].candidates.Contains(value))
                return null;

            var puzzle = ApplyConstraints(cellIndex, value);
            if (puzzle == null)
                return null;
            return puzzle;

        }

        public static SamuraiSudokuPuzzle RandomGrid()
        {
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 81; i++)
            //{
            //    sb.Append('0');
            //}
            //SamuraiSudokuPuzzle puzzle = new SamuraiSudokuPuzzle(GetEmptySamurai());
            MySudokuPuzzle middleMySudokuPuzzle = MySudokuPuzzle.RandomGrid();
            //MySudokuPuzzle middleMySudokuPuzzle = new MySudokuPuzzle("387154692651289473492637815865491327714325968239768154546973281128546739973812546");
            Console.WriteLine("Middle Generated ");
            string middle = middleMySudokuPuzzle.toStringList();
            // Generate Bottom Right
            StringBuilder sb = new StringBuilder();
            sb.Append(middle[60]);
            sb.Append(middle[61]);
            sb.Append(middle[62]);
            sb.Append("000000");
            sb.Append(middle[69]);
            sb.Append(middle[70]);
            sb.Append(middle[71]);
            sb.Append("000000");
            sb.Append(middle[78]);
            sb.Append(middle[79]);
            sb.Append(middle[80]);
            sb.Append("000000");
            sb.Append("000000000000000000000000000000000000000000000000000000");
            MySudokuPuzzle bottomRightMySudokuPuzzle = MySudokuPuzzle.RandomGrid(sb.ToString());
            Console.WriteLine("Bottom Right Generated");
            //END
            // Generate Top Left
            sb.Clear();
            sb.Append("000000000000000000000000000000000000000000000000000000000000");
            sb.Append(middle[0]);
            sb.Append(middle[1]);
            sb.Append(middle[2]);
            sb.Append("000000");
            sb.Append(middle[9]);
            sb.Append(middle[10]);
            sb.Append(middle[11]);
            sb.Append("000000");
            sb.Append(middle[18]);
            sb.Append(middle[19]);
            sb.Append(middle[20]);
            MySudokuPuzzle topLeftMySudokuPuzzle = MySudokuPuzzle.RandomGrid(sb.ToString());
            Console.WriteLine("Top Left Generated");
            // END
            // Generate Top Right
            sb.Clear();
            sb.Append("000000000000000000000000000000000000000000000000000000");
            sb.Append(middle[6]);
            sb.Append(middle[7]);
            sb.Append(middle[8]);
            sb.Append("000000");
            sb.Append(middle[15]);
            sb.Append(middle[16]);
            sb.Append(middle[17]);
            sb.Append("000000");
            sb.Append(middle[24]);
            sb.Append(middle[25]);
            sb.Append(middle[26]);
            sb.Append("000000");
            MySudokuPuzzle topRightMySudokuPuzzle = MySudokuPuzzle.RandomGrid(sb.ToString());
            Console.WriteLine("Top Right Generated");
            // END
            // Generate Bottom Left
            sb.Clear();
            sb.Append("000000");
            sb.Append(middle[54]);
            sb.Append(middle[55]);
            sb.Append(middle[56]);
            sb.Append("000000");
            sb.Append(middle[63]);
            sb.Append(middle[64]);
            sb.Append(middle[65]);
            sb.Append("000000");
            sb.Append(middle[72]);
            sb.Append(middle[73]);
            sb.Append(middle[74]);
            sb.Append("000000000000000000000000000000000000000000000000000000");
            MySudokuPuzzle bottomLeftMySudokuPuzzle = MySudokuPuzzle.RandomGrid(sb.ToString());
            Console.WriteLine("Bottom Left Generated");
            
            string samuraiSeed= SamuraiSudokuPuzzle.fillSamuraiWithStandard(SamuraiSudokuPuzzle.GetEmptySamurai(), topLeftMySudokuPuzzle.toStringList(), 1);
            samuraiSeed = SamuraiSudokuPuzzle.fillSamuraiWithStandard(samuraiSeed, topRightMySudokuPuzzle.toStringList(), 2);
            samuraiSeed = SamuraiSudokuPuzzle.fillSamuraiWithStandard(samuraiSeed, bottomLeftMySudokuPuzzle.toStringList(), 3);
            samuraiSeed = SamuraiSudokuPuzzle.fillSamuraiWithStandard(samuraiSeed, bottomRightMySudokuPuzzle.toStringList(), 4);
            return new SamuraiSudokuPuzzle(SamuraiSudokuPuzzle.fillSamuraiWithStandard(samuraiSeed, middleMySudokuPuzzle.toStringList(), 5));

        }

        public object Clone()
        {
            var clone = new SamuraiSudokuPuzzle();
            foreach (SamuraiCell cell in this.Cells)
            {
                clone.Cells.Add((SamuraiCell)cell.Clone());
            }

            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int maxWidth = 9;
            foreach (SamuraiCell cell in this.Cells)
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

        public SamuraiSudokuPuzzle SolveUsingGuess(Func<SamuraiSudokuPuzzle, bool> SolutionFunc = null)
        {
            if (this.Cells.All(SamuraiCell => SamuraiCell.value != 0))
                return (SolutionFunc != null && SolutionFunc(this)) ? null : this;

            int ActiveCell = FindWorkingCell();
            //Console.WriteLine(nums);
            foreach (int guess in this.Cells[ActiveCell].candidates)
            {
                SamuraiSudokuPuzzle puzzle;
                if ((puzzle = PlaceValue(ActiveCell, guess)) != null)
                    if ((puzzle = puzzle.SolveUsingGuess(SolutionFunc)) != null)
                    {
                        return puzzle;
                    }
            }
            return null;
        }

        public static List<SamuraiSudokuPuzzle> MultiSolve(SamuraiSudokuPuzzle input, int MaximumSolutions = -1)
        {

            var Solutions = new List<SamuraiSudokuPuzzle>();
            input.SolveUsingGuess(p =>
            {
                Solutions.Add(p);
                return Solutions.Count() < MaximumSolutions || MaximumSolutions == -1;
            });
            return Solutions;
        }

        public int FindWorkingCell()
        {
            int minCandidates = (from cell in this.Cells where !cell.isFixed && !cell.isExcept() && cell.value == 0 select cell).Min(cell => cell.candidates.Count);

            foreach (SamuraiCell cell in this.Cells.FindAll(c=>!c.isExcept()))
            {
                if (cell.isFixed || cell.value != 0)
                    continue;
                if (cell.candidates.Count == minCandidates)
                    return cell.index;
            }
            return -1;
        }
        public List<int> FindWorkingCells()
        {
            int minCandidates = (from cell in this.Cells where !cell.isFixed && !cell.isExcept() && cell.value == 0 select cell).Min(cell => cell.candidates.Count);
            List<int> result = new List<int>();
            foreach (SamuraiCell cell in this.Cells.FindAll(c => !c.isExcept()))
            {
                if (cell.isFixed || cell.value != 0)
                    continue;
                if (cell.candidates.Count == minCandidates)
                    result.Add(cell.index);
            }
            return result;
        }

        public static SamuraiSudokuPuzzle GenerateSudokuPuzzle2(SamuraiSudokuPuzzle fullGrid)
        {
            SamuraiSudokuPuzzle puzzle = new SamuraiSudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            List<int> notPossibleIndexes = new List<int>();
            List<int> possibleIndexesToChoose = new List<int>();
            int indexx = 132;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    possibleIndexesToChoose.Add(indexx + j);
                }
                indexx += 21;
            }
            while (puzzle.NumberOfEmptyCells() <= 330)
            {
                /////// perform digging holes ///////
                string stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && !cell.isExcept() && !notPossibleIndexes.Contains(cell.index) && possibleIndexesToChoose.Contains(cell.index)
                                     select cell.index).ToList<int>();
                int randomCellIndex = rand.Next(Indexes.Count);
                StringBuilder sb = new StringBuilder(stringpuzzle);
                sb[Indexes[randomCellIndex]] = '0';
                string newstringpuzzle = sb.ToString();
                //Console.WriteLine(count);
                SamuraiSudokuPuzzle temppuzzle = new SamuraiSudokuPuzzle(newstringpuzzle);
                var solutions = SamuraiSudokuPuzzle.MultiSolve(temppuzzle);
                if (solutions.Count == 1)
                {
                    int weight = 0;
                    int currentRate = SamuraiSudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                    Console.WriteLine(String.Format("empty cells={0} , rate={1}", puzzle.NumberOfEmptyCells(),
                        currentRate.ToString()
                        ));
                    if (currentRate != 4)
                        puzzle = temppuzzle;
                    else
                        notPossibleIndexes.Add(randomCellIndex);
                    //if (temppuzzle.isEqual(puzzle))
                    //    break;
                }
                else
                    notPossibleIndexes.Add(randomCellIndex);
                ////////////////////////////////////
                //count++;
            }
            Console.WriteLine(puzzle);
            return puzzle;
        }

        public static SamuraiSudokuPuzzle GenerateSudokuPuzzle(SamuraiSudokuPuzzle fullGrid)
        {
            SamuraiSudokuPuzzle puzzle = new SamuraiSudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new SamuraiSudokuPuzzle(stringpuzzle);
            while (!Continue && puzzle.NumberOfEmptyCells() <= 330)
            {
                #region new Generation Code
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && !cell.isExcept()
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    SamuraiSudokuPuzzle temppuzzle = new SamuraiSudokuPuzzle(newstringpuzzle);
                    var solutions = SamuraiSudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = SamuraiSudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
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
                    puzzle = new SamuraiSudokuPuzzle(newstringpuzzle2);
                }
                #endregion
            }
                
            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }

        public static SamuraiSudokuPuzzle GenerateSudokuPuzzleTry(SamuraiSudokuPuzzle fullGrid)
        {
            SamuraiSudokuPuzzle puzzle = new SamuraiSudokuPuzzle(fullGrid.toStringList());
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
            string stringpuzzle = puzzle.toStringList();
            StringBuilder sbb = new StringBuilder(stringpuzzle);
            stringpuzzle = sbb.ToString();
            puzzle = new SamuraiSudokuPuzzle(stringpuzzle);
            List<int> possibleIndexesToChoose = new List<int>();
            int indexx = 132;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    possibleIndexesToChoose.Add(indexx + j);
                }
                indexx += 21;
            }
            while (!Continue && puzzle.NumberOfEmptyCells() <= 330)
            {
                #region new Generation Code
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && !cell.isExcept() && possibleIndexesToChoose.Contains(cell.index)
                                     select cell.index).ToList<int>();
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                foreach (int index in Indexes)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    SamuraiSudokuPuzzle temppuzzle = new SamuraiSudokuPuzzle(newstringpuzzle);
                    var solutions = SamuraiSudokuPuzzle.MultiSolve(temppuzzle);
                    if (solutions.Count == 1)
                    {
                        count++;
                        int weight = 0;
                        int currentRate = SamuraiSudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
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
                    puzzle = new SamuraiSudokuPuzzle(newstringpuzzle2);
                }
                #endregion
            }

            Console.WriteLine("Fill count=" + puzzle.toStringList().ToList().FindAll(ch => ch != '0').Count);
            return puzzle;
        }

        public static SamuraiSudokuPuzzle GenerateSudokuPuzzleBySolver(string fullGridString)
        {
            //string lefttop = "001078093200009000700006080003080920002090006000000000000004519407000832009000467";
            StringBuilder ss = new StringBuilder();
            int index11 = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ss.Append(fullGridString[index11 + j]);
                }
                index11 += 21;
            }
            string lefttopfull = ss.ToString();
            string lefttop = (Program.GenerateStandardSudokuRated3LeftTop(lefttopfull)).toStringList();
            StringBuilder sblt = new StringBuilder(fullGridString);
            int cc = 0; 
            int indexx = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sblt[indexx + j] = lefttop[cc];
                    cc++;
                }
                indexx += 21;
            }
            fullGridString = sblt.ToString();
            SamuraiSudokuPuzzle puzzle = new SamuraiSudokuPuzzle(fullGridString);
            Random rand = new Random();
            int count = 0;
            bool Continue = false;
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
            
            List<int> RowColList = new List<int>();
            for (int i = 0; i < 441; i++)
            {
                if(SamuraiSudokuPuzzle.GetEmptySamurai()[i]!='-')
                    RowColList.Add(i);
            }
            StringBuilder sb23 = new StringBuilder(fullGridString);
            List<SamuraiSudokuPuzzle> solutions = new List<SamuraiSudokuPuzzle>();
            do
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = rand.Next(n + 1);
                    char value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
                n = RowColList.Count;
                while (n > 1)
                {
                    n--;
                    int k = rand.Next(n + 1);
                    int value = RowColList[k];
                    RowColList[k] = RowColList[n];
                    RowColList[n] = value;
                }
                sb23 = new StringBuilder(fullGridString);
                char randomNumber = list[0];
                for (int i = 0; i < 441; i++)
                {
                    //if (sb23[i] == randomNumber)
                    //    sb23[i] = '0';
                }
                for (int j = 0; j < 5; j++)
                {
                    int randomRow = RowColList[j];
                    for (int i = 0; i < 441; i++)
                    {
                        //if (sb23[i] != '-')
                        //    if (OnSameRow(i, randomRow))
                        //        sb23[i] = '0';
                    }    
                }
                for (int j = 0; j < 5; j++)
                {
                    int randomCol = RowColList[j+3];
                    for (int i = 0; i < 441; i++)
                    {
                        //if (sb23[i] != '-')
                        //    if (OnSameColumn(i, randomCol))
                        //        sb23[i] = '0';
                    }    
                }

                //char randomNumber2 = list[4];
                //count = 0;
                //for (int i = 0; i < 441; i++)
                //{
                //    if (sb23[i] == randomNumber2)
                //    {
                //        if (count < 10)
                //            count++;
                //        else
                //            sb23[i] = '0';
                //    }
                //}

                solutions = SamuraiSudokuPuzzle.MultiSolve(new SamuraiSudokuPuzzle(sb23.ToString()));
            } while (solutions.Count != 1);
            string stringpuzzle = sb23.ToString();
            puzzle = new SamuraiSudokuPuzzle(stringpuzzle);
            List<int> notPossibleIndexes = new List<int>();
            Console.WriteLine(puzzle.toStringList());
            Console.ReadLine();
            while (!Continue)
            {
                stringpuzzle = puzzle.toStringList();
                List<int> Indexes = (from cell in puzzle.Cells
                                     where cell.isFixed && !cell.isExcept() && !notPossibleIndexes.Contains(cell.index)
                                     select cell.index).ToList<int>();
                List<int> Indexes2 = new List<int>();
                foreach (int ind in Indexes)
                {
                    foreach (int minCand in puzzle.FindWorkingCells())
                    {
                        if (OnSameRow(ind, minCand) || OnSameColumn(ind, minCand) || OnSameBlock(ind, minCand))
                            if (!Indexes2.Contains(ind))
                                Indexes2.Add(ind);
                    }
                }
                Continue = true;
                List<int> possibleIndexes = new List<int>();
                List<int> possibleRates = new List<int>();
                if (Indexes2.Count>11)
                {
                    int n = Indexes2.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rand.Next(n + 1);
                        int value = Indexes2[k];
                        Indexes2[k] = Indexes2[n];
                        Indexes2[n] = value;
                    }
                    Indexes2.RemoveRange(10, Indexes2.Count - 11);
                }
                foreach (int index in Indexes2)
                {
                    StringBuilder sb = new StringBuilder(stringpuzzle);
                    char ch=sb[index];
                    sb[index] = '0';
                    string newstringpuzzle = sb.ToString();
                    SamuraiSudokuPuzzle temppuzzle = new SamuraiSudokuPuzzle(newstringpuzzle);
                    bool canDig = SamuraiSudokuHumanSolver.Solve(temppuzzle,index,ch);

                    //SamuraiSudokuPuzzle solvedPuzzle = SamuraiSudokuHumanSolver.Solve(temppuzzle);
                    if (canDig)
                    {
                        int weight = 0;
                        int currentRate = SamuraiSudokuHumanSolver.ratePuzzle(temppuzzle, out weight);
                        Console.WriteLine(String.Format("empty cells={0} , rate={1}",
                            puzzle.NumberOfEmptyCells(),
                            currentRate.ToString()
                            ));
                        if (currentRate != 4)
                        {
                            possibleIndexes.Add(index);
                            possibleRates.Add(currentRate);
                        }
                        else
                        {
                            notPossibleIndexes.Add(index);
                        }
                    }
                    else
                    {
                        notPossibleIndexes.Add(index);
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
                    puzzle = new SamuraiSudokuPuzzle(newstringpuzzle2);
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
            foreach (SamuraiCell cell in this.Cells)
            {
                sb.Append(cell.value==-1?"-": cell.value.ToString());
            }
            return sb.ToString();
        }

        public bool isEqual(SamuraiSudokuPuzzle puzzle)
        {
            foreach (SamuraiCell cell in this.Cells)
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

        public static string fillSamuraiWithStandard(string originalSamuraiSudokuString, string standardSudokuString, int position)
        {
            StringBuilder sb = new StringBuilder(originalSamuraiSudokuString);
            int shiftOnSamurai = -1;
            switch (position)
            {
                case 1: shiftOnSamurai = 0;
                    break;
                case 2: shiftOnSamurai = 12;
                    break;
                case 3: shiftOnSamurai = 252;
                    break;
                case 4: shiftOnSamurai = 264;
                    break;
                case 5: shiftOnSamurai = 132;
                    break;
                default:
                    break;
            }
            if (shiftOnSamurai == -1)
                return null;
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    sb[i + shiftOnSamurai] = standardSudokuString[i + j * 9];
                }
                shiftOnSamurai = shiftOnSamurai + 21;
            }
            return sb.ToString();
        }

        public static string GetEmptySamurai()
        {
            return "000000000---000000000000000000---000000000000000000---000000000000000000---000000000000000000---000000000000000000---000000000000000000000000000000000000000000000000000000000000000000000000------000000000------------000000000------------000000000------000000000000000000000000000000000000000000000000000000000000000000000000---000000000000000000---000000000000000000---000000000000000000---000000000000000000---000000000000000000---000000000";
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
    }
}
