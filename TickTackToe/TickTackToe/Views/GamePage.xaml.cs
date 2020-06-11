using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TickTacToe.ViewModels;
using TickTacToe.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace TickTackToe.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class GamePage : Page
    {

        public int CellHeight { get; private set; } = 3;
        public int CellWidth{ get; private set; } = 3;

        public GameViewModel ViewModel { get; }

        public GamePage()
        {
            this.InitializeComponent();
            this.ViewModel = new GameViewModel(this.CellWidth, this.CellHeight);
            this.ViewModel.Initialize(this);
            this.ViewModel.DialogService = new TickTacToe.Views.Messengers.DialogService();
            this.DataContext = this.ViewModel;
            this.SizeChanged += this.UpdateSize;
        }

        /// <summary>
        /// ボードのグリッドが必ず正方形になるように調整しています
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSize(object sender, SizeChangedEventArgs e)
        {
            var boadSize = (int)(this.ActualHeight >= this.ActualWidth ? this.ActualWidth : this.ActualHeight) - 50 ;
            this.CenterColumnDefinition.Width = new GridLength(boadSize);
            this.CenterRowDefinition.Height = new GridLength(boadSize);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try {

                var para = e.Parameter;

                if(para is ValueTuple<object, object, int,int> a)
                {
                    this.CellHeight = a.Item3;
                    this.CellWidth = a.Item3;
                    this.ViewModel.StartGame((PlayerType)a.Item1, (PlayerType)a.Item2, a.Item3, a.Item4);
                }


                if (Frame.CanGoBack)
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                }
                else
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                }
            }
            catch
            {

            }
            base.OnNavigatedTo(e);
        }
    }
}
