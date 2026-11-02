type TrackInfo = {
    name: string;
    trackLayout: string;
    lapLengthM: number;
};

type CarInfo = {
    name: string;
    className: string;
};

type PitStopInfo = {
    stationaryTimeS: number;
    fuelAddedL: number;
    tyresChanged: boolean;
    driverSwapped: boolean;
    previousDriverName: string;
    newDriverName: string
};

type MotionInfo = {
    speedKph: number;
    rpm: number;
    gear: number;
};

type InputsInfo = {
    throttle: number;
    brake: number;
    clutch: number;
    steering: number;
};

type TyreInfo = {
    frontCompound: string;
    rearCompound: string;
};

type SessionStartMessage = {
    type: "sessionStart";
    sessionId: string;
    startedAt: string;
    sessionType: number;
    track: TrackInfo;
    car: CarInfo;
};

type LapCompletedMessage = {
    type: "lapCompleted";
    sessionId: string;
    lapNumber: number;
    valid: boolean;
    driverName: string;
    lapTimeS: number;
    bestLapTimeS: number;
    fuelUsedL: number;
    pitStop: PitStopInfo | null;
};

type TickSample = {
    elapsedTimeS: number;
    lapNumber: number;
    lapDistanceM: number;
    position: number;
    motion: MotionInfo;
    inputs: InputsInfo;
    fuelL: number;
    tyres: TyreInfo;
};


type TickBatchMessage = {
    type: "tickBatch";
    sessionId: string;
    batchSeq: number;
    samples: TickSample[];
}

export type {
    TrackInfo,
    CarInfo,
    PitStopInfo,
    MotionInfo,
    InputsInfo,
    TyreInfo,
    SessionStartMessage,
    LapCompletedMessage,
    TickSample,
    TickBatchMessage
};
