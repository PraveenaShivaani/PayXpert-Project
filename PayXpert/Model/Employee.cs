namespace PayXpert.Model
{
    public class Employee //Entity Class
    {
        public int  EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public void CalculateAge()
        {
            
        }
    }
}
