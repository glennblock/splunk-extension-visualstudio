﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Splunk.ModularInputs;

namespace $safeprojectname$
{
    public class Program : ModularInput
    {
        public static int Main(string[] args)
        {
            return Run<Program>(args);
        }

        public override Scheme Scheme
        {
            get {
                return new Scheme
                {$if$ ($generateExampleImplementation$ == true)
                    Title = "Random numbers",
                    Description = "Generate random numbers in the specified range",
                    Arguments = new List<Argument>
                    {
                            new Argument
                            {
                                Name = "min",
                                Description = "Generated value should be at least min",
                                DataType = DataType.Number,
                                RequiredOnCreate = true
                            },
                            new Argument
                            {
                                Name = "max",
                                Description = "Generated value should be less than max",
                                DataType = DataType.Number,
                                RequiredOnCreate = true
                            }
                    }$endif$
                };

            }
        }
            
        public override bool Validate(Validation validation, out string errorMessage)
        {$if$ ($generateExampleImplementation$ == true)
            double min = validation.Parameters["min"].ToDouble();
            double max = validation.Parameters["max"].ToDouble();

            if (min >= max) {
                errorMessage = "min must be less than max.";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }$endif$$if$ ($generateExampleImplementation$ == false)
			errorMessage = "";
			return true;$endif$
        }

        public override async Task StreamEventsAsync(InputDefinition inputDefinition, EventWriter eventWriter)
        {$if$ ($generateExampleImplementation$ == true)
            double min = inputDefinition.Parameters["min"].ToDouble();
            double max = inputDefinition.Parameters["max"].ToDouble();

            Random rnd = new Random();

            while (true)
            {
                await Task.Delay(1000);
                await eventWriter.QueueEventForWriting(new Event
                {
                    Stanza = inputDefinition.Name,
                    Data = "number=" + (rnd.NextDouble() * (max - min) + min)
                });
            }$endif$
        }
    }
}
