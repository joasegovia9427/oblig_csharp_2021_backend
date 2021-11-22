namespace MusicServicesREST.Models
{
    public class BandaIntegranteModel
    {
        public BandaIntegranteModel()
        {
        }

        public BandaIntegranteModel(int bandaId, int integranteId)
        {
            BandaId = bandaId;
            IntegranteId = integranteId;
        }

        public int BandaId
        { get; set;
        }
        public int IntegranteId
        {
            get; set;
        }
    }
}