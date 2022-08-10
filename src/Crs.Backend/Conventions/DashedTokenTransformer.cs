using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Crs.Backend.Conventions
{
    internal sealed class DashedTokenTransformer : IOutboundParameterTransformer
    {
        private const string ReplacePattern = "$1-$2";

        private readonly Regex _regex = new(@"([a-z])([A-Z])", RegexOptions.Compiled);

        public string? TransformOutbound(object? value)
        {
            if (value is string str)
            {
                return _regex.Replace(str, ReplacePattern).ToLower();
            }

            return null;
        }

        public static IActionModelConvention CreateConvention()
        {
            return new RouteTokenTransformerConvention(new DashedTokenTransformer());
        }
    }
}
