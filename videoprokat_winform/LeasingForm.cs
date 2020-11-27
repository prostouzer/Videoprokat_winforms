using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;

namespace videoprokat_winform
{
    public partial class LeasingForm : Form
    {
        MovieCopy currentCopy;
        public LeasingForm(MovieCopy movieCopy)
        {
            InitializeComponent();
            currentCopy = movieCopy;
        }

        private void LeasingForm_Load(object sender, EventArgs e)
        {
            movieNameLabel.Text = currentCopy.Movie.Title;
            movieCommentLabel.Text = currentCopy.Commentary;
            startDatePicker.Value = DateTime.Now;
            endDatePicker.Value = DateTime.Now.AddDays(3);

            using (VideoprokatContext db = new VideoprokatContext())
            {
                var clients = db.Clients.ToList();
                clientsComboBox.DataSource = clients;
                clientsComboBox.DisplayMember = "Name";
                clientsComboBox.ValueMember = "Id";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clientsComboBox.SelectedIndex > -1)
            {
                if (startDatePicker.Value <= endDatePicker.Value)
                {
                    using (VideoprokatContext db = new VideoprokatContext())
                    {
                        int clientId = Convert.ToInt32(clientsComboBox.SelectedValue);
                        int movieCopyId = currentCopy.Id;
                        Client owner = db.Clients.First(r => r.Id == clientId);
                        MovieCopy movieCopy = db.MoviesCopies.First(r => r.Id == movieCopyId);
                        movieCopy.Available = false; // TODO чтобы увидеть изменения на датагриде (чтобы убрался флажок) нужно перезапускать приложение
                        //Leasing leasing = new Leasing(copy, owner, startDatePicker.Value, endDatePicker.Value); // с такими параметрами при передаче MovieCopy copy аргумента в дб добавлялись каждый раз лишние копии фильмов (не прокаты)
                        Leasing leasing = new Leasing();
                        leasing.LeasingStartDate = startDatePicker.Value;
                        leasing.LeasingExpectedEndDate = endDatePicker.Value;
                        leasing.ClientId = clientId;
                        leasing.MovieCopyId = movieCopyId;
                        leasing.ClientName = owner.Name;

                        db.LeasedCopies.Add(leasing);
                        db.SaveChanges();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Конец проката должен быть позже начала");
                }
            }
            else
            {
                MessageBox.Show("Укажите клиента");
            }

            //if (clientsComboBox.SelectedIndex > -1)
            //{
            //    if (startDatePicker.Value <= endDatePicker.Value)
            //    {

            //        int clientId = Convert.ToInt32(clientsComboBox.SelectedValue);
            //        Client owner = db.Clients.First(r => r.Id == clientId);
            //        Leasing leasing = new Leasing(copy, owner, startDatePicker.Value, endDatePicker.Value);
            //        db.LeasedCopies.Add(leasing);
            //        db.SaveChanges();
            //        this.Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Конец проката должен быть позже начала");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Укажите клиента");
            //}
        }

        private void clientsComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            string id = (((Client)e.ListItem).Id).ToString();
            string name = ((Client)e.ListItem).Name;
            e.Value = id + ", " + name;
        }
    }
}
