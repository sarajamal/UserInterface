//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using Test12.DataAccess.Repository.IRepository;
//using static System.Net.Mime.MediaTypeNames;
//using Microsoft.AspNetCore.Components.RenderTree;
//using Microsoft.EntityFrameworkCore;
//using System.Web;
//using Test12.Models.ViewModel;
//using Test12.Models.Models.Production;
//using Test12.Models.Models.trade_mark;
//using System.ComponentModel;
//using Test12.Models.Models.Preparation;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using System.Diagnostics.Metrics;

//namespace Test12.Controllers
//{
//    public class GeneratePDF : Controller
//    {

//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public GeneratePDF(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
//        {
//            _unitOfWork = unitOfWork;
//            _webHostEnvironment = hostEnvironment;

//        }


//        //       public IActionResult GeneratePdf(int? id)
//        //       {

//        //           string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder
//        //           var productionData = _unitOfWork.itemsRepository.Get(u => u.ID_المنتج == id);

//        //           string المنتج_Id = productionData.ID_المنتج.ToString();
//        //           string fkProduct = productionData.ID.ToString();

//        //           var headerImageFileName = productionData.صورة; // Assuming this is the file name
//        //           var fullHeaderImagePath = System.IO.Path.Combine(wwwRootPath, "IMAGES", "الانتاج", المنتج_Id, fkProduct, headerImageFileName);

//        //           // Arabic text to be displayed
//        //           var arabicText = "مرحبا هنا";

//        //           const string formHtml = @"
//        //   <html>
//        //<head>
//        //       <style>
//        //           h2 {
//        //               color: blue;
//        //               text-align: center;
//        //           }

//        //           input[type='text'], input[type='radio'], input[type='checkbox'] {
//        //               margin-bottom: 10px;
//        //           }

//        //           /* Add more styles as needed */
//        //       </style>
//        //   </head>
//        //       <body>

//        //           <h2>واجهات المستخدم </h2>
//        //           <form>
//        //             First name: <br> <input type='text' name='firstname' value=''> <br>
//        //             Last name: <br> <input type='text' name='lastname' value=''> <br>
//        //             <br>
//        //             <p>Please specify your gender:</p>
//        //             <input type='radio' id='female' name='gender' value= 'Female'>
//        //               <label for='female'>Female</label> <br>
//        //               <br>
//        //             <input type='radio' id='male' name='gender' value='Male'>
//        //               <label for='male'>Male</label> <br>
//        //               <br>
//        //             <input type='radio' id='non-binary/other' name='gender' value='Non-Binary / Other'>
//        //               <label for='non-binary/other'>Non-Binary / Other</label>
//        //             <br>
//        //             <p>Please select all medical conditions that apply:</p>
//        //             <input type='checkbox' id='condition1' name='Hypertension' value='Hypertension'>
//        //             <label for='condition1'> Hypertension</label><br>
//        //             <input type='checkbox' id='condition2' name='Heart Disease' value='Heart Disease'>
//        //             <label for='condition2'> Heart Disease</label><br>
//        //             <input type='checkbox' id='condition3' name='Stoke' value='Stoke'>
//        //             <label for='condition3'> Stoke</label><br>
//        //             <input type='checkbox' id='condition4' name='Diabetes' value='Diabetes'>
//        //             <label for='condition4'> Diabetes</label><br>
//        //             <input type='checkbox' id='condition5' name='Kidney Disease' value='Kidney Disease'>
//        //             <label for='condition5'> Kidney Disease</label><br>

//        //             <br>
//        //             <img  src={fullHeaderImagePath}/>
//        //           </form>
//        //       </body>
//        //   </html>";

//        //           // Instantiate Renderer
//        //           var renderer = new ChromePdfRenderer();


//        //           // Step 1. Generate the PDF form
//        //           var pdfForm = renderer.RenderHtmlAsPdf(formHtml);
//        //           pdfForm.SaveAs("BasicForm.pdf"); // Save the generated form

//        //           // Step 2. Reading and Writing PDF form values.
//        //           var formDocument = PdfDocument.FromFile("BasicForm.pdf");

//        //           // Set and Read the value of the "firstname" field
//        //           var firstNameField = formDocument.Form.FindFormField("firstname");
//        //           firstNameField.Value = "Minnie";

//        //           // Set and Read the value of the "lastname" field
//        //           var lastNameField = formDocument.Form.FindFormField("lastname");
//        //           lastNameField.Value = "Mouse";

//        //           // Save the filled form
//        //           formDocument.SaveAs("FilledForm.pdf");

