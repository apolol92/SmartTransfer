package app.transfer.smart.smarttransfer.server_selection_activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.widget.LinearLayout;

import app.transfer.smart.smarttransfer.R;
import app.transfer.smart.smarttransfer.global.User;

public class ServerSelectionActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_server_selection);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        create_ui();


    }

    private void create_ui() {
        for(int i = 0; i < User.getInstance().getWlanServerList().servers.size(); i++) {
            System.out.println(User.getInstance().getWlanServerList().servers.get(i).getName());
            ((LinearLayout)findViewById(R.id.scrollViewLayoutServerSelection)).addView(new ServerSelectionView(getApplicationContext(),User.getInstance().getWlanServerList().servers.get(i)));
        }
    }

}
