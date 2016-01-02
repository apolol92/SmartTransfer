package main;

import java.io.IOException;
import java.io.InputStream;
import java.net.Socket;
import java.util.ArrayList;

/**
 * Created by apolol92 on 02.01.2016.
 */
public class Receiver {
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
}
