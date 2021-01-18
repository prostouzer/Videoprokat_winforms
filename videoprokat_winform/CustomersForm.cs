using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows.Forms;
using videoprokat_winform.Models;
using System.Linq;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class CustomersForm : Form, ICustomersView
    {
        public event Action<Customer> OnAddCustomer;
        public event Action<int, Customer> OnUpdateCustomer;

        public event Action<int> OnCustomerSelectionChanged;

        public new event Action OnLoad;
        public CustomersForm()
        {
            InitializeComponent();

            Load += (sender, args) => OnLoad?.Invoke();

            customersDgv.SelectionChanged += (sender, args) => OnCustomerSelectionChanged?.Invoke(Convert.ToInt32(customersDgv.CurrentRow.Cells["Id"].Value));

            customersDgv.CellEndEdit += (sender, args) =>
            {
                var currentCustomerId = Convert.ToInt32(customersDgv.CurrentRow.Cells["Id"].Value);
                var customer = new Customer(
                    customersDgv.CurrentRow.Cells["Name"].Value.ToString(),
                    (float)customersDgv.CurrentRow.Cells["Rating"].Value);
                OnUpdateCustomer?.Invoke(currentCustomerId, customer);
            };

            addCustomerButton.Click += (sender, args) =>
            {
                var name = customerNameTextBox.Text.Trim();
                var customer = new Customer(name);
                OnAddCustomer?.Invoke(customer);
            };
        }

        public new void Show()
        {
            ShowDialog();
        }

        public bool ConfirmNewCustomer()
        {
            var customerName = customerNameTextBox.Text.Trim();
            if (customerName != "")
            {
                var result = MessageBox.Show($"Добавить {customerName}?", "Новый клиент",
                    MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return false;
                customerNameTextBox.Text = "";
                return true;
            }
            MessageBox.Show("Введите имя нового клиента");
            customerNameTextBox.Text = "";
            return false;
        }
        public void RedrawCustomers(IQueryable<Customer> customersIQueryable)
        {
            var customers = customersIQueryable.ToList();
            BeginInvoke(new Action(() =>
            {
                customersDgv.DataSource = customers;

                customersDgv.Columns["Id"].ReadOnly = true;

                customersDgv.Columns["Id"].HeaderText = "ID";
                customersDgv.Columns["Name"].HeaderText = "Имя";
                customersDgv.Columns["Rating"].HeaderText = "Рейтинг";

                customersDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                customersDgv.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                customersDgv.Columns["Rating"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }));
        }

        public void RedrawLeasings(IQueryable<Leasing> leasingsIQueryable, IQueryable<MovieOriginal> moviesIQueryable, IQueryable<MovieCopy> movieCopiesIQueryable)
        {
            var leasings = leasingsIQueryable.ToList();
            var movies = moviesIQueryable.ToList();
            var movieCopies = movieCopiesIQueryable.ToList();

            var currentCustomerId = Convert.ToInt32(customersDgv.CurrentRow.Cells["Id"].Value);
            var leasedByCustomer = from leasing in leasings
                                   where leasing.CustomerId == currentCustomerId
                                   join movie in movies on leasing.MovieCopy.MovieId equals movie.Id
                                   join movieCopy in movieCopies on leasing.MovieCopyId equals movieCopy.Id
                                   select new
                                   {
                                       Id = leasing.Id,
                                       MovieTitle = movie.Title,
                                       MovieCommentary = movieCopy.Commentary,
                                       StartDate = leasing.StartDate,
                                       ExpectedEndDate = leasing.ExpectedEndDate,
                                       ReturnDate = leasing.ReturnDate,
                                       TotalPrice = leasing.TotalPrice
                                   };
            leasedCopiesDgv.DataSource = leasedByCustomer.ToList();

            leasedCopiesDgv.Columns["Id"].HeaderText = "ID";
            leasedCopiesDgv.Columns["MovieTitle"].HeaderText = "Фильм";
            leasedCopiesDgv.Columns["MovieCommentary"].HeaderText = "Комментарий";
            leasedCopiesDgv.Columns["StartDate"].HeaderText = "Дата начала";
            leasedCopiesDgv.Columns["ExpectedEndDate"].HeaderText = "Ожидаемый возврат";
            leasedCopiesDgv.Columns["ReturnDate"].HeaderText = "Фактический возврат";
            leasedCopiesDgv.Columns["TotalPrice"].HeaderText = "Итоговая цена";

            leasedCopiesDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopiesDgv.Columns["MovieTitle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopiesDgv.Columns["MovieCommentary"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            leasedCopiesDgv.Columns["StartDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopiesDgv.Columns["ExpectedEndDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopiesDgv.Columns["ReturnDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            leasedCopiesDgv.Columns["TotalPrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void customersDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }
    }
}
