using RedFolder.Smoke;

if (args.Length is < 2 or > 3 ||
    !Uri.TryCreate(args[0], UriKind.Absolute, out var baseUri) ||
    (baseUri.Scheme != "https" && baseUri.Scheme != "http") ||
    baseUri.AbsolutePath != "/" || !string.IsNullOrEmpty(baseUri.Query) ||
    !string.IsNullOrEmpty(baseUri.Fragment) || !string.IsNullOrEmpty(baseUri.UserInfo) ||
    args[1].Length != 40 || !args[1].All(Uri.IsHexDigit))
{
    Console.Error.WriteLine("Usage: smoke <http(s)://host[:port]> <full 40-character commit SHA> [timeout-seconds: 1-120]");
    return 2;
}

var seconds = 15;
if (args.Length == 3 && (!int.TryParse(args[2], out seconds) || seconds is < 1 or > 120))
{
    Console.Error.WriteLine("Timeout must be an integer from 1 to 120 seconds.");
    return 2;
}

using var handler = new HttpClientHandler { AllowAutoRedirect = false, UseCookies = false };
using var client = new HttpClient(handler) { BaseAddress = baseUri, Timeout = TimeSpan.FromSeconds(seconds) };
return await SmokeChecks.RunAsync(client, args[1], Console.Out) ? 0 : 1;
