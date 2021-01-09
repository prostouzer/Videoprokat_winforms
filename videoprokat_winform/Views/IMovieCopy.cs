using System;
using System.Collections.Generic;
using System.Text;

namespace videoprokat_winform.Views
{
    interface IMovieCopy
    {
        event Action<int> OnAddMovieCopy;
    }
}
