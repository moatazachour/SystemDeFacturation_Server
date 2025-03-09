namespace SystemDeFacturation_Server.Models
{
    public class Facture
    {
        public int FactureID { get; set; }
        public required string NumFacture { get; set; }
        public required DateOnly DateDeEcheance { get; set; }
        public required decimal MontantTotal { get; set; }
        public required decimal MontantRestantDue { get; set; }
        public required int AcheteurID { get; set; }

    }
}
