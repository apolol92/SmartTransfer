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


    public static void main(String args[]) throws IOException {
        String key = "test123456789123";
        System.out.println("Create cmd");
        Command cmd = CommandFactory.createCommand(12, "USER", 4, "test", "sdf", FileManager.readeFile());
        System.out.println("Send cmd");
        Sender.sendData(Crypter.Encrypt(cmd.toByteArr(), key));
        System.out.println("Receive cmd");
        Command received = CommandFactory.extractCommand(Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),key));
        System.out.println("Save file");
        FileManager.saveFile(received.Data,"incoming");

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
