// import * as jQuery from 'jquery'
// import { ipcRenderer } from 'electron';
const ipcRenderer = window.require('electron').ipcRenderer;
const jQuery = window.require('jquery');


jQuery(function() {
    jQuery("#logout_sidebar_button").on('click', e => {
        processLogout();
    });
});

function processLogout() {
    var userData = sessionStorage.getItem("userData");
    if (userData !== null) {
        sessionStorage.removeItem("userData");
    }
    ipcRenderer.send('logout');
}
