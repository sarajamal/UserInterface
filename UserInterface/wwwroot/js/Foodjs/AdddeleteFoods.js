
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


//صفحة الاضافة مواد غذائية جديدة 
var clickCount = 0;
var lastID = 0; // Initialize lastID globally
function AddnewFoods(BrandFK) {
    
        // Only retrieve lastID from server on the first click
        $.ajax({
            url: '/Food/GetAddID',
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
            var tableBody = document.querySelector("#tblFoods tbody");
            var newRowIndex = tableBody.children.length ;

        // Create a new row for الخطوة1 and الخطوة2 in the same row
        var newRow = document.createElement("tr");
        newRow.innerHTML = `
       <td class="col-5" style="text-align:center;">
            <input type="hidden" name="FoodLoginVMlist[${newRowIndex}].BrandFK" value="${BrandFK}" />
            <input type="hidden" name="FoodLoginVMlist[${newRowIndex}].FoodStuffsID" value="${lastID}" />
            <input type="hidden" name="FoodLoginVMlist[${newRowIndex}].FoodStuffsImage" />
        
             <div>
            <input type= "textarea" class="form-controlstyle1" id="FoodLoginVMlist_${newRowIndex}" name="FoodLoginVMlist[${newRowIndex}].FoodStuffsName">
         </div>
     </td>
        
      <td class="col-5" style="text-align:center;">
        <div class="row">
            <div class=" text-center">
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
//function DeleteFoodRow1(button) {
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
//                var row = button.closest("tr");
//                var rowIndex = parseInt(button.getAttribute("data-row-index"));
//                row.remove();
//                Swal.fire('Deleted!', 'تم الحذف بنجاح!', 'success');
//                updateRowIndicesAfterDeletion1(rowIndex); 
//        }
//    });
//}
//function updateRowIndicesAfterDeletion1(deletedIndex) {
//    var tableBody = document.querySelector("#tblFoods tbody");
//    var rows = tableBody.querySelectorAll("tr");

//    rows.forEach((row, index) => {
//        if (index > deletedIndex) { // تحديث فقط للصفوف بعد الصف المحذوف
//            var newRowIndex = index - 1; // تقليل index بواحد
//            var inputsAndButtons = row.querySelectorAll("input, button");

//            inputsAndButtons.forEach(el => {
//                if (el.name) {
//                    el.name = el.name.replace(/\[\d+\]/, `[${newRowIndex}]`);
//                }
//                if (el.getAttribute("data-row-index") !== null) {
//                    el.setAttribute("data-row-index", newRowIndex);
//                }
//            });
//        }
//    });

//    newIndex = rows.length; // تحديث newIndex بناءً على عدد الصفوف المتبقية
//}

//document.querySelector("#tblFoods tbody").addEventListener("click", function (event) {
//    if (event.target.classList.contains("data-row-index")) {
//        DeleteFoodRow1(event.target);
//    }
//});

//زر الحذف في صفحة الاضافة
//function DeleteRow3(button) {
//    var tableBody = document.querySelector("#tblSteps tbody");
//    var rows = tableBody.children;
//    var rowIndex = button.getAttribute("data-row-index");

//    // Check if the button click corresponds to the last row
//    if (rowIndex == rows.length - 1) {
//        Swal.fire({
//            title: 'هل أنت متأكد؟',
//            icon: 'warning',
//            showCancelButton: true,
//            cancelButtonText: 'الغاء',
//            confirmButtonColor: '#d33',
//            cancelButtonColor: '#3085d6',
//            confirmButtonText: 'نعم!'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                // Remove the last row from the table
//                tableBody.removeChild(rows[rowIndex]);

//                // Display a success message
//                Swal.fire({
//                    icon: 'success',
//                    title: 'تم الحذف بنجاح',

//                });
//            }
//        });
//    } else {
//        // Display a message that you cannot delete rows until they are saved in the database
//        Swal.fire({
//            icon: 'error',
//            title: 'أنت قادر على حذف الصف الأخير فقط ',
//            text: 'يجب حفظ التغييرات أولا والعودة الى صفحة التعديل لحذف الصف.',
//        });
//    }
//}


