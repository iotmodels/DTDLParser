using DTDLParser;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DtdlParserJSInterop;

internal class DmrClient
{
    internal static string DmrBasePath = "https://devicemodels.azure.com";

    static Task<string> ResolveDtmiAsync(string dtmi, string dmrBasePath, CancellationToken ct) =>
       new HttpClient().GetStringAsync($"{dmrBasePath}/{dtmi.Replace(':', '/').Replace(';', '-').ToLowerInvariant()}.json", ct);

    internal static async IAsyncEnumerable<string> DtmiResolverAsync(IReadOnlyCollection<Dtmi> dtmis, [EnumeratorCancellation] CancellationToken ct = default)
    {
        foreach (var dtmi in dtmis.Select(s => s.AbsoluteUri))
        {
            yield return await ResolveDtmiAsync(dtmi, DmrBasePath, ct);
        }
    }
}
