const ipcRenderer = window.require('electron').ipcRenderer;
const jQuery = window.require('jquery');

window.onload = function() {
    var params = window.location.search.substring(1).split('&')[0].split('=');
    jQuery("#message").html(decodeURIComponent(params[1]));
 };

 jQuery(function() {
    jQuery("#error-confirmation").on('click', e => {
        ipcRenderer.send('client-close-window');
    });
});