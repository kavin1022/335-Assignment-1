using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using A1.Dtos;

namespace A1.Helper
{
    public class VCardOutputFormatter : TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            CardOut card = (CardOut)context.Object;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("BEGIN:VCARD");
            builder.AppendLine("VERSION:4.0");
            builder.Append("N:").AppendLine(card.LastName + ";" + card.FirstName + ";;" + card.Title + ";");
            builder.Append("FN:").AppendLine(card.Title + " " + card.FirstName + " " + card.LastName);
            builder.Append("UID:").AppendLine(card.Uid + "");
            builder.Append("ORG:").AppendLine(card.Org);
            builder.Append("EMAIL;TYPE=work:").AppendLine(card.Email);
            builder.Append("TEL:").AppendLine(card.Tel);
            builder.Append("URL:").AppendLine(card.Url);
            builder.Append("CATEGORIES:").AppendLine(card.Categories);
            builder.Append("PHOTO;ENCODING=BASE64;TYPE=").Append(card.PhotoType).Append(":").AppendLine(card.Photo);
            builder.Append("LOGO;ENCODING=BASE64;TYPE=").Append(card.LogoPhotoType).Append(":").AppendLine(card.Logo);
            builder.AppendLine("END:VCARD");
            string outString = builder.ToString();
            byte[] outBytes = selectedEncoding.GetBytes(outString);
            var response = context.HttpContext.Response.Body;
            return response.WriteAsync(outBytes, 0, outBytes.Length);
        }
    }
}
