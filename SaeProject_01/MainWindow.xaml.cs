using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using SaeProject_01.Module;

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
            progressbar_passwort.Visibility = Visibility.Hidden;
            label_progress.Visibility = Visibility.Hidden;
            butn_Anmelden.IsEnabled = checkAnmeldeButton();
            progressbar_passwort.Foreground = Brushes.Red;
            img_PwInfo.Visibility = Visibility.Hidden;
        }

        // Ruft eine Funktion auf die überprüft ob die eingabe den regeln entspricht und schaltet dann den button frei
        private void textbox_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            butn_Anmelden.IsEnabled = butn_Anmelden.Content.Equals("Anmelden") ? checkAnmeldeButton() : checkRegisterButton();
        }

        // Ruft eine Funktion auf die überprüft ob die eingabe den regeln entspricht und schaltet dann den button frei / Ruft die Passwort scoring funktion auf und zeig es in progressbar an
        private void textbox_passwort_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwStreng = PasswortScoring.CheckStrength(textbox_passwort.Password);
            butn_Anmelden.IsEnabled = butn_Anmelden.Content.Equals("Anmelden") ? checkAnmeldeButton() : checkRegisterButton();
            progressbar_passwort.Value = pwStreng;
            if (pwStreng > 2)
            {
                progressbar_passwort.Foreground = Brushes.Red;
            }

            if (pwStreng < 4 && pwStreng >= 2)
            {
                progressbar_passwort.Foreground = Brushes.Yellow;
            }

            if (pwStreng >= 4)
            {
                progressbar_passwort.Foreground = Brushes.Green;
            }
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
                // Checkt ob es den User schon gibt
                if (!db.SetUser(currentUser))
                {
                    MessageBox.Show("Der Username wird schon verwendet");
                }
                else
                {
                    MessageBox.Show("Ihr Account wurde erstellt");
                    butn_Anmelden.Content = "Anmelden";
                    label_Registrieren.Content = "Registrieren?";
                }
            }

            
        }


        // Anmelde Button und Label ändern von anmnelden zu registrierten oder anderst herum
        private void label_Registrieren_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (butn_Anmelden.Content.ToString() == "Anmelden")
            {
                butn_Anmelden.Content = "Registrieren";
                label_Registrieren.Content = "Anmelden?";
                progressbar_passwort.Visibility = Visibility.Visible;
                label_progress.Visibility = Visibility.Visible;
                img_PwInfo.Visibility = Visibility.Visible;
            }
            else
            {
                butn_Anmelden.Content = "Anmelden";
                label_Registrieren.Content = "Registrieren?";
                progressbar_passwort.Visibility = Visibility.Hidden;
                label_progress.Visibility = Visibility.Hidden;
                img_PwInfo.Visibility = Visibility.Hidden;
            }
        }

        // Ändert die Farben der eingaben um den User zu zeigen ab wann es richtrig ist 
        private bool checkAnmeldeButton()
        {
            textbox_username.BorderBrush = textbox_username.Text.Length < 5 ? Brushes.Red : Brushes.Black;
            textbox_username.Foreground = textbox_username.Text.Length < 5 ? Brushes.Red : Brushes.Black;
            textbox_passwort.Foreground = textbox_passwort.Password.Length < 4 ? Brushes.Red : Brushes.Black;
            textbox_passwort.BorderBrush = textbox_passwort.Password.Length < 4 ? Brushes.Red : Brushes.Black;
            return (textbox_username.Text.Length >= 5) && (textbox_passwort.Password.Length >= 5);
        }

        // Überprüft, ob die eingegeben Daten passen und ob der anmeldebutton freigeschaltet wird
        private bool checkRegisterButton()
        {
            if (PasswortScoring.CheckStrength(textbox_passwort.Password) < 2)
            {
                textbox_passwort.Foreground = Brushes.Red;
                textbox_passwort.BorderBrush = Brushes.Red;
            }
            else
            {
                textbox_passwort.Foreground = Brushes.Black;
                textbox_passwort.BorderBrush = Brushes.Black;
            }

            textbox_username.BorderBrush = textbox_username.Text.Length < 5 ? Brushes.Red : Brushes.Black;
            textbox_username.Foreground = textbox_username.Text.Length < 5 ? Brushes.Red : Brushes.Black;

            return (textbox_username.Text.Length >= 5) &&
                   (PasswortScoring.CheckStrength(textbox_passwort.Password) < 2);
        }

        // Infobox für das Registrierten
        private void img_PwInfo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Sie müssen ein Passwort angeben, was so gut ist, das der Balken grün wird");
        }
    }
}