
function DeleteToolVariety2(id) { //هذي فقط للعرض البرمجة في controller , هذي للحذف بعد الحفظ في قاعدة البيانات . 
    Swal.fire({
        title: 'تأكيد !!',
        text: " تأكد من حفظ الصفوف المضافة في الأدوات قبل الحذف ",
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
            var tableBody = document.querySelector("#tblToolVarity2 tbody");
            var rows = tableBody.children;
            var deletedRowIndex = Array.from(rows).indexOf(button.closest("tr"));
            button.closest("tr").remove();

            // Update row numbers in the first cell
            for (var i = 0; i < rows.length; i++) {
                rows[i].cells[0].textContent = i + 1;
            }

            // Update indices for elements in all rows after the deleted row
            for (var i = deletedRowIndex; i < rows.length; i++) {
                var inputsAndButtons = rows[i].querySelectorAll("input, button");
                inputsAndButtons.forEach(el => {
                    if (el.name) {
                        el.name = el.name.replace(/\[\d+\]/, `[${i}]`);
                    }
                    if (el.getAttribute("data-row-index") !== null) {
                        el.setAttribute("data-row-index", i);
                    }
                });
            }
            Swal.fire('تم الحذف!', 'تم الحذف بنجاح!', 'success');
        }
    });
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
              
            <button type="button" class="btn btn-style5" data-row-index="${newRowNumber - 1}" onclick="DeleteRow19(this)">حذف</button> 
        </td>
         `;

    tableBody.appendChild(newRow);
    newRowNumber++;
    numeric++;
}
//صفحة الإضافة 
function DeleteRow19(button) { // AJAX قبل تحفظ في قاعدة البيانات ماتحتاج controller
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
            var tableBody = document.querySelector("#tblToolVarity2 tbody");
            var rows = tableBody.children;
            button.closest("tr").remove();

            // Update row numbers
            for (var i = 0; i < rows.length; i++) {
                rows[i].cells[0].textContent = i + 1;
            }
            Swal.fire('تم الحذف!', 'تم الحذف بنجاح!', 'success');

            updateRowIndicesAfterDeletion19(rowIndex);

        }
    });
}
function updateRowIndicesAfterDeletion19(deletedIndex) {
    var tableBody = document.querySelector("#tblToolVarity2 tbody");
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