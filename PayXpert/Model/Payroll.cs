namespace PayXpert.Model
{
    public class Payroll //Entity Class
    {
        public int PayrollID { get; set; }

        public int EmployeeID { get; set; }
        
        public DateTime PayPeriodStartDate { get; set;}

        public DateTime PayPeriodEndDate { get; set;}

        public int BasicSalary { get; set;}

        public int OvertimePay { get; set; }

        public int Deduction {  get; set; }

        public int NetSalary { get; set; }
    
    }
}
