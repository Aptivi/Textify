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
    /// The E-mail provider (ISP) information
    /// </summary>
    [XmlRoot(ElementName = "emailProvider")]
    public class EmailProvider
    {
        /// <summary>
        /// The list of domains
        /// </summary>
        [XmlElement(ElementName = "domain")]
        public List<string> Domain { get; set; }

        /// <summary>
        /// The full name for the ISP mail server
        /// </summary>
        [XmlElement(ElementName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The short name for the ISP mail server
        /// </summary>
        [XmlElement(ElementName = "displayShortName")]
        public string DisplayShortName { get; set; }

        /// <summary>
        /// List of incoming servers
        /// </summary>
        [XmlElement(ElementName = "incomingServer")]
        public List<IncomingServer> IncomingServer { get; set; }

        /// <summary>
        /// Outgoing server
        /// </summary>
        [XmlElement(ElementName = "outgoingServer")]
        public OutgoingServer OutgoingServer { get; set; }

        /// <summary>
        /// Documentation information
        /// </summary>
        [XmlElement(ElementName = "documentation")]
        public List<Documentation> Documentation { get; set; }

        /// <summary>
        /// The dominating domain
        /// </summary>
        [XmlAttribute(AttributeName = "id")]
        public string DominatingDomain { get; set; }
    }

}
