
namespace RollForward.Views
{
    partial class MainForm
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
            this.versionCombobox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // versionCombobox
            // 
            this.versionCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionCombobox.FormattingEnabled = true;
            this.versionCombobox.Location = new System.Drawing.Point(24, 60);
            this.versionCombobox.Name = "versionCombobox";
            this.versionCombobox.Size = new System.Drawing.Size(251, 21);
            this.versionCombobox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(141, 113);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(134, 40);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 182);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.versionCombobox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Rollforward";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox versionCombobox;
        private System.Windows.Forms.Button okButton;
    }
}