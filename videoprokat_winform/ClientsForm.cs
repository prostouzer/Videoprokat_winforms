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

            clients.Columns["Id"].HeaderText = "ID";
            clients.Columns["Name"].HeaderText = "Имя";
            clients.Columns["Rating"].HeaderText = "Рейтинг";

            clients.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            clients.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            clients.Columns["Rating"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

                leasedCopies.Columns["MovieCopyId"].Visible = false;
                leasedCopies.Columns["MovieCopy"].Visible = false;
                leasedCopies.Columns["ClientId"].Visible = false;
                leasedCopies.Columns["Client"].Visible = false;

                leasedCopies.Columns["Id"].HeaderText = "ID";
                leasedCopies.Columns["LeasingStartDate"].HeaderText = "Дата начала";
                leasedCopies.Columns["LeasingExpectedEndDate"].HeaderText = "Ожидаемый возврат";
                leasedCopies.Columns["ReturnDate"].HeaderText = "Фактический возврат";
                leasedCopies.Columns["TotalPrice"].HeaderText = "Итоговая цена";

                leasedCopies.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                leasedCopies.Columns["LeasingStartDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                leasedCopies.Columns["LeasingExpectedEndDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                leasedCopies.Columns["ReturnDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                leasedCopies.Columns["TotalPrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ClientsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }
    }
}
