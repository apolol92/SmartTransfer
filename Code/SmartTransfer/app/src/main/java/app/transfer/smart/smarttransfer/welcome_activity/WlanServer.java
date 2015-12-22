package app.transfer.smart.smarttransfer.welcome_activity;

/**
 * Created by apolol92 on 22.12.2015.
 */
public class WlanServer {
    private String wlanSsid;
    private String name;
    private String ip;
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
