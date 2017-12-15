using Eto.Drawing;
using Eto.Forms;

namespace WorldOfImagination.Editor.Forms
{
    public class MainForm : Form
    {

        private MenuItem aboutMenu;
        private MenuItem newItemMenu;
        private MenuItem newClasse;
        
        public MainForm()
        {
            Size = new Size(700, 500);
            Shown += (sender, args) => { new DataBaseForm().ShowModal();}; 
            
            // Setup the menu bar.
            aboutMenu   = new ButtonMenuItem { Text = "About" };
            newItemMenu = new ButtonMenuItem { Text = "Item"  };
            
            aboutMenu.Click += (sender, args) => MessageBox.Show("World Of Imagination: Data Base Editor\n (c) 2017 MAKER. All Right Reserved.", "About", MessageBoxButtons.OK);
            
            Menu = new MenuBar { Items =
            {
                new ButtonMenuItem
                {
                    Text = "File",
                    Items =
                    {
                        new ButtonMenuItem() { Text = "New", Items = { newItemMenu } },
                        new ButtonMenuItem() { Text = "Save" }
                    }
                },
                new ButtonMenuItem { Text = "Help", Items = { aboutMenu } }
            }};
            
            // Setup the content.

            Content = new TabControl
            {
                Pages =
                {                    
                    new TabPage
                    {
                        Text = "Classes"
                    },
                    new TabPage
                    {
                        Text = "Skills"
                    },
                    new TabPage
                    {
                        Text = "Items"
                    },
                    new TabPage
                    {
                        Text = "General"
                    }
                }
            };
            
            // Status bar
            
            
        }
    }
}