using PayXpert.Exceptions;
using PayXpert.Model;
using PayXpert.Services;
using PayXpert.Utility;
using System.Data.SqlClient;

namespace PayXpert.Rpository
{
    internal class FinancialRecordRepository 
    {
        string sqlConnection = null;
        SqlCommand cmd = null;

        public FinancialRecordRepository()
        {
            sqlConnection = DbConnUtil.GetConnectionString();
            //sqlConnection = "Server=PRASHI;Database=PayXpert;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";
            cmd = new SqlCommand();
        }

        //Add Financial Record
        public void AddFinancialRecord(int employeeId, DateTime date, string description, double? amount, string recordType)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "INSERT INTO FinancialRecord (EmployeeID, RecordDate, Description, Amount, RecordType) VALUES (@empid , @date , @des , @amount , @type )";
                cmd.Parameters.Clear();                        //Record Id is IDENTITY so cant insert from here
                cmd.Parameters.AddWithValue("@empid", employeeId);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@des", description);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@type", recordType);
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    int reader = cmd.ExecuteNonQuery();
                    if (reader > 0)
                    {
                        Console.WriteLine("Successfully Added Financial Record Details.");
                    }
                    else
                    {
                        throw new FinancialRecordException("Can't Add Values to the Table Financial Record.");
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

        //Get Financial Record By DATE
        public void GetFinancialRecordsForDate(DateTime? date)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM  FinancialRecord Where RecordDate = @date";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@date", date);
                bool flag = false;
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord();
                        record.RecordID = (int)reader["RecordID"];
                        record.EmployeeID = (int)reader["EmployeeID"];
                        record.RecordDate = (DateTime)reader["RecordDate"];
                        record.Description = (string)reader["Description"];
                        record.amount = (int)reader["Amount"];
                        record.RecordType = (string)reader["RecordType"];
                        IFinancialRecordService service = new FinancialRecordService();
                        service.GetFinancialRecordsForDate(record);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new FinancialRecordException("Financial Record Not Found");
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

        //GeT Financial Record For EMPLOYEE
        public void GetFinancialRecordsForEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM  FinancialRecord Where EmployeeID = @empId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", employeeId);
                bool flag = false;
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord();
                        record.RecordID = (int)reader["RecordID"];
                        record.EmployeeID = (int)reader["EmployeeID"];
                        record.RecordDate = (DateTime)reader["RecordDate"];
                        record.Description = (string)reader["Description"];
                        record.amount = (int)reader["Amount"];
                        record.RecordType = (string)reader["RecordType"];
                        IFinancialRecordService service = new FinancialRecordService();
                        service.GetFinancialRecordsForEmployee(record);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new FinancialRecordException("Financial Record Not Found");
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

        //Get Financial Record For ID
        internal void GetFinancialRecordById(int financeID)
        {
            using(SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM FinancialRecord Where RecordID = @new";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@new", financeID);
                try
                {
                    connection.Open();
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord();
                        record.RecordID = (int)reader["RecordID"];
                        record.EmployeeID = (int)reader["EmployeeID"];
                        record.RecordDate = (DateTime)reader["RecordDate"];
                        record.Description = (string)reader["Description"];
                        record.amount = (int)reader["Amount"];
                        record.RecordType = (string)reader["RecordType"];
                        IFinancialRecordService service = new FinancialRecordService();
                        service.GetFinancialRecordById(record);
                    }
                    else
                    {
                        throw new FinancialRecordException("Finance Record Not Found");
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

        //ID Display
        internal void FinancialIdList()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT RecordID FROM FinancialRecord";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord();
                        record.RecordID = (int)reader["RecordID"];
                        IFinancialRecordService financialRecordService = new FinancialRecordService();
                        financialRecordService.FinancialId(record);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new FinancialRecordException("Records Not Found");
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

        //RECORD DATE Display
        public void RecordDateList()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT RecordDate FROM FinancialRecord";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord();
                        record.RecordDate = (DateTime)reader["RecordDate"];
                        IFinancialRecordService financialRecordService = new FinancialRecordService();
                        financialRecordService.FinancialRecordDate(record);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new FinancialRecordException("Records Not Found");
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
    }
}
