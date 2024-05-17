using PayXpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Services
{
    internal interface IPayrollService
    {
        bool CheckTimelyPayroll(Payroll payroll, DateTime startDate, DateTime endDate);
        bool CheckTimePeriod(DateTime startDate, DateTime lastDate);
        public void GeneratePayroll(int employeeId, string startDate, string endDate);

        public void GetPayrollById(Payroll payroll);

        public void GetPayrollsForEmployee(Payroll payroll);

        public void GetPayrollsForPeriod(List<Payroll> payrollList);
        double NetSalaryCalculate(double value, double pay, double deduct);
        void payrollId(Payroll payroll);
    }
}
