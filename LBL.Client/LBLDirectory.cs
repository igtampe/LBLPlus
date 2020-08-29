using System;

namespace Igtampe.LBL.Client {

    /// <summary>
    /// Holds an LBL Directory<br></br>
    /// <br></br>
    /// essentially comprised of two arrays holding the names of folders and the names of files in the directory.</summary>
    public class LBLDirectory {

        /// <summary>Array of all folders in this directory</summary>
        public readonly string[] Folders = null;

        /// <summary>Array of all files in this directory</summary>
        public readonly string[] Files = null;

        /// <summary>Creates an LBLDirectory using a string from the server</summary>
        /// <param name="DirectoryString"></param>
        public LBLDirectory(string DirectoryString) {
            string[] FoldersAndFiles = DirectoryString.Split('~');
            if(!String.IsNullOrEmpty(FoldersAndFiles[0])) { Folders = FoldersAndFiles[0].Split(','); }
            if(!String.IsNullOrEmpty(FoldersAndFiles[1])) { Files = FoldersAndFiles[1].Split(','); }

        }

        
    }
}
