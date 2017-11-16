﻿namespace TelegramBot.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Input;
    using Telegram.Bot;
    using Telegram.Bot.Args;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using TelegramBot.Commands;
    using TelegramBot.Models;


    /// <summary>
    /// This class is responsible for the main window.
    /// </summary>
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static TelegramBotClient Bot;

        public TelegramBotClient BotClient
        {
            get { return Bot; }
        }

        public bool IsActive{
            get
            {
                return Bot.IsReceiving;
            }
        }

        #region Commands
        public ICommand ActivateCommand
        {
            get;
            private set;
        }

        public ICommand DeactivateCommand
        {
            get;
            private set;
        }

        public ICommand OpenFilesFolderCommand
        {
            get;
            private set;
        }

        public ICommand ExitApplicationCommand
        {
            get;
            private set;
        }

        public ICommand ClearLogCommand
        {
            get;
            private set;
        }

        public ICommand OpenLocalFilesCommand
        {
            get;
            private set;
        }
        
        #endregion

        private string _log = "";
        public string Log
        {
            get
            {
                return _log;
            }
            private set
            {
                _log = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Log"));
            }
        }
        public void ClearLog()
        {
            Log = "";
        }
        public MainWindowViewModel()
        {
#if DEBUG
            Bot = new TelegramBotClient("447136859:AAGMz8BN0p21JLO7i9Ob4ridbKTUDpCAD1E");
#else
            Bot = new TelegramBotClient("473027046:AAEWIMqj41Oc33sFIc_yrDcM07XbRDw-Omg");
#endif
            ActivateCommand = new ActivateBotCommand(this);
            DeactivateCommand = new DeactivateBotCommand(this);
            ExitApplicationCommand = new ExitApplicationCommand(this);
            OpenFilesFolderCommand = new OpenFilesFolderCommand(this);
            ClearLogCommand = new ClearLogCommand(this);
            
            if (!Directory.Exists(@"файлы\"))
            {
                Directory.CreateDirectory(@"файлы\");
                OpenFilesFolderCommand.Execute(null);
            }

            OpenLocalFilesCommand = new OpenLocalFilesCommand(this);

            ActivateCommand.Execute(null);
            //CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Activates the bot (hooks MessageUpdate handler and starts receiving)
        /// </summary>
        public void Activate()
        {
            Bot.OnMessage += BotOnMessageReceived;
            Bot.StartReceiving(new UpdateType[] { UpdateType.MessageUpdate });
        }
        /// <summary>
        /// Deactivates the bot (unhooks MessageUpdate handler and stops receiving)
        /// </summary>
        public void Deactivate()
        {
            Bot.StopReceiving();
            Bot.OnMessage -= BotOnMessageReceived;
        }
        /// <summary>
        /// Processes the inbound message
        /// </summary>
        private async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var Rnd = new Random();
            Message msg = e.Message;
            if (msg == null || msg.Type != MessageType.TextMessage)
                return;

            #region Answer logic
            String Answer = "";

            if (msg.Text.StartsWith("/start") || msg.Text.StartsWith("/help"))
            {
                Answer = "Список доступных команд:\r\n" +
                    "/flip - Подбросить монетку\r\n" +
                    "/rand - Случайное число от 1 до 47\r\n" +
                    "/scorebylitvinov - Твоя оценка по Литвинову\r\n" +
                    "/para - Проверь, нужно ли тебе идти на следующую пару\r\n" +
                    "/help - Список всех команд";
            }
            else if (msg.Text.StartsWith("/flip"))
            {
                // Орел и решка
                // для хранения ответа, орел решка
                String flipAnswer = "";
                int Random = Rnd.Next(0, 2);
                if (Random == 0)
                {
                    flipAnswer = "Орел";
                }
                else
                {
                    flipAnswer = "Решка";
                }
                Answer = flipAnswer;
            }
            else if(msg.Text.StartsWith("/rand"))
            {
                // Случайное число
                int Rand = Rnd.Next(0, 48);
                Answer = Rand.ToString();
            }
            else if(msg.Text.StartsWith("/scorebylitvinov"))
            {
                // Узнай свою оценку по Литвинову
                int RandLit = Rnd.Next(1, 11);
                if (RandLit < 9)
                {
                    RandLit = Rnd.Next(1, 66);
                }
                else { RandLit = Rnd.Next(66, 101); }

                if (RandLit < 60) { Answer = "Твоя оценка по Литвинову: " + RandLit.ToString();}
                else { Answer = "Твоя оценка по Литвинову: " + RandLit.ToString() + ", везунчик))"; }

            }
            else if(msg.Text.StartsWith("/para"))
            {
                // Проверка на то, нужно ли идти на пару
                int Random = Rnd.Next(0, 2);
                String paraAnswer = ""; //для хранения ответа, нужно ли идти на пару
                if (Random == 0)//Используем из Орла и решки, так как похожий принцип
                {
                    paraAnswer = "лучше сходить";
                }
                else
                {
                    paraAnswer = "можно не идти";
                }
                
                Answer = "На следующую пару " + paraAnswer;
            }
            else if (msg.Text.Contains("лаб"))
            {
                try
                {
                    using (FileStream stream = FilesAccessor.GetFileByCommand(msg.Text))
                    {
                        await Bot.SendDocumentAsync(msg.Chat.Id, new FileToSend(stream.Name, stream), "Лови", false, msg.MessageId);
                        Log += $"\r\n\r\n{DateTime.Now.ToLocalTime().ToString()}:\r\nCommand received:\r\n{msg.Text}\r\nFrom: {e.Message.From.FirstName} {e.Message.From.LastName}\r\nAnswered with file: {stream.Name}";
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            #endregion

            if (Answer!="")
            {
                await Bot.SendTextMessageAsync(msg.Chat.Id, Answer, ParseMode.Default, false, false, msg.MessageId);
                Log += $"\r\n\r\n{DateTime.Now.ToLocalTime().ToString()}:" +
                    $"\r\nCommand received:" +
                    $"\r\n{e.Message.Text}\r\n" +
                    $"From: {e.Message.From.FirstName} {e.Message.From.LastName}\r\n" +
                    $"Answered with: {Answer}";
               
            }
        }

    }
}
