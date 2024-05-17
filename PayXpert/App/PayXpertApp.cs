using PayXpert.Model;
using PayXpert.Rpository;
using PayXpert.Services;
using PayXpert.Validation;
using System;
using System.Data.SqlTypes;


namespace PayXpert.App
{
    internal class PayXpertApp
    {
        //Object for Input Validation 
        InputValidation validator = new InputValidation();
        internal void Run()
        {
            //Object Creation For Repositories
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();

            FinancialRecordRepository financialRecordRepository = new FinancialRecordRepository();

            PayrollRepository payrollRepository = new PayrollRepository();

            TaxRepostitory taxRepostitory = new TaxRepostitory();

            ReportRepository reportRepository = new ReportRepository();

            bool loop = true;
            while (loop)
            {

                Console.WriteLine("============================================================================");
                Console.WriteLine("                       WELCOME ..! PayXpert List                            ");
                Console.WriteLine("============================================================================");
                Console.WriteLine("1 . Get Employee By Id");
                Console.WriteLine("2 . Get All Employees");
                Console.WriteLine("3 . Add Employee");
                Console.WriteLine("4 . Update Employee");
                Console.WriteLine("5 . Remove Employee");
                Console.WriteLine("6 . Generate Payroll");
                Console.WriteLine("7 . Get Payroll By Id");
                Console.WriteLine("8 . Get Payrolls For Employee");
                Console.WriteLine("9 . Get Payrolls For Period");
                Console.WriteLine("10. Calculate Tax");
                Console.WriteLine("11. Get Tax By Id");
                Console.WriteLine("12. Get Taxes For Employee");
                Console.WriteLine("13. Get Taxes For Year");
                Console.WriteLine("14. Add Financial Record");
                Console.WriteLine("15. Get Financial Record By Id");
                Console.WriteLine("16. Get Financial Records For Employee");
                Console.WriteLine("17. Get Financial Records For Date");
                Console.WriteLine("18. Generate Records");
                Console.WriteLine("19. Exit");

                //Getting Input Option From The User

                Console.WriteLine("Enter Your Choice :: ");
                int choice = int.Parse(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        GetEmployeeById(employeeDetailsRepository);
                        break;
                    case 2:
                        GetAllEmployees(employeeDetailsRepository);
                        break;
                    case 3:
                        AddEmployee(employeeDetailsRepository);
                        break;
                    case 4:
                        UpdateEmployee(employeeDetailsRepository);
                        break;
                    case 5:
                        RemoveEmployee(employeeDetailsRepository);
                        break;
                    case 6:
                        GeneratePayroll(payrollRepository); //one month difference check
                        break;
                    case 7:
                        GetPayrollById(payrollRepository);
                        break;
                    case 8:
                        GetPayrollsForEmployee(payrollRepository);
                        break;
                    case 9:
                        GetPayrollsForPeriod(payrollRepository);
                        break;
                    case 10:
                        CalculateTax(taxRepostitory);
                        break;
                    case 11:
                        GetTaxById(taxRepostitory);
                        break;
                    case 12:
                        GetTaxesForEmployee(taxRepostitory);
                        break;
                    case 13:
                        GetTaxesForYear(taxRepostitory);
                        break;
                    case 14:
                        AddFinancialRecord(financialRecordRepository);
                        break;
                    case 15:
                        GetFinancialRecordById(financialRecordRepository);
                        break;
                    case 16:
                        GetFinancialRecordsForEmployee(financialRecordRepository);
                        break;
                    case 17:
                        GetFinancialRecordsForDate(financialRecordRepository);
                        break;
                    case 18:
                        GenerateReport(reportRepository);
                        break;
                    case 19:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }
        }

        //1.Get Employee By ID
        private void GetEmployeeById(EmployeeDetailsRepository employeeDetailsRepository)
        {
            Console.WriteLine("List Of Employee Id");
            employeeDetailsRepository.EmploeeIdList();
        GetEmpById:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if(EmployeeID == 0)
            {
                goto GetEmpById;
            }
            else
            {
                employeeDetailsRepository.GetEmployeeById(EmployeeID);
            }
        }

        //2.Get All Employees
        private void GetAllEmployees(EmployeeDetailsRepository employeeDetailsRepository)
        {
            employeeDetailsRepository.GetAllEmployees();
        }

        //3.Add Employee
        private void AddEmployee(EmployeeDetailsRepository employeeDetailsRepository)
        {
        firstName:
            Console.WriteLine("Enter Your First Name ::");
            string FirstName = validator.NameValidation(Console.ReadLine());
            if (FirstName == "")
            {
                goto firstName;
            }
        lastName:
            Console.WriteLine("Enter Your Last Name ::");
            string LastName = validator.NameValidation(Console.ReadLine());
            if (LastName == "")
            {
                goto lastName;
            }
        DateOfBirth:
            Console.WriteLine("Enter Your Date of Birth ::");
            DateTime? DateOfBirth = validator.DateValidation(Console.ReadLine());
            if (DateOfBirth == null)
            {
                goto DateOfBirth;
            }
        Gender:
            Console.WriteLine("Enter Your Gender (Female/Male/Others)::");
            string Gender = validator.GenderValidation(Console.ReadLine());
            if (Gender == null)
            {
                goto Gender;
            }
        Email:
            Console.WriteLine("Enter Your Email ::");
            string Email = validator.EmailValidation(Console.ReadLine());
            if (Email == null)
            {
                goto Email;
            }
        PhoneNumber:
            Console.WriteLine("Enter Your Phone Number ::");
            string PhoneNumber = validator.PhoneNumberValidation(Console.ReadLine());
            if (PhoneNumber == null)
            {
                goto PhoneNumber;
            }
            Console.WriteLine("Enter Your Address ::");
            string Address = Console.ReadLine();
        Position:
            Console.WriteLine("Enter Your Position (eg. Manager,Sales Associative,Accountant,Developer,HR Manager) ::");
            string Position = validator.PositionValidation(Console.ReadLine());
            if (Position == null)
            {
                goto Position;
            }
        JoiningDate:
            Console.WriteLine("Enter Your Joining Date ::");
            DateTime? JoiningDate = validator.DateValidation(Console.ReadLine());
            if (JoiningDate == null)
            {
                goto JoiningDate;
            }
        TerminationDate:
            Console.WriteLine("Enter Your Termination Date or Null::");
            string Date = Console.ReadLine();
            DateTime? TerminationDate = null;
            if (Date.ToUpper() == "NULL")
            {
                TerminationDate = null;
            }
            else
            {
                TerminationDate = validator.DateValidation(Date);
                if (TerminationDate == null)
                {
                    goto TerminationDate;
                }
            }
            employeeDetailsRepository.AddEmpployee(FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate);

            //employeeDetailsRepository.AddEmpployee("selvi", "sekar", null, "female","selvi@gmail.com", "7864536789", "namakkal", "manager", null, null);
        }

        //4.Update Employee
        private void UpdateEmployee(EmployeeDetailsRepository employeeDetailsRepository)
        {
            Console.WriteLine("Enter the Column To Update ::");
            Console.WriteLine("1. FirstName");
            Console.WriteLine("2. LastName");
            Console.WriteLine("3. Date Of Birth");
            Console.WriteLine("4. Gender");
            Console.WriteLine("5. Email");
            Console.WriteLine("6. Phone Number");
            Console.WriteLine("7. Address");
            Console.WriteLine("8. Position");
            Console.WriteLine("9. Joining Date");
            Console.WriteLine("10. Termination Date");

            Console.WriteLine("Enter Your Choice");
            int choice = int.Parse(Console.ReadLine());

            Console.WriteLine("List Of Employee Id");
            employeeDetailsRepository.EmploeeIdList();
            Console.WriteLine("Enter Your Employee ID");
            int EmployeeID = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    firstName:
                    Console.WriteLine("Enter Your First Name ::");
                    string FirstName = validator.NameValidation(Console.ReadLine());
                    if (FirstName == "")
                    {
                        goto firstName;
                    }
                    employeeDetailsRepository.FieldChange("FirstName",FirstName,EmployeeID);
                    break;
                case 2:
                    lastName:
                    Console.WriteLine("Enter Your Last Name ::");
                    string LastName = validator.NameValidation(Console.ReadLine());
                    if (LastName == "")
                    {
                        goto lastName;
                    }
                    employeeDetailsRepository.FieldChange("LastName", LastName, EmployeeID);
                    break; 
                case 3:
                    DateOfBirth:
                    Console.WriteLine("Enter Your Date of Birth ::");
                    DateTime? DateOfBirth = validator.DateValidation(Console.ReadLine());
                    if (DateOfBirth == null)
                    {
                        goto DateOfBirth;
                    }
                    employeeDetailsRepository.FieldChange("DateOfBirth", DateOfBirth, EmployeeID);
                    break;
                case 4:
                    Gender:
                    Console.WriteLine("Enter Your Gender (Female/Male/Others)::");
                    string Gender = validator.GenderValidation(Console.ReadLine());
                    if (Gender == null)
                    {
                        goto Gender;
                    }
                    employeeDetailsRepository.FieldChange("Gender", Gender, EmployeeID);
                    break;
                case 5:
                    Email:
                    Console.WriteLine("Enter Your Email ::");
                    string Email = validator.EmailValidation(Console.ReadLine());
                    if (Email == null)
                    {
                        goto Email;
                    }
                    employeeDetailsRepository.FieldChange("Email", Email, EmployeeID);
                    break;
                case 6:
                    PhoneNumber:
                    Console.WriteLine("Enter Your Phone Number ::");
                    string PhoneNumber = validator.PhoneNumberValidation(Console.ReadLine());
                    if (PhoneNumber == null)
                    {
                        goto PhoneNumber;
                    }
                    employeeDetailsRepository.FieldChange("PhoneNumber", PhoneNumber, EmployeeID);
                    break;
                case 7:
                    Console.WriteLine("Enter Your Address ::");
                    string Address = Console.ReadLine();
                    employeeDetailsRepository.FieldChange("Address", Address, EmployeeID);
                    break;
                case 8:
                    Position:
                    Console.WriteLine("Enter Your Position (eg. Manager,Sales Associative,Accountant,Developer,HR Manager) ::");
                    string Position = validator.PositionValidation(Console.ReadLine());
                    if (Position == null)
                    {
                        goto Position;
                    }
                    employeeDetailsRepository.FieldChange("Position", Position, EmployeeID);
                    break;
                case 9:
                    JoiningDate:
                    Console.WriteLine("Enter Your Joining Date ::");
                    DateTime? JoiningDate = validator.DateValidation(Console.ReadLine());
                    if (JoiningDate == null)
                    {
                        goto JoiningDate;
                    }
                    employeeDetailsRepository.FieldChange("JoiningDate", JoiningDate, EmployeeID);
                    break;
                case 10:
                    TerminationDate:
                    Console.WriteLine("Enter Your Termination Date or Null::");
                    string Date = Console.ReadLine();
                    DateTime? TerminationDate = null;
                    if (Date.ToUpper() == "NULL")
                    {
                        TerminationDate = null;
                    }
                    else
                    {
                        TerminationDate = validator.DateValidation(Date);
                        if (TerminationDate == null)
                        {
                            goto TerminationDate;
                        }
                    }
                    employeeDetailsRepository.FieldChange("TerminationDate", TerminationDate, EmployeeID);
                    break;
                default:
                    Console.WriteLine("Give a Valid input");
                    break;
            }
        }

