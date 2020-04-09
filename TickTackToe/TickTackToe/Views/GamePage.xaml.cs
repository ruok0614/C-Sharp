using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
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
        public GamePage()
        {
            this.InitializeComponent();
            this.SizeChanged += UpdateSize;
        }

        /// <summary>
        /// ボードのグリッドが必ず正方形になるように調整しています
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSize(object sender, SizeChangedEventArgs e)
        {
            var boadSize = (int)(this.ActualHeight >= this.ActualWidth ? this.ActualWidth : this.ActualHeight) - 50 ;
            CenterColumnDefinition.Width = new GridLength(boadSize);
            CenterRowDefinition.Height = new GridLength(boadSize);
        }
    }
}
