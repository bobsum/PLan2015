interface IHouseDto {
    id: number;
    name: string;
}

interface IEventPointDto {
    id: number;
    houseId: number;
    houseName: string;
    amount: number;
}

interface IEventDto {
    id: number;
    name: string;
    totalPoints: number;
    points: Array<IEventPointDto>;
}

interface IMagicGamesIntervalDto {
    scoutId: number;
    scoutName: string;
    amount: number;
}

interface IMagicGamesHouseDto {
    houseId: number;
    houseName: string;
    intervals: IMagicGamesIntervalDto[];
}