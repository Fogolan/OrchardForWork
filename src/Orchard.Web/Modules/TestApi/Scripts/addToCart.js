$(document).ready(function () {

    $(".addBook").click(function (event) {
        event.preventDefault();
        AddBook($(this).attr('id'));
    });

});

function AddBook(id) {
    var tokenKey = "tokenInfo";
    var token = JSON.parse(sessionStorage.getItem(tokenKey));
    $.ajax({
        url: 'api/cart/' + id +'?apiKey=12345',
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        data: "grant_type=refresh_token&refresh_token=" + token.refresh_token,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token.access_token);
        },
        success: function (msg) {
            if (msg.success === true) {
                window.location = "/OrchardLocal/testapi/shoppingCart/";
            }
        },
        statusCode: {
            401: function (response) {
                RefreshToken(token.access_token, token.refresh_token);
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}