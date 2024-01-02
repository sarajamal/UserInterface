using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.ViewModel
{
    public class Device_toolsVM
    {
        [ValidateNever]
        public DevicesAndTools Device_toolVM { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public List<DevicesAndTools> Devices_toolsVM { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<DevicesAndTools> Devices_toolsVMorder { get; set; }

        [ValidateNever]
        public Brands tredMaeketToolsVM { get; set; }
        [ValidateNever]
        public LoginTredMarktViewModel WelcomtredmarketDeviceTools { get; set; }
    }
}
