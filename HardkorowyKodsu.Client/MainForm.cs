using HardkorowyKodsu.Server.Models;
using System.Net.Http.Json;

namespace HardkorowyKodsu.Client
{
    public partial class MainForm : Form
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") };

        public MainForm()
        {
            InitializeComponent();
            LoadTables();
            ConfigureUI();
        }

        private void ConfigureUI()
        {
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            treeViewDatabase.BackColor = Color.FromArgb(37, 37, 38);
            treeViewDatabase.ForeColor = Color.White;
            treeViewDatabase.BorderStyle = BorderStyle.None;

            dataGridViewColumns.BackgroundColor = Color.FromArgb(45, 45, 48);
            dataGridViewColumns.ForeColor = Color.White;
            dataGridViewColumns.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dataGridViewColumns.DefaultCellStyle.ForeColor = Color.White;
            dataGridViewColumns.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 38);
            dataGridViewColumns.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewColumns.BorderStyle = BorderStyle.None;

            statusStrip.BackColor = Color.FromArgb(37, 37, 38);
            statusStrip.ForeColor = Color.White;
            toolStripStatusLabel.Text = "Gotowy";
        }

        private async void LoadTables()
        {
            try
            {
                toolStripStatusLabel.Text = "Ładowanie tabel...";
                var tables = await _httpClient.GetFromJsonAsync<List<TableInfo>>("database/tables");
                var views = await _httpClient.GetFromJsonAsync<List<TableInfo>>("database/views");

                treeViewDatabase.Nodes.Clear();
                var tablesNode = treeViewDatabase.Nodes.Add("Tabele");
                var viewsNode = treeViewDatabase.Nodes.Add("Widoki");

                if (tables != null)
                {
                    foreach (var table in tables)
                    {
                        tablesNode.Nodes.Add(table.TableName);
                    }
                }

                if (views != null)
                {
                    foreach (var view in views)
                    {
                        viewsNode.Nodes.Add(view.TableName);
                    }
                }

                treeViewDatabase.ExpandAll();
                toolStripStatusLabel.Text = "Gotowy";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Błąd podczas ładowania tabel";
            }
        }

        private async void treeViewDatabase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node?.TreeView == null) return;

            var tableName = e.Node.Text;

            try
            {
                toolStripStatusLabel.Text = $"Ładowanie kolumn dla {tableName}...";
                var columns = await _httpClient.GetFromJsonAsync<List<ColumnInfo>>($"database/columns/{tableName}");
                dataGridViewColumns.DataSource = columns;
                toolStripStatusLabel.Text = "Gotowy";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Błąd podczas ładowania kolumn";
            }
        }
    }
}
