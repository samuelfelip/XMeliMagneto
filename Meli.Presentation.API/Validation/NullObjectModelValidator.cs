using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meli.Presentation.API.Validation;

// <snippet_Class>
public class NullObjectModelValidator : IObjectModelValidator
{
    public void Validate(ActionContext actionContext,
        ValidationStateDictionary? validationState, string prefix, object? model)
    {

    }
}
// </snippet_Class>
