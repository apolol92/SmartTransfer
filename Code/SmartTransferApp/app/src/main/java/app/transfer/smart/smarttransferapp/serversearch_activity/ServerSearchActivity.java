package app.transfer.smart.smarttransferapp.serversearch_activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.widget.ProgressBar;

import app.transfer.smart.smarttransferapp.R;

public class ServerSearchActivity extends AppCompatActivity {
    private ProgressBar pBar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_server_search);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbarActivityServerSearch);
        setSupportActionBar(toolbar);
        this.pBar = (ProgressBar)findViewById(R.id.server_search_progressbar_searching);
        SearchEngine searchEngine = new SearchEngine(getApplicationContext(),this,this.pBar);
        searchEngine.run();
    }

}
