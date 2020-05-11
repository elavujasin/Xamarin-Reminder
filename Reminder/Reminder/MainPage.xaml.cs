using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Linq;


namespace Reminder
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {


            InitializeComponent();



        }


        void AddButton_clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddReminderPage());
           
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<model>();
                var data = conn.Table<model>().ToList();
                Datalistview.ItemsSource = data;

            }
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            model dataItem = e.Item as model;
            bool answer = await DisplayAlert("Delete alarm", "Are you sure you want to delete this alarm", "Yes", "No");

            if (answer)
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {

                    conn.Delete<model>(dataItem.ID);
                    var data = conn.Table<model>().ToList();
                    Datalistview.ItemsSource = data;


                }

        }


    }
}
