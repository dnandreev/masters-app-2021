using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MastersApp2021{
	public partial class ServicesPage : Page{
		public ServicesPage(){
			InitializeComponent();
			BtnAddService.Visibility = App.CurrentUser.RoleId == 1 ? Visibility.Visible : Visibility.Collapsed;
			LViewServices.ItemsSource = App.Context.Service.ToList();
			ComboDiscount.SelectedIndex = 0;
			ComboSortBy.SelectedIndex = 0;
			UpdateServices();
		}

		void ComboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateServices();

		void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateServices();

		void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e) => UpdateServices();

		void UpdateServices(){
			var services = App.Context.Service.ToList();
			int total = services.Count;
			if(ComboSortBy.SelectedIndex == 0)
				services = services.OrderBy(p => p.CostWithDiscount).ToList();
			else
				services = services.OrderByDescending(p => p.CostWithDiscount).ToList();
			switch (ComboDiscount.SelectedIndex){
				case 1: services = services.Where(p => p.Discount >= 0 && p.Discount < 0.05).ToList(); break;
				case 2: services = services.Where(p => p.Discount >= 0.05 && p.Discount < 0.15).ToList(); break;
				case 3: services = services.Where(p => p.Discount >= 0.15 && p.Discount < 0.30).ToList(); break;
				case 4: services = services.Where(p => p.Discount >= 0.30 && p.Discount < 0.70).ToList(); break;
				case 5: services = services.Where(p => p.Discount >= 0.70 && p.Discount <= 1).ToList(); break;
			}
			services = services.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
			LViewServices.ItemsSource = services;
			BlockRecords.Text = $"{services.Count} из {total}";
		}

		void BtnAddService_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new AddEditServicePage());

		void BtnEdit_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new AddEditServicePage((sender as Button).DataContext as Entities.Service));

		void BtnDelete_Click(object sender, RoutedEventArgs e){
			var currentService = (sender as Button).DataContext as Entities.Service;
			if(MessageBox.Show($"Вы уверены, что хотите удалить услугу: {currentService.Title}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes){
				App.Context.Service.Remove(currentService);
				App.Context.SaveChanges();
				UpdateServices();
			}
		}

		void Page_Loaded(object sender, RoutedEventArgs e) => UpdateServices();
	}
}