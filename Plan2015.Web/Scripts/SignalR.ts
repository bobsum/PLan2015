interface SignalR {
    activityHub: IActivityHubProxy;
    magicGamesSetupHub: IMagicGamesSetupProxy;
    punctualityHub: IPunctualityProxy;
    punctualityStatusHub: IPunctualityStatusProxy;
    scoreHub: IScoreProxy;
    turnoutPointHub: ITurnoutPointProxy;
}

interface IActivityHubProxy {
    client: IActivityClient;
}

interface IActivityClient {
    add: (activity: IActivityDto) => void;
    update: (activity: IActivityDto) => void;
    remove: (id: number) => void;
}

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

interface IPunctualityStatusServer {
    setId: (id: number) => void
}

interface IPunctualityStatusProxy {
    client: IPunctualityStatusClient;
    server: IPunctualityStatusServer;
}

interface IPunctualityStatusClient {
    updated: (status: IPunctualityStatusDto[]) => void
}

interface IScoreProxy {
    client: IScoreClient;
}

interface IScoreClient {
    updated: (status: ISchoolScoreDto[]) => void
}

interface ITurnoutPointProxy {
    client: ITurnoutPointClient;
}

interface ITurnoutPointClient {
    add: (turnoutPoint: ITurnoutPointDto) => void;
}