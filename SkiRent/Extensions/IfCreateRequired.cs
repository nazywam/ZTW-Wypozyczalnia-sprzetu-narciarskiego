using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.Extensions
{
	public class IfCreateRequired : RequiredAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext context)
		{
			Object instance = context.ObjectInstance;
			Type type = instance.GetType();
			Object propertyvalue = type.GetProperty("ID").GetValue(instance, null);
			if (((int) propertyvalue) == 0)
			{
				return base.IsValid(value, context);
			}
			return ValidationResult.Success;
		}
	}
}