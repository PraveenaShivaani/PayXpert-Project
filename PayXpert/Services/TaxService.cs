using PayXpert.Model;

namespace PayXpert.Services
{
    public class TaxService : ITaxService
    {

        //Calculate Tax
        public double CalculateTax(double amount)
        {
            double rate = 0;
            if(amount < 100000)
            {
                rate = 0.10;
            }
            else
            {
                rate = 0.15;
            }
            return amount * rate;
        }

        //Get Tax By ID
        public void GetTaxById(Tax tax)
        {
            Console.WriteLine($" \n Employee ID :: {tax.EmployeeID} \n Tax Year :: {tax.TaxYear} " +
                $"\n Taxable Income :: {tax.TaxableIncome} \n Tax Amount :: {tax.TaxAmount} \n");
        }

        //Get Tax For EMPLOYEE
        public void GetTaxesForEmployee(Tax tax)
        {
                Console.WriteLine($"Tax ID :: {tax.TaxID}  \n Tax Year :: {tax.TaxYear} \n  " +
                $"Taxable Income :: {tax.TaxableIncome} \n TaxAmount :: {tax.TaxAmount} \n");
        }

        //Get Taxes For Year
        public void GetTaxesForYear(string taxYear)
        {
            throw new NotImplementedException();
        }

        //YEAR Display
        public void TaxDate(Tax tax)
        {
            Console.WriteLine($"Tax Year :: {tax.TaxYear}");
        }

        //ID display
        public void TaxId(Tax tax)
        {
            Console.WriteLine($"Tax ID :: {tax.TaxID}");
        }

        //Generate Taxes For Year
        public void GetTaxesForYear(Tax tax)
        {
            Console.WriteLine($"Tax ID :: {tax.TaxID}  \n Employee ID :: {tax.EmployeeID} \n  " +
                $"Taxable Income :: {tax.TaxableIncome} \n TaxAmount :: {tax.TaxAmount} \n");
        }
    }
}
