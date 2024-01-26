function receptionRegister(id) {
    let url = '/DSReception/DSRegister/Register?DharmaServiceID=' + id;


    $.get(url, function (data) {
        $('#dsRegisterDiv').html(data);
    }, 'html');
}