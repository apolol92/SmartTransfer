package app.transfer.smart.smarttransferapp.serverselection_activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;

import app.transfer.smart.smarttransferapp.R;

public class ServerSelectionActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_server_selection);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbarActivityServerSelection);
        setSupportActionBar(toolbar);


    }

}
