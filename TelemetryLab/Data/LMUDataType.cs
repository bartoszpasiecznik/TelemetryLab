using System.Runtime.InteropServices;

namespace TelemetryLab.Data;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct SharedMemoryScoringData { // Remember to check CopySharedMemoryObj still works properly when updating this struct
    public readonly ScoringInfoV01 scoringInfo;
    public readonly ulong scoringStreamSize;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_MAPPED_VEHICLES)]
    public readonly VehicleScoringInfoV01[] vehScoringInfo; // MUST NOT BE MOVED!
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65536)]
    public readonly char[] scoringStream;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct SharedMemoryTelemtryData { // Remember to check CopySharedMemoryObj still works properly when updating this struct
    public readonly byte activeVehicles;
    public readonly byte playerVehicleIdx;
    public readonly byte playerHasVehicle;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_MAPPED_VEHICLES)]
    public readonly TelemInfoV01[] telemInfo;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct SharedMemoryPathData {
    // MAX_PATH = 260
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
    public readonly char[] userData;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
    public readonly char[] customVariables;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
    public readonly char[] stewardResults;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
    public readonly char[] playerProfile;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
    public readonly char[] pluginsFolder;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct SharedMemoryGeneric {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public readonly SharedMemoryEvent[] events;
    public readonly long gameVersion;
    public readonly float FFBTorque;
    public readonly ApplicationStateV01 appInfo;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct SharedMemoryObjectOut { // Remember to check CopySharedMemoryObj still works properly when updating this struct
    public readonly SharedMemoryGeneric generic;
    public readonly SharedMemoryPathData paths;
    public readonly SharedMemoryScoringData scoring;
    public readonly SharedMemoryTelemtryData telemetry;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct SharedMemoryLayout {
    public readonly SharedMemoryObjectOut data;
};

public enum SharedMemoryEvent : uint {
    SME_ENTER,
    SME_EXIT,
    SME_STARTUP,
    SME_SHUTDOWN,
    SME_LOAD,
    SME_UNLOAD,
    SME_START_SESSION,
    SME_END_SESSION,
    SME_ENTER_REALTIME,
    SME_EXIT_REALTIME,
    SME_UPDATE_SCORING,
    SME_UPDATE_TELEMETRY,
    SME_INIT_APPLICATION,
    SME_UNINIT_APPLICATION,
    SME_SET_ENVIRONMENT,
    SME_FFB,
    // SME_MAX
};

public enum IP_VehicleClass : byte {
  Hypercar = 0x00,
  LMP2_ELMS = 0x02,
  LMP2,
  LMP3,
  GTE,
  GT3,
  PaceCar = 0x08,
  Unknown = 0xFF
};

public enum IP_VehicleChampionship : byte {
  WEC_2023 = 0x00, WEC_2024, WEC_2025, WEC_2026,
  ELMS_2025 = 0X10, ELMS_2026,
  Unknown = 0xFF
};

//#########################################################################
//# Version01 public readonly structures                                                   #
//##########################################################################
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct TelemVect3
{
  public double x;
  public double y;
  public double z;
    
  public double this[int i]
  {
    get
    {
      switch (i)
      {
        case 0: return x;
        case 1: return y;
        case 2: return z;
        default: throw new IndexOutOfRangeException();
      }
    }
    set
    {
      switch (i)
      {
        case 0: x = value; break;
        case 1: y = value; break;
        case 2: z = value; break;
        default: throw new IndexOutOfRangeException();
      }
    }
  }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct TelemWheelV01
{
  public readonly double mSuspensionDeflection;  // meters
  public readonly double mRideHeight;            // meters
  public readonly double mSuspForce;             // pushrod load in Newtons
  public readonly double mBrakeTemp;             // Celsius
  public readonly double mBrakePressure;         // currently 0.0-1.0, depending on driver input and brake balance; will convert to true brake pressure (kPa) in future

  public readonly double mRotation;              // radians/sec
  public readonly double mLateralPatchVel;       // lateral velocity at contact patch
  public readonly double mLongitudinalPatchVel;  // longitudinal velocity at contact patch
  public readonly double mLateralGroundVel;      // lateral velocity at contact patch
  public readonly double mLongitudinalGroundVel; // longitudinal velocity at contact patch
  public readonly double mCamber;                // radians (positive is left for left-side wheels, right for right-side wheels)
  public readonly double mLateralForce;          // Newtons
  public readonly double mLongitudinalForce;     // Newtons
  public readonly double mTireLoad;              // NewtonsH

  public readonly double mGripFract;             // an approximation of what fraction of the contact patch is sliding
  public readonly double mPressure;              // kPa (tire pressure)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public readonly double[] mTemperature;        // Kelvin (subtract 273.15 to get Celsius), left/center/right (not to be confused with inside/center/outside!)
  public readonly double mWear;                  // wear (0.0-1.0, fraction of maximum) ... this is not necessarily proportional with grip loss
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
  public readonly char[] mTerrainName;         // the material prefixes from the TDF file
  public readonly byte mSurfaceType;    // 0=dry, 1=wet, 2=grass, 3=dirt, 4=gravel, 5=rumblestrip, 6=special
  public readonly byte mFlat;                    // whether tire is flat
  public readonly byte mDetached;                // whether wheel is detached
  public readonly byte mStaticUndeflectedRadius; // tire radius in centimeters

  public readonly double mVerticalTireDeflection;// how much is tire deflected from its (speed-sensitive) radius
  public readonly double mWheelYLocation;        // wheel's y location relative to vehicle y location
  public readonly double mToe;                   // current toe angle w.r.t. the vehicle

  public readonly double mTireCarcassTemperature;       // rough average of temperature samples from carcass (Kelvin)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public readonly double[] mTireInnerLayerTemperature; // rough average of temperature samples from innermost layer of rubber (before carcass) (Kelvin)
  
  public readonly float mOptimalTemp;
  public readonly byte mCompoundIndex;
  public readonly byte mCompoundType;
  
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
  public readonly byte[] mExpansion;// for future use
};


// Our world coordinate system is left-handed, with +y pointing up.
// The local vehicle coordinate system is as follows:
//   +x points out the left side of the car (from the driver's perspective)
//   +y points out the roof
//   +z points out the back of the car
// Rotations are as follows:
//   +x pitches up
//   +y yaws to the right
//   +z rolls to the right
// Note that ISO vehicle coordinates (+x forward, +y right, +z upward) are
// right-handed.  If you are using that system, be sure to negate any rotation
// or torque data because things rotate in the opposite direction.  In other
// words, a -z velocity in rFactor is a +x velocity in ISO, but a -z rotation
// in rFactor is a -x rotation in ISO!!!
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct TelemInfoV01
{
  // Time
  public readonly int mID;                      // slot ID (note that it can be re-used in multiplayer after someone leaves)
  public readonly double mDeltaTime;             // time since last update (seconds)
  public readonly double mElapsedTime;           // game session time
  public readonly int mLapNumber;               // current lap number
  public readonly double mLapStartET;            // time this lap was started
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly char[] mVehicleName;         // current vehicle name
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly char[] mTrackName;           // current track name

  // Position and derivatives
  public readonly TelemVect3 mPos;               // world position in meters
  public readonly TelemVect3 mLocalVel;          // velocity (meters/sec) in local vehicle coordinates
  public readonly TelemVect3 mLocalAccel;        // acceleration (meters/sec^2) in local vehicle coordinates

  // Orientation and derivatives
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public readonly TelemVect3[] mOri;            // rows of orientation matrix (use TelemQuat conversions if desired), also converts local
                                 // vehicle vectors into world X, Y, or Z using dot product of rows 0, 1, or 2 respectively
  public readonly TelemVect3 mLocalRot;          // rotation (radians/sec) in local vehicle coordinates
  public readonly TelemVect3 mLocalRotAccel;     // rotational acceleration (radians/sec^2) in local vehicle coordinates

  // Vehicle status
  public readonly int mGear;                    // -1=reverse, 0=neutral, 1+=forward gears
  public readonly double mEngineRPM;             // engine RPM
  public readonly double mEngineWaterTemp;       // Celsius
  public readonly double mEngineOilTemp;         // Celsius
  public readonly double mClutchRPM;             // clutch RPM

  // Driver input
  public readonly double mUnfilteredThrottle;    // ranges  0.0-1.0
  public readonly double mUnfilteredBrake;       // ranges  0.0-1.0
  public readonly double mUnfilteredSteering;    // ranges -1.0-1.0 (left to right)
  public readonly double mUnfilteredClutch;      // ranges  0.0-1.0

  // Filtered input (various adjustments for rev or speed limiting, TC, ABS?, speed sensitive steering, clutch work for semi-automatic shifting, etc.)
  public readonly double mFilteredThrottle;      // ranges  0.0-1.0
  public readonly double mFilteredBrake;         // ranges  0.0-1.0
  public readonly double mFilteredSteering;      // ranges -1.0-1.0 (left to right)
  public readonly double mFilteredClutch;        // ranges  0.0-1.0

  // Misc
  public readonly double mSteeringShaftTorque;   // torque around steering shaft (used to be mSteeringArmForce, but that is not necessarily accurate for feedback purposes)
  public readonly double mFront3rdDeflection;    // deflection at front 3rd spring
  public readonly double mRear3rdDeflection;     // deflection at rear 3rd spring

  // Aerodynamics
  public readonly double mFrontWingHeight;       // front wing height
  public readonly double mFrontRideHeight;       // front ride height
  public readonly double mRearRideHeight;        // rear ride height
  public readonly double mDrag;                  // drag
  public readonly double mFrontDownforce;        // front downforce
  public readonly double mRearDownforce;         // rear downforce

  // State/damage info
  public readonly double mFuel;                  // amount of fuel (liters)
  public readonly double mEngineMaxRPM;          // rev limit
  public readonly byte mScheduledStops; // number of scheduled pitstops
  public readonly byte  mOverheating;            // whether overheating icon is shown
  public readonly byte  mDetached;               // whether any parts (besides wheels) have been detached
  public readonly byte  mHeadlights;             // whether headlights are on
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mDentSeverity;// dent severity at 8 locations around the car (0=none, 1=some, 2=more)
  public readonly double mLastImpactET;          // time of last impact
  public readonly double mLastImpactMagnitude;   // magnitude of last impact
  public readonly TelemVect3 mLastImpactPos;     // location of last impact

  // Expanded
  public readonly double mEngineTorque;          // current engine torque (including additive torque) (used to be mEngineTq, but there's little reason to abbreviate it)
  public readonly int mCurrentSector;           // the current sector (zero-based) with the pitlane stored in the sign bit (example: entering pits from third sector gives 0x80000002)
  public readonly byte mSpeedLimiter;   // whether speed limiter is on
  public readonly byte mMaxGears;       // maximum forward gears
  public readonly byte mFrontTireCompoundIndex;   // index within brand
  public readonly byte mRearTireCompoundIndex;    // index within brand
  public readonly double mFuelCapacity;          // capacity in liters
  public readonly byte mFrontFlapActivated;       // whether front flap is activated
  public readonly byte mRearFlapActivated;        // whether rear flap is activated
  public readonly byte mRearFlapLegalStatus;      // 0=disallowed, 1=criteria detected but not allowed quite yet, 2=allowed
  public readonly byte mIgnitionStarter;          // 0=off 1=ignition 2=ignition+starter

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
  public readonly char[] mFrontTireCompoundName;         // name of front tire compound
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
  public readonly char[] mRearTireCompoundName;          // name of rear tire compound

  public readonly byte mSpeedLimiterAvailable;    // whether speed limiter is available
  public readonly byte mAntiStallActivated;       // whether (hard) anti-stall is activated
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
  public readonly byte[] mUnused;                //
  public readonly float mVisualSteeringWheelRange;         // the *visual* steering wheel range

  public readonly double mRearBrakeBias;                   // fraction of brakes on rear
  public readonly double mTurboBoostPressure;              // current turbo boost pressure if available
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public readonly float[] mPhysicsToGraphicsOffset;       // offset from static CG to graphical center
  public readonly float mPhysicalSteeringWheelRange;       // the *physical* steering wheel range

  // deltabest
  public readonly double mDeltaBest;

  public readonly double mBatterybytegeFraction; // Battery bytege as fraction [0.0-1.0]

  // electric boost motor
  public readonly double mElectricBoostMotorTorque; // current torque of boost motor (can be negative when in regenerating mode)
  public readonly double mElectricBoostMotorRPM; // current rpm of boost motor
  public readonly double mElectricBoostMotorTemperature; // current temperature of boost motor
  public readonly double mElectricBoostWaterTemperature; // current water temperature of boost motor cooler if present (0 otherwise)
  public readonly byte mElectricBoostMotorState; // 0=unavailable 1=inactive, 2=propulsion, 3=regeneration
  
  //New since 1.3 update
  public readonly byte mLapInvalidated;
  public readonly byte mABSActive;
  public readonly byte mTCActive;
  public readonly byte mSpeedLimiterActive;
  public readonly byte mWiperState; 
  public readonly byte mTC;
  public readonly byte mTCMax;
  public readonly byte mTCSlip;
  public readonly byte mTCSlipMax;
  public readonly byte mTCCut;
  public readonly byte mTCCutMax;
  public readonly byte mABS;
  public readonly byte mABSMax;
  public readonly byte mMotorMap;
  public readonly byte mMotorMapMax;
  public readonly byte mMigration;
  public readonly byte mMigrationMax;
  public readonly byte mFrontAntiSway;
  public readonly byte mFrontAntiSwayMax;
  public readonly byte mRearAntiSway;
  public readonly byte mRearAntiSwayMax;
  public readonly byte mLiftAndCoastProgress;
  public readonly byte mTrackLimitsSteps; // Normalized track limits points (TrackLimitPoints * TrackLimitStepsPerPoint)
  public readonly float mRegen; //kW
  public readonly float mSoC;
  public readonly float mVirtualEnergy;
  public readonly float mTimeGapCarAhead;
  public readonly float mTimeGapCarBehind;
  public readonly float mTimeGapPlaceAhead;
  public readonly float mTimeGapPlaceBehind;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
  public readonly byte[] mVehicleModel;
  public readonly IP_VehicleClass mVehicleClass;
  public readonly IP_VehicleChampionship mVehicleChampionship;
  
  // Future use
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //103 was before, now its 20 - so new things take 83 bytes total
  public readonly byte[] mExpansion; // for future use (note that the slot ID has been moved to mID above)

  // keeping this at the end of the public readonly structure to make it easier to replace in future versions
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
  public readonly TelemWheelV01[] mWheel; // wheel info (front left, front right, rear left, rear right)
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct GraphicsInfoV01
{
  public readonly TelemVect3 mCamPos;            // camera position
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public readonly TelemVect3[] mCamOri;         // rows of orientation matrix (use TelemQuat conversions if desired), also converts local
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mHWND;                    // app handle

  public readonly double mAmbientRed;
  public readonly double mAmbientGreen;
  public readonly double mAmbientBlue;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct GraphicsInfoV02
{
  //below things commented after 1.3 update
  // public readonly TelemVect3 mCamPos;            // camera position
  // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  // public readonly TelemVect3[] mCamOri;         // rows of orientation matrix (use TelemQuat conversions if desired), also converts local
  // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  // public readonly byte[] mHWND;                    // app handle
  //
  // public readonly double mAmbientRed;
  // public readonly double mAmbientGreen;
  // public readonly double mAmbientBlue;
  public readonly int mID;                      // slot ID being viewed (-1 if invalid)

  // Camera types (some of these may only be used for *setting* the camera type in WantsToViewVehicle())
  //    0  = TV cockpit
  //    1  = cockpit
  //    2  = nosecam
  //    3  = swingman
  //    4  = trackside (nearest)
  //    5  = onboard000
  //       :
  //       :
  // 1004  = onboard999
  // 1005+ = (currently unsupported, in the future may be able to set/get specific trackside camera)
  public readonly int mCameraType;              // see above comments for possible values
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
  public readonly byte[] mExpansion; // for future use (possibly camera name)
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct CameraControlInfoV01
{
  // Cameras
  public readonly int mID;                      // slot ID to view
  public readonly int mCameraType;              // see GraphicsInfoV02 comments for values

  // Replays (note that these are asynchronous)
  public readonly byte mReplayActive;            // This variable is an *input* filled with whether the replay is currently active (as opposed to realtime).
  public readonly byte mReplayUnused;            //
  public readonly byte mReplayCommand;  // 0=do nothing, 1=begin, 2=end, 3=rewind, 4=fast backwards, 5=backwards, 6=slow backwards, 7=stop, 8=slow play, 9=play, 10=fast play, 11=fast forward

  public readonly byte mReplaySetTime;           // Whether to skip to the following replay time:
  public readonly float mReplaySeconds;          // The replay time in seconds to skip to (note: the current replay maximum ET is passed into this variable in case you need it)

  //
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
  public readonly byte[] mExpansion; // for future use (possibly camera name & positions/orientations)
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct MessageInfoV01
{
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
  public readonly byte[] mText;               // message to display

  public readonly byte mDestination;    // 0 = message center, 1 = chat (can be used for multiplayer chat commands)
  public readonly byte mTranslate;      // 0 = do not attempt to translate, 1 = attempt to translate
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 126)]
  public readonly byte[] mExpansion; // for future use (possibly what color, what font, and seconds to display)
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct VehicleScoringInfoV01
{
  public readonly int mID;                      // slot ID (note that it can be re-used in multiplayer after someone leaves)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly char[] mDriverName;          // driver name
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly char[] mVehicleName;         // vehicle name
  public readonly short mTotalLaps;              // laps completed
  public readonly sbyte mSector;           // 0=sector3, 1=sector1, 2=sector2 (don't ask why)
  public readonly sbyte mFinishStatus;     // 0=none, 1=finished, 2=dnf, 3=dq
  public readonly double mLapDist;               // current distance around track
  public readonly double mPathLateral;           // lateral position with respect to *very approximate* "center" path
  public readonly double mTrackEdge;             // track edge (w.r.t. "center" path) on same side of track as vehicle

  public readonly double mBestSector1;           // best sector 1
  public readonly double mBestSector2;           // best sector 2 (plus sector 1)
  public readonly double mBestLapTime;           // best lap time
  public readonly double mLastSector1;           // last sector 1
  public readonly double mLastSector2;           // last sector 2 (plus sector 1)
  public readonly double mLastLapTime;           // last lap time
  public readonly double mCurSector1;            // current sector 1 if valid
  public readonly double mCurSector2;            // current sector 2 (plus sector 1) if valid
  // no current laptime because it instantly becomes "last"

  public readonly short mNumPitstops;            // number of pitstops made
  public readonly short mNumPenalties;           // number of outstanding penalties
  public readonly byte mIsPlayer;                // is this the player's vehicle

  public readonly sbyte mControl;          // who's in control: -1=nobody (shouldn't get this), 0=local player, 1=local AI, 2=remote, 3=replay (shouldn't get this)
  public readonly byte mInPits;                  // between pit entrance and pit exit (not always accurate for remote vehicles)
  public readonly byte mPlace;          // 1-based position
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly char[] mVehicleClass;        // vehicle class

  // Dash Indicators
  public readonly double mTimeBehindNext;        // time behind vehicle in next higher place
  public readonly int mLapsBehindNext;          // laps behind vehicle in next higher place
  public readonly double mTimeBehindLeader;      // time behind leader
  public readonly int mLapsBehindLeader;        // laps behind leader
  public readonly double mLapStartET;            // time this lap was started

  // Position and derivatives
  public readonly TelemVect3 mPos;               // world position in meters
  public readonly TelemVect3 mLocalVel;          // velocity (meters/sec) in local vehicle coordinates
  public readonly TelemVect3 mLocalAccel;        // acceleration (meters/sec^2) in local vehicle coordinates

  // Orientation and derivatives
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public readonly TelemVect3[] mOri;            // rows of orientation matrix (use TelemQuat conversions if desired), also converts local
                                 // vehicle vectors into world X, Y, or Z using dot product of rows 0, 1, or 2 respectively
  public readonly TelemVect3 mLocalRot;          // rotation (radians/sec) in local vehicle coordinates
  public readonly TelemVect3 mLocalRotAccel;     // rotational acceleration (radians/sec^2) in local vehicle coordinates

  // tag.2012.03.01 - stopped casting some of these so variables now have names and mExpansion has shrunk, overall size and old data locations should be same
  public readonly byte mHeadlights;     // status of headlights
  public readonly byte mPitState;       // 0=none, 1=request, 2=entering, 3=stopped, 4=exiting
  public readonly byte mServerScored;   // whether this vehicle is being scored by server (could be off in qualifying or racing heats)
  public readonly byte mIndividualPhase;// game phases (described below) plus 9=after formation, 10=under yellow, 11=under blue (not used)

  public readonly int mQualification;           // 1-based, can be -1 when invalid

  public readonly double mTimeIntoLap;           // estimated time into lap
  public readonly double mEstimatedLapTime;      // estimated laptime used for 'time behind' and 'time into lap' (note: this may changed based on vehicle and setup!?)
  
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
  public readonly char[] mPitGroup;            // pit group (same as team name unless pit is shared)
  public readonly byte mFlag;           // primary flag being shown to vehicle (currently only 0=green or 6=blue)
  public readonly byte mUnderYellow;             // whether this car has taken a full-course caution flag at the start/finish line
  public readonly byte mCountLapFlag;   // 0 = do not count lap or time, 1 = count lap but not time, 2 = count lap and time
  public readonly byte mInGarageStall;           // appears to be within the correct garage stall

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
  public readonly byte[] mUpgradePack;  // Coded upgrades
  public readonly float mPitLapDist;             // location of pit in terms of lap distance

  public readonly float mBestLapSector1;         // sector 1 time from best lap (not necessarily the best sector 1 time)
  public readonly float mBestLapSector2;         // sector 2 time from best lap (not necessarily the best sector 2 time)

  public readonly ulong mSteamID;            // SteamID of the current driver (if any)

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly char[] mVehFilename;		// filename of veh file used to identify this vehicle.

  public readonly short mAttackMode;

  // 2020.11.12 - Took 1 byte from mExpansion to transmit fuel percentage
  public readonly byte mFuelFraction; // Percentage of fuel or battery left in vehicle. 0x00 = 0%; 0xFF = 100%

  // 2021.05.28 - Took 1 byte from mExpansion to transmit DRS (RearFlap) state - consider making this a bitfield if further bytes are needed later on
  public readonly byte mDRSState;

  // Future use
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
  public readonly byte[] mExpansion;		// for future use
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct ScoringInfoV01
{
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly char[] mTrackName;           // current track name
  public readonly int mSession;                 // current session (0=testday 1-4=practice 5-8=qual 9=warmup 10-13=race)
  public readonly double mCurrentET;             // current time
  public readonly double mEndET;                 // ending time
  public readonly int  mMaxLaps;                // maximum laps
  public readonly double mLapDist;               // distance around track
  
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mResultsStreamPointer;

  public readonly int mNumVehicles;             // current number of vehicles

  // Game phases:
  // 0 Before session has begun
  // 1 Reconnaissance laps (race only)
  // 2 Grid walk-through (race only)
  // 3 Formation lap (race only)
  // 4 Starting-light countdown has begun (race only)
  // 5 Green flag
  // 6 Full course yellow / safety car
  // 7 Session stopped
  // 8 Session over
  // 9 Paused (tag.2015.09.14 - this is new, and indicates that this is a heartbeat call to the plugin)
  public readonly byte mGamePhase;

  // Yellow flag states (applies to full-course only)
  // -1 Invalid
  //  0 None
  //  1 Pending
  //  2 Pits closed
  //  3 Pit lead lap
  //  4 Pits open
  //  5 Last lap
  //  6 Resume
  //  7 Race halt (not currently used)
  public readonly sbyte mYellowFlagState;

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public readonly sbyte[] mSectorFlag;      // whether there are any local yellows at the moment in each sector (not sure if sector 0 is first or last, so test)
  public readonly byte mStartLight;       // start light frame (number depends on track)
  public readonly byte mNumRedLights;     // number of red lights in start sequence
  public readonly byte mInRealtime;                // in realtime as opposed to at the monitor
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly char[] mPlayerName;            // player name (including possible multiplayer override)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly char[] mPlrFileName;           // may be encoded to be a legal filename

  // weather
  public readonly double mDarkCloud;               // cloud darkness? 0.0-1.0
  public readonly double mRaining;                 // raining severity 0.0-1.0
  public readonly double mAmbientTemp;             // temperature (Celsius)
  public readonly double mTrackTemp;               // temperature (Celsius)
  public readonly TelemVect3 mWind;                // wind speed
  public readonly double mMinPathWetness;          // minimum wetness on main path 0.0-1.0
  public readonly double mMaxPathWetness;          // maximum wetness on main path 0.0-1.0

  // multiplayer
  public readonly byte mGameMode; // 1 = server, 2 = client, 3 = server and client
  public readonly byte mIsPasswordProtected; // is the server password protected
  public readonly ushort mServerPort; // the port of the server (if on a server)
  public readonly uint mServerPublicIP; // the public IP address of the server (if on a server)
  public readonly int mMaxPlayers; // maximum number of vehicles that can be in the session
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly char[] mServerName; // name of the server
  public readonly float mStartET; // start time (seconds since midnight) of the event

  //
  public readonly double mAvgPathWetness;          // average wetness on main path 0.0-1.0
  public readonly float mSessionTimeRemaining;
  public readonly float mTimeOfDay;
  public readonly byte mIsFixedSetup;
  public readonly byte mTrackGripLevel;
  public readonly byte mCloudCoverage;
  public readonly byte mTrackLimitsStepsPerPenalty;
  public readonly byte mTrackLimitsStepsPerPoint;

  // Future use
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 187)] //1.3 update, changed from 200 to 187 - 13 bytes
  public readonly byte[] mExpansion;

  // keeping this at the end of the public readonly structure to make it easier to replace in future versions
  
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mVehiclePointer; // array of vehicle scoring info's
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct CommentaryRequestInfoV01
{
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly byte[] mName;                  // one of the event names in the commentary INI file
  public readonly double mInput1;                  // first value to pass in (if any)
  public readonly double mInput2;                  // first value to pass in (if any)
  public readonly double mInput3;                  // first value to pass in (if any)
  public readonly byte mSkipChecks;                // ignores commentary detail and random probability of event
};


//#########################################################################
//# Version02 public readonly structures                                                   #
//##########################################################################
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct PhysicsOptionsV01
{
  public readonly byte mTractionControl;  // 0 (off) - 3 (high)
  public readonly byte mAntiLockBrakes;   // 0 (off) - 2 (high)
  public readonly byte mStabilityControl; // 0 (off) - 2 (high)
  public readonly byte mAutoShift;        // 0 (off), 1 (upshifts), 2 (downshifts), 3 (all)
  public readonly byte mAutoClutch;       // 0 (off), 1 (on)
  public readonly byte mInvulnerable;     // 0 (off), 1 (on)
  public readonly byte mOppositeLock;     // 0 (off), 1 (on)
  public readonly byte mSteeringHelp;     // 0 (off) - 3 (high)
  public readonly byte mBrakingHelp;      // 0 (off) - 2 (high)
  public readonly byte mSpinRecovery;     // 0 (off), 1 (on)
  public readonly byte mAutoPit;          // 0 (off), 1 (on)
  public readonly byte mAutoLift;         // 0 (off), 1 (on)
  public readonly byte mAutoBlip;         // 0 (off), 1 (on)

  public readonly byte mFuelMult;         // fuel multiplier (0x-7x)
  public readonly byte mTireMult;         // tire wear multiplier (0x-7x)
  public readonly byte mMechFail;         // mechanical failure setting; 0 (off), 1 (normal), 2 (timescaled)
  public readonly byte mAllowPitcrewPush; // 0 (off), 1 (on)
  public readonly byte mRepeatShifts;     // accidental repeat shift prevention (0-5; see PLR file)
  public readonly byte mHoldClutch;       // for auto-shifters at start of race: 0 (off), 1 (on)
  public readonly byte mAutoReverse;      // 0 (off), 1 (on)
  public readonly byte mAlternateNeutral; // Whether shifting up and down simultaneously equals neutral

  // tag.2014.06.09 - yes these are new, but no they don't change the size of the public readonly structure nor the address of the other variables in it (because we're just using the existing padding)
  public readonly byte mAIControl;        // Whether player vehicle is currently under AI control
  public readonly byte mUnused1;          //
  public readonly byte mUnused2;          //

  public readonly float mManualShiftOverrideTime;  // time before auto-shifting can resume after recent manual shift
  public readonly float mAutoShiftOverrideTime;    // time before manual shifting can resume after recent auto shift
  public readonly float mSpeedSensitiveSteering;   // 0.0 (off) - 1.0
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct EnvironmentInfoV01
{
  // TEMPORARY buffers (you should copy them if needed for later use) containing various paths that may be needed.  Each of these
  // could be relative ("UserData\") or full ("C:\BlahBlah\rFactorProduct\UserData\").
  // mPath[ 0 ] points to the UserData directory.
  // mPath[ 1 ] points to the CustomPluginOptions.JSON filename.
  // mPath[ 2 ] points to the latest results file
  // (in the future, we may add paths for the current garage setup, fully upgraded physics files, etc., any other requests?)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
  public readonly byte[] mPath;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
  public readonly byte[] mExpansion;   // future use
};


// deprecated (callbacks are no longer invoked in DX11) since V8
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct ScreenInfoV01
{
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[]  mAppWindow;                      // Application window handle
  
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mDevicePointer;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mRenderTargetPointer;

  public readonly int mDriver;                         // Current video driver index

  public readonly int mWidth;                          // Screen width
  public readonly int mHeight;                         // Screen height
  public readonly int mPixelFormat;                    // Pixel format
  public readonly int mRefreshRate;                    // Refresh rate
  public readonly int mWindowed;                       // Really just a byteean whether we are in windowed mode

  public readonly int mOptionsWidth;                   // Width dimension of screen portion used by UI
  public readonly int mOptionsHeight;                  // Height dimension of screen portion used by UI
  public readonly int mOptionsLeft;                    // Horizontal starting coordinate of screen portion used by UI
  public readonly int mOptionsUpper;                   // Vertical starting coordinate of screen portion used by UI

  public readonly byte mOptionsLocation;       // 0=main UI, 1=track loading, 2=monitor, 3=on track
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
  public readonly byte[] mOptionsPage;              // the name of the options page

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 224)]
  public readonly byte[] mExpansion;      // future use
};


// replaces the ScreenInfoV01 public readonly structure that was deprecated since V8
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct ApplicationStateV01 {
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mAppWindow;                      // application window handle
  public readonly uint mWidth;                 // screen width
  public readonly uint mHeight;                // screen height
  public readonly uint mRefreshRate;           // refresh rate
  public readonly uint mWindowed;              // really just a byteean whether we are in windowed mode
  public readonly byte mOptionsLocation;       // 0=main UI, 1=track loading, 2=monitor, 3=on track
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
  public readonly char[] mOptionsPage;              // the name of the options page
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 204)]
  public readonly byte[] mExpansion;      // future use
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct CustomControlInfoV01
{
  // The name passed through CheckHWControl() will be the mUntranslatedName prepended with an underscore (e.g. "Track Map Toggle" -> "_Track Map Toggle")
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly byte[] mUntranslatedName;         // name of the control that will show up in UI (but translated if available)
  public readonly int mRepeat;                         // 0=registers once per hit, 1=registers once, waits briefly, then starts repeating quickly, 2=registers as long as key is down
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly byte[] mExpansion;       // future use
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct WeatherControlInfoV01
{
  // The current conditions are passed in with the API call. The following ET (Elapsed Time) value should typically be far
  // enough in the future that it can be interpolated smoothly, and allow clouds time to roll in before rain starts. In
  // other words you probably shouldn't have mCloudiness and mRaining suddenly change from 0.0 to 1.0 and expect that
  // to happen in a few seconds without looking crazy.
  public readonly double mET;                           // when you want this weather to take effect

  // mRaining[1][1] is at the origin (2013.12.19 - and currently the only implemented node), while the others
  // are spaced at <trackNodeSize> meters where <trackNodeSize> is the maximum absolute value of a track vertex
  // coordinate (and is passed into the API call).
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3*3)]
  public readonly double[] mRaining;            // rain (0.0-1.0) at different nodes

  public readonly double mCloudiness;                   // general cloudiness (0.0=clear to 1.0=dark), will be automatically overridden to help ensure clouds exist over rainy areas
  public readonly double mAmbientTempK;                 // ambient temperature (Kelvin)
  public readonly double mWindMaxSpeed;                 // maximum speed of wind (ground speed, but it affects how fast the clouds move, too)

  public readonly byte mApplyCloudinessInstantly;       // preferably we roll the new clouds in, but you can instantly change them now
  public readonly byte mUnused1;                        //
  public readonly byte mUnused2;                        //
  public readonly byte mUnused3;                        //

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 508)]
  public readonly byte[] mExpansion;      // future use (humidity, pressure, air density, etc.)
};


//#########################################################################
//# Version07 public readonly structures                                                   #
//##########################################################################
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct CustomVariableV01
{
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
  public readonly byte[] mCaption;                 // Name of variable. This will be used for storage. In the future, this may also be used in the UI (after attempting to translate).
  public readonly int mNumSettings;                    // Number of available settings. The special value 0 should be used for types that have limitless possibilities, which will be treated as a string type.
  public readonly int mCurrentSetting;                 // Current setting (also the default setting when returned in GetCustomVariable()). This is zero-based, so: ( 0 <= mCurrentSetting < mNumSettings )

  // future expansion
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
  public readonly byte[] mExpansion;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct CustomSettingV01
{
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
  public readonly byte[] mName;                    // public enumerated name of setting (only used if CustomVariableV01::mNumSettings > 0). This will be stored in the JSON file for informational purposes only. It may also possibly be used in the UI in the future.
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct MultiSessionParticipantV01
{
  // input only
  public readonly int mID;                             // slot ID (if loaded) or -1 (if currently disconnected)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly byte[] mDriverName;               // driver name
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
  public readonly byte[] mVehicleName;              // vehicle name
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
  public readonly byte[] mUpgradePack;     // coded upgrades

  public readonly float mBestPracticeTime;              // best practice time
  public readonly int mQualParticipantIndex;           // once qualifying begins, this becomes valid and ranks participants according to practice time if possible
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
  public readonly float[] mQualificationTime;        // best qualification time in up to 4 qual sessions
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
  public readonly float[] mFinalRacePlace;           // final race place in up to 4 race sessions
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
  public readonly float[] mFinalRaceTime;            // final race time in up to 4 race sessions

  // input/output
  public readonly byte mServerScored;                   // whether vehicle is allowed to participate in current session
  public readonly int mGridPosition;                   // 1-based grid position for current race session (or upcoming race session if it is currently warmup), or -1 if currently disconnected
// long mPitIndex;
// long mGarageIndex;

  // future expansion
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
  public readonly byte[] mExpansion;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct MultiSessionRulesV01
{
  // input only
  public readonly int mSession;                        // current session (0=testday 1-4=practice 5-8=qual 9=warmup 10-13=race)
  public readonly int mSpecialSlotID;                  // slot ID of someone who just joined, or -2 requesting to update qual order, or -1 (default/general)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly byte[] mTrackType;                // track type from GDB
  public readonly int mNumParticipants;                // number of participants (vehicles)

  // input/output
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mParticipantPointer;
  
  public readonly int mNumQualSessions;                // number of qualifying sessions configured
  public readonly int mNumRaceSessions;                // number of race sessions configured
  public readonly int mMaxLaps;                        // maximum laps allowed in current session (LONG_MAX = unlimited) (note: cannot currently edit in *race* sessions)
  public readonly int mMaxSeconds;                     // maximum time allowed in current session (LONG_MAX = unlimited) (note: cannot currently edit in *race* sessions)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly byte[] mName;                     // untranslated name override for session (please use mixed case here, it should get uppercased if necessary)

  // future expansion
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
  public readonly byte[] mExpansion;
};


public enum TrackRulesCommandV01               //
{
  TRCMD_ADD_FROM_TRACK = 0,             // crossed s/f line for first time after full-course yellow was called
  TRCMD_ADD_FROM_PIT,                   // exited pit during full-course yellow
  TRCMD_ADD_FROM_UNDQ,                  // during a full-course yellow, the admin reversed a disqualification
  TRCMD_REMOVE_TO_PIT,                  // entered pit during full-course yellow
  TRCMD_REMOVE_TO_DNF,                  // vehicle DNF'd during full-course yellow
  TRCMD_REMOVE_TO_DQ,                   // vehicle DQ'd during full-course yellow
  TRCMD_REMOVE_TO_UNLOADED,             // vehicle unloaded (possibly kicked out or banned) during full-course yellow
  TRCMD_MOVE_TO_BACK,                   // misbehavior during full-course yellow, resulting in the penalty of being moved to the back of their current line
  TRCMD_LONGEST_LINE,                   // misbehavior during full-course yellow, resulting in the penalty of being moved to the back of the longest line
  //------------------
  TRCMD_MAXIMUM                         // should be last
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct TrackRulesActionV01
{
  // input only
  public readonly TrackRulesCommandV01 mCommand;        // recommended action
  public readonly int mID;                             // slot ID if applicable
  public readonly byte mLine;                  // line this command applies to (if applicable)
};

public enum TrackRulesColumnV01
{
  TRCOL_LEFT_LANE = 0,                  // left (inside)
  TRCOL_MIDDLE_LANE,                    // middle
  TRCOL_RIGHT_LANE,                     // right (outside)
  //------------------
  TRCOL_MAX_LANES,                      // should be after the valid static lane choices
  //------------------
  TRCOL_INVALID = TRCOL_MAX_LANES,      // currently invalid (hasn't crossed line or in pits/garage)
  TRCOL_FREECHOICE,                     // free choice (dynamically chosen by driver)
  TRCOL_PENDING,                        // depends on another participant's free choice (dynamically set after another driver chooses)
  //------------------
  TRCOL_MAXIMUM                         // should be last
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct TrackRulesParticipantV01
{
  // input only
 public readonly int mID;                             // slot ID
 public readonly short mFrozenOrder;                   // 0-based place when caution came out (not valid for formation laps)
 public readonly short mPlace;                         // 1-based place (typically used for the initialization of the formation lap track order)
 public readonly float mYellowSeverity;                // a rating of how much this vehicle is contributing to a yellow flag (the sum of all vehicles is compared to TrackRulesV01::mSafetyCarThreshold)
 public readonly double mCurrentRelativeDistance;      // equal to ( ( ScoringInfoV01::mLapDist * this->mRelativeLaps ) + VehicleScoringInfoV01::mLapDist )

  // input/output
  public readonly int mRelativeLaps;                   // current formation/caution laps relative to safety car (should generally be zero except when safety car crosses s/f line); this can be decremented to implement 'wave around' or 'beneficiary rule' (a.k.a. 'lucky dog' or 'free pass')
  public readonly TrackRulesColumnV01 mColumnAssignment;// which column (line/lane) that participant is supposed to be in
  public readonly int mPositionAssignment;             // 0-based position within column (line/lane) that participant is supposed to be located at (-1 is invalid)
  public readonly byte mPitsOpen;              // whether the rules allow this particular vehicle to enter pits right now (input is 2=false or 3=true; if you want to edit it, set to 0=false or 1=true)
  public readonly byte mUpToSpeed;                      // while in the frozen order, this flag indicates whether the vehicle can be followed (this should be false for somebody who has temporarily spun and hasn't gotten back up to speed yet)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
  public readonly byte[] mUnused;                    //
  public readonly double mGoalRelativeDistance;         // calculated based on where the leader is, and adjusted by the desired column spacing and the column/position assignments
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
  public readonly byte[] mMessage;                  // a message for this participant to explain what is going on (untranslated; it will get run through translator on client machines)

  // future expansion
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 192)]
  public readonly byte[] mExpansion;
};

public enum TrackRulesStageV01                 //
{
  TRSTAGE_FORMATION_INIT = 0,           // initialization of the formation lap
  TRSTAGE_FORMATION_UPDATE,             // update of the formation lap
  TRSTAGE_NORMAL,                       // normal (non-yellow) update
  TRSTAGE_CAUTION_INIT,                 // initialization of a full-course yellow
  TRSTAGE_CAUTION_UPDATE,               // update of a full-course yellow
  //------------------
  TRSTAGE_MAXIMUM                       // should be last
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct TrackRulesV01
{
  // input only
  public readonly double mCurrentET;                    // current time
  public readonly TrackRulesStageV01 mStage;            // current stage
  public readonly TrackRulesColumnV01 mPoleColumn;      // column assignment where pole position seems to be located
  public readonly int mNumActions;                     // number of recent actions
  
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mActionPointer;
  
  public readonly int mNumParticipants;                // number of participants (vehicles)

  public readonly byte mYellowFlagDetected;             // whether yellow flag was requested or sum of participant mYellowSeverity's exceeds mSafetyCarThreshold
  public readonly byte mYellowFlagLapsWasOverridden;     // whether mYellowFlagLaps (below) is an admin request (0=no 1=yes 2=clear yellow)

  public readonly byte mSafetyCarExists;                // whether safety car even exists
  public readonly byte mSafetyCarActive;                // whether safety car is active
  public readonly int mSafetyCarLaps;                  // number of laps
  public readonly float mSafetyCarThreshold;            // the threshold at which a safety car is called out (compared to the sum of TrackRulesParticipantV01::mYellowSeverity for each vehicle)
  public readonly double mSafetyCarLapDist;             // safety car lap distance
  public readonly float mSafetyCarLapDistAtStart;       // where the safety car starts from

  public readonly float mPitLaneStartDist;              // where the waypoint branch to the pits breaks off (this may not be perfectly accurate)
  public readonly float mTeleportLapDist;               // the front of the teleport locations (a useful first guess as to where to throw the green flag)

  // future input expansion
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
  public readonly byte[] mInputExpansion;

  // input/output
  public readonly sbyte mYellowFlagState;         // see ScoringInfoV01 for values
  public readonly short mYellowFlagLaps;                // suggested number of laps to run under yellow (may be passed in with admin command)

  public readonly int   mSafetyCarInstruction;           // 0=no change, 1=go active, 2=head for pits
  public readonly float mSafetyCarSpeed;                // maximum speed at which to drive
  public readonly float mSafetyCarMinimumSpacing;       // minimum spacing behind safety car (-1 to indicate no limit)
  public readonly float mSafetyCarMaximumSpacing;       // maximum spacing behind safety car (-1 to indicate no limit)

  public readonly float mMinimumColumnSpacing;          // minimum desired spacing between vehicles in a column (-1 to indicate indeterminate/unenforced)
  public readonly float mMaximumColumnSpacing;          // maximum desired spacing between vehicles in a column (-1 to indicate indeterminate/unenforced)

  public readonly float mMinimumSpeed;                  // minimum speed that anybody should be driving (-1 to indicate no limit)
  public readonly float mMaximumSpeed;                  // maximum speed that anybody should be driving (-1 to indicate no limit)

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
  public readonly byte[] mMessage;                  // a message for everybody to explain what is going on (which will get run through translator on client machines)
  
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public readonly byte[] mParticipantPointer;

  // future input/output expansion
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
  public readonly byte[] mInputOutputExpansion;
};


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public readonly struct PitMenuV01
{
  public readonly int mCategoryIndex;                  // index of the current category
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly byte[] mCategoryName;             // name of the current category (untranslated)

  public readonly int mChoiceIndex;                    // index of the current choice (within the current category)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
  public readonly byte[] mChoiceString;             // name of the current choice (may have some translated words)
  public readonly int mNumChoices;                     // total number of choices (0 <= mChoiceIndex < mNumChoices)

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
  public readonly byte[] mExpansion;      // for future use
};