using PayXpert.Exceptiions;
using PayXpert.Model;

namespace PayXpert.Services
{
    public class PayrollService : IPayrollService
    {
        

        public void GeneratePayroll(int employeeId, string startDate, string endDate)
        {
            throw new NotImplementedException();
        }

        //Get Payroll BY ID
        public void GetPayrollById(Payroll payroll)
        {
            Console.WriteLine($"Employee ID :: {payroll.EmployeeID} \n Basic Salary :: {payroll.BasicSalary} \n Deduction :: {payroll.Deduction} \n " +
                $"Over Time Pay :: {payroll.OvertimePay} \n Net Salary :: {payroll.NetSalary} \n");
        }

        //Get Payroll For Employee
        public void GetPayrollsForEmployee(Payroll payroll)
        {
            Console.WriteLine($"Payroll ID :: {payroll.PayrollID} \n Basic Salary :: {payroll.BasicSalary} \n Deduction :: {payroll.Deduction} \n " +
                $"Over Time Pay :: {payroll.OvertimePay} \n Net Salary :: {payroll.NetSalary} \n");
        }

        //List return
     

        //Net Salary Calculation
        public double NetSalaryCalculate(double value, double pay, double deduct)
        {
            double NetSalary = value + pay - deduct;
            return NetSalary;
        }

        //Print Payroll
        public void payrollId(Payroll payroll)
        {
            Console.WriteLine($"Payroll ID :: {payroll.PayrollID}");
        }

        //Check Time Period
        public bool CheckTimePeriod(DateTime startDate, DateTime lastDate)
        {
            try
            {
                TimeSpan difference = lastDate - startDate;

                if (difference.TotalDays < 30)

                {
                    return true;
                }

                else
                {
                    throw new PayrollGenerationException("It exceeds a Month duration...");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Check Timely Payroll
        public bool CheckTimelyPayroll(Payroll payroll, DateTime startDate, DateTime endDate)
        {
            TimeSpan differenceStart = payroll.PayPeriodStartDate - startDate;        // 1 2 3 4 5 must be grater than 0
            TimeSpan differenceEnd = endDate - payroll.PayPeriodEndDate;
            if( differenceStart.TotalDays > 0 && differenceEnd.TotalDays > 0) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetPayrollsForPeriod(List<Payroll> payrollList)
        {
            foreach (Payroll payroll in payrollList)
            {
                Console.WriteLine($"Payroll ID :: {payroll.PayrollID} \n Employee ID :: {payroll.EmployeeID} \nBasic Salary :: {payroll.BasicSalary} \n Deduction :: {payroll.Deduction} \n " +
                $"Over Time Pay :: {payroll.OvertimePay} \n Net Salary :: {payroll.NetSalary} \n");
            }
        }

        //Gross Calculation
        public int CalculateGrossSalary(int amount, int overtime)
        {
            return amount + overtime;
        }
    }
}
