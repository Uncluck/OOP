namespace Banks.Banks;

public class NotificationEventArgs : EventArgs
{
    public NotificationEventArgs(string msg)
    {
        Message = msg;
    }

    public string Message { get; set; }
}