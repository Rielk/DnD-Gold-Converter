using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DnD_Gold_Converter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private static readonly Regex _regex = new Regex("[^0-9]+");
		private static bool TextContainsNonNumbers(string text)
		{
			return _regex.IsMatch(text);
		}

		private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = TextContainsNonNumbers(e.Text);
		}

		private void NumberBox_PastingEvent(object sender, DataObjectPastingEventArgs e)
		{
			string text = (string)e.DataObject.GetData(typeof(string));
			if (text != null)
			{
				if (TextContainsNonNumbers(text))
				{
					e.CancelCommand();
				}
			}
			else
			{
				e.CancelCommand();
			}
		}
	}
}
