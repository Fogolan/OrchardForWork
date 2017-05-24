$(document).ready(function () {
    GetAllBooks();
});

function DeleteItem(el) {
    var id = $(el).attr('data-item');
    DeleteBook(id);
}

function DeleteBook(id) {
    var tokenKey = "tokenInfo";
    var token = JSON.parse(sessionStorage.getItem(tokenKey));
    $.ajax({
        url: '/OrchardLocal/api/cart/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        data: "grant_type=refresh_token&refresh_token=" + token.refresh_token,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token.access_token);
        },
        success: function (msg) {
            GetAllBooks();
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

function GetAllBooks() {
    var tokenKey = "tokenInfo";
    var token = JSON.parse(sessionStorage.getItem(tokenKey));
    $.ajax({
        url: '/OrchardLocal/api/cart/',
        type: 'GET',
        dataType: 'json',
        data: "grant_type=refresh_token&refresh_token=" + token.refresh_token,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token.access_token);
        },
        success: function (data) {
            WriteResponse(data);
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

function WriteResponse(books) {
    var strResult = "<table><th>Название</th>";
    $.each(books, function (index, book) {
        strResult += "<tr><td>" + book.name + "</td>" +
            "<td class='action'><a id='delItem' data-item='" + book.id + "' onclick='DeleteItem(this);' >x</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);
}