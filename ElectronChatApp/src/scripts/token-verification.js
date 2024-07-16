if (typeof ipcRenderer === 'undefined') {
    const { ipcRenderer } = window.require('electron').ipcRenderer;
}
if (typeof jQuery === 'undefined') {
    const jQuery = window.require('jquery');
}
const { getIssueDate, getUsername} = require(`./../scripts/helper.js`);


window.onload = function() {
    console.log('we are in verification...');
    var username = getUsername();
    verifyTokenIssueDate(username);
 };

 function verifyTokenIssueDate(username) {
    var now = Date.now();
    var tokenDate = Date.parse(getIssueDate());
    var milliseconds = futureDate.getTime() - todayDate.getTime();
    var hours = Math.floor(milliseconds / (60 * 60 * 1000));
    if(hours > 2) {
        ipcRenderer.send('logout-purge', username);
    }
 }