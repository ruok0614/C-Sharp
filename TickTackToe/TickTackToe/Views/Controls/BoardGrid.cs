using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TickTackToe.Models;
using TickTacToe.ViewModels.support;
using TickTacToe.Views.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TickTacToe.Views.Controls
{
	public sealed partial class BoardGrid:Grid
	{

		public int CellWidth
		{
			get { return (int)GetValue(CellWidthProperty); }
			set { SetValue(CellWidthProperty, value); }
		}

		public int CellHeight
		{
			get { return (int)GetValue(CellHeightProperty); }
			set { SetValue(CellHeightProperty, value); }
		}

		public NotifyBoard Board
		{
			get { return (NotifyBoard) GetValue(BoardProperty); }
			set { SetValue(BoardProperty, value); }
		}

		public ICommand SetCommand
		{
			get { return (ICommand)GetValue(SetCommandProperty); }
			set { SetValue(SetCommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for SetCommand.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SetCommandProperty =
			DependencyProperty.Register(nameof(SetCommand), typeof(ICommand), typeof(BoardGrid), new PropertyMetadata(null,LengthChanged));


		// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BoardProperty =
			DependencyProperty.Register(nameof(Board), typeof(NotifyBoard), typeof(BoardGrid), new PropertyMetadata(null,BoardChanged));

		private static void BoardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if(!(d is BoardGrid grid))
			{
				return;
			}

		}

		// Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CellHeightProperty =
			DependencyProperty.Register(nameof(CellHeight), typeof(int), typeof(BoardGrid), new PropertyMetadata(0, LengthChanged));

		// Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CellWidthProperty =
			DependencyProperty.Register(nameof(CellWidth), typeof(int), typeof(BoardGrid), new PropertyMetadata(0, LengthChanged));
		
		
		private static void LengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if(!(d is BoardGrid grid))
			{
				return;
			}
			// グリッドをいったん初期化する。
			var columnCount = grid.ColumnDefinitions.Count;
			for(var c = 0; c < columnCount; c++)
			{ 
				grid.ColumnDefinitions.RemoveAt(0);
			}
			var rowCount = grid.ColumnDefinitions.Count;
			for(var r = 0; r < rowCount; r++)
			{
				grid.RowDefinitions.RemoveAt(0);
			}

			for(var w = 0; w < grid.CellWidth; w++)
			{
				var colDef = new ColumnDefinition
				{
					Width = new GridLength(1, GridUnitType.Star)
				};
				grid.ColumnDefinitions.Add(colDef);
			}
			for(var h = 0; h < grid.CellHeight; h++)
			{
				var rowDef = new RowDefinition
				{
					Height = new GridLength(1, GridUnitType.Star)
				};
				grid.RowDefinitions.Add(rowDef);
			}
			for(var w = 0; w < grid.CellWidth; w++)
			{
				for(var h = 0; h < grid.CellHeight; h++)
				{
					var button = new PieceButton()
					{
						Margin = new Thickness(0),
						HorizontalAlignment = HorizontalAlignment.Stretch,
						VerticalAlignment = VerticalAlignment.Stretch,
						X = w,
						Y = h
					};
					var command = new Binding()
					{
						Path = new PropertyPath(nameof(grid.SetCommand)),
						Source = grid
					};
					var piace = new Binding()
					{
						Path = new PropertyPath(nameof(grid.Board)),
						Source = grid
					};
					button.SetBinding(PieceButton.BoardProperty, piace);
					button.SetBinding(PieceButton.CommandProperty, command);
					Grid.SetColumn(button, w);
					Grid.SetRow(button, h);
					grid.Children.Add(button);
				}
			}

			// 黒い直線を描画
			for(var w = 1; w < grid.CellWidth; w++)
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
				Grid.SetRowSpan(line, grid.CellWidth);
				grid.Children.Add(line);
			}
			for(var h = 1; h < grid.CellHeight; h++)
			{
				var line = new Line()
				{
					X2 = 1,
					Stretch = Stretch.Fill,
					Stroke = new SolidColorBrush(Colors.Black),
					StrokeThickness = 10,
					VerticalAlignment = VerticalAlignment.Bottom,
				};
				Grid.SetColumnSpan(line, grid.CellHeight);
				Grid.SetRowSpan(line, h);
				grid.Children.Add(line);
			}
		}


		public BoardGrid()
		{

			//this.CellWidth = this.CellHeight = 5;
			//for(var w = 0; w < CellWidth; w++)
			//{
			//	var colDef = new ColumnDefinition
			//	{
			//		Width = new GridLength(1, GridUnitType.Star)
			//	};
			//	this.ColumnDefinitions.Add(colDef);
			//}
			//for(var h = 0; h < CellHeight; h++)
			//{
			//	var rowDef = new RowDefinition
			//	{
			//		Height = new GridLength(1, GridUnitType.Star)
			//	};
			//	this.RowDefinitions.Add(rowDef);
			//}
			//for(var w = 0; w < CellWidth; w++)
			//{
			//	for(var h = 0; h < CellHeight; h++)
			//	{
			//		var button = new PieceButton()
			//		{
			//			Margin = new Thickness(0),
			//			HorizontalAlignment = HorizontalAlignment.Stretch,
			//			VerticalAlignment = VerticalAlignment.Stretch,
			//			X = w,
			//			Y = h
			//		};
			//		Grid.SetColumn(button, w);
			//		Grid.SetRow(button, h);
			//		this.Children.Add(button);
			//	}
			//}
			//for(var w = 1; w < CellWidth; w++)
			//{
			//	var line = new Line()
			//	{
			//		Y2 = 1,
			//		Stretch = Stretch.Fill,
			//		Stroke = new SolidColorBrush(Colors.Black),
			//		StrokeThickness = 10,
			//		HorizontalAlignment = HorizontalAlignment.Right,
			//	};
			//	Grid.SetColumnSpan(line, w);
			//	Grid.SetRowSpan(line, CellWidth);
			//	this.Children.Add(line);
			//}
			//for(var h = 1; h < CellHeight; h++)
			//{
			//	var line = new Line()
			//	{
			//		X2 = 1,
			//		Stretch = Stretch.Fill,
			//		Stroke = new SolidColorBrush(Colors.Black),
			//		StrokeThickness = 10,
			//		VerticalAlignment = VerticalAlignment.Bottom,
			//	};
			//	Grid.SetColumnSpan(line, CellHeight);
			//	Grid.SetRowSpan(line, h);
			//	this.Children.Add(line);
			//}
		}
	}
}
