interface SignalR {
    lessonHub: ILessonHubProxy;
    magicGamesHub: IMagicGamesProxy;
}

interface ILessonHubProxy {
    client: ILessonClient;
    //server: ILessonServer;
}

interface ILessonClient {
    add: (lesson: ILessonDto) => void;
    update: (lesson: ILessonDto) => void;
    remove: (id: number) => void;
}

/*interface ILessonServer {
}*/

interface IMagicGamesProxy {
    client: IMagicGamesClient;
}

interface IMagicGamesClient {
    update: (house: IMagicGamesHouseDto) => void;
}