        //5.Remove Employee
        private void RemoveEmployee(EmployeeDetailsRepository employeeDetailsRepository)
        {
            Console.WriteLine("List Of Employee Id");
            employeeDetailsRepository.EmploeeIdList();
        RemoveEmployee:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if (EmployeeID == 0)
            {
                goto RemoveEmployee;
            }
            else
            {
                employeeDetailsRepository.RemoveEmployee(EmployeeID);
            }
        }

        //-------------------------------------PAYROLL--------------------------------
        //6. GeneratePayroll
        private void GeneratePayroll(PayrollRepository payrollRepository)
        {
            Console.WriteLine("List Of Employee Id");
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();
            employeeDetailsRepository.EmploeeIdList();
        GetEmpId:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if (EmployeeID == 0)
            {
                goto GetEmpId;
            }
            if (!employeeDetailsRepository.CheckEmployeeID(EmployeeID))
            {
                Console.WriteLine("Incorrect Employee ID");
                goto GetEmpId;
            }

            StartDate:
            Console.WriteLine("Enter the Pay Period Start Date ::");
            DateTime? StartDate = validator.DateValidation(Console.ReadLine());
            if (StartDate == null)
            {
                goto StartDate;
            }

            lastDate:
            Console.WriteLine("Enter the Pay Period Last Date ::");
            DateTime? LastDate = validator.DateValidation(Console.ReadLine());
            if (LastDate == null)
            {
                goto lastDate;
            }

            BasicSalary:
            Console.WriteLine("Enter the Basic Salary ::");
            String Salary = Console.ReadLine();
            double? value = validator.MoneyValidation(Salary);
            if (value == null)
            {
                goto BasicSalary;
            }

            Overtimepay:
            Console.WriteLine("Enter the Over Time Pay ::");
            String Pay = Console.ReadLine();
            double? pay = validator.MoneyValidation(Pay);
            if (pay == null)
            {
                goto Overtimepay;
            }

            Deduction:
            Console.WriteLine("Enter the Deduction ::");
            String Deduct = Console.ReadLine();
            double? deduct = validator.MoneyValidation(Deduct);
            if (deduct == null)
            {
                goto Deduction;
            }

            IPayrollService payrollService = new PayrollService();
            double NetSalary = payrollService.NetSalaryCalculate((double)value, (double)pay,(double)deduct);

            if (payrollService.CheckTimePeriod((DateTime)StartDate, (DateTime)LastDate))
            {
                payrollRepository.GeneratePayroll(EmployeeId,StartDate,LastDate,Salary,Pay,Deduct,NetSalary);
            }
        }

