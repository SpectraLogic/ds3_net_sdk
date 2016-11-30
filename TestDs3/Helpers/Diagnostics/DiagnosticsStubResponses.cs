using System.Collections.Generic;
using Ds3.Calls;
using Ds3.Models;

namespace TestDs3.Helpers.Diagnostics
{
    internal static class DiagnosticsStubResponses
    {
        public static readonly GetCacheStateSpectraS3Response NullFilesystems =
            new GetCacheStateSpectraS3Response(new CacheInformation
            {
                Filesystems = null
            });

        public static readonly GetCacheStateSpectraS3Response EmptyFilesystems =
            new GetCacheStateSpectraS3Response(new CacheInformation
            {
                Filesystems = new List<CacheFilesystemInformation>()
            });

        public static readonly GetCacheStateSpectraS3Response NoNearCapacity =
            new GetCacheStateSpectraS3Response(new CacheInformation
            {
                Filesystems = new List<CacheFilesystemInformation>
                {
                    new CacheFilesystemInformation
                    {
                        UsedCapacityInBytes = 0,
                        AvailableCapacityInBytes = 100
                    },
                    new CacheFilesystemInformation
                    {
                        UsedCapacityInBytes = 50,
                        AvailableCapacityInBytes = 100
                    }
                }
            });

        public static readonly GetCacheStateSpectraS3Response OneNearCapacity =
            new GetCacheStateSpectraS3Response(new CacheInformation
            {
                Filesystems = new List<CacheFilesystemInformation>
                {
                    new CacheFilesystemInformation
                    {
                        UsedCapacityInBytes = 0,
                        AvailableCapacityInBytes = 100
                    },
                    new CacheFilesystemInformation
                    {
                        UsedCapacityInBytes = 95,
                        AvailableCapacityInBytes = 100
                    }
                }
            });

        public static readonly GetCacheStateSpectraS3Response TwoNearCapacity =
            new GetCacheStateSpectraS3Response(new CacheInformation
            {
                Filesystems = new List<CacheFilesystemInformation>
                {
                    new CacheFilesystemInformation
                    {
                        UsedCapacityInBytes = 95,
                        AvailableCapacityInBytes = 100
                    },
                    new CacheFilesystemInformation
                    {
                        UsedCapacityInBytes = 96,
                        AvailableCapacityInBytes = 100
                    }
                }
            });

        public static readonly GetTapesSpectraS3Response NoTapes =
            new GetTapesSpectraS3Response(new TapeList
            {
                Tapes = new List<Tape>()
            }, null, null);

        public static readonly GetTapesSpectraS3Response OneTape =
            new GetTapesSpectraS3Response(new TapeList
            {
                Tapes = new List<Tape>
                {
                    new Tape()
                }
            }, null, null);

        public static readonly GetTapesSpectraS3Response TwoTapes =
            new GetTapesSpectraS3Response(new TapeList
            {
                Tapes = new List<Tape>
                {
                    new Tape(),
                    new Tape()
                }
            }, null, null);

        public static readonly GetPoolsSpectraS3Response NoPools =
            new GetPoolsSpectraS3Response(new PoolList
            {
                Pools = new List<Pool>()
            }, null, null);

        public static readonly GetPoolsSpectraS3Response NoPoweredOffPools =
            new GetPoolsSpectraS3Response(new PoolList
            {
                Pools = new List<Pool>
                {
                    new Pool
                    {
                        PoweredOn = true
                    }
                }
            }, null, null);

        public static readonly GetPoolsSpectraS3Response OnePoweredOffPool =
            new GetPoolsSpectraS3Response(new PoolList
            {
                Pools = new List<Pool>
                {
                    new Pool
                    {
                        PoweredOn = false
                    },
                    new Pool
                    {
                        PoweredOn = true
                    }
                }
            }, null, null);

        public static readonly GetPoolsSpectraS3Response TwoPoweredOffPool =
            new GetPoolsSpectraS3Response(new PoolList
            {
                Pools = new List<Pool>
                {
                    new Pool
                    {
                        PoweredOn = false
                    },
                    new Pool
                    {
                        PoweredOn = false
                    }
                }
            }, null, null);

