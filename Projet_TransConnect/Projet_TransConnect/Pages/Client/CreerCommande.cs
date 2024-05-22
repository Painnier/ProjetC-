using System;
using System.Linq;
using System.Windows.Forms;

namespace Projet_TransConnect_TANG
{
    public partial class CreerCommande : Form
    {
        private Entreprise transconnect;

        public CreerCommande(Entreprise transconnect)
        {
            this.transconnect = transconnect;
            InitializeComponent();
            splitContainer1.IsSplitterFixed = false;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer2.IsSplitterFixed = false;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer3.IsSplitterFixed = false;
            splitContainer3.FixedPanel = FixedPanel.Panel1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Event handler for textBox2 text change
        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
        {
            // Event handler for splitContainer3 Panel2 paint
        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Event handler for splitContainer3 Panel1 paint
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Collect data from text boxes
            string numeroSecurite = textBox1.Text;
            string nom = textBox2.Text;
            string prenom = textBox3.Text;
            DateTime dateNaissance = DateTime.Parse(textBox6.Text);
            string adressePostale = textBox5.Text;
            string email = textBox4.Text;
            string telephone = textBox8.Text;
            string ville = textBox7.Text;
            string destination = textBox9.Text;

            // Find client by security number
            var client = transconnect.Clients.FirstOrDefault(c => c.NISS == numeroSecurite);
            if (client == null)
            {
                MessageBox.Show("Client non trouvé. Veuillez vérifier le numéro de sécurité sociale.");
                return;
            }

            // Create a new Commande
            var commande = new Commande
            {
                Numero = Guid.NewGuid().ToString(), // Generate a unique number for the command
                Client = client,
                PointA = adressePostale,
                PointB = destination,
                Prix = new Random().Next(100, 1000), // Generate a random price for simplicity
                Vehicule = null, // This should be assigned properly in a real application
                Chauffeur = null, // This should be assigned properly in a real application
                Date = DateTime.Now
            };

            // Add command to the client's list of commands
            client.Commandes.Add(commande);
            transconnect.AjouterCommande(commande);

            MessageBox.Show("Commande créée avec succès !");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
