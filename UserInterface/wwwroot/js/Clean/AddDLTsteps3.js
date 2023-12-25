function AddnewRowstepsUpdate3(CleaningFK) { //صفحة التعديل

    var tableBody = document.querySelector("#tblSteps3 tbody");
    var stepCells = tableBody.querySelectorAll("td");

    newRowIndex = stepCells.length;

    var lastCell = stepCells[stepCells.length - 1];
    console.log("Step lastCell length:", lastCell); // Debugging log

    var lastStepIDInput = lastCell ? lastCell.querySelector(`input[name$="CleaStepsID"]`) : null;
    var lastStepInput = lastCell ? lastCell.querySelector(`input[name$="CleaStepsNum"]`) : null;

    var lastStepIDValue = lastStepIDInput ? parseInt(lastStepIDInput.value) : 0;
    var lastStepValue = lastStepInput ? parseInt(lastStepInput.value) : 0;

    currentStep1Value = lastStepValue + 1;
    currentIDValue = lastStepIDValue + 1;


    var isEven = currentStep1Value % 2 === 0;
    var newRow, newCell;

    // Row creation logic
    if (isEven) {
        newRow = tableBody.lastElementChild;
    } else {
        newRow = document.createElement("tr");
        tableBody.appendChild(newRow);

    }

    newCell = document.createElement("td");
    newCell.style.textAlign = "center";
    newCell.innerHTML = `
        <input type="hidden" name="CleaningSteps[${newRowIndex}].CleaningFK" value="${CleaningFK}" />
        <input type="hidden" name="CleaningSteps[${newRowIndex}].CleaStepsNum" value="${currentStep1Value}" />
        <input type="hidden" name="CleaningSteps[${newRowIndex}].CleaStepsImage"  />
         <div class="row">
            <div class="col-12 text-center">
                <div>${currentStep1Value}</div>
                <div>
                    <img id="PreviewPhoto1_${currentIDValue}" src="/IMAGES/noImage.png" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                </div>
                <div class="form-group mt-2">
                    <input type="file" name="file1_${currentIDValue}" class="border-0 shadow mt-5" id="customFile1_${currentIDValue}" data-preview-id="PreviewPhoto1_${currentIDValue}" onchange="displaySelectedImage(this, 'PreviewPhoto1_${currentIDValue}')">
                    <textarea class="form-control mt-2" id="CleaningSteps_${newRowIndex}" name="CleaningSteps[${newRowIndex}].CleaText"></textarea>
                </div>
                <div class="py-5"></div>
            </div>
        </div>
    `;
    // Append the new cell to the current or new row
    newRow.appendChild(newCell);
    console.log("newCell:", newCell); // Debugging log   

}
   


//صفحة الاضافة..
var currentStep1Value = 1;
var clickCount = 0;
var lastID = 0; // Initialize lastID globally
function AddnewRowstepsNew3(CleanFK) {
    if (clickCount === 0) {
        // Only retrieve lastID from server on the first click
        $.ajax({
            url: '/Clean/GetLastId',
            type: 'GET',
            success: function (response) {
                lastID = parseInt(response) + 1;
                addStep(CleanFK);
            },
            error: function (xhr, status, error) {
                console.error('Error fetching last ID:', error);
            }
        });
    } else {
        // On subsequent clicks, increment lastID locally
        lastID++;
        addStep(CleanFK);
    }
}
function addStep(CleanFK) {
    // Find the table body element
    var tableBody = document.querySelector("#tblSteps3 tbody");

    // Determine if adding to an existing row or creating a new row
    var newRow;
    if (clickCount % 2 === 0) { // Every two clicks, start a new row
        newRow = document.createElement("tr");
        tableBody.appendChild(newRow);
    } else {
        // Get the last row in the table to append a new <td>
        newRow = tableBody.lastElementChild;
    }

    // Create a new <td> for the current step
    var newCell = document.createElement("td");
    newCell.style.textAlign = "center";
    newCell.innerHTML = `
         <input type="hidden" name="CleaningSteps[${clickCount}].BrandFK" value="${CleanFK}" />
         <input type="hidden" name="CleaningSteps[${clickCount}].CleaStepsImage" />
         <input type="hidden" name="CleaningSteps[${clickCount}].CleaStepsNum" value="${currentStep1Value}" />

         <div class="row">
            <div class="col-12 text-center">
                <div>${currentStep1Value}</div>
                <div>
                    <img id="PreviewPhoto1_${lastID}" src="/IMAGES/noImage.png" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                </div>
                <div class="form-group mt-2">
                    <input type="file" name="file1_${lastID}" class="border-0 shadow mt-5" id="customFile1_${lastID}" data-preview-id="PreviewPhoto1_${lastID}" onchange="displaySelectedImage(this, 'PreviewPhoto1_${lastID}')">
                    <textarea class="form-control mt-2" id="CleaningSteps_${clickCount}" name="CleaningSteps[${clickCount}].CleaText"></textarea>
                </div>
            </div>
        </div>
    `;

    // Append the new <td> to the current/new row
    newRow.appendChild(newCell);

    // Increment values for next click
    currentStep1Value++;
    clickCount++;
    console.log("newCell:", newCell); // Debugging log
}

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

    var addButton = document.getElementById("addStepButton3");
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

//take two parameter ID1 = ID_التحضير , id=ID for the step . 
function Deletestep3(id) { // after save in db . 
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
            var formData = new FormData();
            formData.append("id", id);
            $.ajax({
                url:'/Clean/Deletestep3',
                type: 'DELETE',
                data: formData,
                processData: false,
                contentType: false,
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
    });
}

//زر الحذف في صفحة التعديل .
function DeleteRow10(button) {
    var tableBody = document.querySelector("#tblSteps3 tbody");
    var rows = tableBody.children;
    var rowIndex = button.getAttribute("data-row-index");

    // Check if the button click corresponds to the last row
    if (rowIndex == rows.length - 1) {
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
                // Remove the last row from the table
                tableBody.removeChild(rows[rowIndex]);

                // Display a success message
                Swal.fire({
                    icon: 'success',
                    title: 'تم الحذف بنجاح',

                });
            }
        });
    } else {
        // Display a message that you cannot delete rows until they are saved in the database
        Swal.fire({
            icon: 'error',
            title: 'لا يمكنك حذف هذا الصف',
            text: 'يجب حفظ التغييرات أولا والعودة الى صفحة التعديل لحذف الصف.',
        });
    }
}

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
