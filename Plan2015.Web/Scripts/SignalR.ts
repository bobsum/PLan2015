interface SignalR {
    activityHub: IActivityHubProxy;
    magicGamesSetupHub: IMagicGamesSetupHubProxy;
    punctualityHub: IPunctualityHubProxy;
    scoreHub: IScoreHubProxy;
    turnoutPointHub: ITurnoutPointHubProxy;
}

interface IActivityHubProxy {
    client: IActivityHubClient;
}

interface IActivityHubClient {
    add: (activity: IActivityDto) => void;
    update: (activity: IActivityDto) => void;
    remove: (id: number) => void;
}

interface IMagicGamesSetupHubProxy {
    client: IMagicGamesSetupHubClient;
}

interface IMagicGamesSetupHubClient {
    update: (house: IMagicGamesSetupDto) => void;
}

interface IPunctualityHubProxy {
    client: IPunctualityHubClient;
    server: IPunctualityHubServer;
}

interface IPunctualityHubClient {
    add: (punctuality: IPunctualityDto) => void;
    remove: (id: number) => void;
    updatedStatus: (status: IPunctualityStatusHouseDto[]) => void;
}

interface IPunctualityHubServer {
    setId: (newId: number, oldId: number) => void
}

interface IScoreHubProxy {
    client: IScoreHubClient;
}

interface IScoreHubClient {
    updated: (status: ISchoolScoreDto[]) => void
}

interface ITurnoutPointHubProxy {
    client: ITurnoutPointHubClient;
}

interface ITurnoutPointHubClient {
    add: (turnoutPoint: ITurnoutPointDto) => void;
    remove: (id: number) => void ;
}