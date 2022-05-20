using System;
using MVCExample.Entities;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AnilTest
{
    // string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;


    public class AppDbContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Book> Book { get; set; }


        string con = ConfigurationManager.ConnectionStrings[@"Server=(localdb)\localdb; Initial Catalog=Library; Integrated Security=True;"].ConnectionString;
        public AppDbContext() : base()
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\localdb; Initial Catalog=Library; Integrated Security=True;");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }


        public Student AddNewStudent(Student _student)
        {
            var connection = new SqlConnection(con);

            try
            {

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var cmd = new SqlCommand();
                cmd.Connection = connection;

                var insertAddStudentQuery = @"INSERT INTO [Student]
                                             VALUES
                                                   (@stuName
                                                   ,@stuSurname
                                                   ,@studentId
						                           ,@stuMail
						                           ,@stuPhoneNumber
                                                                   )
                                                    SELECT @@IDENTITY ";
                cmd.CommandText = insertAddStudentQuery;
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.AddWithValue("@stuName", _student.stuName);
                cmd.Parameters.AddWithValue("@stuSurname", _student.stuSurname);
                cmd.Parameters.AddWithValue("@studentId", _student.studentId);
                cmd.Parameters.AddWithValue("@stuMail", _student.stuMail);
                cmd.Parameters.AddWithValue("@stuPhoneNumber", _student.stuPhoneNumber);


                var returnedObject = cmd.ExecuteScalar();

                int lastId = 0;

                if (int.TryParse(returnedObject.ToString(), out lastId))
                {
                    _student.Id = lastId;

                    return _student;
                }

                return null;



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
            return _student;
        }

        /*public Student FindStudent(Student _student)
        {
            var connection = new SqlConnection(con);

            try
            {

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM [Student] WHERE studentId =@studentId";
                cmd.Parameters.AddWithValue("@studentId", studentId);


                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return _student;
        }*/


    }
}
