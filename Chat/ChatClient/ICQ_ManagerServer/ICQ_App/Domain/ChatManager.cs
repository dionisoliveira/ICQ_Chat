using System;

using ICQ_AppDomain.Const;
using ICQ_AppDomain.Adpters;
using ICQ_ManagerServer.Interface;

namespace ICQ_AppDomain.Domain
{
    public class ChatManager : IChatManagerService
    {
        private IChatUserDomain _chatUser;
        private IChatGroupDomain _chatGroup;
        private IResponse _response;
        private IICQWebSocket _webSocketServer;


        public ChatManager(IChatUserDomain chatUser,
                           IChatGroupDomain chatGroup,
                           IResponse response)
        {
            _chatUser = chatUser;
            _chatGroup = chatGroup;
            _response = response;
           
        }

     


        public IResponse ProcessMessage(string message, object connectionSocket)
        {
            try
            {
                var command = message.Trim().Split(" ");

                switch (command[0].ToUpper().Trim())
                {
                    case CommandConst.CREATEUSER:
                        return _chatUser.CreateUser(command[1], connectionSocket);

                    case CommandConst.CREATEGROUP:
                        return _chatGroup.CreateNewGroup(command, _chatUser.GetUser(command[command.Length -1]));

                    case CommandConst.CONNECTTOGROUP:
                        return _chatGroup.AddUserInGroup(command, _chatUser.GetUser(command[command.Length - 1]));

                    case CommandConst.CONNECTIONSTABILISHE:
                        return ConnectionStabilished();

                    case CommandConst.GROUPCOMMAND:
                        return GroupCommand(command);

                    case CommandConst.LISTALLGROUP:
                        return _chatGroup.GetAllGroup();

                    case CommandConst.LISTALL:
                        return _chatUser.GetAllUser();

                    case CommandConst.HELPER:
                        return GetHelper();

                    default:
                        return _response.MountMessage(message: $"The command {command[0]} is invalid. User 'Helper' for show command");

                }
            }
            catch (Exception e)
            {
                var messageError = $"Attention error when execute the command {message}";

                Console.WriteLine(e.Message);
                return _response.MountMessage(messageError);
            }
        }

        private IResponse GroupCommand(string[] command)
        {
            try
            {

                switch (command[1].ToUpper().Trim())
                {

                    case CommandConst.LISTALLGROUP:
                        return _chatGroup.GetAllGroup();

                    case CommandConst.EXITGROUP:
                        return _chatGroup.LeaveGroup(command, _chatUser.GetUser(command[3]));

                    case CommandConst.CONNECTIONSTABILISHE:
                        return ConnectionStabilished();
                    case CommandConst.HELPER:
                        return GetHelper();
                    default:
                        return _chatGroup.SendBroadcastGroupMessage(command);

                }
            }
            catch
            {
                throw;
            }
        }




        public IResponse ConnectionStabilished()
        {
            return _response.MountMessage($"Online:" + GetHelper().Message);
        }





        private IResponse GetHelper()
        {

            string helperText = $"{Environment.NewLine} ********COMMAND********{ Environment.NewLine}";
            helperText += $"{CommandConst.CREATEUSER }: Create user - \" UC NAMEUSER \" {Environment.NewLine}";
            helperText += $"{CommandConst.LISTALL }: List all users in server - \"LS\"  {Environment.NewLine}";

            helperText += $"{CommandConst.CREATEGROUP }: Create new group \" GC NAMEGROUP \"  {Environment.NewLine}";
            helperText += $"{CommandConst.LISTALLGROUP }: List all group server - \"LSG\"  {Environment.NewLine}";
            helperText += $"{CommandConst.CONNECTTOGROUP }: Connect user a group - \"JG NAMEGROUP\" {Environment.NewLine}";
           
            helperText += $"{CommandConst.EXITGROUP }: Leave group \" EXIT \"  {Environment.NewLine}";
            helperText += $"{CommandConst.HELPER }: Helper COMMAND \" HELPER \"  {Environment.NewLine}";
            helperText += $"******** USE COMMAND FOR CREATE USER AND GROUP *******************  ";

            return _response.MountMessage(helperText);



        }


    }
}
