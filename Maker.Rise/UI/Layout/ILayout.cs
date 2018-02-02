using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Maker.Rise.UI.Layout
{
    public interface ILayout
    {
        void Refresh(Rectangle host, List<Control> childs);
    }
}