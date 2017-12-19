using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramBot.Common
{
    /// <summary>
    /// Should perform tasks of processing of incoming Update
    /// by a collection of IBotUpdateHandlers.
    /// </summary>
    public interface IBotUpdateDispatcher
    {
        /// <summary>
        /// Adds update handler to collection of hanlers which
        /// should be checked when Update comes
        /// </summary>
        /// <param name="handler">Handler</param>
        void AddHandler(IBotUpdateHandler handler);

        /// <summary>
        /// Removes update handler so it won't be checked when
        /// Update comes
        /// </summary>
        /// <param name="handler">Handler</param>
        void RemoveHandler(IBotUpdateHandler handler);

        /// <summary>
        /// Handles Update (should be passed as OnUpdate handler
        /// of ITelegramBotClient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void HandleUpdate(object sender, UpdateEventArgs args);
    }
}
