
namespace ClientServer.UI.App
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
			this.startServerButton = new System.Windows.Forms.Button();
			this.startClientButton = new System.Windows.Forms.Button();
			this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// startServerButton
			// 
			this.startServerButton.Location = new System.Drawing.Point(12, 12);
			this.startServerButton.Name = "startServerButton";
			this.startServerButton.Size = new System.Drawing.Size(75, 23);
			this.startServerButton.TabIndex = 0;
			this.startServerButton.Text = "button1";
			this.startServerButton.UseVisualStyleBackColor = true;
			this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
			// 
			// startClientButton
			// 
			this.startClientButton.Location = new System.Drawing.Point(94, 12);
			this.startClientButton.Name = "startClientButton";
			this.startClientButton.Size = new System.Drawing.Size(75, 23);
			this.startClientButton.TabIndex = 1;
			this.startClientButton.Text = "button2";
			this.startClientButton.UseVisualStyleBackColor = true;
			this.startClientButton.Click += new System.EventHandler(this.startClientButton_Click);
			// 
			// outputRichTextBox
			// 
			this.outputRichTextBox.Location = new System.Drawing.Point(13, 42);
			this.outputRichTextBox.Name = "outputRichTextBox";
			this.outputRichTextBox.Size = new System.Drawing.Size(1018, 631);
			this.outputRichTextBox.TabIndex = 2;
			this.outputRichTextBox.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1043, 685);
			this.Controls.Add(this.outputRichTextBox);
			this.Controls.Add(this.startClientButton);
			this.Controls.Add(this.startServerButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button startServerButton;
		private System.Windows.Forms.Button startClientButton;
		private System.Windows.Forms.RichTextBox outputRichTextBox;
	}
}

