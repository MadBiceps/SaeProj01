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

        // Färbt den Text rot ein, wenn der Username zu kurz ist.
        private void textbox_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_username.Foreground = textbox_username.Text.Length < 5 ? Brushes.Red : Brushes.Black;
        }

        // Färbt den Text rot ein, wenn das Passwort zu kurz ist.
        private void textbox_passwort_PasswordChanged(object sender, RoutedEventArgs e)
        {
            textbox_passwort.Foreground = textbox_passwort.Password.Length < 5 ? Brushes.Red : Brushes.Black;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Überprüft die länge des Passworts
            if (textbox_username.Text.Length < 5 || textbox_passwort.Password.Length < 5)
            {
                MessageBox.Show("Ihr Username oder Passwort ist zu kurz.");
                return;
            }

            // Gucken ob der User sich anmelden will oder Registrierten. Dann jeweilige Aktion asuführen. 
            if (butn_Anmelden.Content.Equals("Anmelden"))
            {
                var db = new DbController();
                var currentUser = new User(textbox_username.Text, textbox_passwort.Password, false);
                MessageBox.Show(currentUser.checkUser(db.GetUser(textbox_username.Text))
                    ? "Sie sind eingeloggt."
                    : "Ihre Anmeldedaten sind leider falsch.");
                Console.WriteLine(currentUser.checkUser(db.GetUser(currentUser.UserName)));
            }
            else
            {
                var db = new DbController();
                var currentUser = new User(textbox_username.Text, textbox_passwort.Password, false);
                db.SetUser(currentUser);
                MessageBox.Show("Ihr Account wurde erstellt");
                butn_Anmelden.Content = "Anmelden";
                label_Registrieren.Content = "Registrieren?";
            }

            
        }


        // Anmelde Button und Label ändern von anmnelden zu registrierten oder anderst herum
        private void label_Registrieren_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (butn_Anmelden.Content.ToString() == "Anmelden")
            {
                butn_Anmelden.Content = "Registrieren";
                label_Registrieren.Content = "Anmelden?";
            }
            else
            {
                butn_Anmelden.Content = "Anmelden";
                label_Registrieren.Content = "Registrieren?";
            }
        }
    }
}