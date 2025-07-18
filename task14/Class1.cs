namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double total = 0.0;
        double interval = (b - a) / threadsnumber;

        int totalSteps = (int)((b - a) / step);
        int stepsPerThread = totalSteps / threadsnumber;

        Parallel.For(0, threadsnumber, () => 0.0, (i, state, localSum) =>
        {
            double start = a + i * interval;
            double end = (i == threadsnumber - 1) ? b : start + interval;

            double x = start;
            double localStep = (end - start) / stepsPerThread;

            for (int j = 0; j < stepsPerThread; j++)
            {
                localSum += (function(x) + function(x + localStep)) * 0.5 * localStep;
                x += localStep;
            }

            return localSum;
        },
        localSum =>
        {
            Interlocked.Exchange(ref total, total + localSum);
        });

        return total;
    }
}
