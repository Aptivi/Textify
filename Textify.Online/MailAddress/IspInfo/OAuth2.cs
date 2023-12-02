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
    /// The OAuth2 information for the ISP's mail server
    /// </summary>
    [XmlRoot(ElementName = "oAuth2")]
    public class OAuth2
    {
        /// <summary>
        /// The authentication issuer
        /// </summary>
        [XmlElement(ElementName = "issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// The authentication scope
        /// </summary>
        [XmlElement(ElementName = "scope")]
        public string Scope { get; set; }

        /// <summary>
        /// The authentication URL
        /// </summary>
        [XmlElement(ElementName = "authURL")]
        public string AuthURL { get; set; }

        /// <summary>
        /// The OAuth2 token URL
        /// </summary>
        [XmlElement(ElementName = "tokenURL")]
        public string TokenURL { get; set; }
    }

}
