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
    
	public enum TabAnchore { Top, Bottom, Right, Left, None }

    public class WidgetTabContainer : Widget
    {
        Sprite _background;
        Sprite _tab;
        Sprite _tabSelected;

        Sprite _subTab;
        Sprite _subTabSelected;
        Sprite _subContainerTabSelected;

		Rectangle _clientArea => new Padding(32 + (TabAnchore == TabAnchore.Top    ? 64 : 0),
		                                     32 + (TabAnchore == TabAnchore.Bottom ? 64 : 0),
		                                     32 + (TabAnchore == TabAnchore.Left   ? 64 : 0),
		                                     32 + (TabAnchore == TabAnchore.Right  ? 64 : 0)).Apply(UnitBound);
		
		Rectangle _clientAreaBound => new Padding((TabAnchore == TabAnchore.Top    ? 64 : 0),
                                                  (TabAnchore == TabAnchore.Bottom ? 64 : 0),
                                                  (TabAnchore == TabAnchore.Left   ? 64 : 0),
                                                  (TabAnchore == TabAnchore.Right  ? 64 : 0)).Apply(UnitBound);

		public TabAnchore TabAnchore { get; set; } = TabAnchore.Left;
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
                if (t.Content != null)
                {
                    t.Content.UnitBound = Padding.Apply(_clientArea);
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
            }

            if (SelectedTab == null && Tabs.Count > 0) SelectedTab = Tabs.First(); 
            

            if (SelectedTab != null)
            {
                SelectedTab.Content?.UpdateInternal(gameTime);
            }
        }

        private Rectangle GetTabBound(int index)
        {
			if (TabAnchore == TabAnchore.Top || TabAnchore == TabAnchore.Bottom)
			{
				return TabAnchore == TabAnchore.Top 
                    ? new Rectangle(Bound.X + Scale(16 + 76 * index), Bound.Y, Scale(128), Scale(128))
                    : new Rectangle(Bound.X + Scale(16 + 76 * index), Bound.Y + Bound.Height - Scale(128), Scale(128), Scale(128));
            
			}
			else if (TabAnchore == TabAnchore.Left || TabAnchore == TabAnchore.Right)
			{
				return TabAnchore == TabAnchore.Left 
                    ? new Rectangle(Bound.X, Bound.Y + Scale(16 + 76 * index), Scale(128), Scale(128))
                    : new Rectangle(Bound.X + Bound.Width - Scale(128), Bound.Y + Scale(16 + 76 * index), Scale(128), Scale(128));
			}

			return Rectangle.Empty;
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

            _background.Draw(spriteBatch, new Padding(-4).Apply(Scale(_clientArea)), Color.White * 0.99f);


            SelectedTab?.Content?.DrawIternal(spriteBatch, gameTime);
            

            GuiHelper.DrawBox(spriteBatch, Scale(_clientAreaBound), size);


            var onScreenIndex = 0;

            for (int i = 0; i < Tabs.Count; i++)
            {

                var tabBound = GetTabBound(onScreenIndex);
                _tab.Draw(spriteBatch, tabBound, Color.White);
                Tabs[i].Icon?.Draw(spriteBatch, GetTabIconBound(onScreenIndex, false), Color.White);
                

                onScreenIndex++;
            }

            if (SelectedTab != null)
            {
                var index = Tabs.IndexOf(SelectedTab);

                _tabSelected.Draw(spriteBatch, GetTabBound(index), Color.White);


                SelectedTab.Icon?.Draw(spriteBatch, GetTabIconBound(index, true), Color.White);
            }
        }
    }
}
