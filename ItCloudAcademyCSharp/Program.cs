using ItCloudAcademyCSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AdoNet
{

    class Program
    {
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["LibraryConection"].ConnectionString;

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
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Books (Name,Author,Year, userid)
                                             VALUES ('Book5', 'Author5', 2015,2);";
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }
        }

        private static void DeleteBook()
        {
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM Books WHERE id=5 ";
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }
        }

        private static void UpdateBook()
        {
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"Update Books set userid = 4 where Id= 7 ";
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }
        }

        private static void ReadAllBooksByUser()
        {
            List<LibraryuserBook> books = new List<LibraryuserBook>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT u.Id UserId, u.Name UserName ,b.name BookName,
                                            b.author BookAuthor, b.publisher BookPublisher, b.year Year
                                            from users u join Books b on u.id=b.UserID
                                            where u.Id =1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new LibraryuserBook
                            {
                                Id = (int)reader["UserId"],
                                UserName = (string)reader["username"],
                                Bookname = (string)reader["bookname"],
                                BookAuthor = (string)reader["bookauthor"],
                                BookPublisher = (string)reader["bookpublisher"],
                                Year = (int)reader["year"]
                            });
                        }
                    }
                }
            }

            var groupBooks = books.GroupBy(x => x.UserName);
            foreach (var user in groupBooks)
            {
                Console.WriteLine($"UserName:{user.Key}");
                foreach (var book in user)
                {
                    Console.WriteLine($"\t BookName:{book.Bookname}, BookAuthor:{book.BookAuthor}, BookPublisher:{book.BookPublisher}, Year:{book.Year}");
                }
            }
        }

        private static void GetBookCountByAuthor()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select count(*) as CoutnBooks from Books 
                                            where author ='author1'";
                    Console.WriteLine(command.ExecuteScalar());
                }
            }
        }

        private static void GetBookInfoByName()
        {
            List<Book> booksInfo = new List<Book>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select Name, Author, Publisher, Year from Books 
                                        where name = 'book4';";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            booksInfo.Add(new Book
                            {
                                Name = (string)reader["name"],
                                Author = (string)reader["author"],
                                Publisher = (string)reader["publisher"],
                                Year = (int)reader["year"]
                            });
                        }
                    }
                }
            }

            foreach (var book in booksInfo)
            {
                Console.WriteLine($"Name:{book.Name}, Author:{book.Author}, Publisher:{book.Publisher}, Year:{book.Year} ");
            }
        }

        private static void TakeBook()
        {
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"update books 
                                            set userid = 4
                                            where id = 2";
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }
        }

        private static void ReturnBook()
        {
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"update books 
                                            set userid = null
                                            where id = 2";
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }
        }

        private static void AddUser()
        {
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO users (Name,Age)
                                             VALUES ('user6', 42);";
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }
        }

        private static void RemoveUser()
        {
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM Users WHERE id=5 ";
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }
        }
    }
}
