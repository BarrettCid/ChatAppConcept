if (typeof ipcRenderer === 'undefined') {
    const { ipcRenderer } = window.require('electron').ipcRenderer;
}
if (typeof jQuery === 'undefined') {
    const jQuery = window.require('jquery');
}

const { renderServers } = require(`./../scripts/server.js`);

window.onload = async function() {
    await renderServers('#server-bar');
 };

 function loadServer(serverId) {
    ipcRenderer.send('redirect', 'src/server/client.html', 'Server Name', {'serverId': serverId, });
 }
