﻿namespace SSDTLifecycleExtension.Shared.WorkUnits
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.DataAccess;
    using Contracts.Enums;
    using Contracts.Models;
    using JetBrains.Annotations;
    using Models;

    [UsedImplicitly]
    public class CleanLatestArtifactsDirectoryUnit : IWorkUnit<ScriptCreationStateModel>
    {
        [NotNull] private readonly IFileSystemAccess _fileSystemAccess;
        [NotNull] private readonly ILogger _logger;

        public CleanLatestArtifactsDirectoryUnit([NotNull] IFileSystemAccess fileSystemAccess,
                                                 [NotNull] ILogger logger)
        {
            _fileSystemAccess = fileSystemAccess ?? throw new ArgumentNullException(nameof(fileSystemAccess));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private async Task CleanArtifactsDirectoryInternal(IStateModel stateModel,
                                                           ConfigurationModel configuration,
                                                           PathCollection paths)
        {
            if (!configuration.DeleteLatestAfterVersionedScriptGeneration)
            {
                stateModel.CurrentState = StateModelState.DeletedLatestArtifacts;
                return;
            }

            await _logger.LogAsync("Cleaning latest artifacts directory ...");
            // Even if this operation fails, there's no reason to make the whole process fail.
            // Therefore this will not set the stateModel.Result property.
            _fileSystemAccess.TryToCleanDirectory(paths.LatestArtifactsDirectory);

            stateModel.CurrentState = StateModelState.DeletedLatestArtifacts;
        }

        Task IWorkUnit<ScriptCreationStateModel>.Work(ScriptCreationStateModel stateModel,
                                                      CancellationToken cancellationToken)
        {
            if (stateModel == null)
                throw new ArgumentNullException(nameof(stateModel));

            return CleanArtifactsDirectoryInternal(stateModel,
                                                   stateModel.Configuration,
                                                   stateModel.Paths);
        }
    }
}