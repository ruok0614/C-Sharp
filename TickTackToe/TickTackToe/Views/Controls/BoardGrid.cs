using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTacToe.Views.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TickTacToe.Views.Controls
{
	public sealed partial class BoardGrid:Grid
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



		public int MyProperty
		{
			get { return (int)GetValue(MyPropertyProperty); }
			set { SetValue(MyPropertyProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MyPropertyProperty =
			DependencyProperty.Register("MyProperty", typeof(int), typeof(ownerclass), new PropertyMetadata(0));



		// Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HeightProperty =
			DependencyProperty.Register(nameof(Height), typeof(int), typeof(BoardGrid), new PropertyMetadata(0));


		// Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WidthProperty =
			DependencyProperty.Register(nameof(Width), typeof(int), typeof(BoardGrid), new PropertyMetadata(0));


		public BoardGrid()
		{

			this.Width = this.Height = 5;

			for(var w = 0; w < Width; w++)
			{
				var colDef = new ColumnDefinition
				{
					Width = new GridLength(1, GridUnitType.Star)
				};
				this.ColumnDefinitions.Add(colDef);
			}
			for(var h = 0; h < Height; h++)
			{
				var rowDef = new RowDefinition
				{
					Height = new GridLength(1, GridUnitType.Star)
				};
				this.RowDefinitions.Add(rowDef);
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
						X = w,
						Y = h
					};
					Grid.SetColumn(button, w);
					Grid.SetRow(button, h);
					this.Children.Add(button);
				}
			}
			for(var w = 1; w < Width; w++)
			{
				var line = new Line()
				{
					Y2 = 1,
					Stretch = Stretch.Fill,
					Stroke = new SolidColorBrush(Colors.Black),
					StrokeThickness = 10,
					HorizontalAlignment = HorizontalAlignment.Right,
				};
				Grid.SetColumnSpan(line, w);
				Grid.SetRowSpan(line, Width);
				this.Children.Add(line);
			}
			for(var h = 1; h < Height; h++)
			{
				var line = new Line()
				{
					X2 = 1,
					Stretch = Stretch.Fill,
					Stroke = new SolidColorBrush(Colors.Black),
					StrokeThickness = 10,
					VerticalAlignment = VerticalAlignment.Bottom,
				};
				Grid.SetColumnSpan(line, Height);
				Grid.SetRowSpan(line, h);
				this.Children.Add(line);
			}
		}
	}
}
