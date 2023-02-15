using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatReservation
{
    internal class SeatReservation
    {
        public static int bookingNumber = 0;

        static void Main(string[] args)
        {
            int option;
            int[,] theatreArray = new int[8, 10];

            // Initializing 2D array to 0
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    theatreArray[i, j] = 0;
                }
            }

            do
            {
                Console.WriteLine("Menu");
                Console.WriteLine("1. Reserve seats");
                Console.WriteLine("2. Reserve seats with specific starting rows and columns");
                Console.WriteLine("3. Cancel reservation");
                Console.WriteLine("4. Remove empty seats from specific row");
                Console.WriteLine("5. Search for reservation");
                Console.WriteLine("6. Total seats booked");
                Console.WriteLine("7. Display Theatre Map");
                Console.WriteLine("8. Exit Application");
                Console.WriteLine("*********************");

                option = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("User Selection {0}", option);

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter adjacent seats do you require:");
                        int numberOfSeats = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Preference for seating(1 for back, 2 for front )");
                        int preference = Convert.ToInt32(Console.ReadLine());

                        theatreArray = ReserveSeats(theatreArray, numberOfSeats, preference);
                        break;

                    case 2:

                        Console.WriteLine("Enter adjacent seats do you require:");
                        numberOfSeats = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter the row number between 1 to 8:");
                        int row = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter the Column number between 1 to 10:");
                        int columnStart = Convert.ToInt32(Console.ReadLine());

                        ReserveSeats(theatreArray, numberOfSeats, row, columnStart);
                        break;

                    case 3:
                        Console.WriteLine("Please Enter your booking number cancel your seats");
                        int cancelbookingNumber = Convert.ToInt32(Console.ReadLine());

                        CancelSeats(theatreArray, cancelbookingNumber);
                        break;

                    case 4:
                        Console.WriteLine("Which row would you like to remove empty seats from");
                        int rowToDelete = Convert.ToInt32(Console.ReadLine());

                        RemoveEmptySeatsRow(theatreArray, rowToDelete);
                        break;

                    case 5:
                        Console.WriteLine("Reservation Number you are searching for:");
                        int searchReservationNumber = Convert.ToInt32(Console.ReadLine());

                        Search(theatreArray, searchReservationNumber);
                        break;

                    case 6:
                        TotalBooked(theatreArray);
                        break;

                    case 7:
                        DisplayMap(theatreArray);
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (option >= 8);
        }

        static int[,] ReserveSeats(int[,] theatreArray, int numberOfSeats, int preference)
        {
            int row;

            if (preference == 1)
            {
                row = 7;
            }
            else
            {
                row = 0;
            }

            int loopCounter = 0;
            int seatCounter = 0;

            // First loop to check the availability of numberOfSeats adjacent
            // seats
            while (loopCounter <= 9 && seatCounter < numberOfSeats)
            {
                if (theatreArray[row, loopCounter] == 0)
                {
                    seatCounter++;
                }
                if (seatCounter == numberOfSeats)
                    break;

                loopCounter++;
            }
            // Console.WriteLine("loop counter seatCounter numberOfSeats{0} {1}
            // {2}",loopCounter, seatCounter, numberOfSeats);

            // If numberOfSeats adjacent seats are available, reserve them
            if (seatCounter == numberOfSeats)
            {
                bookingNumber++;
                int seat_number = bookingNumber;
                loopCounter = 0;
                seatCounter = 0;
                while (loopCounter <= 9 && seatCounter < numberOfSeats)
                {
                    if (theatreArray[row, loopCounter] == 0)
                    {
                        theatreArray[row, loopCounter] = seat_number;
                        seatCounter++;
                    }

                    // If all seats are reserved, break the loop
                    if (seatCounter == numberOfSeats)
                        break;

                    loopCounter++;
                }
            }
            else
            {
                Console.WriteLine("Reservation could not be made as no adjacent seats are available in row");
            }

            return theatreArray;
        }

        static int[,] ReserveSeats(int[,] theatreArray, int numberOfSeats, int row, int columnStart)
        {
            int loopCounter = columnStart - 1;
            int seatCounter = 0;

            // First loop to check the availability of numberOfSeats adjacent
            // seats
            while (loopCounter <= 9 && seatCounter < numberOfSeats)
            {
                if (theatreArray[row - 1, loopCounter] == 0)
                {
                    seatCounter++;
                }

                if (seatCounter == numberOfSeats ||
                    theatreArray[row - 1, loopCounter] != 0)
                    break;

                loopCounter++;
            }

            // Console.WriteLine("loop counter seatCounter numberOfSeats{0} {1}
            // {2}",loopCounter, seatCounter, numberOfSeats);

            // If numberOfSeats adjacent seats are available, reserve them
            if (seatCounter == numberOfSeats)
            {
                bookingNumber++;
                int seat_number = bookingNumber;
                loopCounter = columnStart - 1;
                ;
                seatCounter = 0;
                while (loopCounter <= 9 && seatCounter < numberOfSeats)
                {
                    if (theatreArray[row - 1, loopCounter] == 0)
                    {
                        theatreArray[row - 1, loopCounter] = seat_number;
                        seatCounter++;
                    }

                    // If all seats are reserved, break the loop
                    if (seatCounter == numberOfSeats)
                        break;

                    loopCounter++;
                }
            }
            else
            {
                Console.WriteLine("Reservation could not be made as no adjacent seats are available in row");
            }

            return theatreArray;
        }

        static int[,] CancelSeats(int[,] theatreArray, int bookingNumber)
        {
            for (int row = 0; row < 7; row++)
            {
                int seatCount = 1;
                while (seatCount <= 10)
                {
                    if (theatreArray[row, seatCount - 1] == bookingNumber)
                    {
                        theatreArray[row, seatCount - 1] = 0;
                    }
                    seatCount++;
                }
            }

            return theatreArray;
        }

        static int[,] RemoveEmptySeatsRow(int[,] theatreArray, int row)
        {
            int slow = 0, fast = 1, temp;
            row--;

            while (fast < 10)
            {
                
                if (theatreArray[row, slow] != 0)
                {
                    slow++;
                    fast++;
                }

                while (theatreArray[row, fast]==0 && fast<9)
                {
                    fast++;
                }

                if (theatreArray[row, slow] == 0)
                {
                    temp = theatreArray[row, slow];
                    theatreArray[row, slow] = theatreArray[row, fast];
                    theatreArray[row, fast] = temp;
                    slow++;
                    fast++;
                }
            }
            return theatreArray;
        }

        static void Search(int[,] theatreArray, int bookingNumber)
        {
            bool found = false;


            for (int row = 0; row <= 7; row++)
            {
                for (int seats = 0; seats < 10; seats++)
                {
                    if (theatreArray[row, seats] == bookingNumber)
                    {
                        Console.WriteLine("Row: {0}, Seat {1}", row + 1, seats + 1);
                        found = true;
                    }
                }
            }

            if (found == false)
            {
                Console.WriteLine("*********************\n{0} Booking number not found", bookingNumber);
            }

        }

        static void TotalBooked(int[,] theatreArray)
        {
            int count_seats = 0;

            for (int row = 0; row <= 7; row++)
            {
                for (int seats = 0; seats < 10; seats++)
                {
                    if (theatreArray[row, seats] != 0) count_seats++;
                }
            }
            Console.WriteLine("The total number of seats reserved in the Theatre {0}", count_seats);
        }

        static void DisplayMap(int[,] theatreArray)
        {
            Console.Write("      ");
            for (int i = 0; i < 10; i++) Console.Write("  Seat {0}", i + 1);
            Console.WriteLine();
            for (int i = 7; i >= 0; i--)
            {
                Console.Write("Row {0} ||  ", i + 1);
                for (int j = 0; j <= 9; j++)
                {
                    Console.Write(theatreArray[i, j] + "       ");
                }
                Console.WriteLine();
            }
        }
    }
}
