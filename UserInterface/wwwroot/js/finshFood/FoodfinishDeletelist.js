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

    dataTable = $('#tblFoodfinish').dataTable({
        "ajax": {
            "url": `/FinishProducts/GetAll?id=${id}`, // Include the received ID in the URL
        },
        "columns": [
            {
                data: 'اسم_المنتج',
                "width": "40%",
                "className": "text-center custom-font-bold"
            },
            {
                data: 'صورة',
                "render": function (data, _, row) {

                    var numericID1 = parseInt(row.iD3, 10);
                    var nameFoodfinish1 = row.اسم_المنتج;
                    var numericID11 = parseInt(row.id, 10);
                    var imagePath3 = `/IMAGES/منتجات_جاهزة/${nameFoodfinish1}/${numericID1}/${numericID11}/${data}`;

                    return `<img src="${imagePath3}" alt="Image" width="150" height="100"/>`;
                },
                "width": "44%",
                "className": "text-center"
            },
            {
                data: 'iD3',
                "render": function (data) {
                    return `<div role="group">
                     <a href="/FinishProducts/FinishProductsIndex?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>               
                     <a onClick=DeleteFinshFood('/FinishProducts/DeleteFinshFood/${data}') class="btn btn-danger "> <i class="bi bi-trash-fill"></i></a>
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

function DeleteFinshFood(url) {
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

