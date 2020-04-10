using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTacToe.Models;

namespace TickTackToe.Models
{
    public class GameFacilitator
    {
        private const int ARRANGEMENT_NUMBER = 3;
        private readonly TicTacBoard board;
        private readonly IPieceInputHundler inputHundler;

        public GameFacilitator(int width, int height)
        {
            this.board = new TicTacBoard(width, height);
            Start();
        }

        public TicTacPiece.Type GetPieceContent(int x, int y)
        {
            return this.board.GetPiece(x,y).Content;
        }

        public void Start()
        {
        }

        public void SetPiece(int x, int y, TicTacPiece.Type type)
        {
            var piece = null as TicTacPiece;
            switch(type)
            {
                case TicTacPiece.Type.None:
                    piece = TicTacPiece.CreateNone();
                    break;
                case TicTacPiece.Type.Cross:
                    piece = TicTacPiece.CreateCross();
                    break;
                case TicTacPiece.Type.Round:
                    piece = TicTacPiece.CreateRound();
                    break;
            }

            if (!board.SetPeace(x, y, piece))
            {
                // 駒が置けない
            }

            if (IsFinish(x, y, piece))
            {
                // TODO　終了通知
            }

        }

        public bool IsFinish(int x, int y, TicTacPiece piece)
        {

            for (var d = 0; d < TicTacBoard.DIRECTION_NUM; d++)
            {
                var xx = x;
                var yy = y;
                xx += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.X];
                yy += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.Y];
                if (CheckIndexOutOrNone(xx, yy))
                {
                    continue;
                }
                var count = 1;
                while (piece.Content == board.GetPiece(xx,yy).Content)
                {
                    count++;
                    xx += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.X];
                    yy += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.Y];
                    if(count == ARRANGEMENT_NUMBER)
                    {
                        return true;
                    }
                    if (CheckIndexOutOrNone(xx, yy))
                    {
                        break;
                    }
                }
            }
            return false;
        }

        private bool CheckIndexOutOrNone(int x, int y)
        {
            if ( (x < 0 || x >= board.Width) ||  ( y < 0 || y>= board.Height))
            {
                return true;
            }

            if(board.GetPiece(x,y).Content == TicTacPiece.Type.None)
            {
                return true;
            }
            return false;
        }

    }
}
