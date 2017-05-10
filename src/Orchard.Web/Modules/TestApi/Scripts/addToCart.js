$(document).ready(function () {

    $(".addBook").click(function (event) {
        event.preventDefault();
        AddBook($(this).attr('id'));
    });

});

function AddBook(id) {
    var tokenKey = "tokenInfo";
    $.ajax({
        url: 'api/cart/' + id +'?apiKey=12345',
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        data: "grant_type=refresh_token&refresh_token=" + sessionStorage.getItem("refresh_token"),
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (msg) {
            if (msg.success == true) {
                window.location = "/OrchardLocal/testapi/shoppingCart/";
            }
        },
        statusCode: {
            401: function (response) {
                var token = sessionStorage.getItem(tokenKey);
                var refreshToken = sessionStorage.getItem("refresh_token");
                RefreshToken(token, refreshToken);
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}