using PayXpert.Exceptiions;
using PayXpert.Exceptions;
using PayXpert.Model;
using PayXpert.Services;
using PayXpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Rpository
{
    internal class ReportRepository
    {
        string sqlConnection = null;
        SqlCommand cmd = null;

        public ReportRepository()
        {
            sqlConnection = DbConnUtil.GetConnectionString();
            //sqlConnection = "Server=PRASHI;Database=PayXpert;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";
            cmd = new SqlCommand();
        }

        public void GenerateReport(int EmployeeID)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Employee Where EmployeeID = @empId";  //employee
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
            
            using (SqlConnection connection = new SqlConnection(sqlConnection)) //payroll
            {
                cmd.CommandText = "SELECT * FROM Employee as E " +
                    "INNER JOIN Payroll as P ON E.EmployeeID = P.EmployeeID " +
                    "Where E.EmployeeID=@empId ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", EmployeeID);
                bool flag = false;
                try
                {
                    connection.Open();
                    cmd.Connection = connection;
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
                        payrollService.GetPayrollsForEmployee(payroll);
                        flag = true;
                    }
                    if (!flag)
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
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                cmd.CommandText = "SELECT * FROM Employee as E " +
                    "INNER JOIN FinancialRecord as F " +
                    "ON E.EmployeeID = F.EmployeeID " +
                    "Where E.EmployeeID=@empId;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@empId", EmployeeID);
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
    }
}
