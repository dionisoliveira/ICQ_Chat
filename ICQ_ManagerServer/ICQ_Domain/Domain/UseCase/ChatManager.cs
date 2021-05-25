using System;
using ICQ_AppDomain.Const;
using ICQ_AppDomain.Domain.Entities.Interface;
using ICQ_AppDomain.Entities;
using ICQ_ManagerServer.Interface;

namespace ICQ_AppDomain.Domain
{
    public class ChatManager : IChatManagerService
    {
        #region PRIVATE
        private IChatUserDomain _chatUser;
        private IChatGroupDomain _chatGroup;

        #endregion

        #region CONSTRUTOR

        public ChatManager(IChatUserDomain chatUser,
                           IChatGroupDomain chatGroup)
        {
            _chatUser = chatUser;
            _chatGroup = chatGroup;


        }
        #endregion

        #region METHOD


        public IResponse ProcessDataReceiver(IDataReceiver dataReceiver)
        {
            try
            {
                var command = dataReceiver.Message.Trim().Split(" ");

                switch (command[0].ToUpper().Trim())
                {
                    case CommandConst.LISTALL:
                    case CommandConst.CREATEUSER:
                    case CommandConst.DM:
                        return _chatUser.ProcessUserData(dataReceiver);

                    case CommandConst.CONNECTIONSTABILISHE:
                        return ConnectionStabilished(dataReceiver);


                    case CommandConst.GROUPCOMMAND:
                    case CommandConst.LISTALLGROUP:
                    case CommandConst.ALLUSERSINGROUP:
                    case CommandConst.CONNECTTOGROUP:
                    case CommandConst.CREATEGROUP:
                    case CommandConst.EXITGROUP:
                        return _chatGroup.ProcessChatData(dataReceiver, _chatUser.GetUser(dataReceiver));

                    case CommandConst.HELPER:
                        return GetHelper(dataReceiver);

                    default:
                        return new Response(message: $"The command {command[0]} is invalid. User 'Helper' for show command");

                }
            }
            catch (Exception e)
            {
                var messageError = $"Attention error when execute the command {dataReceiver.Message}";

                Console.WriteLine(e.Message);
                return new Response(messageError);
            }
        }


        public IResponse ConnectionStabilished(IDataReceiver dataReceiver)
        {

            return new Response($"Online:" + GetHelper(dataReceiver).Message, dataReceiver.Socket);


        }

        private IResponse GetHelper(IDataReceiver dataReceiver)
        {
            if (dataReceiver is null)
            {
                throw new ArgumentNullException(nameof(dataReceiver));
            }

            string helperText = $"{Environment.NewLine} ********COMMAND********{ Environment.NewLine}";
            helperText += $"{CommandConst.CREATEUSER }: Create user - \" UC NAMEUSER \" {Environment.NewLine}";
            helperText += $"{CommandConst.LISTALL }: List all users in server - \"LS\"  {Environment.NewLine}";
            helperText += $"{CommandConst.DM }: Use DM command send direct \" DM NAMEUSER YOURMESSAGE \"  {Environment.NewLine}";

            helperText += $"{CommandConst.CREATEGROUP }: Create new group \" GC NAMEGROUP \"  {Environment.NewLine}";
            helperText += $"{CommandConst.LISTALLGROUP }: List all group server - \"LSG\"  {Environment.NewLine}";
            helperText += $"{CommandConst.CONNECTTOGROUP }: Connect user a group - \"JG NAMEGROUP\" {Environment.NewLine}";

            helperText += $"{CommandConst.EXITGROUP }: Leave group \" EXIT \"  {Environment.NewLine}";

            helperText += $"{CommandConst.HELPER }: Helper COMMAND \" HELPER \"  {Environment.NewLine}";
            helperText += $"******** USE COMMAND FOR CREATE USER AND GROUP *******************  ";

            var response = new Response(helperText, dataReceiver.Socket);

            return response;
        }

        #endregion


    }
}
