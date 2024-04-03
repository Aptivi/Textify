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
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Textify.General;
using Textify.Tools;

namespace Textify.NameGen
{
    /// <summary>
    /// Name generator class
    /// </summary>
	public static class NameGenerator
    {
        internal static Dictionary<NameGenderType, string[]> Names = [];
        internal static string[] Surnames = [];

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
                if (!Names.ContainsKey(genderType))
                {
                    LoadData(genderType);
                    (byte[] bytes, string fileName) =
                        genderType == NameGenderType.Female ? (DataTools.GetDataFrom("FirstNames_Female"), "FirstNames_Female.txt") :
                        genderType == NameGenderType.Male ? (DataTools.GetDataFrom("FirstNames_Male"), "FirstNames_Male.txt") :
                        genderType == NameGenderType.FemaleImplicit ? (DataTools.GetDataFrom("FirstNames_Female_Implicit"), "FirstNames_Female_Implicit.txt") :
                        genderType == NameGenderType.MaleImplicit ? (DataTools.GetDataFrom("FirstNames_Male_Implicit"), "FirstNames_Male_Implicit.txt") :
                        genderType == NameGenderType.Natural ? (DataTools.GetDataFrom("FirstNames_Natural"), "FirstNames_Natural.txt") :
                        (DataTools.GetDataFrom("FirstNames"), "FirstNames.txt");
                    var contentStream = new MemoryStream(bytes);
                    var archive = new ZipArchive(contentStream, ZipArchiveMode.Read);

                    // Open the XML to stream
                    var content = archive.GetEntry(fileName).Open();
                    var read = await new StreamReader(content).ReadToEndAsync();
                    Names.Add(genderType, read.SplitNewLines());
                }
                if (Surnames.Length == 0)
                {
                    DataInitializer.Initialize(DataType.Surnames);
                    (byte[] bytes, string fileName) = (DataTools.GetDataFrom("Surnames"), "Surnames.txt");
                    var contentStream = new MemoryStream(bytes);
                    var archive = new ZipArchive(contentStream, ZipArchiveMode.Read);

                    // Open the XML to stream
                    var content = archive.GetEntry(fileName).Open();
                    var read = await new StreamReader(content).ReadToEndAsync();
                    Surnames = read.SplitNewLines();
                }
            }
            catch (Exception ex)
            {
                throw new TextifyException("Can't get names and surnames:" + $" {ex.Message}", ex);
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
            return GenerateNameArray(Count, NamePrefix, NameSuffix, SurnamePrefix, SurnameSuffix, genderType);
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
            return GenerateNameArray(Count, NamePrefix, NameSuffix, SurnamePrefix, SurnameSuffix, genderType);
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
            return GenerateFirstNameArray(Count, NamePrefix, NameSuffix, genderType);
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
            return GenerateFirstNameArray(Count, NamePrefix, NameSuffix, genderType);
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
            return GenerateFirstNameArray(nameSearchTerm, NamePrefix, NameSuffix, genderType);
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
            return GenerateFirstNameArray(nameSearchTerm, NamePrefix, NameSuffix, genderType);
        }

        /// <summary>
        /// Gets all the first names
        /// </summary>
        /// <param name="genderType">Gender type to use when generating names</param>
        /// <returns>List of all the first names</returns>
        public static string[] GetAllFirstNames(NameGenderType genderType = NameGenderType.Unified) =>
            FindFirstNames("", "", "", genderType);

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

        /// <summary>
        /// Gets all the last names
        /// </summary>
        /// <returns>List of all the last names</returns>
        public static string[] GetAllLastNames() =>
            FindLastNames("", "", "");

        private static string[] GenerateFirstNameArray(int Count, string NamePrefix, string NameSuffix, NameGenderType genderType)
        {
            var random = new Random();
            List<string> namesList = [];
            string[] ProcessedNames = ProcessConditions(Names[genderType], NamePrefix, NameSuffix);

            // Check the names
            if (ProcessedNames.Length == 0)
                throw new TextifyException("The names are not found! Please ensure that the name conditions are correct.");

            // Select random names
            for (int NameNum = 1; NameNum <= Count; NameNum++)
            {
                // Get the names
                string GeneratedName = ProcessedNames[random.Next(ProcessedNames.Length)];
                namesList.Add(GeneratedName);
            }
            return [.. namesList];
        }

