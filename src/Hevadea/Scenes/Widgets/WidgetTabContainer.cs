using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.Utils;
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

    public enum TabAnchore { Top = 0, Bottom = 1, Right = 2, Left = 3, None }

    public class WidgetTabContainer : Container
    {
        private Sprite _background;
        private Sprite _tab;
        private Sprite _tabSelected;

        private Rectangle _clientArea => new BoxElement(32 + (TabAnchore == TabAnchore.Top ? 64 : 0),
                                             32 + (TabAnchore == TabAnchore.Bottom ? 64 : 0),
                                             32 + (TabAnchore == TabAnchore.Left ? 64 : 0),
                                             32 + (TabAnchore == TabAnchore.Right ? 64 : 0)).Apply(UnitBound);

		private Rectangle _clientAreaBound => new BoxElement((TabAnchore == TabAnchore.Top ? 64 : 0),
                                                  (TabAnchore == TabAnchore.Bottom ? 64 : 0),
                                                  (TabAnchore == TabAnchore.Left ? 64 : 0),
                                                  (TabAnchore == TabAnchore.Right ? 64 : 0)).Apply(UnitBound);

        public TabAnchore TabAnchore { get; set; } = TabAnchore.Left;
        public Tab SelectedTab { get; set; }
        public List<Tab> Tabs { get; set; } = new List<Tab>();

        public WidgetTabContainer()
        {
            _background = new Sprite(Ressources.TileGui, new Point(4, 0), new Point(2, 2));
            _tab = new Sprite(Ressources.TileGui, new Point(2, 2), new Point(2, 2));
            _tabSelected = new Sprite(Ressources.TileGui, new Point(0, 2), new Point(2, 2));
        }

        public override void Layout()
        {
            base.Layout();

            foreach (var t in Tabs)
            {
                if (t.Content != null)
                {
                    t.Content.UnitBound = _clientArea;
                    t.Content.RefreshLayout();
                }
            }
        }

        private Rectangle GetTabBound(int index)
        {
            if (TabAnchore == TabAnchore.Top || TabAnchore == TabAnchore.Bottom)
            {
                float offset = UnitBound.Width / 2f - (Tabs.Count) * 72 / 2f - 12;



                return TabAnchore == TabAnchore.Top
                    ? new Rectangle(Bound.X + (int)Scale(offset + 72 * index), Bound.Y, Scale(96), Scale(96))
                    : new Rectangle(Bound.X + (int)Scale(offset + 72 * index), Bound.Y + Bound.Height - Scale(96), Scale(96), Scale(96));
            }

            if (TabAnchore == TabAnchore.Left || TabAnchore == TabAnchore.Right)
            {
                float offset = (UnitBound.Height / 2f) - (Tabs.Count * 72 / 2f) - 72 / 2f;

                return TabAnchore == TabAnchore.Left
                    ? new Rectangle(Bound.X - Scale(12), Bound.Y + (int)Scale(offset + 72 * index), Scale(128), Scale(128))
                    : new Rectangle(Bound.X + Bound.Width - Scale(128) + Scale(12), Bound.Y + (int)Scale(offset + 72 * index), Scale(128), Scale(128));
            }

            return Rectangle.Empty;
        }

        private Rectangle GetTabIconBound(int index, bool isSelected)
        {
			return GetTabBound(index).Padding(Scale(24), Scale(24), Scale(24), Scale(24));
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
            }

            if (SelectedTab == null && Tabs.Count > 0) SelectedTab = Tabs.First();
            SelectedTab?.Content?.UpdateInternal(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var size = Scale(64);
            if (SelectedTab == null && Tabs.Count > 0) SelectedTab = Tabs.First();

			_background.Draw(spriteBatch, Rise.Platform.Family == PlatformFamily.Mobile ? Bound : Scale(_clientArea).Padding(-4), Color.White * 0.99f);

            SelectedTab?.Content?.DrawIternal(spriteBatch, gameTime);

            if (Rise.Platform.Family != PlatformFamily.Mobile)
            {
                GuiHelper.DrawBox(spriteBatch, Scale(_clientAreaBound), size);
            }

            var onScreenIndex = 0;
            foreach (var t in Tabs)
            {
                var tabBound = GetTabBound(onScreenIndex);

                if (SelectedTab != t)
                {
                    if (Rise.Platform.Family != PlatformFamily.Mobile)
                        _tab.Draw(spriteBatch, tabBound, Color.White);
                }
                else 
                {
                    _tabSelected.Draw(spriteBatch, tabBound, Color.White);
                }

                t.Icon?.Draw(spriteBatch, GetTabIconBound(onScreenIndex, false), Color.White);

                onScreenIndex++;
            }

            base.Draw(spriteBatch, gameTime);
        }
    }
}