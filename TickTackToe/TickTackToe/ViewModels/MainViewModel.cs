using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
		public ICommand NavigateNextCommand { get; protected set; }

		/// <summary>
		/// 初期化処理を行います。
		/// </summary>
		/// <param name="gemePage"></param>
		public void Initialize(MainPage mainPage)
		{
			this.View = mainPage;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainViewModel()
		{

		}

		public ICommand PvPCommand
		{
			get
			{
				return this.navigateCommand ?? this.NavigateCommand(GameType.PvP);
			}
		}
		public ICommand PvCCommand
		{
			get
			{
				return this.navigateCommand ?? this.NavigateCommand(GameType.PvC);
			}
		}

		public ICommand NavigateCommand(GameType type)
		{
			return new DelegateCommand(() =>
			{
				this.View.Frame.Navigate(typeof(GamePage), type);
			}
			);
		}


	}
}
