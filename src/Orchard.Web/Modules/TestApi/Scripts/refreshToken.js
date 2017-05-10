function RefreshToken(token, refreshToken) {
    var tokenKey = "tokenInfo";
    console.log(refreshToken);
    var refreshData = "grant_type=refresh_token&refresh_token=" + refreshToken + "&client_id=";
    $.ajax({
        type: 'POST',
        url: '/OrchardLocal/Token',
        contentType: "application/json",
        data: refreshData,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).success(function (data) {
        sessionStorage.setItem(tokenKey, data.access_token);
        sessionStorage.setItem("refresh_token", data.refresh_token);
    }).fail(function (x, y, z) {
        alert(x + '\n' + y + '\n' + z);
    });
}