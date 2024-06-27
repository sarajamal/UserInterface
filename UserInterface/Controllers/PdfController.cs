//using iText.Kernel.Pdf;
//using iText.Layout.Element;
//using iText.Layout.Properties;
//using iText.Layout;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.ViewEngines;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Test12.DataAccess.Repository.IRepository;
//using Test12.Models.ViewModel;
//using iText.Html2pdf;
//using iText.Html2pdf.Resolver.Font;

//namespace UserInterface.Controllers
//{
//    public class PdfController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        private readonly ICompositeViewEngine _viewEngine;

//        public PdfController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, ICompositeViewEngine viewEngine)
//        {
//            _unitOfWork = unitOfWork;
//            _webHostEnvironment = hostEnvironment;
//            _viewEngine = viewEngine;
//        }

//        [HttpGet]
//        public IActionResult GeneratePdf(int foodStuffsId, int brandFK)
//        {
//            var model = new LoginTredMarktViewModel
//            {
//                FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == foodStuffsId),
//                FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll(incloudeProperties: "Brand").Where(u => u.FoodStuffsID == foodStuffsId).ToList(),
//                tredMaeketFoodsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK)
//            };

//            var htmlContent = RenderViewToString("PdfView", model);

//            var fileStream = GeneratePdfFromHtml(htmlContent);

//            return File(fileStream, "application/pdf", "GeneratedPdf.pdf");
//        }

//        private string RenderViewToString(string viewName, LoginTredMarktViewModel model)
//        {
//            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState);
//            using (var sw = new StringWriter())
//            {
//                var viewResult = _viewEngine.FindView(actionContext, viewName, false);

//                if (viewResult.View == null)
//                {
//                    throw new ArgumentNullException($"View {viewName} not found");
//                }

//                ViewData.Model = model;

//                var viewContext = new ViewContext(
//                    actionContext,
//                    viewResult.View,
//                    ViewData,
//                    TempData,
//                    sw,
//                    new HtmlHelperOptions()
//                );

//                viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();
//                return sw.ToString();
//            }
//        }

//        private byte[] GeneratePdfFromHtml(string htmlContent)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                var writer = new PdfWriter(memoryStream);
//                var pdfDoc = new PdfDocument(writer);
//                var document = new Document(pdfDoc);
//                var converterProperties = new ConverterProperties();

//                // Set base URI for CSS and other resources
//                string baseUri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
//                converterProperties.SetBaseUri(baseUri);

//                // Add font provider to properly embed fonts
//                var fontProvider = new DefaultFontProvider(false, false, false);
//                fontProvider.AddStandardPdfFonts();
//                fontProvider.AddDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "fonts"));
//                converterProperties.SetFontProvider(fontProvider);

//                // Convert HTML to PDF
//                HtmlConverter.ConvertToPdf(htmlContent, pdfDoc, converterProperties);

//                document.Close();
//                return memoryStream.ToArray();
//            }
//        }
//    }
//}
