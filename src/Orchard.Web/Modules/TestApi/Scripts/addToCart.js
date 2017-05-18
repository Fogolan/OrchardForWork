$(document).ready(function () {

    $(".addBook").click(function (event) {
        event.preventDefault();
        AddBook($(this).attr('id'));
    });

});

function AddBook(id) {
    var tokenKey = "tokenInfo";
    var token = JSON.parse(getCookie(tokenKey));
    console.log("access",token.access_token);
    console.log("refresh",token.refresh_token);
    $.ajax({
        url: 'api/cart/' + id +'?apiKey=12345',
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        data: "grant_type=refresh_token&refresh_token=" + token.refresh_token,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token.access_token);
        },
        success: function (msg) {
            if (msg.success == true) {
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

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}