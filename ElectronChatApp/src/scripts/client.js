const Pusher = require("pusher-js");
// const jQuery = window.require('jquery');
const { renderChannels, renderMessages, sendMessage } = require(`./../scripts/server.js`);
 let channelMessages = [];


 window.onload = async function() {
    let params = window.location.search.substring(1).split('&')[0].split('=');
    console.log(params);
    if (params.length > 0) {
        let serverId = params[1];
        jQuery("#currentServerId").val(serverId);
        let channels = [];
        await renderChannels("#channel-list", serverId, channels).done(function(channels) {
            jQuery("currentChannelId").val(channels[0].channelId);
            loadChannel(channels[channels.length - 1].channelId);
        });
    }
 };

 jQuery(function() {
    jQuery('#messageToSubmit').keyup(function (e) {
        if(e.keyCode === 13) 
        {
            jQuery("#send-message-button").click();
        }
    });
    jQuery("#send-message-button").on('click', e => {
        let currentChannelId = jQuery('#currentChannelId').val();
        let messageData = jQuery('#messageToSubmit').val();
        if(messageData.length > 0)
            sendMessage(messageData, currentChannelId);
        jQuery('#messageToSubmit').val('');
    }); 
    
});

 //log pusher to console
 Pusher.logToConsole = true;

var listener = new Pusher('897cab9763f2560570bc', {
     cluster: 'us2'
 });

 var channel = listener.subscribe('my-channel');
 channel.bind('my-event', function(data) {
    if (channelMessages.length === 30) {
        popMessage();
    }
    let currentChannelId = jQuery('#currentChannelId').val();
    console.log(data);
    console.log(currentChannelId);
    if(data.channelId == currentChannelId)
        pushMessage(data);
 });

 function pushMessage(data) {
    jQuery.get("../server/message.html", function( template ) {
        template = template.replaceAll('usernameSent', data.username);
        template = template.replaceAll('messageId', data.messageId);
        template = template.replaceAll('messageData', data.message);
        template = template.replaceAll('dateCreated',data.dateCreated);
        template = template.replaceAll('channelId', data.channelId);
        channelMessages.push(template);
        jQuery("#message-window").prepend(template);
    });
}

function popMessage() {
    messages.splice(0, 1);
}

async function loadChannel(channelId) {
    console.log(channelId);
    jQuery('#currentChannelId').val(channelId);
    jQuery('#message-window').html('');
    await renderMessages("#message-window", channelId)
    .done(function(messages) {
        channelMessages = messages;
    });
}
