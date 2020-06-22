using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;

namespace TickTacToe.Models
{
	public class CpuPlayer : IInputHandler
	{
		public async Task<(int, int)> GetPoint(TicTacBoard board, TicTacPiece.Type active)
		{
			await Task.Delay(TimeSpan.FromSeconds(1));
					var settableList = new List<(int x,int y )>();
					for(int x = 0; x < board.Width; x++)
					{
						for(int y = 0; y < board.Height; y++)
						{
							if(board.CanSetPiece(x, y))
							{
								settableList.Add((x, y));
							}
						}
					}
					var randomNumber = new Random().Next(0,settableList.Count);
			return (settableList[randomNumber].x, settableList[randomNumber].y);
		}

    }
}
