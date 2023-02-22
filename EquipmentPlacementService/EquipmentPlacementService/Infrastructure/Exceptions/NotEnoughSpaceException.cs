namespace EquipmentPlacementService.Infrastructure.Exceptions
{
    public class NotEnoughSpaceException : Exception
    {
        public NotEnoughSpaceException() : base()
        {
        }

        public NotEnoughSpaceException(string message)
            : base(message)
        {
        }

        public NotEnoughSpaceException(int premisesId)
           : base($"Premises with id {premisesId} doesn't have enought space")
        {
        }
    }
}
