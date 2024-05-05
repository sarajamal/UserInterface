
$(function () {
     
    // Retrieve the id value from the data attribute in the thead element
    var id = document.querySelector("thead").getAttribute("data-id");
    loadDataTable(id);
});
function loadDataTable(id) {

    // Create a <style> element
    var style = document.createElement('style');
    // Define the CSS rules for your custom class
    style.innerHTML = `
        .custom-font-bold {
            font-family:Calibri;
            font-weight: bold;
            font-size: 16px;
        }
    `;

    // Append the <style> element to the document's <head>
    document.head.appendChild(style);

    dataTable = $('#tblDatadevice').dataTable({
        "ajax": {
            "url": `/Device_tool/GetAll?id=${id}`, // Include the received ID in the URL
        },
        "columns": [
            {
                data: 'devicesAndTools_Name',
                "width": "35%",
                "className": "text-center custom-font-bold",
 
            },
        {
                data: 'devicesAndTools_Image',
                    "render": function (data, _, row) {
                        var numericID = parseInt(row.devicesAndToolsID, 10);
                        var numericFK = parseInt(row.brandFK, 10);

                        // Adjusted imagePath2 to point to the external server
                        var imagePath2 = `/IMAGES/${numericID}/${row.devicesAndTools_Image}`;

                        // Customize the content of the cell with both text and image
                        return `<img src="${imagePath2}" alt="Image" width="150" height="100"/>`;
                    },
                "width": "45%",
                    "className": "text-center"
            },

            {
                data: 'devicesAndToolsID',
                "render": function (data) {
                    return `<div role="group">
                    <button type="button" class="btn btn-style4 fnt-white px-4 device-index-button"
                            data-toggle="modal"
                            data-target="#Index"
                            data-controller="Device_tool"
                            data-action="Index"
                            data-id="${data}">
                       <i class="bi bi-pencil-square"></i> </button>            
                     <a onClick=DelteToolsdevice('/Device_tool/DelteToolsdevice/${data}') class="btn btn-style5 "> <i class="bi bi-trash-fill"></i></a>
                    </div>`;
                },
                "width": "20%",
                "className": "text-center"
            },
            {
                data: 'devicesAndToolsOrder', // Assuming 'Order' is the name of your 'Order' column
                "visible": false, // Hide the "Order" column from the user interface
                "orderable": false // Disable sorting for the "Order" column
            }
        ],
        "order": [] // Disable initial sorting
    });
}
function DelteToolsdevice(url) {
    console.log("DelteToolsdevice function called with URL:", url); // Add this line

    Swal.fire({
        title: 'هل أنت متأكد ؟',
        text: "الاجهزة والأدوات  هل تريد استعادة ماتم حذفه؟",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'الغاء',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'حذف '
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url, // Use the provided ID parameter
                //type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'تم الحذف بنجاح',
                            text: data.message
                        }).then(() => {
                            location.reload(); // Reload the page after successful deletion
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'خطأ',
                            text: data.message
                        });
                    }
                }
            });
        }
    })
}

 

function loadAndShowModal(button) {
    var controller = button.getAttribute('data-controller');
    var action = button.getAttribute('data-action');
    var id = button.getAttribute('data-id');
    var url = `/${controller}/${action}?id=${id}`;
    var targetModalId = button.getAttribute('data-target');

    fetch(url)
        .then(response => response.text())
        .then(html => {
            console.log("Received HTML:", html); // For debugging purposes
            document.body.insertAdjacentHTML('beforeend', html);

            // Show the appropriate modal based on the targetModalId
            if (targetModalId === '#CreateDeviceTools') {
                $('#CreateDeviceTools').modal('show');
            } else if (targetModalId === '#Index') {
                $('#Index').modal('show');
            }
        })
        .catch(error => console.error('Error:', error));
}

document.addEventListener('DOMContentLoaded', function () {
    document.body.addEventListener('click', function (event) {
        if (event.target.matches('.add-button, .device-index-button') || event.target.closest('.add-button, .device-index-button')) {
            const button = event.target.matches('.add-button, .device-index-button') ? event.target : event.target.closest('.add-button, .device-index-button');
            loadAndShowModal(button);
        }
    });
});


//كود عرض الصور من السيرفر اذا كانت string
//{
//    data: 'devicesAndTools_Image',
//        "render": function (data, _, row) {
//            var numericID = parseInt(row.devicesAndToolsID, 10);
//            var numericFK = parseInt(row.brandFK, 10);

//            // Adjusted imagePath2 to point to the external server
//            var imagePath2 = `https://manuals.befranchisor.com/IMAGES/${numericFK}/DevicesAndTools/${numericID}/${row.devicesAndTools_Image}`;

//            // Customize the content of the cell with both text and image
//            return `<img src="${imagePath2}" alt="Image" width="150" height="100"/>`;
//        },
//    "width": "45%",
//        "className": "text-center"
//},

//{
//    data: 'devicesAndTools_Image',
//        "render": function (data, _, row) {
//            // Check if data is not null or undefined and it is actually a byte array
//            if (data && data instanceof Array) {
//                // Convert byte array to Base64 string
//                var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(data)));

//                // Determine the MIME type of the image from the byte array (magic numbers)
//                var mimeType = 'image/jpeg'; // Default to JPEG
//                if (data.length > 3) {
//                    // Check if it's a PNG
//                    if (data[0] === 0x89 && data[1] === 0x50 && data[2] === 0x4E && data[3] === 0x47) {
//                        mimeType = 'image/png';
//                    }
//                    // Other checks can be added here for different MIME types if needed
//                }

//                // Use the Base64 string as the source for the image with the determined MIME type
//                return `<img src="data:${mimeType};base64,${base64String}" alt="Image" width="150" height="100"/>`;
//            } else {
//                // If data is not a byte array, this code path shouldn't be reached. 
//                // You might want to handle this case differently.
//                return 'لايوجد صورة'; // Placeholder for when the image data isn't in the expected byte array format.
//            }

//        },
//    "width": "45%",
//        "className": "text-center"