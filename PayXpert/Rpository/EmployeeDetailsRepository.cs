using PayXpert.Exceptiions;
using PayXpert.Model;
using PayXpert.Services;
using PayXpert.Utility;
using System.Data.SqlClient;

namespace PayXpert.Rpository
{
    internal class EmployeeDetailsRepository
    {
        string sqlConnection = null;
        SqlCommand cmd = null;

        public EmployeeDetailsRepository()
        {
            //sqlConnection = new SqlConnection("Server=PRASHI;Database=HMBank;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true");

            sqlConnection = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();

        }

        //Get Employee By ID
        public void GetEmployeeById(int EmployeeID)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Employee Where EmployeeID = @empId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", EmployeeID);
                try
                {
                    connection.Open();
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = (int)reader["EmployeeId"];
                        employee.FirstName = (string)reader["FirstName"];
                        employee.LastName = (string)reader["LastName"];
                        employee.Dob = (DateTime)reader["DateOfBirth"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Email = (string)reader["Email"];
                        employee.PhoneNumber = (long)reader["PhoneNumber"];
                        employee.Address = (string)reader["Address"];
                        employee.JoiningDate = (DateTime)reader["JoiningDate"];
                        employee.TerminationDate = reader["TerminationDate"] != DBNull.Value ? (DateTime?)reader["TerminationDate"] : null;
                        IEmployeeService employeeService = new EmployeeService();
                        employeeService.GetEmployeeById(employee);
                    }
                    else
                    {
                        throw new EmployeeNotFoundException("Employee Not Fount");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Get All Employees
        public void GetAllEmployees()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT EmployeeID,FirstName FROM Employee";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = (string)reader["FirstName"];
                        employee.EmployeeID = (int)reader["EmployeeID"];
                        IEmployeeService employeeService = new EmployeeService();
                        employeeService.GetEmployeeDetail(employee);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new EmployeeNotFoundException("Records Not Found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Add Employee Detail
        internal void AddEmpployee(string firstName, string lastName, DateTime? dateOfBirth, string gender, string email, string phoneNumber, string? address, string position, DateTime? joiningDate, DateTime? terminationDate)
        {
            int empId = 0;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT Top 1 * FROM Employee ORDER BY EmployeeID DESC";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        empId = (int)reader["EmployeeId"];
                    }
                    else
                    {
                        throw new Exception("Can't Add Values to the Table Employee.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                    empId++;
                }
            }
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "INSERT INTO Employee (EmployeeID, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate) VALUES (@empId , @first , @last , @dob , @gen , @email , @ph , @addr , @pos , @join , @end)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", empId);
                cmd.Parameters.AddWithValue("@first", firstName);
                cmd.Parameters.AddWithValue("@last", lastName);
                //cmd.Parameters.AddWithValue("@dob", dateOfBirth);
                cmd.Parameters.AddWithValue("@gen", gender);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@ph", phoneNumber);
                cmd.Parameters.AddWithValue("@addr", address);
                cmd.Parameters.AddWithValue("@pos", position);
                //cmd.Parameters.AddWithValue("@join", joiningDate);
                //cmd.Parameters.AddWithValue("@end", terminationDate);
                // Handle potential null values for terminationDate
                if (terminationDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@end", terminationDate.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@end", DBNull.Value);
                }
                //dob
                if (dateOfBirth.HasValue)
                {
                    cmd.Parameters.AddWithValue("@dob", dateOfBirth.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@dob", DBNull.Value);
                }
                //joiningDate 
                if (joiningDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@join", joiningDate.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@join", DBNull.Value);
                }
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    int reader = cmd.ExecuteNonQuery();
                    if (reader > 0)
                    {
                        Console.WriteLine("Successfully Added Employee Details.");
                        Console.WriteLine($"The Assign Employee ID is {empId}");
                    }
                    else
                    {
                        throw new Exception("Can't Add Values to the Table Employee.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Update Eployee
        internal void FieldChange(string field, object fieldValue, int employeeID)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = $"UPDATE Employee SET {field} = @new WHERE EmployeeID = @empId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", employeeID);
                cmd.Parameters.AddWithValue("@new", fieldValue);
                try
                {
                    connection.Open();
                    cmd.Connection = connection;

                    int reader = cmd.ExecuteNonQuery();
                    if (reader > 0)
                    {
                        Console.WriteLine("Updated Successfully.");
                    }
                    else
                    {
                        throw new EmployeeNotFoundException("Employee Not Fount");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Remove Employee
        public void RemoveEmployee(int employeeID)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "DELETE FROM Employee WHERE EmployeeID = @empId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", employeeID);
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    int reader = cmd.ExecuteNonQuery();
                    if (reader > 0)
                    {
                        Console.WriteLine("Successfully Deleted Employee Details.");
                    }
                    else
                    {
                        throw new EmployeeNotFoundException($"Employee ID :: {employeeID} NOT FOUND");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //ID DISPLAY
        public void EmploeeIdList()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT EmployeeID FROM Employee";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = (int)reader["EmployeeID"];
                        IEmployeeService employeeService = new EmployeeService();
                        employeeService.EmployeeId(employee);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new EmployeeNotFoundException("Records Not Found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Check Employee Id
        public bool CheckEmployeeID(int id)
        {
            int empId = 0;
            bool flag = false;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT EmployeeID FROM Employee";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        empId = (int)reader["EmployeeId"];
                        if(empId == id)
                        {
                            return true;
                        }
                        flag = true;
                    }
                    if(!flag)
                    {
                        throw new Exception("Can't Didplay the Table Employee.");

                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();

                }
            }
        }
    }
}

