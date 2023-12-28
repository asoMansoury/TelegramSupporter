namespace TelegramBotApp.RenciServiceSSH.OpenVPN
{
    public interface IOpenVPNCommandService
    {
        
        void CreateNewUserCommand(string username,string password);
        void RemoveUserCommand(string username);
        void RestartUserCommand(string username, string password);
    }
}
