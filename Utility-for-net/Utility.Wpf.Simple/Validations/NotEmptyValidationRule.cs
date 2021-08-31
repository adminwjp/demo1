using System.Globalization;
using System.Windows.Controls;

namespace Utility.Wpf.Validations
{
	/// <summary>
	/// 非空 验证
	/// </summary>
	public class NotEmptyValidationRule : ValidationRule
	{
		/// <summary>
		/// 非空 验证
		/// </summary>
		/// <param name="value"></param>
		/// <param name="cultureInfo"></param>
		/// <returns></returns>
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, "值不能为空!")
				: ValidationResult.ValidResult;
		}
	}
}
