using DTDLParser;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace DtdlParserJSInterop;

[SupportedOSPlatform("browser")]
public partial class ModelParserJS
{
    public static void Main() => Console.WriteLine("dotnet loaded, using parser: " + typeof(ModelParser).Assembly.FullName);

    [JSExport]
    public static string ParserVersion() => typeof(ModelParser).Assembly.FullName;

    [JSExport]
    public static async Task<string> ParseAsync(string dtdl)
    {
        ModelParser parser = new();
        return  await parser.ParseToJsonAsync(StringToAsyncEnumerable(dtdl));
    }


    private static async IAsyncEnumerable<string> StringToAsyncEnumerable(string jsonText)
    {
        await Task.Yield();
        yield return jsonText;
    }
}