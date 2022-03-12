using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DemoAPI.Configuration
{
    internal static class ModelStateExtension
    {
        public static List<string> GetModelErrors(this ModelStateDictionary state)
        {
            return state.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList();
        }
    }
}
