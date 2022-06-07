using System.Diagnostics.CodeAnalysis;

namespace PGChatBot;

internal record WeightData<T>(double Weight, T Value)
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static T Random(IEnumerable<WeightData<T>> enumerable)
    {
        var totalPriority = enumerable.Sum(data => data.Weight);
        var random = System.Random.Shared.NextDouble() * totalPriority;
        foreach (var (weight, value) in enumerable)
        {
            random -= weight;
            if (random <= 0)
                return value;
        }

        throw new ArgumentOutOfRangeException();
    }
}