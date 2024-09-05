using System.Text.RegularExpressions;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private static readonly Regex OrderNamePattern = new(@"^ORD_\d+$");
        public string Value { get; }
        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            if (!OrderNamePattern.IsMatch(value))
            {
                throw new ArgumentException("Order name must start with 'ORD_' followed by a number.");
            }

            return new OrderName(value);
        }
    }
}
