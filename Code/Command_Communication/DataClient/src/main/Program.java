package main;

import jdk.internal.util.xml.impl.Input;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.util.ArrayList;
import java.util.Base64;

/**
 * Created by apolol92 on 02.01.2016.
 */
public class Program {


    public static void main(String args[]) throws IOException, InterruptedException {
        String key = "test123456789123";
        //Login to Server
        Command loginCmd = CommandFactory.createCommand(-1, "USER", 9, "none", "none", new byte[1]);
        byte[] loginCmdBytes = Crypter.Encrypt(loginCmd.toByteArr(),key);
        Sender.sendData(loginCmdBytes);
        Command rLoginCmd = CommandFactory.extractCommand(Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),key));
        Sender.socket.close();
        //List Data from Server
        Command listCmd = CommandFactory.createCommand(rLoginCmd.Id,"USER",4,"none","none",new byte[1]);
        byte[] listCmdBytes = Crypter.Encrypt(listCmd.toByteArr(), key);
        Sender.sendData(listCmdBytes);
        Command rListCmd = CommandFactory.extractCommand(Crypter.Decrypt(Receiver.receiveMsg(Sender.socket), key));
        Sender.socket.close();
        System.out.println(rListCmd.Typ);
        System.out.println("FILES:" + rListCmd.Parameter);
        //Download Data from Server
        //C:\Users\Dennis\Pictures\FebSep2015\links.png
        Command downloadCmd = CommandFactory.createCommand(rListCmd.Id, "USER", 0, "man.png","none",new byte[1]);
        byte[] downloadCmdBytes = Crypter.Encrypt(downloadCmd.toByteArr(),key);
        Sender.sendData(downloadCmdBytes);
        Command rDownloadCmd = CommandFactory.extractCommand(Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),key));
        System.out.println(rDownloadCmd.Data.length);
        FileManager.saveFile(rDownloadCmd.Data, "downloaded");
        Sender.socket.close();
        //Upload Data to Server
        Command uploadCmd = CommandFactory.createCommand(rDownloadCmd.Id, "USER", 1, "incoming.png", "none", FileManager.readeFile());
        byte[] uploadCmdBytes = Crypter.Encrypt(uploadCmd.toByteArr(), key);
        Sender.sendData(uploadCmdBytes);
        Command rUploadCmd = CommandFactory.extractCommand(Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),key));
        Sender.socket.close();
        System.out.println(rUploadCmd.Id);
        //Delete Data from Server
        Command deleteCmd = CommandFactory.createCommand(rUploadCmd.Id, "USER", 2, "incoming.png", "none", new byte[1]);
        byte[] deleteCmdBytes = Crypter.Encrypt(deleteCmd.toByteArr(), key);
        Sender.sendData(deleteCmdBytes);
        System.out.println("Wait for delete responds");
        Command rDeleteCmd = CommandFactory.extractCommand(Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),key));
        System.out.println(rDeleteCmd.Parameter);
        Sender.socket.close();
        //Download Thumbnail
        Command thumbCmd = CommandFactory.createCommand(rDeleteCmd.Id, "USER", 10, "man.png", "none", new byte[1]);
        byte[] thumbCmdBytes = Crypter.Encrypt(thumbCmd.toByteArr(), key);
        Sender.sendData(thumbCmdBytes);
        Command rThumbCmd = CommandFactory.extractCommand(Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),key));
        System.out.println(rThumbCmd.Parameter+ " " +rThumbCmd.Data.length);
        FileManager.saveFile(rThumbCmd.Data, "thumb");
        Sender.socket.close();
        //Keep Alive
        Command aliveCmd = CommandFactory.createCommand(rThumbCmd.Id, "USER", 3, "man.png", "none", new byte[1]);
        byte[] aliveCmdBytes = Crypter.Encrypt(aliveCmd.toByteArr(), key);
        Sender.sendData(aliveCmdBytes);
        Sender.socket.close();
        //Logout
        Command logoutCmd = CommandFactory.createCommand(rThumbCmd.Id, "USER", 8, "man.png", "none", new byte[1]);
        byte[] logoutCmdBytes = Crypter.Encrypt(logoutCmd.toByteArr(), key);
        Sender.sendData(logoutCmdBytes);
        Sender.socket.close();
    }



    /*public static String getBase64StringFromBytes(byte[] bytes)
    {
        String b64 = null;
        try {
            //To Base64-String
            b64 = new String(Base64.getEncoder().encode(bytes),"UTF-8");
            //UTF-8
            return b64;
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
        return null;

    }

    static byte[] get64BytesFromBase64String(String str)
    {
        //To
        byte[] data = Base64.getDecoder().decode(str);
        //Return bytes
        return data;

    }

    static byte[] getBytesFromBase64Bytes(byte[] b64) {
        return Base64.getDecoder().decode(b64);
    }*/


  /*  public static void main(String args[]) throws UnsupportedEncodingException {
        byte[] test = "}}".getBytes();
        System.out.println((char)test[1]);
        System.out.println(new String("Hällo".getBytes(),"UTF-8"));
        //1.Read image as byte array
        byte[] data = FileManager.readeFile();
        System.out.println("Data-Length:"+data.length);
        //2.1 Convert byte array to base64 string
        String str64 = getBase64StringFromBytes(data);
        System.out.println(("Str64:"+str64.length()));
        //2.2 Convert base64 string to base64 byte array
        byte[] b64Bytes = str64.getBytes();
        System.out.println("B64:" + b64Bytes.length);
        //3.Send base64 byte array to csharp
        Sender.sendData(data);
        try {
            InputStream receiverStream = Sender.socket.getInputStream();
            ArrayList<Byte> incoming = new ArrayList<Byte>();
            int c;
            while((c = receiverStream.read())!=-1) {
                incoming.add((byte)c);
            }
            byte[] allBytes = new byte[incoming.size()];
            for(int i = 0; i < incoming.size(); i++) {
                allBytes[i] = incoming.get(i);
            }
            FileManager.saveFile(allBytes);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }*/
}
