interface IActivityDto {
    id: number;
    name: string;
    totalPoints: number;
    points: Array<IActivityPointDto>;
}

interface IActivityPointDto {
    id: number;
    houseId: number;
    houseName: string;
    amount: number;
}

interface IHouseDto {
    id: number;
    name: string;
}

interface IHouseScoreDto {
    id: number;
    name: string;
    amount: number;
}

interface IMagicGamesIntervalDto {
    scoutId: number;
    scoutName: string;
    amount: number;
}

interface IMagicGamesMarkerSwipeDto {
    name: string;
    data: string;
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

interface IPunctualityDto {
    id: number;
    name: string;
    deadline: string;
    all: boolean;
}

interface IPunctualityStatusDto {
    houseId: number;
    houseName: string;
    arrived: IScoutDto[];
    missing: IScoutDto[];
}

interface ISchoolScoreDto {
    id: number;
    name: string;
    houses: IHouseScoreDto[];
}

interface IScoutDto {
    id: number;
    name: string;
}

interface ITurnoutSwipeDto {
    name: string;
    data: string;
}

interface ITurnoutPointDto {
    amount: number;
    houseId: number;
    teamMemberId?: number;
}