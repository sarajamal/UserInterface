
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

    var addButton = document.getElementById("addToolsButton");
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


//صفحة الاضافة منتجات جاهزة جديدة 
var clickCount = 0;
var lastID = 0; // Initialize lastID globally
function AddnewFoodReady(BrandFK) {
    
        $.ajax({
            url: '/FinishProducts/GetAddID',
            type: 'POST',
            data: {
                BrandFK: BrandFK
            },
            success: function (response) {
                lastID = parseInt(response);
                addStep(BrandFK);
            },
            error: function (xhr, status, error) {
                console.error('Error fetching last ID:', error);
            }
        });
    
    function addStep(BrandFK) {

        // Find the table body element
        var tableBody = document.querySelector("#tblFinishFood tbody");
 
        // Find the last row index
        var newRowIndex = tableBody.children.length ;

        // Create a new row for الخطوة1 and الخطوة2 in the same row
        var newRow = document.createElement("tr");
        newRow.innerHTML = `
       <td style="text-align:center;" class="col-5">
            <input type="hidden" name="ReadyFoodLoginVMlist[${newRowIndex}].BrandFK" value="${BrandFK}" />
            <input type="hidden" name="ReadyFoodLoginVMlist[${newRowIndex}].ReadyProductsID" value="${lastID}" />
            <input type="hidden" name="ReadyFoodLoginVMlist[${newRowIndex}].ReadyProductsImage" />
          
        <div>
            <textarea class="form-controlstyle1" id="ReadyFoodLoginVMlist_${newRowIndex}" name="ReadyFoodLoginVMlist[${newRowIndex}].ReadyProductsName"></textarea>
         </div>
     </td>
        
      <td style="text-align:center;" class="col-5">
        <div class="row">
            <div class="text-center ">
                <img id="PreviewPhoto1_${lastID}" src="/IMAGES/noImage.png" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
                <input type="file" name="file1_${lastID}" class="border-0 shadow mt-5" id="customFile1_${lastID}" onchange="displaySelectedImage(this, 'PreviewPhoto1_${lastID}')">
            </div>
        </div>
    </td>      
      
</tr>
    `;

        // Append the new الخطوة1 and الخطوة2 row to the table body
        tableBody.appendChild(newRow);
        clickCount++;
        console.log("newCell:", newRow); // Debugging log 
    }
}
 

////زر الحذ في صفحة التعديل قبل الحفظ في قاعدة البيانات .
function DeleteReadyFood1(button) {

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

 
    



//صفحة الاضافة منتجات جاهزة جديدة 
//var clickCount = 0;
//var lastID = 0; // Initialize lastID globally
//function AddnewFoodReady(ReadyFoodFk) {
//    if (clickCount === 0) {
//        // Only retrieve lastID from server on the first click
//        $.ajax({
//            url: '/FinishProducts/GetLastId',
//            type: 'GET',
//            success: function (response) {
//                lastID = parseInt(response) + 1;
//                addStep(ReadyFoodFk);
//            },
//            error: function (xhr, status, error) {
//                console.error('Error fetching last ID:', error);
//            }
//        });
//    } else {
//        // On subsequent clicks, increment lastID locally
//        lastID++;
//        addStep(ReadyFoodFk);
//    }
//    function addStep(ReadyFoodFk) {

//        // Find the table body element
//        var tableBody = document.querySelector("#tblFinishFood tbody");

//        // Find the last row index
//        var newRowIndex = tableBody.children.length;

//        // Create a new row for الخطوة1 and الخطوة2 in the same row
//        var newRow = document.createElement("tr");
//        newRow.innerHTML = `
//       <td style="text-align:center;" class="col-5">
//            <input type="hidden" name="ReadyFoodLoginVMlist[${newRowIndex}].BrandFK" value="${ReadyFoodFk}" />
//            <input type="hidden" name="ReadyFoodLoginVMlist[${newRowIndex}].ReadyProductsImage" />
          
//        <div>
//            <textarea class="form-controlstyle1" id="ReadyFoodLoginVMlist_${newRowIndex}" name="ReadyFoodLoginVMlist[${newRowIndex}].ReadyProductsName"></textarea>
//         </div>
//     </td>
        
//      <td style="text-align:center;" class="col-5">
//        <div class="row">
//            <div class="text-center ">
//                <img id="PreviewPhoto1_${lastID}" src="/IMAGES/noImage.png" alt="Logo" width="125" height="125" style="border: 1px; margin-top: 20px;">
//                <input type="file" name="file1_${lastID}" class="border-0 shadow mt-5" id="customFile1_${lastID}" onchange="displaySelectedImage(this, 'PreviewPhoto1_${lastID}')">
//            </div>
//        </div>
//    </td>      
      
//</tr>
//    `;

//        // Append the new الخطوة1 and الخطوة2 row to the table body
//        tableBody.appendChild(newRow);
//        clickCount++;
//        console.log("newCell:", newRow); // Debugging log 
//    }
//}


////زر الحذ في صفحة التعديل قبل الحفظ في قاعدة البيانات .
//function DeleteReadyFood1(button) {

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
//            var row = button.closest("tr");
//            row.remove();
//            Swal.fire('Deleted!', 'تم الحذف بنجاح!', 'success');
//            updateRowIndices();
//        }
//    });
//}

//function updateRowIndices() {
//    var tableBody = document.querySelector("#tblFinishFood tbody");
//    var rows = tableBody.querySelectorAll("tr");

//    rows.forEach((row, index) => {
//        // تحديث المؤشرات لكل عنصر input و textarea
//        var elements = row.querySelectorAll("input[name*='readyfoodlistVM'], textarea[name*='readyfoodlistVM']");
//        elements.forEach(element => {
//            var name = element.name;
//            // تحديث الاسم باستخدام التعبير النظامي ليطابق الـ index الجديد
//            element.name = name.replace(/\[\d+\]/, `[${index}]`);
//        });

//        // تحديث الأزرار أو أي عناصر أخرى تستخدم الـ index
//        var buttons = row.querySelectorAll("button[data-row-index]");
//        buttons.forEach(button => {
//            button.setAttribute("data-row-index", index);
//        });
//    });
//}