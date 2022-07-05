using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace H_W_10._5_WpfApp
{
    class TelegramMessageClient
    {
        public MainWindow w;

        public TelegramBotClient bot;

        public ObservableCollection<MessageLog> BotMessageLog { get; set; }

        public async Task MessageListener(object sender, Update update, System.Threading.CancellationToken arg3)
        {
            using (StreamWriter sw = new StreamWriter("История Чата.json", true))
            {
                for (int i = 0; i < BotMessageLog.Count;)
                {
                    sw.WriteLine(update.Message.Text);
                    break;
                }
            }

            Debug.WriteLine("-");

            string text = $"{DateTime.Now.ToLongTimeString()}: " +
                          $"{update.Message.Chat.FirstName} " +
                          $"{update.Message.Chat.Id} " +
                          $"{update.Message.Text}";

            Debug.WriteLine($"{text} TypeMessage {update.Message.Type.ToString()}");

            if (update.Message.Text == null) return;

            var messageText = update.Message.Text;

            w.Dispatcher.Invoke(() =>
            {
                BotMessageLog.Add(
                    new MessageLog(
                        messageText, update.Message.Chat.FirstName, DateTime.Now.ToLongTimeString(), update.Message.Chat.Id));
            });
        }

        public TelegramMessageClient(MainWindow w, string PathToken = @"")
        {
            this.BotMessageLog = new ObservableCollection<MessageLog>();

            this.w = w;

            bot = new TelegramBotClient(System.IO.File.ReadAllText(PathToken));

            var cts = new System.Threading.CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new Telegram.Bot.Extensions.Polling.ReceiverOptions
            {
                AllowedUpdates = { }
            };
            bot.StartReceiving(
                MessageListener,
                ErrorHandler,
                receiverOptions,
                cancellationToken
            );

            bot.StartReceiving(MessageListener, ErrorHandler, receiverOptions);

            cts.Cancel();

            cts.Dispose();
        }

        static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, System.Threading.CancellationToken arg3)
        {
            var ErrorMessage = exception.Message;
            {
                ApiRequestException apiRequestException, apiRequestException1, requestException;
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public async Task SendMessage(string Text, string Id)
        {
            long id = long.Parse(Id);

            await bot.SendTextMessageAsync(id, Text);

            using (StreamWriter sw = new StreamWriter("История Чата.json", true))
            {
                for (int i = 0; i < id;)
                {
                    sw.WriteLine(Text, Id);
                    break;
                }
            }
        }
    }
}
