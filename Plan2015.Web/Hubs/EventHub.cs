﻿using Microsoft.AspNet.SignalR;

namespace Plan2015.Web.Hubs
{
    public class EventHub : Hub<IEventClient>
    {
    }
}