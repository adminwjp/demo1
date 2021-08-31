using System.Globalization;
using System.Windows.Controls;

namespace Example.WpfApp.Common
{
	public class NotEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo culture)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, "值不能为空!")
				: ValidationResult.ValidResult;
		}
	}
}