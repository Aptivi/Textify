//
// Textify  Copyright (C) 2023-2025  Aptivi
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

using SpecProbe.Loader;
using SpecProbe.Software.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Textify.General;
using Textify.Tools;

namespace Textify.Data.Unicode.Icu
{
    internal static unsafe class NativeLoader
    {
        internal static string libicuucLibPath = GetLibPath("icuuc77");
        internal static string libicudtLibPath = GetLibPath("icudt77");
        internal static LibraryManager? libManagerIcu;
        internal static bool loaded;
        internal static int apiVersion = 0;

        /// <summary>
        /// Absolute path to the libicuuc library
        /// </summary>
        internal static string LibraryPath =>
            libicuucLibPath;

        /// <summary>
        /// Absolute path to the libicudt library
        /// </summary>
        internal static string DataLibraryPath =>
            libicudtLibPath;

        /// <summary>
        /// Initializes the libicuuc library
        /// </summary>
        internal static void InitializeLibrary() =>
            InitializeLibrary(libicuucLibPath, libicudtLibPath);

        /// <summary>
        /// Initializes the libicuuc library
        /// </summary>
        /// <param name="libPath">Absolute path to the libicuuc library</param>
        /// <param name="dataLibPath">Absolute path to the libicudt library</param>
        internal static void InitializeLibrary(string libPath, string dataLibPath)
        {
            // Set the library path
            string oldLibPath = libicuucLibPath;
            string oldDataLibPath = libicudtLibPath;
            libicuucLibPath = libPath;
            libicudtLibPath = dataLibPath;

            try
            {
                // Start the library up
                var architecture = PlatformHelper.GetArchitecture();
                if (architecture == Architecture.X86 || architecture == Architecture.Arm)
                    throw new TextifyException("32-bit platforms are no longer supported.");
                if (PlatformHelper.IsOnMacOS())
                    libManagerIcu = new LibraryManager(new LibraryFile("libicucore.dylib"));
                else
                {
                    List<string> icudtPaths = [libicudtLibPath, "libicudata.so"];
                    List<string> icuucPaths = [libicuucLibPath, "libicuuc.so"];
                    if (PlatformHelper.IsOnUnix())
                    {
                        string latestLibVerData = GetLatestLibVersion("libicudata");
                        string latestLibVerUc = GetLatestLibVersion("libicuuc");
                        if (!string.IsNullOrEmpty(latestLibVerData))
                            icudtPaths.Add(latestLibVerData);
                        if (!string.IsNullOrEmpty(latestLibVerUc))
                            icuucPaths.Add(latestLibVerUc);
                    }
                    libManagerIcu = new LibraryManager(
                        new LibraryFile([.. icudtPaths]),
                        new LibraryFile([.. icuucPaths])
                    );
                }
                libManagerIcu.LoadNativeLibrary();

                // Verify that this library works
                byte[] verBytes = new byte[4];
                var verDelegate = GetDelegate<NativeInterop.u_getVersion>(libManagerIcu, nameof(NativeInterop.u_getVersion));
                verDelegate.Invoke(verBytes);
                apiVersion = verBytes[0];
            }
            catch (Exception ex)
            {
                libicuucLibPath = oldLibPath;
                libicudtLibPath = oldDataLibPath;
                throw new TextifyException($"Failed to load library. {libicuucLibPath}. {ex.Message}");
            }
            loaded = true;
        }

        internal static string GetLibPath(string libName)
        {
            var asm = typeof(NativeLoader).Assembly;
            return GetLibPath(Path.GetDirectoryName(asm.Location), libName);
        }

        internal static string GetLibPath(string root, string libName)
        {
            string runtimesPath = root + "/";
            string lowerArch = RuntimeInformation.OSArchitecture.ToString().ToLower();
            if (PlatformHelper.IsOnWindows())
                runtimesPath += $"runtimes/win-{lowerArch}/native/{libName}.dll";
            else if (PlatformHelper.IsOnMacOS())
                runtimesPath += $"runtimes/osx-{lowerArch}/native/{libName}.dylib";
            else if (PlatformHelper.IsOnUnix())
                runtimesPath += $"runtimes/linux-{lowerArch}/native/{libName}.so";
            else
                runtimesPath += $"runtimes/freebsd-{lowerArch}/native/{libName}.so";
            return runtimesPath;
        }

        internal static TDelegate GetDelegate<TDelegate>(LibraryManager? libraryManager, string function, bool versioned = true)
            where TDelegate : Delegate
        {
            if (libraryManager is null)
                throw new TextifyException($"Can't get delegate for {function} without initializing the library first");
            if (!versioned)
            {
                return libraryManager.GetNativeMethodDelegate<TDelegate>(function) ??
                    throw new TextifyException($"Can't get delegate for {function}");
            }
            else
            {
                if (apiVersion == 0)
                {
                    for (int i = 0; i <= 1000; i++)
                    {
                        var del = libraryManager.GetNativeMethodDelegate<TDelegate>(function + $"_{i}");
                        if (del is null)
                            continue;
                        return del;
                    }
                }
                else
                {
                    var del = libraryManager.GetNativeMethodDelegate<TDelegate>(function + $"_{apiVersion}");
                    if (del is not null)
                        return del;
                }
                return libraryManager.GetNativeMethodDelegate<TDelegate>(function) ??
                    throw new TextifyException($"Can't get delegate for {function}");
            }
        }

        internal static string GetLatestLibVersion(string libName)
        {
            string finalLibName = $"{libName}.so";
            string unprefixedLibName = finalLibName.RemovePrefix("lib");
            string archName = PlatformHelper.IsOnArm64() ? "aarch64" : "x86_64";
            string[] commonPaths =
            [
                "/lib",
                "/usr/lib",
                "/usr/local/lib",
                "/lib64",
                "/usr/lib64",
                "/usr/local/lib64",
            ];

            // Find the library in these paths
            foreach (string path in commonPaths.Concat(commonPaths.Select((path) => Path.Combine(path, $"{archName}-linux-gnu"))))
            {
                if (!Directory.Exists(path))
                    continue;

                try
                {
                    // Search for versioned files
                    var libFiles = Directory.GetFiles(path, finalLibName + "*")
                        .Concat(Directory.GetFiles(path, unprefixedLibName + "*"));
                    foreach (string file in libFiles.OrderDescendLogically())
                    {
                        if (File.Exists(file))
                            return Path.GetFileName(file);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return "";
        }
    }
}
