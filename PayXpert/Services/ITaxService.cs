using PayXpert.Model;

namespace PayXpert.Services
{
    internal interface ITaxService
    {
        public double CalculateTax(double amount);

        public void GetTaxById(Tax tax);

        public void GetTaxesForEmployee(Tax tax);

        public void GetTaxesForYear(Tax tax);

        public void GetTaxesForYear(string taxYear);
        void TaxDate(Tax tax);
        void TaxId(Tax tax);
    }
}
