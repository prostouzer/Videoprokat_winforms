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
    }
}
