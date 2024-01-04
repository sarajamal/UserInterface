
function DeleteToolVariety2(id) { //هذي فقط للعرض البرمجة في controller , هذي للحذف بعد الحفظ في قاعدة البيانات . 
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
                url: '/Production/DeleteToolVariety2/' + id, // Use the provided ID parameter
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


function AddRowTool2(ProductionFK) { //صفحة التعديل 
    var tableBody = document.querySelector("#tblToolVarity2 tbody");
    var newRowNumber = tableBody.children.length + 1;

    var newRow = document.createElement("tr");
    newRow.innerHTML = `
        <td>${newRowNumber}</td>

        <td><input name="ToolsVarityVM2[${newRowNumber - 1}].ProdTools" class="form-control"  placeholder="الأدات المستخدمة" /></td>

       <td style="text-align:center;" >
            <input type="hidden" name="ToolsVarityVM2[${newRowNumber - 1}].ProductionFK" value="${ProductionFK}" />
            <button type="button" class="btn btn-style5" data-row-index="${newRowNumber}" onclick="DeleteRow9(this)">حذف</button>
        </td>
         `;
    tableBody.appendChild(newRow);
}

function DeleteRow9(button) { // AJAX قبل تحفظ في قاعدة البيانات ماتحتاج controller 
  
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
            var deletedRowIndex = parseInt(button.getAttribute("data-row-index")) - 1; // الحصول على index الصف المحذوف

            row.remove();
            Swal.fire('تم الحذف!', 'تم الحذف بنجاح!', 'success');
            updateRowIndicesAfterDeletion(deletedRowIndex);
        }
    });
}
function updateRowIndicesAfterDeletion(deletedIndex) {
    var tableBody = document.querySelector("#tblToolVarity2 tbody");
    var rows = tableBody.querySelectorAll("tr");

    rows.forEach((row, index) => {
        if (index > deletedIndex) {
            var newRowIndex = index;
            var inputs = row.querySelectorAll("input");
            inputs.forEach(input => {
                if (input.name) {
                    input.name = input.name.replace(/\[\d+\]/, `[${newRowIndex}]`);
                }
            });

            // تحديث العداد الظاهر في الجدول
            row.cells[0].textContent = newRowIndex + 1;
        }
    });

    newRowNumber = rows.length + 1;
    numeric = newRowNumber;
}

 
var newRowNumber = 1;
var numeric = 2;
function AddRowToolnew2(ProductionFK) { //صفحة الإضافة
    var tableBody = document.querySelector("#tblToolVarity2 tbody");

    var newRow = document.createElement("tr");
    newRow.innerHTML = `

        <td style="text-align:center;">${numeric}</td>
        <td><input name="ToolsVarityVM2[${newRowNumber - 1}].ProdTools" class="form-control" placeholder="الأدات المستخدمة" /></td>
       <td style="text-align:center;" >
            <input type="hidden" name="ToolsVarityVM2[${newRowNumber - 1}].ProductionFK" value="${ProductionFK}"   />
              
            <button type="button" class="btn btn-style5" data-row-index="${newRowNumber}" onclick="DeleteRow9(this)">حذف</button> 
        </td>
         `;

    tableBody.appendChild(newRow);
    newRowNumber++;
    numeric++;
}