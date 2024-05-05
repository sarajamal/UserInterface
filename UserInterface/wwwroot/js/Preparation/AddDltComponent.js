//صفحة التعديل 
function Delete(id) {
    Swal.fire({
        title: 'تأكد !! ',
        text: "تأكد أولا من حفظ أي مكونات قمت بإضافتها قبل الحذف ",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'الغاء',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'حذف '
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Preparation/Delete/' + id, // Use the provided ID parameter
                /*type: 'DELETE',*/
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'تم الحذف بنجاح',
                            text: data.message
                        }).then(() => {
                            window.location.href = data.redirectToUrl; // Perform the redirection
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
//صفحة الإضافة 
function Deletec2(id) {
    Swal.fire({
        title: 'تأكد !! ',
        text: "تأكد أولا من حفظ أي مكونات قمت بإضافتها قبل الحذف ",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'الغاء',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'حذف '
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Preparation/Deletec2/' + id, // Use the provided ID parameter
                /*type: 'DELETE',*/
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'تم الحذف بنجاح',
                            text: data.message
                        }).then(() => {
                            window.location.href = data.redirectToUrl; // Perform the redirection
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
//صفحة التعديل
function AddRowcomponentUpdate() {
    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");

    var newRowNumber = tableBody.children.length;

    var PreparationsFK = document.querySelector("#tbComponant").getAttribute("data-preparation-id");//This line retrieves the data-preparation-id attribute value from the table element.
    // This attribute likely holds an identifier associated with the preparation.

    newRow.innerHTML = `
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepIngredientsName" placeholder="المكون" /></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepQuantity" placeholder="الكمية"/></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepUnit" placeholder="الوحدة"/></td>
        

        <td style="text-align:center;">
        <input type="hidden" name="componontVMList[${newRowNumber}].PreparationsFK" value="${PreparationsFK}" />
        <button type="button" class="btn btn-style5" data-row-index="${newRowNumber }" onclick="DeleteRow16(this)">حذف</button>
        </td>
            `;
    tableBody.appendChild(newRow);
    console.log("Row deleted, new row index:", newRowNumber); // Adjusted the debugging log statement
    newRowNumber++;
}

//صفحة التعديل قبل الحفظ في قاعدة البيانات
function DeleteRow16(button) {
    var rowIndex = parseInt(button.getAttribute("data-row-index"));

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
            updateRowIndicesAfterDeletion16(rowIndex);
        }
    });
}

function updateRowIndicesAfterDeletion16(deletedIndex) {
    console.log("Row deleted, deletedIndex", deletedIndex);

    var tableBody = document.querySelector("#tbComponant tbody");
    var rows = tableBody.querySelectorAll("tr");

    // Since rows is a live NodeList, indices are always 0-based and contiguous.
    rows.forEach((row, index) => {
        // Adjust the index for all rows following the deleted one.
        var actualIndex = index;
        if (index > deletedIndex) {
            actualIndex = index ; // Decrease the index for rows after the deleted one
        }
        // Update the names and data-row-index for all inputs and buttons
        var inputsAndButtons = row.querySelectorAll("input[name*='componontVMList'], button[data-row-index]");
        inputsAndButtons.forEach(el => {
            var name = el.name;
            if (name) {
                // Replace only the first occurrence of the pattern to avoid affecting nested indices
                el.name = name.replace(/\[\d+\]/, `[${actualIndex}]`);
            }
            if (el.hasAttribute("data-row-index")) {
                el.setAttribute("data-row-index", actualIndex);
            }
        });
        console.log("Row updated to new index:", actualIndex);
    });
}

// صفحة الإضافة قبل الحفظ في قاعدة البيانات
function DeleteRow1(button) {
    var rowIndex = parseInt(button.getAttribute("data-row-index"));

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
            updateRowIndicesAfterDeletion1(rowIndex);
        }
    });
}