//        //           // Return the filled form as a downloadable file
//        //           var filledFormBytes = System.IO.File.ReadAllBytes("FilledForm.pdf");
//        //           return File(filledFormBytes, "application/pdf", "FilledForm.pdf");


//        //       }

//        //public IActionResult GeneratePdf(int? id , int? fk)
//        //{
//        //    string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder
//        //    var productionData = _unitOfWork.itemsRepository.Get(u => u.ID_المنتج == id);

//        //    var tredmarketData = _unitOfWork.TredMarketRepository.Get(u => u.ID == fk);

//        //    string ID = tredmarketData.ID.ToString();
//        //    string صورة_اللوقو = tredmarketData.صورة_الفوتر.ToString();

//        //    string المنتج_Id = productionData.ID_المنتج.ToString();
//        //    string fkProduct = productionData.ID.ToString();

//        //    var itemsHtml = $"<li>{productionData.اسم_الصنف}</li>";

//        //    // Assuming you have a single image path from the database
//        //    string imagePath = $"{wwwRootPath}/IMAGES/الانتاج/{المنتج_Id}/{fkProduct}/{productionData.صورة}";
//        //    string imgاللوقو = $"{wwwRootPath}/IMAGES/BRAND/{ID}/{صورة_اللوقو}";

//        //    // Convert image to Base64
//        //    string base64Image = ConvertImageToBase64(imagePath);
//        //    string base64Imageاللوقو = ConvertImageToBase6433(imgاللوقو);

//        //    // Construct the HTML for the image
//        //    var imgHtml = $"<img src='data:image/png;base64,{base64Image}' alt='Product Image' style='max-width: 100%; height: auto;' />";
//        //    var imgHtmlاللوقو = $"<img src='data:image/png;base64,{base64Imageاللوقو}' alt='Product Image' style='max-width: 300%; height: 50%; margin-top:0%; margin-left: 0%; margin-right:0%; margin-bottom:0%; ' />";

//        //    // Construct the HTML for the image in the header


//        //    var htmlContent = $@"
//        //<html>
//        //    <head>
//        //        <meta charset='utf-8'>
//        //   <div style='text-align: center;'>
//        //         {imgHtmlاللوقو}
//        //          </div>
//        //    </head>
//        //    <body>
//        //        <h2>Data from Database</h2>
//        //        <ul>
//        //            {itemsHtml}
//        //        </ul>

//        //        <h2>Data from Database2</h2>
//        //        <ul>
//        //            {imgHtml}
//        //        </ul>
//        //    </body>
//        //</html>";

//        //    var renderer = new ChromePdfRenderer();


//        //    var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);
//        //    var pdfBytes = pdfDocument.BinaryData;

//        //    return File(pdfBytes, "application/pdf", "GeneratedDocument.pdf");
//        //}


//        public IActionResult GeneratePdf(int? fk)
//        {

//            ProductionVM PrVM = new()
//            {
//                itemsList = new List<Product>(),
//                tredMaeketVM = new العلامة_التجارية(),
//                componontVMList2 = new List<ProductionIngredients>(),
//                stepsVM2 = new List<الخطوات2>(),
//            };

//            PreComViewModel PreVM = new()
//            {
//                PreparationList = new List<التحضيرات1>(),
//                tredMaeketVM = new العلامة_التجارية(),
//                componontVMList = new List<المكونات>(),
//                stepsVM = new List<الخطوات>(),
//            };
//            PrVM.itemsList = _unitOfWork.itemsRepository.GetAll().Where(u => u.ID == fk).ToList();
//            PreVM.PreparationList = _unitOfWork.PreparationRepository.GetAll().Where(u => u.ID == fk).ToList();

//            string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder

//            var tredmarketData = _unitOfWork.TredMarketRepository.Get(u => u.ID == fk);

//            string صورة_اللوقو = tredmarketData.صورة_الفوتر.ToString();


//            string HTML = "<html>" +
//            "<style>" +
//             "table, th, td {" +
//          "border:1px solid black;" +
//             "}" +

//          "header {" +
//          "text-align: left;" +
//          "font-size: 24px;" +
//          "margin-bottom: 10px;" + // Add margin to separate the header from the underline
//             "}" +

//          "underline {" +
//          "border-bottom: 2px solid black;" + // Add the underline style
//          "width: 100%;" +
//          "display: block;" +
//          "margin-bottom: 10px;" + // Add margin to separate the underline from the content
//             "}" +

