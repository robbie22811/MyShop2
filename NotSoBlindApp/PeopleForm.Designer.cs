namespace WinFormUI
{
    partial class PeopleForm
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
            firstNameText = new TextBox();
            addPersonHeader = new Label();
            listPeopleHeader = new Label();
            listPeopleListBox = new ListBox();
            addPersonButton = new Button();
            refreshListButton = new Button();
            listActivePlayers = new ListBox();
            listActiveHeader = new Label();
            btnMoveToActive = new Button();
            btnMoveToInactive = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnReport = new Button();
            btnAllActive = new Button();
            btnAllInactive = new Button();
            txtFailCount = new TextBox();
            txtRetries = new TextBox();
            label2 = new Label();
            label3 = new Label();
            lblTry = new Label();
            SuspendLayout();
            // 
            // firstNameText
            // 
            firstNameText.Location = new Point(247, 647);
            firstNameText.Name = "firstNameText";
            firstNameText.Size = new Size(461, 48);
            firstNameText.TabIndex = 14;
            // 
            // addPersonHeader
            // 
            addPersonHeader.AutoSize = true;
            addPersonHeader.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            addPersonHeader.Location = new Point(27, 650);
            addPersonHeader.Name = "addPersonHeader";
            addPersonHeader.Size = new Size(214, 40);
            addPersonHeader.TabIndex = 4;
            addPersonHeader.Text = "Add Player:";
            // 
            // listPeopleHeader
            // 
            listPeopleHeader.AutoSize = true;
            listPeopleHeader.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            listPeopleHeader.Location = new Point(25, 31);
            listPeopleHeader.Name = "listPeopleHeader";
            listPeopleHeader.Size = new Size(310, 40);
            listPeopleHeader.TabIndex = 5;
            listPeopleHeader.Text = "Available Players";
            // 
            // listPeopleListBox
            // 
            listPeopleListBox.FormattingEnabled = true;
            listPeopleListBox.ImeMode = ImeMode.Off;
            listPeopleListBox.ItemHeight = 40;
            listPeopleListBox.Location = new Point(25, 80);
            listPeopleListBox.Name = "listPeopleListBox";
            listPeopleListBox.SelectionMode = SelectionMode.MultiExtended;
            listPeopleListBox.Size = new Size(380, 524);
            listPeopleListBox.TabIndex = 6;
            // 
            // addPersonButton
            // 
            addPersonButton.Location = new Point(735, 646);
            addPersonButton.Name = "addPersonButton";
            addPersonButton.Size = new Size(152, 48);
            addPersonButton.TabIndex = 15;
            addPersonButton.Text = "Add";
            addPersonButton.UseVisualStyleBackColor = true;
            addPersonButton.Click += addPersonButton_Click;
            // 
            // refreshListButton
            // 
            refreshListButton.Location = new Point(971, 80);
            refreshListButton.Name = "refreshListButton";
            refreshListButton.Size = new Size(289, 48);
            refreshListButton.TabIndex = 12;
            refreshListButton.Text = "Create Groups";
            refreshListButton.UseVisualStyleBackColor = true;
            refreshListButton.Click += refreshListButton_Click;
            // 
            // listActivePlayers
            // 
            listActivePlayers.FormattingEnabled = true;
            listActivePlayers.ItemHeight = 40;
            listActivePlayers.Location = new Point(565, 80);
            listActivePlayers.Name = "listActivePlayers";
            listActivePlayers.SelectionMode = SelectionMode.MultiExtended;
            listActivePlayers.Size = new Size(338, 524);
            listActivePlayers.TabIndex = 11;
            // 
            // listActiveHeader
            // 
            listActiveHeader.AutoSize = true;
            listActiveHeader.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            listActiveHeader.Location = new Point(565, 27);
            listActiveHeader.Name = "listActiveHeader";
            listActiveHeader.Size = new Size(259, 40);
            listActiveHeader.TabIndex = 10;
            listActiveHeader.Text = "Active Players";
            // 
            // btnMoveToActive
            // 
            btnMoveToActive.Location = new Point(434, 128);
            btnMoveToActive.Name = "btnMoveToActive";
            btnMoveToActive.Size = new Size(104, 48);
            btnMoveToActive.TabIndex = 7;
            btnMoveToActive.Text = ">>>";
            btnMoveToActive.UseVisualStyleBackColor = true;
            btnMoveToActive.Click += btnMoveToActive_Click;
            // 
            // btnMoveToInactive
            // 
            btnMoveToInactive.Location = new Point(434, 415);
            btnMoveToInactive.Name = "btnMoveToInactive";
            btnMoveToInactive.Size = new Size(104, 48);
            btnMoveToInactive.TabIndex = 10;
            btnMoveToInactive.Text = "<<<";
            btnMoveToInactive.UseVisualStyleBackColor = true;
            btnMoveToInactive.Click += btnMoveToInactive_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(971, 556);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(289, 48);
            btnEdit.TabIndex = 13;
            btnEdit.Text = "Edit Players";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDelete.Location = new Point(1220, 733);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(289, 48);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "Reset Schedule";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnReport
            // 
            btnReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReport.Location = new Point(40, 733);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(289, 48);
            btnReport.TabIndex = 16;
            btnReport.Text = "Show Report";
            btnReport.UseVisualStyleBackColor = true;
            btnReport.Click += btnReport_Click;
            // 
            // btnAllActive
            // 
            btnAllActive.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnAllActive.Location = new Point(434, 195);
            btnAllActive.Name = "btnAllActive";
            btnAllActive.Size = new Size(104, 48);
            btnAllActive.TabIndex = 8;
            btnAllActive.Text = "ALL>>>";
            btnAllActive.UseVisualStyleBackColor = true;
            btnAllActive.Click += btnAllActive_Click;
            // 
            // btnAllInactive
            // 
            btnAllInactive.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnAllInactive.Location = new Point(434, 340);
            btnAllInactive.Name = "btnAllInactive";
            btnAllInactive.Size = new Size(104, 48);
            btnAllInactive.TabIndex = 9;
            btnAllInactive.Text = "<<<ALL";
            btnAllInactive.UseVisualStyleBackColor = true;
            btnAllInactive.Click += btnAllInactive_Click;
            // 
            // txtFailCount
            // 
            txtFailCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFailCount.Location = new Point(1387, 80);
            txtFailCount.Name = "txtFailCount";
            txtFailCount.Size = new Size(150, 48);
            txtFailCount.TabIndex = 18;
            // 
            // txtRetries
            // 
            txtRetries.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtRetries.Location = new Point(1387, 190);
            txtRetries.Name = "txtRetries";
            txtRetries.Size = new Size(150, 48);
            txtRetries.TabIndex = 19;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(1387, 48);
            label2.Name = "label2";
            label2.Size = new Size(122, 29);
            label2.TabIndex = 20;
            label2.Text = "Fail Count";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(1387, 158);
            label3.Name = "label3";
            label3.Size = new Size(117, 29);
            label3.TabIndex = 21;
            label3.Text = "Try Count";
            // 
            // lblTry
            // 
            lblTry.AutoSize = true;
            lblTry.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblTry.Location = new Point(971, 147);
            lblTry.Name = "lblTry";
            lblTry.Size = new Size(54, 29);
            lblTry.TabIndex = 22;
            lblTry.Text = "Try:";
            // 
            // PeopleForm
            // 
            AutoScaleDimensions = new SizeF(21F, 40F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1549, 793);
            Controls.Add(lblTry);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtRetries);
            Controls.Add(txtFailCount);
            Controls.Add(btnAllInactive);
            Controls.Add(btnAllActive);
            Controls.Add(btnReport);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnMoveToInactive);
            Controls.Add(btnMoveToActive);
            Controls.Add(listActiveHeader);
            Controls.Add(listActivePlayers);
            Controls.Add(refreshListButton);
            Controls.Add(addPersonButton);
            Controls.Add(listPeopleListBox);
            Controls.Add(listPeopleHeader);
            Controls.Add(addPersonHeader);
            Controls.Add(firstNameText);
            Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(7, 8, 7, 8);
            Name = "PeopleForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cornhole Schedule Maker";
            Load += PeopleForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox firstNameText;
        private Label addPersonHeader;
        private Label listPeopleHeader;
        private ListBox listPeopleListBox;
        private Button addPersonButton;
        private Button refreshListButton;
        private ListBox listActivePlayers;
        private Label listActiveHeader;
        private Button btnMoveToActive;
        private Button btnMoveToInactive;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnReport;
        private Button btnAllActive;
        private Button btnAllInactive;
        private TextBox txtFailCount;
        private TextBox txtRetries;
        private Label label2;
        private Label label3;
        private Label lblTry;
    }
}

