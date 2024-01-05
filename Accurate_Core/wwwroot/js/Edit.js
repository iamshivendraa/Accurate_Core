
function openEditGradeModal(stockNumber, grade, gradePrice) {
    // Set the stockNumber in the hidden input of the modal
    $('#editGradeModal').data('stockNumber', stockNumber);

    // Set the search input value to the stockNumber
    $('#searchInput').val(stockNumber);

    // Set the grade and grade price values in the modal
    $('#gradeInput').val(grade);
    $('#gradePriceInput').val(gradePrice);

    // Open the editGradeModal
    $('#editGradeModal').modal('show');
}

// Updated saveGradeInfo function
function saveGradeInfo() {
    // Retrieve the stockNumber from the data attribute of the modal
    var stockNumber = $('#editGradeModal').data('stockNumber');

    // If stockNumber is not set, try to get it from the search input
    if (!stockNumber) {
        stockNumber = $('#searchInput').val();
    }

    // Rest of the code remains the same...
    var grade = $('#gradeInput').val();
    var gradePrice = $('#gradePriceInput').val();
    
    // Add additional validation if needed

    // Call the action to update grade information
    $.ajax({
        url: '/Order/Edit',
        type: 'POST',
        data: {
            StockNumber: stockNumber,
            Grade: grade,
            GradePrice: gradePrice
        },

       
        success: function (result) {
            // Handle success or display error message
            if (result.success) {
                // Close the modal
                $('#editGradeModal').modal('hide');               
                alert("Records updated successfully!!");

                window.location.reload('/Order/Edit');

            } else {
            
                // Display error message
                // ...
            }
        },
        error: function () {
            // Handle error
            // ...
        }
    });
}
