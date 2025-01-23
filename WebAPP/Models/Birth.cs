namespace WebApp.Models
{
    public class Birth
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) && BirthDate.HasValue && BirthDate.Value < DateTime.Now;
        }

        public int CalculateAge()
        {
            if (!BirthDate.HasValue)
            {
                return 0;
            }

            var age = DateTime.Now.Year - BirthDate.Value.Year;
            if (DateTime.Now.DayOfYear < BirthDate.Value.DayOfYear)
            {
                age--;
            }

            return age;
        }
    }
}