using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        static public void AddBook()
        {
            using (SqlConnection conection = new SqlConnection("LibraryConection"))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Book (Name,Author,Year)
                                             VALUES ('Book1', 'Author1', 2015);";
                    Console.WriteLine(command.ExecuteNonQuery());

                }
            }
        }

        static public void DeleteBook()
        {
            using (SqlConnection conection = new SqlConnection("LibraryConection"))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM Book WHERE id=5 ";
                    Console.WriteLine(command.ExecuteNonQuery());

                }
            }
        }

        static public void UpdateBook()
        {
            using (SqlConnection conection = new SqlConnection("LibraryConection"))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = @"Update Book set Year = 2017 where Id= 2 ";
                    Console.WriteLine(command.ExecuteNonQuery());

                }
            }
        }

        static public void ReadAllBooksWithUser()
        {
            using (SqlConnection conection = new SqlConnection("LibraryConection"))
            {
                conection.Open();
            
            }
        }
    }

}
