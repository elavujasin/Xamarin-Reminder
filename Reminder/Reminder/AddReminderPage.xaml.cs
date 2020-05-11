using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reminder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddReminderPage : ContentPage
	{
       

        public AddReminderPage ()
		{
			InitializeComponent ();
		}

        public void Back_clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        public void Submit_clicked(object sender, EventArgs e)
        {
        
                model Model = new model()
                {
                    Date = date.Date.ToString("dd-MM-yyyy"),
                    Time = time.Time.ToString().Substring(0, 5),
                    Text = Remindertext.Text,
                };
            
          

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<model>();
                int rowsadd = conn.Insert(Model);
                Application.Current.MainPage = new NavigationPage(new MainPage());


            }
        }

    }
}
