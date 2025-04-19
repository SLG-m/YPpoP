using System.Windows.Forms;

namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label кодLabel;
            System.Windows.Forms.Label fileNameLabel;
            System.Windows.Forms.Label fileSizeLabel;
            System.Windows.Forms.Label creationDateLabel;
            System.Windows.Forms.Label creationTimeLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.filesBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.filesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.database1DataSet = new WindowsFormsApp2.Database1DataSet();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.filesBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.filesDataGridView = new System.Windows.Forms.DataGridView();
            this.кодTextBox = new System.Windows.Forms.TextBox();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.fileSizeTextBox = new System.Windows.Forms.TextBox();
            this.creationDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.creationTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.filesTableAdapter = new WindowsFormsApp2.Database1DataSetTableAdapters.FilesTableAdapter();
            this.tableAdapterManager = new WindowsFormsApp2.Database1DataSetTableAdapters.TableAdapterManager();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            кодLabel = new System.Windows.Forms.Label();
            fileNameLabel = new System.Windows.Forms.Label();
            fileSizeLabel = new System.Windows.Forms.Label();
            creationDateLabel = new System.Windows.Forms.Label();
            creationTimeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingNavigator)).BeginInit();
            this.filesBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // кодLabel
            // 
            кодLabel.AutoSize = true;
            кодLabel.Location = new System.Drawing.Point(26, 26);
            кодLabel.Name = "кодLabel";
            кодLabel.Size = new System.Drawing.Size(29, 13);
            кодLabel.TabIndex = 2;
            кодLabel.Text = "Код:";
            // 
            // fileNameLabel
            // 
            fileNameLabel.AutoSize = true;
            fileNameLabel.Location = new System.Drawing.Point(26, 52);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new System.Drawing.Size(57, 13);
            fileNameLabel.TabIndex = 4;
            fileNameLabel.Text = "File Name:";
            // 
            // fileSizeLabel
            // 
            fileSizeLabel.AutoSize = true;
            fileSizeLabel.Location = new System.Drawing.Point(26, 78);
            fileSizeLabel.Name = "fileSizeLabel";
            fileSizeLabel.Size = new System.Drawing.Size(49, 13);
            fileSizeLabel.TabIndex = 6;
            fileSizeLabel.Text = "File Size:";
            // 
            // creationDateLabel
            // 
            creationDateLabel.AutoSize = true;
            creationDateLabel.Location = new System.Drawing.Point(26, 105);
            creationDateLabel.Name = "creationDateLabel";
            creationDateLabel.Size = new System.Drawing.Size(75, 13);
            creationDateLabel.TabIndex = 8;
            creationDateLabel.Text = "Creation Date:";
            // 
            // creationTimeLabel
            // 
            creationTimeLabel.AutoSize = true;
            creationTimeLabel.Location = new System.Drawing.Point(26, 131);
            creationTimeLabel.Name = "creationTimeLabel";
            creationTimeLabel.Size = new System.Drawing.Size(75, 13);
            creationTimeLabel.TabIndex = 10;
            creationTimeLabel.Text = "Creation Time:";
            // 
            // filesBindingNavigator
            // 
            this.filesBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.filesBindingNavigator.BindingSource = this.filesBindingSource;
            this.filesBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.filesBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.filesBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.filesBindingNavigatorSaveItem});
            this.filesBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.filesBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.filesBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.filesBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.filesBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.filesBindingNavigator.Name = "filesBindingNavigator";
            this.filesBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.filesBindingNavigator.Size = new System.Drawing.Size(880, 25);
            this.filesBindingNavigator.TabIndex = 0;
            this.filesBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Добавить";
            // 
            // filesBindingSource
            // 
            this.filesBindingSource.DataMember = "Files";
            this.filesBindingSource.DataSource = this.database1DataSet;
            // 
            // database1DataSet
            // 
            this.database1DataSet.DataSetName = "Database1DataSet";
            this.database1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(43, 22);
            this.bindingNavigatorCountItem.Text = "для {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Удалить";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // filesBindingNavigatorSaveItem
            // 
            this.filesBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.filesBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("filesBindingNavigatorSaveItem.Image")));
            this.filesBindingNavigatorSaveItem.Name = "filesBindingNavigatorSaveItem";
            this.filesBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.filesBindingNavigatorSaveItem.Text = "Сохранить данные";
            this.filesBindingNavigatorSaveItem.Click += new System.EventHandler(this.filesBindingNavigatorSaveItem_Click);
            // 
            // filesDataGridView
            // 
            this.filesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesDataGridView.AutoGenerateColumns = false;
            this.filesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.filesDataGridView.DataSource = this.filesBindingSource;
            this.filesDataGridView.Location = new System.Drawing.Point(12, 28);
            this.filesDataGridView.Name = "filesDataGridView";
            this.filesDataGridView.ReadOnly = true;
            this.filesDataGridView.Size = new System.Drawing.Size(856, 323);
            this.filesDataGridView.TabIndex = 1;
            // 
            // кодTextBox
            // 
            this.кодTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "Код", true));
            this.кодTextBox.Location = new System.Drawing.Point(107, 23);
            this.кодTextBox.Name = "кодTextBox";
            this.кодTextBox.Size = new System.Drawing.Size(200, 20);
            this.кодTextBox.TabIndex = 3;
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "FileName", true));
            this.fileNameTextBox.Location = new System.Drawing.Point(107, 49);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.fileNameTextBox.TabIndex = 5;
            // 
            // fileSizeTextBox
            // 
            this.fileSizeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "FileSize", true));
            this.fileSizeTextBox.Location = new System.Drawing.Point(107, 75);
            this.fileSizeTextBox.Name = "fileSizeTextBox";
            this.fileSizeTextBox.Size = new System.Drawing.Size(200, 20);
            this.fileSizeTextBox.TabIndex = 7;
            // 
            // creationDateDateTimePicker
            // 
            this.creationDateDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.filesBindingSource, "CreationDate", true));
            this.creationDateDateTimePicker.Location = new System.Drawing.Point(107, 101);
            this.creationDateDateTimePicker.Name = "creationDateDateTimePicker";
            this.creationDateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.creationDateDateTimePicker.TabIndex = 9;
            // 
            // creationTimeDateTimePicker
            // 
            this.creationTimeDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.filesBindingSource, "CreationTime", true));
            this.creationTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.creationTimeDateTimePicker.Location = new System.Drawing.Point(107, 127);
            this.creationTimeDateTimePicker.Name = "creationTimeDateTimePicker";
            this.creationTimeDateTimePicker.ShowUpDown = true;
            this.creationTimeDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.creationTimeDateTimePicker.TabIndex = 11;
            // 
            // filesTableAdapter
            // 
            this.filesTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.FilesTableAdapter = this.filesTableAdapter;
            this.tableAdapterManager.UpdateOrder = WindowsFormsApp2.Database1DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 123);
            this.button1.TabIndex = 12;
            this.button1.Text = "Нова сверх";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Код";
            this.dataGridViewTextBoxColumn1.HeaderText = "Код";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FileName";
            this.dataGridViewTextBoxColumn2.HeaderText = "FileName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FileSize";
            this.dataGridViewTextBoxColumn3.HeaderText = "FileSize";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CreationDate";
            dataGridViewCellStyle9.Format = "d";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn4.HeaderText = "CreationDate";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CreationTime";
            dataGridViewCellStyle10.Format = "T";
            dataGridViewCellStyle10.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn5.HeaderText = "CreationTime";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(кодLabel);
            this.groupBox1.Controls.Add(this.creationTimeDateTimePicker);
            this.groupBox1.Controls.Add(creationTimeLabel);
            this.groupBox1.Controls.Add(this.кодTextBox);
            this.groupBox1.Controls.Add(this.creationDateDateTimePicker);
            this.groupBox1.Controls.Add(fileNameLabel);
            this.groupBox1.Controls.Add(creationDateLabel);
            this.groupBox1.Controls.Add(this.fileNameTextBox);
            this.groupBox1.Controls.Add(this.fileSizeTextBox);
            this.groupBox1.Controls.Add(fileSizeLabel);
            this.groupBox1.Location = new System.Drawing.Point(527, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 167);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(316, 487);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 39);
            this.button2.TabIndex = 14;
            this.button2.Text = "Обновить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 538);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.filesDataGridView);
            this.Controls.Add(this.filesBindingNavigator);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingNavigator)).EndInit();
            this.filesBindingNavigator.ResumeLayout(false);
            this.filesBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Database1DataSet database1DataSet;
        private System.Windows.Forms.BindingSource filesBindingSource;
        private Database1DataSetTableAdapters.FilesTableAdapter filesTableAdapter;
        private Database1DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator filesBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton filesBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView filesDataGridView;
        private System.Windows.Forms.TextBox кодTextBox;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.TextBox fileSizeTextBox;
        private System.Windows.Forms.DateTimePicker creationDateDateTimePicker;
        private System.Windows.Forms.DateTimePicker creationTimeDateTimePicker;
        private System.Windows.Forms.Button button1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private GroupBox groupBox1;
        private Button button2;
    }
}

