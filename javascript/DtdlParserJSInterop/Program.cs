using DTDLParser;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace DtdlParserJSInterop;

public partial class ModelParserJS
{
    public static void Main() => Console.WriteLine("dotnet loaded, using parser: " + typeof(ModelParser).Assembly.FullName);
    [JSExport]
    public static async Task<string> ParseAsync(string dtdl, string dmrBasePath)
    {
        string res = string.Empty;
        DmrClient.DmrBasePath = dmrBasePath;
        ModelParser parser = new(new ParsingOptions()
        {
            DtmiResolverAsync = DmrClient.DtmiResolverAsync
        });
       
       return  await parser.ParseToJsonAsync(StringToAsyncEnumerable(dtdl));
    }

    private static async IAsyncEnumerable<string> StringToAsyncEnumerable(string jsonText)
    {
        await Task.Yield();
        yield return jsonText;
    }

}