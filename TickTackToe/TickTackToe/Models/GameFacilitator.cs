using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTacToe.Models;

namespace TickTackToe.Models
{
    /// <summary>
    /// /ゲームの進行を管理するクラスです。
    /// </summary>
    public class GameFacilitator
    {
        public Action gameFinished;
        public Action gameDrawed;
        private const int ARRANGEMENT_NUMBER = 3;
        private readonly TicTacBoard board;
        private bool isFinish = false;
        private TicTacPiece.Type active = TicTacPiece.Type.None;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameFacilitator(int width, int height)
        {
            this.board = new TicTacBoard(width, height); 
        }

       /// <summary>
       /// 現在プレイ中の駒タイプを取得します。
       /// </summary>
        public TicTacPiece.Type Active
        {
            get => this.active;
        }

        /// <summary>
        /// 座標(x,y)にある駒を取得します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public TicTacPiece.Type GetPieceContent(int x, int y)
        {
            return this.board.GetPiece(x,y).Content;
        }

        /// <summary>
        /// ゲームをスタートします。
        /// ○×ゲームでは必ず先行が○になります。
        /// </summary>
        public void Start()
        {
            this.board.Initialize();
            this.isFinish = false;
            this.active = TicTacPiece.Type.Round;
        }

        /// <summary>
        /// 座標(x,y)に指定した駒をセットします。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        public void SetPiece(int x, int y, TicTacPiece.Type type)
        {
            if(this.active == TicTacPiece.Type.None || this.isFinish)
            {
                return;
            }

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
          
            if (!this.board.SetPeace(x, y, piece))
            {
                //TODO 駒が置けないアクションがあれば記述
                return;
            }

            if (this.IsFinish(x, y, piece))
            {
               this.gameFinished.Invoke();
                return;
            }
            if (this.IsDraw())
            {
                this.gameDrawed.Invoke();
                return;
            }

            if (this.active == TicTacPiece.Type.Cross)
            {
                this.active = TicTacPiece.Type.Round;
            }
            else if (this.active == TicTacPiece.Type.Round)
            {
                this.active = TicTacPiece.Type.Cross;
            }

        }

        /// <summary>
        /// ボードの座標(x,y)に駒が設置できるかどうか。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsSetPiece(int x, int y)
        {
            return this.board.GetPiece(x, y).Content == TicTacPiece.Type.None;
        }

        /// <summary>
        /// 引き分けかどうか
        /// </summary>
        /// <returns></returns>
        public bool IsDraw()
        {
            var count = 0;
            for (int y = 0; y < this.board.Height; y++)
            {
                for (int x = 0; x < this.board.Width; x++)
                {
                    if(this.board.GetPiece(y,x).Content != TicTacPiece.Type.None)
                    {
                        count++;
                    }
                }
            }
            if(count == this.board.Height * this.board.Width)
            {
                this.isFinish = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// ゲームが終了した（勝敗が決定した）かどうか。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        public bool IsFinish(int x, int y, TicTacPiece piece)
        {

            for (var d = 0; d < TicTacBoard.DIRECTION_NUM; d++)
            {
                var xx = x;
                var yy = y;
                xx += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.X];
                yy += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.Y];
                if (this.CheckIndexOutOrNone(xx, yy))
                {
                    continue;
                }
                var count = 1;
                while (piece.Content == this.board.GetPiece(xx,yy).Content)
                {
                    count++;
                    xx += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.X];
                    yy += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.Y];
                    if(count == ARRANGEMENT_NUMBER)
                    {
                        this.isFinish = true;
                        return true;
                    }
                    if (this.CheckIndexOutOrNone(xx, yy))
                    {
                        break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 駒がボードの範囲外に出ていないかどうか
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CheckIndexOutOrNone(int x, int y)
        {
            if ( (x < 0 || x >= this.board.Width) ||  ( y < 0 || y>= this.board.Height))
            {
                return true;
            }

            if(this.board.GetPiece(x,y).Content == TicTacPiece.Type.None)
            {
                return true;
            }
            return false;
        }

    }
}
