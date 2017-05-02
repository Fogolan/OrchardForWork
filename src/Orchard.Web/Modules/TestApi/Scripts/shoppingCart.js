$(document).ready(function () {

    $(".addBook").click(function (event) {
        event.preventDefault();
        AddBook($(this).attr('id'));
    });

});

function AddBook(id) {
    $.ajax({
        url: 'api/cart/' + id,
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        success: function (msg) {
            if (msg.Success == true) {
                window.location = "/OrchardLocal/testapi/shoppingCart/";
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteBook(id) {

    $.ajax({
        url: 'api/cart/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.Success == true) {
                window.location = "/OrchardLocal/testapi/shoppingCart/";
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}