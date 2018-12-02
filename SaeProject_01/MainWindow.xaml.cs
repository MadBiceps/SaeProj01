using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SaeProject_01.Controller;
using SaeProject_01.Models;

namespace SaeProject_01
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textbox_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textbox_username.Text.Length < 5)
            {
                textbox_username.Foreground = Brushes.Red;
            }
            else
            {
                textbox_username.Foreground = Brushes.Black;
            }
        }

        private void textbox_passwort_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(textbox_passwort.Password.Length < 5)
            {
                textbox_passwort.Foreground = Brushes.Red;
            }
            else
            {
                textbox_passwort.Foreground = Brushes.Black;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var db = new DbController();
            var currentUser = new User(textbox_username.Text, textbox_passwort.Password, false);
            Console.WriteLine(currentUser.checkUser(db.GetUser(currentUser.UserName)));
        }

        private void label_Registrieren_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(butn_Anmelden.Content.ToString() == "Anmelden")
            {
                butn_Anmelden
            }
        }
    }
}
