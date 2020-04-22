using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace TickTacToe.Views.Controls
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class Borad : UserControl
	{


		public int Width
		{
			get { return (int)GetValue(WidthProperty); }
			set { SetValue(WidthProperty, value); }
		}



		public int Height
		{
			get { return (int)GetValue(HeightProperty); }
			set { SetValue(HeightProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HeightProperty =
			DependencyProperty.Register(nameof(Height), typeof(int), typeof(Borad), new PropertyMetadata(0));


		// Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WidthProperty =
			DependencyProperty.Register(nameof(Width), typeof(int), typeof(Borad), new PropertyMetadata(0));


		public Borad()
		{
			
			for(var w = 0; w < Width; w++)
			{
				var colDef = new ColumnDefinition
				{
					Width = new GridLength(1, GridUnitType.Star)
				};
				this.Grid.ColumnDefinitions.Add(colDef);
			}
			for(var h = 0; h < Height; h++)
			{
				var rowDef = new RowDefinition
				{
					Height = new GridLength(1, GridUnitType.Star)
				};
				this.Grid.RowDefinitions.Add(rowDef);
			}
			
			for(var w = 0; w < Width; w++)
			{
				for(var h = 0; h < Height; h++)
				{
					var button = new PieceButton()
					{
						Margin = new Thickness(0),
						HorizontalAlignment = HorizontalAlignment.Stretch,
						VerticalAlignment = VerticalAlignment.Stretch,
						Background = new SolidColorBrush(Colors.AliceBlue),
						X = w,
						Y = h
					};
					Grid.SetColumn(button, w);
					Grid.SetRow(button,h);
				}
			}
			Background = new SolidColorBrush(Colors.Azure);
			this.InitializeComponent();
		}
	}
}
