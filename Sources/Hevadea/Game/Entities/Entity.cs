using Hevadea.Game.Entities.Component;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Tiles;
using Maker.Rise.Graphic.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class Entity
    {
        #region Properties
        
        public float X { get; private set; }
        public float Y { get; private set; }
        public Point Origin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Direction Facing { get; set; }
        public bool Removed { get; set; } = true;
        public EntityComponentsManager Components { get; }
        public EntityBlueprint Blueprint { get; set; } = null;
        public ParticleSystem ParticleSystem { get; } = new ParticleSystem();
        
        public Level Level { get; private set; }
        public World World { get; private set; }
        public GameManager Game { get; private set; }
        
        public Point Size => new Point(Width, Height);
        public Vector2 Position => new Vector2(X, Y);
        public Rectangle Bound => new Rectangle(Position.ToPoint(), Size);
    
        #endregion

        #region Properties Getters and Setters
        
        public bool IsColliding(Rectangle rect)
        {
            return IsColliding(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public bool IsColliding(float x, float y, int width1, int height1)
        {
            return X < x + width1 &&
                   X + Width > x &&
                   Y < y + height1 &&
                   Height + Y > y;
        }
        
        public TilePosition GetTilePosition(bool onOrigine = false)
        {
            if (onOrigine)
                return new TilePosition((int) ((X + Origin.X) / ConstVal.TileSize),
                    (int) ((Y + Origin.Y) / ConstVal.TileSize));

            return new TilePosition((int) (X / ConstVal.TileSize), (int) (Y / ConstVal.TileSize));
        }

        public Tile GetTileOnMyOrigin()
        {
            return Level.GetTile(GetTilePosition(true));
        }

        public TilePosition GetFacingTile()
        {
            var dir = Facing.ToPoint();
            var pos = GetTilePosition(true);
            
            return new TilePosition(dir.X + pos.X, dir.Y + pos.Y);
        }

        public virtual void SetPosition(float x, float y)
        {
            var oldPosition = GetTilePosition();

            X = x; Y = y;

            var pos = GetTilePosition();

            Level?.RemoveEntityFromTile(oldPosition, this);
            Level?.AddEntityToTile(pos, this);
        }

        #endregion
        
        #region Constructor
        
        public Entity()
        {
            Components = new EntityComponentsManager(this);
            Facing = Direction.Down;
            Origin = new Point(0, 0);
            Width = 32;
            Height = 32;
        }
        
        #endregion

        #region Methodes

        internal void Initialize(Level level, World world, GameManager game)
        {
            Level = level;
            World = world;
            Game = game;
        }
        
        public EntityStorage Save()
        {
            var store = new EntityStorage(GetType().FullName);

            store.Set("X", X);
            store.Set("Y", Y);
            store.Set("Width", Width);
            store.Set("Height", Height);
            store.Set("Facing", (int) Facing);

            Components.Save(store);
            OnSave(store);

            return store;
        }

        public void Load(EntityStorage store)
        {
            X = store.GetFloat("X", X);
            Y = store.GetFloat("Y", Y);
            Width = store.GetInt("Width", Width);
            Height = store.GetInt("Height", Height);

            Facing = (Direction)(int)store.Get("Facing", (int) Facing);

            Components.Load(store);
            OnLoad(store);
        }

        public void Remove()
        {
            Level.RemoveEntity(this);
        }
        
        public void Update(GameTime gameTime)
        {
            Components.Update(gameTime);
            OnUpdate(gameTime);
            ParticleSystem.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Components.Draw(spriteBatch, gameTime);
            OnDraw(spriteBatch, gameTime);
            ParticleSystem.Draw(spriteBatch, gameTime);
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Components.DrawOverlay(spriteBatch, gameTime);
        }
        
        #endregion

        #region Virtual methodes

        public virtual void OnSave(EntityStorage store){}
        public virtual void OnLoad(EntityStorage store){}
        public virtual bool IsBlocking(Entity entity) => false;
        public virtual void OnUpdate(GameTime gameTime) {}
        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime){}
        
        #endregion
    }
}