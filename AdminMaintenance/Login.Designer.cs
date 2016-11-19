namespace AdminMaintenance
{
    partial class Login
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
            this.Name = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveAdmin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Name
            // 
            this.Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Name.Location = new System.Drawing.Point(96, 139);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(401, 32);
            this.Name.TabIndex = 0;
            // 
            // Password
            // 
            this.Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Password.Location = new System.Drawing.Point(96, 247);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(401, 32);
            this.Password.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(91, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mantenimiento de Administradores";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // saveAdmin
            // 
            this.saveAdmin.Location = new System.Drawing.Point(198, 312);
            this.saveAdmin.Name = "saveAdmin";
            this.saveAdmin.Size = new System.Drawing.Size(151, 44);
            this.saveAdmin.TabIndex = 3;
            this.saveAdmin.Text = "Guardar";
            this.saveAdmin.UseVisualStyleBackColor = true;
            this.saveAdmin.Click += new System.EventHandler(this.saveAdmin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 391);
            this.Controls.Add(this.saveAdmin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Name);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Name;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveAdmin;
    }
}

