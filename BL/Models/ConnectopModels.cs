namespace MIFin.Api.BL.Models.Connectop {
    public class GetPageMeResponse {
        public int page_id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public int created { get; set; }
        public int total_users { get; set; }
    }

    public class GetCatFactResponse {
        public string fact { get; set; }
        public int length { get; set; }
    }
}
