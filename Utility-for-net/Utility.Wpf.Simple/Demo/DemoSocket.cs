using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Net.Sockets;
using Utility.Wpf.Demo.ViewModels;

namespace Utility.Wpf.Demo
{
    public  class DemoSocket
    {
        public static SocketClient Client;
        public static void Init()
        {
            //10m
            Client = new SocketClient(1024*1024*10);
            var server = System.Configuration.ConfigurationManager.AppSettings["SocketServer"];
            var client = System.Configuration.ConfigurationManager.AppSettings["SocketClient"];
            var servers = server.Split(':');
            var clients= client.Split(':');
            Client.BindServer(new IPEndPoint(IPAddress.Parse(servers[0]),int.Parse(servers[1])));
            Client.Start(new IPEndPoint(IPAddress.Parse(clients[0]), int.Parse(clients[1])));

        }
        public virtual ResultDto<AddressViewModel> Addresses()
        {
            return null;
        }
        public virtual ResultDto<BankViewModel> Banks()
        {
            return null;
        }
        public virtual ResultDto<TokenViewModel> Tokens()
        {
            return null;
        }
        public virtual ResultDto<FriendViewModel> Friends()
        {
            return null;
        }
        public virtual ResultDto<CityViewModel> Citys()
        {
            return null;
        }
        public virtual ResultDto<MenuViewModel> Menus()
        {
            return null;
        }
        public virtual ResultDto<SourceMaterialViewModel> SourceMaterials()
        {
            return null;
        }
        public virtual ResultDto<UserViewModel> Users()
        {
            return null;
        }
        public virtual ResultDto<UserLogViewModel> UserLogs()
        {
            return null;
        }
        public virtual ResultDto<AdminViewModel> Admins()
        {
            return null;
        }
    }
}
