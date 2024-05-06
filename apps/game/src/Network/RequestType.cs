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
        ProgressionAnswer,
        Choice,
        Input,
        NotifyPlayer,
        NotifyBoard,
        NotifyServer,
        End
    }
}