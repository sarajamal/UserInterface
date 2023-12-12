
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

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
            "url": '/Clean/GetAll'
        },
        "columns": [
            {
                data: 'اسم_الجهاز_أو_الأداة',
                "width": "40%",
                "className": "text-center custom-font-bold"
            },
            {
                data: 'iD_Tandeef',
                "render": function (data) {
                    return `<div role="group">
                     <a href="/Clean/Upsert3?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>               
                     <a onClick=DeleteCleanPost('/Clean/DeleteCleanPost/${data}') class="btn btn-danger "> <i class="bi bi-trash-fill"></i></a>
                    </div>`;
                },
                "width": "16%",
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