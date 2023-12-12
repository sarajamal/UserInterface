// customTypeScript.js

$(document).ready(function () {
    var selectType = $("#selectType");
    var customTypeInput = $("#customTypeInput");

    selectType.change(function () {
        var selectedValue = $(this).val();

        if (selectedValue === "0") { // Assuming "0" is the value for "أضف نوعا جديدا"
            // Display the custom input field
            customTypeInput.show();
        } else {
            // Hide the custom input field if a different option is selected
            customTypeInput.hide();
        }
    });

    // Trigger the change event on page load to handle the initial state
    selectType.trigger("change");

    // Handle form submission to set the value of نوع الصنف
    $("form").submit(function () {
        var selectedValue = selectType.val();

        if (selectedValue === "0") {
            // Set the value of نوع الصنف to the custom input value
            selectType.val(customTypeInput.val());
        }
    });
});