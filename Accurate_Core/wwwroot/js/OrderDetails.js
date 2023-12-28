var js = jQuery.noConflict(true);

js(document).ready(function () {
    js('#orderTable').DataTable();
    var table = js('#editTable').DataTable();

    // Variable to store the timer for opening the modal
    var modalTimer;

    $("#searchInput").on("keyup", function () {
        // Use DataTables search API to filter rows
        table.search(this.value).draw();

        // Clear the previous timer
        clearTimeout(modalTimer);

        // Check if the entered stock number is found in the table
        var enteredStockNumber = this.value;
        var isMatchFound = isStockNumberMatch(enteredStockNumber);

        if (isMatchFound) {
            // Set a new timer to open the modal after 2 seconds
            modalTimer = setTimeout(function (stockNumber) {
                openModalWithText('Match Found on "' + stockNumber + '"');
            }, 2000, enteredStockNumber);
        }
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
});
