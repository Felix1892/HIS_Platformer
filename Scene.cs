using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Security;
using Platformer;
using SFML.Graphics;
using SFML.System;

namespace platformer
{
    public class Scene
    {
        private readonly Dictionary<string, Texture> _textures;
        private readonly List<Entity> _entities;

        public Scene()
        {
            _textures = new Dictionary<string, Texture>();
            _entities = new List<Entity>();
        }

        public void Spawn(Entity entity)
        {
            _entities.Add(entity);
            entity.Create(this);
        }

        public Texture LoadTexture(string name)
        {
            if (_textures.TryGetValue(name, out Texture found))
            {
                return found;
            }
            
            string fileName = $"assets/{name}.png";
            Texture texture = new Texture(fileName);
            _textures.Add(name, texture);
            return texture;
        }

        public void UpdateAll(float deltaTime)
        {
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                Entity entity = _entities[i];
                entity.Update(this, deltaTime);
            }

            for (int i = 0; i < _entities.Count;)
            {
                Entity entity = _entities[i];

                if (entity.Dead) _entities.RemoveAt(i);
                else i++;
            }
        }

        public void RenderAll(RenderTarget target)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                _entities[i].Render(target);
            }
        }

        public bool TryMove(Entity entity, Vector2f movement)
        {
            entity.Position += movement;
            bool collided = false;
            
            for (int i = 0; i < _entities.Count; i++)
            {
                Entity other = _entities[i];
                if (!other.Solid) continue;
                if (other == entity) continue;
                
                FloatRect boundsA = entity.Bounds;
                FloatRect boundsB = other.Bounds;
                if (Collision.RectangleRectangle(boundsA, boundsB, out Collision.Hit hit))
                {
                    entity.Position += hit.Normal * hit.Overlap;
                    i = -1;
                    collided = true;
                }
            }
            
            return collided;
        }
    }    
}
