function showMyImage(fileInput) {
    alert("ZO");
    var allowedExtension = ['jpeg', 'jpg', 'png'];
    var fileExtension = document.getElementById('files').value.split('.').pop().toLowerCase();
    var isValidFile = false;

    for (var index in allowedExtension) {

        if (fileExtension === allowedExtension[index]) {
            isValidFile = true;
            break;
        }
    }
    if (!isValidFile) {
        alert('Các file hình cho phép có dạng : *.' + allowedExtension.join(', *.'));
        document.getElementById("files").value = '';
    }
    else {
        document.getElementById("checkchange").value = "changed";
        var files = fileInput.files;
        for (var i = 0; i < files.length; i++) {
            var file = files[i];

            var img = document.getElementById("img");
            img.file = file;
            var reader = new FileReader();
            reader.onload = (function (aImg) {
                return function (e) {
                    aImg.src = e.target.result;
                };
            })(img);
            reader.readAsDataURL(file);
        }
    }

}