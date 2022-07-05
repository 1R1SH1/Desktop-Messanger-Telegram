namespace H_W_10._5_WpfApp
{
    struct MessageLog
    {
        public string Message { get; set; }

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string Time { get; set; }

        public MessageLog(string message, string firstName, string time, long id)
        {
            this.Message = message;
            this.FirstName = firstName;
            this.Id = id;
            this.Time = time;
        }
    }
}
