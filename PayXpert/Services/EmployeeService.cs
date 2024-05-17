using PayXpert.Model;

namespace PayXpert.Services
{
    public class EmployeeService : IEmployeeService
    {
        public void AddEmployee(int employeeData)
        {
            throw new NotImplementedException();
        }

        public void GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public void GetEmployeeById(Employee employee)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Employee ID :: {employee.EmployeeID} \n" +
                $"Name :: {employee.FirstName + employee.LastName}" +
                $"Date Of Birth :: {employee.Dob}" +
                $"Gender :: {employee.Gender}" +
                $"Email :: {employee.Email}" +
                $"Phone Number :: {employee.PhoneNumber}" +
                $"Address :: {employee.Address}" +
                $"Position :: {employee.Position}" +
                $"Joining Date :: {employee.JoiningDate}" +
                $"Termination Date :: {employee.TerminationDate}");
        }

        public void GetEmployeeDetail(Employee employee)
        {
            Console.WriteLine($"Employee ID :: {employee.EmployeeID} \t First Name :: {employee.FirstName} \n");
        }

        public void RemoveEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(int employeeData)
        {
            throw new NotImplementedException();
        }

        public void EmployeeId(Employee employee)
        {
            Console.WriteLine($"Employee ID :: {employee.EmployeeID}");

        }
    }
}
