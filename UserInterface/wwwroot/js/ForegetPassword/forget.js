//function comparePasswords() {
//    var newPassword = document.getElementById("newPassword");
//    var surePassword = document.getElementById("surePassword");

//    if (newPassword.value !== surePassword.value) {
//        surePassword.setCustomValidity("تأكيد كلمة المرور غير متطابقة");
//    } else {
//        surePassword.setCustomValidity("");
//    }
//}

//document.addEventListener("DOMContentLoaded", function () {
//    var newPassword = document.getElementById("newPassword");
//    var surePassword = document.getElementById("surePassword");

//    newPassword.addEventListener("change", comparePasswords);
//    surePassword.addEventListener("input", comparePasswords);
//});


function comparePasswords() {
    var newPassword = document.getElementById("newPassword");
    var surePassword = document.getElementById("surePassword");
    var message = "";

    // Check if passwords match
    if (newPassword.value !== surePassword.value) {
        message = "كلمة المرور غير متطابقة";
    } else {
        // Check for at least one uppercase and one lowercase letter
        var letterRegex = /(?=.*[a-z])(?=.*[A-Z])/;
        if (!letterRegex.test(newPassword.value)) {
            message = "يجب أن تحتوي كلمة المرور على حرف كبير وحرف صغير";
        }
    }

    // Set custom validity message
    surePassword.setCustomValidity(message);
}

document.addEventListener("DOMContentLoaded", function () {
    var newPassword = document.getElementById("newPassword");
    var surePassword = document.getElementById("surePassword");

    // Add change event listeners
    newPassword.addEventListener("change", comparePasswords);
    surePassword.addEventListener("change", comparePasswords);
});