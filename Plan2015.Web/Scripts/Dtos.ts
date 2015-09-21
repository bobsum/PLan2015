interface IHouseDto {
    id: number;
    name: string;
}


interface ILessonPointDto {
    id: number;
    houseId: number;
    houseName: string;
    amount: number;
}

interface ILessonDto {
    id: number;
    name: string;
    totalPoints: number;
    points: Array<ILessonPointDto>;
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

interface IMagicGamesScoreDto {
    id: number;
    name: string;
    timePoints: number;
    marker: number;
}