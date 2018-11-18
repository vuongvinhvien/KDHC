using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Store.Data.DataDbContext;
using System.Net;
using System.Collections;
using Store.Data.Repositories;
using Microsoft.AspNet.Identity;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
        private RepositoryBase<chatclient> _ChatClient; private RepositoryBase<chatagent2> _Chatagent; private RepositoryBase<Visitor> _ChatVisitor; private RepositoryBase<ChatLine> _LineChat;
        public ChatHub()
        { DataChatBox data = new DataChatBox(); UnitOfWork _Unit = new UnitOfWork(data); _ChatClient = new RepositoryBase<chatclient>(_Unit); _Chatagent = new RepositoryBase<chatagent2>(_Unit); _ChatVisitor = new RepositoryBase<Visitor>(_Unit); _LineChat = new RepositoryBase<ChatLine>(_Unit); }
        public void clientconnect(string name, string email, string sdt, string idsignlr, string idurl, string urladd, string ipmac, string iplocal, string CountTime, string Curmail)
        {
            try
            {
                chatclient client = new chatclient(); client.ip = ipmac; client.name = name; client.sdt = sdt; client.email = email; client.idchat = idurl; client.urlchat = idsignlr; client.value2 = Curmail; client.value3 = iplocal; client.value4 = CountTime; client.addweb = urladd; _ChatClient.Add(client); var listagent = _Chatagent.GetAll().Where(x => x.idchat == idurl); if (listagent.Count() > 0)
                    Clients.Client(idsignlr).chatwithus();
                else
                    Clients.Client(idsignlr).chatwithusoff(); foreach (var agents in listagent)
                { Clients.Client(agents.urlchat).clientonline(name, ipmac, urladd, ipmac + "bb", idsignlr, email, sdt, iplocal, CountTime); }
            }
            catch { }
        }
        public void agentconnect(string name, string email, string idsignlr, string idurl)
        {
            IEnumerable<chatagent2> listagent = _Chatagent.GetAll().Where(x => x.email == email); chatagent2 agent2 = new chatagent2 { name = name, email = email, idchat = idurl, urlchat = idsignlr }; _Chatagent.Add(agent2); IEnumerable<chatclient> listclient = _ChatClient.GetAll().Where(x => x.idchat == idurl); foreach (var clients in listclient)
            { Clients.Client(Context.ConnectionId).updateclientonline(clients.name, clients.email, clients.sdt, clients.ip, clients.addweb, clients.ip + "bb", clients.urlchat, clients.value1, clients.value2, clients.value3, clients.value4); Clients.Client(clients.urlchat).updatereconnect(email); }
            foreach (var agnets in listagent)
            { Clients.Client(agnets.urlchat).disconnectsameagnt(); }
        }
        public void messclient(string idsignlr, string idurl, string name, string message, string ip, string emailAgent, string idagent)
        {
            var ID_Visitor = ip; if (_ChatVisitor.GetById(ID_Visitor) == null)
            {
                _ChatVisitor.Add(new Visitor { ID = ID_Visitor, User_name = name }); _LineChat.Add(new ChatLine { Actor = true, Date = DateTime.Now, Line = message, Agent = string.IsNullOrEmpty(idagent) ? null : idagent, Visitor = ID_Visitor, ID_Customer = idurl });
            }
            else
            {
                _LineChat.Add(new ChatLine { Actor = true, Date = DateTime.Now, Line = message, Agent = string.IsNullOrEmpty(idagent) ? null : idagent, Visitor = ID_Visitor, ID_Customer = idurl });
            }
            IEnumerable<chatagent2> listagent = new List<chatagent2>(); if (string.IsNullOrEmpty(emailAgent))
                listagent = _Chatagent.GetAll().Where(x => x.idchat == idurl);
            else
            {
                listagent = _Chatagent.GetAll().Where(x => x.idchat == idurl && x.email == emailAgent); if (listagent.Count() == 0)
                    listagent = _Chatagent.GetAll().Where(x => x.idchat == idurl);
            }
            foreach (var agents in listagent)
            { Clients.Client(agents.urlchat).broadcastMessage(idsignlr, name, message); }
        }
        public void messagent(string idroom, string name, string message, string IDAgent, string IDdashboard,string emailagent)
        {
           
            IEnumerable<chatclient> listclients = _ChatClient.GetAll().Where(x => x.ip + "bb" == idroom); var ID_Visitor = idroom.Replace("bb", ""); if (_ChatVisitor.GetById(ID_Visitor) == null)
            { _ChatVisitor.Add(new Visitor { ID = ID_Visitor }); }
            try
            {
                _LineChat.Add(new ChatLine { Actor = false, Date = DateTime.Now, Line = message, ID_Customer = IDdashboard, Agent = IDAgent, Visitor = ID_Visitor });
            }
            catch
            { }
            foreach (var clients in listclients)
            { Clients.Client(clients.urlchat).broadcastMessage(name, message, IDAgent); }
            sendsameagent(IDdashboard, "tab_web" + idroom, message, name, emailagent);
        }
        public void tranferagent(string email, string urlchat, string messtranfer, string nameclienttranfer)
        {
            urlchat = urlchat.Remove(urlchat.Length - 2); urlchat = urlchat.Replace("tab_web", ""); IEnumerable<chatclient> listclient = _ChatClient.GetAll().Where(x => x.ip == urlchat); chatagent2 agents = _Chatagent.GetAll().Where(x => x.email == email).FirstOrDefault(); foreach (var clients in listclient)
            {
                if (clients.value1 != "")
                { Clients.Client(clients.urlchat).tranferagen(email); clients.value2 = email; clients.value1 = agents.urlchat; _ChatClient.Update(clients); }
            }
            Clients.Client(agents.urlchat).tranferagentclient(email, urlchat, messtranfer, nameclienttranfer);
        }
        public void sendsameagent(string idurl, string ip, string mess, string nameag, string email)
        {
            var urlchat = ip.Remove(ip.Length - 2); urlchat = urlchat.Replace("tab_web", ""); IEnumerable<chatclient> listclient = _ChatClient.GetAll().Where(x => x.ip == urlchat); foreach (var clients in listclient)
            {
                if (clients.value1 != "")
                { clients.value1 = Context.ConnectionId; clients.value2 = email; _ChatClient.Update(clients); }
            }
            IEnumerable<chatagent2> listagent = _Chatagent.GetAll().Where(x => x.idchat == idurl && x.email != email); foreach (var agents in listagent)
            { Clients.Client(agents.urlchat).massesameganet(idurl, ip, mess, nameag, Context.ConnectionId); }
        }
        public void getagentonline(string idurl, string emailagent)
        {
            IEnumerable<chatagent2> listagent = _Chatagent.GetAll().Where(x => x.idchat == idurl && x.urlchat != Context.ConnectionId && x.email != emailagent); foreach (var agents in listagent)
            { Clients.Client(Context.ConnectionId).alertagent(agents.urlchat, agents.email); }
        }
        public override Task OnDisconnected(bool stop)
        {
            try
            {
                chatclient client = new chatclient(); client = _ChatClient.GetAll().Where(x => x.urlchat == Context.ConnectionId).FirstOrDefault(); _ChatClient.Remove(client); IEnumerable<chatagent2> listagent = _Chatagent.GetAll().Where(x => x.idchat == client.idchat); foreach (var agents in listagent)
                { Clients.Client(agents.urlchat).clientoffline(client.urlchat); }
                return base.OnDisconnected(stop);
            }
            catch
            { }
            try
            {
                chatagent2 agent = new chatagent2(); agent = _Chatagent.GetAll().Where(x => x.urlchat == Context.ConnectionId).FirstOrDefault(); _Chatagent.Remove(agent); IEnumerable<chatagent2> listagent = _Chatagent.GetAll().Where(x => x.idchat == agent.idchat && x.urlchat != Context.ConnectionId); foreach (var agents in listagent)
                { Clients.Client(agents.urlchat).agentoff(agent.urlchat, agent.email, Context.ConnectionId); }
                string clientsemail = _ChatClient.GetAll().Where(x => x.value1 == Context.ConnectionId).SingleOrDefault().value2; IEnumerable<chatclient> clients = _ChatClient.GetAll().Where(x => x.value2 == clientsemail); foreach (var client in clients)
                {
                    Clients.Client(client.urlchat).agentoffline(agent.email); if (client.value1 != null)
                    { client.value1 = null; client.value2 = null; _ChatClient.Update(client); }
                }
            }
            catch
            { }
            return base.OnDisconnected(stop);
        }
    }
}