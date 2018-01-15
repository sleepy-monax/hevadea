using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Items.Materials;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Ressource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Items.Tools
{
    public abstract class ToolItem : Item
    {
        public Material Material { get; private set; }
        public ToolType Type { get; private set; }
        public Sprite Sprite { get; private set; }

        public ToolItem(byte id, Sprite sprite, Material material, ToolType type) : base(id)
        {
            Material = material;
            Type = type;
        }

        public override float GetAttackBonus(Entity target)
        {
            return Type.GetAttackBonus(target) * Type.GetAttackBonus(target);
        }

        public override float GetAttackBonus(Tile target)
        {
            return Type.GetAttackBonus(target) * Type.GetAttackBonus(target);
        }
    }
}
