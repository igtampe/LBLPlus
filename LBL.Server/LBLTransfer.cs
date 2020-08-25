﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Igtampe.LBL.Server {

    /// <summary>Contains and handles an LBL Transfer</summary>
    public class LBLTransfer {

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

        //For sending:

        /// <summary>Holds queue of all lines to send.</summary>
        private Queue<string> Lines;
        

        /// <summary>Creates an LBL Transfer</summary>
        /// <param name="ID">ID of this transfer</param>
        /// <param name="RootDir">Root directory of this LBL server (C:\LBL\)</param>
        /// <param name="Filename">Filename of this file (Transfers\TheSecretPassword.txt)</param>
        /// <param name="Type">Type of this transfer</param>
        public LBLTransfer(int ID, string RootDir, string Filename, bool Overwrite, LBLTransferType Type) {
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

        /// <summary>Adds specified text to the file.</summary>
        /// <param name="text"></param>
        public void Receive(string text) {
            if(Type == LBLTransferType.Send) { throw new InvalidOperationException("LBL Transfer isn't set to receive mode"); }
            File.AppendAllText(RootDir + Filename,text + "\n");
        }

        public string Send() {
            if(Type == LBLTransferType.Receive) { throw new InvalidOperationException("LBL Transfer isn't set to send mode"); }
            if(Lines.Count == 0) { return "LBL.PLSCLOSE"; }
            return Lines.Dequeue();
        }

        /// <summary>Closes this transfer</summary>
        public void Close() {Lines?.Clear();}

        public override string ToString() { return Filename + " (" + ID + ")"; }
        public override int GetHashCode() { return ID; }
        public override bool Equals(object obj) {
            LBLTransfer OtherTransfer = obj as LBLTransfer;
            return OtherTransfer?.ID == ID;
        }

    }
}
