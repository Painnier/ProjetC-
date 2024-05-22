using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_TransConnect_TANG
{
    public partial class ConsultationCommandes : Form
    {
        private Entreprise transconnect;

        public ConsultationCommandes(Entreprise transconnect)
        {
            this.transconnect = transconnect;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Event handler for textBox1 text change
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string numeroSecurite = textBox1.Text;
            var client = transconnect.Clients.FirstOrDefault(c => c.NumeroSecuriteSociale == numeroSecurite);

            if (client != null)
            {
                listView1.Items.Clear();
                foreach (var commande in client.CommandesClient)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        commande.NumeroCommande.ToString(),
                        commande.Start,
                        commande.End,
                        commande.Prix.ToString("C"),
                        commande.CreateDate.ToShortDateString()
                    });
                    listView1.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Client non trouvé.");
            }
        }
    }
}