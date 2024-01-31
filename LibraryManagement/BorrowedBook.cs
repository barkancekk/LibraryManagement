using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class BorrowedBook : Book
    {
        public DateTime returnDate;
        
        public BorrowedBook(string title, string author, string isbn) : base(title, author, isbn)
        {
            returnDate = DateTime.Now.AddDays(30);
        }
    }
}
