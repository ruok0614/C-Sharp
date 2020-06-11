using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;

namespace TickTacToe.Models
{
	public class GuiPlayler : IInputHandler
	{
		public bool excutedCommand = false;
		public (int x, int y) point;

		public void CommandExcuted(int x,int y)
		{
			point = (x, y);
			this.excutedCommand = true;
		}
		
		public async Task<(int, int)> GetPoint(TicTacBoard board)
		{
			while (!excutedCommand)
			{
				try
				{
					await Task.Delay(TimeSpan.FromMilliseconds(200)); 
				}
				catch
				{

				}
			}
			excutedCommand = false;
			return this.point;

		}
	}
}
