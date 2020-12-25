using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace videoprokat_winform.Views
{
    interface IMainFormView : IView
    {
        DataGridView MoviesDgv { get; }
        DataGridView CopiesDgv { get; set; }
        DataGridView LeasingsDgv { get; set; }

        string filter { get; set; }
    }
}
