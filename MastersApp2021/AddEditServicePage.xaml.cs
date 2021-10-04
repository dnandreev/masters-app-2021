using System.IO;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace MastersApp2021{
	public partial class AddEditServicePage : Page{
		Entities.Service currentService = null;
		byte[] mainImageData;

		public AddEditServicePage() => InitializeComponent();

		public AddEditServicePage(Entities.Service service){
			InitializeComponent();
			currentService = service;
			Title = "Редактировать услугу";
			TBoxTitle.Text = currentService.Title;
			TBoxCost.Text = currentService.Cost.ToString();
			TBoxDuration.Text = (currentService.DurationInSeconds / 60).ToString();
			TBoxDescription.Text = currentService.Description;
			if(currentService.Discount > 0)
				TBoxDiscount.Text = (currentService.Discount.Value * 100).ToString();
			if(currentService.MainImage != null)
				ImageService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(currentService.MainImage);
		}

		string CheckErrors(){
			var errorBuilder = new StringBuilder();
			if(string.IsNullOrWhiteSpace(TBoxTitle.Text))
				errorBuilder.AppendLine("Название услуги обязательно для заполнения;");
			var serviceFromDB = App.Context.Service.ToList().FirstOrDefault(p => p.Title.ToLower() == TBoxTitle.Text.ToLower());
			if(serviceFromDB != null && serviceFromDB != currentService)
				errorBuilder.AppendLine("Такая услуга уже есть в базе данных;");
			decimal cost = 0;
			if(decimal.TryParse(TBoxCost.Text, out cost) == false || cost <= 0)
				errorBuilder.AppendLine("Стоимость услуги должна быть положительным числом;");
			int durationInMinutes = 0;
			if(int.TryParse(TBoxDuration.Text, out durationInMinutes) == false || durationInMinutes > 240 || durationInMinutes <= 0)
				errorBuilder.AppendLine("Длительность оказания услуги должна быть положительным числом (не больше, чем 4 часа);");
			if(!string.IsNullOrEmpty(TBoxDiscount.Text)){
				int discount = 0;
				if(int.TryParse(TBoxDiscount.Text, out discount) == false || discount < 0 || discount > 100)
					errorBuilder.AppendLine("Размер скидки - целое число в диапазоне от 0 до 100%;");
			}
			if(errorBuilder.Length > 0)
				errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
			return errorBuilder.ToString();
		}

		void BtnSelectImage_Click(object sender, System.Windows.RoutedEventArgs e){
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
			if(ofd.ShowDialog() == true){
				mainImageData = File.ReadAllBytes(ofd.FileName);
				ImageService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(mainImageData);
			}
		}

		void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e){
			string errorMessage = CheckErrors();
			if(errorMessage.Length > 0)
				MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			else{
				if(currentService == null){
					App.Context.Service.Add(new Entities.Service{
						Title = TBoxTitle.Text,
						Cost = decimal.Parse(TBoxCost.Text),
						DurationInSeconds = int.Parse(TBoxDuration.Text) * 60,
						Description = TBoxDescription.Text,
						Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text) ? 0 : double.Parse(TBoxDiscount.Text) / 100,
						MainImage = mainImageData
					});
					App.Context.SaveChanges();
					MessageBox.Show("Услуга успешно добавлена.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				else{
					currentService.Title = TBoxTitle.Text;
					currentService.Cost = decimal.Parse(TBoxCost.Text);
					currentService.DurationInSeconds = int.Parse(TBoxDuration.Text) * 60;
					currentService.Description = TBoxDescription.Text;
					currentService.Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text) ? 0 : double.Parse(TBoxDiscount.Text) / 100;
					if(mainImageData != null)
						currentService.MainImage = mainImageData;
					App.Context.SaveChanges();
					MessageBox.Show("Услуга успешно отредактирована.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
			NavigationService.GoBack();
		}
	}
}