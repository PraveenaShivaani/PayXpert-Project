namespace PayXpert.Model
{
    public class Tax
    {
        public int TaxID { get; set; }

        public int EmployeeID { get; set;}

        public int TaxYear { get; set;}

        public int TaxableIncome { get; set;}

        public int TaxAmount { get; set; }
    }
}
