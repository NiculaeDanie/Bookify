using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Design_Patterns
{
    public interface IBookFactory
    {
        IBook CreateBook(string title, string contents);
    }
}
