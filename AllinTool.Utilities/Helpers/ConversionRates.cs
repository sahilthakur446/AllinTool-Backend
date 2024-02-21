using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Utilities.Helpers
    {
    public class ConversionRates
        {
        public static readonly Dictionary<(string, string), double> distanceConversionRates = new Dictionary<(string, string), double>
{
    {("kilometer", "meter"), 1000 },
    {("meter", "kilometer"), 1 / 1000.0 },
    {("kilometer", "centimeter"), 100000 },
    {("centimeter", "kilometer"), 1 / 100000.0 },
    {("meter", "centimeter"), 100 },
    {("centimeter", "meter"), 1 / 100.0 },
    {("feet", "meter"), 0.3048 },
    {("meter", "feet"), 1 / 0.3048 },
    {("inch", "centimeter"), 2.54 },
    {("centimeter", "inch"), 1 / 2.54 },
    {("microinch", "inch"), 1 / 1000000.0 },
    {("mile", "kilometer"), 1.60934 },
    {("kilometer", "mile"), 1 / 1.60934 },
    {("feet", "centimeter"), 30.48 }, 
    {("centimeter", "feet"), 1 / 30.48 },
};


        public static readonly Dictionary<(string, string), double> areaConversionRates = new Dictionary<(string, string), double>
    {
        {("squarekilometer", "squaremeter"), 1 * 1000000.0 },
        {("squaremeter", "squarekilometer"), 1 / 1000000.0 },
        {("squarekilometer", "squarecentimeter"), 1 * 1000000.0 * 10000.0 },
        {("squarecentimeter", "squarekilometer"), 1 / (1000000.0 * 10000) },
        {("squaremeter", "squarecentimeter"), 1 * 10000 },
        {("squarecentimeter", "squaremeter"), 1 / 10000.0 },
    };

        public static readonly Dictionary<(string, string), double> volumeConversionRates = new Dictionary<(string, string), double>
    {
        {("cubicmeter", "cubiccentimeter"), 1000000 },
        {("cubiccentimeter", "cubicmeter"), 1 / 1000000.0 },
        {("cubicmeter", "cubicmillimeter"), 1e9 },
        {("cubicmillimeter", "cubicmeter"), 1 / 1e9 },
        {("cubicmeter", "liter"), 1000 },
        {("liter", "cubicmeter"), 1 / 1000.0 },
        {("milliliter", "liter"), 0.001 },
        {("liter", "milliliter"), 1000 },

    };

        public static readonly Dictionary<(string, string), double> temperatureConversionRates = new Dictionary<(string, string), double>
    {
        {("celsius", "fahrenheit"), (9.0 / 5.0) + 32 },
        {("fahrenheit", "celsius"), (5.0 / 9.0) - 32 },
        {("celsius", "kelvin"), 273.15 },
        {("kelvin", "celsius"), -273.15 },
        {("fahrenheit", "kelvin"), 255.372 },
        {("kelvin", "fahrenheit"), -457.87 },
    };

        public static readonly Dictionary<(string, string), double> speedConversionRates = new Dictionary<(string, string), double>
    {
        {("meterpersecond", "kilometerperhour"), 3.6 },
        {("kilometerperhour", "meterpersecond"), 1 / 3.6 },
        {("mileperhour", "kilometerperhour"), 1.60934 },
        {("kilometerperhour", "mileperhour"), 0.621371 },
        {("meterpersecond", "mileperhour"), 2.23694 },
        {("mileperhour", "meterpersecond"), 0.44704 },
    };

        public static readonly Dictionary<(string, string), double> weightConversionRates = new Dictionary<(string, string), double>
    {
        {("kilogram", "gram"), 1000 },
        {("gram", "kilogram"), 1 / 1000.0 },
        {("pound", "ounce"), 16 },
        {("ounce", "pound"), 1 / 16.0 },
        {("kilogram", "pound"), 2.20462 },
        {("pound", "kilogram"), 1 / 2.20462 },
        {("gram", "ounce"), 0.03527396 },
        {("ounce", "gram"), 1 / 0.03527396 },
        {("kilogram", "tonne"), 1 / 1000.0 },
        {("tonne", "kilogram"), 1000 },
        {("ton", "pound"), 2000 },
        {("pound", "ton"), 1 / 2000.0 },
        {("tonne", "gram"), 1e6 },
        {("gram", "tonne"), 1e-6 },
        {("ton", "kilogram"), 907.185 },
        {("kilogram", "ton"), 1 / 907.185 },
        {("grain", "gram"), 0.06479891 },
        {("gram", "grain"), 1 / 0.06479891 },
        {("stone", "pound"), 14 },
        {("pound", "stone"), 1 / 14.0 },
        {("carat", "gram"), 0.2 },
        {("gram", "carat"), 5 },


    };

        public static readonly Dictionary<(string, string), double> powerConversionRates = new Dictionary<(string, string), double>
    {
        {("watt", "kilowatt"), 0.001 },
        {("kilowatt", "watt"), 1000 },
        {("watt", "megawatt"), 1e-6 },
        {("megawatt", "watt"), 1e6 },
        {("horsepower", "watt"), 745.7 },
        {("watt", "horsepower"), 1 / 745.7 },
    };

        public static readonly Dictionary<(string, string), double> energyConversionRates = new Dictionary<(string, string), double>
    {
        {("joule", "kilojoule"), 0.001 },
        {("kilojoule", "joule"), 1000 },
        {("joule", "calorie"), 0.239006 },
        {("calorie", "joule"), 1 / 0.239006 },
        {("joule", "kilocalorie"), 0.000239006 },
        {("kilocalorie", "joule"), 1 / 0.000239006 },
    };

        public static readonly Dictionary<(string, string), double> forceConversionRates = new Dictionary<(string, string), double>
    {
        {("newton", "dyne"), 100000 },
        {("dyne", "newton"), 1 / 100000.0 },
        {("newton", "poundforce"), 0.224809 },
        {("poundforce", "newton"), 1 / 0.224809 },
    };

        public static readonly Dictionary<(string, string), double> dataSizeConversionRates = new Dictionary<(string, string), double>
    {
       {("bit", "byte"), 0.125 },
{("byte", "kilobyte"), 1 / 1024.0 },
{("kilobyte", "megabyte"), 1 / 1024.0 },
{("megabyte", "gigabyte"), 1 / 1024.0 },
{("gigabyte", "terabyte"), 1 / 1024.0 },
{("terabyte", "petabyte"), 1 / 1024.0 },
{("petabyte", "exabyte"), 1 / 1024.0 },
{("exabyte", "zettabyte"), 1 / 1024.0 },
{("zettabyte", "yottabyte"), 1 / 1024.0 },
{("bit", "kilobyte"), 1 / 8192.0 },
{("bit", "megabyte"), 1 / 8388608.0 },
{("kilobyte", "gigabyte"), 1 / 1048576.0 },
{("megabyte", "exabyte"), 1 / 1048576.0 },
{("byte", "gigabyte"), 1 / (1024.0 * 1024.0) },
{("gigabyte", "megabyte"), 1024.0 },
{("megabyte", "kilobyte"), 1024.0 },
{("kilobyte", "bit"), 8192.0 },
{("terabyte", "gigabyte"), 1024.0 },
{("petabyte", "gigabyte"), 1024.0 },
{("exabyte", "petabyte"), 1024.0 },
{("zettabyte", "exabyte"), 1024.0 },
{("yottabyte", "zettabyte"), 1024.0 },
{("bit", "gigabyte"), 1 / 8589934592.0 },
{("kilobyte", "terabyte"), 1 / 1073741824.0 },
{("megabyte", "petabyte"), 1 / 1073741824.0 },
{("gigabyte", "exabyte"), 1 / 1073741824.0 },
{("terabyte", "exabyte"), 1 / 1024.0 },
{("petabyte", "zettabyte"), 1 / 1024.0 }

    };
        }
    }
