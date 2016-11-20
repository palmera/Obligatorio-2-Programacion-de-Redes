namespace AdminMaintenance
{
    partial class AdminMaintenanceMain
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
            this.adminListBox = new System.Windows.Forms.ListBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.addAdminButton = new System.Windows.Forms.Button();
            this.removeAdminButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.modifyAdminButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // adminListBox
            // 
            this.adminListBox.FormattingEnabled = true;
            this.adminListBox.ItemHeight = 16;
            this.adminListBox.Location = new System.Drawing.Point(149, 23);
            this.adminListBox.Name = "adminListBox";
            this.adminListBox.Size = new System.Drawing.Size(282, 292);
            this.adminListBox.TabIndex = 0;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(12, 90);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(131, 22);
            this.nameTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(12, 150);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(131, 22);
            this.passwordTextBox.TabIndex = 2;
            // 
            // addAdminButton
            // 
            this.addAdminButton.Location = new System.Drawing.Point(12, 197);
            this.addAdminButton.Name = "addAdminButton";
            this.addAdminButton.Size = new System.Drawing.Size(131, 23);
            this.addAdminButton.TabIndex = 3;
            this.addAdminButton.Text = "Add Admin";
            this.addAdminButton.UseVisualStyleBackColor = true;
            this.addAdminButton.Click += new System.EventHandler(this.addAdminButton_Click);
            // 
            // removeAdminButton
            // 
            this.removeAdminButton.Location = new System.Drawing.Point(300, 333);
            this.removeAdminButton.Name = "removeAdminButton";
            this.removeAdminButton.Size = new System.Drawing.Size(131, 23);
            this.removeAdminButton.TabIndex = 4;
            this.removeAdminButton.Text = "Remove Admin";
            this.removeAdminButton.UseVisualStyleBackColor = true;
            this.removeAdminButton.Click += new System.EventHandler(this.RemoveAdminButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "New Admin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // modifyAdminButton
            // 
            this.modifyAdminButton.Location = new System.Drawing.Point(149, 333);
            this.modifyAdminButton.Name = "modifyAdminButton";
            this.modifyAdminButton.Size = new System.Drawing.Size(131, 23);
            this.modifyAdminButton.TabIndex = 8;
            this.modifyAdminButton.Text = "Modify Admin";
            this.modifyAdminButton.UseVisualStyleBackColor = true;
            this.modifyAdminButton.Click += new System.EventHandler(this.modifyAdminButton_Click);
            // 
            // AdminMaintenanceMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 401);
            this.Controls.Add(this.modifyAdminButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeAdminButton);
            this.Controls.Add(this.addAdminButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.adminListBox);
            this.Name = "AdminMaintenanceMain";
            this.Text = "AdminMaintenanceMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox adminListBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button addAdminButton;
        private System.Windows.Forms.Button removeAdminButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button modifyAdminButton;
    }
}