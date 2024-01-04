Dropzone.autoDiscover = false;
var myDropzone = new Dropzone("#myDropzone", {
    url: "/Order/Index",
    maxFiles: 1,
    acceptedFiles: ".xlsx, .xls",
    autoProcessQueue: false,
    success: function (file, response) {

        if (response.success) {
            // Hide the modal on successful upload
            $('#exampleModal').modal('hide');

            // Remove the uploaded file from Dropzone
            myDropzone.removeAllFiles(true);

            alert("File Successfully Uploaded!");

        } else {
            $('#exampleModal').modal('hide');
            myDropzone.removeAllFiles(true);
            alert("Plese check your file!");
        }
    }
});
$(document).ready(function () {
        js('#closeButton').on("click",function () {
            $('#exampleModal').modal('hide');
            myDropzone.removeAllFiles(true);

        });

        js('#submitButton').on("click", function () {

            myDropzone.processQueue();
        });
});