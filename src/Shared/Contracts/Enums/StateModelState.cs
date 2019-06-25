﻿namespace SSDTLifecycleExtension.Shared.Contracts.Enums
{
    public enum StateModelState
    {
        Undefined = 0,
        Initialized = 1,
        SqlProjectPropertiesLoaded = 10,
        FormattedTargetVersionLoaded = 20,
        FormattedTargetVersionValidated = 25,
        PathsLoaded = 30,
        PathsVerified = 40,
        TriedToBuildProject = 50,
        TriedToCopyBuildResult = 60,
        TriedToCreateDeploymentFiles = 70,
        ModifiedDeploymentScript = 80
    }
}