using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTackToe.Models
{
    public class TicTacBoard : BaseBoard<TicTacPiece.Type>
    {

        public TicTacBoard(int width,int height):base(width,height)
        {

        }
        public override void Initialize()
        {
            for(int y = 0; y< Height; y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    board[y, x] = TicTacPiece.CreateNone();
                }
            }
        }

        public override bool SetPeace(int x, int y, IPiece<TicTacPiece.Type> piece)
        {
            if(GetPiece(x, y).Content != TicTacPiece.Type.None)
            {
                return false;
            }
            board[y, x] = piece;

            return true;
        }
    }
}
