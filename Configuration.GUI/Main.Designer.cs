using System;
using TI.Configuration.Logic;

namespace Configuration.GUI
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sQLAppConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sQLAppConfigBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
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
            this.sQLAppConfigBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.sQLAppConfigListBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cloneConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSaveAll = new System.Windows.Forms.Button();
            this.buttonAddNewSetting = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonCloneToMode = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.buttonToggle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sQLAppConfigBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sQLAppConfigBindingNavigator)).BeginInit();
            this.sQLAppConfigBindingNavigator.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "Mode";
            this.dataGridViewComboBoxColumn1.HeaderText = "Mode";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.ReadOnly = true;
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // sQLAppConfigBindingSource
            // 
            this.sQLAppConfigBindingSource.AllowNew = true;
            this.sQLAppConfigBindingSource.DataSource = typeof(Configuration.SQL.SQLAppConfig);
            this.sQLAppConfigBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.sQLAppConfigBindingSource_AddingNew);
            this.sQLAppConfigBindingSource.CurrentChanged += new System.EventHandler(this.sQLAppConfigBindingSource_CurrentChanged);
            // 
            // sQLAppConfigBindingNavigator
            // 
            this.sQLAppConfigBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.sQLAppConfigBindingNavigator.BindingSource = this.sQLAppConfigBindingSource;
            this.sQLAppConfigBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.sQLAppConfigBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.sQLAppConfigBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.sQLAppConfigBindingNavigatorSaveItem});
            this.sQLAppConfigBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.sQLAppConfigBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.sQLAppConfigBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.sQLAppConfigBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.sQLAppConfigBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.sQLAppConfigBindingNavigator.Name = "sQLAppConfigBindingNavigator";
            this.sQLAppConfigBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.sQLAppConfigBindingNavigator.Size = new System.Drawing.Size(1024, 25);
            this.sQLAppConfigBindingNavigator.TabIndex = 0;
            this.sQLAppConfigBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
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
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // sQLAppConfigBindingNavigatorSaveItem
            // 
            this.sQLAppConfigBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sQLAppConfigBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("sQLAppConfigBindingNavigatorSaveItem.Image")));
            this.sQLAppConfigBindingNavigatorSaveItem.Name = "sQLAppConfigBindingNavigatorSaveItem";
            this.sQLAppConfigBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.sQLAppConfigBindingNavigatorSaveItem.Text = "Save Data";
            this.sQLAppConfigBindingNavigatorSaveItem.Click += new System.EventHandler(this.sQLAppConfigBindingNavigatorSaveItem_Click_1);
            // 
            // sQLAppConfigListBox
            // 
            this.sQLAppConfigListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sQLAppConfigListBox.ContextMenuStrip = this.contextMenuStrip1;
            this.sQLAppConfigListBox.DataSource = this.sQLAppConfigBindingSource;
            this.sQLAppConfigListBox.DisplayMember = "Name";
            this.sQLAppConfigListBox.FormattingEnabled = true;
            this.sQLAppConfigListBox.Location = new System.Drawing.Point(12, 28);
            this.sQLAppConfigListBox.Name = "sQLAppConfigListBox";
            this.sQLAppConfigListBox.Size = new System.Drawing.Size(233, 589);
            this.sQLAppConfigListBox.TabIndex = 1;
            this.sQLAppConfigListBox.ValueMember = "Created";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cloneConfigToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 26);
            // 
            // cloneConfigToolStripMenuItem
            // 
            this.cloneConfigToolStripMenuItem.Name = "cloneConfigToolStripMenuItem";
            this.cloneConfigToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cloneConfigToolStripMenuItem.Text = "Clone Config";
            this.cloneConfigToolStripMenuItem.Click += new System.EventHandler(this.cloneConfigToolStripMenuItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(251, 62);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(761, 555);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // buttonSaveAll
            // 
            this.buttonSaveAll.Location = new System.Drawing.Point(332, 33);
            this.buttonSaveAll.Name = "buttonSaveAll";
            this.buttonSaveAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAll.TabIndex = 3;
            this.buttonSaveAll.Text = "SAVE";
            this.buttonSaveAll.UseVisualStyleBackColor = true;
            this.buttonSaveAll.Click += new System.EventHandler(this.buttonSaveAll_Click);
            // 
            // buttonAddNewSetting
            // 
            this.buttonAddNewSetting.Location = new System.Drawing.Point(251, 33);
            this.buttonAddNewSetting.Name = "buttonAddNewSetting";
            this.buttonAddNewSetting.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNewSetting.TabIndex = 4;
            this.buttonAddNewSetting.Text = "Add Setting";
            this.buttonAddNewSetting.UseVisualStyleBackColor = true;
            this.buttonAddNewSetting.Click += new System.EventHandler(this.buttonAddNewSetting_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "All"});
            this.comboBox1.Location = new System.Drawing.Point(519, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonCloneToMode
            // 
            this.buttonCloneToMode.Enabled = false;
            this.buttonCloneToMode.Location = new System.Drawing.Point(646, 33);
            this.buttonCloneToMode.Name = "buttonCloneToMode";
            this.buttonCloneToMode.Size = new System.Drawing.Size(111, 23);
            this.buttonCloneToMode.TabIndex = 7;
            this.buttonCloneToMode.Text = "CLONE TO >>";
            this.buttonCloneToMode.UseVisualStyleBackColor = true;
            this.buttonCloneToMode.Click += new System.EventHandler(this.buttonCloneToMode_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(763, 35);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // buttonToggle
            // 
            this.buttonToggle.Location = new System.Drawing.Point(422, 33);
            this.buttonToggle.Name = "buttonToggle";
            this.buttonToggle.Size = new System.Drawing.Size(75, 23);
            this.buttonToggle.TabIndex = 9;
            this.buttonToggle.Text = "TOGGLE";
            this.buttonToggle.UseVisualStyleBackColor = true;
            this.buttonToggle.Click += new System.EventHandler(this.buttonToggle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(892, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "CODE:";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(895, 35);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ReadOnly = true;
            this.textBoxCode.Size = new System.Drawing.Size(117, 20);
            this.textBoxCode.TabIndex = 11;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 629);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonToggle);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.buttonCloneToMode);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonAddNewSetting);
            this.Controls.Add(this.buttonSaveAll);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.sQLAppConfigListBox);
            this.Controls.Add(this.sQLAppConfigBindingNavigator);
            this.Name = "Main";
            this.Text = "Configuration Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sQLAppConfigBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sQLAppConfigBindingNavigator)).EndInit();
            this.sQLAppConfigBindingNavigator.ResumeLayout(false);
            this.sQLAppConfigBindingNavigator.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sQLAppConfigBindingSource;
        private System.Windows.Forms.BindingNavigator sQLAppConfigBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton sQLAppConfigBindingNavigatorSaveItem;
        private System.Windows.Forms.ListBox sQLAppConfigListBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonSaveAll;
        private System.Windows.Forms.Button buttonAddNewSetting;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cloneConfigToolStripMenuItem;
        private System.Windows.Forms.Button buttonCloneToMode;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button buttonToggle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCode;
    }
}

