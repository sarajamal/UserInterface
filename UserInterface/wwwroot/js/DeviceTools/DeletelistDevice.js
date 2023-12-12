
$(document).ready(function () {
     
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
                data: 'اسم_الجهاز_أو_الأداة1',
                "width": "30%",
                "className": "text-center custom-font-bold",
                render: function (data, _, row) {
                    var numericID1 = parseInt(row.iD1, 10);
                    var nameDevice1 = row.اسم_الجهاز_أو_الأداة1;
                    var numericID11 = parseInt(row.id, 10);
                    var imagePath1 = `/IMAGES/DEVICE/${nameDevice1}/${numericID1}/${numericID11}/${row.صورة3}`;

                    // Customize the content of the cell with both text and image
                    return `<div>
                                <p>${data}</p>
                                <img src="${imagePath1}" alt="Image" width="150" height="100"/>
                            </div>`;
                },
                "width": "30%",
                "className": "text-center"
            },
            {
                data: 'اسم_الجهاز_أو_الأداة2',
                "width": "30%",
                "className": "text-center custom-font-bold",
                render: function (data, _, row) {
                    var numericID2 = parseInt(row.iD1, 10);
                    var nameDevice2 = row.اسم_الجهاز_أو_الأداة2;
                    var numericID22 = parseInt(row.id, 10);
                    var imagePath2 = `/IMAGES/DEVICE/${nameDevice2}/${numericID2}/${numericID22}/${row.صورة2}`;

                    // Customize the content of the cell with both text and image
                    return `<div>
                                <p>${data}</p>
                                <img src="${imagePath2}" alt="Image" width="150" height="100"/>
                            </div>`;
                },
                "width": "30%",
                "className": "text-center"
            },
            {
                data: 'اسم_الجهاز_أو_الأداة3',
                "width": "30%",
                "className": "text-center custom-font-bold",
                render: function (data, _, row) {
                    var numericID3 = parseInt(row.iD1, 10);
                    var nameDevice3 = row.اسم_الجهاز_أو_الأداة3;
                    var numericID33 = parseInt(row.id, 10);
                    var imagePath3 = `/IMAGES/DEVICE/${nameDevice3}/${numericID3}/${numericID33}/${row.صورة1}`;

                    // Customize the content of the cell with both text and image
                    return `<div>
                                <p>${data}</p>
                                <img src="${imagePath3}" alt="Image" width="150" height="100"/>
                            </div>`;
                },
                "width": "30%",
                "className": "text-center"
            },
            {
                data: 'iD1',
                "render": function (data) {
                    return `<div role="group">
                     <a href="/Device_tool/Index?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>               
                     <a onClick=DelteToolsdevice('/Device_tool/DelteToolsdevice/${data}') class="btn btn-danger "> <i class="bi bi-trash-fill"></i></a>
                    </div>`;
                },
                "width": "20%",
                "className": "text-center"
            },
            {
                data: 'order', // Assuming 'Order' is the name of your 'Order' column
                "visible": false, // Hide the "Order" column from the user interface
                "orderable": false // Disable sorting for the "Order" column
            }
        ],
        "order": [] // Disable initial sorting
    });
}
function DelteToolsdevice(url) {
    Swal.fire({
        title: 'هل أنت متأكد ؟',
        text: " هل تريد استعادة ماتم حذفه؟",
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
                type: 'DELETE',
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