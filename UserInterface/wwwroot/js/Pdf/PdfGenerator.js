function generatePdf(productionId) {
    var url = '/Production/generatePdf/' + productionId;
    console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        data: {
            productionId: productionId
        },
        success: function (data) {
            console.log(data); // Assuming data is a JSON object
            // Handle the JSON data here
        }
    });
}