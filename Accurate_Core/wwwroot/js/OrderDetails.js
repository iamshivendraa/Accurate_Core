/*var js = jQuery.noConflict(true);*/
$(document).ready(function () {
 
    $('#orderTable').DataTable();   
    $('#openModalBtn').click(function () {
        $('#exampleModal').modal('show');
        $('#exampleModalLabel').text("Upload");
    });
});
