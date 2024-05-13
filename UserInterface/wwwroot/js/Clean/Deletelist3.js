
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

    dataTable = $('#tblDataClean').dataTable({
        "ajax": {
            "url": `/Clean/GetAll?id=${id}`,
        },
        "columns": [
            {
                data: 'deviceName',
                "width": "40%",
                "className": "text-center custom-font-bold"
            },
        
            {
                data: 'cleaningID',
                "render": function (data, type, row) {
                    // Assuming 'row' has a property for BrandFK
                    var brandFk = row.brandFK;
                    return `<div role="group">
                     <a href="/Clean/RedirectToUpsert3?id=${data}&brandFK=${brandFk}" class="btn btn-style4 fnt-white mx-2"> <i class="bi bi-pencil-square"></i></a> 
                     <a onClick=DeleteCleanPost('/Clean/DeleteCleanPost/${data}') class="btn btn-style5 "> <i class="bi bi-trash-fill"></i></a>
                    </div>`;
                },
                "width": "16%",
                "className": "text-center"
            },
            {
                data: 'cleaningOrder', // Assuming 'Order' is the name of your 'Order' column
                "visible": false, // Hide the "Order" column from the user interface
                "orderable": false // Disable sorting for the "Order" column
            }
        ],
        "order": [] // Disable initial sorting
    });
}
function DeleteCleanPost(url) {
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

//function loadAndShowModal(button) {
//    var controller = button.getAttribute('data-controller');
//    var action = button.getAttribute('data-action');
//    var id = button.getAttribute('data-id');
//    var url = `/${controller}/${action}?id=${id}`;
//    var targetModalId = button.getAttribute('data-target');

//    fetch(url)
//        .then(response => response.text())
//        .then(html => {
//            console.log("Received HTML:", html); // For debugging purposes
//            document.body.insertAdjacentHTML('beforeend', html);

//            // Show the appropriate modal based on the targetModalId
//            if (targetModalId === '#Upsert3') {
//                $('#Upsert3').modal('show');
//            } else if (targetModalId === '#CreateClean') {
//                $('#CreateClean').modal('show');
//            }
//        })
//        .catch(error => console.error('Error:', error));
//}

//document.addEventListener('DOMContentLoaded', function () {
//    document.body.addEventListener('click', function (event) {
//        if (event.target.matches('.add-button, .clean-index-button') || event.target.closest('.add-button, .clean-index-button')) {
//            const button = event.target.matches('.add-button, .clean-index-button') ? event.target : event.target.closest('.add-button, .clean-index-button');
//            loadAndShowModal(button);
//        }
//    });
//});