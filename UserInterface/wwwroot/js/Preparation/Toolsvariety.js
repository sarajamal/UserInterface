
function DeleteToolVariety(id) { //هذي فقط للعرض البرمجة في controller , هذي للحذف بعد الحفظ في قاعدة البيانات . 
    Swal.fire({
        title: 'تأكيد !!',
        text: "تأكد من حفظ الصفوف المضافة في الادوات قبل الحذف ",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'الغاء',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'حذف '
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Preparation/DeleteToolVariety/' + id, // Use the provided ID parameter
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


function AddRowTool(PreparationsFK) { //صفحة التعديل 
    var tableBody = document.querySelector("#tblToolVarity tbody");
    var newRowNumber = tableBody.children.length + 1;

    var newRow = document.createElement("tr");
    newRow.innerHTML = `
        <td>${newRowNumber}</td>

        <td><input name="ToolsVarityVM[${newRowNumber - 1}].PrepTools" class="form-control"placeholder="الأدات المستخدمة"  /></td>

       <td style="text-align:center;" >
            <input type="hidden" name="ToolsVarityVM[${newRowNumber - 1}].PreparationsFK" value="${PreparationsFK}"   />
            <button type="button" class="btn btn-style5" data-row-index="${newRowNumber - 1}" onclick="DeleteRow6(this)">حذف</button>
        </td>
         `;
    tableBody.appendChild(newRow);
}

function DeleteRow6(button) { // AJAX قبل تحفظ في قاعدة البيانات ماتحتاج controller 

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
            var tableBody = document.querySelector("#tblToolVarity tbody");
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


var newRowNumber =1;
var numeric = 2;
function AddRowToolnew(PreparationsFK) { //صفحة الإضافة
    var tableBody = document.querySelector("#tblToolVarity tbody");

    var newRow = document.createElement("tr");
    newRow.innerHTML = `

        <td style="text-align:center;">${numeric}</td>
        <td><input name="ToolsVarityVM[${newRowNumber-1 }].PrepTools" class="form-control"  placeholder="الأدات المستخدمة" /></td>
       <td style="text-align:center;" >
            <input type="hidden" name="ToolsVarityVM[${newRowNumber-1 }].PreparationsFK" value="${PreparationsFK}"  />
              
            <button type="button" class="btn btn-style5" data-row-index="${newRowNumber }" onclick="DeleteRow6(this)">حذف</button> 
        </td>
         `;

    tableBody.appendChild(newRow);
    newRowNumber++;
    numeric++;
}

function DeleteRow66(button) { // AJAX قبل تحفظ في قاعدة البيانات ماتحتاج controller 

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
            var tableBody = document.querySelector("#tblToolVarity tbody");
            button.closest("tr").remove();

            // Decrement newRowNumber when a row is deleted
            newRowNumber--;
            // Update the numeric value starting from 2 for each row
            numeric = 2;

            // Update the displayed numeric values and indices in input fields and buttons
            Array.from(tableBody.children).forEach((row, index) => {
                // Update the displayed row number
                row.cells[0].textContent = numeric++;

                // Update indices in input fields and buttons
                var inputsAndButtons = row.querySelectorAll("input[name^='ToolsVarityVM'], button[data-row-index]");
                inputsAndButtons.forEach(el => {
                    if (el.name && el.name.startsWith('ToolsVarityVM')) {
                        el.name = el.name.replace(/\[\d+\]/, `[${index}]`);
                    }
                    if (el.getAttribute("data-row-index") !== null) {
                        el.setAttribute("data-row-index", index);
                    }
                });
            });

            Swal.fire('تم الحذف!', 'تم الحذف بنجاح!', 'success');
        }
    });
}

//function DeleteRow6(button) { // AJAX قبل تحفظ في قاعدة البيانات ماتحتاج controller

//    Swal.fire({
//        title: 'هل أنت متأكد؟',
//        icon: 'warning',
//        showCancelButton: true,
//        cancelButtonText: 'الغاء',
//        confirmButtonColor: '#d33',
//        cancelButtonColor: '#3085d6',
//        confirmButtonText: 'نعم!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            var tableBody = document.querySelector("#tblToolVarity tbody");
//            var rows = tableBody.children;
//            button.closest("tr").remove();

//            // Update row numbers
//            for (var i = 0; i < rows.length; i++) {
//                rows[i].cells[0].textContent = i + 1;
//            }

//            Swal.fire('تم الحذف!', 'تم الحذف بنجاح!', 'success');
//        }
//    });
//}
//document.querySelector("#tblToolVarity tbody").addEventListener("click", function (event) {
//    if (event.target.classList.contains("data-row-index")) {
//        DeleteRow3(event.target);
//    }
//});
