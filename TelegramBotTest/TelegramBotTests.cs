using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelegramBot;
using TelegramBot.Views;
using TelegramBot.ViewModels;

namespace TelegramBotTest
{
    [TestClass]
    public class TelegramBotTests
    {
        [TestMethod]
        public void TelegramBotViewModel_Activate_IfEverythingIsRight()
        {
            MainWindowViewModel model = new MainWindowViewModel();
            model.Activate();
            Assert.IsTrue(model.BotClient.IsReceiving);
            model.Deactivate();
        }

        [TestMethod]
        public void TelegramBotViewModel_Deactivate_IfEverythingIsRight()
        {
            MainWindowViewModel model = new MainWindowViewModel();
            model.Activate();
            Assert.IsTrue(model.BotClient.IsReceiving);
            model.Deactivate();
        }
    }
}
