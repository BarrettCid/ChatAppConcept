
if (typeof ipcRenderer == null) {
    const { ipcRenderer } = require('electron');
}

const { getToken, getUsername} = require(`./../scripts/helper.js`);
const jQuery = window.require('jquery');
const moment = window.require('moment');

//get all servers for current user
 function getServers(){
    var token = getToken();
    var username = getUsername();
    return jQuery.get({
        url : `https://localhost:7225/api/User/getServers/${username}`,
        type : 'GET',
        dataType:'json',
        headers: {"Authorization": `Bearer ${token}`},
        success : function(data) {
            console.log(data);              
            if (data){
                return data;
            }
        },
        error : function(request)
        {
            ipcRenderer.send('error-message', request.responseText);
        }
    });
}

function renderServers(id) {
    var token = getToken();
    var username = getUsername();
    return jQuery.get({
        url : `https://localhost:7225/api/User/getServers/${username}`,
        type : 'GET',
        dataType:'json',
        headers: {"Authorization": `Bearer ${token}`},
        success : function(data) {
            console.log(data);              
            if (data){
                data.forEach(function (server, i) {
                    jQuery.get("../server/server.html", function( template ) {
                        template = template.replaceAll('serverId', server.serverId);
                        template = template.replaceAll('serverName',server.name);
                        jQuery(id).prepend(template);
                    });
                });
            }
        },
        error : function(request)
        {
            ipcRenderer.send('error-message', request.responseText);
        }
    });
}

//get all channels for selected server
function getChannels(serverId) {
    var token = getToken();
    return jQuery.get({
        url : `https://localhost:7225/api/Server/getChannelsByServer/${serverId}`,
        type : 'GET',
        dataType:'json',
        headers: {"Authorization": `Bearer ${token}`},
        success : function(data) {
            console.log(data);              
            if (data){
                return data;
            }
        },
        error : function(request)
        {
            ipcRenderer.send('error-message', request.responseText);
        }
    });
}

function renderChannels(id, serverId) {
    var token = getToken();
    return jQuery.get({
        url : `https://localhost:7225/api/Server/getChannelsByServer/${serverId}`,
        type : 'GET',
        dataType:'json',
        headers: {"Authorization": `Bearer ${token}`},
        success : function(data) {           
            if (data){
                data.forEach(function (channel, i) {
                    jQuery.get("../server/channel.html", function( template ) {
                        template = template.replaceAll('channelId', channel.channelId);
                        template = template.replaceAll('channelName',channel.name);
                        jQuery(id).prepend(template);
                    });
                });
            }
        },
        error : function(request)
        {
            ipcRenderer.send('error-message', request.responseText);
        }
    });
}

function renderMessages(id, channelId) {
    var token = getToken();
    return jQuery.get({
        url : `https://localhost:7225/api/Channel/getMessagesByChannel/${channelId}`,
        type : 'GET',
        dataType:'json',
        headers: {"Authorization": `Bearer ${token}`},
        success : function(data) {           
            if (data){
                data.forEach(function (message, i) {
                    jQuery.get("../server/message.html", function( template ) {
                        template = template.replaceAll('usernameSent', message.username);
                        template = template.replaceAll('messageId', message.messageId);
                        template = template.replaceAll('messageData', message.message);
                        template = template.replaceAll('dateCreated',message.dateCreated);
                        template = template.replaceAll('channelId', message.channelId);
                        
                        jQuery(id).append(template);
                    });
                });
            }
        },
        error : function(request)
        {
            ipcRenderer.send('error-message', request.responseText);
        }
    });
}

function sendMessage(message, channelId) {
    var token = getToken();
    var username = getUsername();
    var data = {
        'Username': username,
        'ChannelId':channelId,
        'Message':message
    }
    return jQuery.ajax({
        url : `https://localhost:7225/api/Server/sendMessage`,
        type : 'POST',
        dataType:'json',
        contentType: "application/json",
        data: JSON.stringify(data),
        headers: {"Authorization": `Bearer ${token}`},
        error : function(request)
        {
            ipcRenderer.send('error-message', request.responseText);
        }
    });
}


module.exports = {getServers, renderServers, renderChannels, renderMessages, sendMessage};




