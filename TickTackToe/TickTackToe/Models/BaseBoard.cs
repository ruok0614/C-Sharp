using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTackToe.Models
{
    public abstract class BaseBoard<T> 
    {
        public static readonly int DIRECTION_NUM = 8;
        public static readonly int X = 1;
        public static readonly int Y = 0;
        public static readonly int[,] ALL_DIRECTION = new int[,]{
            {0,-1},//左
            {-1,-1},//左上
            {-1,0},//上
            {-1,1},//右上
            {0,1},//右
            {1,1},//右下
            {1,0},//下
            {1,-1},//左下
        };

        /// <summary>
        /// ボードの縦のマス数
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// ボードの横のマス数
        /// </summary>
        public int Height { get; private set; }

        public IPiece<T>[,] board;

        /// <summary>
        /// コンストラクタ
        /// 初期状態がある場合は<see cref="Action"/>を差し込んでください．
        /// </summary>
        public BaseBoard(int width,int height)
        {
            this.Width = width;
            this.Height = height;
            Initialize();
        }

        public IPiece<T> GetPiece(int x,int y)
        {
            return board[y, x];
        }

        /// <summary>
        /// ボードの初期化をします
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// ボードに駒をセットします．
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        public abstract bool SetPeace(int x, int y, IPiece<T> piece);


    }
}
