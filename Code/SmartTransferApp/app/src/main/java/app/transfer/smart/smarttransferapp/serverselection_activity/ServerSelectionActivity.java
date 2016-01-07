package app.transfer.smart.smarttransferapp.serverselection_activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.widget.LinearLayout;

import java.util.ArrayList;

import app.transfer.smart.smarttransferapp.R;
import app.transfer.smart.smarttransferapp.serversearch_activity.SearchEngine;
import app.transfer.smart.smarttransferapp.serversearch_activity.WlanServer;
import app.transfer.smart.smarttransferapp.support.DataHelper;

public class ServerSelectionActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_server_selection);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbarActivityServerSelection);
        setSupportActionBar(toolbar);
        Intent intent = getIntent();
        if(intent!=null) {
            DataHelper dataHelper =  (DataHelper)intent.getSerializableExtra(SearchEngine.WLAN_SERVERS_KEY);
            create_ui(dataHelper.getWlanServerArrayList());
        }

    }

    private void create_ui(ArrayList<WlanServer> wlanServerArrayList) {
        LinearLayout scrollViewContent = (LinearLayout)findViewById(R.id.server_selection_scroll_view_content);
        if(wlanServerArrayList.size()==0) {
            //TODO: Empty
        }
        else {
            for (int i = 0; i < wlanServerArrayList.size(); i++) {
                scrollViewContent.addView(new ServerSelectionView(getApplicationContext(), wlanServerArrayList.get(i), this));
            }
        }
    }

}
