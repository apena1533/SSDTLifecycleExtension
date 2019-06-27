﻿namespace SSDTLifecycleExtension.Shared.Services
{
    using System;
    using System.Collections.Generic;
    using Contracts.Services;
    using Contracts;
    using Contracts.Enums;
    using Contracts.Factories;
    using JetBrains.Annotations;
    using Models;

    [UsedImplicitly]
    public class ScriptModifierProviderService : IScriptModifierProviderService
    {
        [NotNull] private readonly IScriptModifierFactory _scriptModifierFactory;

        public ScriptModifierProviderService([NotNull] IScriptModifierFactory scriptModifierFactory)
        {
            _scriptModifierFactory = scriptModifierFactory ?? throw new ArgumentNullException(nameof(scriptModifierFactory));
        }

        IReadOnlyDictionary<ScriptModifier, IScriptModifier> IScriptModifierProviderService.GetScriptModifiers(ConfigurationModel configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var result = new Dictionary<ScriptModifier, IScriptModifier>();

            if (configuration.CommentOutUnnamedDefaultConstraintDrops)
                result[ScriptModifier.CommentOutUnnamedDefaultConstraintDrops] = _scriptModifierFactory.CreateScriptModifier(ScriptModifier.CommentOutUnnamedDefaultConstraintDrops);

            if (configuration.ReplaceUnnamedDefaultConstraintDrops)
                result[ScriptModifier.ReplaceUnnamedDefaultConstraintDrops] = _scriptModifierFactory.CreateScriptModifier(ScriptModifier.ReplaceUnnamedDefaultConstraintDrops);

            if (!string.IsNullOrWhiteSpace(configuration.CustomHeader))
                result[ScriptModifier.AddCustomHeader] = _scriptModifierFactory.CreateScriptModifier(ScriptModifier.AddCustomHeader);

            if (!string.IsNullOrWhiteSpace(configuration.CustomFooter))
                result[ScriptModifier.AddCustomFooter] = _scriptModifierFactory.CreateScriptModifier(ScriptModifier.AddCustomFooter);

            if (configuration.TrackDacpacVersion)
                result[ScriptModifier.TrackDacpacVersion] = _scriptModifierFactory.CreateScriptModifier(ScriptModifier.TrackDacpacVersion);

            return result;
        }
    }
}