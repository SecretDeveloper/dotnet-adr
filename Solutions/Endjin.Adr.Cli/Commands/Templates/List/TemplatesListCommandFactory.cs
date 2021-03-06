﻿// <copyright file="TemplatesListCommandFactory.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Endjin.Adr.Cli.Commands.Templates.List
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Globalization;
    using Endjin.Adr.Cli.Configuration;
    using Endjin.Adr.Cli.Configuration.Contracts;

    public class TemplatesListCommandFactory : ICommandFactory<TemplatesListCommandFactory>
    {
        private readonly ITemplateSettingsManager templateSettingsManager;

        public TemplatesListCommandFactory(ITemplateSettingsManager templateSettingsManager)
        {
            this.templateSettingsManager = templateSettingsManager;
        }

        public Command Create()
        {
            var cmd = new Command("list", "Lists all installed ADR Templates.")
            {
                Handler = CommandHandler.Create((bool idsOnly) =>
                {
                    var templateSettings = this.templateSettingsManager.LoadSettings(nameof(TemplateSettings));

                    if (idsOnly)
                    {
                        foreach (var template in templateSettings.MetaData.Details)
                        {
                            Console.WriteLine(template.Id);
                        }
                    }
                    else
                    {
                        Console.WriteLine("-------");

                        foreach (var template in templateSettings.MetaData.Details)
                        {
                            Console.WriteLine(string.Empty);
                            Console.WriteLine($"Title: {template.Title}");
                            Console.WriteLine($"Id: {template.Id}");
                            Console.WriteLine($"Description: {template.Description}");
                            Console.WriteLine($"Authors: {template.Authors}");
                            Console.WriteLine($"Effort: {template.Effort}");
                            Console.WriteLine($"More Info: {template.MoreInfo}");
                            Console.WriteLine($"Last Modified: {template.LastModified.ToString(CultureInfo.InvariantCulture)}");
                            Console.WriteLine($"Version: {template.Version}");
                            Console.WriteLine(string.Empty);
                            Console.WriteLine("-------");
                        }
                    }
                }),
            };

            cmd.AddOption(new Option("--ids-only"));

            return cmd;
        }
    }
}