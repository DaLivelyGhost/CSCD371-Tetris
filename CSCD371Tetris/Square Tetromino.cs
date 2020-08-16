using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class Square_Tetromino : Tetromino
    {
        public Square_Tetromino()
        {
            this.Spritesheet = new int[4, 4, 4] { { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 7, 7, 0 }, { 0, 7, 7, 0 }, { 0, 7, 7, 0 }, { 0, 7, 7, 0 } },
                                                  { { 0, 7, 7, 0 }, { 0, 7, 7, 0 }, { 0, 7, 7, 0 }, { 0, 7, 7, 0 } } };
        }
    }
}
