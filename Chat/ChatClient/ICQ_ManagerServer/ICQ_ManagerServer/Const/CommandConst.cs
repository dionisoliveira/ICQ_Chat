using System;
namespace ICQ_ManagerServer.Const
{
    public class CommandConst
    {

        public const string CREATEUSER  = "UC";//Create user in server
        public const string CREATEGROUP = "GC"; // Create group in server
        public const string CONNECTTOGROUP = "JG"; // Connect new user in group

        public const string CONNECTIONSTABILISHE = "9001"; // Conection sucess

        public const string DELETEGROUP = "GD"; // Delete group in server
        public const string LISTALL = "LS"; // Show all users registred
     
        public const string ALLUSERSINGROUP = "LSGU"; // Show all menberrs in group
        public const string MESSAGEGROUP = "GCOMMAND"; // Show all menberrs in group


    }
}
