using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace BarcodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int kleinAmount = int.Parse(args[0]);
            int grootAmount = int.Parse(args[1]);
            string outfile = args[2];

            TextWriter writer = new StreamWriter(outfile);

            HtmlTextWriter html = new HtmlTextWriter(writer);

            html.RenderBeginTag(HtmlTextWriterTag.Html);
                html.RenderBeginTag(HtmlTextWriterTag.Head);
                    html.RenderBeginTag(HtmlTextWriterTag.Title);
                        html.Write("JOTARI QR-codes");
                    html.RenderEndTag();
                html.RenderEndTag();
                html.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "Arial");
                html.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "20");
                html.RenderBeginTag(HtmlTextWriterTag.Body);
                    //html.RenderBeginTag(HtmlTextWriterTag.Table);
                        WriteQrCodes(html, kleinAmount, grootAmount); 
                    //html.RenderEndTag();
                html.RenderEndTag();
            html.RenderEndTag();

            writer.Close();
        }

        private static void WriteQrCodes(HtmlTextWriter html, int kleinAmount, int grootAmount)
        {
//            string codeLine = @"
//                <tr>
//                    <td style='border: solid 1px; text-align: center;'>
//                        <div>
//                        <img src='http://qrcode.kaywa.com/img.php?s=8&d={0}' alt='qrcode'/>
//				        <br>
//				        {0}
//                        </div>
//                    </td>
//                </tr>";
            
            for (int klein = 1; klein <= kleinAmount; klein++)
            {
                string htmlLine = GroupDiv("Klein", klein);//String.Format(codeLine, "Klein" + klein.ToString());
                html.Write(htmlLine);
            } 
            for (int groot = 1; groot <= grootAmount; groot++)
            {
                string htmlLine = GroupDiv("Groot", groot);//String.Format(codeLine, "Groot" + groot.ToString());
                html.Write(htmlLine);
            }
        }

        private static string GroupDiv(string category, int number)
        {
            string code = @"<div style='border: solid 1px; text-align: center; width: 300px; margin:10px'>
                        <img src='http://qrcode.kaywa.com/img.php?s=8&d={0}' alt='qrcode'/>
				        <br>
				        {0}
                        </div>";
            string html = String.Format(code, category + number.ToString());
            return html;
        }
    }
}
