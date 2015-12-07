package app.transfer.smart.smarttransferapp;

import java.net.InetAddress;

/**
 * Created by apolol92 on 07.12.2015.
 */
public class ServerInfo {
    /**
     * This is the serverAddress
     */
    InetAddress serverAddress;

    /**
     * Constructor given a ServerAddress
     * @param ia
     */
    public ServerInfo(InetAddress ia) {
        this.serverAddress = ia;
    }

    /**
     * Get Server Port
     * @return
     */
    public int getServerPort() {
        return ServerFinder.MULTICAST_PORT;
    }

    /**
     * Get Server Address
     * @return
     */
    public InetAddress getServerAddress() {
        return serverAddress;
    }

    /**
     * Get IP as string
     * @return
     */
    public String getIpAsString() {
        return String.valueOf(serverAddress);
    }
}
