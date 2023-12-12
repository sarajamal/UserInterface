using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Test12.DataAccess.Repository;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Food;
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

        public IActionResult finishProductionList(int? id) //this for display List Of التحضيرات Page1
        {

            IEnumerable<ReadyProducts> objFoodfinishList = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == id).OrderBy(item => item.ReadyProductsOrder).ToList();

            // Store the FK value in TempData
            TempData["ID"] = id;
            // Display the updated list
            return View(objFoodfinishList);
        }

        public IActionResult FinishProductsIndex(int? id)
        {
            ReadyFoodViewmodel RDVM = new()
            {
                ReadyfoodVM = new ReadyProducts(),
                readyfoodlistVM = new List<ReadyProducts>(),
                tredMaeketReadyfoodVM = new Brands(),

            };
           
                RDVM.tredMaeketReadyfoodVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
                RDVM.ReadyfoodVM = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == id);
                RDVM.readyfoodlistVM = _unitOfWork.readyFoodRepository.GetAll(incloudeProperties: "Brand").Where(u => u.ReadyProductsID == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

                return View(RDVM);
        }

        public IActionResult createFoodfonsh(int? id)
        {
            ReadyFoodViewmodel FooReadyVM = new()
            {
                ReadyfoodVM = new ReadyProducts(),
                readyfoodlistVM = new List<ReadyProducts>(),
                tredMaeketReadyfoodVM = new Brands(),

            };

            FooReadyVM.tredMaeketReadyfoodVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            FooReadyVM.FoodReadyVMorder = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == id);
            FooReadyVM.ReadyfoodVM = new ReadyProducts();
            FooReadyVM.readyfoodlistVM = new List<ReadyProducts>();

            return View(FooReadyVM);
        }

        [HttpPost]
        public IActionResult createFoodfonsh(ReadyFoodViewmodel FoodsReadyVM, int selectFoodReadyValue)
        {

            if (ModelState.IsValid)
            {
                int foodFK = FoodsReadyVM.tredMaeketReadyfoodVM.BrandID;
                if (FoodsReadyVM.ReadyfoodVM.ReadyProductsID == 0)
                {

                    foreach (var ReadyfoodAdd in FoodsReadyVM.readyfoodlistVM)
                    {

                        if (ReadyfoodAdd != null && ReadyfoodAdd.ReadyProductsID == 0)
                        {

                            var newfoods = new ReadyProducts
                            {
                                BrandFK = foodFK,
                                ReadyProductsName = ReadyfoodAdd.ReadyProductsName,
                                
                            };
                            _unitOfWork.readyFoodRepository.Add(newfoods);
                            _unitOfWork.Save();

                            string wwwRootFoodPath = _webHostEnvironment.WebRootPath; // get us root folder


                            var file1Name1 = $"file1_{newfoods.ReadyProductsName}";
                            var file1ForFood1 = HttpContext.Request.Form.Files[file1Name1];

                            string اسم_المنتج = newfoods.ReadyProductsName != null ? newfoods.ReadyProductsName.ToString() : string.Empty;
                           
                            string id = newfoods.BrandFK.ToString();
                            string ID3 = newfoods.ReadyProductsID.ToString();


                            var FoodPath1 = Path.Combine(wwwRootFoodPath, "IMAGES", "منتجات_جاهزة", اسم_المنتج ?? "", ID3, id);
 
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
                                newfoods.ReadyProductsImage = fileName11;
                            }
                            _unitOfWork.Save();
                            //// reOrder2 
                            if (selectFoodReadyValue == 0)
                            {
                                // Get the maximum order value in the existing list
                                double maxOrder = _unitOfWork.readyFoodRepository.GetAll()
                                    .Max(item => item.ReadyProductsOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                                // Round down the maxOrder value to the nearest integer
                                int maxOrderAsInt = (int)Math.Floor(maxOrder);

                                // Set the new order value for the "اخرى" (Other) item
                                double newOrder = maxOrderAsInt + 1.0f;
                                newfoods.ReadyProductsOrder = newOrder;
                            }
                            else
                            {
                                var getIdOrder = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == selectFoodReadyValue);
                                double OldOrder = getIdOrder.ReadyProductsOrder ?? 0.0f; // Default to 0.0f if Order is null
                                double newOrder = OldOrder + 0.1f;
                                newfoods.ReadyProductsOrder = newOrder;
                            }

                            List<ReadyProducts> obdeviceToolsList = _unitOfWork.readyFoodRepository.GetAll().OrderBy(item => item.ReadyProductsOrder).ToList();
                            _unitOfWork.Save();
                        }
                    }

                }
            }

            TempData["success"] = "تم إضافة المنتجات الجاهزة بشكل ناجح";
            return RedirectToAction("finishProductionList", new { id = FoodsReadyVM.tredMaeketReadyfoodVM.BrandID });
        }


        [HttpPost]
        public IActionResult FinishProductsIndex(ReadyFoodViewmodel foodReadyViewModel)
        {

            if (ModelState.IsValid)
            {
                if (foodReadyViewModel.readyfoodlistVM != null)
                {
                    for (int i = 0; i < foodReadyViewModel.readyfoodlistVM.Count; i++)
                    {
                        var foodready = foodReadyViewModel.readyfoodlistVM[i];
                        string اسم_المنتج = foodready.ReadyProductsName != null ? foodready.ReadyProductsName.ToString() : string.Empty;
                       
                        string ID3 = foodready.ReadyProductsID.ToString();
                        string id = foodready.BrandFK.ToString();

                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder
                        var FoodPath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "منتجات_جاهزة", اسم_المنتج ?? "", ID3, id);

                        var file1Name = $"file1_{foodready.ReadyProductsName}";
                        var file1Forfoods = HttpContext.Request.Form.Files[file1Name];

                        if (file1Forfoods != null)
                        {
                            if (!string.IsNullOrEmpty(foodready.ReadyProductsImage)) // Check if there's an existing image path
                            {
                                var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "منتجات_جاهزة", اسم_المنتج ?? "", ID3, id, foodready.ReadyProductsImage);

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
                                file1Forfoods.CopyTo(fileStream1);
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

            return RedirectToAction("finishProductionList", new { id = foodReadyViewModel.ReadyfoodVM.BrandFK });
        }



        //زر الحذف في صفحة قائمة  المنجات الجاهزة 
        #region
        [HttpDelete]
        public IActionResult DeleteFinshFood(int? id)
        {
            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            var deleteFinshFoodPicture = _unitOfWork.readyFoodRepository.Get(u => u.ReadyProductsID == id);
            string اسم_المنتج = deleteFinshFoodPicture.ReadyProductsName != null ? deleteFinshFoodPicture.ReadyProductsName.ToString() : string.Empty;
            string Id1 = deleteFinshFoodPicture.ReadyProductsID.ToString();
            string ID = deleteFinshFoodPicture.BrandFK.ToString();

            // Delete the associated image file
            if (!string.IsNullOrEmpty(deleteFinshFoodPicture.ReadyProductsImage))
            {
                string imagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "منتجات_جاهزة", اسم_المنتج, Id1, ID, deleteFinshFoodPicture.ReadyProductsImage );
                if (System.IO.File.Exists(imagePath1))
                {
                    System.IO.File.Delete(imagePath1);
                }
            }
           
            _unitOfWork.readyFoodRepository.Remove(deleteFinshFoodPicture);
            _unitOfWork.Save();
          
            return Json(new { success = true });
        }
        #endregion


        // تبع List 
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            IEnumerable<ReadyProducts> objfoodList = _unitOfWork.readyFoodRepository.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.ReadyProductsOrder).ToList();

            return Json(new { data = objfoodList });
        }
        #endregion
    }
}


     
