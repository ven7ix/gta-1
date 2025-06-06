﻿using System;
using System.Drawing;

namespace gta_1
{
    public class Player : IEntity
    {
        public int MaximumHP { get; set; }
        public int CurrentHP { get; set; }
        public int Speed { get; set; }
        public bool Interactable { get; set; }
        public Weapon EquippedWeapon { get; set; }

        public Point MovingVector { get; set; }
        public Point LastMovingVector { get; set; }
        public Point Position { get; set; }
        public Point LookingDirection { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle MaxReachBounds { get; set; }
        public bool[,] PlayerRealReach { get; set; }

        public Bitmap Sprite { get; set; }
        public Bitmap[,] AnimationSprites { get; set; }
        public int Frame { get; set; }
        public int TimeElapsedSinceLastFrame { get; set; }

        public Rectangle RenderedBounds { get; set; }

        public struct Weapon
        {
            public int Type;
            public int Range;
            public int Damage;

            public Weapon(int type, int range, int damage)
            {
                Type = type;
                Range = range;
                Damage = damage;
            }
        }

        public Player(Point position, Size size, int maximumHP, int speedModifier, Weapon weapon)
        {
            Interactable = false;

            MaximumHP = maximumHP;
            CurrentHP = maximumHP;
            Speed = Tools.TileSize / speedModifier;
            EquippedWeapon = weapon;

            Position = position;
            Bounds = new Rectangle(position, size);

            PlayerRealReach = new bool[Map.WorldMapSize.X, Map.WorldMapSize.Y];
            MaxReachBounds = new Rectangle()
            {
                Location = new Point()
                {
                    X = Bounds.Location.X - EquippedWeapon.Range * Tools.TileSize,
                    Y = Bounds.Location.Y - EquippedWeapon.Range * Tools.TileSize
                },
                Size = new Size(Bounds.Width + 2 * EquippedWeapon.Range * Tools.TileSize, Bounds.Height + 2 * EquippedWeapon.Range * Tools.TileSize)
            };

            UpdateRenderedBounds();
            UpdateReach();

            Animation.SetAnimationSprites(this);
        }

        public Player(Player playerToCopy)
        {
            MaximumHP = playerToCopy.MaximumHP;
            CurrentHP = playerToCopy.CurrentHP;
            Speed = playerToCopy.Speed;
            Interactable = playerToCopy.Interactable;
            EquippedWeapon = playerToCopy.EquippedWeapon;

            Position = playerToCopy.Position;
            LookingDirection = playerToCopy.LookingDirection;
            Bounds = playerToCopy.Bounds;
            MaxReachBounds = playerToCopy.MaxReachBounds;
            PlayerRealReach = playerToCopy.PlayerRealReach;

            Sprite = playerToCopy.Sprite;
            AnimationSprites = playerToCopy.AnimationSprites;

            RenderedBounds = playerToCopy.RenderedBounds;
        }

        public void Move(Point moveDirection)
        {
            MovingVector = moveDirection;

            if (moveDirection == new Point(0, 0))
                return;

            Point newPosition = new Point()
            {
                X = Position.X + moveDirection.X * Speed,
                Y = Position.Y - moveDirection.Y * Speed
            };

            if (MoveCollisionCheckObject(newPosition))
            {
                if (Math.Abs(moveDirection.X) + Math.Abs(moveDirection.Y) < 2)
                    return;

                newPosition = new Point(Position.X + moveDirection.X * Speed, Position.Y);
                if (MoveCollisionCheckObject(newPosition))
                {
                    newPosition = new Point(Position.X, Position.Y - moveDirection.Y * Speed);
                    if (MoveCollisionCheckObject(newPosition))
                        return;
                }
            }

            if (MoveCollisionCheckEntity(newPosition))
                return;

            Position = newPosition;
            Bounds = new Rectangle(newPosition, Bounds.Size);
            MaxReachBounds = new Rectangle()
            {
                Location = new Point()
                {
                    X = Bounds.Location.X - EquippedWeapon.Range * Tools.TileSize,
                    Y = Bounds.Location.Y - EquippedWeapon.Range * Tools.TileSize
                },
                Size = new Size(Bounds.Width + 2 * EquippedWeapon.Range * Tools.TileSize, Bounds.Height + 2 * EquippedWeapon.Range * Tools.TileSize)
            };

            UpdateReach();
        }

        public bool MoveCollisionCheckObject(Point newPosition)
        {
            Rectangle newBounds = new Rectangle(newPosition, Bounds.Size);

            for (int x = 0; x < Map.WorldMapSize.X; x++)
            {
                for (int y = 0; y < Map.WorldMapSize.Y; y++)
                {
                    Tile tile = Map.WorldMap[x, y];

                    if (!tile.Passable)
                    {
                        //проверка входит ли объект в хитбокс игрока (на каждый из 4х углов)
                        if (newBounds.Contains(tile.Position))
                            return true;

                        if (newBounds.Contains(new Point(tile.Position.X + Tools.TileSize - 1, tile.Position.Y)))
                            return true;

                        if (newBounds.Contains(new Point(tile.Position.X, tile.Position.Y + Tools.TileSize - 1)))
                            return true;

                        if (newBounds.Contains(new Point(tile.Position.X + Tools.TileSize - 1, tile.Position.Y + Tools.TileSize - 1)))
                            return true;
                    }
                }
            }

            return false;
        }

        public bool MoveCollisionCheckEntity(Point newPosition)
        {
            foreach (IEntity entity in Game.entities)
            {
                if (entity == this)
                    continue;

                //проверка входит ли хитбокс игрока в сущность (на каждый из 4х углов)
                if (entity.Bounds.Contains(newPosition))
                    return true;

                if (entity.Bounds.Contains(new Point(newPosition.X + Bounds.Width - 1, newPosition.Y)))
                    return true;

                if (entity.Bounds.Contains(new Point(newPosition.X, newPosition.Y + Bounds.Height - 1)))
                    return true;

                if (entity.Bounds.Contains(new Point(newPosition.X + Bounds.Width - 1, newPosition.Y + Bounds.Height - 1)))
                    return true;
            }

            return false;
        }

        public IEntity Interact(IEntity entity)
        {
            return entity;
        }

        public void Hit(IEntity entity)
        {
            Rectangle entityBoundsRealiveToPlayer = new Rectangle()
            {
                Location = Tools.GetPositionRelativeToPlayer(entity.Position),
                Size = entity.Bounds.Size
            };
            if (!entityBoundsRealiveToPlayer.Contains(LookingDirection))
                return;

            for (int x = 0; x < Map.WorldMapSize.X; x++)
            {
                for (int y = 0; y < Map.WorldMapSize.Y; y++)
                {
                    if (!PlayerRealReach[x, y])
                        continue;

                    if (entity.Bounds.Contains(Map.WorldMap[x, y].Position))
                    {
                        entity.CurrentHP -= EquippedWeapon.Damage;

                        if (entity.CheckIfDead())
                            Sound.EnqueueSound(8);

                        switch (EquippedWeapon.Type)
                        {
                            case 0:
                                Sound.EnqueueSound(6);
                                break;
                            case 1:
                                Sound.EnqueueSound(5);
                                break;
                            case 2:
                                Sound.EnqueueSound(5);
                                break;
                        }

                        return;
                    }
                }
            }
        }

        public void UpdateRenderedBounds()
        {
            RenderedBounds = new Rectangle()
            {
                Location = new Point()
                {
                    X = Tools.ScreenCentre.X - Tools.TileSize * Options.RenderDistance,
                    Y = Tools.ScreenCentre.Y - Tools.TileSize * Options.RenderDistance / 2,
                },
                Size = new Size(Tools.TileSize * Options.RenderDistance * 2, Tools.TileSize * Options.RenderDistance)
            };
        }

        public void UpdateReach()
        {
            Point lookingVector = new Point()
            {
                X = Math.Sign((LookingDirection.X - Tools.ScreenCentre.X) / (Tools.ScreenCentre.X / 6)),
                Y = Math.Sign((LookingDirection.Y - Tools.ScreenCentre.Y) / (Tools.ScreenCentre.Y / 6))
            };
            Rectangle alwayerReachable = new Rectangle()
            {
                Location = new Point()
                {
                    X = Position.X - Tools.TileSize,
                    Y = Position.Y - Tools.TileSize
                },
                Size = new Size(3 * Tools.TileSize, 3 * Tools.TileSize)
            };

            for (int x = 0; x < Map.WorldMapSize.X; x++)
            {
                for (int y = 0; y < Map.WorldMapSize.Y; y++)
                {
                    PlayerRealReach[x, y] = false;
                    Tile tile = Map.WorldMap[x, y];

                    if (alwayerReachable.Contains(tile.Position))
                    {
                        PlayerRealReach[x, y] = tile.Passable;
                        continue;
                    }
                    
                    if (!MaxReachBounds.Contains(tile.Position))
                        continue;

                    if (x - lookingVector.X >= Map.WorldMapSize.X || y - lookingVector.Y >= Map.WorldMapSize.Y || x - lookingVector.X < 0 || y - lookingVector.Y < 0)
                        continue;

                    if (!PlayerRealReach[x - lookingVector.X, y - lookingVector.Y])
                        continue;

                    PlayerRealReach[x, y] = tile.Passable;
                }
            }
        }

        public void CalculateLookingDirection(Point mousePosition)
        {
            LookingDirection = mousePosition;

            Rectangle maxReachBoundsScreen = new Rectangle()
            {
                Location = new Point()
                {
                    X = Tools.ScreenCentre.X - EquippedWeapon.Range * Tools.TileSize,
                    Y = Tools.ScreenCentre.Y - EquippedWeapon.Range * Tools.TileSize
                },
                Size = new Size(Bounds.Width + 2 * EquippedWeapon.Range * Tools.TileSize, Bounds.Height + 2 * EquippedWeapon.Range * Tools.TileSize)
            };

            if (mousePosition.X > maxReachBoundsScreen.Right && mousePosition.Y > maxReachBoundsScreen.Bottom)
            {
                LookingDirection = new Point(maxReachBoundsScreen.Right, maxReachBoundsScreen.Bottom);
                UpdateReach();
                return;
            }
            if (mousePosition.X > maxReachBoundsScreen.Right && mousePosition.Y < maxReachBoundsScreen.Top)
            {
                LookingDirection = new Point(maxReachBoundsScreen.Right, maxReachBoundsScreen.Top);
                UpdateReach();
                return;
            }
            if (mousePosition.X < maxReachBoundsScreen.Left && mousePosition.Y > maxReachBoundsScreen.Bottom)
            {
                LookingDirection = new Point(maxReachBoundsScreen.Left, maxReachBoundsScreen.Bottom);
                UpdateReach();
                return;
            }
            if (mousePosition.X < maxReachBoundsScreen.Left && mousePosition.Y < maxReachBoundsScreen.Top)
            {
                LookingDirection = new Point(maxReachBoundsScreen.Left, maxReachBoundsScreen.Top);
                UpdateReach();
                return;
            }

            if (mousePosition.X > maxReachBoundsScreen.Right)
            {
                LookingDirection = new Point(maxReachBoundsScreen.Right, mousePosition.Y);
            }
            else if (mousePosition.X < maxReachBoundsScreen.Left)
            {
                LookingDirection = new Point(maxReachBoundsScreen.Left, mousePosition.Y);
            }

            if (mousePosition.Y > maxReachBoundsScreen.Bottom)
            {
                LookingDirection = new Point(mousePosition.X, maxReachBoundsScreen.Bottom);
            }
            else if (mousePosition.Y < maxReachBoundsScreen.Top)
            {
                LookingDirection = new Point(mousePosition.X, maxReachBoundsScreen.Top);
            }

            UpdateReach();
        }

        public bool CheckIfDead()
        {
            return CurrentHP <= 0;
        }

        public bool IsMoving(Point movingVector)
        {
            return !(movingVector.X == 0 && movingVector.Y == 0);
        }

        public bool IsMovingDiagonally(Point movingVector)
        {
            return Math.Abs(movingVector.X) + Math.Abs(movingVector.Y) == 2;
        }

        public void RenderEntity(Graphics screen)
        {
            //LookigDirection
            for (int x = 0; x < Map.WorldMapSize.X; x++)
            {
                for (int y = 0; y < Map.WorldMapSize.Y; y++)
                {
                    if (!PlayerRealReach[x, y])
                        continue;

                    //screen.FillRectangle(Brushes.Blue, new Rectangle(Tools.GetPositionRelativeToPlayer(Map.WorldMap[x, y].Position), Bounds.Size));
                }
            }
            screen.DrawLine(Pens.Black, new Point(Tools.ScreenCentre.X + Bounds.Width / 2, Tools.ScreenCentre.Y + Bounds.Height / 2), LookingDirection);

            //Player
            Animation.AnimationHanler(this, 20);
            screen.DrawImage(Sprite, new Rectangle(Tools.ScreenCentre, Bounds.Size));
        }
    }
}