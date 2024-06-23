using Microsoft.AspNetCore.Mvc;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Food;
using Test12.Models.Models.trade_mark;
using Test12.Models.ViewModel;


namespace Test12.Controllers
{
    public class FoodController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FoodController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;

        }

        //=========================GET LIST ===================================================
        public IActionResult RedirectToFoodList(int brandFK)
        {
            TempData["BrandFK"] = brandFK;
            TempData.Keep("BrandFK");
            return RedirectToAction("FoodList");
        }
        public IActionResult FoodList() //this for display List Of التحضيرات Page1
        {
            int? brandFK = TempData["BrandFK"] as int?;
            TempData.Keep("BrandFK"); // Keep the TempData for further use

            LoginTredMarktViewModel FDVM = new()
            {

                FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll()
                .Where(u => u.BrandFK == brandFK).OrderBy(item => item.FoodStuffsOrder).ToList(),
                WelcomTredmarketFood = new LoginTredMarktViewModel()

            };
            FDVM.WelcomTredmarketFood.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK);
            FDVM.WelcomTredmarketFood.DeviceToolsLoginVM = _unitOfWork.DevicesAndTools.Get(u => u.BrandFK == brandFK);
            FDVM.WelcomTredmarketFood.Productionvm = _unitOfWork.itemsRepository.Get(u => u.BrandFK == brandFK);
            FDVM.WelcomTredmarketFood.CleanViewModel = _unitOfWork.CleanRepository.Get(u => u.BrandFK == brandFK);
            FDVM.WelcomTredmarketFood.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == brandFK);
            FDVM.WelcomTredmarketFood.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == brandFK);
            FDVM.WelcomTredmarketFood.PreparationVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == brandFK);
            FDVM.WelcomTredmarketFood.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FDVM.WelcomTredmarketFood.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FDVM.WelcomTredmarketFood.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FDVM.WelcomTredmarketFood.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FDVM.WelcomTredmarketFood.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FDVM.WelcomTredmarketFood.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            FDVM.WelcomTredmarketFood.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == brandFK).ToList();
            // Store the FK value in TempData
            TempData["ID"] = brandFK;
            // Display the updated list
            return View(FDVM);
        }
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            IEnumerable<FoodStuffs> objfoodList = _unitOfWork.FoodRepository.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.FoodStuffsOrder).ToList();

            return Json(new { data = objfoodList });
        }
        #endregion
        //============================================================================

        //---------------------------------صفحة التعديل -----------------------------
        public IActionResult FoodIndex(int? id)
        {
            TempData.Keep("BrandFK"); // Keep the TempData for further use

            LoginTredMarktViewModel FDVM = new()
            {
                FoodLoginVM = new FoodStuffs(),
                FoodLoginVMlist = new List<FoodStuffs>(),
                tredMaeketFoodsVM = new Brands(),

            };

            FDVM.tredMaeketFoodsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            FDVM.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == id);
            FDVM.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll(incloudeProperties: "Brand").Where(u => u.FoodStuffsID == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

            return View(FDVM);
        }
        [HttpPost]
        public async Task<IActionResult> FoodIndex(LoginTredMarktViewModel foodViewModel)
        {
            if (ModelState.IsValid)
            {
                if (foodViewModel.FoodLoginVMlist != null)
                {
                    for (int i = 0; i < foodViewModel.FoodLoginVMlist.Count; i++)
                    {
                        var foods = foodViewModel.FoodLoginVMlist[i];

                        string FoodStuffsID = foods.FoodStuffsID.ToString();
                        string BrandFK = foods.BrandFK.ToString();

                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder
                        var FoodPath = Path.Combine(wwwRootPathSteps, "IMAGES", FoodStuffsID);


                        var file1Name = $"file1_{foods.FoodStuffsID}";
                        var file1Forfoods = HttpContext.Request.Form.Files[file1Name];

                        if (file1Forfoods != null)
                        {
                            if (!string.IsNullOrEmpty(foods.FoodStuffsImage)) // Check if there's an existing image path
                            {
                                var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", FoodStuffsID, foods.FoodStuffsImage);

                                if (System.IO.File.Exists(OldImagePath1))
                                {
                                    System.IO.File.Delete(OldImagePath1); // Delete old image if it exists
                                }
                            }
                            string fileNamefood1 = Guid.NewGuid().ToString() + Path.GetExtension(file1Forfoods.FileName);

                            //اذا المسار مش موجود سو مسار جديد 
                            if (!Directory.Exists(FoodPath))
                            {
                                Directory.CreateDirectory(FoodPath);
                            }

                            using (var fileStream1 = new FileStream(Path.Combine(FoodPath, fileNamefood1), FileMode.Create))
                            {
                                await file1Forfoods.CopyToAsync(fileStream1);
                            }
                            foods.FoodStuffsImage = fileNamefood1; // Update the image path
                        }
                        var existingFoods = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == foods.FoodStuffsID);
                        if (existingFoods != null)
                        {

                            existingFoods.FoodStuffsName = foods.FoodStuffsName;
                            existingFoods.FoodStuffsImage = foods.FoodStuffsImage;

                            _unitOfWork.FoodRepository.Update(existingFoods);
                        }
                        else
                        {
                            _unitOfWork.FoodRepository.Add(foods);
                        }
                        _unitOfWork.Save();
                    }
                }
            }
            TempData["success"] = "تم تحديث المواد الغذائية بشكل ناجح";
            return RedirectToAction("RedirectToFoodList", new { brandFK = foodViewModel.FoodLoginVM.BrandFK });
        }
        //============================================================================

        //------------------------------صفحة الإضافة-----------------------------------
        public IActionResult CreateFood(int? id)
        {
            LoginTredMarktViewModel FooVM = new()
            {
                FoodLoginVM = new FoodStuffs(),
                FoodLoginVMlist = new List<FoodStuffs>(),
                FoodsLoginVMorder = new List<FoodStuffs>(),
                tredMaeketFoodsVM = new Brands(),

            };

            FooVM.tredMaeketFoodsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            FooVM.FoodsLoginVMorder = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == id);
            FooVM.FoodLoginVM = new FoodStuffs();
            FooVM.FoodLoginVMlist = new List<FoodStuffs>();

            return View(FooVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFood(LoginTredMarktViewModel FoodsVM, int selectFoodvalue)
        {
            if (ModelState.IsValid)
            {
                int foodFK = FoodsVM.tredMaeketFoodsVM.BrandID;

                for (int i = 0; i < FoodsVM.FoodLoginVMlist.Count; i++)
                {
                    var foods = FoodsVM.FoodLoginVMlist[i];

                    var newfoods = new FoodStuffs
                    {
                        FoodStuffsID = foods.FoodStuffsID,
                        BrandFK = foodFK,
                        FoodStuffsName = foods.FoodStuffsName,
                    };

                    string wwwRootFoodPath = _webHostEnvironment.WebRootPath; // get root folder
                    var file1Name1 = $"file1_{newfoods.FoodStuffsID}";
                    var file1ForFood1 = HttpContext.Request.Form.Files[file1Name1];

                    string FoodStuffsID = newfoods.FoodStuffsID.ToString();
                    string BrandFK = newfoods.BrandFK.ToString();
                    var FoodPath1 = Path.Combine(wwwRootFoodPath, "IMAGES", FoodStuffsID);

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
                        newfoods.FoodStuffsImage = fileName11;
                    }
                    //// reOrder2 
                    if (selectFoodvalue == 0)
                    {
                        int IDfoods = newfoods.FoodStuffsID;
                        newfoods.FoodStuffsOrder = IDfoods;
                        //// Get the maximum order value in the existing list
                        //double maxOrder = _unitOfWork.FoodRepository.GetAll()
                        //    .Max(item => item.FoodStuffsOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                        //// Round down the maxOrder value to the nearest integer
                        //int maxOrderAsInt = (int)Math.Floor(maxOrder);

                        //// Set the new order value for the "اخرى" (Other) item
                        //double newOrder = maxOrderAsInt + 1.0f;
                        //newfoods.FoodStuffsOrder = newOrder;
                    }
                    else
                    {
                        var getIdOrder = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == selectFoodvalue);
                        int OldOrder = getIdOrder.FoodStuffsID; // Default to 0.0f if Order is null
                        double newOrder = OldOrder + 0.1;
                        newfoods.FoodStuffsOrder = newOrder;
                    }

                    var existingFoods = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == foods.FoodStuffsID);
                    if (existingFoods != null)
                    {
                        existingFoods.FoodStuffsName = foods.FoodStuffsName;
                        existingFoods.FoodStuffsOrder = newfoods.FoodStuffsOrder;
                        existingFoods.FoodStuffsImage = newfoods.FoodStuffsImage ?? existingFoods.FoodStuffsImage;

                        _unitOfWork.FoodRepository.Update(existingFoods);
                    }
                    else
                    {
                        _unitOfWork.FoodRepository.Add(newfoods);
                    }

                    _unitOfWork.Save();
                    
                    List<FoodStuffs> obdeviceToolsList = _unitOfWork.FoodRepository.GetAll().OrderBy(item => item.FoodStuffsOrder).ToList();
                    _unitOfWork.Save();
                }

                TempData["success"] = "تم إضافة المواد الغذائية بشكل ناجح";
                return RedirectToAction("RedirectToFoodList", new { brandFK = FoodsVM.tredMaeketFoodsVM.BrandID });
            }

            return View(FoodsVM);
        }
        //============================================================================

        //زر الحذف في صفحة قائمة  المواد الغذائية 
        #region
        //[HttpDelete]
        public IActionResult DelteFooodSave(int? id)
        {
            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            var deleteFoodPicture = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == id);

            string FoodStuffsID = deleteFoodPicture.FoodStuffsID.ToString();
            string BrandFK = deleteFoodPicture.BrandFK.ToString();

            // Delete the associated image file
            if (!string.IsNullOrEmpty(deleteFoodPicture.FoodStuffsImage))
            {
                string imagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", FoodStuffsID, deleteFoodPicture.FoodStuffsImage);
                if (System.IO.File.Exists(imagePath1))
                {
                    System.IO.File.Delete(imagePath1);
                }
            }

            _unitOfWork.FoodRepository.Remove(deleteFoodPicture);
            _unitOfWork.Save();

            return Json(new { success = true, redirectToUrl = Url.Action("RedirectToFoodList", new { brandFK = BrandFK }) });
        }
        #endregion

        //-------------------GET LAST ID----------------------------------------------
        [HttpGet]
        public IActionResult GetLastId()
        {
            try
            {
                int lastId = _unitOfWork.FoodRepository.GetLastStepId();
                return Ok(lastId);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return StatusCode(500, ex.Message);
            }
        }
        //=========================================POST Add ID ===================================
        [HttpPost]
        public IActionResult GetAddID(int BrandFK, LoginTredMarktViewModel FoodsVM)
        {
            // Fetch the production and steps associated with the given ProductionFK
            FoodsVM.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == BrandFK);
            FoodsVM.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll(incloudeProperties: "Brand").Where(u => u.BrandFK == BrandFK).ToList();

            // Create a new step
            var newDevice = new FoodStuffs
            {
                BrandFK = BrandFK,
            };

            // Save the new step to the database
            _unitOfWork.FoodRepository.Add(newDevice);
            _unitOfWork.Save();

            // Return the new step's ID
            return Json(newDevice.FoodStuffsID);
        }
        //============================================================================

    }
}



