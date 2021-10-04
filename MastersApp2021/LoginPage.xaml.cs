using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MastersApp2021{
	public partial class LoginPage : Page{
		public LoginPage() => InitializeComponent();

		void BtnLogin_Click(object sender, RoutedEventArgs e){
			var currentUser = App.Context.User.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == PBoxPassword.Password);
			if(currentUser != null){
				App.CurrentUser = currentUser;
				NavigationService.Navigate(new ServicesPage());
			}
			else
				MessageBox.Show("Пользователь с такими данными не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}