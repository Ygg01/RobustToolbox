using System.Runtime.CompilerServices;

namespace Robust.Shared.Random;

public struct RandomGenerator
{
    private ulong State;

    public RandomGenerator(int seed)
    {
        // Xorshift should never had seed be 0
        State = (ulong)(seed | 0xBAD_5EED);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong NextUInt64()
    {
        State ^= (State << 13);
        State ^= (State >> 17);
        State ^= (State << 5);
        return State;
    }

    public long NextInt64() => (long)(NextUInt64());

    public uint NextUInt32() => (uint)(NextUInt64() >> 32);
    
    public int Next() => (int)(NextUInt64() >> 32);
}