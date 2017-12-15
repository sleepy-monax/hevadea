using System;
using Eto.Drawing;
using Eto.Forms;
using WorldOfImagination.Data;

namespace WorldOfImagination.Editor.Forms
{
    public class ItemEditorForm : Dialog<Item>
    {
        private TextBox  nameTextBox;
        private TextArea descriptionTextBox;
        private TextArea notesTextBox;

        private NumericUpDown costNumericUpDown;
        private NumericUpDown weightNumericUpDown;
        private NumericUpDown stackSizeNumericUpDown;
        
        private TextBox  iconNameTextBox;

        private Button saveButton;
        private Button closeButton;

        private Item Item;
        
        public ItemEditorForm(Item item)
        {
            Item = item;
            
            Title = $"Item Editor: {item.Name}";
            Maximizable = false;
            Size = new Size(320, 320);

            nameTextBox        = new TextBox {Text = item.Name};
            descriptionTextBox = new TextArea{Text = item.Description};
            notesTextBox       = new TextArea{Text = item.Notes};
            
            costNumericUpDown      = new NumericUpDown{Value = item.Cost};
            weightNumericUpDown    = new NumericUpDown{Value = item.Weight};
            stackSizeNumericUpDown = new NumericUpDown{Value = item.StackSize};

            iconNameTextBox = new TextBox{Text = item.IconName};

            saveButton = new Button{Text = "Save"};
            saveButton.Click += SaveButtonOnClick ;
            
            closeButton = new Button{Text = "Close"};
            closeButton.Click += CloseButtonOnClick;
            
            Content = new TableLayout
            {
                Padding = new Padding(10, 10, 10, 10),
                Spacing = new Size(5, 5),
                Rows =
                {
                    new TableRow(new Label{Text = "Item", Font = new Font(SystemFont.Bold, 18)} /*Todo item icon*/),
                    
                    new TableRow(new Label{Text = "Name"}, nameTextBox),
                    new TableRow(new Label{Text = "Description"}, descriptionTextBox),
                    new TableRow(new Label{Text = "Notes"}, notesTextBox),
                    
                    new Splitter(),
                    
                    new TableRow(new Label{Text = "Cost"}, costNumericUpDown),
                    new TableRow(new Label{Text = "Weight"}, weightNumericUpDown),
                    new TableRow(new Label{Text = "StackSize"}, stackSizeNumericUpDown),
                    
                    new Splitter(),
                    
                    new TableRow(new Label{Text = "IconName"}, iconNameTextBox),
                    
                    new Splitter(),
                    
                    new TableRow(closeButton, saveButton)
                }
            };

            Resizable = false;
        }

        private void CloseButtonOnClick(object sender, EventArgs eventArgs)
        {
            
            Close(Item);
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            Item = new Item()
            {
                Name = nameTextBox.Text,
                Description = descriptionTextBox.Text,
                Notes = notesTextBox.Text,
                
                Cost = (int)costNumericUpDown.Value,
                StackSize = (int)stackSizeNumericUpDown.Value,
                Weight = (float)weightNumericUpDown.Value,
                
                IconName = iconNameTextBox.Text
            };
            
            Close(Item);
            
        }
    }
}