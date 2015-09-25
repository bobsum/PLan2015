interface SignalR {
    activityHub: IActivityHubProxy;
    magicGamesSetupHub: IMagicGamesSetupProxy;
    punctualityHub: IPunctualityProxy;
    turnoutPointHub: ITurnoutPointProxy;
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

interface IMagicGamesSetupProxy {
    client: IMagicGamesSetupClient;
}

interface IMagicGamesSetupClient {
    update: (house: IMagicGamesSetupDto) => void;
}

interface IPunctualityProxy {
    client: IPunctualityClient;
}

interface IPunctualityClient {
    add: (punctuality: IPunctualityDto) => void;
    remove: (id: number) => void;
}

interface ITurnoutPointProxy {
    client: ITurnoutPointClient;
}

interface ITurnoutPointClient {
    add: (turnoutPoint: ITurnoutPointDto) => void;
}