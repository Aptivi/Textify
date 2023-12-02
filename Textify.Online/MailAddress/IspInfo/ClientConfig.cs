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

using System.Xml.Serialization;

namespace Textify.Online.MailAddress.IspInfo
{

    /// <summary>
    /// Client configuration parameters
    /// </summary>
    [XmlRoot(ElementName = "clientConfig")]
    public class ClientConfig
    {
        /// <summary>
        /// The E-mail provider (ISP) information
        /// </summary>
        [XmlElement(ElementName = "emailProvider")]
        public EmailProvider EmailProvider { get; set; }

        /// <summary>
        /// The OAuth2 information for the ISP's mail server
        /// </summary>
        [XmlElement(ElementName = "oAuth2")]
        public OAuth2 OAuth2 { get; set; }

        /// <summary>
        /// The log-in server enablement instructions
        /// </summary>
        [XmlElement(ElementName = "enable")]
        public Enable Enable { get; set; }

        /// <summary>
        /// The webmail configuration
        /// </summary>
        [XmlElement(ElementName = "webMail")]
        public WebMail WebMail { get; set; }
    }

}
