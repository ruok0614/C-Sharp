using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;

namespace TickTacToe.Models
{
    interface IPieceInputHundler
    {
        (int x,int y) GetPoint(TicTacBoard board, TicTacPiece piece);
    }
}
