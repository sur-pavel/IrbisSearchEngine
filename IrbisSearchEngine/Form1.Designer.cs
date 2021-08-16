
namespace IrbisSearchEngine
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.simpleSearchPage = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BriefDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatabaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullDescritption = new System.Windows.Forms.DataGridViewLinkColumn();
            this.simpleSearchButton = new System.Windows.Forms.Button();
            this.simpleSearchTextbox = new System.Windows.Forms.TextBox();
            this.extendedSearchPage = new System.Windows.Forms.TabPage();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.extendedSearchButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControl.SuspendLayout();
            this.simpleSearchPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.extendedSearchPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.simpleSearchPage);
            this.tabControl.Controls.Add(this.extendedSearchPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(912, 536);
            this.tabControl.TabIndex = 0;
            // 
            // simpleSearchPage
            // 
            this.simpleSearchPage.Controls.Add(this.dataGridView1);
            this.simpleSearchPage.Controls.Add(this.simpleSearchButton);
            this.simpleSearchPage.Controls.Add(this.simpleSearchTextbox);
            this.simpleSearchPage.Location = new System.Drawing.Point(4, 24);
            this.simpleSearchPage.Name = "simpleSearchPage";
            this.simpleSearchPage.Padding = new System.Windows.Forms.Padding(3);
            this.simpleSearchPage.Size = new System.Drawing.Size(904, 508);
            this.simpleSearchPage.TabIndex = 0;
            this.simpleSearchPage.Text = "Простой поиск";
            this.simpleSearchPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BriefDescription,
            this.DatabaseName,
            this.FullDescritption});
            this.dataGridView1.Location = new System.Drawing.Point(29, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(855, 448);
            this.dataGridView1.TabIndex = 2;
            // 
            // BriefDescription
            // 
            this.BriefDescription.DataPropertyName = "BriefDescription";
            this.BriefDescription.FillWeight = 111.9289F;
            this.BriefDescription.HeaderText = "Краткое описание";
            this.BriefDescription.Name = "BriefDescription";
            this.BriefDescription.ReadOnly = true;
            // 
            // DatabaseName
            // 
            this.DatabaseName.DataPropertyName = "DatabaseName";
            this.DatabaseName.FillWeight = 30F;
            this.DatabaseName.HeaderText = "База данных";
            this.DatabaseName.Name = "DatabaseName";
            this.DatabaseName.ReadOnly = true;
            // 
            // FullDescritption
            // 
            this.FullDescritption.FillWeight = 35F;
            this.FullDescritption.HeaderText = "Полное описание";
            this.FullDescritption.LinkColor = System.Drawing.Color.Blue;
            this.FullDescritption.Name = "FullDescritption";
            this.FullDescritption.ReadOnly = true;
            this.FullDescritption.Text = "Полное описание";
            this.FullDescritption.UseColumnTextForLinkValue = true;
            // 
            // simpleSearchButton
            // 
            this.simpleSearchButton.Location = new System.Drawing.Point(809, 27);
            this.simpleSearchButton.Name = "simpleSearchButton";
            this.simpleSearchButton.Size = new System.Drawing.Size(75, 23);
            this.simpleSearchButton.TabIndex = 1;
            this.simpleSearchButton.Text = "Поиск";
            this.simpleSearchButton.UseVisualStyleBackColor = true;
            // 
            // simpleSearchTextbox
            // 
            this.simpleSearchTextbox.Location = new System.Drawing.Point(29, 28);
            this.simpleSearchTextbox.Name = "simpleSearchTextbox";
            this.simpleSearchTextbox.Size = new System.Drawing.Size(754, 23);
            this.simpleSearchTextbox.TabIndex = 0;
            // 
            // extendedSearchPage
            // 
            this.extendedSearchPage.Controls.Add(this.textBox4);
            this.extendedSearchPage.Controls.Add(this.comboBox3);
            this.extendedSearchPage.Controls.Add(this.textBox3);
            this.extendedSearchPage.Controls.Add(this.comboBox2);
            this.extendedSearchPage.Controls.Add(this.extendedSearchButton);
            this.extendedSearchPage.Controls.Add(this.textBox2);
            this.extendedSearchPage.Controls.Add(this.comboBox1);
            this.extendedSearchPage.Location = new System.Drawing.Point(4, 24);
            this.extendedSearchPage.Name = "extendedSearchPage";
            this.extendedSearchPage.Padding = new System.Windows.Forms.Padding(3);
            this.extendedSearchPage.Size = new System.Drawing.Size(904, 508);
            this.extendedSearchPage.TabIndex = 1;
            this.extendedSearchPage.Text = "Расширенный поиск";
            this.extendedSearchPage.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(190, 121);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(1080, 23);
            this.textBox4.TabIndex = 6;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(25, 121);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(743, 23);
            this.comboBox3.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(190, 70);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(578, 23);
            this.textBox3.TabIndex = 4;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(25, 70);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 23);
            this.comboBox2.TabIndex = 3;
            // 
            // extendedSearchButton
            // 
            this.extendedSearchButton.Location = new System.Drawing.Point(806, 27);
            this.extendedSearchButton.Name = "extendedSearchButton";
            this.extendedSearchButton.Size = new System.Drawing.Size(75, 23);
            this.extendedSearchButton.TabIndex = 2;
            this.extendedSearchButton.Text = "Поиск";
            this.extendedSearchButton.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(190, 27);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(578, 23);
            this.textBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(25, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 536);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "Поиск в Ирбисе";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.simpleSearchPage.ResumeLayout(false);
            this.simpleSearchPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.extendedSearchPage.ResumeLayout(false);
            this.extendedSearchPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage simpleSearchPage;
        private System.Windows.Forms.Button simpleSearchButton;
        private System.Windows.Forms.TextBox simpleSearchTextbox;
        private System.Windows.Forms.TabPage extendedSearchPage;
        private System.Windows.Forms.Button extendedSearchButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BriefDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatabaseName;
        private System.Windows.Forms.DataGridViewLinkColumn FullDescritption;
    }
}

