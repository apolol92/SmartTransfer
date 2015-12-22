package app.transfer.smart.smarttransfer.welcome_activity;

import android.os.AsyncTask;
import android.util.Log;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.DatagramPacket;
import java.net.MulticastSocket;
import java.net.UnknownHostException;
import java.util.Arrays;

import app.transfer.smart.smarttransfer.global.User;

/**
 * Created by apolol92 on 22.12.2015.
 */
public class BroadcastServerFinder extends AsyncTask<Void, Void, Void> {
    private final int INTERVAL = 5;
    private final int MULTICAST_PORT; // chat port
    private final Integer BUFFER_SIZE = 4096; // size of the reading buffer

    public BroadcastServerFinder() {
        MULTICAST_PORT = 1337;
    }

    private int getTimeSecs() {
        return (int)(System.currentTimeMillis()/1000);
    }
    @Override
    protected Void doInBackground(Void... params) {
        int startTime = getTimeSecs();
        int currentTime = getTimeSecs();
        int counter;
        try {
            MulticastSocket mSocket = new MulticastSocket(MULTICAST_PORT);
            do {
                try {
                    byte data[] = new byte[500];
                    DatagramPacket packet = new DatagramPacket(data,data.length);
                    Log.d("Waiting", "Wait for incoming packet");
                    mSocket.receive(packet);
                    System.out.println(new String(trim(data), "UTF-8"));
                    User.getInstance().getBroadcastServerList().addServer(createServerFromRaw(packet));
                } catch (UnknownHostException e) {
                    e.printStackTrace();
                }
                currentTime = getTimeSecs();
                counter = currentTime-startTime;
            }while(counter<=INTERVAL);
        } catch (IOException e) {
            e.printStackTrace();
        }

        return null;
    }

    private BroadcastServer createServerFromRaw(DatagramPacket packet) {
        BroadcastServer bServer = new BroadcastServer();
        bServer.setIp(String.valueOf(packet.getAddress()));
        try {
            bServer.setName(new String(trim(packet.getData()), "UTF-8"));
            return bServer;
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
        return null;
    }

    static byte[] trim(byte[] bytes)
    {
        int i = bytes.length - 1;
        while (i >= 0 && bytes[i] == 0)
        {
            --i;
        }

        return Arrays.copyOf(bytes, i + 1);
    }


}
