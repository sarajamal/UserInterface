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

            IEnumerable<FoodStuffs> objFoodList = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == id).OrderBy(item => item.FoodStuffsOrder).ToList();

            // Store the FK value in TempData
            TempData["ID"] = id;
            // Display the updated list
            return View(objFoodList);
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
                //if (FoodsVM.FoodViewM.ID2 == 0)
                //{

                //    foreach (var foodAdd in FoodsVM.FoodViewMList)
                //    {

                //        if (foodAdd != null && foodAdd.ID2 == 0)
                //        {

                //            var newfoods = new FoodStuffs
                //            {
                //                ID = foodFK,
                //                اسم_المادة_الغذئية1 = foodAdd.اسم_المادة_الغذئية1,
                //                اسم_المادة_الغذئية2 = foodAdd.اسم_المادة_الغذئية2,
                //                اسم_المادة_الغذئية3 = foodAdd.اسم_المادة_الغذئية3,
                //                اسم_المادة_الغذئية4 = foodAdd.اسم_المادة_الغذئية4,

                //            };
                //            _unitOfWork.FoodRepository.Add(newfoods);
                //            _unitOfWork.Save();

                //            string wwwRootFoodPath = _webHostEnvironment.WebRootPath; // get us root folder


                //            var file1Name1 = $"file1_{newfoods.اسم_المادة_الغذئية1}";
                //            var file1ForFood1 = HttpContext.Request.Form.Files[file1Name1];

                //            string اسم_المادة_الغذئية1 = newfoods.اسم_المادة_الغذئية1 != null ? newfoods.اسم_المادة_الغذئية1.ToString() : string.Empty;
                //            string اسم_المادة_الغذئية2 = newfoods.اسم_المادة_الغذئية2 != null ? newfoods.اسم_المادة_الغذئية2.ToString() : string.Empty;
                //            string اسم_المادة_الغذئية3 = newfoods.اسم_المادة_الغذئية3 != null ? newfoods.اسم_المادة_الغذئية3.ToString() : string.Empty;
                //            string اسم_المادة_الغذئية4 = newfoods.اسم_المادة_الغذئية4 != null ? newfoods.اسم_المادة_الغذئية4.ToString() : string.Empty;
                //            string id = newfoods.ID.ToString();
                //            string ID2 = newfoods.ID2.ToString();


                //            var FoodPath1 = Path.Combine(wwwRootFoodPath, "IMAGES", "مواد", اسم_المادة_الغذئية1 ?? "", ID2, id);
                //            var FoodPath2 = Path.Combine(wwwRootFoodPath, "IMAGES", "مواد", اسم_المادة_الغذئية2 ?? "", ID2, id);
                //            var FoodPath3 = Path.Combine(wwwRootFoodPath, "IMAGES", "مواد", اسم_المادة_الغذئية3 ?? "", ID2, id);
                //            var FoodPath4 = Path.Combine(wwwRootFoodPath, "IMAGES", "مواد", اسم_المادة_الغذئية4 ?? "", ID2, id);

                //            if (file1ForFood1 != null && file1ForFood1.Length > 0)
                //            {
                //                string fileName11 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForFood1.FileName);

                //                if (!Directory.Exists(FoodPath1))
                //                {
                //                    Directory.CreateDirectory(FoodPath1);
                //                }

                //                using (var fileStream = new FileStream(Path.Combine(FoodPath1, fileName11), FileMode.Create)) //save images
                //                {
                //                    file1ForFood1.CopyTo(fileStream);
                //                }
                //                newfoods.صورة1 = fileName11;
                //            }

                //            var fileName2 = $"file2_{newfoods.اسم_المادة_الغذئية2}";
                //            var filefood2 = HttpContext.Request.Form.Files[fileName2];

                //            if (filefood2 != null && filefood2.Length > 0)
                //            {
                //                string fileName22 = Guid.NewGuid().ToString() + Path.GetExtension(filefood2.FileName);

                //                if (!Directory.Exists(FoodPath2))
                //                {
                //                    Directory.CreateDirectory(FoodPath2);
                //                }
                //                using (var filStream = new FileStream(Path.Combine(FoodPath2, fileName22), FileMode.Create)) //save images
                //                {
                //                    filefood2.CopyTo(filStream);
                //                }
                //                newfoods.صورة2 = fileName22;
                //            }
                //            var fileName3 = $"file3_{newfoods.اسم_المادة_الغذئية3}";
                //            var filefood3 = HttpContext.Request.Form.Files[fileName3];

                //            if (filefood3 != null && filefood3.Length > 0)
                //            {
                //                string fileName33 = Guid.NewGuid().ToString() + Path.GetExtension(filefood3.FileName);

                //                if (!Directory.Exists(FoodPath3))
                //                {
                //                    Directory.CreateDirectory(FoodPath3);
                //                }
                //                using (var filStream = new FileStream(Path.Combine(FoodPath3, fileName33), FileMode.Create)) //save images
                //                {
                //                    filefood3.CopyTo(filStream);
                //                }
                //                newfoods.صورة3 = fileName33;
                //            }

                //            var fileName4 = $"file4_{newfoods.اسم_المادة_الغذئية4}";
                //            var filefood4 = HttpContext.Request.Form.Files[fileName4];

                //            if (filefood4 != null && filefood4.Length > 0)
                //            {
                //                string fileName44 = Guid.NewGuid().ToString() + Path.GetExtension(filefood4.FileName);

                //                if (!Directory.Exists(FoodPath4))
                //                {
                //                    Directory.CreateDirectory(FoodPath4);
                //                }
                //                using (var filStream = new FileStream(Path.Combine(FoodPath4, fileName44), FileMode.Create)) //save images
                //                {
                //                    filefood4.CopyTo(filStream);
                //                }
                //                newfoods.صورة4 = fileName44;
                //            }
                //            _unitOfWork.Save();
                //            //// reOrder2 
                //            if (selectFoodvalue == 0)
                //            {
                //                // Get the maximum order value in the existing list
                //                double maxOrder = _unitOfWork.FoodRepository.GetAll()
                //                    .Max(item => item.Order) ?? 0.0f; // Default to 0.0f if there are no existing items

                //                // Round down the maxOrder value to the nearest integer
                //                int maxOrderAsInt = (int)Math.Floor(maxOrder);

                //                // Set the new order value for the "اخرى" (Other) item
                //                double newOrder = maxOrderAsInt + 1.0f;
                //                newfoods.Order = newOrder;
                //            }
                //            else
                //            {
                //                var getIdOrder = _unitOfWork.FoodRepository.Get(u => u.ID2 == selectFoodvalue);
                //                double OldOrder = getIdOrder.Order ?? 0.0f; // Default to 0.0f if Order is null
                //                double newOrder = OldOrder + 0.1f;
                //                newfoods.Order = newOrder;
                //            }

                //            List<FoodStuffs> obdeviceToolsList = _unitOfWork.FoodRepository.GetAll().OrderBy(item => item.Order).ToList();
                //            _unitOfWork.Save();
                //         }
                //    }

                //}
            }

            TempData["success"] = "تم إضافة المواد الغذائية بشكل ناجح";
            return RedirectToAction("FoodList", new { id = FoodsVM.tredMaeketFoodsVM.BrandID });
        }



        //[HttpPost]
        //public IActionResult FoodIndex(FoodVM foodViewModel)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        if (foodViewModel.FoodViewMList != null)
        //        {
        //            for (int i = 0; i < foodViewModel.FoodViewMList.Count; i++)
        //            {
        //var foods = foodViewModel.FoodViewMList[i];
        //string اسم_المادة_الغذئية1 = foods.اسم_المادة_الغذئية1 != null ? foods.اسم_المادة_الغذئية1.ToString() : string.Empty;
        //string اسم_المادة_الغذئية2 = foods.اسم_المادة_الغذئية2 != null ? foods.اسم_المادة_الغذئية2.ToString() : string.Empty;
        //string اسم_المادة_الغذئية3 = foods.اسم_المادة_الغذئية3 != null ? foods.اسم_المادة_الغذئية3.ToString() : string.Empty;
        //string اسم_المادة_الغذئية4 = foods.اسم_المادة_الغذئية4 != null ? foods.اسم_المادة_الغذئية4.ToString() : string.Empty;
        //string ID2 = foods.ID2.ToString();
        //string id = foods.ID.ToString();

        //string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder
        //var FoodPath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية1 ?? "", ID2, id);
        //var FoodPath2 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية2 ?? "", ID2, id);
        //var FoodPath3 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية3 ?? "", ID2, id);
        //var FoodPath4 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية4 ?? "", ID2, id);

        //var file1Name = $"file1_{foods.اسم_المادة_الغذئية1}";
        //var file1Forfoods = HttpContext.Request.Form.Files[file1Name];

        //if (file1Forfoods != null)
        //{
        //    if (!string.IsNullOrEmpty(foods.صورة1)) // Check if there's an existing image path
        //    {
        //        var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية1 ?? "", ID2, id, foods.صورة1);

        //        if (System.IO.File.Exists(OldImagePath1))
        //        {
        //            System.IO.File.Delete(OldImagePath1); // Delete old image if it exists
        //        }
        //    }

        //    string fileNamefood1 = Guid.NewGuid().ToString() + Path.GetExtension(file1Forfoods.FileName);

        //    //اذا المسار مش موجود سو مسار جديد 
        //    if (!Directory.Exists(FoodPath1))
        //    {
        //        Directory.CreateDirectory(FoodPath1);
        //    }

        //    using (var fileStream1 = new FileStream(Path.Combine(FoodPath1, fileNamefood1), FileMode.Create))
        //    {
        //        file1Forfoods.CopyTo(fileStream1);
        //    }

        //    foods.صورة1 = fileNamefood1; // Update the image path
        //}
        //else
        //{
        //    if (!string.IsNullOrEmpty(foods.صورة1)) // Check if there's an existing image path
        //    {
        //        var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية1 ?? "", ID2, id, foods.صورة1);

        //        if (System.IO.File.Exists(OldImagePath1))
        //        {
        //            string fileNamefood11 = Guid.NewGuid().ToString() + Path.GetExtension(file1Forfoods.FileName);

        //            //اذا المسار مش موجود سو مسار جديد 
        //            if (!Directory.Exists(FoodPath1))
        //            {
        //                Directory.CreateDirectory(FoodPath1);
        //            }

        //            // Copy the existing image to a new path
        //            System.IO.File.Copy(OldImagePath1, Path.Combine(FoodPath1, fileNamefood11), true);

        //            foods.صورة1 = fileNamefood11; // Update the image path
        //        }
        //    }
        //}

        //    var file2Name = $"file2_{foods.اسم_المادة_الغذئية2}";
        //    var file1Forfoods2 = HttpContext.Request.Form.Files[file2Name];

        //    if (file1Forfoods2 != null)
        //    {
        //        if (!string.IsNullOrEmpty(foods.صورة2)) // Check if there's an existing image path
        //        {
        //            var OldImagePath2 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية2 ?? "", ID2, id, foods.صورة2);

        //            if (System.IO.File.Exists(OldImagePath2))
        //            {
        //                System.IO.File.Delete(OldImagePath2); // Delete old image if it exists
        //            }
        //        }

        //        string fileNamefood2 = Guid.NewGuid().ToString() + Path.GetExtension(file1Forfoods2.FileName);

        //        //اذا المسار مش موجود سو مسار جديد 
        //        if (!Directory.Exists(FoodPath2))
        //        {
        //            Directory.CreateDirectory(FoodPath2);
        //        }
        //        using (var fileStream1 = new FileStream(Path.Combine(FoodPath2, fileNamefood2), FileMode.Create))
        //        {
        //            file1Forfoods2.CopyTo(fileStream1);
        //        }

        //        foods.صورة2 = fileNamefood2; // Update the image path
        //    }

        //    var file3Name = $"file3_{foods.اسم_المادة_الغذئية3}";
        //    var file1Forfoods3 = HttpContext.Request.Form.Files[file3Name];

        //    if (file1Forfoods3 != null)
        //    {
        //        if (!string.IsNullOrEmpty(foods.صورة3)) // Check if there's an existing image path
        //        {
        //            var OldImagePath3 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية3 ?? "", ID2, id, foods.صورة3);

        //            if (System.IO.File.Exists(OldImagePath3))
        //            {
        //                System.IO.File.Delete(OldImagePath3); // Delete old image if it exists
        //            }
        //        }

        //        string fileNamefood3 = Guid.NewGuid().ToString() + Path.GetExtension(file1Forfoods3.FileName);

        //        //اذا المسار مش موجود سو مسار جديد 
        //        if (!Directory.Exists(FoodPath3))
        //        {
        //            Directory.CreateDirectory(FoodPath3);
        //        }
        //        using (var fileStream1 = new FileStream(Path.Combine(FoodPath3, fileNamefood3), FileMode.Create))
        //        {
        //            file1Forfoods3.CopyTo(fileStream1);
        //        }

        //        foods.صورة3 = fileNamefood3; // Update the image path
        //    }

        //    var file4Name = $"file4_{foods.اسم_المادة_الغذئية4}";
        //    var file4Forfoods = HttpContext.Request.Form.Files[file4Name];

        //    if (file4Forfoods != null)
        //    {
        //        if (!string.IsNullOrEmpty(foods.صورة4)) // Check if there's an existing image path
        //        {
        //            var OldImagePath4 = Path.Combine(wwwRootPathSteps, "IMAGES", "مواد", اسم_المادة_الغذئية4 ?? "", ID2, id, foods.صورة4);

        //            if (System.IO.File.Exists(OldImagePath4))
        //            {
        //                System.IO.File.Delete(OldImagePath4); // Delete old image if it exists
        //            }
        //        }

        //        string fileNamefood4 = Guid.NewGuid().ToString() + Path.GetExtension(file4Forfoods.FileName);

        //        //اذا المسار مش موجود سو مسار جديد 
        //        if (!Directory.Exists(FoodPath4))
        //        {
        //            Directory.CreateDirectory(FoodPath4);
        //        }
        //        using (var fileStream1 = new FileStream(Path.Combine(FoodPath4, fileNamefood4), FileMode.Create))
        //        {
        //            file4Forfoods.CopyTo(fileStream1);
        //        }

        //        foods.صورة4 =  fileNamefood4; // Update the image path
        //    }


        //        var existingFoods = _unitOfWork.FoodRepository.Get(u => u.ID2 == foods.ID2);

        //        if (existingFoods != null)
        //        {

        //            existingFoods.اسم_المادة_الغذئية1 = foods.اسم_المادة_الغذئية1;
        //            existingFoods.اسم_المادة_الغذئية2 = foods.اسم_المادة_الغذئية2;
        //            existingFoods.اسم_المادة_الغذئية3 = foods.اسم_المادة_الغذئية3;
        //            existingFoods.اسم_المادة_الغذئية4 = foods.اسم_المادة_الغذئية4;
        //            existingFoods.صورة1 = foods.صورة1;
        //            existingFoods.صورة2 = foods.صورة2;
        //            existingFoods.صورة3 = foods.صورة3;
        //            existingFoods.صورة4 = foods.صورة4;


        //            _unitOfWork.FoodRepository.Update(existingFoods);
        //        }
        //        else
        //        {
        //            _unitOfWork.FoodRepository.Add(foods);
        //        }
        //        _unitOfWork.Save();
        //    }
        //        }
        //    }
        //    TempData["success"] = "تم تحديث المواد الغذائية بشكل ناجح";

        //    return RedirectToAction("FoodList", new { id = foodViewModel.FoodViewM.BrandFK });
        //}



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
                string imagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", BrandFK , "FoodStuffs", FoodStuffsID, deleteFoodPicture.FoodStuffsImage);
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

    }
}



