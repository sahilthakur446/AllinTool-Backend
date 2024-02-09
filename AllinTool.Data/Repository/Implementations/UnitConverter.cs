using AllinTool.Data.Repository.Abstract;
using AllinTool.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Repository.Implementation
    {
    public class UnitConverter : IUnitConverter
        {
        private double conversionRate;
        public double ConvertedValue { get; set; }

        public bool ConvertUnits(string from, string to, double input, string unitType)
            {
            switch (unitType)
                {
                case "distance":
                    return Converter(ConversionRates.distanceConversionRates, from, to, input);

                case "area":
                    return Converter(ConversionRates.areaConversionRates, from, to, input);

                case "volume":
                    return Converter(ConversionRates.volumeConversionRates, from, to, input);

                case "weight":
                    return Converter(ConversionRates.weightConversionRates, from, to, input);

                case "speed":
                    return Converter(ConversionRates.speedConversionRates, from, to, input);

                case "temperature":
                    return Converter(ConversionRates.temperatureConversionRates, from, to, input);

                case "energy":
                    return Converter(ConversionRates.energyConversionRates, from, to, input);

                case "force":
                    return Converter(ConversionRates.forceConversionRates, from, to, input);

                case "power":
                    return Converter(ConversionRates.powerConversionRates, from, to, input);

                case "datasize":
                    return Converter(ConversionRates.dataSizeConversionRates, from, to, input);
                default:
                    return false;
                }
            }

        private bool Converter(Dictionary<(string, string), double> conversionRatesDictionary, string from, string to, double input)
            {
            if (from == to)
                {
                conversionRate = 1;
                return CalculateConvertedValue(conversionRate, true, input);
                }
            if (conversionRatesDictionary.ContainsKey(("celsius", "fahrenheit")))
                {
                return ConvertTemperature(from, to, input);
                }
            else
                {
                bool result = conversionRatesDictionary.TryGetValue((from.ToLower(), to.ToLower()), out conversionRate);
                return CalculateConvertedValue(conversionRate, result, input);
                }
            }

        private bool CalculateConvertedValue(double conversionRate, bool result, double input)
            {
            if (result)
                {
                ConvertedValue = input * conversionRate;
                return true;
                }
            else
                {
                return false;
                }

            }


        private bool ConvertTemperature(string from, string to, double input)
            {
            switch ((from.ToLower(), to.ToLower()))
                {
                case ("celsius", "fahrenheit"):
                    ConvertedValue = ( input * 9.0 / 5.0 ) + 32;
                    return true;

                case ("fahrenheit", "celsius"):
                    ConvertedValue = ( input - 32 ) * 5.0 / 9.0;
                    return true;

                case ("celsius", "kelvin"):
                    ConvertedValue = input + 273.15;
                    return true;

                case ("kelvin", "celsius"):
                    ConvertedValue = input - 273.15;
                    return true;

                case ("fahrenheit", "kelvin"):
                    ConvertedValue = ( input - 32 ) * 5.0 / 9.0 + 273.15;
                    return true;

                case ("kelvin", "fahrenheit"):
                    ConvertedValue = ( input - 273.15 ) * 9.0 / 5.0 + 32;
                    return true;

                default:
                    return false;
                }
            }
        }
    }
