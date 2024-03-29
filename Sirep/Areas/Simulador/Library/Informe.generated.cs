﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.17929
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sirep.Areas.Simulador.Library
{
    
    #line 2 "..\..\Informe.cshtml"
    using System;
    
    #line default
    #line hidden
    using System.Collections.Generic;
    
    #line 3 "..\..\Informe.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 4 "..\..\Informe.cshtml"
    using Sirep.Areas.Simulador.Library;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.4.1.0")]
    internal partial class Informe : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden

        #line 6 "..\..\Informe.cshtml"

    public Resultado Model { get; set; }
    public int NumMachos { get; set; }
    public int NumHembras { get; set; }
    public DateTime TiempoInicial { get; set; }

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");




WriteLiteral("\r\n");


WriteLiteral(@"

<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""utf-8"" />
    <title>Reporte de los reproductores </title>
    <style>
        thead
        {
            background-color: darkgray;
            color: white;
            text-transform: uppercase;
            font-size: 13px;
        }

        table, td, th
        {
            border: 1px solid black;
            text-align: center;
            font-size: 12px;
        }

            table.simple tr:nth-child(2n+2)
            {
                background-color: lightgray;
            }

            table.simple thead
            {
                background-color: darkblue;
            }

            table.mejoresCruces tr:nth-child(4n+1), table.mejoresCruces tr:nth-child(4n+2)
            {
                background-color: lightgray;
            }

            table.mejoresCruces tr:nth-child(2n) td:nth-child(3n+1)
            {
                font-weight: bold;
            }

        ");


WriteLiteral(@"@media only print
        {
            body
            {
                font-size: 10px;
            }

            thead
            {
                font-size: 11px;
                color: black;
                background-color: lightgray;
            }

            table, td, th
            {
                font-size: 8px;
                margin: 0px;
                padding: 0px;
                border-spacing: 1px;
            }
        }
    </style>
</head>
<body>
    <article>
        <header>
            <h1>Informe SIREP</h1>
            <p>
                Analizados los posibles cruces de ");


            
            #line 83 "..\..\Informe.cshtml"
                                             Write(NumMachos);

            
            #line default
            #line hidden
WriteLiteral(" machos con ");


            
            #line 83 "..\..\Informe.cshtml"
                                                                   Write(NumHembras);

            
            #line default
            #line hidden
WriteLiteral(@"
                hembras.
            </p>
        </header>

        <h2>Resumen general</h2>
        <table class=""simple"">
            <thead>
                <tr>
                    <th>Número de loci</th>
                    <th>Número de cruces</th>
                    <th>Número de machos</th>
                    <th>Número de hembras</th>
                    <th>Número de gametos esperados</th>
                    <th>Número de cigotos esperados</th>
                    <th>Ho promedio de todos los cruces</th>
                    <th>He promedio todos los cruces</th>
                    <th>Fis promedio todos los cruces</th>
                </tr>
            </thead>
");


            
            #line 103 "..\..\Informe.cshtml"
             foreach (var rLoci in Model)
            {

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>");


            
            #line 106 "..\..\Informe.cshtml"
                    Write(rLoci.NumLociComun);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 107 "..\..\Informe.cshtml"
                    Write(rLoci.NumCruces);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 108 "..\..\Informe.cshtml"
                    Write(rLoci.NumMachos);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 109 "..\..\Informe.cshtml"
                    Write(rLoci.NumHembras);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 110 "..\..\Informe.cshtml"
                    Write(rLoci.NumGametosEsperados);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 111 "..\..\Informe.cshtml"
                    Write(rLoci.NumCigotosEsperados);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 112 "..\..\Informe.cshtml"
                    Write(rLoci.HoPromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral(" ");



WriteLiteral("</td>\r\n                    <td>");


            
            #line 113 "..\..\Informe.cshtml"
                    Write(rLoci.HePromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral(" ");



WriteLiteral("</td>\r\n                    <td>");


            
            #line 114 "..\..\Informe.cshtml"
                    Write(rLoci.FisPromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");


            
            #line 116 "..\..\Informe.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n\r\n        <h2>Tablas con la información detallada</h2>\r\n\r\n");


            
            #line 121 "..\..\Informe.cshtml"
         foreach (var rLoci in Model)
        {  

            
            #line default
            #line hidden
WriteLiteral("            <h3>Con  ");


            
            #line 123 "..\..\Informe.cshtml"
                 Write(rLoci.NumLociComun);

            
            #line default
            #line hidden
WriteLiteral(" loci en común</h3>\r\n");



WriteLiteral("            <h4>Tablas de fecuencia alélica</h4>\r\n");


            
            #line 125 "..\..\Informe.cshtml"
            
            for (int index = 0; index < rLoci.ConteoAlelosByLocus.Length; index++)
            {

            
            #line default
            #line hidden
WriteLiteral("            <h5>Conteo alélico para  el locus ");


            
            #line 128 "..\..\Informe.cshtml"
                                         Write(rLoci.GetNombreLocus(index));

            
            #line default
            #line hidden
WriteLiteral("</h5>\r\n");


            
            #line 129 "..\..\Informe.cshtml"
                var conteo = rLoci.ConteoAlelosByLocus[index];

            
            #line default
            #line hidden
WriteLiteral(@"            <table class=""simple"">
                <thead>
                    <tr>
                        <th>Alelo</th>
                        <th>Conteo</th>
                        <th>Porcentaje</th>
                    </tr>
                </thead>

");


            
            #line 139 "..\..\Informe.cshtml"
                 foreach (var fa in conteo)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <td>");


            
            #line 142 "..\..\Informe.cshtml"
                       Write(fa.Alelo);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 143 "..\..\Informe.cshtml"
                       Write(fa.Conteo.ToString("N0"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 144 "..\..\Informe.cshtml"
                       Write(fa.Porcentaje.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("%</td>\r\n                    </tr>\r\n");


            
            #line 146 "..\..\Informe.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </table>\r\n");


            
            #line 148 "..\..\Informe.cshtml"
            }
            

            
            #line default
            #line hidden
WriteLiteral("            <h4>Resumen de los machos</h4>\r\n");


            
            #line 151 "..\..\Informe.cshtml"
        

            
            #line default
            #line hidden
WriteLiteral(@"            <table class=""simple"">
                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Hembras cruzadas</th>
                        <th>Numero cruces aceptables (Ho>0.5)</th>
                        <th>Ho individual</th>
");


            
            #line 159 "..\..\Informe.cshtml"
                         if (Model.Core.NumEstaciones > 1)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <th>Alelos únicos</th>\r\n");


            
            #line 162 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        <th>Ho promedio de cruces</th>\r\n                        <" +
"th>He promedio de cruces</th>\r\n                        <th>Fis promedio</th>\r\n  " +
"                  </tr>\r\n                </thead>\r\n\r\n");


            
            #line 169 "..\..\Informe.cshtml"
                 foreach (var agrupacion in rLoci.ParejasAgrupadasPorMacho)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <td>");


            
            #line 172 "..\..\Informe.cshtml"
                        Write(agrupacion.Agrupador.Codigo);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 173 "..\..\Informe.cshtml"
                        Write(agrupacion.NumCruces);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 174 "..\..\Informe.cshtml"
                        Write(agrupacion.NumCrucesAceptables);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 175 "..\..\Informe.cshtml"
                        Write(agrupacion.Agrupador.Ho.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");


            
            #line 176 "..\..\Informe.cshtml"
                         if (Model.Core.NumEstaciones > 1)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <td>");


            
            #line 178 "..\..\Informe.cshtml"
                            Write(agrupacion.Agrupador.NumAlelosRaros);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");


            
            #line 179 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        <td>");


            
            #line 180 "..\..\Informe.cshtml"
                        Write(agrupacion.HoPromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral(" ");



WriteLiteral("</td>\r\n                        <td>");


            
            #line 181 "..\..\Informe.cshtml"
                        Write(agrupacion.HePromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral(" ");



WriteLiteral("</td>\r\n                        <td>");


            
            #line 182 "..\..\Informe.cshtml"
                        Write(agrupacion.FisPromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    </tr>\r\n");


            
            #line 184 "..\..\Informe.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </table>\r\n");


            
            #line 186 "..\..\Informe.cshtml"
        

            
            #line default
            #line hidden
WriteLiteral("            <h4>Resumen de las hembras</h4>\r\n");



WriteLiteral(@"            <table class=""simple"">
                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Machos cruzados</th>
                        <th>Numero cruces aceptables (Ho>0.5)</th>
                        <th>Ho individual</th>
");


            
            #line 195 "..\..\Informe.cshtml"
                         if (Model.Core.NumEstaciones > 1)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <th>Alelos únicos</th>\r\n");


            
            #line 198 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        <th>Ho promedio de cruces</th>\r\n                        <" +
"th>He promedio de cruces</th>\r\n                        <th>Fis promedio</th>\r\n  " +
"                  </tr>\r\n                </thead>\r\n\r\n");


            
            #line 205 "..\..\Informe.cshtml"
                 foreach (var agrupacion in rLoci.ParejasAgrupadasPorHembra)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <td>");


            
            #line 208 "..\..\Informe.cshtml"
                        Write(agrupacion.Agrupador.Codigo);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 209 "..\..\Informe.cshtml"
                        Write(agrupacion.NumCruces);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 210 "..\..\Informe.cshtml"
                        Write(agrupacion.NumCrucesAceptables);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td>");


            
            #line 211 "..\..\Informe.cshtml"
                        Write(agrupacion.Agrupador.Ho.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");


            
            #line 212 "..\..\Informe.cshtml"
                         if (Model.Core.NumEstaciones > 1)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <td>");


            
            #line 214 "..\..\Informe.cshtml"
                            Write(agrupacion.Agrupador.NumAlelosRaros);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");


            
            #line 215 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        <td>");


            
            #line 216 "..\..\Informe.cshtml"
                        Write(agrupacion.HoPromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral(" ");



WriteLiteral("</td>\r\n                        <td>");


            
            #line 217 "..\..\Informe.cshtml"
                        Write(agrupacion.HePromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral(" ");



WriteLiteral("</td>\r\n                        <td>");


            
            #line 218 "..\..\Informe.cshtml"
                        Write(agrupacion.FisPromedio.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    </tr>\r\n");


            
            #line 220 "..\..\Informe.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </table>\r\n");


            
            #line 222 "..\..\Informe.cshtml"
        

            
            #line default
            #line hidden
WriteLiteral("            <h4>Cruces por macho</h4>\r\n");



WriteLiteral("            <table class=\"mejoresCruces\">\r\n");


            
            #line 225 "..\..\Informe.cshtml"
                 foreach (var sel in rLoci.ParejasAgrupadasPorMacho)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <th rowspan=\"2\">");


            
            #line 228 "..\..\Informe.cshtml"
                                   Write(sel.Agrupador.Codigo);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n\r\n");


            
            #line 230 "..\..\Informe.cshtml"
                         foreach (var pareja in sel.Parejas)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <th colspan=\"3\"\r\n");


            
            #line 233 "..\..\Informe.cshtml"
                             if (pareja.Ho > 0.5)
                            {

            
            #line default
            #line hidden
WriteLiteral("                                ");

WriteLiteral("style=\"background-color: green;\"\r\n");


            
            #line 236 "..\..\Informe.cshtml"
                            }
                            else
                            {

            
            #line default
            #line hidden
WriteLiteral("                                 ");

WriteLiteral("style=\"background-color: red;\"\r\n");


            
            #line 240 "..\..\Informe.cshtml"
                            }

            
            #line default
            #line hidden
WriteLiteral("                            >\r\n                                ");


            
            #line 242 "..\..\Informe.cshtml"
                           Write(pareja.Hembra.Codigo);

            
            #line default
            #line hidden
WriteLiteral("\r\n                            </th>\r\n");


            
            #line 244 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </tr>\r\n");



WriteLiteral("                    <tr>\r\n\r\n");


            
            #line 248 "..\..\Informe.cshtml"
                         foreach (var pareja in sel.Parejas)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <td>");


            
            #line 250 "..\..\Informe.cshtml"
                           Write(pareja.Ho.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");



WriteLiteral("                            <td>");


            
            #line 251 "..\..\Informe.cshtml"
                           Write(pareja.He.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");



WriteLiteral("                            <td>");


            
            #line 252 "..\..\Informe.cshtml"
                           Write(pareja.Fis.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");


            
            #line 253 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </tr>\r\n");


            
            #line 255 "..\..\Informe.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </table>\r\n");


            
            #line 257 "..\..\Informe.cshtml"
        

            
            #line default
            #line hidden
WriteLiteral("            <h4>Cruces por hembra</h4>\r\n");



WriteLiteral("            <table class=\"mejoresCruces\">\r\n");


            
            #line 260 "..\..\Informe.cshtml"
                 foreach (var sel in rLoci.ParejasAgrupadasPorHembra)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <th rowspan=\"2\">");


            
            #line 263 "..\..\Informe.cshtml"
                                   Write(sel.Agrupador.Codigo);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n\r\n");


            
            #line 265 "..\..\Informe.cshtml"
                         foreach (var pareja in sel.Parejas)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <th colspan=\"3\"\r\n");


            
            #line 268 "..\..\Informe.cshtml"
                             if (pareja.Ho > 0.5)
                            {

            
            #line default
            #line hidden
WriteLiteral("                                ");

WriteLiteral("style=\"background-color: green;\"\r\n");


            
            #line 271 "..\..\Informe.cshtml"
                            }
                            else
                            {

            
            #line default
            #line hidden
WriteLiteral("                                 ");

WriteLiteral("style=\"background-color: red;\"\r\n");


            
            #line 275 "..\..\Informe.cshtml"
                            }

            
            #line default
            #line hidden
WriteLiteral("                            >\r\n                                ");


            
            #line 277 "..\..\Informe.cshtml"
                           Write(pareja.Macho.Codigo);

            
            #line default
            #line hidden
WriteLiteral("\r\n                            </th>\r\n");


            
            #line 279 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </tr>\r\n");



WriteLiteral("                    <tr>\r\n\r\n");


            
            #line 283 "..\..\Informe.cshtml"
                         foreach (var pareja in sel.Parejas)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <td>");


            
            #line 285 "..\..\Informe.cshtml"
                           Write(pareja.Ho.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");



WriteLiteral("                            <td>");


            
            #line 286 "..\..\Informe.cshtml"
                           Write(pareja.He.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");



WriteLiteral("                            <td>");


            
            #line 287 "..\..\Informe.cshtml"
                           Write(pareja.Fis.ToString("N3"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");


            
            #line 288 "..\..\Informe.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </tr>\r\n");


            
            #line 290 "..\..\Informe.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </table>\r\n");


            
            #line 292 "..\..\Informe.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n        <footer>\r\n            <hr />\r\n            <p>\r\n                Generado" +
" automáticamente por SIREP en <b>");


            
            #line 297 "..\..\Informe.cshtml"
                                                     Write(DateTime.Now - TiempoInicial);

            
            #line default
            #line hidden
WriteLiteral("</b> a las\r\n                <time datetime=\"");


            
            #line 298 "..\..\Informe.cshtml"
                           Write(DateTime.Now.ToUniversalTime());

            
            #line default
            #line hidden
WriteLiteral("\" pubdate=\"pubdate\">\r\n                    ");


            
            #line 299 "..\..\Informe.cshtml"
               Write(DateTime.Now.ToShortTimeString());

            
            #line default
            #line hidden
WriteLiteral(" de  ");


            
            #line 299 "..\..\Informe.cshtml"
                                                     Write(DateTime.Now.ToShortDateString());

            
            #line default
            #line hidden
WriteLiteral("\r\n                </time>\r\n                <br />\r\n   " +
"   \r\n       " +
"     </p>\r\n        </footer>\r\n    </article>\r\n</body>\r\n</html>\r\n");


        }
    }
}
#pragma warning restore 1591
