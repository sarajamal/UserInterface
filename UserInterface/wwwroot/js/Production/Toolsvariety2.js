
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
//الإضافة
function DeleteToolVariety2t1(id) { //هذي فقط للعرض البرمجة في controller , هذي للحذف بعد الحفظ في قاعدة البيانات . 
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
                url: '/Production/DeleteToolVariety2t1/' + id, // Use the provided ID parameter
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

//زر الحذف في صفحة التعديل قبل الحفظ في قاعدة البيانات
function DeleteRow9(button) { 

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

            updateRowIndicesAfterDeletion9(rowIndex);

        }
    });
}

function updateRowIndicesAfterDeletion9(deletedIndex) {
    console.log("Row deleted, deletedIndex", deletedIndex);

    var tableBody = document.querySelector("#tblToolVarity2 tbody");
    var rows = tableBody.querySelectorAll("tr");

    // Since rows is a live NodeList, indices are always 0-based and contiguous.
    rows.forEach((row, index) => {
        // Adjust the index for all rows following the deleted one.
        var actualIndex = index;
        if (index > deletedIndex) {
            actualIndex = index; // Decrease the index for rows after the deleted one
        }
        // Update the names and data-row-index for all inputs and buttons
        var inputsAndButtons = row.querySelectorAll("input[name*='ToolsVarityVM2'], button[data-row-index]");
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


var numeric = 2;
function AddRowToolnew2(ProductionFK) { //صفحة الإضافة
    var tableBody = document.querySelector("#tblToolVarity2 tbody");

    var newRow = document.createElement("tr");
    var newRowNumber = tableBody.children.length;

    newRow.innerHTML = `

        <td style="text-align:center;">${numeric}</td>
        <td><input name="ToolsVarityVM2List[${newRowNumber - 1}].ProdTools" class="form-control" placeholder="الأدات المستخدمة" /></td>
       <td style="text-align:center;" >
            <input type="hidden" name="ToolsVarityVM2List[${newRowNumber - 1}].ProductionFK" value="${ProductionFK}"   />
              
            <button type="button" class="btn btn-style5" data-row-index="${newRowNumber - 1}" onclick="DeleteRow19(this)">حذف</button> 
        </td>
         `;

    tableBody.appendChild(newRow);
    newRowNumber++;
    // Increment the numeric counter when a new row is added
    numeric = newRowNumber + 1; // Make sure numeric is now set to the next number}
}

function AddRowToolnew22(ProductionFK) { //صفحة الإضافة
    var tableBody = document.querySelector("#tblToolVarity2 tbody");

    var newRow = document.createElement("tr");
    var newRowNumber = tableBody.children.length;

    newRow.innerHTML = `

        <td style="text-align:center;">${newRowNumber+1}</td>
        <td><input name="ToolsVarityVM2List[${newRowNumber}].ProdTools" class="form-control" placeholder="الأدات المستخدمة" /></td>
       <td style="text-align:center;" >
            <input type="hidden" name="ToolsVarityVM2List[${newRowNumber}].ProductionFK" value="${ProductionFK}"   />
              
            <button type="button" class="btn btn-style5" data-row-index="${newRowNumber}" onclick="DeleteRow19(this)">حذف</button> 
        </td>
         `;

    tableBody.appendChild(newRow);
    newRowNumber++;
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
            numeric--;
            // Update the displayed row numbers after deletion
            for (var i = 0; i < rows.length; i++) {
                rows[i].cells[0].textContent = i + 1; // This updates the text to the correct number
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