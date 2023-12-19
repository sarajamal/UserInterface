using Microsoft.AspNetCore.Mvc;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Preparation;
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

        public IActionResult DeviceToolsList(int? id) //this for display List Of التحضيرات Page1
        {

            IEnumerable<DevicesAndTools> objdeviceToolsList = _unitOfWork.Device_tools1.GetAll().Where(u => u.BrandFK == id).OrderBy(item => item.DevicesAndToolsOrder).ToList();

            // Store the FK value in TempData
            TempData["ID"] = id;
            // Display the updated list
            return View(objdeviceToolsList);
        }

        public IActionResult Index(int? id)
        {
            Device_toolsVM PrVM = new()
            {
                Device_toolVM = new DevicesAndTools(),
                Devices_toolsVM = new List<DevicesAndTools>(),
                tredMaeketToolsVM = new Brands(),

            };

            PrVM.tredMaeketToolsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            PrVM.Device_toolVM = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == id);
            PrVM.Devices_toolsVM = _unitOfWork.Device_tools1.GetAll(incloudeProperties: "Brand").Where(u => u.DevicesAndToolsID == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

            return View(PrVM);

        }

        public IActionResult CreateIndex(int? id)
        {
            Device_toolsVM PrVM = new()
            {
                Device_toolVM = new DevicesAndTools(),
                Devices_toolsVM = new List<DevicesAndTools>(),
                tredMaeketToolsVM = new Brands(),

            };

            PrVM.tredMaeketToolsVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            PrVM.Devices_toolsVMorder = _unitOfWork.Device_tools1.GetAll().Where(u => u.BrandFK == id);
            PrVM.Device_toolVM = new DevicesAndTools();
            PrVM.Devices_toolsVM = new List<DevicesAndTools>();

            return View(PrVM);

        }


        [HttpPost]
        public IActionResult CreateIndex(Device_toolsVM device_ToolsVM, int selectDevicetools)
        {

            if (ModelState.IsValid)
            {
                int DeviceFK = device_ToolsVM.tredMaeketToolsVM.BrandID;
                if (device_ToolsVM.Device_toolVM.DevicesAndToolsID == 0)
                {

                    foreach (var deviceAdd in device_ToolsVM.Devices_toolsVM)
                    {

                        if (deviceAdd != null && deviceAdd.DevicesAndToolsID == 0)
                        {

                            var newDevice = new DevicesAndTools
                            {
                                //ID = DeviceFK,
                                //اسم_الجهاز_أو_الأداة1 = deviceAdd.اسم_الجهاز_أو_الأداة1,
                                //اسم_الجهاز_أو_الأداة2 = deviceAdd.اسم_الجهاز_أو_الأداة2,
                                //اسم_الجهاز_أو_الأداة3 = deviceAdd.اسم_الجهاز_أو_الأداة3,

                            };
                            _unitOfWork.Device_tools1.Add(newDevice);
                            _unitOfWork.Save();

                            string wwwRootDevicePath = _webHostEnvironment.WebRootPath; // get us root folder


                            //var file1Name1 = $"file1_{newDevice.اسم_الجهاز_أو_الأداة1}";
                            //var file1ForDevice1 = HttpContext.Request.Form.Files[file1Name1];

                            //string اسم_الجهاز_أو_الأداة1 = newDevice.اسم_الجهاز_أو_الأداة1?.ToString();
                            //string اسم_الجهاز_أو_الأداة2 = newDevice.اسم_الجهاز_أو_الأداة2?.ToString();
                            //string اسم_الجهاز_أو_الأداة3 = newDevice.اسم_الجهاز_أو_الأداة3?.ToString();
                            //string ID = newDevice.ID.ToString();
                            //string ID1 = newDevice.ID1.ToString();


                            //var devicePath1 = Path.Combine(wwwRootDevicePath, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة1 ?? "", ID1, ID);
                            //var devicePath2 = Path.Combine(wwwRootDevicePath, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة2 ?? "", ID1, ID);
                            //var devicePath3 = Path.Combine(wwwRootDevicePath, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة3 ?? "", ID1, ID);

                            //if (file1ForDevice1 != null && file1ForDevice1.Length > 0)
                            //{
                            //    string fileName11 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForDevice1.FileName);

                            //    if (!Directory.Exists(devicePath1))
                            //    {
                            //        Directory.CreateDirectory(devicePath1);
                            //    }

                            //    using (var fileStream = new FileStream(Path.Combine(devicePath1, fileName11), FileMode.Create)) //save images
                            //    {
                            //        file1ForDevice1.CopyTo(fileStream);
                            //    }
                            //    newDevice.صورة3 = fileName11;
                            //}

                            //var fileName2 = $"file2_{newDevice.اسم_الجهاز_أو_الأداة2}";
                            //var fileDevice2 = HttpContext.Request.Form.Files[fileName2];

                            //if (fileDevice2 != null && fileDevice2.Length > 0)
                            //{
                            //    string fileName22 = Guid.NewGuid().ToString() + Path.GetExtension(fileDevice2.FileName);

                            //    if (!Directory.Exists(devicePath2))
                            //    {
                            //        Directory.CreateDirectory(devicePath2);
                            //    }
                            //    using (var filStream = new FileStream(Path.Combine(devicePath2, fileName22), FileMode.Create)) //save images
                            //    {
                            //        fileDevice2.CopyTo(filStream);
                            //    }
                            //    newDevice.صورة2 = fileName22;
                            //}
                            //var fileName3 = $"file3_{newDevice.اسم_الجهاز_أو_الأداة3}";
                            //var fileDevice3 = HttpContext.Request.Form.Files[fileName3];

                            //if (fileDevice3 != null && fileDevice3.Length > 0)
                            //{
                            //    string fileName33 = Guid.NewGuid().ToString() + Path.GetExtension(fileDevice3.FileName);

                            //    if (!Directory.Exists(devicePath3))
                            //    {
                            //        Directory.CreateDirectory(devicePath3);
                            //    }
                            //    using (var filStream = new FileStream(Path.Combine(devicePath3, fileName33), FileMode.Create)) //save images
                            //    {
                            //        fileDevice3.CopyTo(filStream);
                            //    }
                            //    newDevice.صورة1 = fileName33;
                            //}
                            //_unitOfWork.Save();
                            //// reOrder2 
                            if (selectDevicetools == 0)
                            {
                                // Get the maximum order value in the existing list
                                double maxOrder = _unitOfWork.Device_tools1.GetAll()
                                    .Max(item => item.DevicesAndToolsOrder) ?? 0.0f; // Default to 0.0f if there are no existing items

                                // Round down the maxOrder value to the nearest integer
                                int maxOrderAsInt = (int)Math.Floor(maxOrder);

                                // Set the new order value for the "اخرى" (Other) item
                                double newOrder = maxOrderAsInt + 1.0f;
                                newDevice.DevicesAndToolsOrder = newOrder;
                            }
                            else
                            {
                                var getIdOrder = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == selectDevicetools);
                                double OldOrder = getIdOrder.DevicesAndToolsOrder ?? 0.0f; // Default to 0.0f if Order is null
                                double newOrder = OldOrder + 0.1f;
                                newDevice.DevicesAndToolsOrder = newOrder;
                            }

                            List<DevicesAndTools> obdeviceToolsList = _unitOfWork.Device_tools1.GetAll().OrderBy(item => item.DevicesAndToolsOrder).ToList();
                            _unitOfWork.Save();
                        }
                    }

                }
            }

            TempData["success"] = "تم إضافة الأجهزة والأدوات بشكل ناجح";
            return RedirectToAction("DeviceToolsList", new { id = device_ToolsVM.tredMaeketToolsVM.BrandID });
        }

        [HttpPost]
        public IActionResult Index(Device_toolsVM device_ToolsVM)
        {

            if (ModelState.IsValid)
            {
        //        if (device_ToolsVM.Devices_toolsVM != null)
        //        {
        //            for (int i = 0; i < device_ToolsVM.Devices_toolsVM.Count; i++)
        //            {
        //                var devices = device_ToolsVM.Devices_toolsVM[i];
        //                string اسم_الجهاز_أو_الأداة1 = devices.اسم_الجهاز_أو_الأداة1?.ToString();
        //                string اسم_الجهاز_أو_الأداة2 = devices.اسم_الجهاز_أو_الأداة2?.ToString();
        //                string اسم_الجهاز_أو_الأداة3 = devices.اسم_الجهاز_أو_الأداة3?.ToString();
        //                string ID = devices.ID.ToString();
        //                string ID1 = devices.ID1.ToString();


        //                string wwwRootPathSteps = _webHostEnvironment.WebRootPath;
        //                get the root folder

        //var devicePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة1 ?? "", ID1, ID);

        //                var devicePath2 = Path.Combine(wwwRootPathSteps, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة2 ?? "", ID1, ID);

        //                var devicePath3 = Path.Combine(wwwRootPathSteps, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة3 ?? "", ID1, ID);

        //                var file1Name = $"file1_{devices.اسم_الجهاز_أو_الأداة1}";
        //                var file1ForDevice = HttpContext.Request.Form.Files[file1Name];

        //                if (file1ForDevice != null)
        //                {
        //                    if (!string.IsNullOrEmpty(devices.صورة3)) // Check if there's an existing image path
        //                    {
        //                        var OldImagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة1 ?? "", ID1, ID, devices.صورة3);

        //                        if (System.IO.File.Exists(OldImagePath1))
        //                        {
        //                            System.IO.File.Delete(OldImagePath1); // Delete old image if it exists
        //                        }
        //                    }

        //                    string fileNameSteps1 = Guid.NewGuid().ToString() + Path.GetExtension(file1ForDevice.FileName);

        //                    //اذا المسار مش موجود سو مسار جديد 
        //                    if (!Directory.Exists(devicePath1))
        //                    {
        //                        Directory.CreateDirectory(devicePath1);
        //                    }
        //                    using (var fileStream1 = new FileStream(Path.Combine(devicePath1, fileNameSteps1), FileMode.Create))
        //                    {
        //                        file1ForDevice.CopyTo(fileStream1);
        //                    }

        //                    devices.صورة3 = fileNameSteps1; // Update the image path
        //                }

        //                var file2Fordevice = HttpContext.Request.Form.Files[$"file2_{devices.اسم_الجهاز_أو_الأداة2 ?? "".ToString()}"];

        //                if (file2Fordevice != null)
        //                {
        //                    if (!string.IsNullOrEmpty(devices.صورة2)) // Check if there's an existing image path
        //                    {
        //                        var OldImagePath2 = Path.Combine(wwwRootPathSteps, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة2 ?? "", ID1, ID, devices.صورة2);

        //                        if (System.IO.File.Exists(OldImagePath2))
        //                        {
        //                            System.IO.File.Delete(OldImagePath2); // Delete old image if it exists
        //                        }
        //                    }

        //                    string fileNameDevice2 = Guid.NewGuid().ToString() + Path.GetExtension(file2Fordevice.FileName);

        //                    اذا المسار مش موجود سو مسار جديد
        //                    if (!Directory.Exists(devicePath2))
        //                    {
        //                        Directory.CreateDirectory(devicePath2);
        //                    }
        //                    using (var fileStream2 = new FileStream(Path.Combine(devicePath2, fileNameDevice2), FileMode.Create))
        //                    {
        //                        file2Fordevice.CopyTo(fileStream2);
        //                    }

        //                    devices.صورة2 = fileNameDevice2; // Update the image path
        //                }

        //                var file3Name = $"file3_{devices.اسم_الجهاز_أو_الأداة3}";
        //                var file3ForDevice = HttpContext.Request.Form.Files[file3Name];

        //                if (file3ForDevice != null)
        //                {
        //                    if (!string.IsNullOrEmpty(devices.صورة1)) // Check if there's an existing image path
        //                    {
        //                        var OldImagePath3 = Path.Combine(wwwRootPathSteps, "IMAGES", "DEVICE", اسم_الجهاز_أو_الأداة3 ?? "", ID1, ID, devices.صورة1);

        //                        if (System.IO.File.Exists(OldImagePath3))
        //                        {
        //                            System.IO.File.Delete(OldImagePath3); // Delete old image if it exists
        //                        }
        //                    }

        //                    string fileNameDevice3 = Guid.NewGuid().ToString() + Path.GetExtension(file3ForDevice.FileName);
        //                    //اذا المسار مش موجود سو مسار جديد 
        //                    if (!Directory.Exists(devicePath3))
        //                    {
        //                        Directory.CreateDirectory(devicePath3);
        //                    }

        //                    using (var fileStream1 = new FileStream(Path.Combine(devicePath3, fileNameDevice3), FileMode.Create))
        //                    {
        //                        file3ForDevice.CopyTo(fileStream1);
        //                    }

        //                    devices.صورة1 = fileNameDevice3; // Update the image path
        //                }

        //                var existingDevices = _unitOfWork.Device_tools1.Get(u => u.ID1 == devices.ID1);

        //                if (existingDevices != null)
        //                {

        //                    existingDevices.اسم_الجهاز_أو_الأداة1 = devices.اسم_الجهاز_أو_الأداة1;
        //                    existingDevices.اسم_الجهاز_أو_الأداة2 = devices.اسم_الجهاز_أو_الأداة2;
        //                    existingDevices.اسم_الجهاز_أو_الأداة3 = devices.اسم_الجهاز_أو_الأداة3;
        //                    existingDevices.صورة3 = devices.صورة3;
        //                    existingDevices.صورة2 = devices.صورة2;
        //                    existingDevices.صورة1 = devices.صورة1;


        //                    _unitOfWork.Device_tools1.Update(existingDevices);
        //                }
        //                else
        //                {
        //                    _unitOfWork.Device_tools1.Add(devices);
        //                }
        //                _unitOfWork.Save();
        //            }
                //}
                TempData["success"] = "تم تحديث الأجهزة والأدوات بشكل ناجح";
                return RedirectToAction("DeviceToolsList", new
                {
                    id = device_ToolsVM.Device_toolVM.BrandFK
                });
            }
            else
            {
                return View(device_ToolsVM);
            }
        }








        // تبع List 
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            IEnumerable<DevicesAndTools> objPreparationList = _unitOfWork.Device_tools1.GetAll()
                .Where(u => u.BrandFK == id).OrderBy(item => item.DevicesAndToolsOrder).ToList();

            return Json(new { data = objPreparationList });
        }
        #endregion


        //زر الحذف في صفحة قائمة  الأدوات والأجهزة 
        #region
        [HttpDelete]
        public IActionResult DelteToolsdevice(int? id)
        {
            string wwwRootPathSteps = _webHostEnvironment.WebRootPath;

            var deleteDeviceToolPicture = _unitOfWork.Device_tools1.Get(u => u.DevicesAndToolsID == id);
 
            string DevicesAndToolsID = deleteDeviceToolPicture.DevicesAndToolsID.ToString();
            string BrandFK = deleteDeviceToolPicture.BrandFK.ToString();

            // Delete the associated image file
            if (!string.IsNullOrEmpty(deleteDeviceToolPicture.DevicesAndTools_Image))
            {
                string imagePath1 = Path.Combine(wwwRootPathSteps, "IMAGES",  BrandFK, "DeviceAndTools", DevicesAndToolsID, deleteDeviceToolPicture.DevicesAndTools_Image);
                if (System.IO.File.Exists(imagePath1))
                {
                    System.IO.File.Delete(imagePath1);
                }
            }
            
            _unitOfWork.Device_tools1.Remove(deleteDeviceToolPicture);
            _unitOfWork.Save();
     
            return Json(new { success = true });
        }
        #endregion

    }
}
