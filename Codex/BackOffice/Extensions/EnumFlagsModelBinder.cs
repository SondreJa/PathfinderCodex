using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace BackOffice.Extensions
{
    public class EnumFlagsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Fetch value to bind.
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null)
            {
                var modelName = bindingContext.ModelName;
                var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
                if (valueProviderResult == ValueProviderResult.None)
                {
                    return Task.CompletedTask;
                }

                bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

                var valueType = bindingContext.ModelType;
                var model = (Enum)Enum.Parse(valueType, string.Join(",", value.Values.ToArray()));

                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
