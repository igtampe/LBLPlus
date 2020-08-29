<p>
<table>
<tr>
<td width="300">
<img src="https://raw.githubusercontent.com/igtampe/LBLPlus/master/LBL.Server/Resources/LBL%20Standalone.png" width="300" style="float:left"/></td><td>
<h1>LBL Plus</h1>
LBL+ is a file transfer system that sends files Line By Line between client and server. This is a port of the SmokeSignal extension to Switchboard.
</td>
</tr>
</table>
</p>

![Screenshot of LBL in action](https://raw.githubusercontent.com/igtampe/LBLPlus/master/Images/Shot.png)

## Simple and convenient.
LBL is easy to integrate into existing Switchboard applications, and although slow on larger files, it's more than usable for small text, CSV, or other files text based files. It was originally designed to back up Income Registry Files from [ViBE's EzTax](http://www.github.com/igtampe/ViBE). Now with Switchboard, performance can be noticeably better, since a connection can be maintained while uploading/downloading files. 

## How It Works
As its name implies, LBL sends files line by line. The steps to send a file in LBL are as follows:

    1. Open a request with the server. The server replies with the ID of your transfer.
    2. Send all lines, identifying them with the transfer ID.
    3. Close the transfer by sending a close command with the ID.
    
Receiving a file is similarly simple. The process is as follows:
     
     1. Open a request with the server to download a file. The server replies with the ID of the transfer and linecount of the file.
     2. Request all lines from the transfer.
     3. Close the transfer by sending a close command with the ID.
 
Lines don't necessarily have to be sent at the same time, opening up LBL to the possibility of being used for remote logging. Also, now with Switchboard, it's easier for multiple connections to send data to the same time. Users cannot overrite files that are already busy, however. If they want to append to a file that's already open, they'll receive the same ID as the one the previously made one.

Files can be downloaded an unlimited number of times. However, files are loaded into memory when downloaded. If data is appended after a download request has started, the added lines will not be sent.

List of all commands and results avialable in the Help section of the LBL Extension (`Help LBL+`)
```
LBLPlus Extension Version 1.0

(All Commands have the prefix LBL and are separated by ~)

DOWNLOAD~(FILE)    | Starts a download transfer to retrieve that file. Returns transfer ID and linecount, separated by a :
UPLOAD~(FILE)      | Starts an upload transfer to append text to that file. Returns transfer ID
OVERWRITE~(FILE)   | Starts an upload transfer to overwrite text to that file. Returns transfer ID
APPEND~(ID)~(LINE) | Appends a line of text to the file on that transfer.
REQUEST~(ID)       | Requests the next line of text from the file on that transfer.
CLOSE~(ID)         | Closes Transfer with that ID
DIR~(SUBDIR)       | Sends a combination of two comma separated lists separated by a tilde (~).
                   | The first is of all directories, and the second is of all files. Give a
                   | as \directory\
PING               | Ping the LBLPlus Extension

Results:

(text)             | Normal result for some commands
LBL.OK             | Normal result for some commands
LBL.EMPTY          | Empty directory/line
LBL.BUSY           | File is busy.
LBL.PLSCLOSE       | Reached end of file from download request. Close the request already!
LBL.NOTFOUND       | File/directory/transfer not found.
LBL.N              | Not enough permission level to execute.
LBL.A              | Argument Exception
LBL.E:(E):(Stack)  | Unhandled exception with stacktrace
```

LBL+ also uses the new way to make Switchboard servers. This being by using the Switchboard Server Class Library, and just the LBL app as a launcher of sorts.

It's still not super efficient, but it's at least slightly better. Plus, it fits under the Switchboard Server Framework, making it a little more usable for other Switchboard apps that want to handle file transfers.
