﻿using Eddi;
using EddiDataDefinitions;
using EddiDataProviderService;
using EddiStarMapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EddiEdsmResponder
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : UserControl
    {
        public ConfigurationWindow()
        {
            InitializeComponent();

            StarMapConfiguration starMapConfiguration = StarMapConfiguration.FromFile();
            edsmApiKeyTextBox.Text = starMapConfiguration.apiKey;
            edsmCommanderNameTextBox.Text = starMapConfiguration.commanderName;
            edsmFetchLogsButton.Content = String.IsNullOrEmpty(edsmApiKeyTextBox.Text) ? Properties.EDSMResources.log_button_empty_api_key : Properties.EDSMResources.log_button;
        }

        private void edsmCommanderNameChanged(object sender, TextChangedEventArgs e)
        {
            edsmFetchLogsButton.IsEnabled = true;
            edsmFetchLogsButton.Content = Properties.EDSMResources.log_button;
            updateEdsmConfiguration();
        }

        private void edsmApiKeyChanged(object sender, TextChangedEventArgs e)
        {
            edsmFetchLogsButton.IsEnabled = true;
            edsmFetchLogsButton.Content = String.IsNullOrEmpty(edsmApiKeyTextBox.Text) ? Properties.EDSMResources.log_button_empty_api_key : Properties.EDSMResources.log_button;
            updateEdsmConfiguration();
        }

        private void updateEdsmConfiguration()
        {
            StarMapConfiguration edsmConfiguration = StarMapConfiguration.FromFile();
            if (!string.IsNullOrWhiteSpace(edsmApiKeyTextBox.Text))
            {
                edsmConfiguration.apiKey = edsmApiKeyTextBox.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(edsmCommanderNameTextBox.Text))
            {
                edsmConfiguration.commanderName = edsmCommanderNameTextBox.Text.Trim();
            }
            edsmConfiguration.ToFile();
            EDDI.Instance.Reload("EDSM responder");
        }

        /// <summary>
        /// Obtain the EDSM log and sync it with the local datastore
        /// </summary>
        private async void edsmObtainLogClicked(object sender, RoutedEventArgs e)
        {
            StarMapConfiguration starMapConfiguration = StarMapConfiguration.FromFile();

            if (string.IsNullOrEmpty(starMapConfiguration.apiKey))
            {
                edsmFetchLogsButton.IsEnabled = false;
                edsmFetchLogsButton.Content = Properties.EDSMResources.log_button_empty_api_key;
                return;
            }

            string commanderName;
            if (string.IsNullOrEmpty(starMapConfiguration.commanderName))
            {
                // Fetch the commander name from the companion app
                Commander cmdr = EDDI.Instance.Cmdr;
                if (cmdr != null && cmdr.name != null)
                {
                    commanderName = cmdr.name;
                }
                else
                {
                    edsmFetchLogsButton.IsEnabled = false;
                    edsmFetchLogsButton.Content = Properties.EDSMResources.log_button_companion_unconfigured;
                    return;
                }
            }
            else
            {
                commanderName = starMapConfiguration.commanderName;
            }

            edsmFetchLogsButton.IsEnabled = false;
            edsmFetchLogsButton.Content = Properties.EDSMResources.log_button_fetching;

            var progress = new Progress<string>(s => edsmFetchLogsButton.Content = s);
            await Task.Factory.StartNew(() => obtainEdsmLogs(starMapConfiguration, commanderName, progress),
                                            TaskCreationOptions.LongRunning);

            starMapConfiguration.lastSync = DateTime.UtcNow;
            starMapConfiguration.ToFile();
        }

        public static void obtainEdsmLogs(StarMapConfiguration starMapConfiguration, string commanderName, IProgress<string> progress)
        {
            StarMapService starMapService = new StarMapService(starMapConfiguration.apiKey, commanderName);
            try
            {
                Dictionary<string, StarMapLogInfo> systems = starMapService.getStarMapLog();
                Dictionary<string, string> comments = starMapService.getStarMapComments();
                int total = systems.Count;
                int i = 0;

                string[] systemNames = systems.Keys.ToArray();
                while (i < total)
                {
                    int batchSize = Math.Min(total, StarMapService.syncBatchSize);
                    string[] batchNames = systemNames.Skip(i).Take(batchSize).ToArray();
                    List<StarSystem> batchSystems = new List<StarSystem>();

                    List<StarSystem> starSystems = StarSystemSqLiteRepository.Instance.GetOrCreateStarSystems(batchNames, false);
                    foreach (string system in batchNames)
                    {
                        StarSystem CurrentStarSystem = starSystems.FirstOrDefault(s => s.name == system);
                        CurrentStarSystem.visits = systems[system].visits;
                        CurrentStarSystem.lastvisit = systems[system].lastVisit;
                        if (comments.ContainsKey(system))
                        {
                            CurrentStarSystem.comment = comments[system];
                        }
                        batchSystems.Add(CurrentStarSystem);
                    }
                    DataProviderService.saveFromStarMapService(batchSystems);
                    i = i + batchSize;
                    progress.Report($"{Properties.EDSMResources.log_button_fetching_progress} {i}/{total}");
                }
                progress.Report(Properties.EDSMResources.log_button_fetched);
            }
            catch (EDSMException edsme)
            {
                progress.Report(Properties.EDSMResources.log_button_error_received + edsme.Message);
            }
        }
    }
}
