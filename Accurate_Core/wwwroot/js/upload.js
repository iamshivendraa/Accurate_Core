Dropzone.autoDiscover = false;
var myDropzone = new Dropzone("#myDropzone", {
    url: "/Order/Upload",
    autoProcessQueue: false
});

document.getElementById("submit-button").addEventListener("click", function () {
    myDropzone.processQueue(); // Manually trigger file upload
});
