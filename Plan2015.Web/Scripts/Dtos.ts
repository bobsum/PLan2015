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
    visible: boolean;
}

interface IHouseDto {
    id: number;
    name: string;
}

interface IHouseScoreDto {
    id: number;
    name: string;
    amount: number;
    hiddenAmount: number;
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
    start: string;
    stop: string;
    stationId: number;
    stationName: string;
    all: boolean;
}

interface IPunctualityStationDto {
    id: number;
    name: string;
    defaultAll: boolean;
}

interface IPunctualityStatusDto {
    punctuality: IPunctualityDto;
    houses: IPunctualityStatusHouseDto[];
}

interface IPunctualityStatusHouseDto {
    id: number;
    name: string;
    scouts: IPunctualityStatusScoutDto[];
}

interface IPunctualitySwipeDto {
    id: number;
    punctualityId: number;
    rfid: string;
}

interface ISchoolScoreDto {
    id: number;
    name: string;
    houses: IHouseScoreDto[];
}

interface IPunctualityStatusScoutDto {
    id: number;
    name: string;
    arrived: boolean;
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