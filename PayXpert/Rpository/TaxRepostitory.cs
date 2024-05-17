using PayXpert.Exceptiions;
using PayXpert.Model;
using PayXpert.Services;
using PayXpert.Utility;
using System.Data.SqlClient;

namespace PayXpert.Rpository
{
    internal class TaxRepostitory
    {
        string sqlConnection = null;
        SqlCommand cmd = null;

        public TaxRepostitory()
        {
            sqlConnection = DbConnUtil.GetConnectionString();
            //sqlConnection = "Server=PRASHI;Database=PayXpert;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";
            cmd = new SqlCommand();
        }

        //Calculate Tax
        internal int CalculateTax(int employeeID,int year)
        {
            double sum =0;
            //double taxRate = 0.10;
            using (SqlConnection connection = new SqlConnection(sqlConnection)) //to take netsalary from table
            {
                cmd.CommandText = "SELECT * FROM Payroll Where EmployeeID = @empId and YEAR(PayPeriodStartDate) = @year and YEAR(PayPeriodEndDate) = @year ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", employeeID);
                cmd.Parameters.AddWithValue("@year", year);
                bool flag = false;
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.NetSalary = (int)reader["NetSalary"];
                        sum += payroll.NetSalary;
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new Exception("Tax Not Found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
            ITaxService taxService = new TaxService();
            double taxAmount = taxService.CalculateTax(sum);
            int taxId = 0;
            using (SqlConnection connection = new SqlConnection(sqlConnection)) //to generate tax id
            {
                cmd.CommandText = "SELECT Top 1 * FROM Tax ORDER BY TaxID DESC";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        taxId = (int)reader["TaxID"];
                    }
                    else
                    {
                        throw new Exception("Can't Add Values to the Table Tax.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
                finally
                {
                    connection.Close();
                    taxId++;
                }
            }
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "INSERT INTO Tax (TaxID, EmployeeID, TaxYear, TaxableIncome, TaxAmount) VALUES (@taxid , @empid , @year , @income , @amt )";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@taxid", taxId);
                cmd.Parameters.AddWithValue("@empid", employeeID);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@income", sum);
                cmd.Parameters.AddWithValue("@amt", taxAmount);
             
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    int reader = cmd.ExecuteNonQuery();
                    if (reader > 0)
                    {
                        Console.WriteLine($"The Tax Amount For the Employee ID ::{employeeID} For the Year {year} :: {taxAmount}");
                        
                    }
                    else
                    {
                        throw new Exception("Can't Add Values to the Table Tax.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
            return 0;
        }

        //Get Tax By ID
        internal void GetTaxById(int TaxID)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Tax Where TaxID = @TaxId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TaxId", TaxID);
                try
                {
                    connection.Open();
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.EmployeeID = (int)reader["EmployeeId"];
                        tax.TaxYear = (int)reader["TaxYear"];
                        tax.TaxableIncome = (int)reader["TaxableIncome"];
                        tax.TaxAmount = (int)reader["TaxAmount"];
                        ITaxService taxService = new TaxService();
                        taxService.GetTaxById(tax);
                    }
                    else
                    {
                        throw new EmployeeNotFoundException("Tax Not Found");
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

        //Get Taxes For EMPLOYEE
        internal void GetTaxesForEmployee(int employeeID)
        {
            //Console.WriteLine($"---------Taxes for Employee ID: {employeeID}-----------");
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Tax Where EmployeeID = @empId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", employeeID);
                bool flag = false;
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["TaxId"];
                        tax.EmployeeID = (int)reader["EmployeeId"];
                        tax.TaxYear = (int)reader["TaxYear"];
                        tax.TaxableIncome = (int)reader["TaxableIncome"];
                        tax.TaxAmount = (int)reader["TaxAmount"];
                        ITaxService taxService = new TaxService();
                        taxService.GetTaxesForYear(tax);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new Exception("Tax Not Found");
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

        //Get Taxes For Year
        internal void GetTaxesForyear(int year)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                //Console.WriteLine($"---------Taxes foryear: {year}-----------");
                cmd.CommandText = "SELECT * FROM Tax Where TaxYear = @year";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@year", year);
                bool flag = false;
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["TaxId"];
                        tax.EmployeeID = (int)reader["EmployeeId"];
                        tax.TaxYear = (int)reader["TaxYear"];
                        tax.TaxableIncome = (int)reader["TaxableIncome"];
                        tax.TaxAmount = (int)reader["TaxAmount"];
                        ITaxService taxService = new TaxService();
                        taxService.GetTaxesForEmployee(tax);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new Exception("Tax Not Found");
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
        internal void TaxIdList()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT TaxID FROM Tax";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["TaxID"];
                        ITaxService taxService = new TaxService();
                        taxService.TaxId(tax);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new Exception("Records Not Found");
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

        //YEAR Display
        internal void TaxYearList()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT TaxYear FROM Tax";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxYear = (int)reader["TaxYear"];
                        ITaxService taxService = new TaxService();
                        taxService.TaxDate(tax);
                        flag = true;
                    }
                    if (!flag)
                    {
                        throw new Exception("Records Not Found");
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
