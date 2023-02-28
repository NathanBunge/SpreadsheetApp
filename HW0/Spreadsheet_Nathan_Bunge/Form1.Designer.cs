namespace Spreadsheet_Nathan_Bunge
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spreadsheetGrid = new System.Windows.Forms.DataGridView();
            this.colA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spreadsheetGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadsheetGrid
            // 
            this.spreadsheetGrid.AllowUserToAddRows = false;
            this.spreadsheetGrid.AllowUserToDeleteRows = false;
            this.spreadsheetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.spreadsheetGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colA,
            this.colB,
            this.colC,
            this.colD});
            this.spreadsheetGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetGrid.Location = new System.Drawing.Point(0, 0);
            this.spreadsheetGrid.Name = "spreadsheetGrid";
            this.spreadsheetGrid.RowHeadersWidth = 62;
            this.spreadsheetGrid.Size = new System.Drawing.Size(800, 450);
            this.spreadsheetGrid.TabIndex = 0;
            // 
            // colA
            // 
            this.colA.HeaderText = "A";
            this.colA.MinimumWidth = 8;
            this.colA.Name = "colA";
            this.colA.Width = 150;
            // 
            // colB
            // 
            this.colB.HeaderText = "B";
            this.colB.MinimumWidth = 8;
            this.colB.Name = "colB";
            this.colB.Width = 150;
            // 
            // colC
            // 
            this.colC.HeaderText = "C";
            this.colC.MinimumWidth = 8;
            this.colC.Name = "colC";
            this.colC.Width = 150;
            // 
            // colD
            // 
            this.colD.HeaderText = "D";
            this.colD.MinimumWidth = 8;
            this.colD.Name = "colD";
            this.colD.Width = 150;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "Run Demo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.spreadsheetGrid);
            this.Name = "Form1";
            this.Text = "Cpts 321 Nathan Bung 11658843";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spreadsheetGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView spreadsheetGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colD;
        private System.Windows.Forms.Button button1;
    }
}

