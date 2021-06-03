using System.Collections.Generic;

namespace RentalServer
{
    public class ChatService
    {
        private readonly Dictionary<int, string> _users = new();

        public void UserOnline(string connectionId, int userId)
        {
            if (_users.ContainsKey(userId))
                _users.Remove(userId);
            _users.TryAdd(userId, connectionId);
        }

        public void UserOffline(int userId)
        {
            _users.Remove(userId);
        }
        
        public bool IsOnline(int userId)
        {
            return _users.ContainsKey(userId);
        }

        public string GetUserConnectionId(int userId)
        {
            return _users.GetValueOrDefault(userId, "");
        }
    }
}