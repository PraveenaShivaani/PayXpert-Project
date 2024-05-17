using PayXpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Services
{
    internal interface IEmployeeService
    {
        public void GetEmployeeById(Employee employee);

        public void GetAllEmployees();

        public void AddEmployee(int employeeData);

        public void UpdateEmployee(int employeeData);

        public void RemoveEmployee(int employeeId);
        void GetEmployeeDetail(Employee employee);
        //void GetEmployeeId(Employee employee);
        void EmployeeId(Employee employee);
    }
}
