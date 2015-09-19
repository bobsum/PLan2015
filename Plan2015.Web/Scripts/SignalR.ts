interface SignalR {
    eventHub: IEventHubProxy;
    magicGamesHub: IMagicGamesProxy;
}

interface IEventHubProxy {
    client: IEventClient;
    //server: IEventServer;
}

interface IEventClient {
    add: (event: IEventDto) => void;
    update: (event: IEventDto) => void;
    remove: (id: number) => void;
}

/*interface IEventServer {
}*/

interface IMagicGamesProxy {
    client: IMagicGamesClient;
}

interface IMagicGamesClient {
    update: (house: IMagicGamesHouseDto) => void;
}