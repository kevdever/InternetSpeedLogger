/*
 * Copyright 2018 KevDever 
 * See LICENSE.md
 */

using InternetSpeedLogger.Database;
using System;
using System.Threading;

namespace InternetSpeedLogger
{
    public class TestRunnerOptions
    {
        public TestRunnerOptions(bool silent, IResultRepository resultRepository, TimeSpan frequency, int maxRuns, bool hideResults, CancellationToken cancellationToken)
        {
            Silent = silent;
            ResultRepository = resultRepository;
            Frequency = frequency;
            MaxRuns = maxRuns;
            HideResults = hideResults;
            CancellationToken = cancellationToken;
        }

        public TestRunnerOptions()
        {

        }

        public bool Silent { get; set; }
        public Database.IResultRepository ResultRepository { get; set; }
        public TimeSpan Frequency { get; set; }
        public int MaxRuns { get; set; }
        public bool HideResults { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
