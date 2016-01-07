package app.transfer.smart.smarttransferapp.server_communication;

import java.io.IOException;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;

import app.transfer.smart.smarttransferapp.serversearch_activity.WlanServer;

/**
 * Created by apolol92 on 02.01.2016.
 */
public class Sender {
    public static Socket socket;

    /**
     * TODO: DO THIS IN A EXTRA TASK
     * @param data
     * @param wlanServer
     */
    public static void sendData(byte[] data, WlanServer wlanServer) {
        int serverPort = 7000;
        try {
            System.out.println(wlanServer.getIp());
            InetAddress host = InetAddress.getByName(wlanServer.getIp());
            socket = new Socket(host,serverPort);
            OutputStream out = socket.getOutputStream();
            out.write(data, 0, data.length);
        } catch (UnknownHostException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


}
