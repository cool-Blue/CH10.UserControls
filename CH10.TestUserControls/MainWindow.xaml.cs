using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CH10.TestUserControls
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow ()
		{
			InitializeComponent();
		}

		private void OnColorChanged(object sender,
			RoutedPropertyChangedEventArgs<Color> e)
		{
			if (_tbcolor != null)
				_tbcolor.Text = string.Format("Selected Color {0}", e.NewValue);
		}
	}

}
