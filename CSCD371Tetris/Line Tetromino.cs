using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class Line_Tetromino : Tetromino
    {
        public Line_Tetromino()
        {
            this.Spritesheet = new int[4, 4, 4] { { { 0, 0, 6, 0 }, { 0, 0, 0, 0 }, { 0, 6, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 6, 0 }, { 0, 0, 0, 0 }, { 0, 6, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 6, 0 }, { 0, 0, 0, 0 }, { 0, 6, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 6, 0 }, { 6, 6, 6, 6 }, { 0, 6, 0, 0 }, { 6, 6, 6, 6 } } };
        }
    }
}
