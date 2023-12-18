﻿
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.Production;
using Test12.Models.Models.trade_mark;
using Test12.Models.ViewModel;


namespace Test12.Controllers
{
    public class ProductionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductionController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;

        }

        public IActionResult ProductionList(int? id) //this for display List Of التحضيرات Page1
        {
            IEnumerable<Production> objPreparationList = _unitOfWork.itemsRepository.GetAll(incloudeProperties: "component2")
                .Where(u => u.BrandFK == id).OrderBy(item => item.ProductionOrder).ToList();

            // Store the FK value in TempData
            TempData["ID"] = id;
            // Display the updated list
            return View(objPreparationList);
        }

        public IActionResult Upsert1(int? id) // After Enter تعديل Display التحضيرات والمكونات...
        {
            ProductionVM PrVM = new()
            {
                Productionvm = new Production(),
                tredMaeketVM = new Brands(),
                componontVMList2 = new List<ProductionIngredients>(),
                ToolsVarityVM2 = new List<ProductionTools>(),
                stepsVM2 = new List<ProductionSteps>(),

            };
            PrVM.tredMaeketVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            PrVM.Productionvm = _unitOfWork.itemsRepository.Get(u => u.ProductionID == id);
            PrVM.componontVMList2 = _unitOfWork.ComponentRepository2.GetAll(incloudeProperties: "Production").Where(c => c.ProductionFK == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view
            PrVM.ToolsVarityVM2 = _unitOfWork.PrepaToolsVarietyRepository2.GetAll(incloudeProperties: "Production").Where(c => c.ProductionFK == id).ToList(); //هو يحتوي على قائمة من جدول الأدوات واللي يساعده على العرض هي viewD
            PrVM.stepsVM2 = _unitOfWork.StepsPreparationRepository2.GetAll(incloudeProperties: "Production").Where(c => c.ProductionFK == id).ToList(); //هو يحتوي على قائمة من جدول الأدوات واللي يساعده على العرض هي viewD
            return View(PrVM);
        }



        public IActionResult CreateProduction(int? id) // After Enter تعديل Display التحضيرات والمكونات...
        {
            ProductionVM PrVM = new()
            {
                Productionvm = new Production(),
                tredMaeketVM = new Brands(),
                componontVMList2 = new List<ProductionIngredients>(),
                ToolsVarityVM2 = new List<ProductionTools>(),
                stepsVM2 = new List<ProductionSteps>(),
                itemList33333 = new List<Production>(),

            };

            PrVM.Productionvm = new Production();
            PrVM.componontVMList2 = new List<ProductionIngredients>();
            PrVM.ToolsVarityVM2 = new List<ProductionTools>();
            PrVM.stepsVM2 = new List<ProductionSteps>();

            PrVM.tredMaeketVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            PrVM.itemsList = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == id);

            PrVM.itemList33333 = new List<Production>()
            {
         new Production { ProductType = "الأطباق الرئيسية" },
         new Production { ProductType = "المشروبات الساخنة" },
         new Production { ProductType = "المشروبات الباردة" },
         new Production { ProductType = "المقبلات الباردة" },
         new Production { ProductType = "الفطور" },
         new Production { ProductType = " الحلى" },
         new Production { ProductType = "الأطباق الجانبية" },
         new Production { ProductType = "وجبات الأطفال" },
         new Production { ProductType = "السلطات" },
         new Production { ProductType = "الشوربات" },
         new Production { ProductType = "الموهيتو" }

            };

            return View(PrVM);
        }

        [HttpPost]
        public ActionResult CreateProduction(ProductionVM PropaVM, IFormFile? file, int selectedValue, int selectPreparation1)
        {
            if (ModelState.IsValid)
            {
                var FKProduct = PropaVM.tredMaeketVM.BrandID;

                var type = _unitOfWork.itemsRepository.Get(u => u.ProductionID == selectPreparation1);

                if (PropaVM.Productionvm.ProductionID == 0)  // if Add 
                {

                    var setFK = new Production
                    {
                        BrandFK = FKProduct,
                        ProductName = PropaVM.Productionvm.ProductName,
                        PreparationTime = PropaVM.Productionvm.PreparationTime,
                        VersionNumber = PropaVM.Productionvm.VersionNumber,
                        Expiry = PropaVM.Productionvm.Expiry,
                        ProductType = PropaVM.Productionvm.ProductType,
                        Station = PropaVM.Productionvm.Station,

                    };

                    _unitOfWork.itemsRepository.Add(setFK);
                    _unitOfWork.Save();

                    //this code for image if add or update.
                    string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder

                    if (file != null)
                    {

                        // Convert numeric values to strings
                        string ProductionID = setFK.ProductionID.ToString(); // Convert to string
                        string ProductionVMFK = PropaVM.tredMaeketVM.BrandID.ToString(); // Convert to string

                        // Combine paths using Path.Combine, ensuring all arguments are strings
                        // Combine paths using Path.Combine, ensuring all arguments are strings
                        string ProductionDirectory = System.IO.Path.Combine(wwwRootPath, "IMAGES", ProductionVMFK, "Production", ProductionID);

                        //اذا المسار مش موجود سو مسار جديد 
                        if (!Directory.Exists(ProductionDirectory))
                        {
                            Directory.CreateDirectory(ProductionDirectory);
                        }

                        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                        string ProductionPath = System.IO.Path.Combine(ProductionDirectory, fileName);

                        // Use the correct file path when creating FileStream
                        using (var stream = new FileStream(ProductionPath, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                        }

                        setFK.ProductImage = fileName; // Save only the file name in the database
                        _unitOfWork.Save();
                    }

                    int ID_الصنف = setFK.ProductionID;
                    //المكونات
                    var firstComponent = new ProductionIngredients
                    {
                        ProductionFK = ID_الصنف,
                        ProdIngredientsName = Request.Form["ProdIngredientsName"], // Retrieve data from form
                        ProdUnit = Request.Form["ProdUnit"],
                        ProdQuantity = Request.Form["ProdQuantity"]
                    };

                    _unitOfWork.ComponentRepository2.Add(firstComponent);
                    _unitOfWork.Save();

                    if (PropaVM.componontVMList2 != null && PropaVM.componontVMList2.Any())
                    { // if condition checks whether the PropaVM.componontVMList is not null and contains at least one item. 
                        foreach (var componentAdd in PropaVM.componontVMList2)
                        {
                            if (componentAdd != null && componentAdd.ProductionFK == 0)
                            {

                                int componentId = PropaVM.Productionvm.ProductionID;

                                var newComponent = new ProductionIngredients
                                {
                                    ProductionFK = ID_الصنف,
                                    ProdQuantity = componentAdd.ProdQuantity,
                                    ProdUnit = componentAdd.ProdUnit,
                                    ProdIngredientsName = componentAdd.ProdIngredientsName
                                };
                                _unitOfWork.ComponentRepository2.Add(newComponent);
                                _unitOfWork.Save();
                            }
                        }
                    }
                    //أدوات التحضير والصنف2 
                    var firstRowToolAdd = new ProductionTools
                    {
                        ProductionFK = ID_الصنف,
                        ProdTools = Request.Form["ProdTools"],
                    };
                    _unitOfWork.PrepaToolsVarietyRepository2.Add(firstRowToolAdd);
                    _unitOfWork.Save();

                    if (PropaVM.ToolsVarityVM2 != null && PropaVM.ToolsVarityVM2.Any())
                    {
                        foreach (var ToolAdd in PropaVM.ToolsVarityVM2)
                        {
                            if (ToolAdd != null && ToolAdd.ProdToolsID == 0)
                            {

                                var newtool = new ProductionTools
                                {
                                    ProductionFK = ID_الصنف,
                                    ProdTools = ToolAdd.ProdTools
                                };
                                _unitOfWork.PrepaToolsVarietyRepository2.Add(newtool);
                                _unitOfWork.Save();
                            }
                        }
                    }

                    //2الخطوات
                    if (PropaVM.stepsVM2 != null)
                    {
                        foreach (var stepAdd in PropaVM.stepsVM2)
                        {

                            if (stepAdd != null && stepAdd.ProdStepsID == 0)
                            {
                                string wwwRootstepPath = _webHostEnvironment.WebRootPath; // get us root folder

                                var newStep = new ProductionSteps
                                {
                                    ProductionFK = ID_الصنف,
                                    ProdText = stepAdd.ProdText,
                                    ProdStepsNum = stepAdd.ProdStepsNum,

                                };
                                _unitOfWork.StepsPreparationRepository2.Add(newStep);
                                _unitOfWork.Save();

                                var file1Name1 = $"file1_{newStep.ProdStepsID}";
                                var file1ForStep1 = HttpContext.Request.Form.Files[file1Name1];


                                string BrandFK = setFK.BrandFK.ToString();
                                string PropIDstep = newStep.ProdStepsID.ToString();

                                string stepPath1 = System.IO.Path.Combine(wwwRootPath, "IMAGES", BrandFK, "Production", PropIDstep);

                                if (file1ForStep1 != null && file1ForStep1.Length > 0)
                                {
                                    string fileName11 = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file1ForStep1.FileName);

                                    if (!Directory.Exists(stepPath1))
                                    {
                                        Directory.CreateDirectory(stepPath1);
                                    }

                                    using (var fileStream = new FileStream(System.IO.Path.Combine(stepPath1, fileName11), FileMode.Create)) //save images
                                    {
                                        file1ForStep1.CopyTo(fileStream);
                                    }
                                    newStep.ProdSImage = fileName11;
                                }

                                _unitOfWork.Save();
                            }
                        }
                    }

                    //// reOrder2 
                    if (selectedValue == 0)
                    {
                        // Get the maximum order value in the existing list
                        double maxOrder = _unitOfWork.itemsRepository.GetAll()
                            .Max(item => item.ProductionOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                        // Round down the maxOrder value to the nearest integer
                        int maxOrderAsInt = (int)Math.Floor(maxOrder);

                        // Set the new order value for the "اخرى" (Other) item
                        double newOrder = maxOrderAsInt + 1.0f;
                        setFK.ProductionOrder = newOrder;
                    }
                    else
                    {
                        var getIdOrder = _unitOfWork.itemsRepository.Get(u => u.ProductionID == selectedValue);
                        double OldOrder = getIdOrder.ProductionOrder ?? 0.0f; // Default to 0.0f if Order is null
                        double newOrder = OldOrder + 0.1f;
                        setFK.ProductionOrder = newOrder;
                    }

                    List<Production> objReorderlist = _unitOfWork.itemsRepository.GetAll().OrderBy(item => item.ProductionOrder).ToList();
                    _unitOfWork.Save();
                    TempData["success"] = "تم إضافة الاصناف بشكل ناجح";
                }

            }
            return RedirectToAction("ProductionList", new { id = PropaVM.tredMaeketVM.BrandID });
        }


        [HttpPost] //This for Add Or Update Page . 
        public IActionResult Upsert1(ProductionVM PropaVM, IFormFile? file, int selectedValue) // should insert name in Upsert view
        {
            if (!ModelState.IsValid)
            {

                //for update .. 
                int preparationID = PropaVM.Productionvm.ProductionID;
                int toolVarityID = PropaVM.Productionvm.ProductionID;
                int stepsID = PropaVM.Productionvm.ProductionID;

                string ProductionID = PropaVM.Productionvm.ProductionID.ToString();
                string ProductionFK = PropaVM.Productionvm.BrandFK.ToString();
                //this code for image if add or update.
                string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                    // Construct the folder path where the image will be saved
                    string folderPath = System.IO.Path.Combine(wwwRootPath, "IMAGES", ProductionFK, "Production", ProductionID);
                    string ProductionPath = Path.Combine(folderPath, fileName);

                    // Ensure the directory exists
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Delete old image if it exists
                    if (!string.IsNullOrEmpty(PropaVM.Productionvm.ProductImage))
                    {
                        var oldImagePath = System.IO.Path.Combine(folderPath, PropaVM.Productionvm.ProductImage);

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save the image with the new file name
                    using (var fileStream = new FileStream(System.IO.Path.Combine(ProductionPath), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Store only the file name in the database
                    PropaVM.Productionvm.ProductImage = fileName;
                }

                _unitOfWork.itemsRepository.Update(PropaVM.Productionvm); // تحديث Product
                _unitOfWork.Save();

                //ProductionIngredients 
                if (PropaVM.componontVMList2 != null) // تحديث المكونات
                {
                    foreach (var component in PropaVM.componontVMList2)
                    {

                        if (component.ProductionFK == preparationID)
                        {
                            var existingComponent = _unitOfWork.ComponentRepository2.Get(u => u.ProdIngredientsID == component.ProdIngredientsID, incloudeProperties: "Production");
                            if (existingComponent != null)//if is exit from database
                            {
                                existingComponent.ProdQuantity = component.ProdQuantity;
                                existingComponent.ProdUnit = component.ProdUnit;
                                existingComponent.ProdIngredientsName = component.ProdIngredientsName;

                                _unitOfWork.ComponentRepository2.Update(existingComponent);
                                _unitOfWork.Save();
                            }
                            else //if add new row and click عدل
                            {
                                _unitOfWork.ComponentRepository2.Add(component);
                                _unitOfWork.Save();
                            }
                        }
                    }
                }
                //أدوات التحضير والصنف2
                if (PropaVM.ToolsVarityVM2 != null) //تحديث الأدوات.
                {
                    foreach (var toolvariety in PropaVM.ToolsVarityVM2)
                    {

                        if (toolvariety.ProductionFK == toolVarityID)
                        {
                            var existingtoolvariety = _unitOfWork.PrepaToolsVarietyRepository2.Get(u => u.ProdToolsID == toolvariety.ProdToolsID, incloudeProperties: "Production");
                            if (existingtoolvariety != null)//if is exit from database
                            {
                                existingtoolvariety.ProdToolsID = toolvariety.ProdToolsID;

                                _unitOfWork.PrepaToolsVarietyRepository2.Update(existingtoolvariety);
                                _unitOfWork.Save();
                            }
                            else //if add new row and click عدل
                            {
                                _unitOfWork.PrepaToolsVarietyRepository2.Add(toolvariety);
                                _unitOfWork.Save();
                            }
                        }
                    }
                }
                //الخطوات2 
                if (PropaVM.stepsVM2 != null)
                {
                    for (int i = 0; i < PropaVM.stepsVM2.Count; i++)
                    {
                        var Steps = PropaVM.stepsVM2[i];

                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder

                        var existingSteps9 = _unitOfWork.StepsPreparationRepository2.Get(u => u.ProdStepsID == Steps.ProdStepsID);
                        if (existingSteps9 == null)
                        {
                            _unitOfWork.StepsPreparationRepository2.Add(Steps);
                            _unitOfWork.Save();
                        }

                        string IDstep = Steps.ProdStepsID.ToString();
                        string StepsPath = Path.Combine(wwwRootPathSteps, "IMAGES", ProductionFK, "Production", IDstep);

                        var file1Name = $"file1_{Steps.ProdStepsID}";
                        var file1ForStep = HttpContext.Request.Form.Files[file1Name];

                        if (file1ForStep != null)
                        {
                            if (!string.IsNullOrEmpty(Steps.ProdSImage)) // Check if there's an existing image path
                            {
                                var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", ProductionFK, "Production", IDstep, Steps.ProdSImage);

                                if (System.IO.File.Exists(OldImagePath1))
                                {
                                    System.IO.File.Delete(OldImagePath1); // Delete old image if it exists
                                }
                            }

                            string fileNameSteps1 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForStep.FileName);


                            //اذا المسار مش موجود سو مسار جديد 
                            if (!Directory.Exists(StepsPath))
                            {
                                Directory.CreateDirectory(StepsPath);
                            }

                            using (var fileStream1 = new FileStream(Path.Combine(StepsPath, fileNameSteps1), FileMode.Create))
                            {
                                file1ForStep.CopyTo(fileStream1);
                            }

                            Steps.ProdSImage = fileNameSteps1; // Update the image path
                        }


                        // Save or update Steps data to the database
                        if (Steps.ProductionFK == stepsID)
                        {
                            var existingSteps = _unitOfWork.StepsPreparationRepository2.Get(u => u.ProdStepsID == Steps.ProdStepsID);

                            if (existingSteps != null)
                            {

                                existingSteps.ProdText = Steps.ProdText;
                                existingSteps.ProdSImage = Steps.ProdSImage;
                                existingSteps.ProdStepsNum = Steps.ProdStepsNum;

                                _unitOfWork.StepsPreparationRepository2.Update(existingSteps);
                            }
                            else
                            {
                                _unitOfWork.StepsPreparationRepository2.Add(Steps);
                            }
                            _unitOfWork.Save();
                        }
                    }
                }

                TempData["success"] = "تم تحديث الانتاج بشكل ناجح";
                TempData["ID"] = PropaVM.Productionvm.BrandFK;
                return RedirectToAction("ProductionList", new { id = PropaVM.Productionvm.BrandFK });
            }
            else
            {
                return View(PropaVM);
            }
        }

        [HttpGet]
        public IActionResult GetLastId()
        {
            try
            {
                int lastId = _unitOfWork.StepsPreparationRepository2.GetLastStepId();
                return Ok(lastId);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return StatusCode(500, ex.Message);
            }
        }

        //زر حذف صفحة تعديل الأدوات2 
        #region API CALLS 
        [HttpDelete]
        public IActionResult DeleteToolVariety2(int? id) //this is for delete button in rows أدوات التحضير والصنف
        {
            var toolsVarityDelete = _unitOfWork.PrepaToolsVarietyRepository2.Get(u => u.ProdToolsID == id);
            if (toolsVarityDelete == null)
            {

                return Json(new { success = false, Message = "Error While Deleting" });
            }

            _unitOfWork.PrepaToolsVarietyRepository2.Remove(toolsVarityDelete);
            _unitOfWork.Save();
            return Json(new { success = true });
        }
        #endregion

        // تبع List 
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            IEnumerable<Production> objProductionList = _unitOfWork.itemsRepository.GetAll(incloudeProperties: "component2")
                .Where(u => u.BrandFK == id).OrderBy(item => item.ProductionOrder).OrderBy(item => item.ProductionOrder).ToList();

            return Json(new { data = objProductionList });
        }
        #endregion


        // 2زر الحذف تبع المكونات 
        #region API CALLS 
        [HttpDelete]
        public IActionResult Delete(int? id) //this is for delete button in rows component 
        {
            var ComponentDelete2 = _unitOfWork.ComponentRepository2.Get(u => u.ProdIngredientsID == id);
            if (ComponentDelete2 == null)
            {
                return Json(new { success = false, Message = "Error While Deleting" });
            }

            _unitOfWork.ComponentRepository2.Remove(ComponentDelete2);
            _unitOfWork.Save();
            return Json(new { success = true });
        }
        #endregion

        //2زر الحذف في صفحة قائمة Product 
        #region
        [HttpDelete]
        public IActionResult DeletePreparationPost(int? id)
        {
            var DeleteTools = _unitOfWork.PrepaToolsVarietyRepository2.GetAll(incloudeProperties: "Production").Where(u => u.ProductionFK == id).ToList();
            _unitOfWork.PrepaToolsVarietyRepository2.RemoveRange(DeleteTools);


            var DelteComponent = _unitOfWork.ComponentRepository2.GetAll(incloudeProperties: "Production").Where(u => u.ProductionFK == id).ToList();
            _unitOfWork.ComponentRepository2.RemoveRange(DelteComponent);

            var Deletesteps = _unitOfWork.StepsPreparationRepository2.GetAll(incloudeProperties: "Production").Where(u => u.ProductionFK == id).ToList();

            // Check if Deletesteps is not null
            if (Deletesteps != null)
            {
                for (int i = 0; i < Deletesteps.Count; i++)
                {
                    var delet = Deletesteps[i];

                    var BrandId = _unitOfWork.itemsRepository.Get(u => u.ProductionID == delet.ProdStepsID);
                    var IDstep = _unitOfWork.StepsPreparationRepository2.Get(u => u.ProdStepsID == delet.ProdStepsID);

                    string IDStep = IDstep.ProdStepsID.ToString();
                    string FKBrand = BrandId.BrandFK.ToString();

                    // Delete the associated image files
                    if (!string.IsNullOrEmpty(delet.ProdSImage))
                    {
                        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IMAGES", FKBrand, "Production", IDStep, delet.ProdSImage);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }


                    // Remove the entity from the repository
                    _unitOfWork.StepsPreparationRepository2.Remove(delet);
                }
            }

            var DeleteoneOflist = _unitOfWork.itemsRepository.Get(u => u.ProductionID == id);
            if (DeleteoneOflist == null)
            {

                return Json(new { success = false, Message = "خطأ أثناء الحذف " });
            }

            _unitOfWork.itemsRepository.Remove(DeleteoneOflist);
            _unitOfWork.Save();
            return Json(new { success = true });
        }
        #endregion


        //زر الحذف تبع صفحة تعديل الخطوات
        #region API CALLS
        [HttpDelete]
        public IActionResult Deletestep(int? id)
        {
            var stepsToDelete = _unitOfWork.StepsPreparationRepository2.Get(u => u.ProdStepsID == id);
            var BrandFK = _unitOfWork.itemsRepository.Get(u => u.ProductionID == stepsToDelete.ProductionFK);

            string ProStepNum = stepsToDelete.ProdStepsNum != null ? stepsToDelete.ProdStepsNum.ToString() : string.Empty;

            string IDStep = stepsToDelete.ProdStepsID.ToString();
            string FKBrand = BrandFK.BrandFK.ToString();

            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            if (stepsToDelete == null)
            {
                return Json(new { success = false, Message = "Error While Deleting" });
            }

            // Delete the associated image file
            if (!string.IsNullOrEmpty(stepsToDelete.ProdSImage))
            {
                string imagePath = Path.Combine(wwwRootPathSteps, "IMAGES", FKBrand, "Production", IDStep, stepsToDelete.ProdSImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _unitOfWork.StepsPreparationRepository2.Remove(stepsToDelete);
            _unitOfWork.Save();

            // Find all steps with a higher PrepStepsNum
            var ProductionFK = stepsToDelete.ProductionFK;

            var subsequentSteps = _unitOfWork.StepsPreparationRepository2
                .GetAll(incloudeProperties: "Production").Where(u => u.ProductionFK == ProductionFK).ToList(); // Add ToList() to materialize the query;

            // Decrement PrepStepsNum for each subsequent step
            for (int i = 0; i < subsequentSteps.Count; i++)
            {
                var step = subsequentSteps[i];

                if (step.ProdStepsID > id)
                {
                    var getOld = _unitOfWork.StepsPreparationRepository2.Get(u => u.ProdStepsID == step.ProdStepsID);
                    getOld.ProdStepsNum -= 1;
                    _unitOfWork.StepsPreparationRepository2.Update(step);
                }
            }

            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        #endregion


        //public IActionResult GeneratePdf(int? id)
        //{
        //    string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder


        //    ProductionVM PrVM = new()
        //    {
        //        Productionvm = new Product(),
        //        tredMaeketVM = new العلامة_التجارية(),
        //    };
        //    PrVM.Productionvm = _unitOfWork.itemsRepository.Get(u => u.ID_المنتج == id);

        //    string المنتج_Id = PrVM.Productionvm.ID_المنتج.ToString();
        //    string fkProduct = PrVM.Productionvm.ID.ToString();

        //    using (var stream = new MemoryStream())
        //    {
        //        // Set the page size to A4
        //        var pageSize = new iText.Kernel.Geom.PageSize(595, 842); // Width x Height in points (1 inch = 72 points)

        //        using (var writer = new PdfWriter(stream))
        //        {
        //            using (var pdf = new PdfDocument(writer))
        //            {
        //                pdf.SetDefaultPageSize(pageSize);

        //                using (var document = new Document(pdf))
        //                {
        //                    // HEADER . 
        //                    // Assuming your header image file is named "header_image.jpg" and is placed in the "Images" folder
        //                    var headerImageFileName = PrVM.Productionvm.صورة;
        //                    var fullHeaderImagePath = System.IO.Path.Combine(wwwRootPath, "IMAGES", "الانتاج", المنتج_Id, fkProduct, headerImageFileName);

        //                    // Use the full path to create the Image
        //                    var headerImage = new Image(ImageDataFactory.Create(fullHeaderImagePath));

        //                    // Set the size of the header image to span the full width of A4
        //                    headerImage.SetWidth(pageSize.GetWidth());
        //                    headerImage.SetHeight(70); // Set the height in points
        //                                               // Set the top margin of the header image to 0
        //                    headerImage.SetMarginTop(-40);
        //                    headerImage.SetMarginBottom(0);
        //                    headerImage.SetMarginLeft(-36);
        //                    headerImage.SetMarginRight(10);

        //                    // Add the header image to the document
        //                    document.Add(headerImage);

        //                    // Content . 
        //                    var fontFileName = "NotoNaskhArabic-VariableFont_wght.ttf";
        //                    var fontPath = System.IO.Path.Combine("Font", fontFileName);
        //                    var fullFontPath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, fontPath);

        //                    // Use the full path to create the font
        //                    var arabicFont = PdfFontFactory.CreateFont(fullFontPath, PdfEncodings.IDENTITY_H);
        //                    var fontMetrics = arabicFont.GetFontProgram().GetFontMetrics();

        //                    document.SetFont(arabicFont);

        //                    // Set styles for text
        //                    var titleStyle = new Style()
        //                        .SetFontSize(18)
        //                        .SetFont(arabicFont)
        //                        .SetTextAlignment(TextAlignment.CENTER);

        //                    var labelStyle = new Style()
        //                        .SetFontSize(28)
        //                        .SetFont(arabicFont)
        //                        .SetTextAlignment(TextAlignment.RIGHT)
        //                        .SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);

        //                    var invertedArabicStyle = new Style()
        //                        .SetFont(arabicFont)
        //                        .SetFontSize(14)
        //                        .SetBaseDirection(BaseDirection.LEFT_TO_RIGHT);

        //                    //var inputStyle = new Style()
        //                    //    .SetFontSize(16)
        //                    //    .SetFont(arabicFont)
        //                    //    .SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);

        //                    document.Add(new Paragraph("مرحبا بك في العالم").AddStyle(invertedArabicStyle));

        //                    document.Add(new Paragraph("Production Information").AddStyle(titleStyle));
        //                    document.Add(new Paragraph($"اسم الصنف: {PrVM.Productionvm.اسم_الصنف}").AddStyle(labelStyle));
        //                    document.Add(new Paragraph($"رقم النسخة: {PrVM.Productionvm.رقم_النسخة}").AddStyle(labelStyle));
        //                    document.Add(new Paragraph($"مدة الصلاحية: {PrVM.Productionvm.مدة_الصلاحية}").AddStyle(labelStyle));
        //                    document.Add(new Paragraph($"نوع الصنف: {PrVM.Productionvm.نوع_الصنف}").AddStyle(labelStyle));
        //                    //document.Add(new Paragraph($"المحطة: {productionData.المحطة}").AddStyle(inputStyle));
        //                    //document.Add(new Paragraph($"وقت التحضير: {productionData.وقت_التحضير}").AddStyle(inputStyle));

        //                    // Add more iText7 content as needed
        //                }
        //            }
        //        }

        //        // Return the PDF as a FileResult
        //        return File(stream.ToArray(), "application/pdf", "ProductionInfo.pdf");
        //    }
        //}
        //} public IActionResult generatePdf(int? id)
        //{
        //    string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder
        //    var productionData = _unitOfWork.itemsRepository.Get(u => u.ID_المنتج == id);

        //    string المنتج_Id = productionData.ID_المنتج.ToString();
        //    string fkProduct = productionData.ID.ToString();
        //    // Create a MemoryStream to store the PDF content

        //    using (var stream = new MemoryStream())
        //    {
        //        // Set the page size to A4
        //        var pageSize = new iText.Kernel.Geom.PageSize(595, 842); // Width x Height in points (1 inch = 72 points)

        //        using (var writer = new PdfWriter(stream))
        //        {
        //            using (var pdf = new PdfDocument(writer))
        //            {
        //                pdf.SetDefaultPageSize(pageSize);

        //                using (var document = new Document(pdf))
        //                {
        //                    // HEADER . 
        //                    // Assuming your header image file is named "header_image.jpg" and is placed in the "Images" folder
        //                    var headerImageFileName = productionData.صورة;
        //                    var fullHeaderImagePath = System.IO.Path.Combine(wwwRootPath, "IMAGES", "الانتاج", المنتج_Id, fkProduct, headerImageFileName);

        //                    // Use the full path to create the Image
        //                    var headerImage = new Image(ImageDataFactory.Create(fullHeaderImagePath));

        //                    // Set the size of the header image to span the full width of A4
        //                    headerImage.SetWidth(pageSize.GetWidth());
        //                    headerImage.SetHeight(70); // Set the height in points
        //                                               // Set the top margin of the header image to 0
        //                    headerImage.SetMarginTop(-40);
        //                    headerImage.SetMarginBottom(0);
        //                    headerImage.SetMarginLeft(-36);
        //                    headerImage.SetMarginRight(10);

        //                    // Add the header image to the document
        //                    document.Add(headerImage);

        //                    // Content . 
        //                    // Assuming your font file is named "aldhabi.ttf" and is placed in the "Fonts" folder
        //                    var fontFileName = "arabtype.ttf";
        //                    var fontPath = System.IO.Path.Combine("Font","Fonts", fontFileName);


        //                    // Combine with the ContentRootPath or WebRootPath, depending on your project structure
        //                    // With this line
        //                    var fullFontPath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, fontPath);

        //                    // Use the full path to create the font
        //                    var arabicFont = PdfFontFactory.CreateFont(fullFontPath, PdfEncodings.IDENTITY_H);

        //                    document.SetFont(arabicFont);

        //                    // Set styles for text
        //                    var titleStyle = new Style()
        //                        .SetFontSize(18)
        //                        .SetFont(arabicFont)
        //                        .SetTextAlignment(TextAlignment.CENTER)
        //                        .SetBaseDirection(BaseDirection.NO_BIDI);

        //                    var labelStyle = new Style()
        //                        .SetFontSize(16)
        //                        .SetFont(arabicFont)
        //                        .SetBaseDirection(BaseDirection.LEFT_TO_RIGHT);

        //                    var inputStyle = new Style()
        //                        .SetFontSize(16)
        //                        .SetFont(arabicFont)
        //                        .SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);


        //                    document.Add(new Paragraph("Production Information").AddStyle(titleStyle));
        //                    document.Add(new Paragraph($"اسم الصنف: {productionData.اسم_الصنف}").AddStyle(labelStyle));
        //                    document.Add(new Paragraph($"رقم النسخة: {productionData.رقم_النسخة}").AddStyle(labelStyle));
        //                    document.Add(new Paragraph($"مدة الصلاحية: {productionData.مدة_الصلاحية}").AddStyle(labelStyle));
        //                    document.Add(new Paragraph($"نوع الصنف: {productionData.نوع_الصنف}").AddStyle(labelStyle));
        //                    document.Add(new Paragraph($"المحطة: {productionData.المحطة}").AddStyle(inputStyle));
        //                    document.Add(new Paragraph($"وقت التحضير: {productionData.وقت_التحضير}").AddStyle(inputStyle));

        //                    // Add more iText7 content as needed
        //                }
        //            }
        //        }

        //        // Return the PDF as a FileResult
        //        return File(stream.ToArray(), "application/pdf", "ProductionInfo.pdf");
        //    }
        //}

    }
}






