using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Kursach_demo1
{
    public class Game
    {
        //music
        private MediaPlayer music = new MediaPlayer();
        private MediaPlayer DeathSoundEffect = new MediaPlayer();
        private MediaPlayer unPauseSFX = new MediaPlayer(); //soundplay

        //objects
        bool pause = false;
        List<Bullet> bullets = new List<Bullet>();

        Player player;
        Enemy enemy;

        public int score = 0;
        int score_per_enemy_hit = 0, multiplier = 1;
        int stage_number = 0;

        

        //interface
        Field field = new Field();
        GameStats stats;

        public bool Ended = false, Won = false;

        public Game(float df)
        {
            player = new Player(Color.FromRgb(122, 122, 255), 7, df);
            enemy = new Enemy(Color.FromArgb(255, 255, 1, 1), 50, (int)(df + 0.5F));
            stats = new GameStats((int)(9 / df), player.bombs);
            score_per_enemy_hit = (int)(100 * df);

        }



        ////////////////////////////////////////////////////////////////////////
        /// For Start of the battle/////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Start(Canvas screen)
        {
            field.Set(screen);
            SpawnEntities();
            stats.Set(screen, player, enemy.hp);
            StartMusic();
            ChangeStage();
        }
        public void SpawnEntities()
        {
            player.Spawn((int)(field.GetWidth() / 2),
                (int)(field.GetHeight() / 4 * 3), GameField());
            enemy.Spawn((int)(field.GetWidth() / 2),
                (int)(field.GetHeight() / 4), GameField());
             //change
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////
        /// Controls/////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void GameKeyDown(KeyEventArgs e, DispatcherTimer timer)
        {
            switch (e.Key)
            {
                case Key.Up: player.MoveDU(); break;//enum
                case Key.Down: player.MoveDD(); break;
                case Key.Right: player.MoveDR(); break;
                case Key.Left: player.MoveDL(); break;
                case Key.LeftShift: player.Shift(); break;
                case Key.Z: player.Shoot(GameField(), bullets); break;
                case Key.X: if (player.bombs > 0)
                    {
                        ClearBullets();
                        stats.ChangeBombs(player.bombs);
                        player.bombs -= 1;
                    }
                    break;
                case (Key.Escape):
                    Pause();
                    if (Paused())
                        timer.Stop();
                    else
                        timer.Start();
                    break;
            }
        }
        public void GameKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: player.SMoveDU(); break;
                case Key.Down: player.SMoveDD(); break;
                case Key.Right: player.SMoveDR(); break;
                case Key.Left: player.SMoveDL(); break;
                case Key.LeftShift: player.StopShift(); break;
                case Key.Z: player.StopShoot(); break;
            }
        }
        public void Pause()
        {
            pause = !pause;
            if (!pause)
            {

                UnPauseMusic();
            }
            else
            {
                PauseMusic(); UnPauseSFX();
            }
        }
        public bool Paused()
        {
            return pause;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void ClearBullets()
        {
            int x = (int)player.GetX();
            int y = (int)player.GetY();
            int ex = (int)enemy.GetX();
            int ey = (int)enemy.GetY();
            GameField().Children.Clear();
            bullets.Clear();
            player.Spawn(x, y, GameField());
            enemy.Spawn(ex, ey, GameField());
            field.SetBorder();
            GC.Collect();
        }
        public void ChangeStage()
        {
            stage_number++;
            multiplier *= 2;
            if (stage_number == 9)
            {
                Game_Won();
                return;
            }
            enemy.ChngStrategy(GameField(), stage_number);
            stats.ChangeMultiplier(multiplier);
            GC.Collect();
        }

        public void Game_Won()
        {
            score += 10000000;
            Won = true;
            Ended = true;
            StopMusic();
        }
        public void BadEnd()
        {
            Ended = true;
            StopMusic();
        }
        
        public void GameStep()
        {
            enemy.Do(GameField(), bullets);
            player.Do(GameField(), bullets);
            if (bullets.Count > 0)
            {
                for (int i = 0; i < bullets.Count - 1; i++)
                {

                    bullets[i].Move(GameField(), bullets, i);

                    if (bullets[i].DidHit(player))
                    {
                        PlayerGotHit();
                        break;
                    }
                    if (bullets[i].DidHit(enemy))
                        EnemyGotHit(i);
                }
            }
        }

        public void PlayerGotHit()
        {
            player.hp--;
            ClearBullets();
            player.ReSpawn((int)(field.GetWidth() / 2),
                (int)(field.GetHeight() / 4 * 3),
                GameField());
            stats.ChangeHP(player.hp);
            if (player.hp == 0)
                BadEnd();
            score -= 10000;
            multiplier = 1;
            stats.ChangeScore(score);
            stats.ChangeMultiplier(multiplier);
            enemy.SetDelay(25);
            PlayerDeadSFX();
        }
        public void EnemyGotHit(int i)
        {
            if (enemy.hp - player.damage < 0)
                enemy.hp = 0;
            else
                enemy.hp -= player.damage;
            stats.ChangeEnemyHP(enemy.hp);

            bullets[i].Delete(GameField(), bullets, i);
            score += score_per_enemy_hit * multiplier;
            stats.ChangeScore(score);
            if (enemy.hp == 0)
                ChangeStage();
        }
        ////////////////////////////////////////////////////////////////////////
        /// SFX
        ////////////////////////////////////////////////////////////////////////
        public void StartMusic()
        {
            string path = System.IO.Path.
                Combine(Environment.CurrentDirectory, "sfx\\bgm.mp3");
            music.Open(new Uri(path));
            music.Volume = 0.1;
            music.Play();
            
            
        }
        public void PauseMusic()
        { music.Pause();}
        public void UnPauseMusic()
        { music.Play(); }
        public void StopMusic()
        { music.Close();}
        public void PlayerDeadSFX()
        {
            string path = System.IO.Path.Combine
                (Environment.CurrentDirectory, "sfx\\TouhouDeath.mp3");
            DeathSoundEffect.Open(new Uri(path));
            DeathSoundEffect.Play();
        }
        public void UnPauseSFX()
        {
            string path = System.IO.Path.Combine
                (Environment.CurrentDirectory, "sfx\\UnpauseSoundEffect.mp3");
            unPauseSFX.Open(new Uri(path));
            unPauseSFX.Volume = 1;
            unPauseSFX.Play();
        }

        ///////
        ///
        //////

        internal Canvas GameField()
        {
            return field.GetCanvas();
        }
    }
}


