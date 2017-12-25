$(document).ready(function () {
    let underDevelopmentMessage = 'This feature is under development.';
    $('a.not-implemented').tooltip({ title: underDevelopmentMessage, container: "td" });
    $('button.upload-missing').tooltip({ title: underDevelopmentMessage, container: "form" });
});

//$(document).ready(function() {
//    $("input[type='submit']").click(function() {
//        var $fileUpload = $("input[type='file']");
//        console.log($fileUpload);
//        if (parseInt($fileUpload.get(0).files.length) > 5) {
//            alert("You can only upload a maximum of 5 files");
//        }
//    });
//});