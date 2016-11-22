using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CH10.UserControls
{
	/// <summary>
	/// Interaction logic for ColorPicker.xaml
	/// </summary>
	public partial class ColorPicker : UserControl
	{
		public ColorPicker()
		{
			InitializeComponent();
		}
		
		// dp for selected color
		//   POCO backing field
		public Color SelectedColor
		{
			get { return (Color) GetValue(SelectedColorProperty); }
			set { SetValue(SelectedColorProperty, value); }
		}

		//   register it in WPF
		public static readonly DependencyProperty 
			SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color),
				typeof(ColorPicker), new UIPropertyMetadata(Colors.Black, OnSelectedColorChanged));

		// routed event for color selection changed
		//   register it in the WPF routed events system, 
		//   ascociate it with ColorPicker and keep a reference
		public static RoutedEvent SelectedColorChangedEvent =
			EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble,
			typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));

		//   define the CLR event and defer to the WPF subscription management
		//   todo understand this better
		public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
		{
			add { AddHandler(SelectedColorChangedEvent, value); }
			remove { RemoveHandler(SelectedColorChangedEvent, value); }
		}

		static void OnSelectedColorChanged(DependencyObject obj,
			DependencyPropertyChangedEventArgs e)
		{
			var cp = (ColorPicker) obj;
			cp.RaiseEvent(new RoutedPropertyChangedEventArgs<Color>( (Color)e.OldValue, 
				(Color)e.NewValue, SelectedColorChangedEvent));
		}

	}
}