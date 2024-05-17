using PayXpert.Model;

namespace PayXpert.Services
{
    internal class FinancialRecordService : IFinancialRecordService
    {
        public void AddFinancialRecord(int employeeId, string description, int amount, int recordType)
        {
            throw new NotImplementedException();
        }

        //Get Financial Record By ID
        public void GetFinancialRecordById(FinancialRecord record)
        {
            Console.WriteLine($" \n Employee ID :: {record.EmployeeID} \n Record Date :: {record.RecordDate} " +
                $"\n Description :: {record.Description} \n Amount :: {record.RecordType} \n");
        }

        //Get Financial Record For DATE
        public void GetFinancialRecordsForDate(FinancialRecord record)
        {
            Console.WriteLine($" \n Record ID :: {record.RecordID} \n Enployee ID :: {record.EmployeeID} " +
                $"\n Description :: {record.Description} \n Amount :: {record.RecordType} \n");
        }

        //Get Financial Records For EMPLOYEE
        public void GetFinancialRecordsForEmployee(FinancialRecord record)
        {
            Console.WriteLine($" \n Record ID :: {record.RecordID} \n Record Date :: {record.RecordDate} " +
                $"\n Description :: {record.Description} \n Amount :: {record.RecordType} \n");
        }

        //ID Display
        public void FinancialId(FinancialRecord record)
        {
            Console.WriteLine($"Financial ID :: {record.RecordID}");
        }

        //RECORD DATE Display
        public void FinancialRecordDate(FinancialRecord record)
        {
            DateOnly reecorddate = DateOnly.FromDateTime(record.RecordDate); //USING DateOnly.FormateDateTime -- to convert from DateTime ti DateOnly
            Console.WriteLine($"Financial Record Date :: {record.RecordDate}");
        }
    }
}
