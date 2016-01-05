package app.transfer.smart.smarttransferapp.serverselection_activity;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

import app.transfer.smart.smarttransferapp.R;
import app.transfer.smart.smarttransferapp.serversearch_activity.WlanServer;


/**
 * Created by apolol92 on 22.12.2015.
 * This view is for the server selection..
 * each view is a found server..
 * you can enter a password and click on the connect button(childAt(2) in innerLayout)
 */
public class ServerSelectionView extends LinearLayout {
    /**
     * InnterLayout of the view..
     */
    LinearLayout innerLayout;
    /**
     * This is the server
     */
    WlanServer wlanServer;

    /**
     * Creates the ServerSelectionView
     * @param context
     * @param wlanServer
     */
    public ServerSelectionView(final Context context, final WlanServer wlanServer) {
        super(context);
        this.wlanServer = wlanServer;
        //Inflate Layout
        LayoutInflater inflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.server_selection_view, this, true);
        this.innerLayout = (LinearLayout)getChildAt(0);
        ((TextView)this.innerLayout.getChildAt(0)).setText(this.wlanServer.getName()+"("+this.wlanServer.getIp().substring(1)+"):");
        ((Button)this.innerLayout.getChildAt(2)).setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                //Add password to wlanServer
                //wlanServer.setPw(((EditText)innerLayout.getChildAt(1)).getText().toString());
                wlanServer.setPw("test123456789123");
                //Set current server
                //SqliteManager.getInstance().setCurrentServer(wlanServer);
                //Connect to
                //ServerCommunicator.sendLogin(wlanServer,context);

            }
        });

    }
}
