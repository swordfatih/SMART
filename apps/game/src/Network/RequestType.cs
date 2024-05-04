namespace Network
{
    public enum RequestType
    {
        Error,
        Connect,
        Disconnect,
        Start,
        ServerMessage,
        PlayerMessage,
        BoardMessage,
        ChoiceAnswer,
        Choice,
        Input,
        NotifyPlayer,
        NotifyBoard,
        NotifyServer,
        End
    }
}