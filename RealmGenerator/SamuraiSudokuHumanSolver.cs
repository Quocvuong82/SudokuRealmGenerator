using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmGenerator
{
    public enum SamuraiSudokuRules { NakedSingle, HiddenSingle, LockedCandidatesPointing, LockedCandidatesClaiming, LockedPair, LockedTriple, HiddenPair, Other, NotValid, XWing }
    public static class SamuraiSudokuHumanSolver
    {
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

        public static bool onSameRow(int index1, int index2)
        {
            return Rows[index1].Intersect(Rows[index2]).Any();
        }

        public static bool onSameColumn(int index1, int index2)
        {
            return Cols[index1].Intersect(Cols[index2]).Any();
        }

        public static bool onSameBlock(int index1, int index2)
        {
            return Blocks[index1].Intersect(Blocks[index2]).Any();
        }

        public static SamuraiSudokuPuzzle PutValue(SamuraiSudokuPuzzle input, int cellIndex, int value)
        {
            foreach (int peerIndex in SamuraiSudokuPuzzle.Peers[cellIndex])
            {
                input.Cells[peerIndex].candidates = (input.Cells[peerIndex].candidates.Except(new List<int> { value })).ToList<int>();
            }
            input.Cells[cellIndex].value = value;
            input.Cells[cellIndex].isFixed = true;
            return input;
        }

        public static bool Solve(SamuraiSudokuPuzzle puzzle, int index,char value)
        {
            SamuraiSudokuPuzzle tempPuzzle = puzzle.Clone() as SamuraiSudokuPuzzle;
            SamuraiSudokuSolverStep step;
            do
            {
                tempPuzzle = SamuraiSudokuHumanSolver.SolveStep(tempPuzzle, out step);
                if (tempPuzzle.toStringList()[index] == value)
                    return true;
                //Console.WriteLine(step.ToString());
            } while (step.rule != SamuraiSudokuRules.NotValid);
            return false;
        }

        public static SamuraiSudokuPuzzle SolveStep(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = SolveStepUsingNakedSingle(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            result = SolveStepUsingHiddenSingle(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedCandidatePointing(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedCandidateClaiming(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedPair(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            result = SolveStepUsingLockedTriple(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            result = SolveStepUsingHiddenPair(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            result = SolveStepUsingXWing(input, out step);
            if (step.rule != SamuraiSudokuRules.NotValid)
                return result;
            return input;

        }

        public static SamuraiSudokuPuzzle SolveStepUsingXWing(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - X-Wing /////////
            foreach (SamuraiCell cell in result.Cells.FindAll(c => !c.isFixed && !c.isExcept()))
            {
                foreach (int candidate in cell.candidates)
                {
                    List<SamuraiCell> rowPeerExceptCellContainingThisCandidate = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && onSameRow(cell.index,c.index) && c.candidates.Contains(candidate));
                    if (rowPeerExceptCellContainingThisCandidate.Count == 1)
                    {
                        // we have the first row that contains candidate and this candidate only appears in two cells of the row
                        // lets check for two other cells in other row
                        List<SamuraiCell> columnPeerExceptCellContainingThisCandidate = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && onSameColumn(cell.index,c.index) && c.candidates.Contains(candidate));
                        if (columnPeerExceptCellContainingThisCandidate.Count > 0)
                        {
                            foreach (SamuraiCell cell2 in columnPeerExceptCellContainingThisCandidate)
                            {
                                List<SamuraiCell> rowPeerForColumnPeerCell = result.Cells.FindAll(c => !c.isFixed && c.index != cell2.index && onSameRow(c.index,cell2.index) && c.candidates.Contains(candidate));
                                if (rowPeerForColumnPeerCell.Count == 1)
                                    if (onSameColumn(rowPeerForColumnPeerCell[0].index,rowPeerExceptCellContainingThisCandidate[0].index))
                                    {
                                        // we have found 4 cells containing x wing 
                                        // let's check for affected cells ..
                                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && c.index != cell2.index && onSameColumn(c.index,cell.index) && onSameColumn(c.index,cell2.index) && c.candidates.Contains(candidate));
                                        affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && c.index != rowPeerExceptCellContainingThisCandidate[0].index && c.index != rowPeerForColumnPeerCell[0].index && onSameColumn(c.index,rowPeerExceptCellContainingThisCandidate[0].index) && onSameColumn(c.index,rowPeerForColumnPeerCell[0].index) && c.candidates.Contains(candidate)));
                                        if (affectedCells.Count > 0)
                                        {
                                            // X WING IS FOUND 
                                            List<int> indexesAffected = (
                                                from c in affectedCells
                                                select c.index).ToList<int>();
                                            foreach (SamuraiCell affectedCell in affectedCells)
                                            {
                                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                                            }
                                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate }, new List<int> { cell.index, rowPeerExceptCellContainingThisCandidate[0].index, cell2.index, rowPeerForColumnPeerCell[0].index }, indexesAffected, SamuraiSudokuRules.XWing);
                                            return result;
                                        }
                                    }
                            }
                        }
                    }
                    /////////////
                    List<SamuraiCell> columnPeerExceptCellContainingThisCandidate2 = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && onSameColumn(cell.index,c.index) && c.candidates.Contains(candidate));
                    if (columnPeerExceptCellContainingThisCandidate2.Count == 1)
                    {
                        // we have the first column that contains candidate and this candidate only appears in two cells of the column
                        // lets check for two other cells ib other column
                        List<SamuraiCell> rowPeerExceptCellContainingThisCandidate2 = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && onSameRow(cell.index,c.index) && c.candidates.Contains(candidate));
                        if (rowPeerExceptCellContainingThisCandidate2.Count > 0)
                        {
                            foreach (SamuraiCell cell2 in rowPeerExceptCellContainingThisCandidate2)
                            {
                                List<SamuraiCell> columnPeerForRowPeerCell = result.Cells.FindAll(c => !c.isFixed && c.index != cell2.index && onSameColumn(c.index,cell2.index) && c.candidates.Contains(candidate));
                                if (columnPeerForRowPeerCell.Count == 1)
                                    if (onSameRow(columnPeerForRowPeerCell[0].index,columnPeerExceptCellContainingThisCandidate2[0].index))
                                    {
                                        // we have found 4 cells containing x wing 
                                        // let's check for affected cells ..
                                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && c.index != cell.index && c.index != cell2.index && onSameRow(c.index,cell.index) && onSameRow(c.index,cell2.index) && c.candidates.Contains(candidate));
                                        affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && c.index != columnPeerExceptCellContainingThisCandidate2[0].index && c.index != columnPeerForRowPeerCell[0].index && onSameRow(c.index,columnPeerExceptCellContainingThisCandidate2[0].index) && onSameRow(c.index,columnPeerForRowPeerCell[0].index) && c.candidates.Contains(candidate)));
                                        if (affectedCells.Count > 0)
                                        {
                                            // X WING IS FOUND 
                                            List<int> indexesAffected = (
                                                from c in affectedCells
                                                select c.index).ToList<int>();
                                            foreach (SamuraiCell affectedCell in affectedCells)
                                            {
                                                result.Cells[affectedCell.index].candidates.Remove(candidate);
                                            }
                                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate }, new List<int> { cell.index, columnPeerExceptCellContainingThisCandidate2[0].index, cell2.index, columnPeerForRowPeerCell[0].index }, indexesAffected, SamuraiSudokuRules.XWing);
                                            return result;
                                        }
                                    }
                            }
                        }
                    }
                }
            }
            ///////// END   - X-Wing /////////
            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static SamuraiSudokuPuzzle SolveStepUsingHiddenPair(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - Hidden Pair /////////
            foreach (SamuraiCell cell in result.Cells.FindAll(c => !c.isFixed))
            {
                List<SamuraiCell> rowPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && onSameRow(c.index,cell.index) && c.index != cell.index);
                List<SamuraiCell> columnPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && onSameColumn(c.index,cell.index) && c.index != cell.index);
                List<SamuraiCell> blockPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && onSameBlock(c.index,cell.index) && c.index != cell.index);
                // iterate over all candidates to see if there are candidates that can be used as candidate 1 in Hidden Pair

                foreach (int candidate in cell.candidates)
                {
                    List<SamuraiCell> rowPeersCellsContainingCandidate1 = rowPeersExceptCell.FindAll(c => c.candidates.Contains(candidate));
                    List<SamuraiCell> columnPeersCellsContainingCandidate1 = columnPeersExceptCell.FindAll(c => c.candidates.Contains(candidate));
                    List<SamuraiCell> blockPeersCellsContainingCandidate1 = blockPeersExceptCell.FindAll(c => c.candidates.Contains(candidate));
                    if (rowPeersCellsContainingCandidate1.Count == 1)
                    {
                        // we have found candidate 1
                        foreach (int candidate2 in cell.candidates.Except(new List<int> { candidate }).ToList<int>())
                        {
                            // check to see if the cell containing candidate 1 also contains candidate 2
                            if (rowPeersCellsContainingCandidate1[0].candidates.Contains(candidate2))
                            {
                                // check to see if the other row peers except cell 1 and cell 2 doesn't contains this candidate
                                if (!rowPeersExceptCell.Except(rowPeersCellsContainingCandidate1).ToList<SamuraiCell>().FindAll(c => c.candidates.Contains(candidate2)).Any())
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
                                    if (candidatesToRemoveFromCell.Count > 0)
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
                                        step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate, candidate2 }, new List<int> { cell.index, rowPeersCellsContainingCandidate1[0].index }, new List<int> { cell.index, rowPeersCellsContainingCandidate1[0].index }, SamuraiSudokuRules.HiddenPair);
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
                                if (!columnPeersExceptCell.Except(columnPeersCellsContainingCandidate1).ToList<SamuraiCell>().FindAll(c => c.candidates.Contains(candidate2)).Any())
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
                                        step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate, candidate2 }, new List<int> { cell.index, columnPeersCellsContainingCandidate1[0].index }, new List<int> { cell.index, columnPeersCellsContainingCandidate1[0].index }, SamuraiSudokuRules.HiddenPair);
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
                                if (!blockPeersExceptCell.Except(blockPeersCellsContainingCandidate1).ToList<SamuraiCell>().FindAll(c => c.candidates.Contains(candidate2)).Any())
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
                                        step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate, candidate2 }, new List<int> { cell.index, blockPeersCellsContainingCandidate1[0].index }, new List<int> { cell.index, blockPeersCellsContainingCandidate1[0].index }, SamuraiSudokuRules.HiddenPair);
                                        return result;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            ///////// END   - Locked Candidate /////////
            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static SamuraiSudokuPuzzle SolveStepUsingLockedTriple(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - Locked Candidate /////////
            foreach (SamuraiCell cell in result.Cells.FindAll(c => !c.isFixed))
            {
                // find row peers with candidates that union with candidates in cell equal 3 in length
                List<SamuraiCell> rowPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && onSameRow(c.index,cell.index) && c.index != cell.index && (cell.candidates.Union(c.candidates).ToList<int>()).Count == 3);
                List<SamuraiCell> columnPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && onSameColumn(c.index,cell.index) && c.index != cell.index && (cell.candidates.Union(c.candidates).ToList<int>()).Count == 3);
                List<SamuraiCell> blockPeersExceptCell = result.Cells.FindAll(c => !c.isFixed && onSameBlock(c.index,cell.index) && c.index != cell.index && (cell.candidates.Union(c.candidates).ToList<int>()).Count == 3);
                if (rowPeersExceptCell.Count > 0)
                {
                    // iterate over row peers to find another cell that implement the rules of Locked Triples
                    foreach (SamuraiCell cell2 in rowPeersExceptCell)
                    {
                        List<int> unionCandidatesBetweenCellAndCell2 = cell.candidates.Union(cell2.candidates).ToList<int>();
                        List<SamuraiCell> rowPeersExceptCellAndCell2 = (rowPeersExceptCell.Except(new List<SamuraiCell> { cell2 })).ToList<SamuraiCell>();
                        if (rowPeersExceptCellAndCell2.Count > 0)
                        {
                            foreach (SamuraiCell cell3 in rowPeersExceptCellAndCell2)
                            {
                                List<int> unionCandidatesBetweenCellAndCell3 = cell.candidates.Union(cell3.candidates).ToList<int>();
                                List<int> unionCandidatesBetweenCell2AndCell3 = cell2.candidates.Union(cell3.candidates).ToList<int>();
                                if (unionCandidatesBetweenCellAndCell2.Count == 3 && unionCandidatesBetweenCellAndCell3.Count == 3 && unionCandidatesBetweenCell2AndCell3.Count == 3)
                                {
                                    if ((unionCandidatesBetweenCellAndCell2.Intersect(unionCandidatesBetweenCellAndCell3).Intersect(unionCandidatesBetweenCell2AndCell3)).ToList<int>().Count == 3)
                                    {
                                        // we have three cells that satisfy the LOCKED TRIPLE conditions
                                        // let's check the afftected cells .

                                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && onSameRow(c.index,cell.index) && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any());
                                        if ((onSameBlock(cell.index,cell2.index)) && (onSameBlock(cell2.index,cell3.index)))
                                        {
                                            // These three cells are on the same block also ..
                                            affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && onSameBlock(c.index,cell.index) && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any()));
                                        }
                                        if (affectedCells.Count > 0)
                                        {
                                            // WE HAVE FOUND ROW LOCKED TRIPLE
                                            foreach (SamuraiCell affectedCell in affectedCells)
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
                                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, unionCandidatesBetweenCellAndCell2, new List<int> { cell.index, cell2.index, cell3.index }, indexesAffected, SamuraiSudokuRules.LockedTriple);
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
                    foreach (SamuraiCell cell2 in columnPeersExceptCell)
                    {
                        List<int> unionCandidatesBetweenCellAndCell2 = cell.candidates.Union(cell2.candidates).ToList<int>();
                        List<SamuraiCell> columnPeersExceptCellAndCell2 = (columnPeersExceptCell.Except(new List<SamuraiCell> { cell2 })).ToList<SamuraiCell>();
                        if (columnPeersExceptCellAndCell2.Count > 0)
                        {
                            foreach (SamuraiCell cell3 in columnPeersExceptCellAndCell2)
                            {
                                List<int> unionCandidatesBetweenCellAndCell3 = cell.candidates.Union(cell3.candidates).ToList<int>();
                                List<int> unionCandidatesBetweenCell2AndCell3 = cell2.candidates.Union(cell3.candidates).ToList<int>();
                                if (unionCandidatesBetweenCellAndCell2.Count == 3 && unionCandidatesBetweenCellAndCell3.Count == 3 && unionCandidatesBetweenCell2AndCell3.Count == 3)
                                {
                                    if ((unionCandidatesBetweenCellAndCell2.Intersect(unionCandidatesBetweenCellAndCell3).Intersect(unionCandidatesBetweenCell2AndCell3)).ToList<int>().Count == 3)
                                    {
                                        // we have three cells that satisfy the LOCKED TRIPLE conditions
                                        // let's check the afftected cells .

                                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && onSameColumn(c.index,cell.index) && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any());
                                        if ((onSameBlock(cell.index,cell2.index)) && (onSameBlock(cell2.index,cell3.index)))
                                        {
                                            // These three cells are on the same block also ..
                                            affectedCells.AddRange(result.Cells.FindAll(c => !c.isFixed && onSameBlock(c.index,cell.index) && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any()));
                                        }
                                        if (affectedCells.Count > 0)
                                        {
                                            // WE HAVE FOUND COLUMN LOCKED TRIPLE
                                            foreach (SamuraiCell affectedCell in affectedCells)
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
                                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, unionCandidatesBetweenCellAndCell2, new List<int> { cell.index, cell2.index, cell3.index }, indexesAffected, SamuraiSudokuRules.LockedTriple);
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
                    foreach (SamuraiCell cell2 in blockPeersExceptCell)
                    {
                        List<int> unionCandidatesBetweenCellAndCell2 = cell.candidates.Union(cell2.candidates).ToList<int>();
                        List<SamuraiCell> blockPeersExceptCellAndCell2 = (blockPeersExceptCell.Except(new List<SamuraiCell> { cell2 })).ToList<SamuraiCell>();
                        if (blockPeersExceptCellAndCell2.Count > 0)
                        {
                            foreach (SamuraiCell cell3 in blockPeersExceptCellAndCell2)
                            {
                                List<int> unionCandidatesBetweenCellAndCell3 = cell.candidates.Union(cell3.candidates).ToList<int>();
                                List<int> unionCandidatesBetweenCell2AndCell3 = cell2.candidates.Union(cell3.candidates).ToList<int>();
                                if (unionCandidatesBetweenCellAndCell2.Count == 3 && unionCandidatesBetweenCellAndCell3.Count == 3 && unionCandidatesBetweenCell2AndCell3.Count == 3)
                                {
                                    if ((unionCandidatesBetweenCellAndCell2.Intersect(unionCandidatesBetweenCellAndCell3).Intersect(unionCandidatesBetweenCell2AndCell3)).ToList<int>().Count == 3)
                                    {
                                        // we have three cells that satisfy the LOCKED TRIPLE conditions
                                        // let's check the afftected cells .

                                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && onSameBlock(c.index,cell.index) && c.index != cell.index && c.index != cell2.index && c.index != cell3.index && c.candidates.Intersect(unionCandidatesBetweenCellAndCell2).Any());

                                        if (affectedCells.Count > 0)
                                        {
                                            // WE HAVE FOUND COLUMN LOCKED TRIPLE
                                            foreach (SamuraiCell affectedCell in affectedCells)
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
                                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, unionCandidatesBetweenCellAndCell2, new List<int> { cell.index, cell2.index, cell3.index }, indexesAffected, SamuraiSudokuRules.LockedTriple);
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
            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static SamuraiSudokuPuzzle SolveStepUsingLockedCandidatePointing(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - Locked Candidate /////////
            foreach (SamuraiCell cell in result.Cells.FindAll(c => !c.isFixed))
            {
                //// get the list of all cells in the same block as the cell
                //int blockIndex = Blocks[cell.index];
                List<SamuraiCell> blockCells = result.Cells.FindAll(c => onSameBlock(c.index,cell.index) && !c.isFixed);
                //// iterate over all candidates in the cell
                foreach (int candidate in cell.candidates)
                {
                    //// get the block cells that contains this candidate
                    List<SamuraiCell> cellsContainingLockedCandidate = blockCells.FindAll(c => c.candidates.Contains(candidate) && c.index != cell.index);
                    bool rowLockedCandidateFound = true;

                    //// check to see if the locked candidate on the same row
                    foreach (SamuraiCell c in cellsContainingLockedCandidate)
                    {
                        if (!onSameRow(cell.index, c.index))
                            rowLockedCandidateFound = false;
                    }
                    if (rowLockedCandidateFound) // we have found a cells containing candidate on the same row
                    {
                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && onSameRow(c.index,cell.index) && !onSameBlock(c.index,cell.index) && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND ROW LOCKED CANDIDATE
                            foreach (SamuraiCell affectedCell in affectedCells)
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
                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate }, indexesRuleAppliedTo, indexesAffected, SamuraiSudokuRules.LockedCandidatesPointing);
                            return result;
                        }
                    }

                    bool columnLockedCandidateFound = true;

                    //// check to see if the locked candidate on the same column
                    foreach (SamuraiCell c in cellsContainingLockedCandidate)
                    {
                        if (!onSameColumn(cell.index, c.index))
                            columnLockedCandidateFound = false;
                    }
                    if (columnLockedCandidateFound) // we have found a cells containing candidate on the same column
                    {
                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && onSameColumn(c.index,cell.index) && !onSameBlock(c.index,cell.index) && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND COLUMN LOCKED CANDIDATE
                            foreach (SamuraiCell affectedCell in affectedCells)
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
                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate }, indexesRuleAppliedTo, indexesAffected, SamuraiSudokuRules.LockedCandidatesPointing);
                            return result;
                        }
                    }
                }
            }
            ///////// END   - Locked Candidate /////////
            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static SamuraiSudokuPuzzle SolveStepUsingLockedCandidateClaiming(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - Locked Candidate /////////
            foreach (SamuraiCell cell in result.Cells.FindAll(c => !c.isFixed))
            {
                //// get the list of all cells in the same row as the cell
                //int rowIndex = Rows[cell.index];
                //// get the list of all cells in the same column as the cell
                //int colIndex = Cols[cell.index];
                List<SamuraiCell> rowCells = result.Cells.FindAll(c => onSameRow(c.index,cell.index) && !c.isFixed);
                List<SamuraiCell> colCells = result.Cells.FindAll(c => onSameColumn(c.index,cell.index) && !c.isFixed);
                //// iterate over all candidates in the cell
                foreach (int candidate in cell.candidates)
                {
                    //// get the row cells that contains this candidate
                    List<SamuraiCell> cellsContainingLockedCandidate = rowCells.FindAll(c => c.candidates.Contains(candidate) && c.index != cell.index);
                    //// get the row cells that contains this candidate
                    List<SamuraiCell> cellsContainingLockedCandidate2 = colCells.FindAll(c => c.candidates.Contains(candidate) && c.index != cell.index);
                    bool blockLockedCandidateFound = true;

                    //// check to see if the locked candidate on the same block
                    foreach (SamuraiCell c in cellsContainingLockedCandidate)
                    {
                        if (!onSameBlock(cell.index, c.index))
                            blockLockedCandidateFound = false;
                    }
                    if (blockLockedCandidateFound) // we have found a cells containing candidate on the same block
                    {
                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && onSameBlock(c.index,cell.index) && !onSameRow(c.index,cell.index) && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND BLOCK LOCKED CANDIDATE
                            foreach (SamuraiCell affectedCell in affectedCells)
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
                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate }, indexesRuleAppliedTo, indexesAffected, SamuraiSudokuRules.LockedCandidatesClaiming);
                            return result;
                        }
                    }

                    blockLockedCandidateFound = true;
                    //// check to see if the locked candidate on the same block
                    foreach (SamuraiCell c in cellsContainingLockedCandidate2)
                    {
                        if (!onSameBlock(cell.index, c.index))
                            blockLockedCandidateFound = false;
                    }
                    if (blockLockedCandidateFound) // we have found a cells containing candidate on the same block
                    {
                        List<SamuraiCell> affectedCells = result.Cells.FindAll(c => !c.isFixed && onSameBlock(c.index,cell.index) && !onSameColumn(c.index,cell.index) && c.candidates.Contains(candidate));
                        if (affectedCells.Count > 0)
                        {
                            // WE HAVE FOUND BLOCK LOCKED CANDIDATE
                            foreach (SamuraiCell affectedCell in affectedCells)
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
                            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, new List<int> { candidate }, indexesRuleAppliedTo, indexesAffected, SamuraiSudokuRules.LockedCandidatesClaiming);
                            return result;
                        }
                    }

                }
            }
            ///////// END   - Locked Candidate /////////
            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static SamuraiSudokuPuzzle SolveStepUsingLockedPair(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - Locked Pair /////////
            List<SamuraiCell> cellsWithTwoCandidates = result.Cells.FindAll(c => !c.isFixed && c.candidates.Count == 2);
            foreach (SamuraiCell cell in cellsWithTwoCandidates)
            {
                List<SamuraiCell> cellsEqualinCandidates = (cellsWithTwoCandidates.Except(new List<SamuraiCell> { cell })).ToList<SamuraiCell>().FindAll(c => cell.isEqualInCandidates(c) && SamuraiSudokuPuzzle.IsPeer(cell.index, c.index));
                if (cellsEqualinCandidates.Count > 0)
                {
                    int index1 = cell.index;
                    int index2 = cellsEqualinCandidates[0].index;
                    List<int> affectedCells = new List<int>();
                    if (onSameRow(index1,index2)) // the Locked Pair is in the same Row
                    {
                        affectedCells.AddRange((
                    from rowpeerindex in SamuraiSudokuPuzzle.Peers[index1]
                    where onSameRow(rowpeerindex,index1)
                    && !result.Cells[rowpeerindex].isFixed
                    && rowpeerindex != index1 && rowpeerindex != index2
                    && result.Cells[rowpeerindex].candidates.Intersect(cell.candidates).Any()
                    select rowpeerindex).ToList<int>());
                    }
                    if (onSameColumn(index1,index2))
                    {
                        affectedCells.AddRange((
                    from colpeerindex in SamuraiSudokuPuzzle.Peers[index1]
                    where onSameColumn(colpeerindex,cell.index)
                    && !result.Cells[colpeerindex].isFixed
                    && colpeerindex != index1 && colpeerindex != index2
                    && result.Cells[colpeerindex].candidates.Intersect(cell.candidates).Any()
                    select colpeerindex).ToList<int>());
                    }
                    if (onSameBlock(index1,index2))
                    {
                        affectedCells.AddRange((
                    from blockpeerindex in SamuraiSudokuPuzzle.Peers[index1]
                    where onSameBlock(blockpeerindex,cell.index)
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
                        step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.RemoveCandidates, cell.candidates, new List<int> { cell.index, cellsEqualinCandidates[0].index }, affectedCells, SamuraiSudokuRules.LockedPair);
                        return result;
                    }
                }
            }
            ///////// END   - Locked Pair /////////
            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static SamuraiSudokuPuzzle SolveStepUsingHiddenSingle(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - Hidden Single /////////
            foreach (SamuraiCell cell in result.Cells.FindAll(c => !c.isFixed))
            {

                List<int> RowPeers = (
                    from rowpeerindex in SamuraiSudokuPuzzle.Peers[cell.index]
                    where onSameRow(rowpeerindex,cell.index)
                    && !result.Cells[rowpeerindex].isFixed
                    select rowpeerindex).ToList<int>();
                List<int> ColPeers = (
                    from colpeerindex in SamuraiSudokuPuzzle.Peers[cell.index]
                    where onSameColumn(colpeerindex,cell.index)
                    && !result.Cells[colpeerindex].isFixed
                    select colpeerindex).ToList<int>();
                List<int> BlockPeers = (
                    from blockpeerindex in SamuraiSudokuPuzzle.Peers[cell.index]
                    where onSameBlock(blockpeerindex,cell.index)
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
                        step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, rowpeerswithvalue[0], i, SamuraiSudokuRules.HiddenSingle);
                        return PutValue(result, rowpeerswithvalue[0], i);
                    }
                    else if (colpeerswithvalue.Count == 1)
                    {
                        step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, colpeerswithvalue[0], i, SamuraiSudokuRules.HiddenSingle);
                        return PutValue(result, colpeerswithvalue[0], i);
                    }
                    else if (blockpeerswithvalue.Count == 1)
                    {
                        step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, blockpeerswithvalue[0], i, SamuraiSudokuRules.HiddenSingle);
                        return PutValue(result, blockpeerswithvalue[0], i);
                    }
                }
            }
            ///////// END - Hidden Single /////////

            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static SamuraiSudokuPuzzle SolveStepUsingNakedSingle(SamuraiSudokuPuzzle input, out SamuraiSudokuSolverStep step)
        {
            SamuraiSudokuPuzzle result = input;

            ///////// START - Naked Single  /////////
            foreach (SamuraiCell cell in result.Cells.FindAll(c => !c.isFixed && c.value == 0 && c.candidates.Count == 1))
            {
                step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, cell.index, cell.candidates[0], SamuraiSudokuRules.NakedSingle);
                return PutValue(result, cell.index, cell.candidates[0]);

            }
            ///////// END - Naked Single    /////////

            step = new SamuraiSudokuSolverStep(SamuraiSudokuStepType.PutValue, -1, -1, SamuraiSudokuRules.NotValid);
            return result;
        }

        public static int ratePuzzle(SamuraiSudokuPuzzle puzzle, out int weight)
        {
            SamuraiSudokuPuzzle tempPuzzle = puzzle.Clone() as SamuraiSudokuPuzzle;
            SamuraiSudokuSolverStep step;
            int rate = 0;
            weight = 0;
            do
            {
                tempPuzzle = SamuraiSudokuHumanSolver.SolveStep(tempPuzzle, out step);
                if (step.rule == SamuraiSudokuRules.NakedSingle && rate < 1)
                    rate = 1;
                if (step.rule == SamuraiSudokuRules.HiddenSingle && rate < 1)
                    rate = 1;
                if (step.rule == SamuraiSudokuRules.LockedCandidatesClaiming && rate < 2)
                    rate = 2;
                if (step.rule == SamuraiSudokuRules.LockedCandidatesPointing && rate < 2)
                    rate = 2;
                if (step.rule == SamuraiSudokuRules.LockedPair && rate < 2)
                    rate = 2;
                if (step.rule == SamuraiSudokuRules.HiddenPair && rate < 2)
                    rate = 2;
                if (step.rule == SamuraiSudokuRules.LockedTriple && rate < 2)
                    rate = 2;
                if (step.rule == SamuraiSudokuRules.XWing && rate < 3)
                    rate = 3;
                switch (step.rule)
                {
                    case SamuraiSudokuRules.NakedSingle: weight += 4;
                        break;
                    case SamuraiSudokuRules.HiddenSingle: weight += 14;
                        break;
                    case SamuraiSudokuRules.LockedCandidatesPointing: weight += 50;
                        break;
                    case SamuraiSudokuRules.LockedCandidatesClaiming: weight += 50;
                        break;
                    case SamuraiSudokuRules.LockedPair: weight += 40;
                        break;
                    case SamuraiSudokuRules.LockedTriple: weight += 60;
                        break;
                    case SamuraiSudokuRules.HiddenPair: weight += 70;
                        break;
                    case SamuraiSudokuRules.Other:
                        break;
                    case SamuraiSudokuRules.NotValid:
                        break;
                    case SamuraiSudokuRules.XWing: weight += 140;
                        break;
                    default:
                        break;
                }
                //Console.WriteLine(step.ToString());
            } while (step.rule != SamuraiSudokuRules.NotValid);
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
        public static void SolveStepByStep(SamuraiSudokuPuzzle puzzle)
        {
            SamuraiSudokuPuzzle tempPuzzle = puzzle.Clone() as SamuraiSudokuPuzzle;
            SamuraiSudokuSolverStep step;
            do
            {
                tempPuzzle = SamuraiSudokuHumanSolver.SolveStep(tempPuzzle, out step);
                Console.WriteLine(step.ToString());
            } while (step.rule != SamuraiSudokuRules.NotValid);
        }
    }

    public enum SamuraiSudokuStepType { PutValue, RemoveCandidates }

    public class SamuraiSudokuSolverStep
    {
        public int index;
        public List<int> indexesRuleAppliedTo;
        public SamuraiSudokuRules rule;
        public int value;
        public List<int> indexesAffected;
        public SamuraiSudokuStepType type;
        public List<int> values;

        public SamuraiSudokuSolverStep(SamuraiSudokuStepType type, int index, int value, SamuraiSudokuRules rule)
        {
            this.index = index;
            this.value = value;
            this.rule = rule;
            this.type = type;
        }

        public SamuraiSudokuSolverStep(SamuraiSudokuStepType type, List<int> values, List<int> indexesRuleAppliedTo, List<int> indexesAffected, SamuraiSudokuRules rule)
        {
            this.indexesRuleAppliedTo = indexesRuleAppliedTo;
            this.indexesAffected = indexesAffected;
            this.rule = rule;
            this.type = type;
            this.values = values;
        }

        public override string ToString()
        {
            if (this.type == SamuraiSudokuStepType.PutValue)
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
            //return "r" + SamuraiSudokuHumanSolver.Rows[index] + "c" + SamuraiSudokuHumanSolver.Cols[index];
            return "r" + SamuraiSudokuHumanSolver.Rows[index][0] + "c" + SamuraiSudokuHumanSolver.Cols[index][0];
        }

        
    }
}
