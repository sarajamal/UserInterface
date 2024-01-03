



function displaySelectedImage(input, imgId) {

    // Get the reference to the HTML img element based on the provided imgId
    var imgElement = document.getElementById(imgId);

    // Check if a file has been selected in the input element
    if (input.files && input.files[0]) {

        // Create a new FileReader object to read the selected file
        var reader = new FileReader();

        // Define an event handler for when the FileReader has finished reading the file
        reader.onload = function (e) {

            // Set the 'src' attribute of the img element to the read image data
            imgElement.src = e.target.result;
        };
        // Read the selected file as a data URL (base64 encoded)
        reader.readAsDataURL(input.files[0]);
    }
}

function toggleAddButtonVisibility(value) {

    var addButton = document.getElementById("addToolsButton");
    var redMessage = document.querySelector(".red-message");
    var saveButton = document.getElementById("saveChange");


    if (value.trim() !== "") {
        addButton.disabled = false; // Enable the button if text is entered
        saveButton.disabled = false; // Enable the button if text is entered

        redMessage.style.display = "none"; // Hide the red message
    }

    // Disable all delete buttons with the class 'delete-button'
    var deleteButtons = document.querySelectorAll(".delete-button");
    deleteButtons.forEach(function (button) {
        button.disabled = true;
    });

}

//صفحة الاضافة اجهزة وادوات جديدة 
var clickCount = 0;
var lastID = 0; // Initialize lastID globally
function AddnewtoolsDeviceNew(DeviceToolsFK) {

    if (clickCount === 0) {
        // Only retrieve lastID from server on the first click
        $.ajax({
            url: '/Device_tool/GetLastId',
            type: 'GET',
            success: function (response) {
                lastID = parseInt(response) + 1;
                addStep(DeviceToolsFK);
            },
            error: function (xhr, status, error) {
                console.error('Error fetching last ID:', error);
            }
        });
    } else {
        // On subsequent clicks, increment lastID locally
        lastID++;
        addStep(DeviceToolsFK);
    }

    function addStep(DeviceToolsFK) {

        // Find the table body element
        var tableBody = document.querySelector("#tblDeviceTools tbody");

        // Find the last row index
        var newRowIndex = tableBody.children.length - 1;

        // Create a new row for الخطوة1 and الخطوة2 in the same row
        var newRow = document.createElement("tr");
        newRow.innerHTML = `
      <td style="text-align:center;">
        <input type="hidden" name="Devices_toolsVM[${newRowIndex}].BrandFK" value="${DeviceToolsFK}" />
        <input type="hidden" name="Devices_toolsVM[${newRowIndex}].DevicesAndTools_Image" />

        <div class="form-group">
            <textarea class="form-control" id="Devices_toolsVM_${newRowIndex}" name="Devices_toolsVM[${newRowIndex}].DevicesAndTools_Name"></textarea>
        </div>
    </td>
    <td style="text-align:center;">
        <div class="row">
            <div class="col-12 text-center">
                <img id="PreviewPhoto1_${lastID}" src="/IMAGES/noImage.png" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                <input type="file" name="file1_${lastID}" class="border-0 shadow mt-5" id="customFile1_${lastID}" onchange="displaySelectedImage(this, 'PreviewPhoto1_${lastID}')">
            </div>
        </div>
    </td>
    <td style="text-align: center;">
        <button type="button" class="btn btn-style5" data-row-index="${newRowIndex}" onclick="DeletetoolsdeviceRow1(this)">حذف</button>
    </td>
`;

        // Append the new الخطوة1 and الخطوة2 row to the table body
        tableBody.appendChild(newRow);
        clickCount++;
        console.log("newCell:", newRow); // Debugging log 
    }
}



//زر الحذ في صفحة التعديل قبل الحفظ في قاعدة البيانات .
function DeletetoolsdeviceRow1(button) {
    /*var rowIndex = button.getAttribute("data-row-index");*/

    Swal.fire({
        title: 'هل أنت متأكد؟',
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'الغاء',
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'نعم!'
    }).then((result) => {
        if (result.isConfirmed) {
            var row = button.closest("tr");
            row.remove();
            Swal.fire('Deleted!', 'تم الحذف بنجاح!', 'success');
        }
    });
}
document.querySelector("#tblDeviceTools tbody").addEventListener("click", function (event) {
    if (event.target.classList.contains("data-row-index")) {
        DeleteRow1(event.target);
    }
});


//زر الحذف في صفحة الاضافة
//function DeleteRow3(button) {
//    var tableBody = document.querySelector("#tblSteps tbody");
//    var rows = tableBody.children;
//    var rowIndex = button.getAttribute("data-row-index");

//    // Check if the button click corresponds to the last row
//    if (rowIndex == rows.length - 1) {
//        Swal.fire({
//            title: 'هل أنت متأكد؟',
//            icon: 'warning',
//            showCancelButton: true,
//            cancelButtonText: 'الغاء',
//            confirmButtonColor: '#d33',
//            cancelButtonColor: '#3085d6',
//            confirmButtonText: 'نعم!'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                // Remove the last row from the table
//                tableBody.removeChild(rows[rowIndex]);

//                // Display a success message
//                Swal.fire({
//                    icon: 'success',
//                    title: 'تم الحذف بنجاح',

//                });
//            }
//        });
//    } else {
//        // Display a message that you cannot delete rows until they are saved in the database
//        Swal.fire({
//            icon: 'error',
//            title: 'أنت قادر على حذف الصف الأخير فقط ',
//            text: 'يجب حفظ التغييرات أولا والعودة الى صفحة التعديل لحذف الصف.',
//        });
//    }
//}


