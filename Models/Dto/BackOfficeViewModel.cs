namespace B_U2_S2_G5_PROJECT.Models.Dto
{
    public class BackOfficeViewModel
    {
        public Cliente Cliente { get; set; } = new Cliente();
        public Camera Camera { get; set; }=new Camera();
        public Prenotazione Prenotazione { get; set; }= new Prenotazione();
    }
}
