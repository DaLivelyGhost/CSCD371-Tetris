using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class Backward_S_Tetromino : Tetromino
    {
        public Backward_S_Tetromino()
        {
            this.Spritesheet = new int[4, 4, 4] { { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 0, 0 }, { 0, 0, 3, 0 }, { 0, 0, 0, 0 }, { 0, 0, 3, 0 } },
                                                  { { 3, 3, 0, 0 }, { 0, 3, 3, 0 }, { 3, 3, 0, 0 }, { 0, 3, 3, 0 } },
                                                  { { 0, 3, 3, 0 }, { 0, 3, 0, 0 }, { 0, 3, 3, 0 }, { 0, 3, 0, 0 } } };
        }
    }
}
