

namespace PayXpert.Model
{
    public class FinancialRecord //Entity Class - Which has only the props
    {
        public int RecordID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime RecordDate { get; set; }

        public string Description { get; set; }

        public int amount { get; set; }

        public string RecordType { get; set; }
    }
}
