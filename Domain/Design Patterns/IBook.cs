using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Design_Patterns
{
    public interface IBook
    {
        string title { get; set; }
        string contents { get; set; }
        void DisplayContents();
    }
}
