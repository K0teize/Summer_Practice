namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double total = 0.0;
        double interval = (b - a) / threadsnumber;
        Object lockObject = new Object();
        var barrier = new Barrier(threadsnumber);
        var threads = new Thread[threadsnumber];
        int totalSteps = (int)((b - a) / step);
        int stepsPerThread = totalSteps / threadsnumber;

        for (int i = 0; i < threadsnumber; i++)
        {
            int threadNum = i; 
            threads[i] = new Thread(() => 
            {
                double start = a + threadNum * interval;
                double end = (threadNum == threadsnumber - 1) ? b : start + interval;
                
                double localSum = 0.0;
                double x = start;
                double localStep = (end - start) / stepsPerThread;

                for (int j = 0; j < stepsPerThread; j++)
                {
                    localSum += (function(x) + function(x + localStep)) * 0.5 * localStep;
                    x += localStep;
                }
                lock (lockObject)
                {
                    total += localSum;
                }
                
                barrier.SignalAndWait();
            });
            
            threads[i].Start();
        }
        foreach (var thread in threads)
        {
            thread.Join();
        }
        return total;
    }
}
