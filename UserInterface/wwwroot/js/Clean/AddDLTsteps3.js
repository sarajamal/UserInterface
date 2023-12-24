function AddnewRowstepsUpdate3(preparationId) { //صفحة التعديل

    var tableBody = document.querySelector("#tblSteps3 tbody");
    var newRowIndex = tableBody.children.length;


    var lastStep1 = tableBody.lastElementChild.querySelector(`input[name^="stepsVM3[${newRowIndex - 1}].رقم_الخطوة1"]`);
    var lastStep2 = tableBody.lastElementChild.querySelector(`input[name^="stepsVM3[${newRowIndex - 1}].رقم_الخطوة2"]`);

    // because I trying to parse the value of these elements, but if they don't exist, they will be null, and I cannot parse null as an integer.
    var currentValueStep1 = lastStep1 ? parseInt(lastStep1.value) : 0;
    var currentValueStep2 = lastStep2 ? parseInt(lastStep2.value) : 0;

    if (currentValueStep2 == 0) {
        // Create a red text message
        var textElement = document.createElement("span");
        textElement.style.color = "red";
        textElement.textContent = "يجب تعبئة خانة الخطوة.*";
        textElement.classList.add("red-message"); // Add the class "red-message"

        var newCurrentValueStep2 = currentValueStep1 + 1;
        var tdElement = document.createElement("td");

        // Assuming NewcurrentValueStep1 is the ID of the element

        // Create the HTML for الخطوة2 and set it as the content of the <td> element
        tdElement.innerHTML = `
        <input type="hidden" name="stepsVM3[${newRowIndex - 1}].رقم_الخطوة2" value="${newCurrentValueStep2}" />
        <input type="hidden" name="stepsVM3[${newRowIndex - 1}].الصورة2" value="data-image-for=رقم_الخطوة2_${newRowIndex - 1}" />

        <div class="row">
            <div class="col-12 text-center">
                <div>${newCurrentValueStep2}</div>

                <div>
                    <img id="PreviewPhoto2_${newCurrentValueStep2}" document.getElementById("PreviewPhoto2_${newCurrentValueStep2}") src="/images/noImage.jpg" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                </div>

                <div class="form-group mt-2">
                    <input type="file" name="file2_${newCurrentValueStep2}" class="border-0 shadow mt-5" id="customFile2_${newCurrentValueStep2}" data-preview-id="PreviewPhoto2_${newCurrentValueStep2}" onchange="displaySelectedImage(this, 'PreviewPhoto2_${newCurrentValueStep2}')">
                   <textarea asp-for="itemStep.الخطوة2" id="stepsVM3_${newCurrentValueStep2}" class="form-control mt-2 @Html.ValidationClassFor(model => model.itemStep.الخطوة2)" name="stepsVM3[${newRowIndex - 1}].الخطوة2" oninput="toggleAddButtonVisibility(this.value)"></textarea>
                     ${textElement.outerHTML}
                </div>
            </div>
        </div>
    `;
        var targetPosition = 1; // Change to 2 for the third position
        // Append the <td> element to the <tr> element
        var lastRow = tableBody.lastElementChild;
        lastRow.insertBefore(tdElement, lastRow.children[targetPosition]);

        document.getElementById("addStepButton3").disabled = true;
    }
    else {
        var NewcurrentValueStep1 = currentValueStep2 + 1
        var NewVaueStep2 = currentValueStep2 + 2

        // Create a red text message
        var textElement = document.createElement("span");
        textElement.style.color = "red";
        textElement.textContent = "يجب تعبئة خانة الخطوة.*";
        textElement.classList.add("red-message"); // Add the class "red-message"


        // Add الخطوة1 row
        var newRow = document.createElement("tr");
        newRow.innerHTML = `
    <input type="hidden" name="stepsVM3[${newRowIndex}].ID_Tandeef1" value="${preparationId}" />
    <input type="hidden" name="stepsVM3[${newRowIndex}].رقم_الخطوة1" value="${NewcurrentValueStep1}" />
    <input type="hidden" name="stepsVM3[${newRowIndex}].الصورة1" value="data-image-for=رقم_الخطوة1_${newRowIndex}" />

    <div class="row">
        <div class="col-12 text-center">
            <div>${NewcurrentValueStep1}</div>
            <div>
                <img id="PreviewPhoto1_${NewcurrentValueStep1}" document.getElementById("PreviewPhoto1_${NewcurrentValueStep1}") src="/images/noImage.jpg" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
            </div>
            <div class="form-group mt-2">
                <input type="file" name="file1_${NewcurrentValueStep1}" class="border-0 shadow mt-5" id="customFile1_${NewcurrentValueStep1}" data-preview-id="PreviewPhoto1_${NewcurrentValueStep1}" onchange="displaySelectedImage(this, 'PreviewPhoto1_${NewcurrentValueStep1}')">
                <textarea asp-for="itemStep.الخطوة1" id="stepsVM_${NewcurrentValueStep1}" class="form-control mt-2 @Html.ValidationClassFor(model => model.itemStep.الخطوة1)" name="stepsVM[${newRowIndex}].الخطوة1"></textarea>
               ${textElement.outerHTML} <!-- Append the red text here -->
            </div>

             <td style="text-align: center;">

                 <input type="hidden" name="stepsVM3[${newRowIndex}].رقم_الخطوة2" value="${NewVaueStep2}" />
                 <input type="hidden" name="stepsVM3[${newRowIndex}].الصورة2" value="data-image-for=رقم_الخطوة2_${newRowIndex}"/>

                <div>${NewVaueStep2}</div>
                <div>
                    <img id="PreviewPhoto2_${NewVaueStep2}" src="/images/noImage.jpg" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                </div>
                <div class="form-group mt-2">
                    <input type="file" name="file2_${NewVaueStep2}" class="border-0 shadow mt-5" id="customFile2_${NewVaueStep2}" data-preview-id="PreviewPhoto2_${NewVaueStep2}" onchange="displaySelectedImage(this, 'PreviewPhoto2_${NewVaueStep2}')">
                    <textarea asp-for="itemStep.الخطوة2" id="stepsVM3_${NewVaueStep2}" class="form-control mt-2" name="stepsVM3[${newRowIndex}].الخطوة2"oninput="toggleAddButtonVisibility(this.value)" ></textarea>
                     ${textElement.outerHTML}
                </div>
            </div>
        </div>
        </td>
        </div>
    </div>

    <td style="text-align: center;">
        <button type="button" class="btn btn-danger"  data-row-index="${newRowIndex}" onclick="DeleteRow10(this)" >حذف</button>
    </td>
    `;

        // Append the new الخطوة1 row to the table body
        tableBody.appendChild(newRow);
        document.getElementById("addStepButton3").disabled = true;

    }
}



