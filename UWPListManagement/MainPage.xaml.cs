using Newtonsoft.Json;
using System;
using System.IO;
using UWPListManagement.Dialogs;
using UWPListManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ListManagement.services;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPListManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\SaveData.json";
        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new MainViewModel(persistencePath);

        }

        private async void AddToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog();
            await dialog.ShowAsync();
        }

        private async void EditToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }
        private void DeleteToDoClick(object sender, RoutedEventArgs e)
        {
            ItemService.Current.Remove((DataContext as MainViewModel).SelectedItem);
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemService.Current.Save();
            var msg = new MessageDialog("All Saved");
            await msg.ShowAsync();
        }

        private void SearchBtn(object sender, RoutedEventArgs e)
        {
            ItemService.Current.Query = SearchQueryText.Text;
            ItemService.Current.Search();
        }

        private async void AddAppointment(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog();
            await dialog.ShowAsync();
        }

        private async void EditAppointment(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }

        private void DoneIt_Click(object sender, RoutedEventArgs e)
        {
            ItemService.Current.DoneIt((DataContext as MainViewModel).SelectedItem);
        }
    }
}