//function DeleteRow(button) {
//    var rowIndex = button.getAttribute("data-row-index");
//    var tableBody = document.querySelector("#tblSteps tbody");
//    var rows = tableBody.children;

//    // Find the row to delete
//    var rowToDelete = rows[rowIndex];

//    // Determine the رقم_الخطوة values for الخطوة1 and الخطوة2 in the row to delete
//    var رقم_الخطوة1ToDelete = parseInt(rowToDelete.querySelector(`input[name^="stepsVM[${rowIndex}].رقم_الخطوة1"]`).value);
//    var رقم_الخطوة2ToDelete = parseInt(rowToDelete.querySelector(`input[name^="stepsVM[${rowIndex}].رقم_الخطوة2"]`).value);

//    // Delete the row
//    rowToDelete.remove();

//    // Update the رقم_الخطوة values for الخطوة1 and الخطوة2 in the following rows
//    for (var i = rowIndex++ ; i < rows.length; i++) {
//        var رقم_الخطوة1Input = rows[i].querySelector(`input[name^="stepsVM[${i}].رقم_الخطوة1"]`);
//        var رقم_الخطوة2Input = rows[i].querySelector(`input[name^="stepsVM[${i}].رقم_الخطوة2"]`);

//        if (رقم_الخطوة1Input && رقم_الخطوة2Input) {
//            رقم_الخطوة1Input.value = رقم_الخطوة1ToDelete;
//            رقم_الخطوة2Input.value = رقم_الخطوة2ToDelete;

//            // Update any displayed رقم_الخطوة values in the row
//            rows[i].querySelector(`div[data-رقم_الخطوة1="${i}"]`).textContent = رقم_الخطوة1ToDelete;
//            rows[i].querySelector(`div[data-رقم_الخطوة2="${i}"]`).textContent = رقم_الخطوة2ToDelete;
//        }
//    }
//}

//function DeleteRow(button) {
//    var rowIndex = button.getAttribute("data-row-index");
//    var tableBody = document.querySelector("#tblSteps tbody");
//    var rows = tableBody.children;

//    // Find the row to delete
//    var rowToDelete = rows[rowIndex];

//    // Determine the رقم_الخطوة values for الخطوة1 and الخطوة2 in the row to delete
//    var lastToDelete1 = parseInt(rowToDelete.querySelector(`input[name^="stepsVM[${rowIndex}].رقم_الخطوة1"]`).value);
//    var lastToDelete2 = parseInt(rowToDelete.querySelector(`input[name^="stepsVM[${rowIndex}].رقم_الخطوة2"]`).value);

//    // Delete the row
//    rowToDelete.remove();

//    // Decrement the رقم_الخطوة values for الخطوة1 and الخطوة2 in the following rows
//    for (var i = rowIndex; i < rows.length; i++) {
//        var رقم_الخطوة1Input = rows[i].querySelector(`input[name^="stepsVM[${i}].رقم_الخطوة1"]`);
//        var رقم_الخطوة2Input = rows[i].querySelector(`input[name^="stepsVM[${i}].رقم_الخطوة2"]`);

//        if (رقم_الخطوة1Input && رقم_الخطوة2Input) {
//            var رقم_الخطوة1 = parseInt(رقم_الخطوة1Input.value);
//            var رقم_الخطوة2 = parseInt(رقم_الخطوة2Input.value);

//            رقم_الخطوة1 -= 2;
//            رقم_الخطوة2 -= 2;

//            رقم_الخطوة1Input.value = رقم_الخطوة1;
//            رقم_الخطوة2Input.value = رقم_الخطوة2;

//            // Update any displayed رقم_الخطوة values in the row
//            rows[i].querySelector(`div[data-رقم_الخطوة1="${i}"]`).textContent = رقم_الخطوة1;
//            rows[i].querySelector(`div[data-رقم_الخطوة2="${i}"]`).textContent = رقم_الخطوة2;
//        }
//    }
//}

//function deletestep1(رقم_الخطوة1) {
//    const tdToDelete = document.querySelector(`td[data-id="${رقم_الخطوة1}"]`);
//    if (tdToDelete) {
//        const tableRow = tdToDelete.parentElement; // Get the parent <tr> element

//        // Remove the specific <td> element
//        tdToDelete.remove();


//    }
//}

//function deletestep1(رقم_الخطوة) {
//    // Find the TD to delete
//    const tdToDelete = document.querySelector(`td[data-id="${رقم_الخطوة}"]`);
//    if (tdToDelete) {
//        const currentRow = tdToDelete.parentElement;
//        const tableBody = currentRow.parentElement;
//        const currentIndex = Array.from(tableBody.children).indexOf(currentRow);

//        // Find the next row
//        const nextRow = tableBody.children[currentIndex + 1];
//        if (nextRow) {
//            const رقم_الخطوة2Cell = nextRow.querySelector(`td[data-id="${رقم_الخطوة}_2"]`);

//            if (رقم_الخطوة2Cell) {
//                // Move content from رقم_الخطوة2 to رقم_الخطوة1 in the next row
//                const رقم_الخطوة1Cell = nextRow.querySelector(`td[data-id="${رقم_الخطوة}_1"]`);
//                if (رقم_الخطوة1Cell) {
//                    رقم_الخطوة1Cell.innerHTML = رقم_الخطوة2Cell.innerHTML;
//                }

//                // Remove the deleted TD
//                tdToDelete.remove();
//            }
//        }
//    }
//}
