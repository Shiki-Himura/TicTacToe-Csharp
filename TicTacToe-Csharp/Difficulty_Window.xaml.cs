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
using System.Windows.Shapes;

namespace TicTacToe_Csharp
{
    /// <summary>
    /// Interaktionslogik für Difficulty_Window.xaml
    /// </summary>
    public partial class Difficulty_Window : Window
    {
        private int difficulty;
        public Difficulty_Window()
        {
            InitializeComponent();
        }

        private void Button_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;

            if (rbtn.IsChecked == true && rbtn.Name == "Easy")
            {
                difficulty = 80;
            }
            
            if (rbtn.IsChecked == true && rbtn.Name == "Hard")
            {
                difficulty = 20;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow game = new();

            if (difficulty is 80 or 20)
            {
                game.SetDifficulty(difficulty);
                game.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Please Select a Difficulty");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            Application.Current.Shutdown();
        }
    }
}
