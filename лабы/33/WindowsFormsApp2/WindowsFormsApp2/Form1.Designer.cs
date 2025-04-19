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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.Label кодLabel;
            System.Windows.Forms.Label fileNameLabel;
            System.Windows.Forms.Label fileSizeLabel;
            System.Windows.Forms.Label creationDateLabel;
            System.Windows.Forms.Label creationTimeLabel;
            this.filesBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.filesBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.filesDataGridView = new System.Windows.Forms.DataGridView();
            this.кодTextBox = new System.Windows.Forms.TextBox();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.fileSizeTextBox = new System.Windows.Forms.TextBox();
            this.creationDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.creationTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.filesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.database1DataSet = new WindowsFormsApp2.Database1DataSet();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filesTableAdapter = new WindowsFormsApp2.Database1DataSetTableAdapters.FilesTableAdapter();
            this.tableAdapterManager = new WindowsFormsApp2.Database1DataSetTableAdapters.TableAdapterManager();
            this.button1 = new System.Windows.Forms.Button();
            кодLabel = new System.Windows.Forms.Label();
            fileNameLabel = new System.Windows.Forms.Label();
            fileSizeLabel = new System.Windows.Forms.Label();
            creationDateLabel = new System.Windows.Forms.Label();
            creationTimeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingNavigator)).BeginInit();
            this.filesBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            this.SuspendLayout();
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
            this.filesBindingNavigator.Size = new System.Drawing.Size(824, 25);
            this.filesBindingNavigator.TabIndex = 0;
            this.filesBindingNavigator.Text = "bindingNavigator1";
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
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(43, 22);
            this.bindingNavigatorCountItem.Text = "для {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
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
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Удалить";
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
            this.filesDataGridView.AutoGenerateColumns = false;
            this.filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.filesDataGridView.DataSource = this.filesBindingSource;
            this.filesDataGridView.Location = new System.Drawing.Point(12, 40);
            this.filesDataGridView.Name = "filesDataGridView";
            this.filesDataGridView.ReadOnly = true;
            this.filesDataGridView.Size = new System.Drawing.Size(776, 220);
            this.filesDataGridView.TabIndex = 1;
            // 
            // кодLabel
            // 
            кодLabel.AutoSize = true;
            кодLabel.Location = new System.Drawing.Point(523, 359);
            кодLabel.Name = "кодLabel";
            кодLabel.Size = new System.Drawing.Size(29, 13);
            кодLabel.TabIndex = 2;
            кодLabel.Text = "Код:";
            // 
            // кодTextBox
            // 
            this.кодTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "Код", true));
            this.кодTextBox.Location = new System.Drawing.Point(604, 356);
            this.кодTextBox.Name = "кодTextBox";
            this.кодTextBox.Size = new System.Drawing.Size(200, 20);
            this.кодTextBox.TabIndex = 3;
            // 
            // fileNameLabel
            // 
            fileNameLabel.AutoSize = true;
            fileNameLabel.Location = new System.Drawing.Point(523, 385);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new System.Drawing.Size(57, 13);
            fileNameLabel.TabIndex = 4;
            fileNameLabel.Text = "File Name:";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "FileName", true));
            this.fileNameTextBox.Location = new System.Drawing.Point(604, 382);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.fileNameTextBox.TabIndex = 5;
            // 
            // fileSizeLabel
            // 
            fileSizeLabel.AutoSize = true;
            fileSizeLabel.Location = new System.Drawing.Point(523, 411);
            fileSizeLabel.Name = "fileSizeLabel";
            fileSizeLabel.Size = new System.Drawing.Size(49, 13);
            fileSizeLabel.TabIndex = 6;
            fileSizeLabel.Text = "File Size:";
            // 
            // fileSizeTextBox
            // 
            this.fileSizeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "FileSize", true));
            this.fileSizeTextBox.Location = new System.Drawing.Point(604, 408);
            this.fileSizeTextBox.Name = "fileSizeTextBox";
            this.fileSizeTextBox.Size = new System.Drawing.Size(200, 20);
            this.fileSizeTextBox.TabIndex = 7;
            // 
            // creationDateLabel
            // 
            creationDateLabel.AutoSize = true;
            creationDateLabel.Location = new System.Drawing.Point(523, 438);
            creationDateLabel.Name = "creationDateLabel";
            creationDateLabel.Size = new System.Drawing.Size(75, 13);
            creationDateLabel.TabIndex = 8;
            creationDateLabel.Text = "Creation Date:";
            // 
            // creationDateDateTimePicker
            // 
            this.creationDateDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.filesBindingSource, "CreationDate", true));
            this.creationDateDateTimePicker.Location = new System.Drawing.Point(604, 434);
            this.creationDateDateTimePicker.Name = "creationDateDateTimePicker";
            this.creationDateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.creationDateDateTimePicker.TabIndex = 9;
            // 
            // creationTimeLabel
            // 
            creationTimeLabel.AutoSize = true;
            creationTimeLabel.Location = new System.Drawing.Point(523, 464);
            creationTimeLabel.Name = "creationTimeLabel";
            creationTimeLabel.Size = new System.Drawing.Size(75, 13);
            creationTimeLabel.TabIndex = 10;
            creationTimeLabel.Text = "Creation Time:";
            // 
            // creationTimeDateTimePicker
            // 
            this.creationTimeDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.filesBindingSource, "CreationTime", true));
            this.creationTimeDateTimePicker.Location = new System.Drawing.Point(604, 460);
            this.creationTimeDateTimePicker.Name = "creationTimeDateTimePicker";
            this.creationTimeDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.creationTimeDateTimePicker.TabIndex = 11;
            // Установите свойство Format на Time
            creationTimeDateTimePicker.Format = DateTimePickerFormat.Time;

            // Установите свойство ShowUpDown в true для удобного выбора времени
            creationTimeDateTimePicker.ShowUpDown = true;
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Код";
            this.dataGridViewTextBoxColumn1.HeaderText = "Код";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FileName";
            this.dataGridViewTextBoxColumn2.HeaderText = "FileName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FileSize";
            this.dataGridViewTextBoxColumn3.HeaderText = "FileSize";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CreationDate";
            this.dataGridViewTextBoxColumn4.HeaderText = "CreationDate";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CreationTime";
            this.dataGridViewTextBoxColumn5.HeaderText = "CreationTime";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
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
            this.button1.Location = new System.Drawing.Point(80, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 123);
            this.button1.TabIndex = 12;
            this.button1.Text = "Нова сверх";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 500);
            this.Controls.Add(this.button1);
            this.Controls.Add(кодLabel);
            this.Controls.Add(this.кодTextBox);
            this.Controls.Add(fileNameLabel);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(fileSizeLabel);
            this.Controls.Add(this.fileSizeTextBox);
            this.Controls.Add(creationDateLabel);
            this.Controls.Add(this.creationDateDateTimePicker);
            this.Controls.Add(creationTimeLabel);
            this.Controls.Add(this.creationTimeDateTimePicker);
            this.Controls.Add(this.filesDataGridView);
            this.Controls.Add(this.filesBindingNavigator);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingNavigator)).EndInit();
            this.filesBindingNavigator.ResumeLayout(false);
            this.filesBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TextBox кодTextBox;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.TextBox fileSizeTextBox;
        private System.Windows.Forms.DateTimePicker creationDateDateTimePicker;
        private System.Windows.Forms.DateTimePicker creationTimeDateTimePicker;
        private System.Windows.Forms.Button button1;
    }
}

