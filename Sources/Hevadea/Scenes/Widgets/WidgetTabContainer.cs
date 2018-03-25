using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Scenes.Widgets
{

    public class Tab
    {
        public Sprite Icon { get; set; }
        public Widget Content { get; set; }
    }

    public class ContainerTab : Tab
    {
        private Tab _selectedChildren;

        public Tab SelectedChildren
        {
            get
            {
                if (_selectedChildren == null && Childrens.Count > 0)
                {
                    _selectedChildren = Childrens.First();
                }

                return _selectedChildren;
            }
            set => _selectedChildren = value;
        }

        public List<Tab> Childrens { get; set; } = new List<Tab>();
    }

    public class WidgetTabContainer : Widget
    {
        private Sprite _background;
        private Sprite _tab;
        private Sprite _tabSelected;

        private Sprite _subTab;
        private Sprite _subTabSelected;
        private Sprite _subContainerTabSelected;

        private Rectangle _clientArea => new Padding(32, 32, 96, 32).Apply(UnitBound);
        private Rectangle _clientAreaBound => new Padding(0, 0, 64, 0).Apply(UnitBound);

        public Tab SelectedTab { get; set; }
        public List<Tab> Tabs { get; set; } = new List<Tab>();

        public WidgetTabContainer()
        {
            _background = new Sprite(Ressources.TileGui, new Point(4, 0), new Point(2, 2));
            _tab = new Sprite(Ressources.TileGui, new Point(2, 2), new Point(2, 2));
            _tabSelected = new Sprite(Ressources.TileGui, new Point(0, 2), new Point(2, 2));
            _subContainerTabSelected = new Sprite(Ressources.TileGui, new Point(0, 4), new Point(2, 2));
            _subTab = new Sprite(Ressources.TileGui, new Point(6, 2), new Point(2, 2));
            _subTabSelected = new Sprite(Ressources.TileGui, new Point(4, 2), new Point(2, 2));

        }

        public override void RefreshLayout()
        {
            foreach (var t in Tabs)
            {
                if (t is ContainerTab c)
                {
                    c.SelectedChildren.Content.UnitBound = _clientArea;
                    c.SelectedChildren.Content.RefreshLayout();
                }
                else if (t.Content != null)
                {
                    t.Content.UnitBound = _clientArea;
                    t.Content.RefreshLayout();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            int onScreenIndex = 0;
            for (int i = 0; i < Tabs.Count; i++)
            {
                if (Rise.Pointing.AreaClick(GetTabIconBound(onScreenIndex, SelectedTab == Tabs[i])))
                {
                    SelectedTab = Tabs[i];
                }

                onScreenIndex++;

                if (Tabs[i] is ContainerTab container && container == SelectedTab)
                {
                    for (int j = 0; j < container.Childrens.Count; j++)
                    {
                        if (Rise.Pointing.AreaClick(GetTabIconBound(onScreenIndex, container.SelectedChildren == container.Childrens[j])))
                        {
                            container.SelectedChildren = container.Childrens[j];
                        }

                        onScreenIndex++;
                    }
                }
            }

            if (SelectedTab == null && Tabs.Count > 0) SelectedTab = Tabs.First(); 
            

            if (SelectedTab is ContainerTab c)
            {
                c.SelectedChildren?.Content?.UpdateInternal(gameTime);
            }
            else if (SelectedTab != null)
            {
                SelectedTab.Content?.UpdateInternal(gameTime);
            }
        }

        private Rectangle GetTabBound(int index)
        {
            return new Rectangle(Bound.X, Bound.Y + Scale(16 + 76 * index), Scale(128), Scale(128));
        }

        private Rectangle GetTabIconBound(int index, bool isSelected, bool isSubTab = false)
        {
            if (isSubTab)
            {
                return new Padding(Scale(32), Scale(32), Scale(32), Scale(32)).Apply(GetTabBound(index));
            }
            else
            {
                if (isSelected)
                    return new Padding(Scale(28), Scale(28), Scale(12), Scale(44)).Apply(GetTabBound(index));
                return new Padding(Scale(32), Scale(32), Scale(20), Scale(44)).Apply(GetTabBound(index));
            }

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var size = Scale(64);
            if (SelectedTab == null && Tabs.Count > 0) SelectedTab = Tabs.First();

            _background.Draw(spriteBatch, Scale(_clientArea), Color.White);

            if (SelectedTab is ContainerTab c)
            {
                c.SelectedChildren?.Content?.DrawIternal(spriteBatch, gameTime);
            }
            else
            {
                SelectedTab?.Content?.DrawIternal(spriteBatch, gameTime);
            }

            GuiHelper.DrawBox(spriteBatch, Scale(_clientAreaBound), size);


            var onScreenIndex = 0;

            for (int i = 0; i < Tabs.Count; i++)
            {
                if (Tabs[i] != SelectedTab)
                {
                    var tabBound = GetTabBound(onScreenIndex);
                    _tab.Draw(spriteBatch, tabBound, Color.White);
                    Tabs[i].Icon?.Draw(spriteBatch, GetTabIconBound(onScreenIndex, false), Color.White);

                }
                else
                {
                    if (SelectedTab is ContainerTab container)
                        onScreenIndex += container.Childrens.Count;
                }

                onScreenIndex++;
            }

            if (SelectedTab != null)
            {
                var index = Tabs.IndexOf(SelectedTab);

                if (SelectedTab is ContainerTab cont)
                {
                    for (int j = 0; j < cont.Childrens.Count; j++)
                    {
                        if (cont.SelectedChildren != cont.Childrens[j])
                        {
                            var childTabIndex = index + j + 1;
                            var tabBound = GetTabBound(childTabIndex);

                            _subTab.Draw(spriteBatch, tabBound, Color.White);
                            cont.Childrens[j].Icon?.Draw(spriteBatch, GetTabIconBound(childTabIndex, false, true), Color.White);
                        }
                    }

                    if (cont.SelectedChildren != null)
                    {
                        var childTabIndex = index + cont.Childrens.IndexOf(cont.SelectedChildren) + 1;
                        _subTabSelected.Draw(spriteBatch, GetTabBound(childTabIndex), Color.White);
                        cont.SelectedChildren.Icon?.Draw(spriteBatch, GetTabIconBound(childTabIndex, true, true), Color.White);
                    }

                    _subContainerTabSelected.Draw(spriteBatch, GetTabBound(index), Color.White);
                }
                else
                {
                    _tabSelected.Draw(spriteBatch, GetTabBound(index), Color.White);
                }

                SelectedTab.Icon?.Draw(spriteBatch, GetTabIconBound(index, true), Color.White);
            }
        }
    }
}
