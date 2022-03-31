using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ListManagement.models;
using ListManagement.services;
using System.Collections.ObjectModel;
// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class AppointmentDialog : ContentDialog
    {
        private ObservableCollection<Item> _appointmentCollection;
        public AppointmentDialog()
        {
            this.InitializeComponent();
            _appointmentCollection = ItemService.Current.Items;

            DataContext = new Appointment();
        }
        public AppointmentDialog(Item item)
        {
            this.InitializeComponent();
            _appointmentCollection = ItemService.Current.Items;
            DataContext = item;
            var ls = new ListBox();
            foreach (var Attendee in AttendeesList.Items)
            {
                ls.Items.Add(Attendee.ToString());
            }
            AttendeesList.ItemsSource = ls.Items;
        }
      
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = DataContext as Appointment;
            foreach (var Attendee in AttendeesList.Items)
            {
                item.Attendees.Add(Attendee.ToString());
            }
            if (_appointmentCollection.Any(i => i.Id == item.Id))
            {
                var itemToUpdate = _appointmentCollection.FirstOrDefault(i => i.Id == item.Id);
                var index = _appointmentCollection.IndexOf(itemToUpdate);
                _appointmentCollection.RemoveAt(index);
                _appointmentCollection.Insert(index, item);
            }
            else
            {
                ItemService.Current.Add(DataContext as Appointment);
            }

        }


        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void AddAttendee_Click(object sender, RoutedEventArgs e)
        {
            var ls = new ListBox();
            foreach (var item in AttendeesList.Items)
            {
                ls.Items.Add(item.ToString());
            }
            ls.Items.Add(NewAttendeeTitle.Text);
            AttendeesList.ItemsSource = ls.Items;
            NewAttendeeTitle.Text = "";
        }
    }
}
