using Microsoft.AspNetCore.Mvc;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.Production;
using Test12.Models.Models.trade_mark;
using Test12.Models.ViewModel;

namespace Test12.Controllers
{
    public class CleanController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CleanController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;

        }


        public IActionResult CleanList(int? id) //this for display List Of التحضيرات Page1
        {
            IEnumerable<Cleaning> objCleanList = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == id).OrderBy(item => item.CleaningOrder).ToList();

            // Store the FK value in TempData
            TempData["ID"] = id;

            // Display the updated list
            return View(objCleanList);
        }

        // تبع List  قائمة التنظيف
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {
            IEnumerable<Cleaning> objCleanList = _unitOfWork.CleanRepository.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.CleaningOrder).ToList();

            return Json(new { data = objCleanList });
        }
        #endregion


        //زر الحذف في صفحة قائمة التنظيف 
        #region
        [HttpDelete]
        public IActionResult DeleteCleanPost(int? id)
        {

            var Deletesteps = _unitOfWork.StepsCleanRepository3.GetAll(incloudeProperties: "Cleaning").Where(u => u.CleaningFK == id).ToList();
            if (Deletesteps != null)
            {
                for (int i = 0; i < Deletesteps.Count; i++)
                {
                    var delet = Deletesteps[i];
                    var BrandId = _unitOfWork.CleanRepository.Get(u => u.CleaningID == delet.CleaningFK);
                    var IDstep = _unitOfWork.StepsCleanRepository3.Get(u => u.CleaStepsID == delet.CleaStepsID);

                    string IDStep = IDstep.CleaStepsID.ToString();
                    string FKBrand = BrandId.BrandFK.ToString();
                    // Delete the associated image file
                    if (!string.IsNullOrEmpty(delet.CleaStepsImage))
                    {
                        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IMAGES", FKBrand, "Cleaning", IDStep, delet.CleaStepsImage);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    _unitOfWork.StepsCleanRepository3.Remove(delet);
                }

            }

            var DeleteoneOflist = _unitOfWork.CleanRepository.Get(u => u.CleaningID == id);
            if (DeleteoneOflist == null)
            {

                return Json(new { success = false, Message = "Error While Deleting" });
            }

            _unitOfWork.CleanRepository.Remove(DeleteoneOflist);
            _unitOfWork.Save();
            return Json(new { success = true });
        }
        #endregion


        public IActionResult Upsert3(int? id) // After Enter تعديل Display التحضيرات والمكونات...
        {
            CleanVM CLVM = new()
            {
                CleanViewModel = new Cleaning(),
                CleanList = new List<Cleaning>(),
                CleaningSteps = new List<CleaningSteps>(),
                tredMaeketCleanVM = new Brands(),

            };

            CLVM.CleanList = _unitOfWork.CleanRepository.GetAll().Where(c => c.CleaningID == id).ToList();
            CLVM.tredMaeketCleanVM = _unitOfWork.TredMarketRepository.Get(c => c.BrandID == id);
            CLVM.CleanViewModel = _unitOfWork.CleanRepository.Get(c => c.CleaningID == id);
            CLVM.CleaningSteps = _unitOfWork.StepsCleanRepository3.GetAll().Where(c => c.CleaningFK == id).ToList();

            return View(CLVM);
        }

        [HttpPost] //This for Add Or Update Page . 
        public IActionResult Upsert3(CleanVM cleanVM) // should insert name in Upsert view
        {
            if (ModelState.IsValid)
            {
                //for update .. 
                int stepsID = cleanVM.CleanViewModel.CleaningID;

                _unitOfWork.CleanRepository.Update(cleanVM.CleanViewModel);
                _unitOfWork.Save();

                //الخطوات
                if (cleanVM.CleaningSteps != null)
                {
                    for (int i = 0; i < cleanVM.CleaningSteps.Count; i++)
                    {
                        var Steps = cleanVM.CleaningSteps[i];

                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

                        var existingSteps9 = _unitOfWork.StepsCleanRepository3.Get(u => u.CleaStepsID == Steps.CleaStepsID, incloudeProperties: "Cleaning");
                        if (existingSteps9 == null)
                        {
                            _unitOfWork.StepsCleanRepository3.Add(Steps);
                            _unitOfWork.Save();
                        }

                        string IDstep = Steps.CleaStepsID.ToString();
                        string CleanVMFk = cleanVM.CleanViewModel.BrandFK.ToString();

                        string StepsPath = Path.Combine(wwwRootPathSteps, "IMAGES", CleanVMFk, "Cleaning", IDstep);

                        var file1Name = $"file1_{Steps.CleaStepsID}";
                        var file1ForStep = HttpContext.Request.Form.Files[file1Name];

                        if (file1ForStep != null)
                        {
                            if (!string.IsNullOrEmpty(Steps.CleaStepsImage)) // Check if there's an existing image path
                            {
                                var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", CleanVMFk, "Cleaning", IDstep, Steps.CleaStepsImage);

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

                            Steps.CleaStepsImage = fileNameSteps1; // Update the image path
                        }

                        // Save or update Steps data to the database
                        if (Steps.CleaningFK == stepsID) // int stepsID = PrepaVM.PreparationVM.التحضير_ID;
                        {
                            var existingSteps = _unitOfWork.StepsCleanRepository3.Get(u => u.CleaStepsID == Steps.CleaStepsID, incloudeProperties: "Cleaning");

                            if (existingSteps != null)
                            {

                                existingSteps.CleaStepsImage = Steps.CleaStepsImage;
                                existingSteps.CleaText = Steps.CleaText;
                                existingSteps.CleaStepsNum = Steps.CleaStepsNum;

                                _unitOfWork.StepsCleanRepository3.Update(existingSteps);
                            }
                            else
                            {
                                _unitOfWork.StepsCleanRepository3.Add(Steps);
                            }

                            _unitOfWork.Save();
                        }
                    }
                }

                TempData["success"] = "تم تحديث التنظيف بشكل ناجح";

                return RedirectToAction("CleanList", new { id = cleanVM.CleanViewModel.BrandFK });
            }

            else
            {
                return View(cleanVM);
            }
        }

        //زر الحذف تبع صفحة تعديل الخطوات
        #region API CALLS
        [HttpDelete]
        public IActionResult Deletestep3(int? id)
        {
            var stepsToDelete = _unitOfWork.StepsCleanRepository3.Get(u => u.CleaStepsID == id);

            var BrandFK = _unitOfWork.CleanRepository.Get(u => u.CleaningID == stepsToDelete.CleaningFK);

            string IDStep = stepsToDelete.CleaStepsID.ToString();
            string FKBrand = BrandFK.BrandFK.ToString();

            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            if (stepsToDelete == null)
            {
                return Json(new { success = false, Message = "Error While Deleting" });
            }

            // Delete the associated image file
            if (!string.IsNullOrEmpty(stepsToDelete.CleaStepsImage))
            {
                string imagePath = Path.Combine(wwwRootPathSteps, "IMAGES", FKBrand, "Cleaning", IDStep, stepsToDelete.CleaStepsImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _unitOfWork.StepsCleanRepository3.Remove(stepsToDelete);
            _unitOfWork.Save();

            // Find all steps with a higher PrepStepsNum
            var CleaningFK = stepsToDelete.CleaningFK;

            var subsequentSteps = _unitOfWork.StepsCleanRepository3
                .GetAll(incloudeProperties: "Cleaning").Where(u => u.CleaningFK == CleaningFK).ToList(); // Add ToList() to materialize the query;

            // Decrement PrepStepsNum for each subsequent step
            for (int i = 0; i < subsequentSteps.Count; i++)
            {
                var step = subsequentSteps[i];

                if (step.CleaStepsID > id)
                {
                    var getOld = _unitOfWork.StepsCleanRepository3.Get(u => u.CleaStepsID == step.CleaStepsID);
                    getOld.CleaStepsNum -= 1;
                    _unitOfWork.StepsCleanRepository3.Update(step);
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
    }
}