function updateRowIndicesAfterDeletion1(deletedIndex) {
    var tableBody = document.querySelector("#tbComponant tbody");
    var rows = tableBody.querySelectorAll("tr");

    rows.forEach((row, index) => {
        if (index > deletedIndex) { // تحديث فقط للصفوف بعد الصف المحذوف
            var newRowIndex = index - 1; // تقليل index بواحد
            var inputsAndButtons = row.querySelectorAll("input, button");

            inputsAndButtons.forEach(el => {
                if (el.name) {
                    el.name = el.name.replace(/\[\d+\]/, `[${newRowIndex}]`);
                }
                if (el.getAttribute("data-row-index") !== null) {
                    el.setAttribute("data-row-index", newRowIndex);
                    console.log("Row deleted, new row index:", newRowIndex); // Adjusted the debugging log statement
                }
            });
        }
    });

    newIndex = rows.length; // تحديث newIndex بناءً على عدد الصفوف المتبقية
}

//صفحة الإضافة 
function AddRowcomponentnew() {

    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");

    var PreparationsFK = document.querySelector("#tbComponant").getAttribute("data-preparation-id");
    // newRowNumber should be the next index which is the current length of the tableBody children.
    var newRowNumber = tableBody.children.length;

    newRow.innerHTML = `
         <td><input  class="form-control" name="componontVMList[${newRowNumber-1}].PrepIngredientsName" placeholder="المكون"/></td>
         <td><input class="form-control" name="componontVMList[${newRowNumber-1}].PrepQuantity"placeholder="الكمية"/></td>
        <td><input  class="form-control" name="componontVMList[${newRowNumber-1}].PrepUnit" placeholder="الوحدة"/></td>
        

    <td style="text-align:center;">
    <input type="hidden" name="componontVMList[${newRowNumber-1}].PreparationsFK" value="${PreparationsFK}" />
    <button type="button" class="btn btn-style5" data-row-index="${newRowNumber-1}" onclick="DeleteRow1(this)">حذف</button>
    </td>
`;

    tableBody.appendChild(newRow);
}

function AddRowcomponentnew22() {

    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");

    var PreparationsFK = document.querySelector("#tbComponant").getAttribute("data-preparation-id");
    // newRowNumber should be the next index which is the current length of the tableBody children.
    var newRowNumber = tableBody.children.length;

    newRow.innerHTML = `
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepIngredientsName" placeholder="المكون" /></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepQuantity" placeholder="الكمية"/></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepUnit" placeholder="الوحدة"/></td>
        

   <td style="text-align:center;">
        <input type="hidden" name="componontVMList[${newRowNumber}].PreparationsFK" value="${PreparationsFK}" />
        <button type="button" class="btn btn-style5" data-row-index="${newRowNumber}" onclick="DeleteRow16(this)">حذف</button>
        </td>
`;

    tableBody.appendChild(newRow);
}
//يجب إضافة خطوة واحدة على الأقل 
function validateAndSubmit() {
    var stepCount = document.querySelectorAll("#tblSteps tbody td").length;
    var customFileInput = document.getElementById("customFile");
    var fileErrorMessage = document.getElementById("errorMessage");
    var stepErrorMessage = document.getElementById("redMessage");

    // Check if the file is selected
    if (customFileInput.files.length === 0) {
        fileErrorMessage.style.display = "block";
        scrollToElement(customFileInput);
        return; // Stop the function execution
    } else {
        fileErrorMessage.style.display = "none";
    }

    // Check if at least one step is added
    if (stepCount === 1) {
        stepErrorMessage.style.display = "block";
        scrollToElement(document.getElementById("addStepButton1"));
        return; // Stop the function execution
    } else {
        stepErrorMessage.style.display = "none";
    }

    // If all validations pass, submit the form
    document.getElementById("MyForm").submit();
}

function scrollToElement(element) {
    var elementOffset = element.getBoundingClientRect().top + window.scrollY;
    window.scrollTo({
        top: elementOffset,
        behavior: "smooth"
    });
}

 //بعد الضغط على tab فيصفحة information . 
document.addEventListener('DOMContentLoaded', (event) => {
    var tabElements = document.querySelectorAll('a[data-action="tab"]');

    tabElements.forEach(function (tab) {
        tab.addEventListener('click', function (e) {
            e.preventDefault();
            var href = this.getAttribute('href'); // Using href instead of data-action-url
            if (href) {
                window.location.href = href;
            }
        });
    });
});