using MiNET.Plugins;
using MiNET;
using MiNET.Net;
using MiNET.Plugins.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Timers;
using MiNET.Utils;
using MiNET.Worlds;

namespace ExamplePlugin
{
    [Plugin(PluginName = "ExamplePlugin", Description = "ExamplePlugin", PluginVersion = "1.0", Author = "ComorDev")]
    public class Class1 : Plugin
    {

        protected override void OnEnable()
        {
            base.OnEnable();
            Context.Server.PlayerFactory.PlayerCreated += PlayerCreated;
            Task task = Task.Factory.StartNew(() =>
            {
                Context.Server.StopServer();
            });
            Console.WriteLine("[Example plugin] - loaded");
            Console.WriteLine("[Example plugin] - загрузился");
        }

        private void PlayerCreated(object sender, PlayerEventArgs e)
        {
            var player = e.Player;
            player.PlayerJoin += PlayerJoin;//PLAYER JOIN EVENT
        }

        private void PlayerJoin(object sender, PlayerEventArgs e)
        {
            Player player = e.Player;
                player.SendMessage($"Добро пожаловать на сервер, {player.NameTag}");
                player.SendMessage($"Welcome to the server, {player.NameTag}");
                player.Level.BroadcastMessage($"На сервер зашел игрок {player.NameTag}");
                player.Level.BroadcastMessage($"A player has entered the server {player.NameTag}");
        }
        public override void OnDisable()
        {
            base.OnDisable();
        }

        public void MyClock(object sender, ElapsedEventArgs e)
        {
            Environment.Exit(1);
        }

        //Create command
        [Command(Name = "clearinv", Description = "Clear inventory")]
        public void clearinv(Player player)
        {
            player.Inventory.Clear();
            player.SendMessage("Все ваши предметы удалены из инвенторя!");
            player.SendMessage("All of your items have been removed from your inventory!");
        }

        [Command(Name = "gm", Description = "Gamemode", Permission = "gm.use")]
        public void gm(Player player, int gamemode)
        {
            player.Inventory.Clear();
            player.SetGameMode((GameMode) gamemode);
            player.SendMessage("Изменен режим игры");
            player.SendMessage("Game mode changed!");
        }
    }
}
