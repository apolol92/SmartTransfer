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
            if(File.Exists(LOG_PATH))
            {
                System.IO.File.Delete(LOG_PATH);
            }
           
        }

        public void getReady()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis()+":"+"new round:get ready",true);
            this.streamWriter.Close();
        }

        public void incomingCommand()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "new command:a new command received", true);
            this.streamWriter.Close();
        }

        public void correctPassword()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct password:the password was correct", true);
            this.streamWriter.Close();
        }

        public void wrongPassword()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong password:the password was incorrect", true);           
            this.streamWriter.Close();
        }

        public void wrongCmdFormat(string cmd)
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong command format:the command format was wrong", true);
            this.streamWriter.Close();
        }

        public void correctCmdFormat()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct command format:the command format was correct", true);
            this.streamWriter.Close();
        }

        public void userKilled()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "user got killed:the user was too old", true);
            this.streamWriter.Close();
        }

        public void noActiveUser()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "no active user:till now there is no active user", true);
            this.streamWriter.Close();
        }

        public void killerForgiven()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "forgived user:the user survived", true);
            this.streamWriter.Close();
        }

        public void isNoLoginCommand()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "no login command:there was no login command", true);
            this.streamWriter.Close();
        }

        public void getDataFromServer()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "get data:get data from server", true);
            this.streamWriter.Close();
        }

        public void loginSucceed()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "login:user logged in", true);
            this.streamWriter.Close();
        }

        public void saveDataOnServer(Command cmd)
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "saved data:saved "+cmd.Filename+" on server", true);
            this.streamWriter.Close();
        }

        public void wrongId()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong id:user has got the wrong id", true);
            this.streamWriter.Close();
        }

        public void deleteFileFromServer(Command cmd)
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "deleted file:deleted "+cmd.Filename+" from server", true);
            this.streamWriter.Close();
        }

        public void getAvaibleFiles()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "get avaible files:get all avaible files from server as list of filenames", true);
            this.streamWriter.Close();
        }

        public void correctId()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct id:the user has got the id", true);
            this.streamWriter.Close();
        }

        public void userLoggedOut()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "user logged out:user now logged out", true);
            this.streamWriter.Close();
        }

        public void clientWantThumbnail(Command cmd)
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "download thumbnail:user want to have the thumbnail from "+cmd.Filename, true);
            this.streamWriter.Close();
        }

        public void undefinedError()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "error:undefined error", true);
            this.streamWriter.Close();
        }

        public void generatedNewId()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "new id:new id generated", true);
            this.streamWriter.Close();
        }

        public long GetCurrentUnixTimestampMillis()
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public void finishedCommand()
        {
            this.streamWriter = File.AppendText(LOG_PATH);
            this.streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "finished command:all is done", true);
            this.streamWriter.Close();
        }
    }
}
