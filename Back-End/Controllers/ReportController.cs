
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Back_End.Controllers
//{
//  [ApiController]
//[Route("api/report")]
//    public sealed class ReportController : ControllerBase
//    {
//        private readonly IConverter _converter;

//        public ReportController(IConverter converter)
//        {
//            _converter = converter;
//        }

//        [HttpGet]
//        [Route("hello")]
//        public IActionResult Hello()
//        {
//            IDocument document = CreateDocument("Hello world", "<h1>Hello world</h1>");
//            byte[] content = _converter.Convert(document);
//            return File(content, "application/pdf", "hello.pdf");
//        }

//        private static IDocument CreateDocument(string title, string htmlContent)
//        {
//            return new HtmlToPdfDocument
//            {
//                GlobalSettings =
//            {
//                ColorMode = ColorMode.Color,
//                Orientation = Orientation.Portrait,
//                PaperSize = PaperKind.A4,
//                Margins = new MarginSettings { Top = 20, Bottom = 20 },
//                DocumentTitle = title,
//            },
//                Objects =
//            {
//                new ObjectSettings
//                {
//                    PagesCount = true,
//                    HtmlContent = htmlContent,
//                    WebSettings = { DefaultEncoding = "utf-8" },
//                    FooterSettings =
//                    {
//                        FontSize = 9,
//                        Right = "Page [page] of [toPage]",
//                        Line = true,
//                        Spacing = 2.5,
//                    },
//                },
//            },
//            };
//        }
//    }
//}
