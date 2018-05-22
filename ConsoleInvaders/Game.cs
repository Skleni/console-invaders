using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInvaders
{
    class Game
    {
        private const int SPAWN_INTERVAL = 3000;
        private const int PLAYER_STRENGTH = 3;
        private const int ENEMY_STRENGTH = 2;

        private int enemyStrength = ENEMY_STRENGTH;

        private Random random;

        private DateTime lastSpawn;

        public int Score { get; private set; }
        public int DestroyedShips { get; private set; }

        public Map Map { get; private set; }
        public Ship Ship { get; private set; }
        public IList<Bullet> Bullets { get; private set; }
        public IList<Enemy> Enemies { get; private set; }
        public IList<Explosion> Explosions { get; private set; }

        public bool IsPlaying
        {
            get { return Ship != null || Enemies.Count > 0; }
        }

        public Game(int mapWidth, int mapHeight)
        {
            random = new Random();
            lastSpawn = DateTime.Now;

            Score = 0;
            DestroyedShips = 0;

            Map = new Map(mapWidth, mapHeight);
            Bullets = new List<Bullet>();
            Enemies = new List<Enemy>();
            Explosions = new List<Explosion>();

            Ship = new Ship(this, Ship.PLAYER, (Map.Width / 2) - (Ship.PLAYER.Length / 2), Map.Height - 1, PLAYER_STRENGTH);
        }

        public void Draw() 
        {
            Map.Draw();
            if (Ship != null)
                Ship.Draw();
        }

        public void Update()
        {
            if (Ship != null)
            {
                if ((DateTime.Now - lastSpawn).TotalMilliseconds >= SPAWN_INTERVAL)
                    SpawnEnemy();
            }

            for (int b = 0; b < Bullets.Count; b++)
            {
                Bullets[b].Update();
                Bullets[b].Clear();

                if (Bullets[b].IsInMap)
                {
                    bool hit = false;

                    for (int e = 0; e < Enemies.Count; e++)
                    {
                        if (Enemies[e].IsAtCoordinates(Bullets[b].X, Bullets[b].Y) ||
                            Enemies[e].IsAtCoordinates(Bullets[b].LastX, Bullets[b].LastY))
                        {
                            hit = true;

                            Explosions.Add(new Explosion(this, Bullets[b].X, Bullets[b].LastY));
                            if (Enemies[e].Hit())
                            {
                                DestroyedShips++;
                                Enemies[e].Explode();
                                Enemies[e].Delete();
                                Enemies.RemoveAt(e--);
                            }
                            break;
                        }
                    }

                    if (hit)
                    {
                        Bullets.RemoveAt(b--);
                        IncreaseScore(1);
                    }
                    else
                    {
                        Bullets[b].Redraw();
                    }
                }
                else
                {
                    Bullets.RemoveAt(b--);
                }
            }

            for (int e = 0; e < Enemies.Count; e++)
            {
                Enemies[e].Update();
                Enemies[e].Clear();

                if (Enemies[e].IsInMap)
                {
                    if (Ship != null)
                    {
                        bool hit = false;

                        int y = Enemies[e].Y;
                        foreach (int x in Enemies[e].GetXValues())
                        {
                            if (Ship.IsAtCoordinates(x, y) || Ship.WasAtCoordinates(x, y))
                            {
                                hit = true;
                                break;
                            }
                        }

                        if (hit)
                        {
                            Explosions.Add(new Explosion(this, Ship.X, Ship.Y));
                            
                            Enemies[e].Explode();
                            Enemies[e].Delete();
                            Enemies.RemoveAt(e--);

                            Ship.Explode();
                            Ship.Delete();
                            Ship = null;
                            break;
                        }
                        else
                            Enemies[e].Redraw();
                    }
                   
                    Enemies[e].Redraw();
                }
                else
                {
                    Enemies[e].Clear();
                    Enemies.RemoveAt(e--);
                }
            }

            for (int e = 0; e < Explosions.Count; e++)
            {
                Explosions[e].Update();
                Explosions[e].Clear();

                if (Explosions[e].IsFinished)
                    Explosions.RemoveAt(e--);
                else
                    Explosions[e].Redraw();
            }

            if (Ship != null)
            {
                Ship.Update();
                Ship.Clear();
                Ship.Redraw();
            }
        }

        private void SpawnEnemy()
        {
            int x = random.Next(Map.Width - 2 * Map.Offset) + Map.Offset;
            var enemy = new Enemy(this, x, Map.Offset, enemyStrength);
            enemy.Draw();
            Enemies.Add(enemy);
            
            lastSpawn = DateTime.Now;
        }

        private void IncreaseScore(int score)
        {
            Score += score;
            enemyStrength = ENEMY_STRENGTH + (DestroyedShips / 20);
        }
    }
}
