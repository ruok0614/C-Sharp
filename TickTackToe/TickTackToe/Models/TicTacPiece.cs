using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTackToe.Models
{
    /// <summary>
    /// 三目並べの駒です．
    /// </summary>
    public class TicTacPiece : IPiece<TicTacPiece.Type>
    {
        /// <summary>
        /// 〇を作成します．
        /// </summary>
        /// <returns></returns>
        public static TicTacPiece CreateRound()
        {
            return new TicTacPiece(Type.Round);
        }

        /// <summary>
        /// ×を作成します．
        /// </summary>
        /// <returns></returns>
        public static TicTacPiece CreateCross()
        {
            return new TicTacPiece(Type.Cross);
        }

        /// <summary>
        /// 駒が置いていない状態を作成します．
        /// </summary>
        /// <returns></returns>
        public static TicTacPiece CreateNone()
        {
            return new TicTacPiece(Type.None);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tic"></param>
        private TicTacPiece(Type tic)
        {
            Content = tic;
        }

        public Type Content {
            get => Content;
            private set { }
         }


        /// <summary>
        /// 三目並べ用の駒
        /// </summary>
        public enum Type
        {
        　// 〇
        　Round,
        　// ×
            Cross,
            //ない
            None
        }
           
    }
}
