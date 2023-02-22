import { dotnet } from './dotnet.js'
import {render } from './renderDtdl.js'

const { getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const config = getConfig()
const assemblyExports = await getAssemblyExports(config.mainAssemblyName)
await dotnet.run();

const parserVersionDiv = document.getElementById('parserVersion')
const out = document.getElementById('out')
const el = document.getElementById('dtdl-text')

parserVersionDiv.innerText = assemblyExports.DtdlParserJSInterop.ModelParserJS.ParserVersion()

const validate = async () => {
    // @ts-check
    out.innerHTML = '.. parsing ..'
    out.style.color = 'grey'
    /** @type import("./DtdlOm").DtdlObjectModel */
    let parseResult = ''
    try {
        parseResult = JSON.parse(await assemblyExports.DtdlParserJSInterop.ModelParserJS.ParseAsync(el.value))
        render(parseResult)
        out.innerHTML = JSON.stringify(parseResult, null, 2)
        out.style.color = 'black'
    }
    catch (err) {
        console.error(err)
        out.innerHTML = JSON.stringify(JSON.parse(err.message), null, 2)
        out.style.color = 'red'
    }
}
el.onchange = validate
await validate()

