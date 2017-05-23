$(document).ready(function () {
    GetAllBooks();
});

function DeleteItem(el) {
    var id = $(el).attr('data-item');
    DeleteBook(id);
}

function DeleteBook(id) {
    var tokenKey = "tokenInfo";
    $.ajax({
        url: '/OrchardLocal/api/cart/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        data: "grant_type=refresh_token&refresh_token=" + sessionStorage.getItem("refresh_token"),
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (msg) {
            GetAllBooks();
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

function GetAllBooks() {
    var tokenKey = "tokenInfo";
    $.ajax({
        url: '/OrchardLocal/api/cart/',
        type: 'GET',
        dataType: 'json',
        data: "grant_type=refresh_token&refresh_token=" + sessionStorage.getItem("refresh_token"),
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (data) {
            WriteResponse(data);
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

function WriteResponse(books) {
    var strResult = "<table><th>Название</th>";
    $.each(books, function (index, book) {
        strResult += "<tr><td>" + book.name + "</td>" +
            "<td class='action'><a id='delItem' data-item='" + book.id + "' onclick='DeleteItem(this);' >x</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);
}