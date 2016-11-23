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
			SelectedColor = Colors.OrangeRed;
			InitializeComponent();
			this.uc.DataContext = this;		// set the run time context
		}
		
		
		// DEPENDENCY PROPERTIES

		// dp for selected color
		//   POCO backing field
		public Color SelectedColor
		{
			get { return (Color) GetValue(SelectedColorProperty); }
			set { SetValue(SelectedColorProperty, value); }
		}

		//   register it in WPF and store the dp identifier on a static field
		//   include property metadata defaultValue value and PropertyChangedCallback
		public static readonly DependencyProperty 
			SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color),
				typeof(ColorPicker), new UIPropertyMetadata(Colors.OrangeRed, OnSelectedColorChanged));

		
		// ROUTED EVENTS

		// routed event for color selection changed that will be called by the DP system
		//   register it in the WPF routed events system
		//   conform to the WPF standard event type for property changed 
		//   ascociate it with ColorPicker and keep a backing reference
		private static readonly RoutedEvent SelectedColorChangedEvent =
			EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble,
			typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));

		//   exposed a CLR, event to manage incoming subscriptions 
		//    - pass subscribers to the WPF Routed Events system
		//      callbacks will be managed by the WPF event system
		public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
		{
			add { AddHandler(SelectedColorChangedEvent, value); }
			remove { RemoveHandler(SelectedColorChangedEvent, value); }
		}
		// Callback for the DP system property changed event
		// Receive the property event and emit a routed event
		static void OnSelectedColorChanged(DependencyObject obj,
			DependencyPropertyChangedEventArgs e)
		{
			var cp = (ColorPicker) obj;
			cp.RaiseEvent(new RoutedPropertyChangedEventArgs<Color>( (Color)e.OldValue, 
				(Color)e.NewValue, SelectedColorChangedEvent));
		}

	}
}