        //7. Get Payroll By ID
        private void GetPayrollById(PayrollRepository payrollRepository)
        {
            Console.WriteLine("List Of Payroll Id");
            payrollRepository.PayrollList();
        GetPayrollById:
            Console.WriteLine("Enter the PayrollID :: ");
            String PayrollId = Console.ReadLine();
            int PayrollID = validator.EmployeeIDValidation(PayrollId);
            if (PayrollID == 0)
            {
                goto GetPayrollById;
            }
            else
            {
                payrollRepository.GetPayrollById(PayrollID);
            }
        }

        //8. Get Payrolls For Employee
        private void GetPayrollsForEmployee(PayrollRepository payrollRepository)
        {
            Console.WriteLine("List Of Employee Id");
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();
            employeeDetailsRepository.EmploeeIdList();
        GetEmpId:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if (EmployeeID == 0)
            {
                goto GetEmpId;
            }
            if (!employeeDetailsRepository.CheckEmployeeID(EmployeeID)) //Check wherther the employeeId is a crt foriegn key
            {
                Console.WriteLine("Incorrect Employee ID");
                goto GetEmpId;
            }
            payrollRepository.GetPayrollForEmplloyee(EmployeeID);
        }


        //9. Get Payrolls For Period
        private void GetPayrollsForPeriod(PayrollRepository payrollRepository)
        {
        StartDate:
            Console.WriteLine("Enter the Pay Period Start Date ::");
            DateTime? StartDate = validator.DateValidation(Console.ReadLine());
            if (StartDate == null)
            {
                goto StartDate;
            }

        lastDate:
            Console.WriteLine("Enter the Pay Period End Date ::");
            DateTime? EndDate = validator.DateValidation(Console.ReadLine());
            if (EndDate == null)
            {
                goto lastDate;
            }

            payrollRepository.GetPayrollForPeriod(StartDate,EndDate);
        }

