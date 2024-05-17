using PayXpert.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace PayXpert.Validation
{
    public class InputValidation
    {
        public InputValidation()
        {}

        // Validating the Employee ID input
        public int EmployeeIDValidation(string id)
        {
            try 
            {
                 int empId = int.Parse(id);          //Check for integer
            }
            catch(Exception ex)
            {
                Console.WriteLine("Employee Id should be an Integer");
                return 0;
            }
            try
            {
                int empId = int.Parse(id);
                if (empId >= 100)                   //check for value greater than 100
                {
                    return empId;
                }
                else
                {
                    throw new InvalidInputException("Employee Id Should be greater than 100");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //Employee First Name & Last Name Validation
        public string NameValidation(string Name)
        {
            string pattern = "^[a-zA-Z]*$"; //Regular expression pattern to match only letters
            try
            {
                if (Regex.IsMatch(Name, pattern))   // Check if the input string matches the pattern
                {
                    return Name;
                }
                else
                {
                    throw new InvalidInputException("Name Must Only Contain Alphabets");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        //Date Validation
        public DateTime? DateValidation(string DOB)
        {
            DateTime date = DateTime.Today;
            DateTime dob;
            try
            {
                if (DateTime.TryParse(DOB, out dob))       //DOB - String
                {
                    dob = DateTime.Parse(DOB);
                }
                else
                {
                    throw new Exception("Invalid Date Format");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            try                                             //Chech Whether date exceeds todays date
            {
                DateTime dateTime = DateTime.Parse(DOB);
                if(date > dateTime)
                {
                    return dateTime;
                }
                else
                {
                    throw new Exception("Date is Exceeding Today's Date");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //Gender Validation
        public string GenderValidation(string Gender)
        {
            string gender = Gender.ToUpper();
            try
            {
                if (gender == "MALE" || gender == "FEMALE" || gender == "OTHERS")
                {
                    return gender;
                }
                else
                {
                    throw new Exception("Invalid Gender");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Email Validation
        public string EmailValidation(string Email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            try
            {
                if (Regex.IsMatch(Email, pattern))
                {
                    return Email;
                }
                else
                {
                    throw new InvalidInputException("Invalid email address format.");
                }
            }
            catch(InvalidInputException e)      //Not user defined
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Phone Number Validation
        public string PhoneNumberValidation(string PhoneNumber)
        {
            //Validating phone nnumber
            string pattern = @"^\d{10}$";
            try
            {
                if (Regex.IsMatch(PhoneNumber, pattern))
                {
                    return PhoneNumber;
                }
                else
                {
                    throw new ArgumentException("Invalid phone number format. Please enter a 10-digit phone number.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Position Validation
        public string PositionValidation(string Position)
        {
            string position = Position.ToUpper();
            try
            {
                if (position == "MANAGER" || position == "HR MANAGER" || position == "DEVELOPER" || position == "SALES EXECUTIVE" || position == "ACCOUNTANT")
                {
                    return position;
                }
                else
                {
                    throw new InvalidInputException("Choose Relevant Position From The list above.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Money Validation
        internal double? MoneyValidation(string salary)
        {
            try
            {
                foreach (char i in salary)
                {
                    if (char.IsDigit(i) || i == '.')
                    {
                        continue;
                    }
                    else
                    {
                        throw new InvalidInputException("Please Enter olny Digits.");            //digit validation
                    }
                }
                if (salary[salary.Length - 1] == '.')
                {
                    throw new InvalidInputException("Please Place . in correct location");      //'.' validation
                }
                double money = double.Parse(salary);
                if(money <= 0)
                {
                    throw new Exception("Amount Should be Greater than 0");
                }
                else
                {
                    return money;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        //Finance Validation
        internal int FinancialIDValidation(string? financeId)
        {
            try
            {
                int FinanceId = int.Parse(financeId);          //Check for integer
            }
            catch (Exception ex)
            {
                Console.WriteLine("Finance Id should be an Integer");
                return 0;
            }
            try
            {
                int FinanceId = int.Parse(financeId);
                if (FinanceId >= 0)                   //check for value greater than 0 that is not negative
                {
                    return FinanceId;
                }
                else
                {
                    throw new InvalidInputException("Finance Id Should be greater than 0");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //YEAR Validation
        internal int  YearValidation(string year) 
        {
            int Year = 0;
            // Define the regex pattern to match only numbers
            string pattern = @"^\d+$";
            try 
            { 
                // Check if the input string matches the pattern
                if(Regex.IsMatch(year, pattern))    //check for string
                {
                    if(year.Length == 4)            //check for count
                    {
                        Year = int.Parse(year);
                    }
                    else
                    {
                        throw new Exception("Enter Only 4 Digit Year.");
                    }
                }
                else
                {
                    throw new Exception("Year Format is Wrong.");
                }
                DateTime dateTime = DateTime.Now;
                int CurrentYear = dateTime.Year;
                if (CurrentYear > Year)            //Check for year exceed current year
                {
                    return Year;
                }
                else
                {
                    throw new Exception("Year is Exceeding Current Year.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //Record Type Validation
        internal string RecordTypeValidation(string type)
        {
            string Type = type.ToUpper();
            try
            {
                if (Type == "CREDIT" || Type == "DEBIT" )
                {
                    return type;
                }
                else
                {
                    throw new InvalidInputException("Choose Credit or Debit.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
