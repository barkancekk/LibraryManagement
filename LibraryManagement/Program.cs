using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class Program
    {
        public static void Main()
        {
            Library lb = new Library();
            int currentUser = 0;
            int operation = 100;
            bool login = false;
            int lop = 100;

            while(!login)
            {
                Console.WriteLine("1- Login");
                Console.WriteLine("2- Sign in");
                Console.WriteLine("0- Exit");
                lop = int.Parse(Console.ReadLine());
                switch (lop)
                {
                    case 1:
                        Console.WriteLine("Enter your user ID");
                        string id = Console.ReadLine();
                        List<User> users = lb.users;
                        foreach(User user in users)
                        {
                            if (user.id == id)
                            {
                                currentUser = users.IndexOf(user);
                                login = true;
                                break;
                            }
                        }
                        if(!login)
                        {
                            Console.WriteLine("User not found Error!");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Enter your name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter your lastname: ");
                        string lastname = Console.ReadLine();
                        string userId = lb.NewUser(name, lastname);
                        Console.WriteLine(userId);
                        break;

                    case 0:
                        return;
                }
            }
            /* Adding some books to the library stock */
            lb.AddBook(new Book("THE WITCHER: The last Wish", "Andrej Sapkowski", "12345"));
            lb.AddCopies("12345", 45);
            lb.AddBook(new Book("WHEEL OF TIME", "Robert Jordan", "02580"));
            lb.AddCopies("02580", 34);
            lb.AddBook(new Book("Harry Potter and the PRISONER OF AZKABAN", "J.K. Rowling", "78945"));
            lb.AddCopies("78945", 17);
            lb.AddBook(new Book("LotR: THE FELLOWSHIP OF THE RING", "J.R.R. Tolkien", "96325"));
            lb.AddCopies("96325", 29);
            lb.AddBook(new Book("Hunger Games: CATCHING FIRE", "Suzanne Collins", "74581"));
            lb.AddCopies("74581", 8);

            while (operation != 0 && login)
            {
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
                Console.WriteLine("\nSelect an operation\n");
                Console.WriteLine("1- Donate a Book");
                Console.WriteLine("2- Show all Books");
                Console.WriteLine("3- Search a Book");
                Console.WriteLine("4- Borrow a Book");
                Console.WriteLine("5- Return a Book");
                Console.WriteLine("6- Show overdue Books");
                Console.WriteLine("7- Add copies");
                Console.WriteLine("8- Remove copies");
                Console.WriteLine("0- Exit");

                operation = int.Parse(Console.ReadLine());
                switch (operation) 
                {
                    // add a book
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter Book Title: ");
                            string title = Console.ReadLine();
                            Console.WriteLine("Enter Author: ");
                            string author = Console.ReadLine();
                            Console.WriteLine("Enter ISBN: ");
                            string isbn = Console.ReadLine();
                            Book b = new Book(title, author, isbn);
                            lb.AddBook(b);
                        }
                        catch(Exception ex) 
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    // show all books
                    case 2:
                        List<Book> books = lb.GetBooks();
                        foreach(Book book in  books)
                        {
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine("Title: " + book.title);
                            Console.WriteLine("Author: " + book.author);
                            Console.WriteLine("ISBN: " + book.isbn);
                            Console.WriteLine("Copies in stock: " + book.inStock);
                            Console.WriteLine("Copies borrowed: " + book.borrowed);
                            Console.WriteLine();
                        }
                        break;

                    // search a book
                    case 3:
                        Console.WriteLine("Enter author name or part of the title: ");
                        string key = Console.ReadLine();
                        List<Book> searchedBooks = lb.SearchBook(key);
                        foreach (Book book in searchedBooks)
                        {
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine("Title: " + book.title);
                            Console.WriteLine("Author: " + book.author);
                            Console.WriteLine("ISBN: " + book.isbn);
                            Console.WriteLine("Copies in stock: " + book.inStock);
                            Console.WriteLine("Copies borrowed: " + book.borrowed);
                            Console.WriteLine();
                        }
                        break;
                    
                    // borrow a book
                    case 4:
                        try
                        {
                            Console.WriteLine("Enter ISBN of the book you want to borrow: ");
                            string borrow_isbn = Console.ReadLine();
                            lb.BorrowBook(borrow_isbn, lb.users.ElementAt(currentUser));
                        }
                        catch(Exception ex) 
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    
                    // return a book
                    case 5:
                        try 
                        {
                            Console.WriteLine("Enter ISBN of the book you want to return: ");
                            string return_isbn = Console.ReadLine();
                            lb.ReturnBook(return_isbn, lb.users.ElementAt(currentUser));
                        }
                        catch(Exception ex) 
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    // show overdue books
                    case 6:
                        List<BorrowedBook> overdue = lb.showOverdue();
                        foreach(BorrowedBook book in overdue)
                        {
                            Console.WriteLine("-------------------");
                            Console.WriteLine($"Title: {book.title}");
                            Console.WriteLine($"Author: {book.author}");
                            Console.WriteLine($"ISBN: {book.isbn}");
                            Console.WriteLine($"Return Date: {book.returnDate}");
                        }
                        break;

                    // add copies
                    case 7:
                        try
                        {
                            Console.WriteLine("----------------------");
                            Console.WriteLine("Enter ISBN of the book: ");
                            string inc_isbn = Console.ReadLine();
                            Console.WriteLine("Enter amount you want to add: ");
                            int inc = int.Parse(Console.ReadLine());
                            lb.AddCopies(inc_isbn, inc);
                            Console.WriteLine("Successfully added!");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    
                    // remove copies
                    case 8:
                        try
                        {
                            Console.WriteLine("----------------------");
                            Console.WriteLine("Enter ISBN of the book: ");
                            string dec_isbn = Console.ReadLine();
                            Console.WriteLine("Enter amount you want to remove: ");
                            int dec = int.Parse(Console.ReadLine());
                            lb.RemoveCopies(dec_isbn, dec);
                            Console.WriteLine("Successfully removed!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
            }
        }
    }
}
