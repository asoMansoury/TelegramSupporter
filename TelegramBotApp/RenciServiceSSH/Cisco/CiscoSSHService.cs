namespace TelegramBotApp.RenciServiceSSH.Cisco
{
    public class CiscoSSHService : BaseSShService, ICiscoSSHService
    {
        public CiscoSSHService(string hostname, int port, string username, string password) : base(hostname, port, username, password)
        {
        }
        public void CreateNewUserCommand(string username, string password)
        {
            var createUserCommand = $"sudo echo {password.Trim()} | sudo ocpasswd -c /etc/ocserv/ocpasswd {username.Trim()}";
            var commands = new List<string>();
            commands.Add(createUserCommand);
            base.ExecuteRemoteCommand(commands);
        }

        public void RemoveUserCommand(string username)
        {
            var RemoveUserCommand = $"sudo ocpasswd -c /etc/ocserv/ocpasswd -d  {username}";
            var commands = new List<string>();
            commands.Add(RemoveUserCommand);
            base.ExecuteRemoteCommand(commands);
        }

        public void RestartUserCommand(string username, string password)
        {
            var createUserCommand = $"sudo ocpasswd -c /etc/ocserv/ocpasswd -d  {username} && sudo echo {password.Trim()} | sudo ocpasswd -c /etc/ocserv/ocpasswd {username.Trim()}";
            var commands = new List<string>();
            commands.Add(createUserCommand);
            base.ExecuteRemoteCommand(commands);
        }
    }
}
