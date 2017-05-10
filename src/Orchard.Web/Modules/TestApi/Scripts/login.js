$(function () {
    var tokenKey = "tokenInfo";
    $('#submitLogin').click(function (e) {
        e.preventDefault();
        var loginData = {
            grant_type: 'password',
            username: $('#emailLogin').val(),
            password: $('#passwordLogin').val()
        };

        $.ajax({
            type: 'POST',
            url: '/OrchardLocal/Token',
            data: loginData
        }).success(function (data) {
            $('.userName').text(data.Email);
            $('.userInfo').css('display', 'block');
            $('.loginForm').css('display', 'none');

            sessionStorage.setItem(tokenKey, data.access_token);
            sessionStorage.setItem("refresh_token", data.refresh_token);
            console.log(data);
        }).fail(function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        });
    });

    $('#logOut').click(function (e) {
        e.preventDefault();
        sessionStorage.removeItem(tokenKey);
    });
})