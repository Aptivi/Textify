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

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Textify.Online.MailAddress.IspInfo
{

    /// <summary>
    /// The incoming server information (POP3 or IMAP)
    /// </summary>
    [XmlRoot(ElementName = "incomingServer")]
    public class IncomingServer
    {
        /// <summary>
        /// The hostname for the server
        /// </summary>
        [XmlElement(ElementName = "hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// The port for the server. Usually 995 or 993, depending on the server.
        /// </summary>
        [XmlElement(ElementName = "port")]
        public int Port { get; set; }

        /// <summary>
        /// The socket type. Usually SSL or STARTTLS
        /// </summary>
        [XmlElement(ElementName = "socketType")]
        public string SocketType { get; set; }

        /// <summary>
        /// The username indicator. Usually, it's set to %EMAILADDRESS%, which means the E-mail address placeholder.
        /// </summary>
        [XmlElement(ElementName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// The authentication methods
        /// </summary>
        [XmlElement(ElementName = "authentication")]
        public List<string> Authentication { get; set; }

        /// <summary>
        /// The server type. Usually "imap" or "pop3"
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// POP3 server properties
        /// </summary>
        [XmlElement(ElementName = "pop3")]
        public Pop3 Pop3 { get; set; }
    }

}