        private static string[] GenerateLastNameArray(int Count, string SurnamePrefix, string SurnameSuffix)
        {
            var random = new Random();
            List<string> surnamesList = [];
            string[] ProcessedSurnames = ProcessConditions(Surnames, SurnamePrefix, SurnameSuffix);

            // Check the surnames
            if (ProcessedSurnames.Length == 0)
                throw new TextifyException("The surnames are not found! Please ensure that the surname conditions are correct.");

            // Select random surnames
            for (int NameNum = 1; NameNum <= Count; NameNum++)
            {
                // Get the surnames
                string GeneratedSurname = ProcessedSurnames[random.Next(ProcessedSurnames.Length)];
                surnamesList.Add(GeneratedSurname);
            }
            return [.. surnamesList];
        }

        private static string[] GenerateFirstNameArray(string nameSearchTerm, string NamePrefix, string NameSuffix, NameGenderType genderType)
        {
            // Process the names according to suffix and/or prefix check requirement
            string[] ProcessedNames = ProcessConditions(Names[genderType], NamePrefix, NameSuffix, nameSearchTerm);

            // Check the names
            if (ProcessedNames.Length == 0)
                throw new TextifyException("The names are not found! Please ensure that the name conditions are correct.");

            return ProcessedNames;
        }

        private static string[] GenerateLastNameArray(string nameSearchTerm, string SurnamePrefix, string SurnameSuffix)
        {
            // Process the surnames according to suffix and/or prefix check requirement
            string[] ProcessedSurnames = ProcessConditions(Surnames, SurnamePrefix, SurnameSuffix, nameSearchTerm);

            // Check the surnames
            if (ProcessedSurnames.Length == 0)
                throw new TextifyException("The surnames are not found! Please ensure that the surname conditions are correct.");

            return ProcessedSurnames;
        }

        private static string[] GenerateNameArray(int Count, string NamePrefix, string NameSuffix, string SurnamePrefix, string SurnameSuffix, NameGenderType genderType)
        {
            List<string> namesList = [];

            // Get random names and surnames
            string[] names = GenerateFirstNameArray(Count, NamePrefix, NameSuffix, genderType);
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

        private static string[] ProcessConditions(string[] data, string prefix, string suffix, string searchTerm = "")
        {
            // Check if the prefix and suffix check is required
            bool SurnamePrefixCheckRequired = !string.IsNullOrEmpty(prefix);
            bool SurnameSuffixCheckRequired = !string.IsNullOrEmpty(suffix);

            // Process the surnames according to suffix and/or prefix check requirement
            string[] ProcessedSurnames = Surnames;
            if (SurnamePrefixCheckRequired && SurnameSuffixCheckRequired)
                data = Surnames.Where((str) => str.StartsWith(prefix) && str.EndsWith(suffix)).ToArray();
            else if (SurnamePrefixCheckRequired)
                data = Surnames.Where((str) => str.StartsWith(prefix)).ToArray();
            else if (SurnameSuffixCheckRequired)
                data = Surnames.Where((str) => str.EndsWith(suffix)).ToArray();
            return data.Where((str) => str.Contains(searchTerm)).ToArray();
        }

        private static void LoadData(NameGenderType gender)
        {
            switch (gender)
            {
                case NameGenderType.Unified:
                    DataInitializer.Initialize(DataType.Names);
                    break;
                case NameGenderType.Female:
                    DataInitializer.Initialize(DataType.NamesFemale);
                    break;
                case NameGenderType.Male:
                    DataInitializer.Initialize(DataType.NamesMale);
                    break;
                case NameGenderType.FemaleImplicit:
                    DataInitializer.Initialize(DataType.NamesFemaleImplicit);
                    break;
                case NameGenderType.MaleImplicit:
                    DataInitializer.Initialize(DataType.NamesMaleImplicit);
                    break;
                case NameGenderType.Natural:
                    DataInitializer.Initialize(DataType.NamesNatural);
                    break;
                default:
                    throw new TextifyException($"Gender type {gender} is invalid.");
            }
        }
    }
}
