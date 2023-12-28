namespace TelegramBotApp.RenciServiceSSH.OpenVPN
{
    public class OpenVPNCommandService : BaseSShService, IOpenVPNCommandService
    {
        public OpenVPNCommandService(string hostname, int port, string username, string password) : base(hostname, port, username, password)
        {
        }

        public void CreateNewUserCommand(string username,string password)
        {
            var createUserCommand = $"sudo useradd -m {username} && echo {username}:${password} | sudo chpasswd";
            var commands = new List<string>();
            commands.Add(createUserCommand);
            base.ExecuteRemoteCommand(commands);
        }

        public void RemoveUserCommand(string username)
        {
            var RemoveUserCommand = $"sudo userdel -r {username} && rm - rf / home /{username}";
            var commands = new List<string>();
            commands.Add(RemoveUserCommand);
            base.ExecuteRemoteCommand(commands);
        }

        public void RestartUserCommand(string username, string password)
        {
            var RemoveUserCommand = $"sudo userdel -r {username} && rm -rf /home/{username} && sudo useradd -m {username} && echo {username}:${password} | sudo chpasswd";
            var commands = new List<string>();
            commands.Add(RemoveUserCommand);
            base.ExecuteRemoteCommand(commands);
        }
    }
}
