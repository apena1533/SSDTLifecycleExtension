﻿namespace SSDTLifecycleExtension.Shared.Contracts.DataAccess
{
    using System;
    using System.Threading.Tasks;
    using Shared;

    public interface IVisualStudioAccess
    {
        /// <summary>
        /// Gets the kind of the selected project.
        /// </summary>
        /// <returns>The kind of the currently selected project, or <b><see cref="Guid.Empty"/></b>, if none is selected.</returns>
        Guid GetSelectedProjectKind();

        /// <summary>
        /// Gets the selected SQL project.
        /// </summary>
        /// <returns>The currently selected SQL project, or <b>null</b>, if the selected item is no SQL project.</returns>
        SqlProject GetSelectedSqlProject();

        Task ClearSSDTLifecycleOutputAsync();

        void ShowModalError([NotNull] string error);

        void BuildProject([NotNull] SqlProject project);

        Task StartLongRunningTaskIndicatorAsync();

        Task StopLongRunningTaskIndicatorAsync();

        /// <summary>
        /// Adds the item at the <paramref name="targetPath"/> to the <paramref name="project"/> properties, if it doesn't exist there already.
        /// </summary>
        /// <param name="project">The project to add the item to.</param>
        /// <param name="targetPath">The full path of the item to add to the properties.</param>
        void AddItemToProjectProperties([NotNull] SqlProject project,
                                        [NotNull] string targetPath);
    }
}