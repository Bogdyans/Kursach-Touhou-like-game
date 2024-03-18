using Kursach_demo1.Leaderboardfolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Kursach_demo1.Game_Interface.MenuMaster;

namespace Kursach_demo1.Game_Interface
{
    public class MenuMaster
    {
        MainMenu mm;

        public void Start(Canvas screen)
        {
            MainMenu mm = new MainMenu();
            mm.Set(screen);
            
        }

        public float ReturnDiff()
        {
            return mm.setted_diff;
        }
        
        
        
        public abstract class Menu
        {

            protected Canvas MenuCanvas = new Canvas();

            public abstract void Set(Canvas screen);
            public void UnSet()
            {   
                
                MenuCanvas.Visibility= Visibility.Hidden;
                MenuCanvas = null;
                
                
            }
            public Button SetButton(Canvas canvas, string Content, int width, int height, int x, int y)
            {
                Button bttn = new Button
                {
                    Content = Content,
                    Width = width,
                    Height = height
                };
                canvas.Children.Add(bttn);
                Canvas.SetLeft(bttn, x);
                Canvas.SetTop(bttn, y);
                return bttn;
            }

            public Label SetLabel(Canvas canvas, string Content, int FontSize, int x, int y)
            {
                Label lbl = new Label
                {
                    FontSize = FontSize,
                    Content = Content,
                };
                canvas.Children.Add(lbl);
                Canvas.SetLeft(lbl, x);
                Canvas.SetTop(lbl, y);
                return lbl;
            }
            public void SetMenuCanvas(Canvas screen)
            {
                MenuCanvas = new Canvas();
                MenuCanvas.Width = screen.Width;
                MenuCanvas.Height = screen.Height;
                screen.Children.Add(MenuCanvas);
            }
        }

        public class MainMenu : Menu
        {
            public float setted_diff;
            
            public override void Set(Canvas screen)
            {
                SetMenuCanvas(screen);
                

                SetLabel(MenuCanvas, "Touhou", 108, 243, 317);

                Button strtbttn = SetButton(MenuCanvas, "New Game", 418, 130, 874, 65);
                strtbttn.Click += (s, e) => { Hide();  SetDiff(screen); };

                Button ldrbrdbttn = SetButton(MenuCanvas, "LeaderBoard", 418, 130, 874, 225);
                ldrbrdbttn.Click += (s, e) => { Hide(); LeaderBoardMenu lm = new LeaderBoardMenu(); lm.Set(screen); };

                Button ctrlbttn = SetButton(MenuCanvas, "Controls", 418, 130, 874, 395);
                ctrlbttn.Click += (s, e) => { Hide(); ControlsMenu cm = new ControlsMenu(); cm.Set(screen); };

                Button extbttn = SetButton(MenuCanvas, "Exit", 418, 130, 874, 618);
                extbttn.Click += (s, e) => { UnSet(); };
            }

            public void SetDiff(Canvas screen)
            {
                

                Label golbl = SetLabel(MenuCanvas, "Choose Difficulty", 72, 100, 300);

                Button svscrbttn = SetButton(MenuCanvas, "Easy", 418, 130, 874, 200);
                svscrbttn.Click += (s, e) => { UnSet(); setted_diff = 1; };

                Button normbttn = SetButton(MenuCanvas, "Normal", 418, 130, 874, 350);
                normbttn.Click += (s, e) => { UnSet(); setted_diff = 1.5F; };

                Button hrdbttn = SetButton(MenuCanvas, "Hard", 418, 130, 874, 500);
                hrdbttn.Click += (s, e) => { UnSet(); setted_diff = 3; };
            }

            public void Hide()
            {
                MenuCanvas.Visibility = Visibility.Hidden;
            }
        }


        //public class DifficultyMenu : Menu
        //{
        //    float setted_diff;
        //    public override void Set(Canvas screen)
        //    {
        //        SetMenuCanvas(screen);

        //        Label golbl = SetLabel(MenuCanvas, "Choose Difficulty", 72, 100, 300);

        //        Button svscrbttn = SetButton(MenuCanvas, "Easy", 418, 130, 874, 200);
        //        svscrbttn.Click += (s, e) => { UnSet(); setted_diff = 1; };

        //        Button normbttn = SetButton(MenuCanvas, "Normal", 418, 130, 874, 350);
        //        normbttn.Click += (s, e) => { UnSet(); setted_diff = 1.5F; };

