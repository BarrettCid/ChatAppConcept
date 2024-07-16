using ChatAppAPI.Services.Interfaces;
using ChatAppContext.Entities;
using PusherServer;

namespace ChatAppAPI.Services
{
    public class PusherService : IPusherService
    {
        private string _appId;
        private string _key;
        private string _custer;
        public PusherService(string appId, string key, string cluster) 
        {
            this._appId = appId;
            this._key = key;
            this._custer = cluster;
        }

        public async Task<bool> SendPusherMessageByMessageEntity(Message messageEntity, string pusherChannel, string pusherEvent)
        {
            //run pusher to push message to clients listening
            var options = new PusherOptions
            {
                Cluster = "us2",
                Encrypted = true
            };

            var pusher = new Pusher(
                this._appId,
                this._key,
                this._custer,
                options);



            var result = await pusher.TriggerAsync(
                pusherChannel,
                pusherEvent,
                new { messageId = messageEntity.MessageId, channelId = messageEntity.ChannelId.ToString(), message = messageEntity.MessageData, username = messageEntity.User.EmailAddress, dateCreated = messageEntity.DateCreated.ToString("MM/dd/yy HH:mm") });

            return true;
        }
    }
}
