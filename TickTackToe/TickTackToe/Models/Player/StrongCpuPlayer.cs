using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;

namespace TickTacToe.Models.Player
{
	/// <summary>
	/// 強いCPUのクラス
	/// </summary>
    public class StrongCpuPlayer: IInputHandler
    {
		public async Task<(int, int)> GetPoint(TicTacBoard board, TicTacPiece.Type active)
		{
			await Task.Delay(TimeSpan.FromSeconds(1));
            var priorityMap = new double[board.Height, board.Width];
            var maxPriority = 0.0;
			for (int x = 0; x < board.Width; x++)
			{
				for (int y = 0; y < board.Height; y++)
				{
                    var priorityNumber = 0.0;
                    if (board.CanSetPiece(x, y))
					{
                        var crossPriority = CalcPriority(x, y, TicTacPiece.Type.Cross, board);
                        var roundPriority = CalcPriority(x, y, TicTacPiece.Type.Round, board);

                        //自分の駒の優先度を1UP
                        switch (active)
                        {
                            case TicTacPiece.Type.None:
                                break;
                            case TicTacPiece.Type.Cross:
                                crossPriority += 0.5;
                                break;
                            case TicTacPiece.Type.Round:
                                roundPriority += 0.5;
                                break;
                        }
                        if(roundPriority>= crossPriority)
                        {
                            priorityNumber = roundPriority;
                            priorityNumber += crossPriority/100;
                        }
                        else
                        {
                            priorityNumber = crossPriority;
                            priorityNumber += roundPriority / 100;
                        }
                    }
                    priorityMap[y, x] = priorityNumber;
                    if(priorityNumber > maxPriority)
                    {
                        maxPriority = priorityNumber;
                    }
                }
			}
            var settableList = new List<(int x, int y)>();
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    if(priorityMap[y,x]== maxPriority)
                    {
                        settableList.Add((x, y));
                    }
                }
            }
            var primaryPoint = settableList.OrderBy(v => Math.Abs(v.x - board.Width / 2.0) + Math.Abs(v.y - board.Height / 2));
            return primaryPoint.First();
            //var randomNumber = new Random().Next(0, settableList.Count);
            //return (settableList[randomNumber].x, settableList[randomNumber].y);
        }

        private double CalcPriority(int x, int y, TicTacPiece.Type type, TicTacBoard board)
        {
            var maxCount = 0;
            for (var d = 0; d < TicTacBoard.DIRECTION_NUM / 2; d++)
            {
                var count = 1;
                foreach (int fc in new int[] { 1, -1 })
                {
                    var xx = x;
                    var yy = y;
                    xx += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.X] * fc;
                    yy += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.Y] * fc;

                    if (this.CheckIndexOutOrNone(xx, yy,board))
                    {
                        continue;
                    }
                    while (type == board.GetPiece(xx, yy).Content)
                    {
                        count++;
                        xx += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.X] * fc;
                        yy += TicTacBoard.ALL_DIRECTION[d, TicTacBoard.Y] * fc;

                        if (this.CheckIndexOutOrNone(xx, yy, board))
                        {
                            break;
                        }
                    }
                }
                if (count > maxCount)
                {
                    maxCount = count;
                }
            }
            return maxCount;
        }
        /// <summary>
        /// 駒がボードの範囲外に出ていないかどうか
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CheckIndexOutOrNone(int x, int y, TicTacBoard board)
        {
            if ((x < 0 || x >= board.Width) || (y < 0 || y >= board.Height))
            {
                return true;
            }

            if (board.GetPiece(x, y).Content == TicTacPiece.Type.None)
            {
                return true;
            }
            return false;
        }
    }
}
