package app.transfer.smart.smarttransferapp;

/**
 * Created by apolol92 on 07.12.2015.
 */
public class User {
    //create an object of SingleObject
    private static User instance = new User();


    //Selected server
    ServerInfo selectedServer;

    //make the constructor private so that this class cannot be instantiated
    private User(){}

    //Get the only object available
    public static User getInstance() {
        return instance;
    }

    /**
     * Initialize instance
     */
    public void init(ServerInfo selectedServer) {
        this.selectedServer = selectedServer;
    }

    /**
     * Get selected Server
     * @return
     */
    public ServerInfo getSelectedServer() {
        return this.selectedServer;
    }

    /**
     * Set selected Server
     * @param selectedServer
     */
    public void setSelectedServer(ServerInfo selectedServer) {
        this.selectedServer = selectedServer;
    }



}
