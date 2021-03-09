using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfSharp.Pdf.IO;

namespace lovepdf.Controllers
{
    public class PdfController : ApiControllerBase
    {
        private readonly ILogger<PdfController> _logger;
        public PdfController(ILogger<PdfController> logger)
        {
            _logger = logger;
        }

        // 上传 pdf 文件, 返回其中信息
        // 必须使用 [FromForm]
        // 变量名称需要匹配表单中的上传文件字段名称
        // 使用 Consumes 来限制请求媒体类型，不匹配的话，返回 Method Not Allowed
        [HttpPost("info")]
        [Consumes("multipart/form-data")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PdfInfo([FromForm] IFormFile pdf)
        {
            using (var stream = new MemoryStream())
            {
                await pdf.CopyToAsync(stream);
                _logger.LogInformation( $"file length: { stream.Length}.");

                stream.Seek(0, SeekOrigin.Begin);
                var tempPath = System.IO.Path.GetTempFileName();
                System.IO.File.WriteAllBytes(tempPath, stream.ToArray());
                PdfSharp.Pdf.PdfDocument document = PdfReader.Open(tempPath, PdfDocumentOpenMode.InformationOnly);

                var creator = document.Info.Creator;
                var result = new {
                    creator = creator
                };

                return new JsonResult(result);
            }
        }

    }
}