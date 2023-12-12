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

    var preparationId = document.querySelector("#tbComponant").getAttribute("data-preparation-id");//This line retrieves the data-preparation-id attribute value from the table element.
    // This attribute likely holds an identifier associated with the preparation.

    newRow.innerHTML = `
        <td><input class="form-control" name="componontVMList[${newRowNumber}].المكون" /></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].الكمية" /></td>
        <td><input class="form-control" name="componontVMList[${newRowNumber}].الوحدة" /></td>
        

        <td style="text-align:center;">
        <input type="hidden" name="componontVMList[${newRowNumber}].ID_التحضير" value="${preparationId}" />
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

    var preparationId = document.querySelector("#tbComponant").getAttribute("data-preparation-id");


    newRow.innerHTML = `
         <td><input  class="form-control" name="componontVMList[${newIndex}].المكون" /></td>
         <td><input class="form-control" name="componontVMList[${newIndex}].الكمية"/></td>
        <td><input  class="form-control" name="componontVMList[${newIndex}].الوحدة" /></td>
        

    <td style="text-align:center;">
    <input type="hidden" name="componontVMList[${newIndex}].ID_التحضير" value="${preparationId}" />
    <button type="button" class="btn btn-danger" data-row-index="${newIndex}" onclick="DeleteRow1(this)">حذف</button>
    </td>
`;

    tableBody.appendChild(newRow);
    newIndex++;
}
