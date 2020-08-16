using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD371Tetris
{
    class Tetromino
    {
        public int[,,] Spritesheet;

        public Tetromino()
        {
            Spritesheet = new int[1, 4, 4];
        }
    }
}
