using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace videoprokat_winform.Views
{
    public interface IView
    {
        void Show();
        void Close();
    }
}
