using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects
{
    public abstract class GameObject
    {
        private readonly List<Component> _components = new List<Component>();

        public virtual void OnUpdate(GameTime gt) {}
        public virtual void OnDraw(SpriteBatch sb, GameTime gt){}
        
        public void Update(GameTime gt)
        {
            foreach (var c in _components)
            {
                c.Update(gt);
            }
            
            OnUpdate(gt);
        }

        public void Draw(SpriteBatch sb, GameTime gt)
        {
            foreach (var c in _components)
            {
                c.Draw(sb, gt);
            }
            
            OnDraw(sb, gt);
        }
        
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T c)
                {
                    return c;
                }
            }

            return null;
        }
        
        public bool GetComponent<T>(out T component) where T : Component
        {
            for (var i = 0; i < _components.Count; i++)
            {
                if (!(_components[i] is T c)) continue;
                component = c;
                return true;
            }

            component = null;
            return false;
        }

        public List<Component> GetComponnents() => _components;
        
        public List<Component> GetComponnents<T>() where  T : Component
        {
            var result = new List<Component>();
            
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T c)
                {
                    result.Add(c);
                }
            }

            return result;
        }
        
        public bool GetComponnents<T>(out List<Component> result) where  T : Component
        {
            result = new List<Component>();
            
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T c)
                {
                    result.Add(c);
                }
            }

            return result.Any();
        }

        public T Attach<T>(T component) where T : Component => AddComponnent(component); 
        
        public T AddComponnent<T>(T component) where T : Component
        {
            _components.Add(component);
            component.GameObject = this;
            return component;
        }

        public bool HasComponent<T>() where T : Component
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T)
                {
                    return true;
                }
            }

            return false;
        }
    }
}