//صفحة التعديل 
function Delete(id) {
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
                url: '/Preparation/Delete/' + id, // Use the provided ID parameter
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

function AddRowcomponentUpdate() { //صفحة التعديل . 
    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");// This line creates a new <tr> (table row) element, which will represent the new row that you're adding.

    var newRowNumber = tableBody.children.length; //This line calculates the index or position at which the new row will be inserted.
    // It's based on the number of existing rows in the table body.

    var PreparationsFK = document.querySelector("#tbComponant").getAttribute("data-preparation-id");//This line retrieves the data-preparation-id attribute value from the table element.
    // This attribute likely holds an identifier associated with the preparation.

    newRow.innerHTML = `
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepIngredientsName" /></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepQuantity" /></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].PrepUnit" /></td>
        

        <td style="text-align:center;">
        <input type="hidden" name="componontVMList[${newRowNumber}].PreparationsFK" value="${PreparationsFK}" />
        <button type="button" class="btn btn-danger" data-row-index="${newRowNumber}" onclick="DeleteRow1(this)">حذف</button>
        </td>
            `;
    tableBody.appendChild(newRow);
    newRowNumber++;
}
// صفحة التعديل قبل الحفظ في قاعدة البيانات 
function DeleteRow1(button) {
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
document.querySelector("#tbComponant tbody").addEventListener("click", function (event) {
    if (event.target.classList.contains("data-row-index")) {
        DeleteRow1(event.target);
    }
});
//صفحة الإضافة 
var newIndex = 0;
function AddRowcomponentnew() {

    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");

    var PreparationsFK = document.querySelector("#tbComponant").getAttribute("data-preparation-id");


    newRow.innerHTML = `
         <td><input  class="form-control" name="componontVMList[${newIndex}].PrepIngredientsName" /></td>
         <td><input class="form-control" name="componontVMList[${newIndex}].PrepQuantity"/></td>
        <td><input  class="form-control" name="componontVMList[${newIndex}].PrepUnit" /></td>
        

    <td style="text-align:center;">
    <input type="hidden" name="componontVMList[${newIndex}].PreparationsFK" value="${PreparationsFK}" />
    <button type="button" class="btn btn-danger" data-row-index="${newIndex}" onclick="DeleteRow1(this)">حذف</button>
    </td>
`;

    tableBody.appendChild(newRow);
    newIndex++;
}


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