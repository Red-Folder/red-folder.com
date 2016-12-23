using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RedFolder.ViewModels.BlogCollection;

namespace RedFolder.ViewModels
{
    public interface IPager
    {
        int PageNo { get; }

        int BlogsPerPage { get; }

        string FilterBy { get; }

        OrderBy OrderedBy { get; }

        int Pages { get; }
    }
}
