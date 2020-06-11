using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;

namespace TickTacToe.ViewModels.support
{
	/// <summary>
	/// Viewへの通知用ボード
	/// </summary>
	public class NotifyBoard: INotifyPropertyChanged
	{
		//　ここもIreadOnryを使う
		public static NotifyBoard Create(IPiece<TicTacPiece.Type>[,] board)
		{
			return new NotifyBoard(board);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private IPiece<TicTacPiece.Type>[,] board;

		private  NotifyBoard(IPiece<TicTacPiece.Type>[,] board)
		{
			this.Board = board;
			RaisePropertyChanged(nameof(Board));
		}
		public IPiece<TicTacPiece.Type>[,] Board
		{
			get => this.board;
			set {
				this.board = value;
				this.RaisePropertyChanged();
			}
		}

		private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	}
}
