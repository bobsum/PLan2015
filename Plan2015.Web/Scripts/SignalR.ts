interface SignalR {
    activityHub: IActivityHubProxy;
    magicGamesHub: IMagicGamesProxy;
}

interface IActivityHubProxy {
    client: IActivityClient;
    //server: IActivityServer;
}

interface IActivityClient {
    add: (activity: IActivityDto) => void;
    update: (activity: IActivityDto) => void;
    remove: (id: number) => void;
}

/*interface IActivityServer {
}*/

interface IMagicGamesProxy {
    client: IMagicGamesClient;
}

interface IMagicGamesClient {
    update: (house: IMagicGamesHouseDto) => void;
}