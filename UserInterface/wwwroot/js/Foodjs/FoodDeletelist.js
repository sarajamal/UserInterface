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

    dataTable = $('#tblFood').dataTable({
        "ajax": {
            "url": `/Food/GetAll?id=${id}`, // Include the received ID in the URL
        },
        "columns": [
            {
                data: 'foodStuffsName',
                "width": "35%",
                "className": "text-center custom-font-bold",

            },
            {
                data: 'foodStuffsImage',

                "render": function (data, _, row) {
                    var numericID = parseInt(row.foodStuffsID, 10);
                    var numericFK = parseInt(row.brandFK, 10);

                    var imagePath2 = `/IMAGES/${numericFK}/FoodStuffs/${numericID}/${row.foodStuffsImage}`;

                    // Customize the content of the cell with both text and image
                    return `<img src="${imagePath2}" alt="Image" width="150" height="100"/>`;
                },
                "width": "45%",
                "className": "text-center"
            },

            {
                data: 'foodStuffsID',
                "render": function (data) {
                    return `<div role="group">
                            <button type="button" class="btn btn-style4 fnt-white px-4 food-index-button"
                            data-toggle="modal"
                            data-target="#FoodsIndex"
                            data-controller="Food"
                            data-action="FoodIndex"
                            data-id="${data}">
                       <i class="bi bi-pencil-square"></i> </button>             
                       <a onClick=DelteFooodSave('/Food/DelteFooodSave/${data}') class="btn btn-style5 "> <i class="bi bi-trash-fill"></i></a>
                    </div>`;
                },
                "width": "15%",
                "className": "text-center"
            },
            {
                data: 'foodStuffsOrder', // Assuming 'Order' is the name of your 'Order' column
                "visible": false, // Hide the "Order" column from the user interface
                "orderable": false // Disable sorting for the "Order" column
            }
        ],
        "order": [] // Disable initial sorting
    });
}

function DelteFooodSave(url) {
    console.log("DelteToolsdevice function called with URL:", url); // Add this line

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
    var targetModalId = button.getAttribute('data-target');

    fetch(url)
        .then(response => response.text())
        .then(html => {
            console.log("Received HTML:", html); // For debugging purposes
            document.body.insertAdjacentHTML('beforeend', html);

            // Show the appropriate modal based on the targetModalId
            if (targetModalId === '#CreateFoods') {
                $('#CreateFoods').modal('show');
            } else if (targetModalId === '#FoodsIndex') {
                $('#FoodsIndex').modal('show');
            }
        })
        .catch(error => console.error('Error:', error));
}

document.addEventListener('DOMContentLoaded', function () {
    document.body.addEventListener('click', function (event) {
        if (event.target.matches('.add-button, .food-index-button') || event.target.closest('.add-button, .food-index-button')) {
            const button = event.target.matches('.add-button, .food-index-button') ? event.target : event.target.closest('.add-button, .food-index-button');
            loadAndShowModal(button);
        }
    });
});

//function validateForm() {
//    // Get the text input and file input elements
//    var textInput = document.getElementById('اسم_المادة_الغذئية1_@Model.FoodViewMList[i]');
//    var fileInput = document.getElementById('customFile1_@Model.FoodViewMList[i]');

//    // Check if the user has added new text
//    var textInputValue = textInput.value.trim();

//    if (!textInputValue) {
//        // Alert the user or show an error message
//        alert('Please add new text before submitting.');
//        return false; // Prevent form submission
//    }

//    // If new text is added, update the file input name attribute
//    if (textInputValue) {
//        fileInput.name = 'file1_' + textInputValue;
//    }

//    // Additional validation logic if needed

//    // If everything is valid, allow form submission
//    return true;
//}

//$(document).ready(function () {
//    $('#tblFood').on('click', '.edit-button', function () {
//        var foodId = $(this).data('id');

//        // Optionally, make an AJAX request to get the details of the food item
//        $.ajax({
//            url: '/Food/FoodIndex', // Update with the correct URL
//            type: 'GET',
//            data: { id: foodId },
//            success: function (response) {
//                // Populate the modal with the response
//                // Assuming the server returns the HTML content to be displayed in the modal
//                $('#editFoodModal .modal-body').html(response);
//            }
//        });

//        // Open the modal
//        $('#editFoodModal').modal('show');
//    });
//});

