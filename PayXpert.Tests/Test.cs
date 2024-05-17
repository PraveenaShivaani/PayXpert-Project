
using NUnit.Framework;
using PayXpert.Services;
using PayXpert.Validation;
using System;


namespace PayXpert.Tests
{
    public class Test
    {
        TaxService tax = new TaxService();
        PayrollService payroll = new PayrollService();
        InputValidation valid = new InputValidation();


        [TestCase(1200)]
        public void CalculateTax(double num1)
        {
            int expected = 120;
            double Tax = tax.CalculateTax(num1);
            Assert.That(expected, Is.EqualTo(Tax));
        }

        [TestCase(1000)]
        public void CalculateTax1(double num1)
        {
            int expected = 100;
            double Tax = tax.CalculateTax(num1);
            Assert.That(expected, Is.EqualTo(Tax));
        }

        //Net Salary Calculate

        [TestCase(1000,500,100)]
        public void NetSalaryCalculate(double num1, double num2 ,double num3) 
        {
            double expected = 1400;
            double Salary = payroll.NetSalaryCalculate(num1, num2, num3);
            Assert.That(expected, Is.EqualTo(Salary));

        }

        [TestCase(120000)]  //greater tax rate 15% 
        public void CalculateTax2(double num1)
        {
            int expected = 18000;
            double Tax = tax.CalculateTax(num1);
            Assert.That(expected, Is.EqualTo(Tax));
        }

        [TestCase(50000)]
        public void CalculateTax3(double num1)
        {
            int expected = 5000;
            double Tax = tax.CalculateTax(num1);
            Assert.That(expected, Is.EqualTo(Tax));
        }

        [TestCase("10")]

        public void CheckEmployeeId(string num)
        {
            int expected = 0;  
            int num1 = valid.EmployeeIDValidation(num);
            Assert.That(expected, Is.EqualTo(num1));
        }

        //Calculate Gross Salary For Employee

        [TestCase(3500,500)]
        public void CalculateGrossSalary(int num1,int num2)
        {
            int expected = 4000;
            int num = payroll.CalculateGrossSalary(num1,num2);
            Assert.That(expected, Is.EqualTo(num));
        }

        // Process Payroll For Employee
        [TestCase("2023-05-10", "2023-07-20")]
        public void TestMethod(string date1, string date2)
        {
            // Parse the string parameters to DateTime
            DateTime givenDate1 = DateTime.Parse(date1);
            DateTime givenDate2 = DateTime.Parse(date2);

            bool time =  payroll.CheckTimePeriod(givenDate1, givenDate2);
            // Now you can use givenDate1 and givenDate2 as DateTime objects
            Assert.That(time,Is.False);
        }
    }
}
