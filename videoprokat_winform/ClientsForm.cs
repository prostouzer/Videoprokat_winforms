using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;
using System.Linq;
using System.Data.Entity;

namespace videoprokat_winform
{
    public partial class ClientsForm : Form
    {
        VideoprokatContext db;
        public ClientsForm()
        {
            InitializeComponent();
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            db = new VideoprokatContext();
            db.Clients.Load();

            clients.DataSource = db.Clients.Local.ToList();
            clients.Columns["Id"].ReadOnly = true;
        }

        int DefaultRating = 100;
        private void button1_Click(object sender, EventArgs e)
        {
            string newClientName = clientNameTextBox.Text.Trim();
            if (newClientName != "")
            {
                Client client = new Client();
                client.Name = newClientName;
                client.Rating = DefaultRating;

                db.Clients.Add(client);
                db.SaveChanges();
                clients.DataSource = db.Clients.Local.ToList();

                clientNameTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Введите имя нового клиента");
                clientNameTextBox.Text = "";
            }
        }

        private void clients_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }

        private void clients_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void clients_SelectionChanged(object sender, EventArgs e)
        {
            int currentClientId = -1;
            if (clients.CurrentRow != null)
            {
                currentClientId = Convert.ToInt32(clients.CurrentRow.Cells["Id"].Value);
                Client currentClient = new Client();
                currentClient = db.Clients.First(c => c.Id == currentClientId);
                var leasedByClient = (from r in db.LeasedCopies
                                      where r.ClientId == currentClient.Id
                                      select r).ToList();
                leasedCopies.DataSource = leasedByClient;

                leasedCopies.Columns["ClientName"].Visible = false;
                leasedCopies.Columns["MovieCopyId"].Visible = false;
                leasedCopies.Columns["MovieCopy"].Visible = false;
                leasedCopies.Columns["ClientId"].Visible = false;
                leasedCopies.Columns["Client"].Visible = false;
            }
        }
    }
}
