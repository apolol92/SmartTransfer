package app.transfer.smart.smarttransfer.welcome_activity;

import java.util.ArrayList;

/**
 * Created by apolol92 on 22.12.2015.
 */
public class BroadcastServerList {
    ArrayList<BroadcastServer> serverList;

    public BroadcastServerList() {
        this.serverList = new ArrayList<BroadcastServer>();
    }
    public void addServer(BroadcastServer server) {
        if(this.serverList.size()==0) {
            this.serverList.add(server);
        }
        else {
            for (int i = 0; i < this.serverList.size(); i++) {
                if (this.serverList.get(i).getIp().compareTo(server.getIp())!=0) {
                    this.serverList.add(server);
                }
            }
        }
    }

    public ArrayList<BroadcastServer> getServerList() {
        return this.serverList;
    }
}
