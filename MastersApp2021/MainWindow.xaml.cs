using System.Windows;

namespace MastersApp2021{
	public partial class MainWindow : Window{
		public MainWindow(){
			InitializeComponent();
			FrameMain.Navigate(new LoginPage());
		}

		void BtnBack_Click(object sender, RoutedEventArgs e){
			if(FrameMain.CanGoBack)
				FrameMain.GoBack();
		}
	}
}