using System.ComponentModel.DataAnnotations;

namespace Web_Based_Major_Project___API.Validation
{
    public class PositiveIntListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is List<int> list)
            {
                return list.All(x => x > 0);
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"All elements in {name} must be positive numbers.";
        }
    }
}
