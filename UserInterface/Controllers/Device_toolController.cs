using Microsoft.AspNetCore.Mvc;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.trade_mark;
using Test12.Models.ViewModel;

namespace Test12.Controllers
{
    public class Device_toolController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Device_toolController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;

        }
        //=========================GET LIST ===========================================
        public IActionResult RedirectToDeviceToolsList(int brandFK)
        {
            TempData["BrandFK"] = brandFK;
            TempData.Keep("BrandFK");

            return RedirectToAction("DeviceToolsList");
        }
        public IActionResult DeviceToolsList() //this for display List Of التحضيرات Page1
        {
            int? brandFK = TempData["BrandFK"] as int?;
            TempData.Keep("BrandFK"); // Keep the TempData for further use

            LoginTredMarktViewModel PrVM = new()
            {
                DeviceToolsLoginVMlist = _unitOfWork.Device_tools1.GetAll()
                .Where(u => u.BrandFK == brandFK).OrderBy(item => item.DevicesAndToolsOrder).ToList(),


                WelcomtredmarketDeviceTools = new LoginTredMarktViewModel()

            };
            PrVM.WelcomtredmarketDeviceTools.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK);
            PrVM.WelcomtredmarketDeviceTools.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomtredmarketDeviceTools.Productionvm = _unitOfWork.itemsRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomtredmarketDeviceTools.CleanLoginVM = _unitOfWork.CleanRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomtredmarketDeviceTools.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomtredmarketDeviceTools.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomtredmarketDeviceTools.PreparationVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == brandFK);
            PrVM.WelcomtredmarketDeviceTools.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomtredmarketDeviceTools.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomtredmarketDeviceTools.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomtredmarketDeviceTools.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomtredmarketDeviceTools.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomtredmarketDeviceTools.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == brandFK).ToList();
            PrVM.WelcomtredmarketDeviceTools.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == brandFK).ToList();

            PrVM.tredMaeketToolsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == brandFK);
            PrVM.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == brandFK);
            PrVM.DeviceToolsLoginVMlist = _unitOfWork.Device_tools1.GetAll(incloudeProperties: "Brand").Where(u => u.DevicesAndToolsID == brandFK).ToList();
            // Store the FK value in TempData
            TempData["ID"] = brandFK;
            // Display the updated list
            return View(PrVM);
        } 

        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            IEnumerable<DevicesAndTools> objPreparationList = _unitOfWork.Device_tools1.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.DevicesAndToolsOrder).ToList();

            return Json(new { data = objPreparationList });
        }
        #endregion
        //============================================================================

        //---------------------------------صفحة التعديل -----------------------------
        public IActionResult Index(int? id)
        {
            TempData.Keep("BrandFK"); // Keep the TempData for further use

            LoginTredMarktViewModel PrVM = new()
            {
                DeviceToolsLoginVM = new DevicesAndTools(),
                DeviceToolsLoginVMlist = new List<DevicesAndTools>(),
                tredMaeketToolsVM = new Brands(),

            };

            PrVM.tredMaeketToolsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            PrVM.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == id);
            PrVM.DeviceToolsLoginVMlist = _unitOfWork.Device_tools1.GetAll(incloudeProperties: "Brand").Where(u => u.DevicesAndToolsID == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

            return View(PrVM);
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginTredMarktViewModel device_ToolsVM)
        {

            if (ModelState.IsValid)
            {
                if (device_ToolsVM.DeviceToolsLoginVM != null)
                {
                    for (int i = 0; i < device_ToolsVM.DeviceToolsLoginVMlist.Count; i++)
                    {
                        var devices = device_ToolsVM.DeviceToolsLoginVMlist[i];

                        string DevicesAndToolsID = devices.DevicesAndToolsID.ToString();
                        string BrandFK = devices.BrandFK.ToString();
                        string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

                        var devicePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", DevicesAndToolsID);

                        var file1Name = $"file1_{devices.DevicesAndToolsID}";
                        var file1ForDevice = HttpContext.Request.Form.Files[file1Name];

                        if (file1ForDevice != null)
                        {
                            if (!string.IsNullOrEmpty(devices.DevicesAndTools_Image)) // Check if there's an existing image path
                            {
                                var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", DevicesAndToolsID, devices.DevicesAndTools_Image);

                                if (System.IO.File.Exists(OldImagePath1))
                                {
                                    System.IO.File.Delete(OldImagePath1); // Delete old image if it exists
                                }
                            }

                            string fileNameSteps1 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForDevice.FileName);

                            //اذا المسار مش موجود سو مسار جديد 
                            if (!Directory.Exists(devicePath1))
                            {
                                Directory.CreateDirectory(devicePath1);
                            }
                            using (var fileStream1 = new FileStream(Path.Combine(devicePath1, fileNameSteps1), FileMode.Create))
                            {
                                await file1ForDevice.CopyToAsync(fileStream1);
                            }

                            devices.DevicesAndTools_Image = fileNameSteps1; // Update the image path
                        }

                        var existingDevices = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == devices.DevicesAndToolsID);

                        if (existingDevices != null)
                        {

                            existingDevices.DevicesAndTools_Name = devices.DevicesAndTools_Name;

                            existingDevices.DevicesAndTools_Image = devices.DevicesAndTools_Image;

                            _unitOfWork.Device_tools1.Update(existingDevices);
                        }
                        else
                        {
                            _unitOfWork.Device_tools1.Add(devices);
                        }
                        _unitOfWork.Save();
                    }
                } 
            }
            TempData["success"] = "تم تحديث الأجهزة والأدوات بشكل ناجح";
            return RedirectToAction("RedirectToDeviceToolsList", new { brandFK = device_ToolsVM.DeviceToolsLoginVM.BrandFK });
        }
        //============================================================================

        //------------------------------صفحة الإضافة-----------------------------------
        public IActionResult CreateIndex(int? id)
        {
            LoginTredMarktViewModel PrVM = new()
            {
                DeviceToolsLoginVM = new DevicesAndTools(),
                DeviceToolsLoginVMlist = new List<DevicesAndTools>(),
                tredMaeketToolsVM = new Brands(),

            };

            PrVM.tredMaeketToolsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            PrVM.Devices_toolsVMorder = _unitOfWork.Device_tools1.GetAll().Where(u => u.BrandFK == id);
            PrVM.DeviceToolsLoginVM = new DevicesAndTools();
            PrVM.DeviceToolsLoginVMlist = new List<DevicesAndTools>();

            return View(PrVM);

        }
        [HttpPost]
        public async Task<IActionResult> CreateIndex(LoginTredMarktViewModel device_ToolsVM, int selectDevicetools)
        {
            if (ModelState.IsValid)
            {
                int DeviceFK = device_ToolsVM.tredMaeketToolsVM.BrandID;
                if (device_ToolsVM.DeviceToolsLoginVM.DevicesAndToolsID == 0)
                {
                    foreach (var deviceAdd in device_ToolsVM.DeviceToolsLoginVMlist)
                    {
                        if (deviceAdd != null && deviceAdd.DevicesAndToolsID == 0)
                        {
                            int LastId = _unitOfWork.Device_tools1.GetLastStepId();
                            int LastId1 = LastId + 1;
                            var newDevice = new DevicesAndTools
                            {
                                DevicesAndToolsID = LastId1,
                                BrandFK = DeviceFK,
                                DevicesAndTools_Name = deviceAdd.DevicesAndTools_Name,

                            };

                            string wwwRootDevicePath = _webHostEnvironment.WebRootPath; // get us root folder
                            var file1Name1 = $"file1_{newDevice.DevicesAndToolsID}";
                            var file1ForDevice1 = HttpContext.Request.Form.Files[file1Name1];

                            string DevicesAndToolsID = newDevice.DevicesAndToolsID.ToString();
                            string BrandFK = newDevice.BrandFK.ToString();

                            var devicePath1 = Path.Combine(wwwRootDevicePath, "IMAGES", DevicesAndToolsID);

                            if (file1ForDevice1 != null && file1ForDevice1.Length > 0)
                            {
                                string fileName11 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForDevice1.FileName);

                                if (!Directory.Exists(devicePath1))
                                {
                                    Directory.CreateDirectory(devicePath1);
                                }

                                using (var fileStream = new FileStream(Path.Combine(devicePath1, fileName11), FileMode.Create)) //save images
                                {
                                    await file1ForDevice1.CopyToAsync(fileStream);
                                }
                                newDevice.DevicesAndTools_Image = fileName11;

                            }
                            _unitOfWork.Device_tools1.Add(newDevice);
                            _unitOfWork.Save();

                            //// reOrder2 
                            if (selectDevicetools == 0)
                            {
                                int IDDevice = newDevice.DevicesAndToolsID;
                                newDevice.DevicesAndToolsOrder = IDDevice;

                                //// Get the maximum order value in the existing list
                                //double maxOrder = _unitOfWork.Device_tools1.GetAll()
                                //    .Max(item => item.DevicesAndToolsOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                                //// Round down the maxOrder value to the nearest integer
                                //int maxOrderAsInt = (int)Math.Floor(maxOrder);

                                //// Set the new order value for the "اخرى" (Other) item
                                //double newOrder = maxOrderAsInt + 1.0f;
                                //newDevice.DevicesAndToolsOrder = newOrder;
                            }
                            else
                            {
                                var getIdOrder = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == selectDevicetools);
                                int OldOrder = getIdOrder.DevicesAndToolsID; // Default to 0.0f if Order is null
                                double newOrder = OldOrder + 0.1;
                                newDevice.DevicesAndToolsOrder = newOrder;
                            }

                            List<DevicesAndTools> obdeviceToolsList = _unitOfWork.Device_tools1.GetAll().OrderBy(item => item.DevicesAndToolsOrder).ToList();
                            _unitOfWork.Save();
                        }
                    }

                }
            }

            TempData["success"] = "تم إضافة الأجهزة والأدوات بشكل ناجح";
            return RedirectToAction("RedirectToDeviceToolsList", new { brandFK = device_ToolsVM.tredMaeketToolsVM.BrandID });
        }
        //============================================================================

        //زر الحذف في صفحة قائمة  الأدوات والأجهزة 
        #region
        //[HttpDelete]
        public IActionResult DelteToolsdevice(int? id)
        {
            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            var deleteDeviceToolPicture = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == id);

            string DevicesAndToolsID = deleteDeviceToolPicture.DevicesAndToolsID.ToString();
            string BrandFK = deleteDeviceToolPicture.BrandFK.ToString();

            // Delete the associated image file
            if (!string.IsNullOrEmpty(deleteDeviceToolPicture.DevicesAndTools_Image))
            {
                string imagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", DevicesAndToolsID, deleteDeviceToolPicture.DevicesAndTools_Image);
                if (System.IO.File.Exists(imagePath1))
                {
                    System.IO.File.Delete(imagePath1);
                }
            }

            _unitOfWork.Device_tools1.Remove(deleteDeviceToolPicture);
            _unitOfWork.Save();

            return Json(new { success = true, redirectToUrl = Url.Action("RedirectToDeviceToolsList", new { BrandFK = BrandFK }) }); //أحتاج يرجع لنفس صفحة التعديل 
        }
        #endregion

        //-------------------GET LAST ID----------------------------------------------
        [HttpGet]
        public IActionResult GetLastId()
        {
            try
            {
                int lastId = _unitOfWork.Device_tools1.GetLastStepId();
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
