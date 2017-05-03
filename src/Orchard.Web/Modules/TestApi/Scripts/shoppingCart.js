﻿$(document).ready(function () {
    GetAllBooks();
});

function DeleteItem(el) {
    var id = $(el).attr('data-item');
    DeleteBook(id);
}

function DeleteBook(id) {

    $.ajax({
        url: '/OrchardLocal/api/cart/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (msg) {
            GetAllBooks();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function GetAllBooks() {
    $.ajax({
        url: '/OrchardLocal/api/cart/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponse(data);
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