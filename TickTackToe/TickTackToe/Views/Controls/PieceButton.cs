using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;
using TickTacToe.ViewModels.support;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TickTacToe.Views.Controls
{
	/// <summary>
	/// 〇×ゲームの駒コントロール
	/// </summary>
	public class PieceButton:Button
	{
		private ImageBrush roundImageBrush;
		private ImageBrush crossImageBrush;
		private TicTacPiece.Type type;

		public NotifyBoard Board
		{
			get { return (NotifyBoard)GetValue(BoardProperty); }
			set { SetValue(BoardProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty YProperty =
			DependencyProperty.Register("Y", typeof(int), typeof(PieceButton), new PropertyMetadata(0, XYChanged));
		// Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty XProperty =
			DependencyProperty.Register("X", typeof(int), typeof(PieceButton), new PropertyMetadata(0, XYChanged));
		// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BoardProperty =
			DependencyProperty.Register(nameof(Board), typeof(NotifyBoard), typeof(PieceButton), new PropertyMetadata(null,BoardChanged));

		/// <summary>
		/// 座標x
		/// </summary>
		public int X
		{		
			get { return (int)GetValue(XProperty); }
			set { SetValue(XProperty, value); }
		}

		/// <summary>
		/// 座標y
		/// </summary>
		public int Y
		{
			get { return (int)GetValue(YProperty); }
			set { SetValue(YProperty, value); }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PieceButton()
		{
			this.Resources.Source = new Uri("ms-appx:///Views/Styles/StyleDictionary.xaml");
			this.Style = (Style)this.Resources["DisplayEffectInvalidButtonStyle"];
			this.roundImageBrush = CreateImageBrush("ms-appx:///Views/Images/maru.png");
			this.crossImageBrush = CreateImageBrush("ms-appx:///Views/Images/batu.png");
			this.Background = new SolidColorBrush(Colors.Transparent);
			this.CommandParameter = (0, 0);
		}


		private static void XYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(d is PieceButton piceButton))
			{
				return;
			}
			piceButton.CommandParameter = (piceButton.X, piceButton.Y);
		}
		
		private static void BoardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if(!(d is PieceButton piceButton))
			{
				return;
			}

			var piace = piceButton.Board.Board[piceButton.Y,piceButton.X].Content;

			switch(piace)
			{
				case (TicTacPiece.Type.None):
					piceButton.Background = new SolidColorBrush(Colors.Transparent);
					break;
				case (TicTacPiece.Type.Cross):
					piceButton.Background = piceButton.crossImageBrush;
					break;
				case (TicTacPiece.Type.Round):
					piceButton.Background = piceButton.roundImageBrush;
					break;
			}
		}

		private static ImageBrush CreateImageBrush(string imageSource)
		{
			var imageBrush = new ImageBrush();
			var uri = new Uri(imageSource);
			var normalImage = new BitmapImage(uri);
			imageBrush.ImageSource = normalImage;

			return imageBrush;
		}
	}
}
