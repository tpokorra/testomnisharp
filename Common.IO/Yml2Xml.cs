//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
//
// Copyright 2004-2015 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Ict.Common.IO
{
    /// <summary>
    /// TYml2Xml is able to parse a YML file and store it in an XmlDocument
    /// </summary>
    public class TYml2Xml
    {
        /// <summary>
        /// contains the lines of the yml document
        /// </summary>
        protected string[] lines = null;
        /// <summary>
        /// the current line that we are parsing
        /// </summary>
        protected Int32 currentLine = -1;
        /// <summary>
        /// the filename of the file that we are parsing
        /// </summary>
        protected string filename = "";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="AFilename"></param>
        public TYml2Xml(string AFilename)
        {
            filename = AFilename;

            // file should be in unicode encoding
            // StreamReader DetectEncodingFromByteOrderMarks does not work for ANSI?
            TextReader reader = new StreamReader(filename, TTextFile.GetFileEncoding(filename), false);
            lines = reader.ReadToEnd().Replace("\r", "").Split(new char[] { '\n' });
            reader.Close();
            currentLine = -1;
        }
    }
}
