package app.transfer.smart.smarttransfer;

/**
 * Created by apolol92 on 23.12.2015.
 */
public class Command {
    public int id;
    public String username;
    public int typ;
    public String filename;
    public String parameter;
    public String data;

    @Override
    public String toString()
    {
        return "{" + id + ";" + username + ";" + typ + ";" + filename + ";" + parameter + ";" + data + "}";
    }
}