        //------------------------------FINANCIAL RECORD----------------------------------

        //Get Financial Records For Date
        private void GetFinancialRecordsForDate(FinancialRecordRepository financialRecordRepository)
        {
            Console.WriteLine("List Of Financial Record Date");
            financialRecordRepository.RecordDateList();
        GetId:
            Console.WriteLine("Enter the Financial Record Date :: ");
            String RecordDate = Console.ReadLine();
            DateTime? FRecordDate = validator.DateValidation(RecordDate);
            if (FRecordDate == null)
            {
                goto GetId;
            }
            financialRecordRepository.GetFinancialRecordsForDate(FRecordDate);
        }

        // Get Finance For EMPLOYEE
        private void GetFinancialRecordsForEmployee(FinancialRecordRepository financialRecordRepository)
        {
            Console.WriteLine("List Of Employee Id");
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();
            employeeDetailsRepository.EmploeeIdList();
        GetId:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if (EmployeeID == 0)
            {
                goto GetId;
            }
            if (!employeeDetailsRepository.CheckEmployeeID(EmployeeID)) //Check wherther the employeeId is a crt foriegn key
            {
                Console.WriteLine("Incorrect Employee ID");
                goto GetId;
            }
            financialRecordRepository.GetFinancialRecordsForEmployee(EmployeeID);
        }

        //Get Financial Record BY ID
        private void GetFinancialRecordById(FinancialRecordRepository financialRecordRepository)
        {
            Console.WriteLine("List Of Financial Id");
            financialRecordRepository.FinancialIdList();
        GetFinanceById:
            Console.WriteLine("Enter the Finance ID :: ");
            String FinanceId = Console.ReadLine();
            int FinanceID = validator.FinancialIDValidation(FinanceId);
            if (FinanceID == 0)
            {
                goto GetFinanceById;
            }
            else
            {
                financialRecordRepository.GetFinancialRecordById(FinanceID);
            }
        }


