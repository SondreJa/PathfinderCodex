using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;

namespace BackOffice.Extensions
{
    // Stolen from https://github.com/Bitmapped/MvcEnumFlags/blob/master/src/MvcEnumFlags/CheckBoxesForEnumFlagsModelHtmlHelper.cs
    // Made .NET Core compatible
    public static class CheckBoxesForEnumFlagsModelHtmlHelper
    {
        public static IHtmlContent CheckBoxesForEnumFlagsFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            var metadata = modelExplorer.Metadata;
            var enumModelType = metadata.ModelType;
            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);

            if (enumModelType.IsGenericType && enumModelType.GetGenericTypeDefinition() == typeof(Nullable<>))
                enumModelType = Nullable.GetUnderlyingType(enumModelType);
            
            if (!enumModelType.IsEnum)
            {
                throw new ArgumentException("This helper can only be used with enums. Type used was: " + enumModelType.FullName + ".");
            }

            var sb = new StringBuilder();
            foreach (Enum item in Enum.GetValues(enumModelType))
            {
                if (Convert.ToInt64(item) != 0)
                {
                    var templateInfo = htmlHelper.ViewData.TemplateInfo;
                    var id = $"{fullHtmlFieldName}_{templateInfo.GetFullHtmlFieldName(item.ToString())}";

                    var checkbox = new TagBuilder("input")
                    {
                        Attributes =
                        {
                            ["id"] = id,
                            ["name"] = fullHtmlFieldName,
                            ["type"] = "checkbox",
                            ["value"] = item.ToString()
                        }
                    };

                    // TODO: Add functionality to check checkbox when editing already created spells

                    sb.AppendLine(GetString(checkbox));

                    var label = new TagBuilder("label") { Attributes = { ["for"] = id } };
                    var field = item.GetType().GetField(item.ToString());

                    var displayString = field.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault() is DisplayAttribute display ? display.Name : item.ToString();
                    label.InnerHtml.SetContent(displayString);

                    sb.AppendLine(GetString(label));
                    sb.AppendLine("<br />");
                }
            }

            return new HtmlString(sb.ToString());
        }

        private static string GetString(IHtmlContent content)
        {
            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
