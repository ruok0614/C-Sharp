using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TickTackToe.Views;
using TickTacToe.Views;

namespace TickTacToe.ViewModels
{
	public class MainViewModel: BindableBase
	{
		private ICommand navigateCommand;

		public MainPage View { get; private set; } = null;
		public ObservableCollection<string> PlayerTypes { get; } = new ObservableCollection<string> { PlayerType.CPU.ToString(), PlayerType.Player.ToString(), PlayerType.StrongCPU.ToString() };
		public ObservableCollection<int> PieceLength { get; } = new ObservableCollection<int> {3,4,5,6,7,8,9,10};
		public ObservableCollection<int> WinNumber { get; } = new ObservableCollection<int> { 3, 4, 5, 6, 7, 8 };

		public ICommand NavigateNextCommand { get; protected set; }

		public string SelectedCrossPlayerType { set; get; }

		public string SelectedRoundPlayerType { set; get; }

		public int SelectedPieceLength { set; get; }

		public int SelectedWinNumber { set; get; }

		/// <summary>
		/// 初期化処理を行います。
		/// </summary>
		/// <param name="gemePage"></param>
		public void Initialize(MainPage mainPage)
		{
			this.View = mainPage;
			this.SelectedCrossPlayerType = this.PlayerTypes.First();
			this.SelectedRoundPlayerType = this.PlayerTypes.First();
			this.SelectedPieceLength = this.PieceLength.First();
			this.SelectedWinNumber = this.WinNumber.First();

		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainViewModel()
		{

		}

		public ICommand GameStartCommand
		{
			get
			{

				return new DelegateCommand(() =>
				{
					Enum.TryParse(typeof(PlayerType), this.SelectedRoundPlayerType, out var round);
					Enum.TryParse(typeof(PlayerType), this.SelectedCrossPlayerType, out var cross);
					this.View.Frame.Navigate(typeof(GamePage), (round,cross,this.SelectedPieceLength,this.SelectedWinNumber));
				}
				);
			}
		}



	}
}
