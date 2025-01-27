namespace HardkorowyKodsu.Client
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TreeView treeViewDatabase;
        private System.Windows.Forms.DataGridView dataGridViewColumns;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.dataGridViewColumns = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // treeViewDatabase
            this.treeViewDatabase.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeViewDatabase.Location = new System.Drawing.Point(0, 0);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(200, 450);
            this.treeViewDatabase.TabIndex = 0;
            this.treeViewDatabase.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDatabase_AfterSelect);

            // dataGridViewColumns
            this.dataGridViewColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewColumns.Location = new System.Drawing.Point(200, 0);
            this.dataGridViewColumns.Name = "dataGridViewColumns";
            this.dataGridViewColumns.Size = new System.Drawing.Size(600, 450);
            this.dataGridViewColumns.TabIndex = 1;

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";

            // toolStripStatusLabel
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLabel.Text = "Gotowy";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewColumns);
            this.Controls.Add(this.treeViewDatabase);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.Text = "HardkorowyKodsu - Przeglądarka bazy danych";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
