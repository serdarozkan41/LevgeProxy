using LevgeProxy;
using System;
using System.Windows.Forms;

namespace LevgeProxyForm
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            ListViewItem lvi = new ListViewItem("Başladı.---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            listView1.Items.Add(lvi);

            Server server = new Server(listView1);
            server.LocalAddress = "0.0.0.0";
            server.RemoteAddress = "212.64.211.117";
            server.RemotePort = 27001;
            server.LocalPort = 27001;

            server.Start();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Clipboard.SetDataObject(e.Item.Text);
        }
    }
}
