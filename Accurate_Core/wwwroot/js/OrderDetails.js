var js = jQuery.noConflict(true);

js(document).ready(function () {
    js('#orderTable').DataTable();
    $('#openModalBtn').click(function () {
        $('#exampleModal').modal('show')
        $('#exampleModalLabel').text("Upload");
    });
   
});