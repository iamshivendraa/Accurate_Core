//var js = jQuery.noConflict(true);

//js(function () {
//    var table = $('#editTable').DataTable();
//    var searchTimer;
//    var searchInput = $("#searchInput");

//    js('#openModalBtn').on('click', function () {
//        $('#exampleModal').modal('show');
//        $('#exampleModalLabel').text("Upload");
//    });




//    searchInput.on("input", function () {
//        // Clear the previous timer
//        clearTimeout(searchTimer);

//        // Set a timer to start searching after 2 seconds of inactivity
//        searchTimer = setTimeout(function () {
//            // Use DataTables search API to filter rows
//            table.search(searchInput.val(), false, true);
//            // Check if the entered stock number is found in the table
//            var enteredStockNumber = searchInput.val();
//            var isMatchFound = isStockNumberMatch(enteredStockNumber);

//            if (isMatchFound) {

//                openModalWithText('Match Found on "' + enteredStockNumber + '"');
//            }
//        }, 2000);
//    });


//    function openModalWithText(text) {
//        $('#myModal').modal('show');
//        $('#myModalLabel').text(text);
//    }

//    function isStockNumberMatch(enteredStockNumber) {
//        // Get an array of stock numbers in the table
//        var stockNumbers = table.column(0).data().toArray();

//        // Check if the entered stock number is in the array
//        return stockNumbers.includes(enteredStockNumber);
//    }
//});


﻿var js = jQuery.noConflict(true);



js(document).ready(function () {
    js('#orderTable').DataTable();
    var table = js('#editTable').DataTable();
    var searchTimer;
    var searchInput = $("#searchInput");
    updateDisplay();

    searchInput.on("input", function () {
        // Clear the previous timer
        clearTimeout(searchTimer);

        // Set a timer to start searching after 2 seconds of inactivity
        searchTimer = setTimeout(function () {
            // Use DataTables search API to filter rows
            table.search(searchInput.val(), false, true); 
            // Check if the entered stock number is found in the table
            var enteredStockNumber = searchInput.val();
            var isMatchFound = isStockNumberMatch(enteredStockNumber);

            if (isMatchFound) {
               
                openModalWithText('Match Found on "' + enteredStockNumber + '"');
            }
        }, 2000);
    });

    js('#openModalBtn').click(function () {
        $('#exampleModal').modal('show');
        $('#exampleModalLabel').text("Upload");

    });
    function openModalWithText(text) {
        $('#myModal').modal('show');
        $('#myModalLabel').text(text);
    }

    function isStockNumberMatch(enteredStockNumber) {
        // Get an array of stock numbers in the table
        var stockNumbers = table.column(0).data().toArray();

        // Check if the entered stock number is in the array
        return stockNumbers.includes(enteredStockNumber);
    }
    

    // Function to update the display based on the toggle state
    function updateDisplay() {
        var showMatchedOnly = $('#matchedToggle').prop('checked');

        $('.data-row').each(function () {
            var isMatched = $(this).find('.fa-check').length > 0;

            if (showMatchedOnly && !isMatched) {
                $(this).hide();
            } else {
                $(this).show();
            }
        });
    }
    

    // Handle toggle switch change event
    js('#matchedToggle').change(function () {
        updateDisplay();
    });

    // Initial setup
    
});



