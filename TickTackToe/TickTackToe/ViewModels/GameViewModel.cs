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
using TickTacToe.Models;
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
		private IInputHandler roundPlayer;
		private IInputHandler crossPlayer;

		#region プロパティ

		public int Widrh { get; private set ; }
		public int Height { get; private set; }

		/// <summary>
		/// 現在アクティブな駒を取得します。
		/// </summary>
		public TicTacPiece.Type Active
		{
			get => gameFacilitator == null ? TicTacPiece.Type.None: gameFacilitator.Active;
		}

		/// <summary>
		/// ボードを取得します。
		/// </summary>
		public NotifyBoard Board
		{
			get => Active == TicTacPiece.Type.None ? null:NotifyBoard.Create(gameFacilitator.Board);
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
		}

		/// <summary>
		/// 初期化処理を行います。
		/// </summary>
		/// <param name="gemePage"></param>
		public void Initialize(GamePage gemePage)
		{
			this.View = gemePage;
		}

		public void StartGame(PlayerType round,PlayerType cross,int length,int winNumber)
		{
			switch (round)
			{
				case PlayerType.CPU:
					roundPlayer = new CpuPlayer();
					break;
				case PlayerType.Player:
					roundPlayer = new GuiPlayler();
					break;
				default:
					break;
			}
			switch (cross)
			{
				case PlayerType.CPU:
					crossPlayer = new CpuPlayer();
					break;
				case PlayerType.Player:
					crossPlayer = new GuiPlayler();
					break;
				default:
					break;
			}
			this.Widrh = length;
			this.Height = length;
			this.RaisePropertyChanged(nameof(Height));
			this.RaisePropertyChanged(nameof(Widrh));
			this.SetPiaceCommand = this.SetPieceCommand();

			this.gameFacilitator = new GameFacilitator(this.Widrh, this.Height, this.roundPlayer, this.crossPlayer, winNumber);
			this.gameFacilitator.gameFinished += GameFinished;
			this.gameFacilitator.gameDrawed += GameDrawed;
			this.gameFacilitator.boardChanged += BoardPropertyChanged;
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

		private ICommand SetPieceCommand()
		{
			return new DelegateCommandT<(int, int)>(
				(xy) =>
				{
					var player = null as GuiPlayler;

					switch (Active)
					{
						case TicTacPiece.Type.None:
							break;
						case TicTacPiece.Type.Cross:
							player = (GuiPlayler)this.crossPlayer;
							break;
						case TicTacPiece.Type.Round:
							player = (GuiPlayler)this.roundPlayer;
							break;
					}
					player.CommandExcuted(xy.Item1,xy.Item2);
				},
				(xy) =>
				{
					return gameFacilitator.CanSetPiece(xy.Item1, xy.Item2);
				}
				);
		}
	}
}
