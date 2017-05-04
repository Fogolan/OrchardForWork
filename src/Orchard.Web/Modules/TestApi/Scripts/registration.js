$(function() {
    $('#submit').click(function(e) {
        e.preventDefault();
        var data = {
            Email: $('#email').val(),
            Password: $('#password').val(),
            ConfirmPassword: $('#confirmpassword').val()
        };

        $.ajax({
            url: '/OrchardLocal/api/account/',
            type: 'POST',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(data)
        }).success(function(data) {
            alert("Регистрация пройдена");
        }).error(function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        });
    });
});