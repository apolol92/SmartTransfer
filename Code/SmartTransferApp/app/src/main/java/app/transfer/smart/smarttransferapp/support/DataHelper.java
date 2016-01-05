package app.transfer.smart.smarttransferapp.support;

import java.io.Serializable;
import java.util.ArrayList;

import app.transfer.smart.smarttransferapp.serversearch_activity.WlanServer;

/**
 * Created by apolol92 on 05.01.2016.
 * This class is used for transfer data between Activities
 */
public class DataHelper implements Serializable {
    private ArrayList<WlanServer> wlanServerArrayList;

    public DataHelper(ArrayList<WlanServer> wlanServerArrayList) {
        this.wlanServerArrayList = wlanServerArrayList;
    }

    public ArrayList<WlanServer> getWlanServerArrayList() {
        return  this.wlanServerArrayList;
    }
}
