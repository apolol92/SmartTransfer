package app.transfer.smart.smarttransferapp.serversearch_activity;

import android.content.Context;
import android.content.Intent;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.widget.ProgressBar;

import java.util.ArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ThreadPoolExecutor;

import app.transfer.smart.smarttransferapp.serverselection_activity.ServerSelectionActivity;
import app.transfer.smart.smarttransferapp.support.DataHelper;

/**
 * Created by apolol92 on 05.01.2016.
 * This class searchs for available servers in the network
 */
public class SearchEngine {
    /**
     * Reference to the current context
     */
    Context context;
    /**
     * Reference to the current activity
     */
    ServerSearchActivity serverSearchActivity;
    /**
     * Asynchronous Task for searching WLAN-Servers
     */
    WlanServerFinder wlanServerFinder;
    /**
     * Asynchronous Task for updating the progressbar from searching WLAN-Servers
     */
    ProgressbarAsyncTask pbTask;
    /**
     * Contains all founded WLAN-SERVERS
     */
    public ArrayList<WlanServer> wlanServers;
    /**
     * WLAN-SERVERS KEY
     */
    public static final String WLAN_SERVERS_KEY = "wlan_servers";

    /**
     * Creates the SearchEngine
     * @param context, Reference to the current context
     * @param serverSearchActivity, Reference to the current activity
     * @param progressBar, Asynchronous Task for updating the progressbar from searching WLAN-Servers
     */
    public SearchEngine(Context context, ServerSearchActivity serverSearchActivity, ProgressBar progressBar) {
        this.context = context;
        this.serverSearchActivity = serverSearchActivity;
        this.wlanServers = new ArrayList<WlanServer>();
        this.wlanServerFinder = new WlanServerFinder(this);
        this.pbTask = new ProgressbarAsyncTask(this,progressBar,this.wlanServerFinder);
    }


    /**
     * Run the SearchEngine
     */
    public void run() {
        ThreadPoolExecutor mPool;
        mPool = (ThreadPoolExecutor) Executors.newFixedThreadPool(5);
        this.wlanServerFinder.executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
        try {
            this.pbTask.executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
            new Thread(new Runnable() {
                @Override
                public void run() {
                    while(pbTask.isDone()==false) {

                    }
                    WifiManager wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
                    WifiInfo info = wifiManager.getConnectionInfo ();
                    for(int i = 0; i < wlanServers.size(); i++) {
                        WlanServer wlanServer = new WlanServer();
                        wlanServer.setIp(wlanServers.get(i).getIp());
                        wlanServer.setName(wlanServers.get(i).getName());
                        wlanServer.setWlanSsid(info.getSSID());
                        wlanServers.add(wlanServer);
                        startNextActivity();
                        serverSearchActivity.finish();
                        try {
                            while(true) {
                                this.finalize();
                            }
                        } catch (Throwable throwable) {
                            throwable.printStackTrace();
                        }
                    }
                }
            }).start();


        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * This method adds new servers to wlanServers(list)
     * @param wlanServer
     */
    public void addServerToWlanServers(WlanServer wlanServer) {
        if(this.wlanServers.size()==0) {
            this.wlanServers.add(wlanServer);
        }
        else {
            for (int i = 0; i < this.wlanServers.size(); i++) {
                if (this.wlanServers.get(i).getIp().compareTo(wlanServer.getIp())!=0) {
                    this.wlanServers.add(wlanServer);
                }
            }
        }
    }

    /**
     * Start ServerSelectionActivity
     */
    public void startNextActivity() {
        Intent nIntent = new Intent(this.context,ServerSelectionActivity.class);
        Bundle b = new Bundle();
        b.putSerializable(WLAN_SERVERS_KEY,new DataHelper(this.wlanServers));
        nIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        nIntent.putExtras(b);
        this.context.startActivity(nIntent);
        this.serverSearchActivity.finish();
    }
}
