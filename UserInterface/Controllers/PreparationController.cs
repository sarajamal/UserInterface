
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.trade_mark;
using Test12.Models.ViewModel;


namespace Test12.Controllers
{
    public class PreparationController : Controller
    {
        /* private readonly ApplicationDbContext _context;*/

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PreparationController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;

        }
        //للإرسال FK الى صفحة القائمة بدون رقم ف URL
        public IActionResult RedirectToPreparation(int brandFK)
        {
            TempData["BrandFK"] = brandFK;
            TempData.Keep("BrandFK");
            return RedirectToAction("PreparationList");
        }
        public IActionResult RedirectToUpsert(int? id, int? brandFk)
        {
            TempData["BrandFK"] = brandFk;
            TempData["ID"] = id;
            TempData.Keep("BrandFK");
            return RedirectToAction("Upsert");
        }

        public IActionResult RedirectToCreatePreparation(int brandFk)
        {
            TempData["BrandFK"] = brandFk;
            TempData.Keep("BrandFK");
            return RedirectToAction("CreatePreparation");
        }
        public IActionResult PreparationList() //this for display List Of التحضيرات Page1
        {
            int? brandFK = TempData["BrandFK"] as int?;
            TempData.Keep("BrandFK"); // Keep the TempData for further use
            PreComViewModel PrVM = new PreComViewModel
            {
                PreparationList = _unitOfWork.PreparationRepository.GetAll(incloudeProperties: "component")
                            .Where(u => u.BrandFK == brandFK).OrderBy(item => item.PreparationsOrder).ToList(),
                WelcomTredMarketPrecomponent = new LoginTredMarktViewModel()

            };
            PrVM.WelcomTredMarketPrecomponent.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK);
            PrVM.WelcomTredMarketPrecomponent.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.ProductionLoginVM = _unitOfWork.itemsRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.CleanLoginVM = _unitOfWork.CleanRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.PreparatonLoginVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == brandFK).ToList();
            TempData["ID"] = brandFK;
            // Assuming you handle the header through layout or another mechanism
            return View(PrVM);

        }

        public IActionResult Upsert() // After Enter تعديل Display التحضيرات والمكونات...
        {
            int? brandFk = TempData["BrandFK"] as int?;
            int? id = TempData["ID"] as int?;

            PreComViewModel PrVM = new()
            {
                PreparationVM = new Preparations(),
                componontVMList = new List<PreparationIngredients>(),
                ToolsVarityVM = new List<PreparationTools>(),
                stepsVM = new List<PreparationSteps>(),
                tredMaeketVM = new Brands(),
                WelcomTredMarketPrecomponent = new LoginTredMarktViewModel()

            };

            PrVM.WelcomTredMarketPrecomponent.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFk);
            PrVM.WelcomTredMarketPrecomponent.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.BrandFK == brandFk);
            PrVM.WelcomTredMarketPrecomponent.ProductionLoginVM = _unitOfWork.itemsRepository.Get(u => u.BrandFK == brandFk);
            PrVM.WelcomTredMarketPrecomponent.CleanLoginVM = _unitOfWork.CleanRepository.Get(u => u.BrandFK == brandFk);
            PrVM.WelcomTredMarketPrecomponent.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == brandFk);
            PrVM.WelcomTredMarketPrecomponent.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == brandFk);
            PrVM.WelcomTredMarketPrecomponent.PreparatonLoginVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == brandFk);
            PrVM.WelcomTredMarketPrecomponent.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == brandFk).ToList();
            PrVM.WelcomTredMarketPrecomponent.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == brandFk).ToList();
            PrVM.WelcomTredMarketPrecomponent.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == brandFk).ToList();
            PrVM.WelcomTredMarketPrecomponent.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == brandFk).ToList();
            PrVM.WelcomTredMarketPrecomponent.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == brandFk).ToList();
            PrVM.WelcomTredMarketPrecomponent.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == brandFk).ToList();
            PrVM.WelcomTredMarketPrecomponent.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == brandFk).ToList();
            PrVM.tredMaeketVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            PrVM.PreparationVM = _unitOfWork.PreparationRepository.Get(u => u.PreparationsID == id);
            PrVM.componontVMList = _unitOfWork.ComponentRepository.GetAll(incloudeProperties: "Preparation").Where(c => c.PreparationsFK == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view
            PrVM.ToolsVarityVM = _unitOfWork.PrepaToolsVarietyRepository.GetAll(incloudeProperties: "Preparation").Where(c => c.PreparationsFK == id).ToList(); //هو يحتوي على قائمة من جدول الأدوات واللي يساعده على العرض هي viewD
            PrVM.stepsVM = _unitOfWork.StepsPreparationRepository.GetAll(incloudeProperties: "Preparation").Where(c => c.PreparationsFK == id).ToList(); //هو يحتوي على قائمة من جدول الأدوات واللي يساعده على العرض هي viewD
            return View(PrVM);
        }


        public IActionResult CreatePreparation() // After Enter تعديل Display التحضيرات والمكونات...
        {
            int? brandFK = TempData["BrandFK"] as int?;
            TempData.Keep("BrandFK"); // Keep the TempData for further use
            PreComViewModel PrVM = new()
            {
                PreparationVM = new Preparations(),
                componontVMList = new List<PreparationIngredients>(),
                ToolsVarityVM = new List<PreparationTools>(),
                stepsVM = new List<PreparationSteps>(),
                tredMaeketVM = new Brands(),
                WelcomTredMarketPrecomponent = new LoginTredMarktViewModel()

            };

            PrVM.WelcomTredMarketPrecomponent.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK);
            PrVM.WelcomTredMarketPrecomponent.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.ProductionLoginVM = _unitOfWork.itemsRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.CleanLoginVM = _unitOfWork.CleanRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.PreparatonLoginVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomTredMarketPrecomponent.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomTredMarketPrecomponent.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == brandFK).ToList();
            PrVM.PreparationVM = new Preparations();
            PrVM.componontVMList = new List<PreparationIngredients>();
            PrVM.ToolsVarityVM = new List<PreparationTools>();
            PrVM.stepsVM = new List<PreparationSteps>();

            PrVM.tredMaeketVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK);


            PrVM.PreparationList = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == brandFK);

            return View(PrVM);
        }


        [HttpPost] //This for Add Or Update Page . 
        public IActionResult CreatePreparation(PreComViewModel PrepaVM, IFormFile? file, int selectedValue) // should insert name in Upsert view
        {
            if (ModelState.IsValid)
            {

                var FK = PrepaVM.tredMaeketVM.BrandID;
                //for update .. 

                if (PrepaVM.PreparationVM.PreparationsID == 0)  // if Add 
                {

                    var setFK = new Preparations
                    {
                        BrandFK = FK,
                        prepareName = PrepaVM.PreparationVM.prepareName,
                        PreparationTime = PrepaVM.PreparationVM.PreparationTime,
                        VersionNumber = PrepaVM.PreparationVM.VersionNumber,
                        Expiry = PrepaVM.PreparationVM.Expiry,
                        NetWeight = PrepaVM.PreparationVM.NetWeight,
                        Station = PrepaVM.PreparationVM.Station,


                    };

                    _unitOfWork.PreparationRepository.Add(setFK);
                    _unitOfWork.Save();

                    //this code for image if add or update.
                    string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder

                    if (file != null)
                    {

                        // Convert numeric values to strings
                        string PreparationsID = setFK.PreparationsID.ToString(); // Convert to string
                        string preparationVMFk = PrepaVM.tredMaeketVM.BrandID.ToString(); // Convert to string

                        // Combine paths using Path.Combine, ensuring all arguments are strings
                        // Combine paths using Path.Combine, ensuring all arguments are strings
                        string PreparationDirectory = Path.Combine(wwwRootPath, "IMAGES",  PreparationsID);

                        //اذا المسار مش موجود سو مسار جديد 
                        if (!Directory.Exists(PreparationDirectory))
                        {
                            Directory.CreateDirectory(PreparationDirectory);
                        }

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        string PreparationPath = Path.Combine(PreparationDirectory, fileName);

                        // Use the correct file path when creating FileStream
                        using (var stream = new FileStream(PreparationPath, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                        }

                        setFK.prepareImage = fileName; // Save only the file name in the database
                        _unitOfWork.Save();
                        //return RedirectToAction("RedirectToUpsert", new { id = setFK.PreparationsID, brandFk = PrepaVM.PreparationVM.BrandFK });

                    }

                    int vvv = setFK.PreparationsID;
                    //المكونات

                    int lastIdComponents = _unitOfWork.ComponentRepository.GetLastComponentId();
                    int LastId1Components = lastIdComponents + 1;
                    var firstComponent = new PreparationIngredients
                    {
                        PrepIngredientsID = LastId1Components,
                        PreparationsFK = vvv,
                        PrepIngredientsName = Request.Form["PrepIngredientsName"], // Retrieve data from form
                        PrepUnit = Request.Form["PrepUnit"],
                        PrepQuantity = Request.Form["PrepQuantity"]
                    };

                    _unitOfWork.ComponentRepository.Add(firstComponent);
                    _unitOfWork.Save();

                    if (PrepaVM.componontVMList != null && PrepaVM.componontVMList.Any())
                    { // if condition checks whether the PrepaVM.componontVMList is not null and contains at least one item. 
                        foreach (var componentAdd in PrepaVM.componontVMList)
                        {
                            if (componentAdd != null && componentAdd.PreparationsFK == 0)
                            {
                                LastId1Components++;
                                var newComponent = new PreparationIngredients
                                {
                                    PrepIngredientsID = LastId1Components,
                                    PreparationsFK = vvv,
                                    PrepQuantity = componentAdd.PrepQuantity,
                                    PrepUnit = componentAdd.PrepUnit,
                                    PrepIngredientsName = componentAdd.PrepIngredientsName
                                };
                                _unitOfWork.ComponentRepository.Add(newComponent);
                                _unitOfWork.Save();
                            }
                        }
                    }
                    //أدوات التحضير والصنف 

                    int lastIdTools = _unitOfWork.PrepaToolsVarietyRepository.GetLastToolsId();
                    int LastId1Tools = lastIdTools + 1;
                    var firstRowToolAdd = new PreparationTools
                    {
                        PrepToolsID = LastId1Tools,
                        PreparationsFK = vvv,
                        PrepTools = Request.Form["PrepTools"],
                    };
                    _unitOfWork.PrepaToolsVarietyRepository.Add(firstRowToolAdd);
                    _unitOfWork.Save();

                    if (PrepaVM.ToolsVarityVM != null && PrepaVM.ToolsVarityVM.Any())
                    {
                        foreach (var ToolAdd in PrepaVM.ToolsVarityVM)
                        {
                            if (ToolAdd != null && ToolAdd.PrepToolsID == 0)
                            {
                                LastId1Tools++;

                                var newtool = new PreparationTools
                                {
                                    PrepToolsID = LastId1Tools,
                                    PreparationsFK = vvv,
                                    PrepTools = ToolAdd.PrepTools
                                };
                                _unitOfWork.PrepaToolsVarietyRepository.Add(newtool);
                                _unitOfWork.Save();
                            }
                        }
                    }
                    //الخطوات

                    if (PrepaVM.stepsVM != null)
                    {
                        foreach (var stepAdd in PrepaVM.stepsVM)
                        {

                            if (stepAdd != null && stepAdd.PrepStepsID == 0)
                            {
                                string wwwRootstepPath = _webHostEnvironment.WebRootPath; // get us root folder


                                int PrepStepsID = vvv;
                                int LastId = _unitOfWork.StepsPreparationRepository.GetLastStepId();
                                int LastId1 = LastId + 1;

                                var newStep = new PreparationSteps
                                {
                                    PrepStepsID = LastId1,
                                    PreparationsFK = PrepStepsID,
                                    PrepText = stepAdd.PrepText,
                                    PrepStepsNum = stepAdd.PrepStepsNum

                                };

                                var file1Name1 = $"file1_{newStep.PrepStepsID}";
                                var file1ForStep1 = HttpContext.Request.Form.Files[file1Name1];

                                string BrandVMFk = setFK.BrandFK.ToString();
                                string PrepStepsID1 = newStep.PrepStepsID.ToString();

                                string stepPath = Path.Combine(wwwRootPath, "IMAGES",  PrepStepsID1);

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
                                    newStep.PrepImage = fileName11;
                                }
                                _unitOfWork.StepsPreparationRepository.Add(newStep);
                                _unitOfWork.Save();
                            }
                        }
                    }

                    //// reOrder2 
                    if (selectedValue == 0)
                    {
                        // Get the maximum order value in the existing list
                        double maxOrder = _unitOfWork.PreparationRepository.GetAll()
                            .Max(item => item.PreparationsOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                        // Round down the maxOrder value to the nearest integer
                        int maxOrderAsInt = (int)Math.Floor(maxOrder);

                        // Set the new order value for the "اخرى" (Other) item
                        double newOrder = maxOrderAsInt + 1.0f;
                        setFK.PreparationsOrder = newOrder;
                    }
                    else
                    {
                        var getIdOrder = _unitOfWork.PreparationRepository.Get(u => u.PreparationsID == selectedValue);
                        double OldOrder = getIdOrder.PreparationsOrder ?? 0.0f; // Default to 0.0f if Order is null
                        double newOrder = OldOrder + 0.1f;
                        setFK.PreparationsOrder = newOrder;
                    }

                    List<Preparations> objPreparationList = _unitOfWork.PreparationRepository.GetAll().OrderBy(item => item.PreparationsOrder).ToList();
                    _unitOfWork.Save();
                    TempData["success"] = "تم إضافة التحضيرات بشكل ناجح";
                }



            }
            return RedirectToAction("RedirectToPreparation", new { brandFK = PrepaVM.tredMaeketVM.BrandID });
        }


        [HttpPost] //This for Add Or Update Page . 
        public IActionResult Upsert(PreComViewModel PrepaVM, IFormFile? file) // should insert name in Upsert view
        {
            if (ModelState.IsValid)
            {

                //for update .. 
                int preparationID = PrepaVM.PreparationVM.PreparationsID;
                int toolVarityID = PrepaVM.PreparationVM.PreparationsID;
                int stepsID = PrepaVM.PreparationVM.PreparationsID;

                //this code for image if add or update.
                string wwwRootPath = _webHostEnvironment.WebRootPath; // get us root folder


                string PreparationsID = PrepaVM.PreparationVM.PreparationsID.ToString();
                string PreparationFK = PrepaVM.PreparationVM.BrandFK.ToString();

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


                    // Construct the folder path where the image will be saved
                    string folderPath = Path.Combine(wwwRootPath, "IMAGES",  PreparationsID);
                    string PreparationPath = Path.Combine(folderPath, fileName);

                    // Ensure the directory exists
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Delete old image if it exists
                    if (!string.IsNullOrEmpty(PrepaVM.PreparationVM.prepareImage))
                    {
                        var oldImagePath = Path.Combine(folderPath, PrepaVM.PreparationVM.prepareImage);

                        try
                        {
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                                Console.WriteLine($"File deleted successfully: {oldImagePath}");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception (log it, display an error message, etc.)
                            Console.WriteLine($"Error deleting old image: {ex.Message}");
                        }
                    }

                    // Use the correct file path when creating FileStream
                    using (var stream = new FileStream(PreparationPath, FileMode.Create))
                    {
                        file.CopyToAsync(stream); // Ensure to use await since CopyToAsync is an async method
                    }

                    // Store only the file name in the database
                    PrepaVM.PreparationVM.prepareImage = fileName;
                }


                _unitOfWork.PreparationRepository.Update(PrepaVM.PreparationVM); // تحديث التحضيرات
                _unitOfWork.Save();

                //المكونات 
                if (PrepaVM.componontVMList != null) // تحديث المكونات
                {
                    for (int i = 0; i < PrepaVM.componontVMList.Count; i++)
                    {
                        var Components = PrepaVM.componontVMList[i];

                        int lastIdComponents = _unitOfWork.ComponentRepository.GetLastComponentId();
                        int LastId1Components = lastIdComponents + 1;

                        var existingComponent = _unitOfWork.ComponentRepository.Get(u => u.PrepIngredientsID == Components.PrepIngredientsID, incloudeProperties: "Preparation");
                        if (existingComponent == null)
                        {
                            var newComponent = new PreparationIngredients
                            {
                                PrepIngredientsID = LastId1Components,
                                PreparationsFK = preparationID,
                                PrepQuantity = Components.PrepQuantity,
                                PrepUnit = Components.PrepUnit,
                                PrepIngredientsName = Components.PrepIngredientsName
                            };
                            _unitOfWork.ComponentRepository.Add(newComponent);
                            _unitOfWork.Save();
                        }
                        else
                        {
                            existingComponent.PrepQuantity = Components.PrepQuantity;
                            existingComponent.PrepUnit = Components.PrepUnit;
                            existingComponent.PrepIngredientsName = Components.PrepIngredientsName;

                            _unitOfWork.ComponentRepository.Update(existingComponent);
                            _unitOfWork.Save();
                        }


                    }
                }
                //أدوات التحضير والصنف
                if (PrepaVM.ToolsVarityVM != null) //تحديث الأدوات.
                {
                    for (int i = 0; i < PrepaVM.ToolsVarityVM.Count; i++)
                    {
                        var Tools = PrepaVM.ToolsVarityVM[i];

                        int lastIdTools = _unitOfWork.PrepaToolsVarietyRepository.GetLastToolsId();
                        int LastId1Tools = lastIdTools + 1;

                        var existingtoolvariety = _unitOfWork.PrepaToolsVarietyRepository.Get(u => u.PrepToolsID == Tools.PrepToolsID, incloudeProperties: "Preparation");
                        if (existingtoolvariety == null)
                        {
                            var firstRowToolAdd = new PreparationTools
                            {
                                PrepToolsID = LastId1Tools,
                                PreparationsFK = preparationID,
                                PrepTools = Tools.PrepTools,
                            };
                            _unitOfWork.PrepaToolsVarietyRepository.Add(firstRowToolAdd);
                            _unitOfWork.Save();
                        }
                        else
                        {
                            existingtoolvariety.PrepTools = Tools.PrepTools;
                            _unitOfWork.PrepaToolsVarietyRepository.Update(existingtoolvariety);
                            _unitOfWork.Save();
                        }
                    }
                }
                //الخطوات 
                if (PrepaVM.stepsVM != null)
                {
                    for (int i = 0; i < PrepaVM.stepsVM.Count; i++)
                    {
                        var Steps = PrepaVM.stepsVM[i];

                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath; // get the root folder

                        int LastId = _unitOfWork.StepsPreparationRepository.GetLastStepId();
                        int LastId1 = LastId + 1;

                        var existingSteps9 = _unitOfWork.StepsPreparationRepository.Get(u => u.PrepStepsID == Steps.PrepStepsID, incloudeProperties: "Preparation");
                        if (existingSteps9 == null)
                        {
                            var newStep = new PreparationSteps
                            {
                                PrepStepsID = LastId1,
                                PreparationsFK = Steps.PreparationsFK,
                                PrepText = Steps.PrepText,
                                PrepStepsNum = Steps.PrepStepsNum

                            };

                            string IDstep = newStep.PrepStepsID.ToString();
                            string preparationVMFk = PrepaVM.PreparationVM.BrandFK.ToString();

                            string StepsPath = Path.Combine(wwwRootPath, "IMAGES",  IDstep);

                            var file1Name = $"file1_{newStep.PrepStepsID}";
                            var file1ForStep = HttpContext.Request.Form.Files[file1Name];

                            if (file1ForStep != null)
                            {
                                if (!string.IsNullOrEmpty(Steps.PrepImage)) // Check if there's an existing image path
                                {
                                    var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES",  IDstep, newStep.PrepImage);

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
                                    file1ForStep.CopyToAsync(fileStream1);
                                }

                                newStep.PrepImage = fileNameSteps1; // Update the image path
                                _unitOfWork.StepsPreparationRepository.Add(newStep);
                                _unitOfWork.Save();
                            }
                        }
                        else
                        {
                            string IDstep = Steps.PrepStepsID.ToString();
                            string preparationVMFk = PrepaVM.PreparationVM.BrandFK.ToString();

                            string StepsPath = Path.Combine(wwwRootPath, "IMAGES",  IDstep);

                            var file1Name = $"file1_{Steps.PrepStepsID}";
                            var file1ForStep = HttpContext.Request.Form.Files[file1Name];

                            if (file1ForStep != null)
                            {
                                if (!string.IsNullOrEmpty(Steps.PrepImage)) // Check if there's an existing image path
                                {
                                    var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES",  IDstep, Steps.PrepImage);

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
                                    file1ForStep.CopyToAsync(fileStream1);
                                }
                                Steps.PrepImage = fileNameSteps1;
                            }

                            // Save or update Steps data to the database
                            if (Steps.PreparationsFK == stepsID) // int stepsID = PrepaVM.PreparationVM.التحضير_ID;
                            {
                                var existingSteps = _unitOfWork.StepsPreparationRepository.Get(u => u.PrepStepsID == Steps.PrepStepsID, incloudeProperties: "Preparation");

                                if (existingSteps != null)
                                {

                                    existingSteps.PrepText = Steps.PrepText;
                                    existingSteps.PrepImage = Steps.PrepImage;
                                    existingSteps.PrepStepsNum = Steps.PrepStepsNum;

                                    _unitOfWork.StepsPreparationRepository.Update(existingSteps);
                                }
                                else
                                {
                                    _unitOfWork.StepsPreparationRepository.Add(Steps);
                                }
                                _unitOfWork.Save();
                            }
                        }
                    }
                }
                TempData["success"] = "تم تحديث التحضيرات بشكل ناجح";

                return RedirectToAction("RedirectToUpsert", new { id = PrepaVM.PreparationVM.PreparationsID, brandFk = PrepaVM.PreparationVM.BrandFK });
            }

            else
            {
                return View(PrepaVM);
            }
        }

        [HttpGet]
        public IActionResult GetLastId()
        {
            try
            {
                int lastId = _unitOfWork.StepsPreparationRepository.GetLastStepId();
                return Ok(lastId);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return StatusCode(500, ex.Message);
            }
        }


        // تبع List 
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {
            var preparations = _unitOfWork.PreparationRepository.GetAll(incloudeProperties: "component")
          .Where(u => u.BrandFK == id).OrderBy(item => item.PreparationsOrder).ToList();

            return Json(new { data = preparations });
        }
        #endregion


        // زر الحذف تبع المكونات 
        #region API CALLS 
        [HttpDelete]
        public IActionResult Delete(int? id) //this is for delete button in rows component 
        {
            var ComponentDelete = _unitOfWork.ComponentRepository.Get(u => u.PrepIngredientsID == id);
            int PreparationFk = ComponentDelete.PreparationsFK;
            var BrandFKEx = _unitOfWork.PreparationRepository.Get(u => u.PreparationsID == PreparationFk);
            int? BranFK = BrandFKEx.BrandFK;
            if (ComponentDelete == null)
            {
                return Json(new { success = false, Message = "Error While Deleting" });
            }

            _unitOfWork.ComponentRepository.Remove(ComponentDelete);
            _unitOfWork.Save();
            return Json(new { success = true, redirectToUrl = Url.Action("RedirectToUpsert", new { id = PreparationFk, BrandFK = BranFK }) });
        }
        #endregion

        //زر حذف صفحة تعديل الأدوات 
        #region API CALLS 
        [HttpDelete]
        public IActionResult DeleteToolVariety(int? id) //this is for delete button in rows أدوات التحضير والصنف
        {

            var toolsVarityDelete = _unitOfWork.PrepaToolsVarietyRepository.Get(u => u.PrepToolsID == id);
            int PreparationFk = toolsVarityDelete.PreparationsFK;
            var BrandFKEx = _unitOfWork.PreparationRepository.Get(u => u.PreparationsID == PreparationFk);
            int? BranFK = BrandFKEx.BrandFK;

            if (toolsVarityDelete == null)
            {

                return Json(new { success = false, Message = "Error While Deleting" });
            }

            _unitOfWork.PrepaToolsVarietyRepository.Remove(toolsVarityDelete);
            _unitOfWork.Save();
            return Json(new { success = true, redirectToUrl = Url.Action("RedirectToUpsert", new { id = PreparationFk, BrandFK = BranFK }) }); //أحتاج يرجع لنفس صفحة التعديل 
        }
        #endregion

        ////زر الحذف تبع صفحة تعديل الخطوات
        #region API CALLS
        [HttpDelete]
        public IActionResult Deletesteps(int? id)
        {
            var stepsToDelete = _unitOfWork.StepsPreparationRepository.Get(u => u.PrepStepsID == id);
            var BrandFK = _unitOfWork.PreparationRepository.Get(u => u.PreparationsID == stepsToDelete.PreparationsFK);

            string IDStep = stepsToDelete.PrepStepsID.ToString();
            string FKBrand = BrandFK.BrandFK.ToString();

            //عشان أوجهه لصفحة التعديل 
            int PreparationFk = stepsToDelete.PreparationsFK;
            int? BranFK = BrandFK.BrandFK;

            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            if (stepsToDelete == null)
            {
                return Json(new { success = false, Message = "Error While Deleting" });
            }

            // Delete the associated image file
            if (!string.IsNullOrEmpty(stepsToDelete.PrepImage))
            {
                string imagePath = Path.Combine(wwwRootPathSteps, "IMAGES", IDStep, stepsToDelete.PrepImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _unitOfWork.StepsPreparationRepository.Remove(stepsToDelete);
            _unitOfWork.Save();

            // Find all steps with a higher PrepStepsNum
            var preparationFK = stepsToDelete.PreparationsFK;

            var subsequentSteps = _unitOfWork.StepsPreparationRepository
                .GetAll(incloudeProperties: "Preparation").Where(u => u.PreparationsFK == preparationFK).ToList(); // Add ToList() to materialize the query;

            // Decrement PrepStepsNum for each subsequent step
            for (int i = 0; i < subsequentSteps.Count; i++)
            {
                var step = subsequentSteps[i];

                if (step.PrepStepsID > id)
                {
                    var getOld = _unitOfWork.StepsPreparationRepository.Get(u => u.PrepStepsID == step.PrepStepsID);
                    getOld.PrepStepsNum -= 1;
                    _unitOfWork.StepsPreparationRepository.Update(step);
                }
            }
            _unitOfWork.Save();

            return Json(new { success = true, redirectToUrl = Url.Action("RedirectToUpsert", new { id = PreparationFk, BrandFK = BranFK }) }); //أحتاج يرجع لنفس صفحة التعديل 

        }
        #endregion

        ////زر الحذف في صفحة قائمة التحضيرات 
        //#region
        //[HttpDelete]
        //public IActionResult DeletePreparationPost(int? id)
        //{
        //    var DeleteTools = _unitOfWork.PrepaToolsVarietyRepository.GetAll(incloudeProperties: "Preparation").Where(u => u.PreparationsFK == id).ToList();
        //    _unitOfWork.PrepaToolsVarietyRepository.RemoveRange(DeleteTools);


        //    var DelteComponent = _unitOfWork.ComponentRepository.GetAll(incloudeProperties: "Preparation").Where(u => u.PreparationsFK == id).ToList();
        //    _unitOfWork.ComponentRepository.RemoveRange(DelteComponent);

        //    var Deletesteps = _unitOfWork.StepsPreparationRepository.GetAll(incloudeProperties: "Preparation").Where(u => u.PreparationsFK == id).ToList();
        //    if (Deletesteps != null)
        //    {
        //        for (int i = 0; i < Deletesteps.Count; i++)
        //        {
        //            var delet = Deletesteps[i];
        //            var BrandId = _unitOfWork.PreparationRepository.Get(u => u.PreparationsID == delet.PreparationsFK);
        //            var IDstep = _unitOfWork.StepsPreparationRepository.Get(u => u.PrepStepsID == delet.PrepStepsID);

        //            string IDStep = IDstep.PrepStepsID.ToString();
        //            string FKBrand = BrandId.BrandFK.ToString();

        //            if (!string.IsNullOrEmpty(delet.PrepImage))
        //            {
        //                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IMAGES", FKBrand, "Preparation", IDStep, delet.PrepImage);
        //                if (System.IO.File.Exists(imagePath))
        //                {
        //                    System.IO.File.Delete(imagePath);
        //                }
        //            }

        //            _unitOfWork.StepsPreparationRepository.Remove(delet);
        //        }


        //    }
        //     var DeleteoneOflist = _unitOfWork.PreparationRepository.Get(u => u.PreparationsID == id);
        //     string FKBrandToRedyrect = DeleteoneOflist.BrandFK.ToString();

        //    //عشان أوجهه لصفحة List 
        //     int? FKBrandToRedyrect1 = DeleteoneOflist.BrandFK;
        //    if (DeleteoneOflist == null)
        //    {

        //        return Json(new { success = false, Message = "Error While Deleting" });
        //    }
        //    string IDStep2 = DeleteoneOflist.PreparationsID.ToString();
        //    string FKBrand2 = DeleteoneOflist.BrandFK.ToString();

        //    if (!string.IsNullOrEmpty(DeleteoneOflist.prepareImage))
        //    {
        //        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IMAGES", FKBrand2, "Preparation", IDStep2, DeleteoneOflist.prepareImage);
        //        if (System.IO.File.Exists(imagePath))
        //        {
        //            System.IO.File.Delete(imagePath);
        //        }
        //    }
        //    _unitOfWork.PreparationRepository.Remove(DeleteoneOflist);
        //    _unitOfWork.Save();
        //    return Json(new { success = true, redirectToUrl = Url.Action("RedirectToPreparation", new {  BrandFK = FKBrandToRedyrect1 }) }); //أحتاج يرجع لنفس صفحة التعديل 

        // }
        //#endregion

    }
}


