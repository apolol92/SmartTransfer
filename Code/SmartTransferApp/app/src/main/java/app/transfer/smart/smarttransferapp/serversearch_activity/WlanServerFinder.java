package app.transfer.smart.smarttransferapp.serversearch_activity;

import android.os.AsyncTask;
import android.util.Log;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.DatagramPacket;
import java.net.MulticastSocket;
import java.net.UnknownHostException;
import java.util.Arrays;

/**
 * Created by apolol92 on 05.01.2016.
 */
public class WlanServerFinder extends AsyncTask<Void, Void, Void> {
    /**
     * Reference to the parent Activity
     */
    private SearchEngine parent;
    /**
     * How long should it search
     */
    private final int INTERVAL = 5;
    /**
     * On which port should it search?
     */
    private final int MULTICAST_PORT = 1337; // chat port
    /**
     * Size of the reading buffer
     */
    private final Integer BUFFER_SIZE = 4096;

    /**
     * Get Timestamp in seconds
     * @return
     */
    private int getTimeSecs() {
        return (int)(System.currentTimeMillis()/1000);
    }

    /**
     * Create a WlanServerFinder
     * @param parent, Reference to the parent Activity
     */
    public WlanServerFinder(SearchEngine parent) {
        this.parent = parent;
    }

    /**
     * Background opteration.. searchs for WLAN-Servers
     * @param params
     * @return
     */
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
                    this.parent.addServerToWlanServers(createServerFromRaw(packet));
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

    /**
     * This method creates a WLAN-Server from raw UDP-Datagram
     * @param packet UDP-Datagram
     * @return WLAN-Server
     */
    private WlanServer createServerFromRaw(DatagramPacket packet) {
        WlanServer wServer = new WlanServer();
        wServer.setIp(String.valueOf(packet.getAddress()));
        try {
            wServer.setName(new String(trim(packet.getData()), "UTF-8"));
            wServer.setName(wServer.getName());
            wServer.setIp(wServer.getIp().substring(1));
            return wServer;
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
        return null;
    }

    /**
     * Trim byte array
     * @param bytes
     * @return
     */
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
