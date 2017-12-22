$(document).ready(function () {
    let underDevelopmentMessage = 'This feature is under development.';
    $('a.not-implemented').tooltip({ title: underDevelopmentMessage, container: "td" });
    $('button.upload-missing').tooltip({ title: underDevelopmentMessage, container: "form" });
});