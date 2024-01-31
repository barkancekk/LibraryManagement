using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class Book
    {
        public string title;
        public string author;
        public string isbn;
        public int inStock;
        public int borrowed;

        public Book(string title, string author, string isbn)
        {
            this.title = title;
            this.author = author;
            this.isbn = isbn;
            borrowed = 0;
        }
        public Book(string title, string author, string isbn, int inStock)
        {
            this.title = title;
            this.author = author;
            this.isbn = isbn;
            this.inStock = inStock;
            borrowed = 0;
        }

        public override string ToString()
        {
            return ($"{title}\n{author}\n{isbn}\n{inStock}\n{borrowed}\n");
        }
    }
}
