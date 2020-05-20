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
using TickTacToe.ViewModels.support;
using TickTacToe.Views.Messengers;

namespace TickTacToe.ViewModels
{
	// ***************************宿題**************************
	// イベントハンドラーを増やす。ViewModelが賢すぎる。
	public class GameViewModel: BindableBase
	{
		public GamePage View { get; private set; } = null;
		private GameFacilitator gameFacilitator;
		private ICommand setCommand;
		private DelegateCommand startCommand;
		private DialogService dialogService;
	

		#region プロパティ

		public int Widrh { get; }
		public int Height { get; }

		/// <summary>
		/// 現在アクティブな駒を取得します。
		/// </summary>
		public TicTacPiece.Type Active
		{
			get => gameFacilitator.Active;
		}

		/// <summary>
		/// ボードを取得します。
		/// </summary>
		public NotifyBoard Board
		{
			get => NotifyBoard.Create(gameFacilitator.Board);
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
		public GameViewModel(int width, int height)
		{
			// TODO MainViewからゲームモードを選んでそのモードによって異なるsetCommandをで差し込むようにする。
			this.setCommand = this.SetPiecePvP();
			this.Widrh = width;
			this.Height = height;
			this.gameFacilitator = new GameFacilitator(width, height);
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
				return this.setCommand;
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
			RaisePropertyChanged(nameof(this.Board));
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

		private ICommand SetPiecePvP()
		{
			return new DelegateCommandT<(int, int)>(
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
				);
		}

		private ICommand SetPiecePvC()
		{
			return new DelegateCommandT<(int, int)>(
				(xy) =>
				{

					this.gameFacilitator.SetPiece(xy.Item1, xy.Item2, Active);
					RaisePropertyChanged(nameof(this.Active));
					BoardPropertyChanged();

					if(this.gameFacilitator.IsFinish())
					{
						return;
					}

					var settableList = new List<(int x,int y )>();
					for(int x = 0; x < this.Widrh; x++)
					{
						for(int y = 0; y < this.Height; y++)
						{
							if(this.gameFacilitator.IsSetPiece(x, y))
							{
								settableList.Add((x, y));
							}
						}
					}
					var randomNumber = new Random().Next(0,settableList.Count);
					this.gameFacilitator.SetPiece(settableList[randomNumber].x, settableList[randomNumber].y, Active);
					RaisePropertyChanged(nameof(this.Active));
					BoardPropertyChanged();
				},
				(xy) =>
				{
					return gameFacilitator.IsSetPiece(xy.Item1, xy.Item2);
				}
				);
		}

	}
}
