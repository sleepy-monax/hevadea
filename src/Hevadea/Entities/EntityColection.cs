using Hevadea.Registry;
using Hevadea.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Entities
{
    public class EntityColection : List<Entity>
    {
        public void SortForRender()
        {
            Sort((a, b) => (a.Y + a.SortingOffset).CompareTo(b.Y + b.SortingOffset));
        }

        public void UpdateAll(GameTime gameTime)
        {
            foreach (var e in this)
            {
                foreach (var sys in SYSTEMS.Systems)
                {
                    if (e.Match(sys.Filter) && sys is IProcessSystem process)
                    {
                        process.Process(e, gameTime);
                    }
                }

                e.Update(gameTime);
            }
        }
    }
}