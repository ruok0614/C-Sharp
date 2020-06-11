using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TickTacToe.Models;
using TickTacToe.ViewModels;
using TickTacToe.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TickTackToe.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MainViewModel();
            this.ViewModel.Initialize(this);
            this.DataContext = this.ViewModel;
            this.roundBox.SelectionChanged += this.SelectRoundChanged;
            this.crossBox.SelectionChanged += this.SelectrossChanged;
            this.winNum.SelectionChanged += this.SelectWinNumChanged;
            this.length.SelectionChanged += this.SelectLengthChanged; 
        }

        private void SelectRoundChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox box))
            {
                return;
            }
            this.ViewModel.SelectedRoundPlayerType = box.SelectedValue.ToString();
        }

        private void SelectrossChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox box))
            {
                return;
            }
            this.ViewModel.SelectedCrossPlayerType = box.SelectedValue.ToString();
        }
        private void SelectWinNumChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox box))
            {
                return;
            }
            this.ViewModel.SelectedWinNumber = (int)box.SelectedValue;
        }
        private void SelectLengthChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox box))
            {
                return;
            }
            this.ViewModel.SelectedPieceLength = (int)box.SelectedValue;
        }
    }
}