        //        Button hrdbttn = SetButton(MenuCanvas, "Hard", 418, 130, 874, 500);
        //        hrdbttn.Click += (s, e) => { UnSet(); setted_diff = 3; };
        //    }
        //}

        public class GameOverMenu : Menu
        {
            int score;
            ISaver leaderboard = new LeaderBoard(System.IO.Path.Combine(Environment.CurrentDirectory, "LeaderBords.txt"));
            public GameOverMenu(int score)
            {
                this.score = score;
            }

            public override void Set(Canvas screen)
            {
                SetMenuCanvas(screen);

                Label lbl = SetLabel(MenuCanvas, "Game Over", 108, 100, 300);

                Label scrlbl = SetLabel(MenuCanvas, $"Your score: {score}", 54, 850, 225);

                Button svscrbttn = SetButton(MenuCanvas, "Save Score", 418, 130, 874, 394);
                svscrbttn.Click += (s, e) => { UnSet(); Save(); MainMenu mm = new MainMenu(); mm.Set(screen); };

                Button mnmnbttn = SetButton(MenuCanvas, "To Main Menu", 418, 130, 874, 563);
                mnmnbttn.Click += (s, e) => { UnSet(); MainMenu mm = new MainMenu(); mm.Set(screen); };
            }

            public void Save()
            {
                leaderboard.Save($"{score}");
                    MessageBox.Show("Score saved");
                    
            }
        }
        public class LeaderBoardMenu : Menu
        {
            ILoader leaderBoard = new LeaderBoard(System.IO.Path.Combine(Environment.CurrentDirectory, "LeaderBords.txt"));
            public override void Set(Canvas screen)
            {
                SetMenuCanvas(screen);

                Label controls = SetLabel(MenuCanvas, "LeaderBoard", 32, 70, 15);

                List<string> scores = leaderBoard.Load();
                for (int i = 0; i < scores.Count; i++)
                {
                    Label score = SetLabel(MenuCanvas, scores[i], 32, 84, 60 + i * 40);
                }

                Button mnmnbttn = SetButton(MenuCanvas, "To Main Menu", 418, 130, 874, 463);
                mnmnbttn.Click += (s, e) => { UnSet(); MainMenu mm = new MainMenu(); mm.Set(screen); };
            
            }
        }

        public class ControlsMenu : Menu
        {
            public override void Set(Canvas screen)
            {
                
                SetMenuCanvas(screen);

                Label controls = SetLabel(MenuCanvas, "Controls", 32, 70, 15);

                Label arrows = SetLabel(MenuCanvas, "-Left/Right/Up/Down Arrow - Move Left/Right/Up/Down", 32, 84, 84);

                Label z = SetLabel(MenuCanvas, "-Z - Shoot", 32, 84, 148);

                Label x = SetLabel(MenuCanvas, "-X - Bomb", 32, 84, 212);

                Label shift = SetLabel(MenuCanvas, "-Shift - Slowdown (Lower player's speed)", 32, 84, 276);


                Button mnmnbttn = SetButton(MenuCanvas, "To Main Menu", 418, 130, 874, 463);
                mnmnbttn.Click += (s, e) => { UnSet(); MainMenu mm = new MainMenu(); mm.Set(screen); };
                
            }
        }

        public class WonMenu : Menu
        {
            int score;
            ISaver leaderboard = new LeaderBoard(System.IO.Path.Combine(Environment.CurrentDirectory, "LeaderBords.txt"));
            public WonMenu(int score)
            {
                this.score = score;
            }

            public override void Set(Canvas screen)
            {
                SetMenuCanvas(screen);

                Label golbl = SetLabel(MenuCanvas, "You Won", 108, 100, 300);

                Label scrlbl = SetLabel(MenuCanvas, $"Your score: {score}", 54, 850, 225);

                Button svscrbttn = SetButton(MenuCanvas, "Save Score", 418, 130, 874, 394);
                svscrbttn.Click += (s, e) => { UnSet(); Save(); MainMenu mm = new MainMenu(); mm.Set(screen); };

                Button mnmnbttn = SetButton(MenuCanvas, "To Main Menu", 418, 130, 874, 563);
                mnmnbttn.Click += (s, e) => { UnSet(); MainMenu mm = new MainMenu(); mm.Set(screen); };
            }
            public void Save()
            {
                leaderboard.Save($"{score}");
                MessageBox.Show("Score saved");

            }
        }
    }
}
