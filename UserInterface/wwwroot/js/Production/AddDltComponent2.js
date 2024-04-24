//صفحة التعديل 
function Delete(id) {
    Swal.fire({
        title: 'تأكد !! ؟',
        text: " قبل الحذف تأكد من حفظ الصفوف المضافة في المكونات",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'الغاء',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'حذف '
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Production/Delete/' + id, // Use the provided ID parameter
                //type: 'DELETE',
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

function AddRowcomponentUpdate2() { //صفحة التعديل . 
    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");// This line creates a new <tr> (table row) element, which will represent the new row that you're adding.

    var newRowNumber = tableBody.children.length; //This line calculates the index or position at which the new row will be inserted.
    // It's based on the number of existing rows in the table body.

    var componentFk = document.querySelector("#tbComponant").getAttribute("data-Production-id");//This line retrieves the data-preparation-id attribute value from the table element.
    // This attribute likely holds an identifier associated with the preparation.

    newRow.innerHTML = `
        <td><input class="form-control" name="componontVMList2[${newRowNumber}].ProdIngredientsName" placeholder="المكون"/></td>
        <td><input class="form-control" name="componontVMList2[${newRowNumber}].ProdQuantity" placeholder="الكمية" /></td>
        <td><input class="form-control" name="componontVMList2[${newRowNumber}].ProdUnit" placeholder="الوحدة"/></td>
        

        <td style="text-align:center;">
        <input type="hidden" name="componontVMList2[${newRowNumber}].ProductionFK" value="${componentFk}" />
        <button type="button" class="btn btn-style5" data-row-index="${newRowNumber}" onclick="DeleteRow91(this)">حذف</button>
        </td>
            `;
    tableBody.appendChild(newRow);
    newRowNumber++;
}

//زر الحذف في صفحة التعديل قبل الحفظ في قاعدة البيانات
function DeleteRow91(button) {
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
            updateRowIndicesAfterDeletion91(rowIndex);
        }
    });
}
function updateRowIndicesAfterDeletion91(deletedIndex) {
    console.log("Row deleted, deletedIndex", deletedIndex);

    var tableBody = document.querySelector("#tbComponant tbody");
    var rows = tableBody.querySelectorAll("tr");

    // Since rows is a live NodeList, indices are always 0-based and contiguous.
    rows.forEach((row, index) => {
        // Adjust the index for all rows following the deleted one.
        var actualIndex = index;
        if (index > deletedIndex) {
            actualIndex = index; // Decrease the index for rows after the deleted one
        }
        // Update the names and data-row-index for all inputs and buttons
        var inputsAndButtons = row.querySelectorAll("input[name*='componontVMList2'], button[data-row-index]");
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

//زر الحذف للإضافة قبل الحفظ في قاعدة البيانات 
 function DeleteRow99(button) {
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
            updateRowIndicesAfterDeletion99(rowIndex);
        }
    });
}
function updateRowIndicesAfterDeletion99(deletedIndex) {
    var tableBody = document.querySelector("#tbComponant tbody");
    var rows = tableBody.querySelectorAll("tr");

    rows.forEach((row, index) => {
        if (index > deletedIndex) {
            var newRowIndex = index - 1;
            var inputsAndButtons = row.querySelectorAll("input, button");

            inputsAndButtons.forEach(el => {
                if (el.name) {
                    el.name = el.name.replace(/\[\d+\]/, `[${newRowIndex}]`);
                }
                if (el.getAttribute("data-row-index") !== null) {
                    el.setAttribute("data-row-index", newRowIndex);
                }
            });
        }
    });

    newIndex = rows.length;
}

//صفحة الإضافة 
var newIndex = 0;
function AddRowcomponentnew() {

    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");

    var componentFk = document.querySelector("#tbComponant").getAttribute("data-Production-id");


    newRow.innerHTML = `
         <td><input  class="form-control" name="componontVMList2[${newIndex}].ProdIngredientsName"placeholder="المكون" /></td>
         <td><input class="form-control" name="componontVMList2[${newIndex}].ProdQuantity"placeholder="الكمية"/></td>
        <td><input  class="form-control" name="componontVMList2[${newIndex}].ProdUnit" placeholder="الوحدة"/></td>
        

    <td style="text-align:center;">
    <input type="hidden" name="componontVMList[${newIndex}].ProductionFK" value="${componentFk}" />
    <button type="button" class="btn btn-style5" data-row-index="${newIndex}" onclick="DeleteRow99(this)">حذف</button>
    </td>
`;

    tableBody.appendChild(newRow);
    newIndex++;
}

function validateFormAndSubmit() {
    console.log("Submit button clicked"); // Add this line

    // Check if the product type is selected
    var productType = document.getElementById('selectType').value;
    if (!productType) {
        document.getElementById('errorMessage').style.display = 'block';
        scrollToElement(productType);
        return  ; // Prevent form submission
    } else {
        document.getElementById('errorMessage').style.display = 'none';
    }

    // Check if an image is selected
    var fileInput = document.getElementById('customFile1');
    if (fileInput.files.length === 0) {
        document.getElementById('errorMessage1').style.display = 'block';
        scrollToElement(fileInput);
        return  ; // Prevent form submission
    } else {
        document.getElementById('errorMessage1').style.display = 'none';
    }

    // Check if at least one step is added
    var stepCount = document.querySelectorAll('#tblSteps2 tbody tr').length;
    if (stepCount === 1) {
        document.getElementById('redMessage1').style.display = 'block';
        scrollToElement(document.getElementById('addStepButton2'));
        return  ; // Prevent form submission
    } else {
        document.getElementById('redMessage1').style.display = 'none';
    }
    // If all validations pass, submit the form
    document.getElementById("myForm1").submit();
}

function scrollToElement(element) {
    var elementOffset = element.getBoundingClientRect().top + window.scrollY;
    window.scrollTo({
        top: elementOffset,
        behavior: "smooth"
    });
}