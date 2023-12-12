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


        public IActionResult CleanList() //this for display List Of التحضيرات Page1
        {
            IEnumerable<Cleaning> objCleanList = _unitOfWork.CleanRepository.GetAll().OrderBy(item => item.CleaningOrder).ToList();

            // Display the updated list
            return View(objCleanList);
        }

        // تبع List  قائمة التنظيف
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Cleaning> objCleanList = _unitOfWork.CleanRepository.GetAll().OrderBy(item => item.CleaningOrder).ToList();
            return Json(new { data = objCleanList });
        }
        #endregion


        //زر الحذف في صفحة قائمة التنظيف 
        #region
        [HttpDelete]
        public IActionResult DeleteCleanPost(int? id)
        {

            var Deletesteps = _unitOfWork.StepsCleanRepository3.Get(u => u.ID_Tandeef1 == id);

            // Delete the associated image file
            if (!string.IsNullOrEmpty(Deletesteps?.الصورة1))
            {
                string imagePath = _webHostEnvironment.WebRootPath + Deletesteps.الصورة1;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            else
            {
                _unitOfWork.StepsCleanRepository3.Remove(Deletesteps);
            }

            if (!string.IsNullOrEmpty(Deletesteps?.الصورة2))
            {
                string imagePath2 = _webHostEnvironment.WebRootPath + Deletesteps.الصورة2;
                if (System.IO.File.Exists(imagePath2))
                {
                    System.IO.File.Delete(imagePath2);
                }
            }

            else
            {
                _unitOfWork.StepsCleanRepository3.Remove(Deletesteps);
            }
            _unitOfWork.StepsCleanRepository3.Remove(Deletesteps);

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
                stepsVM3 = new List<الخطوات3>(),
                tredMaeketCleanVM = new Brands(),

            };

            if (id == null || id == 0)
            {
                CLVM.CleanViewModel = new Cleaning();
                CLVM.CleanList = new List<Cleaning>();
                CLVM.stepsVM3 = new List<الخطوات3>();
                CLVM.tredMaeketCleanVM = new Brands();

                CLVM.CleanList = _unitOfWork.CleanRepository.GetAll().ToList(); ;

                return View(CLVM);
            }
            else //Update.
            {
                CLVM.CleanViewModel = _unitOfWork.CleanRepository.Get(u => u.CleaningID == id);
                CLVM.stepsVM3 = _unitOfWork.StepsCleanRepository3.GetAll().Where(c => c.ID_Tandeef1 == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

                return View(CLVM);
            };
        }

        [HttpPost] //This for Add Or Update Page . 
        public IActionResult Upsert3(CleanVM cleanVM, int selectedValue3) // should insert name in Upsert view
        {
            if (ModelState.IsValid)
            {
                //for update .. 
                //int preparationID = PrepaVM.PreparationVM.التحضير_ID;
                //int toolVarityID = PrepaVM.PreparationVM.التحضير_ID;
                int stepsID = cleanVM.CleanViewModel.CleaningID;

                //this code for image if add or update.
                string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder



                if (cleanVM.CleanViewModel.CleaningID == 0)  // if Add 
                {

                    _unitOfWork.CleanRepository.Add(cleanVM.CleanViewModel);
                    //_unitOfWork.PreparationRepository.Add(PrepaVM.PreparationName.Select());
                    _unitOfWork.Save();


                    //الخطوات
                    if (cleanVM.stepsVM3 != null)
                    {
                        foreach (var stepAdd in cleanVM.stepsVM3)
                        {

                            if (stepAdd != null && stepAdd.ID == 0)
                            {
                                string wwwRootstepPath = _webHostEnvironment.WebRootPath; // get us root folder
                                string stepPath = Path.Combine(wwwRootPath, @"images\DEVICE");

                                int stepID = cleanVM.CleanViewModel.CleaningID;
                                var newStep = new الخطوات3
                                {
                                    ID_Tandeef1 = stepID,
                                    الخطوة1 = stepAdd.الخطوة1,
                                    الخطوة2 = stepAdd.الخطوة2,
                                    رقم_الخطوة1 = stepAdd.رقم_الخطوة1,
                                    رقم_الخطوة2 = stepAdd.رقم_الخطوة2,

                                };
                                var file1Name1 = $"file1_{newStep.رقم_الخطوة1}";
                                var file1ForStep1 = HttpContext.Request.Form.Files[file1Name1];

                                if (file1ForStep1 != null && file1ForStep1.Length > 0)
                                {
                                    string fileName11 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForStep1.FileName);

                                    using (var fileStream = new FileStream(Path.Combine(stepPath, fileName11), FileMode.Create)) //save images
                                    {
                                        file1ForStep1.CopyTo(fileStream);
                                    }
                                    newStep.الصورة1 = @"\images\DEVICE\" + fileName11;
                                }

                                var fileName2 = $"file2_{newStep.رقم_الخطوة2}";
                                var fileStep2 = HttpContext.Request.Form.Files[fileName2];

                                if (fileStep2 != null && fileStep2.Length > 0)
                                {
                                    string fileName22 = Guid.NewGuid().ToString() + Path.GetExtension(fileStep2.FileName);

                                    using (var filStream = new FileStream(Path.Combine(stepPath, fileName22), FileMode.Create)) //save images
                                    {
                                        fileStep2.CopyTo(filStream);
                                    }
                                    newStep.الصورة2 = @"\images\DEVICE\" + fileName22;
                                }
                                _unitOfWork.StepsCleanRepository3.Add(newStep);
                                _unitOfWork.Save();
                            }
                        }
                    }
                    //// reOrder2 
                    if (selectedValue3 == 0)
                    {
                        // Get the maximum order value in the existing list
                        double maxOrder = _unitOfWork.CleanRepository.GetAll()
                            .Max(item => item.CleaningOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                        // Round down the maxOrder value to the nearest integer
                        int maxOrderAsInt = (int)Math.Floor(maxOrder);

                        // Set the new order value for the "اخرى" (Other) item
                        double newOrder = maxOrderAsInt + 1.0f;
                        cleanVM.CleanViewModel.CleaningOrder = newOrder;
                    }
                    else
                    {
                        var getIdOrder = _unitOfWork.CleanRepository.Get(u => u.CleaningID == selectedValue3);
                        double OldOrder = getIdOrder.CleaningOrder ?? 0.0f; // Default to 0.0f if Order is null
                        double newOrder = OldOrder + 0.1f;
                        cleanVM.CleanViewModel.CleaningOrder = newOrder;
                    }

                    List<Cleaning> objcleanList = _unitOfWork.CleanRepository.GetAll().OrderBy(item => item.CleaningOrder).ToList();
                    _unitOfWork.Save();
                    TempData["success"] = "تم إضافة التحضيرات بشكل ناجح";
                }

                else //for update التحضيرات,المكونات,الخطوات,الأدوات
                {
                    _unitOfWork.CleanRepository.Update(cleanVM.CleanViewModel); // تحديث التحضيرات
                    _unitOfWork.Save();


                    //الخطوات 
                    if (cleanVM.stepsVM3 != null)
                    {
                        for (int i = 0; i < cleanVM.stepsVM3.Count; i++)
                        {
                            var Steps = cleanVM.stepsVM3[i];

                            string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder
                            string StepsPath = Path.Combine(wwwRootPathSteps, @"images\DEVICE");

                            var file1Name = $"file1_{Steps.رقم_الخطوة1}";
                            var file1ForStep = HttpContext.Request.Form.Files[file1Name];

                            if (file1ForStep != null)
                            {
                                if (!string.IsNullOrEmpty(Steps.الصورة1)) // Check if there's an existing image path
                                {
                                    var OldImagePath1 = Path.Combine(wwwRootPathSteps, Steps.الصورة1.TrimStart('\\'));

                                    if (System.IO.File.Exists(OldImagePath1))
                                    {
                                        System.IO.File.Delete(OldImagePath1); // Delete old image if it exists
                                    }
                                }

                                string fileNameSteps1 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForStep.FileName);
                                using (var fileStream1 = new FileStream(Path.Combine(StepsPath, fileNameSteps1), FileMode.Create))
                                {
                                    file1ForStep.CopyTo(fileStream1);
                                }

                                Steps.الصورة1 = @"\images\DEVICE\" + fileNameSteps1; // Update the image path
                            }

                            var file2ForStep = HttpContext.Request.Form.Files[$"file2_{Steps.رقم_الخطوة2}"];

                            if (file2ForStep != null)
                            {
                                if (!string.IsNullOrEmpty(Steps.الصورة2)) // Check if there's an existing image path
                                {
                                    var OldImagePath2 = Path.Combine(wwwRootPathSteps, Steps.الصورة2.TrimStart('\\'));

                                    if (System.IO.File.Exists(OldImagePath2))
                                    {
                                        System.IO.File.Delete(OldImagePath2); // Delete old image if it exists
                                    }
                                }

                                string fileNameSteps2 = Guid.NewGuid().ToString() + Path.GetExtension(file2ForStep.FileName);
                                using (var fileStream2 = new FileStream(Path.Combine(StepsPath, fileNameSteps2), FileMode.Create))
                                {
                                    file2ForStep.CopyTo(fileStream2);
                                }

                                Steps.الصورة2 = @"\images\DEVICE\" + fileNameSteps2; // Update the image path
                            }
                            // Save or update Steps data to the database
                            if (Steps.ID_Tandeef1 == stepsID)
                            {
                                var existingSteps = _unitOfWork.StepsCleanRepository3.Get(u => u.ID == Steps.ID);

                                if (existingSteps != null)
                                {

                                    existingSteps.الخطوة1 = Steps.الخطوة1;
                                    existingSteps.الصورة1 = Steps.الصورة1;
                                    existingSteps.رقم_الخطوة1 = Steps.رقم_الخطوة1;

                                    existingSteps.الخطوة2 = Steps.الخطوة2;
                                    existingSteps.الصورة2 = Steps.الصورة2;
                                    existingSteps.رقم_الخطوة2 = Steps.رقم_الخطوة2;

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
                }
                return RedirectToAction("CleanList");
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
            var stepsToDelete = _unitOfWork.StepsCleanRepository3.Get(u => u.ID == id);

            if (stepsToDelete == null)
            {
                return Json(new { success = false, Message = "Error While Deleting" });
            }

            // Delete the associated image file
            if (!string.IsNullOrEmpty(stepsToDelete.الصورة1))
            {
                string imagePath = _webHostEnvironment.WebRootPath + stepsToDelete.الصورة1;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            if (!string.IsNullOrEmpty(stepsToDelete.الصورة2))
            {
                string imagePath2 = _webHostEnvironment.WebRootPath + stepsToDelete.الصورة2;
                if (System.IO.File.Exists(imagePath2))
                {
                    System.IO.File.Delete(imagePath2);
                }
            }

            int LastStep1 = (stepsToDelete?.رقم_الخطوة1 ?? 0) - 2;
            int LastStep2 = (stepsToDelete?.رقم_الخطوة2 ?? 0) - 2;

            // Delete the selected step
            _unitOfWork.StepsCleanRepository3.Remove(stepsToDelete);
            _unitOfWork.Save();

            var StepId = stepsToDelete.ID_Tandeef1;
            var stepsInPreparation = _unitOfWork.StepsCleanRepository3.GetAll().Where(c => c.ID_Tandeef1 == StepId).ToList();

            //هنا لتغيير الرقم ضروري يصير فيه لوب والشرط أن ضروري id اصغر منه الموجود 
            for (int i = 0; i < stepsInPreparation.Count; i++)
            {
                var step = stepsInPreparation[i];

                if (step.ID > id)
                {
                    step.رقم_الخطوة1 = LastStep1 + 2;
                    step.رقم_الخطوة2 = LastStep2 + 2;

                    LastStep1 += 2;
                    LastStep2 += 2;

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