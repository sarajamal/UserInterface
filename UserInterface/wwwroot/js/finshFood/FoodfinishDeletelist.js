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
                data: 'readyProductsName',
                "width": "40%",
                "className": "text-center custom-font-bold"
            },
            {
                data: 'readyProductsImage',
                "render": function (data, _, row) {

                    var numericID = parseInt(row.readyProductsID, 10);
                    var numericFK = parseInt(row.brandFK, 10);
                    
                    var imagePath3 = `/IMAGES/${numericFK}/ReadyProducts/${numericID}/${data}`;

                    return `<img src="${imagePath3}" alt="Image" width="150" height="100"/>`;
                },
                "width": "44%",
                "className": "text-center"
            },
            {
                data: 'readyProductsID',
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
                data: 'readyProductsOrder', // Assuming 'Order' is the name of your 'Order' column
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

function loadAndShowModal(button) {
    var controller = button.getAttribute('data-controller');
    var action = button.getAttribute('data-action');
    var id = button.getAttribute('data-id');
    var url = `/${controller}/${action}?id=${id}`;

    fetch(url)
        .then(response => response.text())
        .then(html => {
            console.log("Received HTML:", html); // Add this line to log the HTML
            // Dynamically add the modal HTML to the page
            document.body.insertAdjacentHTML('beforeend', html);

            // Now that the modal is part of the document, show it
            $('#CreateFinshFoods').modal('show');
        })
        .catch(error => console.error('Error:', error));
}
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.add-button').forEach(function (button) {
        button.addEventListener('click', function () {
            loadAndShowModal(this);
        });
    });
});