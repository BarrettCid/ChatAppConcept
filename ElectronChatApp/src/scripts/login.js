const ipcRenderer = window.require('electron').ipcRenderer;
const jQuery = window.require('jquery');

window.onload = function() {
    if (isLoggedIn()) {
        ipcRenderer.send('login-success', 'server/server_select');
    }
 };

 jQuery(function() {
    jQuery("#login-button").on('click', e => {
        let username = jQuery("#username").val();
        let password = jQuery("#userPassword").val();
        if (username.length == 0 || password.length == 0)
        {
            ipcRenderer.send('error-message', "Please enter a username and password to continue.");    
        }
        else
        {
            processLogin();
        }
    });
});

function isLoggedIn() {
    var userData = sessionStorage.getItem("userData");
    if (userData == null) {
        return false;
    } else {
        return userData.TokenExpired;
    }
}

function processLogin() {
    let username = jQuery("#username").val();
    let password = jQuery("#userPassword").val();
    jQuery.post({
        url : `https://localhost:7225/api/User/login?username=${username}&password=${password}`,
        type : 'POST',
        dataType:'json',
        success : function(data) {              
            if (data){
                console.log(data);
                buildSession(data);
                ipcRenderer.send('login-success');
            }
        },
        error : function(request)
        {
            ipcRenderer.send('error-message', request.responseText);
        }
    });
}

function buildSession(response) {
    window.sessionStorage.setItem(`${response.emailAddress}_token`, response.tokenId);
    window.sessionStorage.setItem(`${response.emailAddress}_issueDate`, response.issueDate);
    window.sessionStorage.setItem(`${response.emailAddress}_username`, response.emailAddress);
}


