using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kursach_demo1
{
    internal class GameStats
    {
        Canvas space;
        List<Ellipse> players_hp = new List<Ellipse>();
        List<Ellipse> players_bimbs = new List<Ellipse>();
        Label scorelbl, multiplierlbl, enemyhplbl;
        public GameStats(int php, int pbombs)
        {
            space = new Canvas
            {
                //Canvas.Right = 86.4,Canvas.Top = "91",Canvas.Left = "1149"
                Width = 343.2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 544.3,
                Visibility = Visibility.Hidden
            };
            Border border = new Border
            {
                Width = 343.2,
                Height = 544.3,
                BorderThickness = new Thickness(3),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };
            space.Children.Add(border);
        }
        public void Set(Canvas screen, Player player, int enemy_hp)
        {
            SetCanvas(screen);
            SetScore();
            SetPlayerHP(player.hp);
            SetBombs(player.bombs);
            SetMultiplier();
            SetEnemyHP(enemy_hp);
        }
        public void SetCanvas(Canvas screen)
        {
            screen.Children.Add(space);
            Canvas.SetTop(space, 86);
            Canvas.SetLeft(space, 1034.1);
            space.Visibility = Visibility.Visible;
        }
        public void SetScore()
        {
            scorelbl = new Label
            {
                Content = $"Score: {0}",
                FontSize = 20
            };
            space.Children.Add(scorelbl);
            Canvas.SetLeft(scorelbl, 14);
        }
        public void SetBombs(int player_bimbs)
        {
            Label Bombs = new Label();
            Bombs.Content = "Bombs: ";
            Bombs.FontSize = 20;
            space.Children.Add(Bombs);
            Canvas.SetTop(Bombs, 50);
            Canvas.SetLeft(Bombs, 14);
            int dx = 90;
            for (int i = 0; i < player_bimbs; i++)
            {
                Ellipse bombsel = new Ellipse
                {
                    Width = 18,
                    Height = 18,
                    Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255))
                };
                players_bimbs.Add(bombsel);
                space.Children.Add(bombsel);
                Canvas.SetLeft(bombsel, dx);
                Canvas.SetTop(bombsel, 60);
                dx += 22;
            }
        }
        public void SetPlayerHP(int player_hp)
        {
            Label HP = new Label();
            HP.Content = "HP: ";
            HP.FontSize = 20;
            space.Children.Add(HP);
            Canvas.SetTop(HP, 30);
            Canvas.SetLeft(HP, 14);
            int dx = 60;
            for (int i = 0; i < player_hp; i++)
            {
                Ellipse hpel = new Ellipse
                {
                    Width = 18,
                    Height = 18,
                    Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0))
                };
                players_hp.Add(hpel);
                space.Children.Add(hpel);
                Canvas.SetLeft(hpel, dx);
                Canvas.SetTop(hpel, 40);
                dx += 22;
            }
        }
        public void SetMultiplier()
        {
            multiplierlbl = new Label
            {
                Content = $"Multiplier: {1}",
                FontSize = 20
            };
            space.Children.Add(multiplierlbl);
            Canvas.SetLeft(multiplierlbl, 14);
            Canvas.SetTop(multiplierlbl, 70);
        }
        public void SetEnemyHP(int enemy_hp)
        {
            enemyhplbl = new Label
            {
                Content = $"Enemy's HP: {enemy_hp}",
                FontSize = 20
            };
            space.Children.Add(enemyhplbl);
            Canvas.SetLeft(enemyhplbl, 14);
            Canvas.SetTop(enemyhplbl, 95);
        }
        public void ChangeHP(int playerhp)
        {
            space.Children.RemoveAt(space.Children.IndexOf(players_hp[playerhp]));
            players_hp.RemoveAt(playerhp);
        }
        public void ChangeScore(int score)
        {
            scorelbl.Content = $"Score: {score}";
        }
        public void ChangeMultiplier(int multiplier)
        {
            multiplierlbl.Content = $"Multiplier: {multiplier}";
        }
        public void ChangeEnemyHP(int enemy_hp)
        {
            enemyhplbl.Content = $"Enemy's HP: {enemy_hp}";
        }
        public void ChangeBombs(int playerbombs)
        {
            if (playerbombs == 0) return;
            space.Children.RemoveAt(space.Children.IndexOf
                (players_bimbs[playerbombs-1]));
            players_bimbs.RemoveAt(playerbombs-1);
        }
    }
}
