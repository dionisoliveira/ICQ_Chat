using System;
using ICQ_ManagerServer;
using ICQ_ManagerServer.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IC_AppTest
{
    [TestClass]
    public class ChatManagerTest
    {
        private static ServiceProvider _container;
        private IChatManagerService _chatManager;


        public ChatManagerTest()
        {
            _container = new IoCRegister().InitIoC();
            _chatManager = _container.GetService<IChatManagerService>();
        }
        [TestMethod]
        public void RegisterUser()
        {
            var command = "UC DIONS";
            var result = _chatManager.ProcessMessage(command, null);
            Console.Write(result.Message);
            Assert.AreEqual<bool>(true, result.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserTwoTimeSameUser()
        {
            var command = "UC DIONS";
            var result = _chatManager.ProcessMessage(command, null);
            var resultValidation = _chatManager.ProcessMessage(command, null);
            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(false, resultValidation.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserAndCreateGroup()
        {
            var commandUser = "UC DIONS";
            var result = _chatManager.ProcessMessage(commandUser, null);
            var split = result.Message.Split("\\");
            var commandGroup = $"GC TESLA {split[1]}";

            var resultValidation = _chatManager.ProcessMessage(commandGroup, null);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(true, resultValidation.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserAndJoinInGroupNotExist()
        {
            var commandUser = "UC DIONS";
            var result = _chatManager.ProcessMessage(commandUser, null);
            var split = result.Message.Split("\\");
            var commandGroup = $"JG VW {split[1]}";

            var resultValidation = _chatManager.ProcessMessage(commandGroup, null);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(false, resultValidation.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserAndCreateGroupAndJoinInGroup()
        {
            var commandUser = "UC DIONS";
            var result = _chatManager.ProcessMessage(commandUser, null);
            var paramgroup = result.Message.Split("\\");
            var commandGroup = $"GC FORD {paramgroup[1]}";

            var resultValidation = _chatManager.ProcessMessage(commandGroup, null);
            var paramJoinGroup = resultValidation.Message.Split("\\");
            var commandJoin = $"JG FORD {paramJoinGroup[1]} {paramJoinGroup[2]}";
            var resultJG = _chatManager.ProcessMessage(commandJoin, null);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(true, resultJG.IsSucessMessage);
        }

        [TestMethod]
        public void GetHelper()
        {
            var commandUser = "HELPER";
            var result = _chatManager.ProcessMessage(commandUser, null);
         

            Console.Write(result.Message);
            Assert.AreEqual<bool>(true, result.IsSucessMessage);
        }

        [TestMethod]
        public void LeaveTheGroup()
        {
            var commandUser = "UC DIONS";
            var result = _chatManager.ProcessMessage(commandUser, null);
            var paramgroup = result.Message.Split("\\");
            var commandGroup = $"GC FORD {paramgroup[1]}";

            var resultValidation = _chatManager.ProcessMessage(commandGroup, null);
            var paramJoinGroup = resultValidation.Message.Split("\\");
            var commandJoin = $"GCOMMAND EXIT {paramJoinGroup[1]} {paramJoinGroup[2]}";
            var resultJG = _chatManager.ProcessMessage(commandJoin, null);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(true, resultJG.IsSucessMessage);
        }


        [TestMethod]
        public void JoinTheGroup()
        {
            var commandUser = "UC DIONS";
            var result = _chatManager.ProcessMessage(commandUser, null);

            var userJoin = "UC PEDRO";
            var resultJoin = _chatManager.ProcessMessage(userJoin, null);

            var paramgroup = result.Message.Split("\\");
            var commandGroup = $"GC FORD {paramgroup[1]}";
            var createGroup = _chatManager.ProcessMessage(commandGroup, null);

            var resultJG = createGroup.Message.Split("\\");
            var joinCommand = $"JG FORD {paramgroup[1]}";

            var resultValidation = _chatManager.ProcessMessage(joinCommand, null);

           
            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(true, resultValidation.IsSucessMessage);
        }
    }
}
