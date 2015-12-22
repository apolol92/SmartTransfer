package app.transfer.smart.smarttransfer.global;

import app.transfer.smart.smarttransfer.welcome_activity.BroadcastServerList;
import app.transfer.smart.smarttransfer.welcome_activity.WlanServerList;

/**
 * Created by apolol92 on 21.12.2015.
 */
public class User {
    BroadcastServerList broadcastServerList;
    WlanServerList wlanServerList;
    private User() {
        this.broadcastServerList = new BroadcastServerList();
        this.wlanServerList = new WlanServerList();
    }

    private static User user = null;

    public static void init() {
        if(user == null) {
            user = new User();
        }
    }

    public static User getInstance() {
        return user;
    }

    public BroadcastServerList getBroadcastServerList() {
        return this.broadcastServerList;
    }
    public WlanServerList getWlanServerList() {
        return this.wlanServerList;
    }

    public void setWlanServerList(WlanServerList wlanServerList) {
        this.wlanServerList = wlanServerList;
    }

}
