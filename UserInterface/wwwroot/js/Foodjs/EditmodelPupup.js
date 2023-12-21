$(document).ready(function () {
    $('#tblFood').on('click', '.edit-button', function () {
        var foodId = $(this).data('id');

        // Optionally, make an AJAX request to get the details of the food item
        $.ajax({
            url: '/Food/FoodIndex', // Update with the correct URL
            type: 'GET',
            data: { id: foodId },
            success: function (response) {
                // Populate the modal with the response
                // Assuming the server returns the HTML content to be displayed in the modal
                $('#editFoodModal .modal-body').html(response);
            }
        });

        // Open the modal
        $('#editFoodModal').modal('show');
    });
});