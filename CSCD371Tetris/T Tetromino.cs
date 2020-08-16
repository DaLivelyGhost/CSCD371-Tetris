using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class T_Tetromino : Tetromino
    {
        public T_Tetromino()
        {
            this.Spritesheet = new int[4, 4, 4] { { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 0, 0 }, { 0, 5, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 5, 0 } },
                                                  { { 0, 0, 5, 0 }, { 0, 5, 5, 0 }, { 5, 5, 5, 0 }, { 0, 5, 5, 0 } },
                                                  { { 0, 5, 5, 5 }, { 0, 5, 0, 0 }, { 0, 5, 0, 0 }, { 0, 0, 5, 0 } } };
        }
    }
}
