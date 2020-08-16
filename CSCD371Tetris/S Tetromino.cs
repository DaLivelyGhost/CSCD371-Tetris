using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class S_Tetromino : Tetromino
    {
        public S_Tetromino()
        {
            this.Spritesheet = new int[4, 4, 4] { { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 0, 0 }, { 0, 4, 0, 0 }, { 0, 0, 0, 0 }, { 0, 4, 0, 0 } },
                                                  { { 0, 0, 4, 4 }, { 0, 4, 4, 0 }, { 0, 0, 4, 4 }, { 0, 4, 4, 0 } },
                                                  { { 0, 4, 4, 0 }, { 0, 0, 4, 0 }, { 0, 4, 4, 0 }, { 0, 0, 4, 0 } } };
        }
    }
}
