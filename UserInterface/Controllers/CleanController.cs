using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //صفحة التعديل   
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


        //صفحة الاضافة
        public IActionResult CreateClean(int? id) // After Enter تعديل Display التحضيرات والمكونات...
        {
            CleanVM CLVM = new()
            {
                CleanViewModel = new Cleaning(),
                CleaningVMorder = new List<Cleaning>(),
                CleaningSteps = new List<CleaningSteps>(),
                tredMaeketCleanVM = new Brands(),

            };

            CLVM.CleaningVMorder = _unitOfWork.CleanRepository.GetAll().Where(c => c.BrandFK == id).ToList();
            CLVM.tredMaeketCleanVM = _unitOfWork.TredMarketRepository.Get(c => c.BrandID == id);
            CLVM.CleanViewModel = new Cleaning();
            CLVM.CleaningSteps = new List<CleaningSteps>();

            return View(CLVM);
        }

        //POST صفحة الاضافة 
        [HttpPost]
        public IActionResult CreateClean(CleanVM clean, int selectCleaning) // After Enter تعديل Display التحضيرات والمكونات...
        {
            if (ModelState.IsValid)
            {

                var FK = clean.tredMaeketCleanVM.BrandID;
                //for update .. 

                if (clean.CleanViewModel.CleaningID == 0)  // if Add 
                {

                    var setFK = new Cleaning
                    {
                        BrandFK = FK,
                        DeviceName = clean.CleanViewModel.DeviceName,
                        Note = clean.CleanViewModel.Note,

                    };
                    _unitOfWork.CleanRepository.Add(setFK);
                    _unitOfWork.Save();
                    int FKClean = setFK.CleaningID;

                    //الخطوات

                    if (clean.CleaningSteps != null)
                    {
                        foreach (var stepAdd in clean.CleaningSteps)
                        {

                            if (stepAdd != null && stepAdd.CleaStepsID == 0)
                            {
                                string wwwRootstepPath = _webHostEnvironment.WebRootPath; // get us root folder


                                int CleaningFK = FKClean;
                                var newStep = new CleaningSteps
                                {
                                    CleaningFK = CleaningFK,
                                    CleaText = stepAdd.CleaText,
                                    CleaStepsNum = stepAdd.CleaStepsNum

                                };
                                _unitOfWork.StepsCleanRepository3.Add(newStep);
                                _unitOfWork.Save();

                                var file1Name1 = $"file1_{newStep.CleaStepsID}";
                                var file1ForStep1 = HttpContext.Request.Form.Files[file1Name1];

                                string BrandVMFk = setFK.BrandFK.ToString();
                                string CleanStepsID1 = newStep.CleaStepsID.ToString();

                                string stepPath = Path.Combine(wwwRootstepPath, "IMAGES", BrandVMFk, "Cleaning", CleanStepsID1);

                                if (file1ForStep1 != null && file1ForStep1.Length > 0)
                                {
                                    string fileName11 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForStep1.FileName);

                                    if (!Directory.Exists(stepPath))
                                    {
                                        Directory.CreateDirectory(stepPath);
                                    }

                                    using (var fileStream = new FileStream(Path.Combine(stepPath, fileName11), FileMode.Create)) //save images
                                    {
                                        file1ForStep1.CopyTo(fileStream);
                                    }
                                    newStep.CleaStepsImage = fileName11;
                                }
                                _unitOfWork.Save();
                            }
                        }
                    }

                    //// reOrder2 
                    if (selectCleaning == 0)
                    {
                        // Get the maximum order value in the existing list
                        double maxOrder = _unitOfWork.CleanRepository.GetAll()
                            .Max(item => item.CleaningOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                        // Round down the maxOrder value to the nearest integer
                        int maxOrderAsInt = (int)Math.Floor(maxOrder);

                        // Set the new order value for the "اخرى" (Other) item
                        double newOrder = maxOrderAsInt + 1.0f;
                        setFK.CleaningOrder = newOrder;
                    }
                    else
                    {
                        var getIdOrder = _unitOfWork.CleanRepository.Get(u => u.CleaningID == selectCleaning);
                        double OldOrder = getIdOrder.CleaningOrder ?? 0.0f; // Default to 0.0f if Order is null
                        double newOrder = OldOrder + 0.1f;
                        setFK.CleaningOrder = newOrder;
                    }

                    List<Cleaning> objCleanList = _unitOfWork.CleanRepository.GetAll().OrderBy(item => item.CleaningOrder).ToList();
                    _unitOfWork.Save();
                    TempData["success"] = "تم إضافة التنظيف بشكل ناجح";
                }



            }
            return RedirectToAction("CleanList", new { id = clean.tredMaeketCleanVM.BrandID });
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
                        int LastId = _unitOfWork.CleanRepository.GetLastStepId();
                        int LastId1 = LastId + 1;
                        var existingSteps9 = _unitOfWork.StepsCleanRepository3.Get(u => u.CleaStepsID == Steps.CleaStepsID, incloudeProperties: "Cleaning");
                        if (existingSteps9 == null)
                        {
                            var newStep = new CleaningSteps
                            {
                                CleaStepsID = LastId1,
                                CleaningFK = Steps.CleaningFK,
                                CleaText = Steps.CleaText,
                                CleaStepsNum = Steps.CleaStepsNum

                            };
                     
                            string IDstep = newStep.CleaStepsID.ToString();
                            string CleanVMFk = cleanVM.CleanViewModel.BrandFK.ToString();

                            string StepsPath = Path.Combine(wwwRootPathSteps, "IMAGES", CleanVMFk, "Cleaning", IDstep);

                            var file1Name = $"file1_{newStep.CleaStepsID}";
                            var file1ForStep = HttpContext.Request.Form.Files[file1Name];

                            if (file1ForStep != null)
                            {
                                if (!string.IsNullOrEmpty(Steps.CleaStepsImage)) // Check if there's an existing image path
                                {
                                    var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", CleanVMFk, "Cleaning", IDstep, newStep.CleaStepsImage);

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

                                newStep.CleaStepsImage = fileNameSteps1; // Update the image path
                                _unitOfWork.StepsCleanRepository3.Add(newStep);
                                _unitOfWork.Save();

                            }

                        }
                        else
                        {

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

        [HttpGet]
        public IActionResult GetLastId()
        {
            try
            {
                int lastId = _unitOfWork.CleanRepository.GetLastStepId();
                _unitOfWork.Save();

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