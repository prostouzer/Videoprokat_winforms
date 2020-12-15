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
    public partial class CustomersForm : Form
    {
        VideoprokatContext db;
        public CustomersForm(VideoprokatContext context)
        {
            InitializeComponent();
            db = context;
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            db.Customers.Load();

            customers.DataSource = db.Customers.Local.ToList();
            customers.Columns["Id"].ReadOnly = true;

            customers.Columns["Id"].HeaderText = "ID";
            customers.Columns["Name"].HeaderText = "Имя";
            customers.Columns["Rating"].HeaderText = "Рейтинг";

            customers.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            customers.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            customers.Columns["Rating"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            string newCustomerName = customerNameTextBox.Text.Trim();
            if (newCustomerName != "")
            {
                DialogResult result = MessageBox.Show($"Добавить {newCustomerName}?", "Новый клиент",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Customer customer = new Customer(newCustomerName);

                    db.Customers.Add(customer);
                    db.SaveChanges();
                    customers.DataSource = db.Customers.Local.ToList();

                    customerNameTextBox.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Введите имя нового клиента");
                customerNameTextBox.Text = "";
            }
        }

        private void customers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }

        private void customers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void customers_SelectionChanged(object sender, EventArgs e)
        {
            int currentCustomerId = -1;
            if (customers.CurrentRow != null)
            {
                currentCustomerId = Convert.ToInt32(customers.CurrentRow.Cells["Id"].Value);
                Customer currentCustomer = db.Customers.First(c => c.Id == currentCustomerId);

                var leasedByCustomer = from leasing in db.LeasedCopies
                                     where leasing.CustomerId == currentCustomer.Id
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
                leasedCopies.DataSource = leasedByCustomer.ToList();
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
    }
}
