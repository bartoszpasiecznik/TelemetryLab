// using System.Runtime.InteropServices;
//
// namespace TelemetryLab;
//
// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
// public readonly struct SharedMemoryScoringData { // Remember to check CopySharedMemoryObj still works properly when updating this struct
//     public readonly ScoringInfoV01 scoringInfo;
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
//     public readonly byte[] scoringStreamSize;
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_MAPPED_VEHICLES)]
//     public readonly VehicleScoringInfoV01[] vehScoringInfo; // MUST NOT BE MOVED!
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65536)]
//     public readonly char[] scoringStream;
// };
//
// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
// public readonly struct SharedMemoryTelemtryData { // Remember to check CopySharedMemoryObj still works properly when updating this struct
//     public readonly uint activeVehicles;
//     public readonly uint playerVehicleIdx;
//     public readonly bool playerHasVehicle;
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_MAPPED_VEHICLES)]
//     public readonly  TelemInfoV01[] telemInfo;
// };
//
// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
// public struct SharedMemoryPathData {
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
//     public readonly char[] userData;
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
//     public readonly char[] customVariables;
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
//     public readonly  char[] stewardResults;
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
//     public readonly  char[] playerProfile;
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMUConstants.MAX_PATH)]
//     public readonly char[] pluginsFolder;
// };
//
// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
// public readonly struct SharedMemoryEvent
// {
//     public readonly uint SME_ENTER;
//     public readonly uint SME_EXIT;
//     public readonly uint SME_STARTUP;
//     public readonly uint SME_SHUTDOWN;
//     public readonly uint SME_LOAD;
//     public readonly uint SME_UNLOAD;
//     public readonly uint SME_START_SESSION;
//     public readonly uint SME_END_SESSION;
//     public readonly uint SME_ENTER_REALTIME;
//     public readonly uint SME_EXIT_REALTIME;
//     public readonly uint SME_UPDATE_SCORING;
//     public readonly uint SME_UPDATE_TELEMETRY;
//     public readonly uint SME_INIT_APPLICATION;
//     public readonly uint SME_UNINIT_APPLICATION;
//     public readonly uint SME_SET_ENVIRONMENT;
//     public readonly uint SME_FFB;
//     // public readonly uint SME_MAX;
// };
//
// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
// public struct SharedMemoryGeneric
// {
//     // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
//     public readonly  SharedMemoryEvent events;
//     public readonly int gameVersion;
//     public readonly float FFBTorque;
//     public readonly ApplicationStateV01 appInfo;
// };
//
// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
// public readonly struct SharedMemoryObjectOut { // Remember to check CopySharedMemoryObj still works properly when updating this struct
//     public readonly SharedMemoryGeneric generic;
//     public readonly SharedMemoryPathData paths;
//     public readonly SharedMemoryScoringData scoring;
//     public readonly SharedMemoryTelemtryData telemetry;
// };
//
// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
// public readonly struct SharedMemoryLayout {
//     public readonly SharedMemoryObjectOut data;
// };