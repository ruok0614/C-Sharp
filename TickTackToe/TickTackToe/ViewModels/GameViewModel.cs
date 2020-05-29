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
using TickTacToe.Views;
using TickTacToe.Views.Messengers;

namespace TickTacToe.ViewModels
{
	// ***************************宿題**************************
	// イベントハンドラーを増やす。ViewModelが賢すぎる。
	public class GameViewModel: BindableBase
	{
		public GamePage View { get; private set; } = null;
		private GameFacilitator gameFacilitator;
		private ICommand setPiaceCommand;
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

		/// <summary>
		/// ピースを置くコマンドを取得します。
		/// </summary>
		public ICommand SetPiaceCommand
		{
			get
			{
				return this.setPiaceCommand;
			}
			private set
			{
				this.setPiaceCommand = value;
				this.RaisePropertyChanged(nameof(SetPiaceCommand));
			}
		}

		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GameViewModel(int width, int height)
		{
			// TODO MainViewからゲームモードを選んでそのモードによって異なるsetCommandをで差し込むようにする。

			this.Widrh = width;
			this.Height = height;
			this.gameFacilitator = new GameFacilitator(width, height);
			this.gameFacilitator.gameFinished += GameFinished;
			this.gameFacilitator.gameDrawed += GameDrawed;
			this.gameFacilitator.boardChanged += BoardPropertyChanged;
		}

		/// <summary>
		/// 初期化処理を行います。
		/// </summary>
		/// <param name="gemePage"></param>
		public void Initialize(GamePage gemePage)
		{
			this.View = gemePage;
		}

		public void StartGame(GameType gameType)
		{
			switch(gameType)
			{
				case GameType.PvP:
					this.SetPiaceCommand = this.SetPiecePvP();
					break;
				case GameType.PvC:
					this.SetPiaceCommand = this.SetPiecePvC();
					break;
				case GameType.Com:
					// TODO 課題3で実装
					break;
			}
			this.gameFacilitator.Start();
		}

		public ICommand StartCommand
		{
			get
			{
				return this.startCommand ?? (this.startCommand = new DelegateCommand(
				() =>
				{
					gameFacilitator.Start();
				}
				));
			}
		}

		private void BoardPropertyChanged()
		{
			this.RaisePropertyChanged(nameof(this.Board));
			this.RaisePropertyChanged(nameof(this.Active));
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
					this.gameFacilitator.SetPiece(xy.Item1, xy.Item2);
				},
				(xy) =>
				{
					return gameFacilitator.CanSetPiece(xy.Item1, xy.Item2);
				}
				);
		}

		private ICommand SetPiecePvC()
		{
			return new DelegateCommandT<(int, int)>(
				(xy) =>
				{

					this.gameFacilitator.SetPiece(xy.Item1, xy.Item2);


					if(this.gameFacilitator.IsFinish())
					{
						return;
					}

					var settableList = new List<(int x,int y )>();
					for(int x = 0; x < this.Widrh; x++)
					{
						for(int y = 0; y < this.Height; y++)
						{
							if(this.gameFacilitator.CanSetPiece(x, y))
							{
								settableList.Add((x, y));
							}
						}
					}
					var randomNumber = new Random().Next(0,settableList.Count);
					this.gameFacilitator.SetPiece(settableList[randomNumber].x, settableList[randomNumber].y);
				},
				(xy) =>
				{
					return gameFacilitator.CanSetPiece(xy.Item1, xy.Item2);
				}
				);
		}

	}
}
