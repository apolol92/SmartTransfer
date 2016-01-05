package app.transfer.smart.smarttransferapp.serversearch_activity;

import android.os.AsyncTask;
import android.util.Log;
import android.widget.ProgressBar;

/**
 * Created by apolol92 on 05.01.2016.
 * This class updates the progressbar in ServerSearchActivity..
 * After 5 seconds left, it's done..
 */
public class ProgressbarAsyncTask extends AsyncTask<Void, Integer, Void> {
    /**
     * Reference to parent activity
     */
    SearchEngine parent;
    /**
     * How long should it run?
     */
    private final int INTERVAL = 5;
    /**
     * Reference to progressbar from ServerSearchActivity
     */
    private ProgressBar pBar;
    /**
     * Reference to wlanServerFinder
     */
    private WlanServerFinder wlanServerFinder;

    /**
     * Create ProgressbarAsyncTask
     * @param parent
     * @param pBar
     * @param wlanServerFinder
     */
    public ProgressbarAsyncTask(SearchEngine parent, ProgressBar pBar, WlanServerFinder wlanServerFinder) {
        this.parent = parent;
        this.pBar = pBar;
        this.wlanServerFinder = wlanServerFinder;
    }

    /**
     * Get Timestamp in seconds
     * @return
     */
    private int getTimeSecs() {
        return (int) (System.currentTimeMillis() / 1000);
    }

    /**
     * Is done?
     * @return
     */
    public boolean isDone() {
        return done;
    }

    private boolean done;

    /**
     * Background work..
     * @param params
     * @return
     */
    @Override
    protected Void doInBackground(Void... params) {
        int startTime = getTimeSecs();
        int currentTime = getTimeSecs();
        int counter;
        do {
            currentTime = getTimeSecs();
            counter = currentTime - startTime;
            publishProgress((int) ((float) counter * 100f / (float) INTERVAL));
        } while (counter <= INTERVAL);
        wlanServerFinder.cancel(true);
        for(int i = 0; i < parent.wlanServers.size();i++) {
            Log.d("WLAN-SERVER", parent.wlanServers.get(i).getIp());
        }
        done = true;
        return null;
    }

    /**
     * Update progressbar
     * @param values
     */
    @Override
    protected void onProgressUpdate(Integer... values) {
        this.pBar.setProgress(values[0]);
        super.onProgressUpdate(values);
    }
}

