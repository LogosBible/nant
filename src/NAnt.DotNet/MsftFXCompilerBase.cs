// NAnt - A .NET build tool
// Copyright (C) 2002-2003 Scott Hernandez
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

// Scott Hernandez (ScottHernandez@hotmail.com)

using System;
using System.Collections.Specialized;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

using SourceForge.NAnt.Attributes;

namespace SourceForge.NAnt.Tasks {

    /// <summary>Provides the abstract base class for a Microsoft .Net Framework compiler task.</summary>
    public abstract class MsftFXCompilerBase : CompilerBase {
        public override string ProgramFileName  {
            get {
                return ProgramFilepath(this);
            } 
        }
        public static string ProgramFilepath(ExternalProgramBase epb) {
            
            string enableLookup = epb.Project.Properties["doNotFind.dotnet.exes"];
            if(enableLookup != null && bool.Parse(enableLookup) == true)
                return epb.Name;

            // Instead of relying on the .NET compilers to be in the user's path, point
            // to the compiler directly since it lives in the .NET Framework's runtime directory
            string pfn = null;
            try {
                pfn = Path.Combine(Path.GetDirectoryName(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory()), epb.Name + ".exe");
            }
            catch {
                //no-op ignore the error
            }
            if(pfn != null && File.Exists(pfn))
                return pfn;
            else 
                return epb.Name;
        }
    }
}
