using Kursach_demo1.Game_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace Kursach_demo1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        Game game;
        bool started = false;
        DispatcherTimer timer;
        MenuMaster menuMaster = new MenuMaster();

        Canvas GameCanvas;
        Canvas SomeCanvas;
        
        public LeaderBoard leaderboard = new LeaderBoard(System.IO.Path.Combine(Environment.CurrentDirectory, "LeaderBords.txt"));
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            //menuMaster.Start(screen);
            timer.Tick += new EventHandler(TimerEvent);
            timer.Interval = TimeSpan.FromMilliseconds(20);
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            game.GameStep();
            if (game.Ended)
            {
                timer.Stop();
                HideGameCanvas();

                if (game.Won)
                {
                    SetWonMenu();
                    return;
                }
                SetGameOverCanvas();
            }
        }

        private void Start()
        {
            started = true;
            HideMainMenu();
            SetGameCanvas();
            timer.Start();
            game.Start(GameCanvas);
        }

        public void HideMainMenu()
        {
            MainMenu.Visibility = Visibility.Hidden;
        }

        public void ShowMainMenu()
        {
            MainMenu.Visibility = Visibility.Visible;
        }

        public void SetGameCanvas()
        {
            GameCanvas = new Canvas();
            GameCanvas.Width = screen.Width;
            GameCanvas.Height = screen.Height;
            screen.Children.Add(GameCanvas);

        }

        public void HideGameCanvas()
        {
            GameCanvas.Visibility = Visibility.Hidden;
            GameCanvas = null;
        }

        public void SetDiffCanvas(object sender, RoutedEventArgs e)
        {
            SetMenuCanvas();
            HideMainMenu();

            Label golbl = SetLabel(SomeCanvas, "Choose Difficulty", 72, 100, 300);

            Button svscrbttn = SetButton(SomeCanvas, "Easy", 418, 130, 874, 200);
            svscrbttn.Click += SetEasy;

            Button normbttn = SetButton(SomeCanvas, "Normal", 418, 130, 874, 350);
            normbttn.Click += SetNormal;

            Button hrdbttn = SetButton(SomeCanvas, "Hard", 418, 130, 874, 500);
            hrdbttn.Click += SetHard;
        }

        public void HideMenu()
        {
            SomeCanvas.Visibility = Visibility.Hidden;
            SomeCanvas = null;
        }

        private void SetHard(object sender, RoutedEventArgs e)
        {
            game = new Game(3);
            HideMenu();
            Start();
        }

        private void SetEasy(object sender, RoutedEventArgs e)
        {
            game = new Game(1);
            HideMenu();
            Start();
        }

        private void SetNormal(object sender, RoutedEventArgs e)
        {
            game = new Game(1.5F);
            HideMenu();
            Start();
        }

        public void SetGameOverCanvas()
        {
            SetMenuCanvas();

            Label lbl = SetLabel(SomeCanvas, "Game Over", 108, 100, 300);

            Label scrlbl = SetLabel(SomeCanvas, $"Your score: {game.score}", 54, 850, 225);

            Button svscrbttn = SetButton(SomeCanvas, "Save Score", 418, 130, 874, 394);
            svscrbttn.Click += SaveScore;
            svscrbttn.Click += (s, e) => HideMenu();

            Button mnmnbttn = SetButton(SomeCanvas, "To Main Menu", 418, 130, 874, 563);
            mnmnbttn.Click += (s, e) => ShowMainMenu();
            mnmnbttn.Click += (s, e) => HideMenu();

        }

        public void SetControlsCanvas(object sender, EventArgs es)
        {
            HideMainMenu();
            SetMenuCanvas();

            Label controls = SetLabel(SomeCanvas, "Controls", 32, 70, 15);

            Label arrows = SetLabel(SomeCanvas, "-Left/Right/Up/Down Arrow - Move Left/Right/Up/Down", 32, 84, 84);

            Label z = SetLabel(SomeCanvas, "-Z - Shoot", 32, 84, 148);

            Label x = SetLabel(SomeCanvas, "-X - Bomb", 32, 84, 212);

            Label shift = SetLabel(SomeCanvas, "-Shift - Slowdown (Lower player's speed)", 32, 84, 276);


            Button mnmnbttn = SetButton(SomeCanvas, "To Main Menu", 418, 130, 874, 463);
            mnmnbttn.Click += (s, e) => ShowMainMenu();
            mnmnbttn.Click += (s, e) => HideMenu();
        }

        public void Set_LeaderBoards(object sender, RoutedEventArgs es)
        {
            SetMenuCanvas();
            HideMainMenu();

            Label controls = SetLabel(SomeCanvas, "LeaderBoard", 32, 70, 15);


            List<string> scores = leaderboard.Load();
            for (int i = 0; i < scores.Count; i++)
            {
                Label score = SetLabel(SomeCanvas, scores[i], 32, 84, 60 + i * 40);
            }

            Button mnmnbttn = SetButton(SomeCanvas, "To Main Menu", 418, 130, 874, 463);
            mnmnbttn.Click += (s, e) => ShowMainMenu();
            mnmnbttn.Click += (s, e) => HideMenu();
        }


        private void SetWonMenu()
        {
            SetMenuCanvas();

            Label golbl = SetLabel(SomeCanvas, "You Won", 108, 100, 300);

            Label scrlbl = SetLabel(SomeCanvas, $"Your score: {game.score}", 54, 850, 225);

            Button svscrbttn = SetButton(SomeCanvas, "Save Score", 418, 130, 874, 394);
            svscrbttn.Click += SaveScore;
            svscrbttn.Click += (s, e) => HideMenu();

            Button mnmnbttn = SetButton(SomeCanvas, "To Main Menu", 418, 130, 874, 563);
            mnmnbttn.Click += (s, e) => ShowMainMenu();
            mnmnbttn.Click += (s, e) => HideMenu();
        }


        ////////////////////////////////////////////////////////////////////////////
        /////// Controls
        ////////////////////////////////////////////////////////////////////////////
        private void CanvasKUE(object sender, KeyEventArgs e)
        {
            //switch (e.Key)
            //{
            //    case (Key.LeftShift): game.GameKeyUp("SpeedUp"); break;
            //    case (Key.Z): game.GameKeyUp("StopShoot"); break;
            //    case (Key.Up): game.GameKeyUp("Up"); break;
            //    case (Key.Down): game.GameKeyUp("Down"); break;
            //    case (Key.Left): game.GameKeyUp("Left"); break;
            //    case (Key.Right): game.GameKeyUp("Right"); break;
            //}
            if (started)
                game.GameKeyUp(e);
        }
        private void CanvasKDE(object sender, KeyEventArgs e)
        {
            //switch (e.Key)
            //{
            //    case (Key.LeftShift): game.GameKeyDown("SlowDown"); break;
            //    case (Key.Z): game.GameKeyDown("Shoot"); break;
            //    case (Key.Up): game.GameKeyDown("Up"); break;
            //    case (Key.Down): game.GameKeyDown("Down"); break;
            //    case (Key.Left): game.GameKeyDown("Left"); break;
            //    case (Key.Right): game.GameKeyDown("Right"); break;
            //    case (Key.Escape):
            //        game.Pause();
            //        if (game.Paused())
            //            timer.Stop();
            //        else
            //            timer.Start();
            //        break;
            //    case (Key.X): game.GameKeyDown("Bomb"); break;
            //}
            if (started)
                game.GameKeyDown(e, timer);
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
        public void SetMenuCanvas()
        {
            SomeCanvas = new Canvas();
            SomeCanvas.Width = screen.Width;
            SomeCanvas.Height = screen.Height;
            screen.Children.Add(SomeCanvas);
        }

        //////////////////////////////////////////////////////////////////////////
        ///// Windows actions
        //////////////////////////////////////////////////////////////////////////


        private void MainWind_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (started)
                game.StopMusic();
        }
        private void SaveScore(object sender, RoutedEventArgs e)
        {
            leaderboard.Save($"{game.score}");
            MessageBox.Show("Score saved");
            ShowMainMenu();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
