package app.transfer.smart.smarttransferapp;

/**
 * Created by apolol92 on 07.12.2015.
 */
public class Command {
    public int Id;
    public String Username;
    public int Typ;
    public String Filename;
    public String Parameter;
    public String Data;

    @Override
    public String toString()
    {
        return "{" + Id + ";" + Username + ";" + Typ + ";" + Filename + ";" + Parameter + ";" + Data + "}";
    }
}
