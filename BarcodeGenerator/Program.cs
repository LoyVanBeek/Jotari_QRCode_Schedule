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
            if (args.Length < 3)
            {
                Console.WriteLine("GEBRUIK: BarcodeGenerator <aantal Klein> <aantal Groot> <outputbestand>");
            }

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
                html.Write(html.NewLine);
                html.RenderBeginTag(HtmlTextWriterTag.Body);
                    //html.RenderBeginTag(HtmlTextWriterTag.Table);
                        WriteQrCodes(html, kleinAmount, grootAmount, 3); 
                    //html.RenderEndTag();
                html.RenderEndTag();
            html.RenderEndTag();

            writer.Close();
        }

        private static void WriteQrCodes(HtmlTextWriter html, int kleinAmount, int grootAmount, int repeat)
        {
            html.RenderBeginTag(HtmlTextWriterTag.Table);

                for (int klein = 1; klein <= kleinAmount; klein++)
                {
                    html.RenderBeginTag(HtmlTextWriterTag.Tr);
                    for (int i=0; i<repeat; i++)
                    {
                        html.RenderBeginTag(HtmlTextWriterTag.Td);
                        GroupDiv(html, "Klein", klein);
                        html.RenderEndTag();
                    }
                    html.RenderEndTag();
                } 
                for (int groot = 1; groot <= grootAmount; groot++)
                {
                    html.RenderBeginTag(HtmlTextWriterTag.Tr);
                    for (int i = 0; i < repeat; i++)
                    {
                        html.RenderBeginTag(HtmlTextWriterTag.Td);
                        GroupDiv(html, "Groot", groot);
                        html.RenderEndTag();
                    }
                    html.RenderEndTag();
                }

            html.RenderEndTag();
        }

        private static string GroupDiv(string category, int number, int x, int y)
        {
            string code = @"<div style='border: solid 1px; 
                                        text-align: center; 
                                        width:  300px; 
                                        margin: 10px
                                        top:    {1}px'
                                        left:   {2}px'>
                        <img src='http://qrcode.kaywa.com/img.php?s=8&d={0}' alt='qrcode'/>
				        <br>
				            {0}
                        </div>";
            string html = String.Format(code, category + number.ToString(), y, x);
            return html;
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

        private static void GroupDiv(HtmlTextWriter html, string category, int number)
        {
            html.AddStyleAttribute("border",      "solid 2px");
            html.AddStyleAttribute("text-align",  "center");
            html.AddStyleAttribute("width",       "320px");
            html.AddStyleAttribute("margin",      "10px");
            html.RenderBeginTag(HtmlTextWriterTag.Div);

            string imgSource = string.Format("http://qrcode.kaywa.com/img.php?s=8&d={0}", category + number.ToString());
            html.AddAttribute("src", imgSource);
            html.RenderBeginTag(HtmlTextWriterTag.Img); 
            html.RenderEndTag();

            html.RenderBeginTag(HtmlTextWriterTag.Br);
            html.RenderEndTag();
            html.Write(category + number.ToString());

            html.RenderEndTag();
        }
    }
}
