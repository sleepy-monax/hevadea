using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Scenes.Widgets
{
    public class Tab
    {
        public bool Transparent { get; set; } = false;
        public Sprite Icon { get; set; }
        public Layout Content { get; set; }
        public Color Color { get; set; } = Color.White;
    }

    public enum TabAnchore
    {
        Top = 0,
        Bottom = 1,
        Right = 2,
        Left = 3,
        None
    }

    public class WidgetTabContainer : Widget
    {
        private Sprite _background;
        private Sprite _tab;
        private Sprite _tabSelected;

        private Rectangle _clientArea => UnitBound.Padding(9 + (TabAnchore == TabAnchore.Top ? 96 : 0),
            9 + (TabAnchore == TabAnchore.Bottom ? 96 : 0),
            9 + (TabAnchore == TabAnchore.Left ? 96 : 0),
            9 + (TabAnchore == TabAnchore.Right ? 96 : 0));

        private Rectangle _clientAreaBound => new Spacing(TabAnchore == TabAnchore.Top ? 96 : 0,
            TabAnchore == TabAnchore.Bottom ? 96 : 0,
            TabAnchore == TabAnchore.Left ? 96 : 0,
            TabAnchore == TabAnchore.Right ? 96 : 0).Apply(UnitBound);

        public TabAnchore TabAnchore { get; set; } = TabAnchore.Left;
        public Tab SelectedTab { get; set; }
        public List<Tab> Tabs { get; set; } = new List<Tab>();

        public WidgetTabContainer()
        {
            _background = new Sprite(Resources.TileGui, new Point(4, 0), new Point(2, 2));
            _tab = new Sprite(Resources.TileGui, new Point(2, 2), new Point(2, 2));
            _tabSelected = new Sprite(Resources.TileGui, new Point(0, 2), new Point(2, 2));
        }

        public override void RefreshLayout()
        {
            foreach (var t in Tabs)
                if (t.Content != null)
                {
                    t.Content.UnitBound = _clientArea;
                    t.Content.RefreshLayout();
                }
        }

        private Rectangle GetTabBound(int index)
        {
            if (TabAnchore == TabAnchore.Top || TabAnchore == TabAnchore.Bottom)
            {
                var offset = UnitBound.Width / 2f - Tabs.Count * 72 / 2f - 12;

                return TabAnchore == TabAnchore.Top
                    ? new Rectangle(Bound.X + (int) Scale(offset + 72 * index), Bound.Y, Scale(96), Scale(96))
                    : new Rectangle(Bound.X + (int) Scale(offset + 72 * index), Bound.Y + Bound.Height - Scale(96),
                        Scale(96), Scale(96));
            }

            if (TabAnchore == TabAnchore.Left || TabAnchore == TabAnchore.Right)
            {
                var offset = UnitBound.Height / 2f - Tabs.Count * 96 / 2f;

                return TabAnchore == TabAnchore.Left
                    ? new Rectangle(Bound.X, Bound.Y + (int) Scale(offset + 96 * index), Scale(96), Scale(96))
                    : new Rectangle(Bound.X + Bound.Width - Scale(128), Bound.Y + (int) Scale(offset + 96 * index),
                        Scale(96), Scale(96));
            }

            return Rectangle.Empty;
        }

        private Rectangle GetTabIconBound(int index, bool isSelected)
        {
            return GetTabBound(index).Padding(Scale(24), Scale(24), Scale(24), Scale(24));
        }

        public override void Update(GameTime gameTime)
        {
            var onScreenIndex = 0;
            for (var i = 0; i < Tabs.Count; i++)
            {
                if (Rise.Pointing.AreaClick(GetTabIconBound(onScreenIndex, SelectedTab == Tabs[i])))
                    SelectedTab = Tabs[i];

                onScreenIndex++;
            }

            if (Rise.Input.KeyTyped(Keys.Tab))
            {
                var i = Tabs.IndexOf(SelectedTab);
                SelectedTab = Tabs[(i + 1) % Tabs.Count];
            }

            if (SelectedTab == null && Tabs.Count > 0) SelectedTab = Tabs.First();
            SelectedTab?.Content?.UpdateInternal(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var size = Scale(16 * 3);
            if (SelectedTab == null && Tabs.Count > 0) SelectedTab = Tabs.First();

            if (!(SelectedTab?.Transparent ?? false))
                _background.Draw(spriteBatch, Scale(_clientArea), Color.White * 0.99f);

            SelectedTab?.Content?.DrawIternal(spriteBatch, gameTime);

            if (!(SelectedTab?.Transparent ?? false))
                GuiHelper.DrawBox(spriteBatch, Scale(_clientAreaBound), size);

            var onScreenIndex = 0;
            foreach (var t in Tabs)
            {
                var tabBound = GetTabBound(onScreenIndex);

                if (SelectedTab != t)
                {
                    if (Rise.Platform.Family != PlatformFamily.Mobile)
                        _tab.Draw(spriteBatch, tabBound, t.Color);
                    t.Icon?.Draw(spriteBatch, GetTabIconBound(onScreenIndex, false), Color.White);
                }

                onScreenIndex++;
            }

            if (SelectedTab != null)
            {
                var index = Tabs.IndexOf(SelectedTab);
                var tabBound = GetTabBound(index);

                _tabSelected.Draw(spriteBatch, tabBound, SelectedTab.Color);
                SelectedTab.Icon?.Draw(spriteBatch, GetTabIconBound(index, false), Color.White);
            }

            base.Draw(spriteBatch, gameTime);
        }
    }
}