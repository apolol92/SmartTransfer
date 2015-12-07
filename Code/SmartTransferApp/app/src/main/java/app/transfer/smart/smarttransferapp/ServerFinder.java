package app.transfer.smart.smarttransferapp;

import android.util.Log;

import java.io.IOException;
import java.lang.reflect.Array;
import java.net.DatagramPacket;
import java.net.MulticastSocket;
import java.net.UnknownHostException;
import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;

/**
 * Created by apolol92 on 07.12.2015.
 */
public class ServerFinder {
    /**
     * This is the port of the DServer
     */
    public static final int MULTICAST_PORT = 1337;
    /**
     * This will be the MSG from the server
     */
    final String DSERVER_MSG = "SmartTransferServer\n";

    /**
     * This is the ServerFinder
     */
    public ServerFinder() {

    }

    /**
     * Find Servers
     * @return a list of ServerInfos
     */
    public ArrayList<ServerInfo> findServers() {
        ArrayList<ServerInfo> allServer = new ArrayList<ServerInfo>();
        Long tsLong = System.currentTimeMillis()/1000;
        String ts = tsLong.toString();
        try {
            MulticastSocket mSocket = new MulticastSocket(MULTICAST_PORT);

            while (System.currentTimeMillis()/1000 -  tsLong <=5) {
                byte data[] = new byte[500];
                try {
                    DatagramPacket packet = new DatagramPacket(data,data.length);
                    mSocket.receive(packet);
                    Log.d("Hallo", String.valueOf(packet.getAddress()));
                    String msg = Arrays.toString(packet.getData());
                    if(msg.compareTo(DSERVER_MSG)==0) {
                        ServerInfo serverInfo = new ServerInfo(packet.getAddress());
                        //TODO something with ServerInfo
                        allServer.add(serverInfo);
                    }

                } catch (UnknownHostException e) {

                    e.printStackTrace();
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        return allServer;
    }

}
