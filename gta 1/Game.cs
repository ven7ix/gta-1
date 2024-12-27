using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace gta_1
{
    public partial class Game : Form
    {
        public static IEntity player;
        public static List<IEntity> entities = new List<IEntity>();
        private readonly HashSet<Keys> PressedMovementKeys = new HashSet<Keys>();

        //Весь рендер происходит относительно игорка (игрок не двигается)
        //А все расчеты происходят относительно карты (карта не двигается)
        public Game()
        {
            InitializeComponent();
            Sound.Initialize();
            Sound.PlayMusic(0);

            Screen.MouseMove -= Screen_MouseMove;
            KeyDown -= Game_KeyDown;
            KeyUp -= Game_KeyUp;
            Screen.MouseClick -= Screen_MouseClick;
            Screen.Paint -= Screen_Paint;
            Resize -= Game_Resize;
            FormClosing -= Game_FormClosing;
        }

        private void TimerGameLoop_Tick(object sender, EventArgs e)
        {
            player.UpdateRenderedBounds();
            CheckMovementKeys();

            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].CheckIfDead())
                    entities.RemoveAt(i--);
            }

            foreach (IEntity entity in entities)
            {
                if (!player.RenderedBounds.Contains(Tools.GetPositionRelativeToPlayer(entity.Position)))
                    continue;

                if (entity is NPC)
                    (entity as NPC).Wonder();
            }

            if (player.CheckIfDead())
                TimerGameLoop.Stop();

            Screen.Invalidate();
        }

        private void Screen_MouseMove(object sender, MouseEventArgs e)
        {
            player.CalculateLookingDirection(e.Location);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (e.KeyCode == Keys.Escape)
            {
                if (player.CheckIfDead())
                {
                    if (!PictureBoxMenu.Visible)
                    {
                        Sound.PlayMusic(0);

                        PictureBoxMenu.Visible = true;
                        ButtonResume.Enabled = ButtonResume.Visible = true;
                        ButtonStartNewGame.Enabled = ButtonStartNewGame.Visible = true;
                        ButtonOptions.Enabled = ButtonOptions.Visible = true;
                        ButtonQuit.Enabled = ButtonQuit.Visible = true;
                    }
                    else
                    {
                        PictureBoxMenu.Visible = false;
                        ButtonResume.Enabled = ButtonResume.Visible = false;
                        ButtonStartNewGame.Enabled = ButtonStartNewGame.Visible = false;
                        ButtonOptions.Enabled = ButtonOptions.Visible = false;
                        ButtonQuit.Enabled = ButtonQuit.Visible = false;

                        Sound.MusicOutput?.Dispose();
                        Sound.MemoryStreamMusic?.Dispose();
                    }
                    return;
                }

                if (TimerGameLoop.Enabled)
                {
                    Sound.PlayMusic(0);

                    TimerGameLoop.Stop();
                    PictureBoxMenu.Visible = true;
                    ButtonResume.Enabled = ButtonResume.Visible = true;
                    ButtonStartNewGame.Enabled = ButtonStartNewGame.Visible = true;
                    ButtonOptions.Enabled = ButtonOptions.Visible = true;
                    ButtonQuit.Enabled = ButtonQuit.Visible = true;
                }
                else
                {
                    TimerGameLoop.Start();
                    PictureBoxMenu.Visible = false;
                    ButtonResume.Enabled = ButtonResume.Visible = false;
                    ButtonStartNewGame.Enabled = ButtonStartNewGame.Visible = false;
                    ButtonOptions.Enabled = ButtonOptions.Visible = false;
                    ButtonQuit.Enabled = ButtonQuit.Visible = false;

                    Sound.MusicOutput?.Dispose();
                    Sound.MemoryStreamMusic?.Dispose();
                }
            }
            else if (e.KeyCode == Keys.E)
            {
                if (player.CheckIfDead())
                    return;

                if (player.IsMoving(player.MovingVector))
                    return;

                foreach (IEntity entity in entities)
                    player = entity.Interact(player);

                if (!entities.Contains(player))
                    entities.Add(player);
            }
            else if (!PressedMovementKeys.Contains(e.KeyCode))
            {
                if (player.CheckIfDead())
                    return;

                PressedMovementKeys.Add(e.KeyCode);
            }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            PressedMovementKeys.Remove(e.KeyCode);
        }

        private void CheckMovementKeys()
        {
            if (PressedMovementKeys.Contains(Keys.W) && PressedMovementKeys.Contains(Keys.D))
            {
                player.Move(new Point(1, 1));
            }
            else if (PressedMovementKeys.Contains(Keys.W) && PressedMovementKeys.Contains(Keys.A))
            {
                player.Move(new Point(-1, 1));
            }
            else if (PressedMovementKeys.Contains(Keys.S) && PressedMovementKeys.Contains(Keys.D))
            {
                player.Move(new Point(1, -1));
            }
            else if (PressedMovementKeys.Contains(Keys.S) && PressedMovementKeys.Contains(Keys.A))
            {
                player.Move(new Point(-1, -1));
            }
            else if (PressedMovementKeys.Contains(Keys.W))
            {
                player.Move(new Point(0, 1));
            }
            else if (PressedMovementKeys.Contains(Keys.A))
            {
                player.Move(new Point(-1, 0));
            }
            else if (PressedMovementKeys.Contains(Keys.S))
            {
                player.Move(new Point(0, -1));
            }
            else if (PressedMovementKeys.Contains(Keys.D))
            {
                player.Move(new Point(1, 0));
            }
            else
            {
                player.Move(new Point(0, 0));
            }
        }

        private void Screen_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (IEntity entity in entities)
            {
                if (player is Player)
                    (player as Player).Hit(entity);
            }
        }

        private void Screen_Paint(object sender, PaintEventArgs e)
        {
            Graphics screen = e.Graphics;

            Map.RenderTiles(screen);

            foreach (IEntity entity in entities)
                entity.RenderEntity(screen);

            Map.RenderWalls(screen);

            Map.RenderRoofs(screen);
        }


        /// <summary>
        /// Menu
        /// </summary>
        private void Game_Resize(object sender, EventArgs e)
        {
            Tools.UpdateScreenSize(new Point(Screen.Width, Screen.Height));
            player.UpdateRenderedBounds();
        }

        private void ButtonStartNewGame_Click(object sender, EventArgs e)
        {
            Screen.MouseMove += Screen_MouseMove;
            KeyDown += Game_KeyDown;
            KeyUp += Game_KeyUp;
            Screen.MouseClick += Screen_MouseClick;
            Screen.Paint += Screen_Paint;
            Resize += Game_Resize;
            FormClosing += Game_FormClosing;

            PictureBoxMenu.Visible = false;
            ButtonResume.Enabled = ButtonResume.Visible = false;
            ButtonStartNewGame.Enabled = ButtonStartNewGame.Visible = false;
            ButtonOptions.Enabled = ButtonOptions.Visible = false;
            ButtonQuit.Enabled = ButtonQuit.Visible = false;

            player = null;
            entities.Clear();
            PressedMovementKeys.Clear();
            if (Map.WorldMap != null)
                Array.Clear(Map.WorldMap, 0, Map.WorldMap.Length);
            if (Map.Roads != null)
                Array.Clear(Map.Roads, 0, Map.Roads.Length);
            if (Map.Pavements != null)
                Array.Clear(Map.Pavements, 0, Map.Pavements.Length);


            Tools.Initialize(64, new Point(Screen.Width, Screen.Height));

            Map.LoadMapFromFile();

            player = new Player(Map.WorldMap[30, 30].Position, new Size(Tools.TileSize, Tools.TileSize), 100, 8, new Player.Weapon(0, 1, 10));
            entities.Add(player);
            TimerGameLoop.Start();

            Sound.Initialize();
            Sound.MusicOutput?.Dispose();
            Sound.MemoryStreamMusic?.Dispose();
        }

        private void ButtonResume_Click(object sender, EventArgs e)
        {
            TimerGameLoop.Start();
            PictureBoxMenu.Visible = false;
            ButtonResume.Enabled = ButtonResume.Visible = false;
            ButtonStartNewGame.Enabled = ButtonStartNewGame.Visible = false;
            ButtonOptions.Enabled = ButtonOptions.Visible = false;
            ButtonQuit.Enabled = ButtonQuit.Visible = false;

            Sound.MusicOutput?.Dispose();
            Sound.MemoryStreamMusic?.Dispose();
        }

        private void ButtonQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Tools.CancellationToken.Cancel();
        }

        private void ButtonOptions_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }
    }
}