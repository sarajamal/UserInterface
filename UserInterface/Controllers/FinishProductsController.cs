using Microsoft.AspNetCore.Mvc;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.ReadyFood;
using Test12.Models.Models.trade_mark;
using Test12.Models.ViewModel;

namespace Test12.Controllers
{
    public class FinishProductsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FinishProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;

        }
        //-------------------------------GET LIST------------------------------------
        public IActionResult RedirectToFinishProductionList(int brandFK)
        {
            TempData["BrandFK"] = brandFK;
            TempData.Keep("BrandFK");

            return RedirectToAction("finishProductionList");
        }
        public IActionResult finishProductionList() //this for display List Of التحضيرات Page1
        {
            int? brandFK = TempData["BrandFK"] as int?;
            TempData.Keep("BrandFK"); // Keep the TempData for further use

            LoginTredMarktViewModel FooReadyVM = new()
            {
                ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll()
                 .Where(u => u.BrandFK == brandFK).OrderBy(item => item.ReadyProductsOrder).ToList(),
                WelcomTredmarketReadyFood = new LoginTredMarktViewModel()

            };
            FooReadyVM.WelcomTredmarketReadyFood.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK);
            FooReadyVM.WelcomTredmarketReadyFood.DeviceToolsLoginVM = _unitOfWork.DevicesAndTools.Get(u => u.BrandFK == brandFK);
            FooReadyVM.WelcomTredmarketReadyFood.Productionvm = _unitOfWork.itemsRepository.Get(u => u.BrandFK == brandFK);
            FooReadyVM.WelcomTredmarketReadyFood.CleanViewModel = _unitOfWork.CleanRepository.Get(u => u.BrandFK == brandFK);
            FooReadyVM.WelcomTredmarketReadyFood.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == brandFK);
            FooReadyVM.WelcomTredmarketReadyFood.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == brandFK);
            FooReadyVM.WelcomTredmarketReadyFood.PreparationVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == brandFK);
            FooReadyVM.WelcomTredmarketReadyFood.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FooReadyVM.WelcomTredmarketReadyFood.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FooReadyVM.WelcomTredmarketReadyFood.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FooReadyVM.WelcomTredmarketReadyFood.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FooReadyVM.WelcomTredmarketReadyFood.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FooReadyVM.WelcomTredmarketReadyFood.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FooReadyVM.WelcomTredmarketReadyFood.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == brandFK).ToList();

            TempData["ID"] = brandFK;

            // Display the updated list
            return View(FooReadyVM);
        }

        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            IEnumerable<ReadyProducts> objfoodList = _unitOfWork.readyFoodRepository.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.ReadyProductsOrder).ToList();

            return Json(new { data = objfoodList });
        }
        #endregion
        //============================================================================

        //-------------------------------صفحة التعديل---------------------------------
        public IActionResult FinishProductsIndex(int? id)
        {
            TempData.Keep("BrandFK"); // Keep the TempData for further use

            LoginTredMarktViewModel RDVM = new()
            {
                ReadyFoodLoginVM = new ReadyProducts(),
                ReadyFoodLoginVMlist = new List<ReadyProducts>(),
                tredMaeketReadyfoodVM = new Brands(),

            };

            RDVM.tredMaeketReadyfoodVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            RDVM.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == id);
            RDVM.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll(incloudeProperties: "Brand").Where(u => u.ReadyProductsID == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

            return View(RDVM);
        }
        [HttpPost]
        public async Task<IActionResult> FinishProductsIndex(LoginTredMarktViewModel foodReadyViewModel)
        {
            if (ModelState.IsValid)
            {
                if (foodReadyViewModel.ReadyFoodLoginVMlist != null)
                {
                    for (int i = 0; i < foodReadyViewModel.ReadyFoodLoginVMlist.Count; i++)
                    {
                        var foodready = foodReadyViewModel.ReadyFoodLoginVMlist[i];

                        string ReadyProductsID = foodready.ReadyProductsID.ToString();
                        string BrandFK = foodready.BrandFK.ToString();

                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder
                        var FoodPath1 = Path.Combine(wwwRootPathSteps, "IMAGES", ReadyProductsID);

                        var file1Name = $"file1_{foodready.ReadyProductsID}";
                        var file1Forfoods = HttpContext.Request.Form.Files[file1Name];

                        if (file1Forfoods != null)
                        {
                            if (!string.IsNullOrEmpty(foodready.ReadyProductsImage)) // Check if there's an existing image path
                            {
                                var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", ReadyProductsID, foodready.ReadyProductsImage);

                                if (System.IO.File.Exists(OldImagePath1))
                                {
                                    System.IO.File.Delete(OldImagePath1); // Delete old image if it exists
                                }
                            }

                            string fileNamefood1 = Guid.NewGuid().ToString() + Path.GetExtension(file1Forfoods.FileName);

                            //اذا المسار مش موجود سو مسار جديد 
                            if (!Directory.Exists(FoodPath1))
                            {
                                Directory.CreateDirectory(FoodPath1);
                            }

                            using (var fileStream1 = new FileStream(Path.Combine(FoodPath1, fileNamefood1), FileMode.Create))
                            {
                                await file1Forfoods.CopyToAsync(fileStream1);
                            }

                            foodready.ReadyProductsImage = fileNamefood1; // Update the image path
                        }

                        var existingFoodٌReady = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == foodready.ReadyProductsID);

                        if (existingFoodٌReady != null)
                        {
                            existingFoodٌReady.ReadyProductsName = foodready.ReadyProductsName;
                            existingFoodٌReady.ReadyProductsImage = foodready.ReadyProductsImage;
                            _unitOfWork.readyFoodRepository.Update(existingFoodٌReady);
                        }
                        else
                        {
                            _unitOfWork.readyFoodRepository.Add(foodready);
                        }
                        _unitOfWork.Save();
                    }
                }
            }
            TempData["success"] = "تم تحديث المنتجات الجاهزة بشكل ناجح";

            return RedirectToAction("RedirectToFinishProductionList", new { brandFK = foodReadyViewModel.ReadyFoodLoginVM.BrandFK });
        }
        //============================================================================

        //-------------------------------صفحة الإضافة----------------------------------
        public IActionResult createFoodfonsh(int? id)
        {
            LoginTredMarktViewModel FooReadyVM = new()
            {
                ReadyFoodLoginVM = new ReadyProducts(),
                ReadyFoodLoginVMlist = new List<ReadyProducts>(),
                tredMaeketReadyfoodVM = new Brands(),

            };

            FooReadyVM.tredMaeketReadyfoodVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            FooReadyVM.FoodReadyVMorder = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == id);
            FooReadyVM.ReadyFoodLoginVM = new ReadyProducts();
            FooReadyVM.ReadyFoodLoginVMlist = new List<ReadyProducts>();

            return View(FooReadyVM);
        }
        [HttpPost]
        public async Task<IActionResult> createFoodfonsh(LoginTredMarktViewModel FoodsReadyVM, int selectFoodReadyValue)
        {
            if (ModelState.IsValid)
            {
                int foodFK = FoodsReadyVM.tredMaeketReadyfoodVM.BrandID;
                    for (int i = 0; i < FoodsReadyVM.ReadyFoodLoginVMlist.Count; i++)
                    {
                        var foodready = FoodsReadyVM.ReadyFoodLoginVMlist[i];
                        //int LastId = _unitOfWork.readyFoodRepository.GetLastStepId();
                        //int LastId1 = LastId + 1;

                        var newfoods = new ReadyProducts
                        {
                            ReadyProductsID = foodready.ReadyProductsID,
                            BrandFK = foodFK,
                            ReadyProductsName = foodready.ReadyProductsName,

                        };

                        string wwwRootFoodPath = _webHostEnvironment.WebRootPath; // get us root folder
                        var file1Name1 = $"file1_{newfoods.ReadyProductsID}";
                        var file1ForFood1 = HttpContext.Request.Form.Files[file1Name1];


                        string BrandFK = newfoods.BrandFK.ToString();
                        string ReadyProductsID = newfoods.ReadyProductsID.ToString();
                        var FoodPath1 = Path.Combine(wwwRootFoodPath, "IMAGES", ReadyProductsID);

                        if (file1ForFood1 != null && file1ForFood1.Length > 0)
                        {
                            string fileName11 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForFood1.FileName);

                            if (!Directory.Exists(FoodPath1))
                            {
                                Directory.CreateDirectory(FoodPath1);
                            }

                            using (var fileStream = new FileStream(Path.Combine(FoodPath1, fileName11), FileMode.Create)) //save images
                            {
                                await file1ForFood1.CopyToAsync(fileStream);
                            }
                            newfoods.ReadyProductsImage = fileName11;
                        }
                    //// reOrder2 
                    if (selectFoodReadyValue == 0)
                    {
                        int IDfinish = newfoods.ReadyProductsID;
                        newfoods.ReadyProductsOrder = IDfinish;
                        //// Get the maximum order value in the existing list
                        //double maxOrder = _unitOfWork.readyFoodRepository.GetAll()
                        //    .Max(item => item.ReadyProductsOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                        //// Round down the maxOrder value to the nearest integer
                        //int maxOrderAsInt = (int)Math.Floor(maxOrder);

                        //// Set the new order value for the "اخرى" (Other) item
                        //double newOrder = maxOrderAsInt + 1.0f;
                        //newfoods.ReadyProductsOrder = newOrder;
                    }
                    else
                    {
                        var getIdOrder = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == selectFoodReadyValue);
                        int OldOrder = getIdOrder.ReadyProductsID; // Default to 0.0f if Order is null
                        double newOrder = OldOrder + 0.1;
                        newfoods.ReadyProductsOrder = newOrder;
                    }

                    var existingFoodReady = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == foodready.ReadyProductsID);

                        if (existingFoodReady != null)
                        {
                            existingFoodReady.ReadyProductsName = foodready.ReadyProductsName;
                        existingFoodReady.ReadyProductsOrder = newfoods.ReadyProductsOrder;
                            existingFoodReady.ReadyProductsImage = newfoods.ReadyProductsImage ?? existingFoodReady.ReadyProductsImage;
                        _unitOfWork.readyFoodRepository.Update(existingFoodReady);
                        }
                        else
                        {
                            _unitOfWork.readyFoodRepository.Add(foodready);
                        }
                        _unitOfWork.Save();

                        List<ReadyProducts> obdeviceToolsList = _unitOfWork.readyFoodRepository.GetAll().OrderBy(item => item.ReadyProductsOrder).ToList();
                        _unitOfWork.Save();
                    }
                    TempData["success"] = "تم إضافة المنتجات الجاهزة بشكل ناجح";
                    return RedirectToAction("RedirectToFinishProductionList", new { brandFK = FoodsReadyVM.tredMaeketReadyfoodVM.BrandID });
            }
            return View(FoodsReadyVM);
        }
        //============================================================================


        //زر الحذف في صفحة قائمة  المنجات الجاهزة 
        #region
        //[HttpDelete]
        public IActionResult DeleteFinshFood(int? id)
        {
            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            var deleteFinshFoodPicture = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == id);

            string ReadyProductsID = deleteFinshFoodPicture.ReadyProductsID.ToString();
            string BrandFK = deleteFinshFoodPicture.BrandFK.ToString();

            // Delete the associated image file
            if (!string.IsNullOrEmpty(deleteFinshFoodPicture.ReadyProductsImage))
            {
                string imagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", ReadyProductsID, deleteFinshFoodPicture.ReadyProductsImage);
                if (System.IO.File.Exists(imagePath1))
                {
                    System.IO.File.Delete(imagePath1);
                }
            }

            _unitOfWork.readyFoodRepository.Remove(deleteFinshFoodPicture);
            _unitOfWork.Save();

            return Json(new { success = true, redirectToUrl = Url.Action("RedirectToFinishProductionList", new { brandFK = BrandFK }) });
        }
        #endregion
        //============================================================================

        //-------------------GET LAST ID----------------------------------------------
        [HttpGet]
        public IActionResult GetLastId()
        {
            try
            {
                int lastId = _unitOfWork.readyFoodRepository.GetLastStepId();
                return Ok(lastId);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return StatusCode(500, ex.Message);
            }
        }
        //============================================================================
        //=========================================POST Add ID ===================================
        [HttpPost]
        public IActionResult GetAddID(int BrandFK, LoginTredMarktViewModel FoodsReadyVM)
        {
            // Fetch the production and steps associated with the given ProductionFK
            FoodsReadyVM.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == BrandFK);
            FoodsReadyVM.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll(incloudeProperties: "Brand").Where(u => u.BrandFK == BrandFK).ToList();
            // Create a new step
            var newDevice = new ReadyProducts
            {
                BrandFK = BrandFK,
            };

            // Save the new step to the database
            _unitOfWork.readyFoodRepository.Add(newDevice);
            _unitOfWork.Save();

            // Return the new step's ID
            return Json(newDevice.ReadyProductsID);
        }
        //============================================================================

    }
}



