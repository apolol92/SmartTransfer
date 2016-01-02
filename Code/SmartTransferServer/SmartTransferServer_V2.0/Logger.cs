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
        public static StreamWriter streamWriter;

        public Logger()
        {
            if(File.Exists(LOG_PATH))
            {
                System.IO.File.Delete(LOG_PATH);
            }
           
        }

        public static void incomingCommand(string command)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "new round:get ready", true);
            streamWriter.Close();
        }

        public static void print(string command)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + command, true);
            streamWriter.Close();
        }

        public static void getReady()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis()+":"+"new round:get ready",true);
            streamWriter.Close();
        }

        public static void incomingCommand()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "new command:a new command received", true);
            streamWriter.Close();
        }

        public static void correctPassword()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct password:the password was correct", true);
            streamWriter.Close();
        }

        public static void wrongPassword(string decryptedRequestCommandStr)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong password:the password was incorrect " + decryptedRequestCommandStr, true);           
            streamWriter.Close();
        }

        public static void wrongCmdFormat(string cmd)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong command format:the command format was wrong", true);
            streamWriter.Close();
        }

        public static void correctCmdFormat()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct command format:the command format was correct", true);
            streamWriter.Close();
        }

        public static void userKilled()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "user got killed:the user was too old", true);
            streamWriter.Close();
        }

        public static void noActiveUser()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "no active user:till now there is no active user", true);
            streamWriter.Close();
        }

        public static void killerForgiven()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "forgived user:the user survived", true);
            streamWriter.Close();
        }

        public static void isNoLoginCommand()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "no login command:there was no login command", true);
            streamWriter.Close();
        }

        public static void getDataFromServer()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "get data:get data from server", true);
            streamWriter.Close();
        }

        public static void loginSucceed(int id)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "login:user logged in " + id, true);
            streamWriter.Close();
        }

        public static void saveDataOnServer(Command cmd)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "saved data:saved "+cmd.Filename+" on server", true);
            streamWriter.Close();
        }

     

        public static void wrongId(int id)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "wrong id:user has got the wrong id "+id, true);
            streamWriter.Close();
        }

        public static void deleteFileFromServer(Command cmd)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "deleted file:deleted "+cmd.Filename+" from server", true);
            streamWriter.Close();
        }

        public static void getAvaibleFiles()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "get avaible files:get all avaible files from server as list of filenames", true);
            streamWriter.Close();
        }

        public static void correctId(int id)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "correct id:the user has got the id " + id, true);
            streamWriter.Close();
        }

        public static void userLoggedOut()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "user logged out:user now logged out", true);
            streamWriter.Close();
        }

        public static void clientWantThumbnail(Command cmd)
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "download thumbnail:user want to have the thumbnail from "+cmd.Filename, true);
            streamWriter.Close();
        }

        public static void undefinedError()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "error:undefined error", true);
            streamWriter.Close();
        }

        public static void generatedNewId()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "new id:new id generated", true);
            streamWriter.Close();
        }

        public static long GetCurrentUnixTimestampMillis()
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public static void finishedCommand()
        {
            streamWriter = File.AppendText(LOG_PATH);
            streamWriter.WriteLine(GetCurrentUnixTimestampMillis() + ":" + "finished command:all is done", true);
            streamWriter.Close();
        }
    }
}
