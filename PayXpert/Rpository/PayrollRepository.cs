using PayXpert.Exceptiions;
using PayXpert.Model;
using PayXpert.Services;
using PayXpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Rpository
{
    internal class PayrollRepository
    {
        string sqlConnection = null;
        SqlCommand cmd = null;

        public PayrollRepository()
        {
            sqlConnection = DbConnUtil.GetConnectionString();
            //sqlConnection = "Server=PRASHI;Database=PayXpert;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";
            cmd = new SqlCommand();
        }

        //Get Payroll By ID
        public void GetPayrollById(int payrollID)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Payroll Where PayrollID = @payId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@payId", payrollID);
                try
                {
                    connection.Open();
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.EmployeeID = (int)reader["EmployeeID"];
                        payroll.PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"];
                        payroll.PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"];
                        payroll.BasicSalary = (int)reader["BasicSalary"];
                        payroll.OvertimePay = (int)reader["OvertimePay"];
                        payroll.Deduction = (int)reader["Deductions"];
                        payroll.NetSalary = (int)reader["NetSalary"];
                        IPayrollService payrollService = new PayrollService();
                        payrollService.GetPayrollById(payroll);
                    }
                    else
                    {
                        throw new Exception("Payroll Not Found");
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

        //Generate Payroll
        internal void GeneratePayroll(string employeeId, DateTime? startDate, DateTime? endDate, string salary, string pay, string deduct, double netSalary)
        {
            int payrollId = 0;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT Top 1 * FROM Payroll ORDER BY PayrollID DESC";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        payrollId = (int)reader["PayrollID"];
                    }
                    else
                    {
                        throw new PayrollGenerationException("Can't Add Values to the Table Payroll.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                    payrollId++;
                }
            }
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "INSERT INTO Payroll (PayrollID, EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OverTimePay, Deductions, NetSalary) VALUES (@payid , @empid , @start , @end , @salary , @over , @deduction , @net )";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@payid", payrollId);
                cmd.Parameters.AddWithValue("@empid", employeeId);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@over", pay);
                cmd.Parameters.AddWithValue("@deduction", deduct);
                cmd.Parameters.AddWithValue("@net", netSalary);
                if (startDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@start", startDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@start", DBNull.Value);
                }
                //dob
                if (endDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@end", endDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@end", DBNull.Value);
                }
               
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    int reader = cmd.ExecuteNonQuery();
                    if (reader > 0)
                    {
                        Console.WriteLine("Successfully Added Payroll Details.");
                        Console.WriteLine($"The Assign Payroll ID is {payrollId}");
                    }
                    else
                    {
                        throw new PayrollGenerationException("Can't Add Values to the Table Payroll.");
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

        //Get Payroll For EMPLOYEE
        internal void GetPayrollForEmplloyee(int employeeID)
        {
            //Console.WriteLine($"---------Payroll For Employee Id : {employeeID}-----------");
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Payroll Where EmployeeID = @empId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", employeeID);
                bool flag = false;
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["PayrollID"];
                        payroll.EmployeeID = (int)reader["EmployeeID"];
                        payroll.PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"];
                        payroll.PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"];
                        payroll.BasicSalary = (int)reader["BasicSalary"];
                        payroll.OvertimePay = (int)reader["OvertimePay"];
                        payroll.Deduction = (int)reader["Deductions"];
                        payroll.NetSalary = (int)reader["NetSalary"];
                        IPayrollService payrollService = new PayrollService();
                        payrollService.GetPayrollsForEmployee(payroll);
                        flag = true;
                    }
                    if(!flag) 
                    {
                        throw new EmployeeNotFoundException("Employee Not Found");
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

        //Get Payroll For PERIOD
        public void GetPayrollForPeriod(DateTime? startDate, DateTime? endDate)
        {
            //Console.WriteLine($"---------Pyroll for Period: {startDate} to {endDate}-----------");
            List<Payroll> payrollList = new List<Payroll>();
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Payroll";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["PayrollID"];
                        payroll.EmployeeID = (int)reader["EmployeeID"];
                        payroll.PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"];
                        payroll.PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"];
                        payroll.BasicSalary = (int)reader["BasicSalary"];
                        payroll.OvertimePay = (int)reader["OvertimePay"];
                        payroll.Deduction = (int)reader["Deductions"];
                        payroll.NetSalary = (int)reader["NetSalary"];
                        IPayrollService payrollService = new PayrollService();
                        if(payrollService.CheckTimelyPayroll(payroll, (DateTime)startDate, (DateTime)endDate))
                        {
                            payrollList.Add(payroll);
                        }
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
                    PayrollService payroll = new PayrollService();
                    payroll.GetPayrollsForPeriod(payrollList);
                }
            }
        }

        //ID Disply
        public void PayrollList()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT PayrollID FROM Payroll";
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    bool flag = false;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["payrollID"];
                        IPayrollService payrollService = new PayrollService();
                        payrollService.payrollId(payroll);
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
