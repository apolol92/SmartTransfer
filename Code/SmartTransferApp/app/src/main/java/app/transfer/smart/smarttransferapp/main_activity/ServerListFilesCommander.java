package app.transfer.smart.smarttransferapp.main_activity;

import android.os.AsyncTask;

import app.transfer.smart.smarttransferapp.main_activity.dfile.DFileListServer;
import app.transfer.smart.smarttransferapp.server_communication.Command;
import app.transfer.smart.smarttransferapp.server_communication.CommandFactory;
import app.transfer.smart.smarttransferapp.server_communication.Crypter;
import app.transfer.smart.smarttransferapp.server_communication.Receiver;
import app.transfer.smart.smarttransferapp.server_communication.Sender;
import app.transfer.smart.smarttransferapp.serversearch_activity.WlanServer;

/**
 * Created by apolol92 on 07.01.2016.
 */
public class ServerListFilesCommander extends AsyncTask<Void,Void,Void> {

    private Integer lastId;
    private WlanServer wlanServer;
    private Command serverResponse;
    private DFileListServer dFileListServer;

    public ServerListFilesCommander(WlanServer wlanServer, Integer lastId, DFileListServer dFileListServer) {
        this.wlanServer = wlanServer;
        this.lastId = lastId;
        this.dFileListServer = dFileListServer;
    }

    @Override
    protected Void doInBackground(Void... params) {
        System.out.println(this.lastId);
        Command loginCmd = CommandFactory.createCommand(this.lastId, "USER", 4, "none", "none", new byte[1]);
        Sender.sendData(Crypter.Encrypt(loginCmd.toByteArr(), this.wlanServer.getPw()), this.wlanServer);
        System.out.println("Sent");
        byte[] serverResponseBytes = Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),wlanServer.getPw());
        this.serverResponse = CommandFactory.extractCommand(serverResponseBytes);
        this.lastId = this.serverResponse.Id;
        return null;
    }
    /**
     * After receiving command.. do following
     * @param aVoid
     */
    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        System.out.println(this.serverResponse.Parameter);
        this.dFileListServer.dFiles.addAll(this.dFileListServer.extractFileListStr(this.serverResponse.Parameter));
        System.out.println(this.dFileListServer.dFiles.size());
        this.dFileListServer.addDFiles2ScrollViewContent();
        //this.dFileListServer.downloadNdFiles();
        System.out.println("Finished listing..");
        ServerThumCommander serverThumCommander = new ServerThumCommander(wlanServer,this.lastId,this.dFileListServer.dFiles.get(0));
        serverThumCommander.execute();
    }
}