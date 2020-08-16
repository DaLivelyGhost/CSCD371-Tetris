using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class Luigi_Tetromino : Tetromino
    {
        public Luigi_Tetromino()
        {
            this.Spritesheet = new int[4, 4, 4] { { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 2, 0, 0 }, { 0, 0, 0, 0 }, { 0, 2, 2, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 2, 0, 0 }, { 0, 2, 2, 2 }, { 0, 0, 2, 0 }, { 0, 0, 2, 0 } },
                                                  { { 0, 2, 2, 0 }, { 0, 2, 0, 0 }, { 0, 0, 2, 0 }, { 2, 2, 2, 0 } } };
        }
    }
}
