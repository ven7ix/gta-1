using System;
using System.Drawing;

namespace gta_1
{
    internal class Weapon : IEntity
    {
        public int MaximumHP { get; set; }
        public int CurrentHP { get; set; }
        public int Speed { get; set; }
        public bool Interactable { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Type { get; set; }

        public Point MovingVector { get; set; }
        public Point LastMovingVector { get; set; }
        public Point Position { get; set; }
        public Point LookingDirection { get; set; }
        public Rectangle Bounds { get; set; }

        public Bitmap Sprite { get; set; }
        public Bitmap[,] AnimationSprites { get; set; }
        public int Frame { get; set; }
        public int TimeElapsedSinceLastFrame { get; set; }

        public Rectangle RenderedBounds { get; set; }

        public Weapon(Point position, Size size, int type, int range, int damage, bool interactable)
        {
            MaximumHP = -1;
            CurrentHP = -1;
            Speed = 0;
            Type = type;
            Range = range;
            Damage = damage;
            Interactable = interactable;

            Position = position;
            LookingDirection = position;
            Bounds = new Rectangle(position, size);

            Animation.SetAnimationSprites(this);
        }

        public void Move(Point moveDirection)
        {
            return;
        }

        public bool MoveCollisionCheckObject(Point newPosition)
        {
            return false;
        }

        public bool MoveCollisionCheckEntity(Point newPosition)
        {
            return false;
        }

        public IEntity Interact(IEntity entity)
        {
            if (!Interactable)
                return entity;

            if (!Tools.GetBoundsRelativeToEntity(this, entity).Contains(entity.LookingDirection))
                return entity;

            Rectangle interactableHitbox = new Rectangle()
            {
                Location = new Point()
                {
                    X = Bounds.Location.X - Tools.TileSize,
                    Y = Bounds.Location.Y - Tools.TileSize
                },
                Size = new Size(Bounds.Width + 2 * Tools.TileSize, Bounds.Height + 2 * Tools.TileSize)
            };
            if (!interactableHitbox.Contains(Game.player.Position))
                return entity;

            if (entity is Player)
            {
                CurrentHP = 0;
                (entity as Player).EquippedWeapon = new Player.Weapon(Type, Range, Damage);

                Sound.EnqueueSound(0);
            }

            return entity;
        }

        public void UpdateRenderedBounds()
        {
            return;
        }

        public void CalculateLookingDirection(Point mousePosition)
        {
            return;
        }

        public bool CheckIfDead()
        {
            return CurrentHP == 0;
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
            if (!Game.player.RenderedBounds.Contains(Tools.GetPositionRelativeToPlayer(Position)))
                return;

            //Weapon
            screen.DrawImage(Sprite, new Rectangle(Tools.GetPositionRelativeToPlayer(Position), Bounds.Size));
        }
    }
}