//              "@media print {" +
//            " body {" +
//            "size: A4;" +
//            " margin: 0;" +
//            "   }" +
//            "@page {" +
//            "size: A4;" +
//            "margin: 1cm;" +
//            "}" +

//           " @page: after {" +
//           " content: counter(page);"+
//           " position: fixed;"+
//           " bottom: 0.5cm;"+
//            "right: 1cm;"+
//           "}"+

//            "}" +
//        "</style>" +
//          "<body>" +
//             $"<header>الانتاج</header>" + // Add the header
//                         "<underline></underline>"; // Add the underline
//            // Iterate through the list of ProductionIngredients and add rows to the HTML table
//            foreach (var items in PrVM.itemsList)
//            {
//                string ID_المنتج = items.ID_المنتج.ToString();
//                string ID = items.ID.ToString();

//                string imgProductPath = $"{wwwRootPath}/IMAGES/الانتاج/{ID_المنتج}/{ID}/{items.صورة}";

//                // Convert image to Base64
//                string base64Image = ConvertImageToBase64(imgProductPath);
//                string imageType = GetImageType(imgProductPath);

//                // Construct the HTML for the image
//                var imgHtml = $"<img src='data:image/{imageType};base64,{base64Image}' alt='Product Image' style='width: 80%; height: 40;' />";
//                HTML += $"<h4>{items.اسم_الصنف}</h4>"+
//                $"{imgHtml}" +
//              "<table>" +
//                  "<tr>" +
//                      "<th>اسم الصنف </th>" +
//                      "<th>نوع  الصنف</th>" +
//                      "<th>المحطة</th>" +
//                      "<th>وقت التحضير </th>" +
//                  "</tr>";

//                HTML += "<tr>" +
//                    $"<td>{items.اسم_الصنف}</td>" +
//                    $"<td>{items.نوع_الصنف}</td>" +
//                    $"<td>{items.المحطة}</td>" +
//                    $"<td>{items.وقت_التحضير}</td>" +
//                    "</tr>";

//                HTML += "</table>" +
//                "<h4>المكونات</h4>" +
//                "<table>" +
//                "<tr>" +
//                "<th>الكمية</th>" +
//                "<th>الوحدة</th>" +
//                "<th>المكون </th>" +
//                "</tr>";

//                PrVM.componontVMList2 = _unitOfWork.ComponentRepository2.GetAll(incloudeProperties: "Product").Where(u => u.ID_الصنف == items.ID_المنتج).ToList();
//                foreach (var component in PrVM.componontVMList2)
//                {
//                    HTML += "<tr>" +
//                        $"<td>{component.المكون}</td>" +
//                        $"<td>{component.الكمية}</td>" +
//                        $"<td>{component.الوحدة}</td>" +
//                        "</tr>";
//                }
//                HTML += "</table>" +
//                  "<h4>طريقة الاعداد </h4>" +
//                  "<table>";

//                PrVM.stepsVM2 = _unitOfWork.StepsPreparationRepository2.GetAll(incloudeProperties: "Product").Where(u => u.ID_الصنف == items.ID_المنتج).ToList();
//                foreach (var Stepsproduct in PrVM.stepsVM2)
//                {
//                    string ID_stepsالصنف = Stepsproduct.ID_الصنف.ToString();
//                    string IDsteps = Stepsproduct.ID.ToString();
//                    string رقم_الخطوة1 = Stepsproduct.رقم_الخطوة1.ToString();
//                    string رقم_الخطوة2 = Stepsproduct.رقم_الخطوة2.ToString();

//                    string imgsteps1tPath = $"{wwwRootPath}/IMAGES/الانتاج/{رقم_الخطوة1}/{ID_stepsالصنف}/{IDsteps}/{Stepsproduct.الصورة1}";
//                    string imgsteps2tPath = $"{wwwRootPath}/IMAGES/الانتاج/{رقم_الخطوة2}/{ID_stepsالصنف}/{IDsteps}/{Stepsproduct.الصورة2}";

//                    // Convert image to Base64
//                    string base64Imagesteps1 = ConvertImageToBase64(imgsteps1tPath);
//                    string base64Imagesteps2 = ConvertImageToBase64(imgsteps2tPath);
//                    string imageTypeSteps1 = GetImageType(imgsteps1tPath);
//                    string imageTypeSteps2 = GetImageType(imgsteps2tPath);

//                    // Construct the HTML for the image
//                    var imgHtmlSteps1 = $"<img src='data:image/{imageTypeSteps1};base64,{base64Imagesteps1}' alt='Steps Image' style='width: 60%; height: 60;' />";
//                    var imgHtmlSteps2 = $"<img src='data:image/{imageTypeSteps2};base64,{base64Imagesteps2}' alt='Steps Image' style='width: 60%; height: 60;' />";

