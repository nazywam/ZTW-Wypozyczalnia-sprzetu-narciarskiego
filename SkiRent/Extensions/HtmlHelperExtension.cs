using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SkiRent
{
	public static class HtmlHelperExtension
	{

		public static string FieldHasError(this HtmlHelper helper, string propertyName, string errorClass = "is-invalid", string validClass = "is-valid")
		{
			if (helper.ViewData.ModelState != null && helper.ViewData.ModelState.Count>0)
			{
				if (!helper.ViewData.ModelState.IsValidField(propertyName))
					return errorClass;
				else
					return validClass;
			}
			return String.Empty;
		}

		public static string FieldHasError<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression, string errorClass = "is-invalid")
		{
			var expressionString = ExpressionHelper.GetExpressionText(expression);
			var modelName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionString);
			return FieldHasError(helper, modelName, errorClass);
		}
	}
}