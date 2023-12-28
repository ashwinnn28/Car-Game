using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the movement COMMAND for the car ex:-(SF10RB10S):");
        string input = Console.ReadLine();

        int distance = 0;
        int currentGear = 1;
        int time = 0;
        int rtime = 0;
        bool started = false;

        for (int i = 0; i < input.Length;)
        {
            char command = input[i];

            switch (command)
            {
                case 'S':
                    if (!started)
                    {
                        Console.WriteLine("Starting the car.");
                        started = true;
                    }
                    else
                    {
                        Console.WriteLine("Stopping the car.");
                        started = false;
                    }
                    i++;
                    break;
                case 'F':
                    if (started)
                    {
                        int forwardDistance = GetDistance(input, ref i);
                        distance += forwardDistance;
                        int forwardTime = forwardDistance;
                        time += forwardTime;
                        currentGear = RaiseGear(currentGear);
                        Thread.Sleep(forwardTime * 1000);
                        Console.WriteLine($"Moving forward {forwardDistance} meters in gear {currentGear}. Total distance: {distance} meters. Time taken: {forwardTime} sec. Total time: {time} sec");
                        
                    }
                    else
                    {
                        Console.WriteLine("Ignoring forward command. The car is not started.");
                        i++;
                    }
                    break;
                case 'B':
                    if (started)
                    {
                        int backwardDistance = GetDistance(input, ref i);
                        distance += backwardDistance;
                        int backwardTime = 4 * backwardDistance;
                        rtime += backwardTime;
                        time += backwardTime;
                        Thread.Sleep(backwardTime * 1000);
                        Console.WriteLine($"Moving backward {backwardDistance} meters in reverse gear. Total distance: {distance} meters. Time taken: {backwardTime} sec. Total time: {time} sec");
                        currentGear = ReverseGear();
                    }
                    else
                    {
                        Console.WriteLine("Ignoring backward command. The car is not started.");
                        i++;
                    }
                    break;
                case 'L':
                    int turnLeftTime = GetTurnTime(input, ref i);
                    Console.WriteLine("Turning left and lowering the gear.");
                    currentGear = LowerGear(currentGear);
                    turnLeftTime = 5;
                    time += turnLeftTime;
                    Thread.Sleep(turnLeftTime * 1000);
                    Console.WriteLine($"Current gear: {currentGear}. Time taken: {turnLeftTime} sec. Total time: {time} sec");
                   
                    break;
                case 'R':
                    int turnRightTime = GetTurnTime(input, ref i);
                    Console.WriteLine("Turning right and lowering the gear.");
                    currentGear = LowerGear(currentGear);
                    turnRightTime = 5;
                    time += turnRightTime;
                    Thread.Sleep(turnRightTime * 1000);
                    Console.WriteLine($"Current gear: {currentGear}. Time taken: {turnRightTime} sec. Total time: {time} sec");
                    
                    break;
                default:
                    Console.WriteLine($"Invalid command '{command}'. Ignoring.");
                    i++;
                    break;
            }
        }
        Thread.Sleep(time * 1000);
        Console.WriteLine($"The car has traveled a total distance of {distance} meters in {time} sec.");
        Console.ReadLine();
    }

    static int GetDistance(string input, ref int index)
    {
        int distance = 0;
        index++;

        while (index < input.Length && Char.IsDigit(input[index]))
        {
            distance = distance * 10 + (input[index] - '0');
            index++;
        }

        return distance;
    }

    static int GetTurnTime(string input, ref int index)
    {
        int time = 0;
        index++;

        while (index < input.Length && Char.IsDigit(input[index]))
        {
            time = time * 10 + (input[index] - '0');
            index++;
        }

        return time;
    }

    static int LowerGear(int currentGear)
    {
        Console.WriteLine($"Lowering gear from {currentGear} to {Math.Max(1, currentGear - 1)}");
        return Math.Max(1, currentGear - 1);
    }

    static int RaiseGear(int currentGear)
    {
        return Math.Min(5, currentGear + 1);
    }

    static int ReverseGear()
    {
        return -1;
    }
}
