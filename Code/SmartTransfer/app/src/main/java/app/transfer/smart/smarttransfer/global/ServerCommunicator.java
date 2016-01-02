package app.transfer.smart.smarttransfer.global;

import android.content.Context;
import android.content.Intent;
import android.widget.ImageView;

import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;

import app.transfer.smart.smarttransfer.Command;
import app.transfer.smart.smarttransfer.CommandFactory;
import app.transfer.smart.smarttransfer.MainActivity;
import app.transfer.smart.smarttransfer.crypto.Crypter;
import app.transfer.smart.smarttransfer.welcome_activity.WlanServer;

/**
 * Created by apolol92 on 23.12.2015.
 */
public class ServerCommunicator {

    public static void sendMsgAsByteArr(Socket socket, byte[] msg) {
        try {
            socket.getOutputStream().write(msg);
            System.out.println("sent cmd..");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public static byte[] receiveMsg(Socket socket) {
        String msg = "";
        int c;
        ArrayList<Byte> incoming = new ArrayList<Byte>();
        try {
            InputStream in = socket.getInputStream();
            while((c = in.read())!=-1) {
                incoming.add((byte)c);
            }
            byte[] allBytes = new byte[incoming.size()];
            for(int i = 0; i < incoming.size(); i++) {
                allBytes[i] = incoming.get(i);
            }
            msg = new String(allBytes);
            return allBytes;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
    public static void sendLogin(final WlanServer wlanServer, final Context context) {
        new Thread(new Runnable() {
            @Override
            public void run() {
                final String SERVER_IP=wlanServer.getIp().substring(1);
                final int SERVER_PORT = 7000;
                //Create Command
                CommandFactory cmdFactory = new CommandFactory();
                Command cmd = cmdFactory.createCommand(-1, "USER", 9, "none", "SERVER_PW", "none");
                System.out.println("Cmd created..");

                //-----
                try {
                    System.out.println(SERVER_IP);
                    InetAddress serverAddr = InetAddress.getByName(SERVER_IP);
                    System.out.println(SERVER_IP);
                    Socket socket = new Socket(SERVER_IP, SERVER_PORT);
                    System.out.println("Socket created..");
                    //sends the message to the server
                    PrintWriter mBufferOut = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);
                    //System.out.println(wlanServer.getPw() + cmd.toString());
                    byte[] msg2Send = Crypter.Encrypt("test123456789123"+cmd.toString(), "test123456789123");
                    //System.out.println(msg2Send);
                    sendMsgAsByteArr(socket, msg2Send);
                    Command recCmd = cmdFactory.extractCommandFromStr(Crypter.Decrypt(receiveMsg(socket),"test123456789123"));
                    socket.close();
                    System.out.println(recCmd.toString());
                    System.out.println(recCmd.id);
                    System.out.println("RECEIVED ID:"+recCmd.id);
                    User.getInstance().id = recCmd.id;
                    //Toast.makeText(context,recCmd.id,Toast.LENGTH_LONG).show();
                    Intent intent = new Intent(context, MainActivity.class);
                    intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                    context.startActivity(intent);
                    this.finalize();


            } catch (UnknownHostException e) {
                    System.out.println("ERROR 1111111111111111111");
                    e.printStackTrace();
                } catch (IOException e) {
                    System.out.println("ERROR 2222222222222222222");
                    e.printStackTrace();
                } catch (Throwable throwable) {
                    throwable.printStackTrace();
                }
            }}).start();

    }

    public static void sendImg(final byte[] img, final WlanServer wlanServer, final Context context) {
        new Thread(new Runnable() {
            @Override
            public void run() {
                final String SERVER_IP=wlanServer.getIp().substring(1);
                final int SERVER_PORT = 7000;
                //Create Command
                CommandFactory cmdFactory = new CommandFactory();
                //{42;Hans;1;C:\\Users\\Dennis\\Pictures\\test\\abc;none;Inhalt}
                Command cmd = null;
                cmd = cmdFactory.createCommand(User.getInstance().id, "USER", 1, "hallowelt.png", "SERVER_PW", new String(img));
                System.out.print("HERE:::::"+cmd.toString());
                System.out.println("Cmd created..");
                System.out.println("Length: " + cmd.toString().length());
                //-----
                try {
                    System.out.println(SERVER_IP);
                    InetAddress serverAddr = InetAddress.getByName(SERVER_IP);
                    System.out.println(SERVER_IP);
                    Socket socket = new Socket(SERVER_IP, SERVER_PORT);
                    System.out.println("Socket created..");
                    //sends the message to the server
                    PrintWriter mBufferOut = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);
                    //System.out.println(wlanServer.getPw() + cmd.toString());
                    byte[] msg2Send = Crypter.Encrypt("test123456789123"+cmd.toString(), "test123456789123");
                    System.out.print(new String(msg2Send));
                    //System.out.println(msg2Send);
                    sendMsgAsByteArr(socket, msg2Send);
                    Command recCmd = cmdFactory.extractCommandFromStr(Crypter.Decrypt(receiveMsg(socket), "test123456789123"));
                    socket.close();
                    //Command recCmd = cmdFactory.extractCommandFromStr(Crypter.Decrypt(receiveMsg(socket),"test123456789123"));
                    //socket.close();
                    //System.out.println(recCmd.toString());
                    //System.out.println(recCmd.id);
                    //System.out.println("RECEIVED ID:"+recCmd.id);
                    //User.getInstance().id = recCmd.id;



                } catch (UnknownHostException e) {
                    System.out.println("ERROR 1111111111111111111");
                    e.printStackTrace();
                } catch (IOException e) {
                    System.out.println("ERROR 2222222222222222222");
                    e.printStackTrace();
                } catch (Throwable throwable) {
                    throwable.printStackTrace();
                }

            }}).start();
    }

    public void receiveData(final WlanServer wlanServer, final Context context, final ImageView iView) {

    }




}
