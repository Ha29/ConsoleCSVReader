using System;

namespace Week1_ConsoleCSVDataReader
{
    public class Person : IComparable<Person>
    {
        [CsvHelper.Configuration.Attributes.Name("ID")]
        public int Id { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Job Title")]
        public string JobTitle { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Email Address")]
        public string Email { get; set; }
        [CsvHelper.Configuration.Attributes.Name("First Name")]
        public string FirstName { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Last Name")]
        public string LastName { get; set; }
        public DateTime Anniversary { get; set; }
        public string State { get; set; }
        public int Age { get; set; }
        /// <summary>
        /// Used for the sort method when records are sorted through comparison. 
        /// Sorts by state alphabetically then by Aniversary ascending. 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int CompareTo(Person b)
        {
            int state_result = String.Compare(this.State, b.State);
            if (state_result == 0)
            {
                return DateTime.Compare(this.Anniversary, b.Anniversary);
            } else
            {
                return state_result;
            }

        }
    }
}