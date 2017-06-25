using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ItCloudAcademyCSharp;
using AdoNet;

namespace EntityFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            bool escape = true;
            while (escape)
            {
                Console.WriteLine("Pres Number for choise Method:");
                Console.WriteLine($"\t Menu");
                Console.WriteLine("1:AddBook;");
                Console.WriteLine("2:DeleteBook;");
                Console.WriteLine("3:UpdateBook;");
                Console.WriteLine("4:ReadAllBooksByUser;");
                Console.WriteLine("5:GetBookCountByAuthor;");
                Console.WriteLine("6:GetBookInfoByName;");
                Console.WriteLine("7:TakeBook;");
                Console.WriteLine("8:ReturnBook;");
                Console.WriteLine("9:AddUser");
                Console.WriteLine("10:RemoveUser;");
                Console.WriteLine("0:Exit;");

                string menuNumber = Console.ReadLine();
                switch (menuNumber)
                {
                    case "1":
                        {
                            AddBook();
                            break;
                        }
                    case "2":
                        {
                            DeleteBook();
                            break;
                        }
                    case "3":
                        {
                            UpdateBook();
                            break;
                        }
                    case "4":
                        {
                            ReadAllBooksByUser();
                            break;
                        }
                    case "5":
                        {
                            GetBookCountByAuthor();
                            break;
                        }
                    case "6":
                        {
                            GetBookInfoByName();
                            break;
                        }
                    case "7":
                        {
                            TakeBook();
                            break;
                        }
                    case "8":
                        {
                            ReturnBook();
                            break;
                        }
                    case "9":
                        {
                            AddUser();
                            break;
                        }
                    case "10":
                        {
                            RemoveUser();
                            break;
                        }
                    case "0":
                        {
                            escape = false;
                            break;
                        }
                }
                Console.WriteLine("Pres any key to continue..");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void AddBook()
        {
            using (LibraryContext context = new LibraryContext())
            {
                context.Books.Add(new Books { Name = "book10", Author = "author15", Publisher = "publisher28", Year = 2017, UsersID = null });
                context.SaveChanges();
            }
        }

        private static void RemoveUser()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var result = context.Users.FirstOrDefault(x=>x.Name=="user5");
                context.Users.Remove(result);
                context.SaveChanges();
                Console.WriteLine($"User:{result.Name} delet");                
            }
        }

        private static void AddUser()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var result = context.Database.ExecuteSqlCommand(@"insert into Users (Name, Age) values ('user5',38 )");
                Console.WriteLine($"Add {result} items in Table Users");
            }
        }

        private static void ReturnBook()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var result = context.Books.FirstOrDefault(x => x.Id == 5);
                if (result.UsersID != null)
                {                    
                    var userId = context.Users.FirstOrDefault(x => x.Id == result.UsersID);
                    result.UsersID = null;
                    Console.WriteLine($"User:{userId.Name} return book: {result.Name} , {result.Author}, {result.Year} ");
                    context.SaveChanges();
                }               
            }
        }

        private static void TakeBook()
        {
            using (LibraryContext context = new LibraryContext())
            {               
                var result = context.Books.FirstOrDefault(x => x.Id == 5);
                if (result.UsersID == null)
                {
                    result.UsersID = 3;
                    context.SaveChanges();
                    var userId =context.Users.FirstOrDefault(x => x.Id==3);
                    Console.WriteLine($"User:{userId.Name} take book: {result.Name} , {result.Author}, {result.Year} ");
                }
                else
                {
                    Console.WriteLine("Book is busy");
                }
            }
        }

        private static void GetBookInfoByName()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var result = context.Books.Where(x => x.Name == "book4");
                foreach (var item in result)
                {
                    Console.WriteLine($"BookName:{item.Name}, BookAuthor:{item.Author}, BookPublisher:{item.Publisher}, Year:{item.Year}");
                }
            }
        }

        private static void GetBookCountByAuthor()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var result = context.Books.Count(x => x.Author == "author3");
                Console.WriteLine(result);
            }
        }

        private static void ReadAllBooksByUser()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var result = context.Users.Join(context.Books, x => x.Id, s => s.UsersID,
                             (x, s) => new
                             {
                                 UserName = x.Name,
                                 BookName = s.Name,
                                 BookAuthor = s.Author,
                                 BookPublisher = s.Publisher,
                                 Year = s.Year
                             }).GroupBy(x => x.UserName);
                foreach (var book in result)
                {
                    if (book.Key == "user3")
                    {
                        Console.WriteLine(book.Key);
                        foreach (var item in book)
                        {
                            Console.WriteLine($"\t BookName:{item.BookName}, BookAuthor:{item.BookAuthor}, BookPublisher:{item.BookPublisher}, Year:{item.Year}");
                        }
                    }
                }
            }
        }

        private static void UpdateBook()
        {
            using (LibraryContext context = new LibraryContext())
            {
                Books book = context.Books.FirstOrDefault(x => x.Id == 5);
                if (book != null)
                {
                    book.Publisher = "publisher45";
                    context.SaveChanges();
                }
            }
        }

        private static void DeleteBook()
        {
            using (LibraryContext context = new LibraryContext())
            {
                Books book = context.Books.FirstOrDefault(x => x.Id == 4);
                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
        }
    }
}
