using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Plugin.LocalNotifications;
using System.Threading;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Reminder
{
    public partial class App : Application
    {
        public static string FilePath;
        public bool flag = true;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
        public App(string filePath)
        {
            InitializeComponent();

            MainPage = new MainPage();
            FilePath = filePath;
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            flag = false;
           
        }

        public void execute()
        {
            var thread = new Thread(new ThreadStart(startProcess))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void startProcess()
        {




            using (SQLiteConnection conn = new SQLiteConnection(FilePath))

            {
                    while (flag)

                   {
                      conn.CreateTable<model>();
                      var data = conn.Table<model>().ToList();



                     if (data.Capacity > 0 && conn.ExecuteScalar<int>("SELECT SUM(flag) FROM model ") > 0)


                        foreach (model x in data)
                        {

                            if (x.flag == 1 && x.Time == DateTime.Now.ToString("HH:mm") && x.Date == DateTime.Today.Date.ToString("dd-MM-yyyy"))

                            {

                                CrossLocalNotifications.Current.Show(x.DateTime, x.Text);
                                conn.Execute("UPDATE model SET flag = 0 Where Id= ?", x.ID);

                            }


                        }

                    else
                        flag = false;

                    }

            }

        }

        protected override void OnSleep()
        {

            flag = true;
            execute();
            
        }

        protected override void OnResume()
        {

            // Handle when your app resumes
            flag = false;
          
        }
    }
}
