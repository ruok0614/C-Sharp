using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TickTackToe.Models;
using TickTackToe.Views;
using TickTacToe.Views.Messengers;

namespace TickTacToe.ViewModels
{
	public class GameViewModel: BindableBase
	{
		public GamePage View { get; private set; } = null;
		private GameFacilitator gameFacilitator;
		private DelegateCommandT<(int,int)> setCommand;
		private DelegateCommand startCommand;
		private DialogService dialogService;
		public TicTacPiece.Type Active
		{
			get => gameFacilitator.Active;
		}

		#region プロパティ
		/// <summary>
		/// ボード座標(0,0)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad00
		{
			get => this.gameFacilitator.GetPieceContent(0,0);
		}
		/// <summary>
		/// ボード座標(1,0)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad10
		{
			get => this.gameFacilitator.GetPieceContent(1, 0);
		}
		/// <summary>
		/// ボード座標(2,0)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad20
		{
			get => this.gameFacilitator.GetPieceContent(2, 0);
		}
		/// <summary>
		/// ボード座標(0,1)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad01
		{
			get => this.gameFacilitator.GetPieceContent(0, 1);
		}
		/// <summary>
		/// ボード座標(1,1)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad11
		{
			get => this.gameFacilitator.GetPieceContent(1, 1);
		}
		/// <summary>
		/// ボード座標(2,1)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad21
		{
			get => this.gameFacilitator.GetPieceContent(2, 1);
		}
		/// <summary>
		/// ボード座標(0,2)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad02
		{
			get => this.gameFacilitator.GetPieceContent(0, 2);
		}
		/// <summary>
		/// ボード座標(1,2)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad12
		{
			get => this.gameFacilitator.GetPieceContent(1, 2);
		}
		/// <summary>
		/// ボード座標(2,2)にある駒の種類を取得します。
		/// </summary>
		public TicTacPiece.Type Borad22
		{
			get => this.gameFacilitator.GetPieceContent(2, 2);
		}

		/// <summary>
		/// ダイアログを表示するサービスを取得または設定します。
		/// </summary>
		public DialogService DialogService
		{
			get { return this.dialogService; }
			set { this.SetProperty(ref this.dialogService, value); }
		}

		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GameViewModel()
		{
			this.gameFacilitator = new GameFacilitator(3, 3);
			this.gameFacilitator.gameFinished += GameFinished;
			this.gameFacilitator.gameDrawed += GameDrawed;
		}

		/// <summary>
		/// 初期化処理を行います。
		/// </summary>
		/// <param name="gemePage"></param>
		public void Initialize(GamePage gemePage)
		{
			this.View = gemePage;
		}


		public ICommand SetCommand {
			get {
				return this.setCommand ?? (this.setCommand = new DelegateCommandT<(int, int)>(
				(xy) =>
				{

					this.gameFacilitator.SetPiece(xy.Item1, xy.Item2, Active);
					RaisePropertyChanged(nameof(this.Active));
					BoardPropertyChanged();
				},
				(xy) =>
				{
					return gameFacilitator.IsSetPiece(xy.Item1, xy.Item2);
				}
				));
			}
		}

		public ICommand StartCommand
		{
			get
			{
				return this.startCommand ?? (this.startCommand = new DelegateCommand(
				() =>
				{
					gameFacilitator.Start();
					BoardPropertyChanged();
				}
				));
			}
		}

		private void BoardPropertyChanged()
		{
			RaisePropertyChanged(nameof(this.Borad00));
			RaisePropertyChanged(nameof(this.Borad10));
			RaisePropertyChanged(nameof(this.Borad20));
			RaisePropertyChanged(nameof(this.Borad01));
			RaisePropertyChanged(nameof(this.Borad11));
			RaisePropertyChanged(nameof(this.Borad21));
			RaisePropertyChanged(nameof(this.Borad02));
			RaisePropertyChanged(nameof(this.Borad12));
			RaisePropertyChanged(nameof(this.Borad22));
		}

		
		private void GameFinished()
		{
			var winPlayer = string.Empty;
			if(this.Active == TicTacPiece.Type.Cross)
			{
				winPlayer = "×";
			}
			else
			{
				winPlayer = "○";
			}
			this.dialogService.ShowMessage("ゲーム終了", $"{winPlayer}の勝利です.");
		}
		private void GameDrawed()
		{
			this.dialogService.ShowMessage("ゲーム終了", $"引き分けです．");
		}

	}
}
