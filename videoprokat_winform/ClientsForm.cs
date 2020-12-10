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
        private void addClientButton_Click(object sender, EventArgs e)
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

                var leasedByClient = from leasing in db.LeasedCopies
                                     where leasing.ClientId == currentClient.Id
                                     join movie in db.MoviesOriginal on leasing.MovieCopy.MovieId equals movie.Id
                                     join movieCopy in db.MoviesCopies on leasing.MovieCopyId equals movieCopy.Id
                                     select new
                                     {
                                         Id = leasing.Id,
                                         MovieTitle = movie.Title,
                                         MovieCommentary = movieCopy.Commentary,
                                         StartDate = leasing.LeasingStartDate,
                                         ExpectedEndDate = leasing.LeasingExpectedEndDate,
                                         ReturnDate = leasing.ReturnDate,
                                         TotalPrice = leasing.TotalPrice,
                                     };
                leasedCopies.DataSource = leasedByClient.ToList();
                RedrawLeasedCopiesDgv();
            }
        }

        private void RedrawLeasedCopiesDgv()
        {
            leasedCopies.Columns["Id"].HeaderText = "ID";
            leasedCopies.Columns["MovieTitle"].HeaderText = "Фильм";
            leasedCopies.Columns["MovieCommentary"].HeaderText = "Комментарий";
            leasedCopies.Columns["StartDate"].HeaderText = "Дата начала";
            leasedCopies.Columns["ExpectedEndDate"].HeaderText = "Ожидаемый возврат";
            leasedCopies.Columns["ReturnDate"].HeaderText = "Фактический возврат";
            leasedCopies.Columns["TotalPrice"].HeaderText = "Итоговая цена";

            leasedCopies.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopies.Columns["MovieTitle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopies.Columns["MovieCommentary"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            leasedCopies.Columns["StartDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopies.Columns["ExpectedEndDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopies.Columns["ReturnDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopies.Columns["TotalPrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ClientsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }
    }
}
