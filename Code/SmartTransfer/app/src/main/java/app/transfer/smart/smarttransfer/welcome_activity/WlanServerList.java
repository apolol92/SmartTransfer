package app.transfer.smart.smarttransfer.welcome_activity;

import java.util.ArrayList;

/**
 * Created by apolol92 on 22.12.2015.
 */
public class WlanServerList {
    public ArrayList<WlanServer> servers;
    public WlanServerList() {
        this.servers = new ArrayList<WlanServer>();
    }
    public void addWlanServer(WlanServer wlanServer)
    {
        if(contains(wlanServer)==false) {
            this.servers.add(wlanServer);
        }

    }

    public boolean contains(WlanServer wlanServer) {
        if(wlanServer==null) {
            return false;
        }
        for(int i = 0; i < servers.size(); i++) {
            if(wlanServer.getWlanSsid().compareTo(servers.get(i).getWlanSsid())==0 && wlanServer.getIp().compareTo(servers.get(i).getIp())==0) {
                return true;
            }
        }
        return false;
    }
}