//                    HTML += "<tr>" +
//                        $"<td>{Stepsproduct.رقم_الخطوة1}</td>" +
//                        $"<td>{Stepsproduct.الخطوة1}</td>" +
//                        $"<td>{imgHtmlSteps1}</td>" +
//                        "</tr>"+
//                        "<tr>" +
//                        $"<td>{Stepsproduct.رقم_الخطوة2}</td>" +
//                        $"<td>{Stepsproduct.الخطوة2}</td>" +
//                        $"<td>{imgHtmlSteps2}</td>" +
//                        "</tr>";
//                }
//                HTML += "</table>" ;
//            }
//            ///////////////////////////////////////////////////////////////////////////////////////////////
//            // Add a page break after the table
//            HTML += "<div style='page-break-after: always;'></div>";
//            // Now, you can start a new page with a header or any other content

//            HTML += 
//             $"<header>التحضيرات</header>" + // Add the header
//                         "<underline></underline>"; // Add the underline
//            // Iterate through the list of ProductionIngredients and add rows to the HTML table
//            foreach (var Preparation in PreVM.PreparationList)
//            {
//                string ID_التحضير = Preparation.التحضير_ID.ToString();
//                string ID = Preparation.ID.ToString();

//                string imgPreparationPath = $"{wwwRootPath}/IMAGES/التحضيرات/{ID_التحضير}/{ID}/{Preparation.الصورة_النهائية}";

//                // Convert image to Base64
//                string base64Image = ConvertImageToBase64(imgPreparationPath);
//                string imageType = GetImageType(imgPreparationPath);

//                // Construct the HTML for the image
//                var imgHtml = $"<img src='data:image/{imageType};base64,{base64Image}' alt='Product Image' style='width: 80%; height: 40;' />";
//                HTML += $"<h4>{Preparation.اسم_التحضير}</h4>" +
//                $"{imgHtml}" +
//              "<table>" +
//                  "<tr>" +
//                      "<th>اسم التحضير </th>" +
//                      "<th>الوزن الصافي</th>" +
//                      "<th>المحطة</th>" +
//                      "<th>وقت التحضير </th>" +
//                  "</tr>";

//                HTML += "<tr>" +
//                    $"<td>{Preparation.اسم_التحضير}</td>" +
//                    $"<td>{Preparation.الوزن_الصافي}</td>" +
//                    $"<td>{Preparation.المحطة}</td>" +
//                    $"<td>{Preparation.وقت_التحضير}</td>" +
//                    "</tr>";

//                HTML += "</table>" +
//                "<h3>المكونات</h3>" +
//                "<table>" +
//                "<tr>" +
//                "<th>الكمية</th>" +
//                "<th>الوحدة</th>" +
//                "<th>المكون </th>" +
//                "</tr>";

//                PreVM.componontVMList = _unitOfWork.ComponentRepository.GetAll(incloudeProperties: "التحضيرات").Where(u => u.ID_التحضير == Preparation.التحضير_ID).ToList();
//                foreach (var component in PrVM.componontVMList2)
//                {
//                    HTML += "<tr>" +
//                        $"<td>{component.المكون}</td>" +
//                        $"<td>{component.الكمية}</td>" +
//                        $"<td>{component.الوحدة}</td>" +
//                        "</tr>";
//                }
//                HTML += "</table>";

//            }

//            HTML += "</body>" +
//                    "</html>";

//            try
//            {

//                var renderer = new ChromePdfRenderer();
//                renderer.RenderingOptions.Timeout = 200; // Set the timeout to 2 minutes (in seconds)
//                var pdfDocument = renderer.RenderHtmlAsPdf(HTML); 
//                var pdfBytes = pdfDocument.BinaryData;
//                return File(pdfBytes, "application/pdf", "GeneratedDocument.pdf");
//            }
//            catch (Exception ex)
//            {
//                // Log or handle the exception
//                Console.WriteLine("PDF generation error: " + ex.Message);
//                return BadRequest("Error generating PDF");
//            }
//        }


//        private string ConvertImageToBase64(string imagePath)
//        {
//            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
//            return Convert.ToBase64String(imageBytes);
//        }



//        private string GetImageType(string imagePath)
//        {
//            // Get the file extension from the image path
//            string fileExtension = Path.GetExtension(imagePath)?.TrimStart('.');
//            return fileExtension;
//        }
//    }
//}
