namespace TelegramBotApp.RenciServiceSSH.Cisco
{
    public interface ICiscoSSHService
    {
        void CreateNewUserCommand(string username, string password);
        void RemoveUserCommand(string username);
        void RestartUserCommand(string username, string password);
    }
}
