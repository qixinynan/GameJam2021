using TinyMessenger;

public enum CommonStatus {
    Show = 0,
    Close
}

public class ConmonMessage : ITinyMessage {
    public object Sender { get; private set; }
    public CommonStatus curStatus;

    public ConmonMessage(CommonStatus status) {
        curStatus = status;
    }
}