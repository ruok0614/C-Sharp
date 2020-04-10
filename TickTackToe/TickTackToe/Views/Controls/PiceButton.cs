using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TickTacToe.Views.Controls
{
	public class PiceButton:Button
	{
		private ImageBrush roundImageBrush;
		private ImageBrush crossImageBrush;
		private TicTacPiece.Type type;

		public TicTacPiece.Type Type
		{
			get { return (TicTacPiece.Type)this.GetValue(TypeProperty); }
			set { this.SetValue(TypeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TypeProperty =
			DependencyProperty.Register("Type", typeof(TicTacPiece.Type), typeof(PiceButton), new PropertyMetadata(TicTacPiece.Type.None, TypeChanged));

		public PiceButton()
		{
			this.roundImageBrush = CreateImageBrush("ms-appx:///Views/Images/maru.jpg");
			this.crossImageBrush = CreateImageBrush("ms-appx:///Views/Images/batu.jpg");
		}

		private static void TypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if(!(d is PiceButton piceButton))
			{
				return;
			}

			switch(piceButton.type)
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
		private void PiceButton_Click(object sender, RoutedEventArgs e)
		{
			this.Type = TicTacPiece.Type.Cross;
		}



	}
}
