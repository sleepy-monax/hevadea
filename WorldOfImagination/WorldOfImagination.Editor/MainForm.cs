using Eto.Drawing;
using Eto.Forms;

namespace WorldOfImagination.Editor
{
    public class MainForm : Form
    {

        public MainForm()
        {
            Title = "My Cross-Platform App";
            ClientSize = new Size(200, 200);
            Content = new TableLayout
            {
                Spacing = new Size(5, 5), // space between each cell
                Padding = new Padding(10, 10, 10, 10), // space around the table's sides
                Rows =
                {
                    new TableRow(
                        new TableCell(new Label { Text = "First Column" }, true),
                        new TableCell(new Label { Text = "Second Column" }, true),
                        new Label { Text = "Third Column" }
                    ),
                    new TableRow(
                        new TextBox { Text = "Some text" },
                        new DropDown { Items = { "Item 1", "Item 2", "Item 3" } },
                        new CheckBox { Text = "A checkbox" },
                        new Button { Text = "click me"}
                    ),
                    // by default, the last row & column will get scaled. This adds a row at the end to take the extra space of the form.
                    // otherwise, the above row will get scaled and stretch the TextBox/ComboBox/CheckBox to fill the remaining height.
                    new TableRow { ScaleHeight = false }
                }
            };
        }
    }
}