//صفحة الاضافة..
var currentStep1Value = 1; // Initialize رقم_الخطوة1
var currentStep2Value = 2; // Initialize رقم_الخطوة2
function AddnewRowstepsNew3(preparationId) {
    // Find the table body element
    var tableBody = document.querySelector("#tblSteps3 tbody");

    // Find the last row index
    var newRowIndex = tableBody.children.length;

    // Calculate the values for the new row
    var newStep1Value = currentStep1Value;
    var newStep2Value = currentStep2Value;



    // Create a new row for الخطوة1 and الخطوة2 in the same row
    var newRow = document.createElement("tr");
    newRow.innerHTML = `
       
        <td style="text-align: center;">
         <input type="hidden" name="stepsVM3[${newRowIndex}].ID_Tandeef1" value="${preparationId}" />
         <input type="hidden" name="stepsVM3[${newRowIndex}].رقم_الخطوة1" value="${newStep1Value}" />
         <input type="hidden" name="stepsVM3[${newRowIndex}].الصورة1" value="data-image-for=رقم_الخطوة1_${newRowIndex}" />

        <div class="row">
            <div class="col-12 text-center">
                <div>${newStep1Value}</div>
                <div>
                    <img id="PreviewPhoto1_${newStep1Value}"  src="/images/noImage.jpg" document.getElementById("PreviewPhoto1_${newStep1Value}") alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                </div>
                <div class="form-group mt-2">
                    <input type="file" name="file1_${newStep1Value}" class="border-0 shadow mt-5" id="customFile1_${newStep1Value}" data-preview-id="PreviewPhoto1_${newStep1Value}" onchange="displaySelectedImage(this, 'PreviewPhoto1_${newStep1Value}')">
                    <textarea asp-for="itemStep.الخطوة1" id="stepsVM3_${newStep1Value}" class="form-control mt-2 " name="stepsVM3[${newRowIndex}].الخطوة1"></textarea>
                </div>
                 </td>
                 <td style="text-align: center;">

                 <input type="hidden" name="stepsVM3[${newRowIndex}].رقم_الخطوة2" value="${newStep2Value}" />
                 <input type="hidden" name="stepsVM3[${newRowIndex}].الصورة2" value="data-image-for=رقم_الخطوة2_${newRowIndex}"/>

                <div>${newStep2Value}</div>
                <div>
                    <img id="PreviewPhoto2_${newStep2Value}" src="/images/noImage.jpg" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                </div>
                <div class="form-group mt-2">
                    <input type="file" name="file2_${newStep2Value}" class="border-0 shadow mt-5" id="customFile2_${newStep2Value}" data-preview-id="PreviewPhoto2_${newStep2Value}" onchange="displaySelectedImage(this, 'PreviewPhoto2_${newStep2Value}')">
                    <textarea asp-for="itemStep.الخطوة2" id="stepsVM3_${newStep2Value}" class="form-control mt-2" name="stepsVM3[${newRowIndex}].الخطوة2" ></textarea>
                </div>
            </div>
        </div>
        </td>
       
    `;

    // Append the new الخطوة1 and الخطوة2 row to the table body
    tableBody.appendChild(newRow);

    // Increment رقم_الخطوة1 and رقم_الخطوة2 for the next row
    currentStep1Value = currentStep1Value + 2;
    currentStep2Value = currentStep2Value + 2;


    //// Disable the add button
    //document.getElementById("addStepButton").disabled = true;
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
