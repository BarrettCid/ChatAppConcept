if (typeof ipcRenderer === 'undefined') {
    const { ipcRenderer } = window.require('electron').ipcRenderer;
}
if (typeof jQuery === 'undefined') {
    const jQuery = window.require('jquery');
}

function getUsername() {
    let keys = Object.keys(window.sessionStorage);
    let index = keys.indexOf('username');
    var key = keys.find(key => key.includes('username'));
    if (key) {
        return window.sessionStorage.getItem(key);
    } else {
        ipcRenderer.send('logout');
    }
 }

function getToken() {
    let username = getUsername();
    let keys = Object.keys(window.sessionStorage);
    let index = keys.indexOf(`${username}_token`);
    if (index) {
        return window.sessionStorage.getItem(keys[index]);
    } else {
        ipcRenderer.send('logout');
    }
 }

function getIssueDate() {
    let username = getUsername();
    let keys = Object.keys(window.sessionStorage);
    let index = keys.indexOf(`${username}_issueDate`);
    if (index) {
        return window.sessionStorage.getItem(keys[index]);
    } else {
        ipcRenderer.send('logout');
    }
 }


 module.exports = {getUsername, getToken, getIssueDate};