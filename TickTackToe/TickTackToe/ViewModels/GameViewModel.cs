using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TickTackToe.Models;
using TickTackToe.Views;

namespace TickTacToe.ViewModels
{
	public class GameViewModel:Observable
	{
		public GamePage View { get; private set; } = null;
		private GameFacilitator gameFacilitator;
		private TicTacPiece.Type active;

		public TicTacPiece.Type Active
		{
			get => active;
			private set { this.Set(ref this.active, value, nameof(this.Active)); }
		}

		#region コマンド
		/// <summary>
		/// 会議室名を取得します。
		/// </summary>
		public TicTacPiece.Type Borad00
		{
			get => this.gameFacilitator.GetPieceContent(0,0);
		}
		#endregion


		public void Initialize(GamePage gemePage)
		{
			this.View = gemePage;
			this.gameFacilitator = new GameFacilitator(3,3);
		}

		public DelegateCommand SetCommand(int x , int y)
		{
			return new DelegateCommand(
				() => {
					this.gameFacilitator.SetPiece(x, y, active);	
				},
				() => { }
				);
		}
	}
}
