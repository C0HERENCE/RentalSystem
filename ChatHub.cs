using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace RentalServer
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        // 当用户连线时 在$connection.start()后执行
        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Session.GetInt32("id");
            if (!userId.HasValue)
                return;
            _chatService.UserOnline(Context.ConnectionId, userId.Value);
        }

        // 当用户离线时在 $connection.stop()后执行
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Session.GetInt32("id");
            if (!userId.HasValue)
                return;
            _chatService.UserOffline(userId.Value);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="targetId">目标用户的id</param>
        /// <param name="myName">我的昵称</param>
        /// <param name="message">消息本体</param>
        public async Task SendMessage(string targetId, string myName, string message)
        {
            var userId = Context.GetHttpContext().Session.GetInt32("id");
            if (!userId.HasValue)
                return;
            if (_chatService.IsOnline(int.Parse(targetId)))
                await Clients
                    .Clients(_chatService.GetUserConnectionId(int.Parse(targetId)))
                    .SendAsync("ReceiveMessage", userId, myName, message);
            else
            {
                await Clients.Client(Context.ConnectionId).SendAsync("NotifyMail");
            }
        }
    }
}