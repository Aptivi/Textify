//
// Textify  Copyright (C) 2023-2024  Aptivi
//
// This file is part of Textify
//
// Textify is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Textify is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Textify.Online.NameGen
{
    /// <summary>
    /// Name generator class
    /// </summary>
	public static class NameGenerator
    {
        internal static string[] Names = [];
        internal static string[] Surnames = [];
        private static NameGenderType lastGenderType = NameGenderType.Unified;
        private static readonly HttpClient NameClient = new();
        private static readonly string nameAddressPart = "https://cdn.jsdelivr.net/gh/Aptivi/NamesList@latest/Processed/";
        private static readonly string unifiedNameListFileName = "FirstNames.txt";
        private static readonly string femaleNameListFileName = "FirstNames_Female.txt";
        private static readonly string maleNameListFileName = "FirstNames_Male.txt";
        private static readonly string surnameListFileName = "Surnames.txt";

        /// <summary>
        /// Populates the names and the surnames for the purpose of initialization
        /// </summary>
        /// <param name="genderType">Gender type to use when generating names</param>
        public static void PopulateNames(NameGenderType genderType = NameGenderType.Unified) =>
            PopulateNamesAsync(genderType).Wait();

        /// <summary>
        /// [Async] Populates the names and the surnames for the purpose of initialization
        /// </summary>
        /// <param name="genderType">Gender type to use when generating names</param>
        public static async Task PopulateNamesAsync(NameGenderType genderType = NameGenderType.Unified)
        {
            try
            {
                string surnameAddress = $"{nameAddressPart}{surnameListFileName}";
                string namesFileName =
                    genderType == NameGenderType.Female ? femaleNameListFileName :
                    genderType == NameGenderType.Male ? maleNameListFileName :
                    unifiedNameListFileName;
                string nameAddress = $"{nameAddressPart}{namesFileName}";

                if (Names.Length == 0 || genderType != lastGenderType)
                    Names = await PopulateInternalAsync(nameAddress);
                if (Surnames.Length == 0)
                    Surnames = await PopulateInternalAsync(surnameAddress);
                lastGenderType = genderType;
            }
            catch (Exception ex)
            {
                throw new Exception("Can't get names and surnames:" + $" {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Generates the names
        /// </summary>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateNames(NameGenderType genderType = NameGenderType.Unified) =>
            GenerateNames(10, "", "", "", "", genderType);

        /// <summary>
        /// Generates the names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateNames(int Count, NameGenderType genderType = NameGenderType.Unified) =>
            GenerateNames(Count, "", "", "", "", genderType);

        /// <summary>
        /// Generates the names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="NamePrefix">What should the name start with?</param>
        /// <param name="NameSuffix">What should the name end with?</param>
        /// <param name="SurnamePrefix">What should the surname start with?</param>
        /// <param name="SurnameSuffix">What should the surname end with?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateNames(int Count, string NamePrefix, string NameSuffix, string SurnamePrefix, string SurnameSuffix, NameGenderType genderType = NameGenderType.Unified)
        {
            // Initialize names
            PopulateNames(genderType);
            return GenerateNameArray(Count, NamePrefix, NameSuffix, SurnamePrefix, SurnameSuffix);
        }

        /// <summary>
        /// [Async] Generates the names
        /// </summary>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateNamesAsync(NameGenderType genderType = NameGenderType.Unified) =>
            await GenerateNamesAsync(10, "", "", "", "", genderType);

        /// <summary>
        /// [Async] Generates the names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateNamesAsync(int Count, NameGenderType genderType = NameGenderType.Unified) =>
            await GenerateNamesAsync(Count, "", "", "", "", genderType);

        /// <summary>
        /// [Async] Generates the names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="NamePrefix">What should the name start with?</param>
        /// <param name="NameSuffix">What should the name end with?</param>
        /// <param name="SurnamePrefix">What should the surname start with?</param>
        /// <param name="SurnameSuffix">What should the surname end with?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateNamesAsync(int Count, string NamePrefix, string NameSuffix, string SurnamePrefix, string SurnameSuffix, NameGenderType genderType = NameGenderType.Unified)
        {
            // Initialize names
            await PopulateNamesAsync(genderType);
            return GenerateNameArray(Count, NamePrefix, NameSuffix, SurnamePrefix, SurnameSuffix);
        }

        /// <summary>
        /// Generates the first names
        /// </summary>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateFirstNames(NameGenderType genderType = NameGenderType.Unified) =>
            GenerateFirstNames(10, "", "", genderType);

        /// <summary>
        /// Generates the first names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateFirstNames(int Count, NameGenderType genderType = NameGenderType.Unified) =>
            GenerateFirstNames(Count, "", "", genderType);

        /// <summary>
        /// Generates the first names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="NamePrefix">What should the name start with?</param>
        /// <param name="NameSuffix">What should the name end with?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateFirstNames(int Count, string NamePrefix, string NameSuffix, NameGenderType genderType = NameGenderType.Unified)
        {
            // Initialize names
            PopulateNames(genderType);
            return GenerateFirstNameArray(Count, NamePrefix, NameSuffix);
        }

        /// <summary>
        /// [Async] Generates the first names
        /// </summary>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateFirstNamesAsync(NameGenderType genderType = NameGenderType.Unified) =>
            await GenerateFirstNamesAsync(10, "", "", genderType);

        /// <summary>
        /// [Async] Generates the first names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateFirstNamesAsync(int Count, NameGenderType genderType = NameGenderType.Unified) =>
            await GenerateFirstNamesAsync(Count, "", "", genderType);

        /// <summary>
        /// [Async] Generates the first names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="NamePrefix">What should the name start with?</param>
        /// <param name="NameSuffix">What should the name end with?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateFirstNamesAsync(int Count, string NamePrefix, string NameSuffix, NameGenderType genderType = NameGenderType.Unified)
        {
            // Initialize names
            await PopulateNamesAsync(genderType);
            return GenerateFirstNameArray(Count, NamePrefix, NameSuffix);
        }

        /// <summary>
        /// Generates the last names
        /// </summary>
        /// <returns>List of generated names</returns>
        public static string[] GenerateLastNames() =>
            GenerateLastNames(10, "", "");

        /// <summary>
        /// Generates the last names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateLastNames(int Count) =>
            GenerateLastNames(Count, "", "");

        /// <summary>
        /// Generates the last names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="SurnamePrefix">What should the surname start with?</param>
        /// <param name="SurnameSuffix">What should the surname end with?</param>
        /// <returns>List of generated names</returns>
        public static string[] GenerateLastNames(int Count, string SurnamePrefix, string SurnameSuffix)
        {
            // Initialize names
            PopulateNames();
            return GenerateLastNameArray(Count, SurnamePrefix, SurnameSuffix);
        }

        /// <summary>
        /// [Async] Generates the last names
        /// </summary>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateLastNamesAsync() =>
            await GenerateLastNamesAsync(10, "", "");

        /// <summary>
        /// [Async] Generates the last names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateLastNamesAsync(int Count) =>
            await GenerateLastNamesAsync(Count, "", "");

        /// <summary>
        /// [Async] Generates the last names
        /// </summary>
        /// <param name="Count">How many names to generate?</param>
        /// <param name="SurnamePrefix">What should the surname start with?</param>
        /// <param name="SurnameSuffix">What should the surname end with?</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> GenerateLastNamesAsync(int Count, string SurnamePrefix, string SurnameSuffix)
        {
            // Initialize names
            await PopulateNamesAsync();
            return GenerateLastNameArray(Count, SurnamePrefix, SurnameSuffix);
        }

        /// <summary>
        /// Generates the first names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] FindFirstNames(string nameSearchTerm, NameGenderType genderType = NameGenderType.Unified) =>
            FindFirstNames(nameSearchTerm, "", "", genderType);

        /// <summary>
        /// Generates the first names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <param name="NamePrefix">What should the name start with?</param>
        /// <param name="NameSuffix">What should the name end with?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static string[] FindFirstNames(string nameSearchTerm, string NamePrefix, string NameSuffix, NameGenderType genderType = NameGenderType.Unified)
        {
            // Initialize names
            PopulateNames(genderType);
            return GenerateFirstNameArray(nameSearchTerm, NamePrefix, NameSuffix);
        }

        /// <summary>
        /// [Async] Generates the first names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> FindFirstNamesAsync(string nameSearchTerm, NameGenderType genderType = NameGenderType.Unified) =>
            await FindFirstNamesAsync(nameSearchTerm, "", "", genderType);

        /// <summary>
        /// [Async] Generates the first names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <param name="NamePrefix">What should the name start with?</param>
        /// <param name="NameSuffix">What should the name end with?</param>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> FindFirstNamesAsync(string nameSearchTerm, string NamePrefix, string NameSuffix, NameGenderType genderType = NameGenderType.Unified)
        {
            // Initialize names
            await PopulateNamesAsync(genderType);
            return GenerateFirstNameArray(nameSearchTerm, NamePrefix, NameSuffix);
        }

        /// <summary>
        /// Generates the last names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <returns>List of generated names</returns>
        public static string[] FindLastNames(string nameSearchTerm) =>
            FindLastNames(nameSearchTerm, "", "");

        /// <summary>
        /// Generates the last names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <param name="SurnamePrefix">What should the surname start with?</param>
        /// <param name="SurnameSuffix">What should the surname end with?</param>
        /// <returns>List of generated names</returns>
        public static string[] FindLastNames(string nameSearchTerm, string SurnamePrefix, string SurnameSuffix)
        {
            // Initialize names
            PopulateNames();
            return GenerateLastNameArray(nameSearchTerm, SurnamePrefix, SurnameSuffix);
        }

        /// <summary>
        /// [Async] Generates the last names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> FindLastNamesAsync(string nameSearchTerm) =>
            await FindLastNamesAsync(nameSearchTerm, "", "");

        /// <summary>
        /// [Async] Generates the last names
        /// </summary>
        /// <param name="nameSearchTerm">Search term for the name</param>
        /// <param name="SurnamePrefix">What should the surname start with?</param>
        /// <param name="SurnameSuffix">What should the surname end with?</param>
        /// <returns>List of generated names</returns>
        public static async Task<string[]> FindLastNamesAsync(string nameSearchTerm, string SurnamePrefix, string SurnameSuffix)
        {
            // Initialize names
            await PopulateNamesAsync();
            return GenerateLastNameArray(nameSearchTerm, SurnamePrefix, SurnameSuffix);
        }

        internal static string[] GenerateFirstNameArray(int Count, string NamePrefix, string NameSuffix)
        {
            var random = new Random();
            List<string> namesList = [];

            // Check if the prefix and suffix check is required
            bool NamePrefixCheckRequired = !string.IsNullOrEmpty(NamePrefix);
            bool NameSuffixCheckRequired = !string.IsNullOrEmpty(NameSuffix);

            // Process the names according to suffix and/or prefix check requirement
            string[] ProcessedNames = Names;
            if (NamePrefixCheckRequired && NameSuffixCheckRequired)
                ProcessedNames = Names.Where((str) => str.StartsWith(NamePrefix) && str.EndsWith(NameSuffix)).ToArray();
            else if (NamePrefixCheckRequired)
                ProcessedNames = Names.Where((str) => str.StartsWith(NamePrefix)).ToArray();
            else if (NameSuffixCheckRequired)
                ProcessedNames = Names.Where((str) => str.EndsWith(NameSuffix)).ToArray();

            // Check the names
            if (ProcessedNames.Length == 0)
                throw new Exception("The names are not found! Please ensure that the name conditions are correct.");

            // Select random names
            for (int NameNum = 1; NameNum <= Count; NameNum++)
            {
                // Get the names
                string GeneratedName = ProcessedNames[random.Next(ProcessedNames.Length)];
                namesList.Add(GeneratedName);
            }
            return [.. namesList];
        }

        internal static string[] GenerateLastNameArray(int Count, string SurnamePrefix, string SurnameSuffix)
        {
            var random = new Random();
            List<string> surnamesList = [];

            // Check if the prefix and suffix check is required
            bool SurnamePrefixCheckRequired = !string.IsNullOrEmpty(SurnamePrefix);
            bool SurnameSuffixCheckRequired = !string.IsNullOrEmpty(SurnameSuffix);

            // Process the surnames according to suffix and/or prefix check requirement
            string[] ProcessedSurnames = Surnames;
            if (SurnamePrefixCheckRequired && SurnameSuffixCheckRequired)
                ProcessedSurnames = Surnames.Where((str) => str.StartsWith(SurnamePrefix) && str.EndsWith(SurnameSuffix)).ToArray();
            else if (SurnamePrefixCheckRequired)
                ProcessedSurnames = Surnames.Where((str) => str.StartsWith(SurnamePrefix)).ToArray();
            else if (SurnameSuffixCheckRequired)
                ProcessedSurnames = Surnames.Where((str) => str.EndsWith(SurnameSuffix)).ToArray();

            // Check the surnames
            if (ProcessedSurnames.Length == 0)
                throw new Exception("The surnames are not found! Please ensure that the surname conditions are correct.");

            // Select random surnames
            for (int NameNum = 1; NameNum <= Count; NameNum++)
            {
                // Get the surnames
                string GeneratedSurname = ProcessedSurnames[random.Next(ProcessedSurnames.Length)];
                surnamesList.Add(GeneratedSurname);
            }
            return [.. surnamesList];
        }

        internal static string[] GenerateFirstNameArray(string nameSearchTerm, string NamePrefix, string NameSuffix)
        {
            var random = new Random();
            List<string> namesList = [];

            // Check if the prefix and suffix check is required
            bool NamePrefixCheckRequired = !string.IsNullOrEmpty(NamePrefix);
            bool NameSuffixCheckRequired = !string.IsNullOrEmpty(NameSuffix);

            // Process the names according to suffix and/or prefix check requirement
            string[] ProcessedNames = Names;
            if (NamePrefixCheckRequired && NameSuffixCheckRequired)
                ProcessedNames = Names.Where((str) => str.StartsWith(NamePrefix) && str.EndsWith(NameSuffix)).ToArray();
            else if (NamePrefixCheckRequired)
                ProcessedNames = Names.Where((str) => str.StartsWith(NamePrefix)).ToArray();
            else if (NameSuffixCheckRequired)
                ProcessedNames = Names.Where((str) => str.EndsWith(NameSuffix)).ToArray();
            ProcessedNames = ProcessedNames.Where(str => str.Contains(nameSearchTerm)).ToArray();

            // Check the names
            if (ProcessedNames.Length == 0)
                throw new Exception("The names are not found! Please ensure that the name conditions are correct.");

            return ProcessedNames;
        }

        internal static string[] GenerateLastNameArray(string nameSearchTerm, string SurnamePrefix, string SurnameSuffix)
        {
            var random = new Random();
            List<string> surnamesList = [];

            // Check if the prefix and suffix check is required
            bool SurnamePrefixCheckRequired = !string.IsNullOrEmpty(SurnamePrefix);
            bool SurnameSuffixCheckRequired = !string.IsNullOrEmpty(SurnameSuffix);

            // Process the surnames according to suffix and/or prefix check requirement
            string[] ProcessedSurnames = Surnames;
            if (SurnamePrefixCheckRequired && SurnameSuffixCheckRequired)
                ProcessedSurnames = Surnames.Where((str) => str.StartsWith(SurnamePrefix) && str.EndsWith(SurnameSuffix)).ToArray();
            else if (SurnamePrefixCheckRequired)
                ProcessedSurnames = Surnames.Where((str) => str.StartsWith(SurnamePrefix)).ToArray();
            else if (SurnameSuffixCheckRequired)
                ProcessedSurnames = Surnames.Where((str) => str.EndsWith(SurnameSuffix)).ToArray();
            ProcessedSurnames = ProcessedSurnames.Where(str => str.Contains(nameSearchTerm)).ToArray();

            // Check the surnames
            if (ProcessedSurnames.Length == 0)
                throw new Exception("The surnames are not found! Please ensure that the surname conditions are correct.");

            return ProcessedSurnames;
        }

        internal static string[] GenerateNameArray(int Count, string NamePrefix, string NameSuffix, string SurnamePrefix, string SurnameSuffix)
        {
            List<string> namesList = [];

            // Get random names and surnames
            string[] names = GenerateFirstNameArray(Count, NamePrefix, NameSuffix);
            string[] surnames = GenerateLastNameArray(Count, SurnamePrefix, SurnameSuffix);

            // Select random names
            for (int NameNum = 1; NameNum <= Count; NameNum++)
            {
                // Get the names
                string GeneratedName = names[NameNum - 1];
                string GeneratedSurname = surnames[NameNum - 1];
                namesList.Add(GeneratedName + " " + GeneratedSurname);
            }
            return [.. namesList];
        }

        internal static async Task<string[]> PopulateInternalAsync(string nameLink)
        {
            HttpResponseMessage Response = await NameClient.GetAsync(nameLink);
            Response.EnsureSuccessStatusCode();
            Stream SurnamesStream = await Response.Content.ReadAsStreamAsync();
            string SurnamesString = new StreamReader(SurnamesStream).ReadToEnd();
            return SurnamesString.SplitNewLines();
        }

        /// <summary>
        /// Makes a string array with new line as delimiter
        /// </summary>
        /// <param name="Str">Target string</param>
        /// <returns></returns>
        internal static string[] SplitNewLines(this string Str) =>
            Str.Replace(Convert.ToChar(13), default).Split(Convert.ToChar(10));
    }
}
