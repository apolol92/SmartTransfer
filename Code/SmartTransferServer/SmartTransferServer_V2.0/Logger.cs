using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer_V2._0
{
    public class Logger
    {
        public static readonly string LOG_PATH = "eventlog.txt";
        StreamWriter streamWriter;

        public Logger()
        {
            this.streamWriter = new StreamWriter(@LOG_PATH);
        }
        public void getReady()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis()+":"+"new round:get ready",true);
        }

        public void incomingCommand()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "new command:a new command received", true);
        }

        public void correctPassword()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct password:the password was correct", true);
        }

        public void wrongPassword()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong password:the password was incorrect", true);
        }

        public void wrongCmdFormat(string cmd)
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong command format:the command format was wrong", true);
        }

        public void correctCmdFormat()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct command format:the command format was correct", true);
        }

        public void userKilled()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "user got killed:the user was too old", true);
        }

        public void noActiveUser()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "no active user:till now there is no active user", true);
        }

        public void killerForgiven()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "forgived user:the user survived", true);
        }

        public void isNoLoginCommand()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "no login command:there was no login command", true);
        }

        public void getDataFromServer()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "get data:get data from server", true);
        }

        public void loginSucceed()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "login:user logged in", true);
        }

        internal void saveDataOnServer(Command cmd)
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "saved data:saved "+cmd.Filename+" on server", true);
        }

        public void wrongId()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong id:user has got the wrong id", true);
        }

        public void deleteFileFromServer(Command cmd)
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "deleted file:deleted "+cmd.Filename+" from server", true);
        }

        public void getAvaibleFiles()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "get avaible files:get all avaible files from server as list of filenames", true);
        }

        public void correctId()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct id:the user has got the id", true);
        }

        public void userLoggedOut()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "user logged out:user now logged out", true);
        }

        public void clientWantThumbnail(Command cmd)
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "download thumbnail:user want to have the thumbnail from "+cmd.Filename, true);
        }

        public void undefinedError()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "error:undefined error", true);
        }

        public void generatedNewId()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "new id:new id generated", true);
        }

        public long GetCurrentUnixTimestampMillis()
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public void finishedCommand()
        {
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "finished command:all is done", true);
        }
    }
}
