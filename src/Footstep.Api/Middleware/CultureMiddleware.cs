using System.Globalization;

namespace Footstep.Api.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly IList<string> _supportedCultures = new List<string>
        {
            "pt-BR",
            "en-US",
        };

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestedCulture = context.Request
                .GetTypedHeaders()
                .AcceptLanguage
                ?.FirstOrDefault()?
                .Value.ToString();

            var cultureName = _supportedCultures.Contains(requestedCulture!)
                ? requestedCulture
                : _supportedCultures.First();

            var cultureInfo = new CultureInfo(cultureName!);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
