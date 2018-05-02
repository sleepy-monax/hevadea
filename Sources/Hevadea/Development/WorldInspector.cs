using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects.Entities;
using Hevadea.Worlds;
using System;
using System.Windows.Forms;

namespace Hevadea.Development
{
    public class WorldInspector : Form
    {
        public WorldInspector(World world)
        {
            Text = "World Inspector";

            TabControl tabControl = new TabControl()
            {
                Dock = DockStyle.Fill
            };

            foreach (var l in world.Levels)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(l.Width, l.Height);

                for (int x = 0; x < l.Width; x++)
                {
                    for (int y = 0; y < l.Height; y++)
                    {
                        var c = l.GetTile(x, y).MiniMapColor;
                        bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B));
                    }
                }

                TabPage page = new TabPage(l.Name);
                page.Controls.Add(new PictureBox() { SizeMode = PictureBoxSizeMode.Zoom, Image = bmp, Dock = DockStyle.Fill });
                tabControl.TabPages.Add(page);
            }

            Panel entityPanel = new Panel() { Width = 320, Dock = DockStyle.Left };
            Button jsonButton = new Button() { Text = "Generate Json", Dock = DockStyle.Bottom };
            Button playerButon = new Button() { Text = "Player", Dock = DockStyle.Top };

            NumericUpDown UeidSelector = new NumericUpDown() { Dock = DockStyle.Top, Maximum = 0xffffffff};
            PropertyGrid propertyGrid = new PropertyGrid() { Dock = DockStyle.Fill };

            playerButon.Click += (sender, e) => { UeidSelector.Value = world.Game.MainPlayer.Ueid; };
            jsonButton.Click += (sender, e) =>
            {
                if (propertyGrid.SelectedObject is Entity entity)
                {
                    
                    var frm = new Form();
                    var txt = new TextBox() { Text = entity.Save().ToJson(), Multiline = true, Dock = DockStyle.Fill };
                    frm.Controls.Add(txt);
                    frm.StartPosition = FormStartPosition.CenterParent;

                    frm.ShowDialog();
                }
            };

            UeidSelector.ValueChanged += (sender, e) => 
            {
                propertyGrid.SelectedObject = world.GetEntityByUeid(Decimal.ToInt32(UeidSelector.Value));
            };

            entityPanel.Controls.Add(propertyGrid);
            entityPanel.Controls.Add(UeidSelector);
            entityPanel.Controls.Add(playerButon);
            entityPanel.Controls.Add(jsonButton);
            Controls.Add(tabControl);
            Controls.Add(entityPanel);
        }
    }
}
