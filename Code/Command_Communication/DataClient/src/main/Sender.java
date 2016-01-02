package main;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 * Created by apolol92 on 02.01.2016.
 */
public class Sender {
    public static Socket socket;
    public static void sendData(byte[] data) {
        int serverPort = 2210;
        try {
            InetAddress host = InetAddress.getByName("localhost");
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
