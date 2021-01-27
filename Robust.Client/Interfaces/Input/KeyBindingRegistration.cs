﻿using Robust.Client.Input;
using Robust.Shared.Input;
using Robust.Shared.Interfaces.Serialization;
using Robust.Shared.Serialization;

namespace Robust.Client.Interfaces.Input
{
    public struct KeyBindingRegistration : IExposeData
    {
        public BoundKeyFunction Function;
        public KeyBindingType Type;
        public Keyboard.Key BaseKey;
        public Keyboard.Key Mod1;
        public Keyboard.Key Mod2;
        public Keyboard.Key Mod3;
        public int Priority;
        public bool CanFocus;
        public bool CanRepeat;

        public void ExposeData(ObjectSerializer serializer)
        {
            serializer.DataField(ref Function, "function", default);
            serializer.DataField(ref Type, "type", KeyBindingType.State);
            serializer.DataField(ref BaseKey, "key", default);
            serializer.DataField(ref Mod1, "mod1", default);
            serializer.DataField(ref Mod2, "mod2", default);
            serializer.DataField(ref Mod3, "mod3", default);
            serializer.DataField(ref Priority, "priority", 0);
            serializer.DataField(ref CanFocus, "canFocus", false);
            serializer.DataField(ref CanRepeat, "canRepeat", false);
        }

        public IDeepClone DeepClone()
        {
            return new KeyBindingRegistration()
            {
                Function = Function,
                Type = Type,
                BaseKey = BaseKey,
                Mod1 = Mod1,
                Mod2 = Mod3,
                Priority = Priority,
                CanFocus = CanFocus,
                CanRepeat = CanRepeat
            };
        }
    }
}
