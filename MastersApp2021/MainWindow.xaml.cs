using System.Windows;

namespace MastersApp2021{
	public partial class MainWindow : Window{
		public MainWindow(){
			InitializeComponent();
			FrameMain.Navigate(new ServicesPage()); //NavigationService.Navigate(new AddEditPage());
		}

		void BtnBack_Click(object sender, RoutedEventArgs e){
			if(FrameMain.CanGoBack)
				FrameMain.GoBack(); //NavigationService.GoBack();
		}
	}
}