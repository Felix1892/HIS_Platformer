using System.Xml.Schema;
using SFML.Graphics;
using SFML.System;


namespace platformer
{
    public class Entity
    {
        private readonly string textureName;
        protected readonly Sprite sprite;
        public bool Dead;

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }

        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.LoadTexture(textureName);
        }

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual FloatRect Bounds => sprite.GetGlobalBounds();
        public virtual bool Solid => false;

        public virtual void Update(Scene scene, float deltaTime)
        {
            
        }

        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }    
}
