namespace MIFin.Api.Globals {
    public class HandlerResult {
        public HandlerResult() {
        }
        public HandlerResult(Exception ex) {
            errorCode = 1;
            Message = ex.ToString();
        }
        public HandlerResult(int errorCode, string message) {
            this.errorCode = errorCode;
            this.message = message;
        }
        int errorCode = 0;
        public int ErrorCode {
            get { return errorCode; }
            set { errorCode = value; }
        }
        string message = string.Empty;
        public string Message {
            get { return message; }
            set { message = value; }
        }
        //string data = string.Empty;
        //public string Data {
        //    get { return data; }
        //    set { data = value; }
        //}
        object data = string.Empty;
        public object Data {
            get { return data; }
            set { data = value; }
        }

        // public List<HandlerResultDetails> ErrorDetails = new List<HandlerResultDetails>() { };
    }
}
