using PayXpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Services
{
    internal interface IFinancialRecordService
    {
        public void AddFinancialRecord(int employeeId, string description, int amount, int recordType);

        public void GetFinancialRecordById(FinancialRecord record);

        public void GetFinancialRecordsForEmployee(FinancialRecord record);

        public void GetFinancialRecordsForDate(FinancialRecord record);
        void FinancialId(FinancialRecord record);
        void FinancialRecordDate(FinancialRecord record);
    }
}
