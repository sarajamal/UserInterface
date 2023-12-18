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
                url: '/Production/Delete/' + id, // Use the provided ID parameter
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

function AddRowcomponentUpdate2() { //صفحة التعديل . 
    var tableBody = document.querySelector("#tbComponant tbody");
    var newRow = document.createElement("tr");// This line creates a new <tr> (table row) element, which will represent the new row that you're adding.

    var newRowNumber = tableBody.children.length; //This line calculates the index or position at which the new row will be inserted.
    // It's based on the number of existing rows in the table body.

    var componentFk = document.querySelector("#tbComponant").getAttribute("data-Production-id");//This line retrieves the data-preparation-id attribute value from the table element.
    // This attribute likely holds an identifier associated with the preparation.

    newRow.innerHTML = `
        <td><input class="form-control" name="componontVMList2[${newRowNumber}].ProdIngredientsName" /></td>
        <td><input class="form-control" name="componontVMList2[${newRowNumber}].ProdQuantity" /></td>
        <td><input class="form-control" name="componontVMList2[${newRowNumber}].ProdUnit" /></td>
        

        <td style="text-align:center;">
        <input type="hidden" name="componontVMList2[${newRowNumber}].ProductionFK" value="${componentFk}" />
        <button type="button" class="btn btn-danger" data-row-index="${newRowNumber}" onclick="DeleteRow99(this)">حذف</button>
        </td>
            `;
    tableBody.appendChild(newRow);
    newRowNumber++;
}
// صفحة التعديل قبل الحفظ في قاعدة البيانات 
function DeleteRow99(button) {
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

    var componentFk = document.querySelector("#tbComponant").getAttribute("data-Production-id");


    newRow.innerHTML = `
         <td><input  class="form-control" name="componontVMList2[${newIndex}].ProdIngredientsName" /></td>
         <td><input class="form-control" name="componontVMList2[${newIndex}].ProdQuantity"/></td>
        <td><input  class="form-control" name="componontVMList2[${newIndex}].ProdUnit" /></td>
        

    <td style="text-align:center;">
    <input type="hidden" name="componontVMList[${newIndex}].ProductionFK" value="${componentFk}" />
    <button type="button" class="btn btn-danger" data-row-index="${newIndex}" onclick="DeleteRow99(this)">حذف</button>
    </td>
`;

    tableBody.appendChild(newRow);
    newIndex++;
}
