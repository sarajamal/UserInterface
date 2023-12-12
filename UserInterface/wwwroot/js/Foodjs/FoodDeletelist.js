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
                data: 'اسم_المادة_الغذئية1',
                "width": "20%",
                "className": "text-center custom-font-bold",
                render: function (data, _, row) {
                    var numericID1 = parseInt(row.iD2, 10);
                    var nameFood1 = row.اسم_المادة_الغذئية1;
                    var numericID11 = parseInt(row.id, 10);
                    var imagePath1 = `/IMAGES/مواد/${nameFood1}/${numericID1}/${numericID11}/${row.صورة1}`;

                    // Customize the content of the cell with both text and image
                    return `<div>
                                <p>${data}</p>
                                <img src="${imagePath1}" alt="Image" width="150" height="100"/>
                            </div>`;
                },
                "width": "20%",
                "className": "text-center"
            },
            {
                data: 'اسم_المادة_الغذئية2',
                "width": "20%",
                "className": "text-center custom-font-bold",
                render: function (data, _, row) {
                    var numericID2 = parseInt(row.iD2, 10);
                    var nameFood2 = row.اسم_المادة_الغذئية2;
                    var numericID22 = parseInt(row.id, 10);
                    var imagePath2 = `/IMAGES/مواد/${nameFood2}/${numericID2}/${numericID22}/${row.صورة2}`;

                    // Customize the content of the cell with both text and image
                    return `<div>
                                <p>${data}</p>
                                <img src="${imagePath2}" alt="Image" width="150" height="100"/>
                            </div>`;
                },
                "width": "20%",
                "className": "text-center"
            },
            {
                data: 'اسم_المادة_الغذئية3',
                "width": "20%",
                "className": "text-center custom-font-bold",
                render: function (data, _, row) {
                    var numericID3 = parseInt(row.iD2, 10);
                    var nameFood3 = row.اسم_المادة_الغذئية3;
                    var numericID33 = parseInt(row.id, 10);
                    var imagePath3 = `/IMAGES/مواد/${nameFood3}/${numericID3}/${numericID33}/${row.صورة3}`;

                    // Customize the content of the cell with both text and image
                    return `<div>
                                <p>${data}</p>
                                <img src="${imagePath3}" alt="Image" width="150" height="100"/>
                            </div>`;
                },
                "width": "20%",
                "className": "text-center"
            },
            {
                data: 'اسم_المادة_الغذئية4',
                "width": "20%",
                "className": "text-center custom-font-bold",
                render: function (data, _, row) {
                    var numericID4 = parseInt(row.iD2, 10);
                    var nameFood4 = row.اسم_المادة_الغذئية4;
                    var numericID44 = parseInt(row.id, 10);
                    var imagePath4 = `/IMAGES/مواد/${nameFood4}/${numericID4}/${numericID44}/${row.صورة4}`;

                    // Customize the content of the cell with both text and image
                    return `<div>
                                <p>${data}</p>
                                <img src="${imagePath4}" alt="Image" width="150" height="100"/>
                            </div>`;
                },
                "width": "20%",
                "className": "text-center"
            },
            {
                data: 'iD2',
                "render": function (data) {
                    return `<div role="group">
                     <a href="/Food/FoodIndex?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>               
                     <a onClick=DelteFooodSave('/Food/DelteFooodSave/${data}') class="btn btn-danger "> <i class="bi bi-trash-fill"></i></a>
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

function DelteFooodSave(url) {
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

function validateForm() {
    // Get the text input and file input elements
    var textInput = document.getElementById('اسم_المادة_الغذئية1_@Model.FoodViewMList[i]');
    var fileInput = document.getElementById('customFile1_@Model.FoodViewMList[i]');

    // Check if the user has added new text
    var textInputValue = textInput.value.trim();

    if (!textInputValue) {
        // Alert the user or show an error message
        alert('Please add new text before submitting.');
        return false; // Prevent form submission
    }

    // If new text is added, update the file input name attribute
    if (textInputValue) {
        fileInput.name = 'file1_' + textInputValue;
    }

    // Additional validation logic if needed

    // If everything is valid, allow form submission
    return true;
}