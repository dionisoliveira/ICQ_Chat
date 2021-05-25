using System;
using ICQ_AppAplicattion;
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
            var dataInput = new DataInput(command, null);
            var result = _chatManager.ProcessDataReceiver(dataInput);
            Console.Write(result.Message);
            Assert.AreEqual<bool>(true, result.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserTwoTimeSameUser()
        {
            var command = "UC DIONS";
            var dataInput = new DataInput(command, null);
            var result = _chatManager.ProcessDataReceiver(dataInput);

            var resultValidation = _chatManager.ProcessDataReceiver(dataInput);
            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(false, resultValidation.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserAndCreateGroup()
        {
            var commandUser = "UC DIONS";
            var dataInput = new DataInput(commandUser, null);
            var result = _chatManager.ProcessDataReceiver(dataInput);
            var split = result.Message.Split("\\");
            var commandGroup = $"GC TESLA {split[1]}";

            var dataInputCreateGroup = new DataInput(commandGroup, null);

            var resultValidation = _chatManager.ProcessDataReceiver(dataInputCreateGroup);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(true, resultValidation.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserAndJoinInGroupNotExist()
        {
            var commandUser = "UC DIONS";
            var dataInputCreateGroup = new DataInput(commandUser, null);

            var result = _chatManager.ProcessDataReceiver(dataInputCreateGroup);
            var split = result.Message.Split("\\");
            var commandGroup = $"JG VW {split[1]}";


            var dataInputJoinGroup = new DataInput(commandGroup, null);
            var resultValidation = _chatManager.ProcessDataReceiver(dataInputJoinGroup);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(false, resultValidation.IsSucessMessage);
        }

        [TestMethod]
        public void RegisterUserAndCreateGroupAndJoinInGroup()
        {
            var commandUser = "UC DIONS";
            var dataInputCreateUser = new DataInput(commandUser, null);
            var result = _chatManager.ProcessDataReceiver(dataInputCreateUser);

            var paramgroup = result.Message.Split("\\");

            var commandGroup = $"GC FORD {paramgroup[1]}";
            var dataInputCreateGroup = new DataInput(commandGroup, null);
            var resultValidation = _chatManager.ProcessDataReceiver(dataInputCreateGroup);

            var paramJoinGroup = resultValidation.Message.Split("\\");

            var commandJoin = $"JG FORD {paramJoinGroup[1]} {paramJoinGroup[2]}";
            var dataInputJoinGroup = new DataInput(commandJoin, null);
            var resultJG = _chatManager.ProcessDataReceiver(dataInputJoinGroup);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(true, resultJG.IsSucessMessage);
        }

        [TestMethod]
        public void GetHelper()
        {
            var commandUser = "HELPER";
            var dataHelperCommand = new DataInput(commandUser, null);
            var result = _chatManager.ProcessDataReceiver(dataHelperCommand);
            Console.Write(result.Message);
            Assert.AreEqual<bool>(true, result.IsSucessMessage);
        }

        [TestMethod]
        public void LeaveTheGroup()
        {
            var commandUser = "UC DIONS";
            var dataUser = new DataInput(commandUser, null);
            var result = _chatManager.ProcessDataReceiver(dataUser);

            var paramgroup = result.Message.Split("\\");

            var commandGroup = $"GC FORD {paramgroup[1]}";
            var dataGroup = new DataInput(commandGroup, null);
            var resultValidation = _chatManager.ProcessDataReceiver(dataGroup);

            var paramJoinGroup = resultValidation.Message.Split("\\");

            var commandExit = $"GCOMMAND EXIT {paramJoinGroup[1]} {paramJoinGroup[2]}";
            var grouCommandExit = new DataInput(commandExit, null);

            var resultLeaveTheGroup = _chatManager.ProcessDataReceiver(grouCommandExit);

            Console.Write(resultValidation.Message);
            Assert.AreEqual<bool>(true, resultLeaveTheGroup.IsSucessMessage);
        }



    }
}
