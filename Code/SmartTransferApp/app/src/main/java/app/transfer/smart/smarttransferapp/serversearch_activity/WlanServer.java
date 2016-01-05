package app.transfer.smart.smarttransferapp.serversearch_activity;

import java.io.Serializable;

/**
 * Created by apolol92 on 05.01.2016.
 * Contains all informations about the WLAN-Server
 */
public class WlanServer implements Serializable {
    /**
     * The WLAN-SSID in which the WLAN-Server is
     */
    private String wlanSsid;
    /**
     * WLAN-Server name
     */
    private String name;
    /**
     * WLAN-Server IP
     */
    private String ip;
    /**
     * WLAN-Server PW
     */
    private String pw;

    public String getWlanSsid() {
        return wlanSsid;
    }

    public String getName() {
        return name;
    }

    public String getIp() {
        return ip;
    }

    public String getPw() {
        return pw;
    }

    public void setWlanSsid(String wlanSsid) {
        this.wlanSsid = wlanSsid;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setIp(String ip) {
        this.ip = ip;
    }

    public void setPw(String pw) {
        this.pw = pw;
    }
}

