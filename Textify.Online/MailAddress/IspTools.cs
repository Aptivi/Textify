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
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using Textify.Online.MailAddress.IspInfo;

namespace Textify.Online.MailAddress
{
    /// <summary>
    /// Internet Service Provider server information
    /// </summary>
    public static class IspTools
    {
        /// <summary>
        /// Gets the ISP configuration for the specified mail address
        /// </summary>
        /// <param name="address">The mail address to parse. Must include the ISP hostname.</param>
        /// <param name="staging">Whether to use the Thunderbird staging server</param>
        /// <returns>The ISP client config for specified mail address</returns>
        public static ClientConfig GetIspConfig(string address, bool staging = false)
        {
            // Database addresses
            string databaseAddress = "https://autoconfig.thunderbird.net/v1.1/";
            string stagingDatabaseAddress = "https://autoconfig-stage.thunderbird.net/v1.1/";

            // Get the final database address
            string hostName = new Uri($"mailto:{address}").Host;
            string finalDatabaseAddress = $"{databaseAddress}{hostName}";
            if (staging)
                finalDatabaseAddress = $"{stagingDatabaseAddress}{hostName}";

            // Apparently, the XML documents grabbed from the database don't have this below XML header
            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            // Get the XML document for the ISP
            WebClient client = new WebClient();
            xmlBuilder.AppendLine(client.DownloadString(finalDatabaseAddress));
            string xmlContent = xmlBuilder.ToString();

            // Get the client config
            ClientConfig clientConfig;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientConfig), new XmlRootAttribute("clientConfig") { IsNullable = false });
            StringReader sr = new StringReader(xmlContent);
            clientConfig = (ClientConfig)xmlSerializer.Deserialize(sr);
            return clientConfig;
        }
    }
}
