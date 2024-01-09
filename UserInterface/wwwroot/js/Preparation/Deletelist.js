
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

    dataTable = $('#tblData1').dataTable({
        "ajax": {
            "url": `/Preparation/GetAll?id=${id}`, // Include the received ID in the URL
        },
        "columns": [
            {
                data: 'prepareName',
                "width": "40%",
                "className": "text-center custom-font-bold"
            },
            {
                data: 'prepareImage',
                "render": function (data, _, row) {
                    var numericID = parseInt(row.brandFK, 10); // Extract numeric part
                    var numericID2 = parseInt(row.preparationsID, 10); // Extract numeric part

                    var imagePath = `/IMAGES/${numericID}/Preparation/${numericID2}/${data}`;
                    return `<img src="${imagePath}" alt="Image" width="150" height="100"/>`;
                },
                "width": "44%",
                "className": "text-center"
            },
            {
                data: 'preparationsID',
                "render": function (data, type, row) {
                    // Assuming 'row' has a property for BrandFK
                    var brandFk = row.brandFK;

                    return `<div role="group">
            <a href="/Preparation/RedirectToUpsert?id=${data}&brandFK=${brandFk}" class="btn btn-style4 fnt-white mx-2"> 
                <i class="bi bi-pencil-square"></i>
            </a>               
            <a onClick=DeletePreparationPost('/Preparation/DeletePreparationPost/${data}') class="btn btn-style5 "> 
                <i class="bi bi-trash-fill"></i>
            </a>
        </div>`;
                },
                "width": "16%",
                "className": "text-center"
            },
            {
                data: 'preparationsOrder', // Assuming 'Order' is the name of your 'Order' column
                "visible": false, // Hide the "Order" column from the user interface
                "orderable": false // Disable sorting for the "Order" column
            }
        ],
        "order": [] // Disable initial sorting
    });
}
function DeletePreparationPost(url) {
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