        //Add Financial Record
        private void AddFinancialRecord(FinancialRecordRepository financialRecordRepository)
        {
            Console.WriteLine("List Of Employee Id");
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();
            employeeDetailsRepository.EmploeeIdList();
        Get:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int employeeId = validator.EmployeeIDValidation(EmployeeId);
            if (employeeId == 0)
            {
                goto Get;
            }
            if (!employeeDetailsRepository.CheckEmployeeID(employeeId))
            {
                Console.WriteLine("Incorrect Employee ID");
                goto Get;
            }

            DateTime date = DateTime.Now;

            Console.WriteLine("Enter the Description ::");
            string description = Console.ReadLine();
            

        amt:
            Console.WriteLine("Enter the Amount ::");
            String Amount = Console.ReadLine();
            double? amount = validator.MoneyValidation(Amount);
            if (amount == null)
            {
                goto amt;
            }

        RecordType:
            Console.WriteLine("Enter the Record Type (credit/debit)::");
            String Type = Console.ReadLine();
            string type = validator.RecordTypeValidation(Type);
            if (type == null)
            {
                goto RecordType;
            }


            financialRecordRepository.AddFinancialRecord(employeeId,date,description,amount,type.ToLower());
            
        }


        //-----------------------------------------------------TAX-------------------------------------------

        //13. Get Taxes For YEAR
        private void GetTaxesForYear(TaxRepostitory taxRepostitory)
        {
            Console.WriteLine("List Of Tax Year");
            taxRepostitory.TaxYearList();
        Getyear:
            Console.WriteLine("Enter the Tax Year :: ");
            string Year = Console.ReadLine();
            int year = validator.YearValidation(Year);
            if (year == 0)
            {
                goto Getyear;
            }
            taxRepostitory.GetTaxesForyear(year);
        }

        //12.Get Tax For Employee
        private void GetTaxesForEmployee(TaxRepostitory taxRepostitory)
        {
            Console.WriteLine("List Of Employee Id");                               //referenge the same employee function
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();
            employeeDetailsRepository.EmploeeIdList();
        EmpId:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if (EmployeeID == 0)
            {
                goto EmpId;
            }
            if (!employeeDetailsRepository.CheckEmployeeID(EmployeeID)) //Check wherther the employeeId is a crt foriegn key
            {
                Console.WriteLine("Incorrect Employee ID");
                goto EmpId;
            }
            taxRepostitory.GetTaxesForEmployee(EmployeeID);
        }

        //11.Get Tax By ID
        private void GetTaxById(TaxRepostitory taxRepostitory)
        {
            Console.WriteLine("List Of Tax Id");
            taxRepostitory.TaxIdList();
            GetTaxById:
            Console.WriteLine("Enter the Tax ID :: ");
            String TaxId = Console.ReadLine();
            int TaxID = validator.FinancialIDValidation(TaxId);
            if (TaxID == 0)
            {
                goto GetTaxById;
            }
            else
            {
                taxRepostitory.GetTaxById(TaxID);
            }
        }

        //Calucalte Tax
        private void CalculateTax(TaxRepostitory taxRepostitory)
        {
            //Console.WriteLine("List Of Tax Year");  //Getting Year
            //taxRepostitory.TaxYearList();
        Getyear:
            Console.WriteLine("Enter the Tax Year :: ");
            string Year = Console.ReadLine();
            int year = validator.YearValidation(Year);
            if (year == 0)
            {
                goto Getyear;
            }
            Console.WriteLine("List Of Employee Id");         //Displaying Id of Employee
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();
            employeeDetailsRepository.EmploeeIdList();
            Id:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if (EmployeeID == 0)
            {
                goto Id;
            }
            else
            {
                taxRepostitory.CalculateTax(EmployeeID,year);
            }
        }

        //----------------------------------------------Test Case----------------------------------------------

        //Calculate Net Salary For Employee
        //public void CalculateNetSalary()
        //{
            
        //}

        //------------------------------------
        public void GenerateReport(ReportRepository reportRepository)
        {
            Console.WriteLine("List Of Employee Id");
            EmployeeDetailsRepository employeeDetailsRepository = new EmployeeDetailsRepository();
            employeeDetailsRepository.EmploeeIdList();
        report:
            Console.WriteLine("Enter the EmployeeID :: ");
            String EmployeeId = Console.ReadLine();
            int EmployeeID = validator.EmployeeIDValidation(EmployeeId);
            if (EmployeeID == 0)
            {
                goto report;
            }
            else
            {
                reportRepository.GenerateReport(EmployeeID);
            }

        }

    }
}