namespace BusBookingSystem.Application.Common
{
    public static class MessageConstants
    {
        // Error Messages
        public static class Errors
        {
            public const string GeneralError = "An unexpected error occurred.";
            public const string NotFound = "The requested resource was not found.";
            public const string ValidationError = "Invalid data provided.";
            
            public static class Bus
            {
                public const string FetchError = "Unable to fetch bus information.";
                public const string NotFound = "Bus not found.";
                public const string InvalidBusNumber = "Invalid bus number provided.";
            }

            public static class Booking
            {
                public const string FetchError = "Unable to fetch booking information.";
                public const string CreateError = "Unable to create booking.";
                public const string UpdateError = "Unable to update booking.";
                public const string DeleteError = "Unable to delete booking.";
                public const string NotFound = "Booking not found.";
                public const string InvalidBookingData = "Invalid booking data provided.";
            }
        }

        // Success Messages
        public static class Success
        {
            public static class Bus
            {
                public const string Created = "Bus created successfully.";
                public const string Updated = "Bus updated successfully.";
                public const string Deleted = "Bus deleted successfully.";
            }

            public static class Booking
            {
                public const string Created = "Booking created successfully.";
                public const string Updated = "Booking updated successfully.";
                public const string Deleted = "Booking deleted successfully.";
            }
        }

        // Log Messages
        public static class Logs
        {
            public static class Bus
            {
                public const string FetchingBuses = "Fetching active buses";
                public const string FetchingBusByNumber = "Fetching bus by number: {0}";
                public const string ErrorFetchingBuses = "Error occurred while fetching buses";
                public const string ErrorFetchingBusByNumber = "Error occurred while fetching bus by number: {0}";
            }

            public static class Booking
            {
                public const string CreatingBooking = "Creating new booking";
                public const string UpdatingBooking = "Updating booking: {0}";
                public const string DeletingBooking = "Deleting booking: {0}";
                public const string ErrorCreatingBooking = "Error occurred while creating booking";
                public const string ErrorUpdatingBooking = "Error occurred while updating booking: {0}";
                public const string ErrorDeletingBooking = "Error occurred while deleting booking: {0}";
                
                // Added new messages
                public const string FetchingBooking = "Retrieving booking with ID: {0}";
                public const string ErrorFetchingBooking = "Error occurred while fetching booking: {0}";
                public const string FetchingBookings = "Retrieving bookings with skip: {0}, take: {1}";
                public const string ErrorFetchingBookings = "Error occurred while fetching bookings";
                public const string FetchingBookingsCount = "Getting total bookings count";
                public const string ErrorFetchingBookingsCount = "Error occurred while getting bookings count";
            }
        }
    }
} 