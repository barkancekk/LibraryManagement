using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class Library
    {
        int count = 0;
        public List<Book> books;
        public List<User> users;

        public Library()
        {
            books = new List<Book>();
            users = new List<User>();
        }

        /* Create a user */
        public string NewUser(string name, string lastName)
        {
            User user = new User(name, lastName, count+1);
            if(!users.Contains(user))
            {
                users.Add(user);
                count++;
                return name.ToCharArray()[0].ToString() + lastName.ToCharArray()[0].ToString() + count.ToString();
            }
            else
            {
                return "This user already has an account!";
            }            
        }

        /* Add a book to the library */
        public void AddBook(Book book)
        {
            foreach(Book b in books) 
            {
                if((!book.title.Equals(b.title) || !book.author.Equals(b.author)) && book.isbn.Equals(b.isbn))
                {
                    throw new Exception("This ISBN belongs to another book!");
                }
            }
            if(books.Count > 0)
            {
                bool found = false;
                foreach(Book b in books)
                {
                    if (b.isbn == book.isbn)
                    {
                        b.inStock++;
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    books.Add(book);
                    book.inStock = 1;
                }
            }
            else
            {
                books.Add(book);
                book.inStock = 1;
            }
        }

        public void AddCopies(String isbn, int value)
        {
            bool found = false;
            foreach (Book book in books)
            {
                if (book.isbn == isbn)
                {
                    book.inStock += value;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                throw new Exception("\nBook not found in library");
            }
        }

        /* Decrease copies of a book */
        public void RemoveCopies(String isbn, int value)
        {
            bool found = false;
            foreach(Book book in books)
            {
                if(book.isbn == isbn)
                {
                    if (book.inStock-value >= 0)
                    {
                        book.inStock -= value;
                        found = true;
                        break;
                    }
                    else
                    {
                        throw new Exception("\nNot enough copies\n");
                    }
                }
            }
            if(!found)
            {
                throw new Exception("\nBook not found in library");
            }
        }

        /* List all books */
        public List<Book> GetBooks()
        {
            return books;
        }

        /* Search books by title or author */
        public List<Book> SearchBook(string key)
        {
            List<Book> result = new List<Book>();
            foreach (Book book in books)
            {
                if (book.title.Contains(key) || book.author.Contains(key))
                {
                    result.Add(book);
                }
            }
            return result;
        }

        /* Borrow a book from library */
        public void BorrowBook(string isbn, User user)
        {
            bool found = false;
            bool duplicate = false;

            foreach (Book book in books)
            {
                if (book.isbn == isbn && book.inStock > 0)
                {
                    found = true; //library has this book
                    foreach(Book b in user.borrowed)
                    {
                        if(b.isbn == book.isbn)
                        {
                            duplicate = true; // user borrowed one copy of this book before
                            throw new Exception("\nYou already have borrowed this book!\n");
                        }
                    }
                    if(!duplicate)
                    {
                        book.inStock--;
                        book.borrowed++;
                        BorrowedBook newBorrowed = new BorrowedBook(book.title, book.author, book.isbn);
                        user.borrowed.Add(newBorrowed);
                        Console.WriteLine("---------------------------------------------------");
                        Console.WriteLine("Return until " + newBorrowed.returnDate.ToString());
                        break;
                    }
                }  
            }
            if (!found)
            {
                throw new Exception("\nBook not found or out of stock!\n");
            }
        }

        /* Return a book to the library */
        public void ReturnBook(string isbn, User user)
        {
            bool found = false;
            bool userBorrowed = false;
            int warning = 0;

            foreach (Book book in books)
            {
                if (book.isbn == isbn && book.borrowed > 0)
                {
                    found = true; // library has this book
                    foreach (Book b in user.borrowed)
                    {
                        if (b.isbn == book.isbn)
                        {
                            userBorrowed = true; // user borrowed this book
                            book.inStock++;
                            book.borrowed--;
                            BorrowedBook removeBorrowed = new BorrowedBook(book.title, book.author, book.isbn);
                            warning = DateTime.Compare(DateTime.Now, removeBorrowed.returnDate);
                            user.borrowed.Remove(removeBorrowed);
                            Console.WriteLine("---------------------------------------------------");
                            Console.WriteLine("Book returned to the library.");
                            break;
                        }
                    }
                    if(warning > 0) 
                    {
                        Console.WriteLine("You are late! Please be careful with the return dates.");
                        Console.WriteLine("Your penalty points: " + ++user.penalty);
                    }
                    if (!userBorrowed)
                    {
                        throw new Exception("\nYou did not borrowed this book from our library!\n");  
                    }
                }
            }
            if (!found)
            {
                throw new Exception("\nThis book does not appear in our library records!\n");
            }
        }

        public List<BorrowedBook> showOverdue()
        {
            List<BorrowedBook> overdue = new List<BorrowedBook>();

            foreach(User user in users)
            {
                foreach(BorrowedBook b in user.borrowed)
                {
                    if(DateTime.Compare(DateTime.Now, b.returnDate) > 0)
                    {
                        overdue.Add(b);
                    }
                }
            }
            return overdue;
        }
    }
}
