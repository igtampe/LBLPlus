﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Igtampe.LBL.Server {

    /// <summary>Contains and handles an LBL Transfer</summary>
    public class LBLServerTransfer {

        //------------------------------[Variables]------------------------------

        /// <summary>ID of this transfer</summary>
        public int ID { get; protected set; }

        /// <summary>Filename of this transfer</summary>
        public readonly string Filename;

        /// <summary>Root directory that is set on creation</summary>
        private readonly string RootDir;

        /// <summary>Type of this transfer</summary>
        public readonly LBLTransferType Type;

        /// <summary>LBL Transfer Types</summary>
        public enum LBLTransferType {Send, Receive}

        public bool Busy { get; protected set; } = false;

        //For sending:

        /// <summary>Holds queue of all lines to send.</summary>
        private readonly Queue<string> Lines;

        /// <summary>Amount of lines remaining on this transfer.</summary>
        public int LineCount => Lines.Count;

        //------------------------------[Constructor]------------------------------

        /// <summary>Creates an LBL Transfer</summary>
        /// <param name="ID">ID of this transfer</param>
        /// <param name="RootDir">Root directory of this LBL server (C:\LBL\)</param>
        /// <param name="Filename">Filename of this file (Transfers\TheSecretPassword.txt)</param>
        /// <param name="Type">Type of this transfer</param>
        public LBLServerTransfer(int ID, string RootDir, string Filename, bool Overwrite, LBLTransferType Type) {
            this.ID = ID;
            this.RootDir = RootDir;
            this.Filename = Filename;
            this.Type = Type;

            switch(Type) {
                case LBLTransferType.Send:
                    //If we must send back a file, load the file into memory and put them in a queue.
                    Lines = new Queue<string>(File.ReadAllLines(RootDir + Filename));
                    break;
                case LBLTransferType.Receive:
                    //If we're receiving a file and we have to overwrite, delete the existing file.
                    //The LBL Extension should check that if we need to overwrite, the file should not be being written to.
                    if(Overwrite && File.Exists(RootDir + Filename)) { File.Delete(RootDir + Filename); }
                    break;
                default:
                    throw new ArgumentException("MUST SPECIFY TYPE");
            }
        }

        //------------------------------[Functions]------------------------------

        /// <summary>Adds specified text to the file this transfer is tied to.</summary>
        /// <param name="text"></param>
        public void Receive(string text) {
            if(Type == LBLTransferType.Send) { throw new InvalidOperationException("LBL Transfer isn't set to receive mode"); }
            
            //wait for not busy
            while(Busy) { }
            
            //do the operation
            Busy = true;
            File.AppendAllText(RootDir + Filename,text + "\n");
            Busy = false;
        }

        /// <summary>Gets the next line for a client</summary>
        /// <returns>The next line on the file, or LBL.PLSCLOSE if there are no more lines</returns>
        public string Send() {
            if(Type == LBLTransferType.Receive) { throw new InvalidOperationException("LBL Transfer isn't set to send mode"); }
            
            //wait for not busy
            while(Busy) { }

            //Do the operation
            Busy = true;
            
            if(Lines.Count == 0) {
                Busy = false;
                return "LBL.PLSCLOSE"; 
            }

            String returnString = Lines.Dequeue();
            Busy = false;
            return returnString;
        }

        /// <summary>Closes this transfer</summary>
        public void Close() {
            //wait for not busy
            while(Busy) { }

            //close
            Lines?.Clear();
        }

        //------------------------------[Object Functions]------------------------------

        /// <summary>Turns this transfer to a string</summary>
        /// <returns>Filename (ID)</returns>
        public override string ToString() { return Filename + " (" + ID + ")"; }

        /// <summary>Gets a HashCode for this connection</summary>
        /// <returns>The ID</returns>
        public override int GetHashCode() { return ID; }

        /// <summary>Finds out if this connection and another are the same.</summary>
        /// <param name="obj">obj to compare</param>
        /// <returns>True if the ID of this transfer equals the other's, or if the object is a string and the filename is the same, or if the object is a string or integer if its the same to the ID</returns>
        public override bool Equals(object obj) {
            LBLServerTransfer OtherTransfer = obj as LBLServerTransfer;
            string OtherFilename = obj.ToString();

            return OtherTransfer?.ID == ID || OtherFilename.ToUpper() == Filename.ToUpper() || OtherFilename == ID.ToString();
        }

    }
}
