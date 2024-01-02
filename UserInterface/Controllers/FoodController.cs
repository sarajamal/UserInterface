using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Device_Tools;
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
        public IActionResult FoodList(int? id) //this for display List Of التحضيرات Page1
        {
            FoodVM FDVM = new()
            {
               
                FoodViewMList = _unitOfWork.FoodRepository.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.FoodStuffsOrder).ToList(),
            WelcomTredmarketFood = new LoginTredMarktViewModel()

            };
            FDVM.WelcomTredmarketFood.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            FDVM.WelcomTredmarketFood.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.BrandFK == id);
            FDVM.WelcomTredmarketFood.ProductionLoginVM = _unitOfWork.itemsRepository.Get(u => u.BrandFK == id);
            FDVM.WelcomTredmarketFood.CleanLoginVM = _unitOfWork.CleanRepository.Get(u => u.BrandFK == id);
            FDVM.WelcomTredmarketFood.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == id);
            FDVM.WelcomTredmarketFood.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == id);
            FDVM.WelcomTredmarketFood.PreparatonLoginVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == id);
            FDVM.WelcomTredmarketFood.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            FDVM.WelcomTredmarketFood.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            FDVM.WelcomTredmarketFood.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            FDVM.WelcomTredmarketFood.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            FDVM.WelcomTredmarketFood.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            FDVM.WelcomTredmarketFood.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            FDVM.WelcomTredmarketFood.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == id).ToList();
            // Store the FK value in TempData
            TempData["ID"] = id;
            // Display the updated list
            return View(FDVM);
        }

        public IActionResult FoodIndex(int? id)
        {
            FoodVM FDVM = new()
            {
                FoodViewM = new FoodStuffs(),
                FoodViewMList = new List<FoodStuffs>(),
                tredMaeketFoodsVM = new Brands(),

            };

            FDVM.tredMaeketFoodsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            FDVM.FoodViewM = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == id);
            FDVM.FoodViewMList = _unitOfWork.FoodRepository.GetAll(incloudeProperties: "Brand").Where(u => u.FoodStuffsID == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

            return View(FDVM);
        }

        public IActionResult CreateFood(int? id)
        {
            FoodVM FooVM = new()
            {
                FoodViewM = new FoodStuffs(),
                FoodViewMList = new List<FoodStuffs>(),
                tredMaeketFoodsVM = new Brands(),

            };

            FooVM.tredMaeketFoodsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            FooVM.FoodsVMorder = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == id);
            FooVM.FoodViewM = new FoodStuffs();
            FooVM.FoodViewMList = new List<FoodStuffs>();

            return View(FooVM);
        }


        [HttpPost]
        public IActionResult CreateFood(FoodVM FoodsVM, int selectFoodvalue)
        {

            if (ModelState.IsValid)
            {
                int foodFK = FoodsVM.tredMaeketFoodsVM.BrandID;
                if (FoodsVM.FoodViewM.FoodStuffsID == 0)
                {

                    foreach (var foodAdd in FoodsVM.FoodViewMList)
                    {
                        int lastId = _unitOfWork.FoodRepository.GetLastStepId();
                        int LastId1 = lastId + 1;
                        if (foodAdd != null && foodAdd.FoodStuffsID == 0)
                        {

                            var newfoods = new FoodStuffs
                            {
                                FoodStuffsID = LastId1, 
                                BrandFK = foodFK,
                                FoodStuffsName = foodAdd.FoodStuffsName,

                            };

                            string wwwRootFoodPath = _webHostEnvironment.WebRootPath; // get us root folder


                            var file1Name1 = $"file1_{newfoods.FoodStuffsID}";
                            var file1ForFood1 = HttpContext.Request.Form.Files[file1Name1];


                            string FoodStuffsID = newfoods.FoodStuffsID.ToString();
                            string BrandFK = newfoods.BrandFK.ToString();


                            var FoodPath1 = Path.Combine(wwwRootFoodPath, "IMAGES", BrandFK, "FoodStuffs", FoodStuffsID);


                            if (file1ForFood1 != null && file1ForFood1.Length > 0)
                            {
                                string fileName11 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForFood1.FileName);

                                if (!Directory.Exists(FoodPath1))
                                {
                                    Directory.CreateDirectory(FoodPath1);
                                }

                                using (var fileStream = new FileStream(Path.Combine(FoodPath1, fileName11), FileMode.Create)) //save images
                                {
                                    file1ForFood1.CopyTo(fileStream);
                                }
                                newfoods.FoodStuffsImage = fileName11;
                            }
                            _unitOfWork.FoodRepository.Add(newfoods);
                            _unitOfWork.Save();
                            //// reOrder2 
                            if (selectFoodvalue == 0)
                            {
                                // Get the maximum order value in the existing list
                                double maxOrder = _unitOfWork.FoodRepository.GetAll()
                                    .Max(item => item.FoodStuffsOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                                // Round down the maxOrder value to the nearest integer
                                int maxOrderAsInt = (int)Math.Floor(maxOrder);

                                // Set the new order value for the "اخرى" (Other) item
                                double newOrder = maxOrderAsInt + 1.0f;
                                newfoods.FoodStuffsOrder = newOrder;
                            }
                            else
                            {
                                var getIdOrder = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == selectFoodvalue);
                                double OldOrder = getIdOrder.FoodStuffsOrder ?? 0.0f; // Default to 0.0f if Order is null
                                double newOrder = OldOrder + 0.1f;
                                newfoods.FoodStuffsOrder = newOrder;
                            }

                            List<FoodStuffs> obdeviceToolsList = _unitOfWork.FoodRepository.GetAll().OrderBy(item => item.FoodStuffsOrder).ToList();
                            _unitOfWork.Save();
                        }
                    }

                }
            }

            TempData["success"] = "تم إضافة المواد الغذائية بشكل ناجح";
            return RedirectToAction("FoodList", new { id = FoodsVM.tredMaeketFoodsVM.BrandID });
        }



        [HttpPost]
        public IActionResult FoodIndex(FoodVM foodViewModel)
        {

            if (ModelState.IsValid)
            {
                if (foodViewModel.FoodViewMList != null)
                {
                    for (int i = 0; i < foodViewModel.FoodViewMList.Count; i++)
                    {
                        var foods = foodViewModel.FoodViewMList[i];

                        string FoodStuffsID = foods.FoodStuffsID.ToString();
                        string BrandFK = foods.BrandFK.ToString();

                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder
                        var FoodPath = Path.Combine(wwwRootPathSteps, "IMAGES", BrandFK, "FoodStuffs", FoodStuffsID);


                        var file1Name = $"file1_{foods.FoodStuffsID}";
                        var file1Forfoods = HttpContext.Request.Form.Files[file1Name];

                        if (file1Forfoods != null)
                        {
                            if (!string.IsNullOrEmpty(foods.FoodStuffsImage)) // Check if there's an existing image path
                            {
                                var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", BrandFK, "FoodStuffs", FoodStuffsID, foods.FoodStuffsImage);

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
                                file1Forfoods.CopyTo(fileStream1);
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

            return RedirectToAction("FoodList", new { id = foodViewModel.FoodViewM.BrandFK });
        }



        //زر الحذف في صفحة قائمة  المواد الغذائية 
        #region
        [HttpDelete]
        public IActionResult DelteFooodSave(int? id)
        {
            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            var deleteFoodPicture = _unitOfWork.FoodRepository.Get(u => u.FoodStuffsID == id);

            string FoodStuffsID = deleteFoodPicture.FoodStuffsID.ToString();
            string BrandFK = deleteFoodPicture.BrandFK.ToString();

            // Delete the associated image file
            if (!string.IsNullOrEmpty(deleteFoodPicture.FoodStuffsImage))
            {
                string imagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", BrandFK, "FoodStuffs", FoodStuffsID, deleteFoodPicture.FoodStuffsImage);
                if (System.IO.File.Exists(imagePath1))
                {
                    System.IO.File.Delete(imagePath1);
                }
            }

            _unitOfWork.FoodRepository.Remove(deleteFoodPicture);
            _unitOfWork.Save();

            return Json(new { success = true });
        }
        #endregion


        // تبع List 
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            IEnumerable<FoodStuffs> objfoodList = _unitOfWork.FoodRepository.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.FoodStuffsOrder).ToList();

            return Json(new { data = objfoodList });
        }
        #endregion

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

    }
}



