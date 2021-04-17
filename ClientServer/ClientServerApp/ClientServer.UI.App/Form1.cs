using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientServer.Client;
using ClientServer.Server;

namespace ClientServer.UI.App
{
	public partial class Form1 : Form
	{
		private delegate void SetTextDelegate(Control c, string text);

		public Form1()
		{
			InitializeComponent();

			WorldServer.Output += s => AddText(outputRichTextBox, s);
			WorldServerClient.Output += s => AddText(outputRichTextBox, s);
		}

		private void startServerButton_Click(object sender, EventArgs e)
		{
			Task.Run(WorldServer.StartServer);
		}

		private void startClientButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				for (int i = 0; i < 50; i++)
				{
					Task.Run(WorldServerClient.ConnectClient);
				}
			});
		}

		private void AddText(Control c, string text)
		{
			if (c != null)
			{
				if (c.InvokeRequired)
				{
					c.Invoke(new SetTextDelegate(AddText), new object[] { c, text });
				}
				else
					c.Text += 
						$"\n {text}";
			}
		}
	}
}
