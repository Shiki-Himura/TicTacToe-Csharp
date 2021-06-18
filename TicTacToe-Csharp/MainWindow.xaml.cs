using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TicTacToe_Csharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int difficulty;
        private readonly int[][] field = new int[][]
            {
                new int[]{0, 0, 0 },
                new int[]{0, 0, 0 },
                new int[]{0, 0, 0 }
            };

        public MainWindow()
        {
            // TODO - Check functionality
            InitializeComponent();
        }

        public void SetDifficulty(int diff)
        {
            difficulty = diff;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;            

            bool player_one = true;
            string button = btn.Tag.ToString();
            int x = int.Parse(button[0].ToString());
            int y = int.Parse(button[1].ToString());
            btn.Content = "X";
            btn.IsHitTestVisible = false;
            field[x][y] = 1;

            BestMove(field);
            Render(field);
            Validate(field);
            WinnerAlert(field);

        }

        private void BestMove(int[][] tmp_field)
        {
            int rnd = GetRandomNumber(0, 100);
            int bestScore = int.MaxValue;
            (int i, int j) move = (i: 0, j: 0);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tmp_field[i][j] == 0)
                    {
                        tmp_field[i][j] = 2;
                        int score = MiniMax(tmp_field, true);
                        if (score < bestScore)
                        {
                            bestScore = score;
                            move = (i, j);
                        }
                        tmp_field[i][j] = 0;
                    }
                }
            }

            if (rnd < 20) //difficulty)
            {
                Tuple<int, int> rndMove = GetRandomIndex(tmp_field);
                tmp_field[rndMove.Item1][rndMove.Item2] = 2;
            }
            else
            {
                tmp_field[move.i][move.j] = 2;
            }
        }

        private int MiniMax(int[][] tmp_field, bool player_one)
        {
            int bestScore;
            int game_over = Validate(tmp_field);

            if (game_over != 2)
            {
                if (player_one && game_over != 0)
                {
                    return -10;
                }
                else if (game_over != 0)
                {
                    return 10;
                }
                else
                {
                    return 0;
                }
            }

            if (player_one)
            {
                bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tmp_field[i][j] == 0)
                        {
                            tmp_field[i][j] = 1;
                            int score = MiniMax(tmp_field, false);

                            if (score > bestScore)
                            {
                                bestScore = score;
                            }
                            tmp_field[i][j] = 0;
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tmp_field[i][j] == 0)
                        {
                            tmp_field[i][j] = 2;
                            int score = MiniMax(tmp_field, true);

                            if (score < bestScore)
                            {
                                bestScore = score;
                            }
                            tmp_field[i][j] = 0;
                        }
                    }
                }

                return bestScore;
            }
        }

        private static List<Tuple<int, int>> GetPossibleMoves(int[][] tmp_field)
        {
            List<Tuple<int, int>> possible = new();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tmp_field[i][j] == 0)
                    {
                        possible.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return possible;
        }

        private static int GetRandomNumber(int min, int max)
        {
            Random rng = new();
            return rng.Next(min, max);
        }

        private void Render(int[][] tmp_field)
        {
            List<Button> btn = GetButtons();

            int[] newArr = tmp_field.SelectMany(x => x).ToArray();

            for (int i = 0; i < newArr.Length; i++)
            {
                if (newArr[i] != 0 && newArr[i] == 2)
                {
                    btn[i].Content = "O";
                    btn[i].IsHitTestVisible = false;
                }
            }
        }

        private static int Validate(int[][] tmp_field)
        {
            for (int i = 0; i < 3; i++)
            {
                if (tmp_field[i][0] != 0 && Equals3(tmp_field[i][0], tmp_field[i][1], tmp_field[i][2])
                || tmp_field[0][i] != 0 && Equals3(tmp_field[0][i], tmp_field[1][i], tmp_field[2][i]))
                {
                    return 1;
                }
            }

            if (tmp_field[0][0] != 0 && Equals3(tmp_field[0][0], tmp_field[1][1], tmp_field[2][2])
            || tmp_field[0][2] != 0 && Equals3(tmp_field[0][2], tmp_field[1][1], tmp_field[2][0]))
            {
                return 1;
            }

            bool empty_field = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tmp_field[i][j] == 0)
                    {
                        empty_field = true;
                    }
                }
            }

            if (empty_field == false)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }

        private static bool Equals3(int one, int two, int three)
        {
            return one == two && one == three;
        }

        private void WinnerAlert(int[][] field)
        {
            List<Button> btn = GetButtons();

            if (Validate(field) == 1)
            {
                foreach (Button button in btn)
                {
                    IsHitTestVisible = false;
                }
                MessageBox.Show("You´ve Won!");
            }
        }

        private static Tuple<int, int> GetRandomIndex(int[][] tmp_field)
        {
            List<Tuple<int, int>> array = GetPossibleMoves(tmp_field);
            int min = 0;
            int max = array.Count - 1;

            return array[GetRandomNumber(min, max)];
        }

        private List<Button> GetButtons()
        {
            Grid myGrid = (Grid)Content;
            List<Button> btn_List = myGrid.Children.Cast<Button>().Where(x => x.GetType() == typeof(Button)).ToList();

            return btn_List;
        }
    }
}