        public static readonly GetDataPlannerBlobStoreTasksSpectraS3Response NoReadingTasks =
            new GetDataPlannerBlobStoreTasksSpectraS3Response(
                new BlobStoreTasksInformation
                {
                    Tasks = new List<BlobStoreTaskInformation>()
                }
            );

        public static readonly GetDataPlannerBlobStoreTasksSpectraS3Response OneReadingTasks =
            new GetDataPlannerBlobStoreTasksSpectraS3Response(
                new BlobStoreTasksInformation
                {
                    Tasks = new List<BlobStoreTaskInformation>
                    {
                        new BlobStoreTaskInformation
                        {
                            Name = "VerifyTapeTask",
                            State = BlobStoreTaskState.PENDING_EXECUTION
                        },
                        new BlobStoreTaskInformation
                        {
                            Name = "ReadChunkFromTapeTask",
                            State = BlobStoreTaskState.IN_PROGRESS
                        },
                        new BlobStoreTaskInformation
                        {
                            Name = "ReadChunkFromTapeTask",
                            State = BlobStoreTaskState.READY
                        }
                    }
                }
            );

        public static readonly GetDataPlannerBlobStoreTasksSpectraS3Response NoWritingTasks =
            new GetDataPlannerBlobStoreTasksSpectraS3Response(
                new BlobStoreTasksInformation
                {
                    Tasks = new List<BlobStoreTaskInformation>()
                }
            );

        public static readonly GetDataPlannerBlobStoreTasksSpectraS3Response OneWritingTasks =
            new GetDataPlannerBlobStoreTasksSpectraS3Response(
                new BlobStoreTasksInformation
                {
                    Tasks = new List<BlobStoreTaskInformation>
                    {
                        new BlobStoreTaskInformation
                        {
                            Name = "VerifyTapeTask",
                            State = BlobStoreTaskState.PENDING_EXECUTION
                        },
                        new BlobStoreTaskInformation
                        {
                            Name = "WriteChunkFromTapeTask",
                            State = BlobStoreTaskState.IN_PROGRESS
                        },
                        new BlobStoreTaskInformation
                        {
                            Name = "WriteChunkFromTapeTask",
                            State = BlobStoreTaskState.READY
                        }
                    }
                }
            );

        public static readonly GetDs3TargetsSpectraS3Response NoTargets =
            new GetDs3TargetsSpectraS3Response(new Ds3TargetList
            {
                Ds3Targets = new List<Ds3Target>()
            }, null, null);

        public static readonly GetDs3TargetsSpectraS3Response ClientTargets =
            new GetDs3TargetsSpectraS3Response(new Ds3TargetList
            {
                Ds3Targets = new List<Ds3Target>
                {
                    new Ds3Target
                    {
                        DataPathEndPoint = "T1",
                        AdminAuthId = "Id1",
                        AdminSecretKey = "Key1"
                    },
                    new Ds3Target
                    {
                        DataPathEndPoint = "T2",
                        AdminAuthId = "Id2",
                        AdminSecretKey = "Key2"
                    }
                }
            }, null, null);

        public static readonly GetDs3TargetsSpectraS3Response ClientT1Targets =
            new GetDs3TargetsSpectraS3Response(new Ds3TargetList
            {
                Ds3Targets = new List<Ds3Target>
                {
                    new Ds3Target
                    {
                        DataPathEndPoint = "T3",
                        AdminAuthId = "Id3",
                        AdminSecretKey = "Key3"
                    },
                    new Ds3Target
                    {
                        DataPathEndPoint = "T4",
                        AdminAuthId = "Id4",
                        AdminSecretKey = "Key4"
                    }
                }
            }, null, null);

        public static readonly GetDs3TargetsSpectraS3Response ClientT2Targets =
            new GetDs3TargetsSpectraS3Response(new Ds3TargetList
            {
                Ds3Targets = new List<Ds3Target>
                {
                    new Ds3Target
                    {
                        DataPathEndPoint = "T5",
                        AdminAuthId = "Id5",
                        AdminSecretKey = "Key5"
                    }
                }
            }, null, null);
    }
}