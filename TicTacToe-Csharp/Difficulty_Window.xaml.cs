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
        public Difficulty_Window()
        {
            InitializeComponent();
        }
        // TODO - Connect window to main window. 
        // TODO - get buttons and connect to method.

        private List<Button> GetButtons()
        {
            Grid myGrid = (Grid)Content;
            List<Button> btn_List = myGrid.Children.Cast<Button>().Where(x => x.GetType() == typeof(Button)).ToList();

            return btn_List;
        }
    }
}
