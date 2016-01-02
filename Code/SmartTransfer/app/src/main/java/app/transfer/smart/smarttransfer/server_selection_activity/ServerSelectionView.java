package app.transfer.smart.smarttransfer.server_selection_activity;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

import app.transfer.smart.smarttransfer.R;
import app.transfer.smart.smarttransfer.global.ServerCommunicator;
import app.transfer.smart.smarttransfer.global.SqliteManager;
import app.transfer.smart.smarttransfer.welcome_activity.WlanServer;

/**
 * Created by apolol92 on 22.12.2015.
 */
public class ServerSelectionView extends LinearLayout {
    LinearLayout innerLayout;
    WlanServer wlanServer;

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
                SqliteManager.getInstance().setCurrentServer(wlanServer);
                //Connect to
                ServerCommunicator.sendLogin(wlanServer,context);

            }
        });

    }
}
