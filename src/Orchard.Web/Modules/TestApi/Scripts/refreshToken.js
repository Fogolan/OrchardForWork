function RefreshToken(token, refreshToken) {
    var tokenKey = "tokenInfo";
    var tokenObject = JSON.parse(sessionStorage.getItem(tokenKey));
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
        tokenObject.access_token = data.access_token;
        tokenObject.refresh_token = data.refresh_token;
        sessionStorage.setItem(tokenKey, JSON.stringify(tokenObject));
    }).fail(function (x, y, z) {
        alert(x + '\n' + y + '\n' + z);
    });
}