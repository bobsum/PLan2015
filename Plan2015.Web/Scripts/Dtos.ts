interface IHouseDto {
    id: number;
    name: string;
}

interface IActivityPointDto {
    id: number;
    houseId: number;
    houseName: string;
    amount: number;
}

interface IActivityDto {
    id: number;
    name: string;
    totalPoints: number;
    points: Array<IActivityPointDto>;
}

interface IMagicGamesIntervalDto {
    scoutId: number;
    scoutName: string;
    amount: number;
}

interface IMagicGamesSetupDto {
    houseId: number;
    houseName: string;
    intervals: IMagicGamesIntervalDto[];
}

interface IMagicGamesScoreDto {
    id: number;
    name: string;
    timePoints: number;
    marker: number;
}