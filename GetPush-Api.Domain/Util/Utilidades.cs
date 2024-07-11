namespace GetPush_Api.Domain.Util
{
    public class Utilidades
    {
        public DateTime RecuperaDataAtualBrasil()
        {
            var timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);
        }
    }
}
