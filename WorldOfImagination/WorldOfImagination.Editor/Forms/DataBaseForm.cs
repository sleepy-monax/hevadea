using System;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using WorldOfImagination.Data;
using WorldOfImagination.Json;

namespace WorldOfImagination.Editor.Forms
{
    public class DataBaseForm : Dialog
    {
        public GameDataBase Database;
        public DataBaseForm()
        {
            Content = new TableLayout
            {
                Padding = new Padding(10, 10, 10, 10),
                Spacing = new Size(5, 5),
                Rows =
                {
                    new Label{Text = "DataBase loader", Font = new Font(SystemFont.Bold, 18)},
                    new Splitter(),
                    new Button((sender, e) =>
                    {
                        var openDialog = new OpenFileDialog();
                        if (openDialog.ShowDialog(this.Parent) == DialogResult.Ok)
                        {
                            try
                            {
                                Database = File.ReadAllText(openDialog.FileName).FromJson<GameDataBase>();
                                Close();
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show($"Something go wrong!");
                                Console.WriteLine(exception);
                                throw;
                            }
                        }
                    }){Text = "Import Existing DataBase"},
                    new Button((sender, e) => {Database = new GameDataBase();}){Text = "Create a new DataBase"},
                    new Splitter(),
                    new Button((sender, e) => {Application.Instance.Quit();}){Text = "Close the editor"}
                }
            };
        }
    }
}