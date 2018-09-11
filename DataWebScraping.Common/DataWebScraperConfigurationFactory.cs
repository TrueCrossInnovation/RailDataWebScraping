﻿using DataWebScraping.Common.DataWebScraperSteps;
using DataWebScraping.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DataWebScraping.Common
{
    public class DataWebScraperConfigurationFactory : IDataWebScraperStepFactory
    {
        private Dictionary<string, Type> _dataWebScraperSteps;

        public IReadOnlyDictionary<string, Type> DataWebScraperSteps => _dataWebScraperSteps;

        public DataWebScraperConfigurationFactory()
        {
            _dataWebScraperSteps = new Dictionary<string, Type>();            
            _dataWebScraperSteps.Add(DataWebScraperStepType.LogInStep, typeof(SetTextBoxDataWebScraperStep));
        }        

        public IDataWebScraperStep GetDataWebScraperStep(IDataWebScraperRawStep dataWebScraperRawStep)
        {
            string typeStep = dataWebScraperRawStep.TypeStep;

            Type type = null;
            object args = new object();
            if (_dataWebScraperSteps.TryGetValue(typeStep, out type))
            {                
                return (IDataWebScraperStep)Activator.CreateInstance(type, args);
            }

            throw new NotImplementedException($"The factory didn't find a type {typeStep} registered to be created.");
        }
    }
}