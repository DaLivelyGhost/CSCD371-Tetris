using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class Waluigi_Tetromino : Tetromino
    {
        public Waluigi_Tetromino()
        {
            this.Spritesheet = new int[4, 4, 4] { { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 1, 0 }, { 0, 0, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } },
                                                  { { 0, 0, 1, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 1, 1, 1, 0 } },
                                                  { { 0, 1, 1, 0 }, { 0, 1, 1, 1 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 } } };
            
        }

    }
}
