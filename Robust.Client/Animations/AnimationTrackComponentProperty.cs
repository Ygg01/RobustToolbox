using System;
using JetBrains.Annotations;
using Robust.Shared.Animations;
using Robust.Shared.Interfaces.GameObjects;
using Robust.Shared.Interfaces.Serialization;

namespace Robust.Client.Animations
{
    [UsedImplicitly]
    public sealed class AnimationTrackComponentProperty : AnimationTrackProperty
    {
        public Type? ComponentType { get; set; }
        public string? Property { get; set; }

        protected override void ApplyProperty(object context, object value)
        {
            if (Property == null || ComponentType == null)
            {
                throw new InvalidOperationException("Must set parameters to non-null values.");
            }

            var entity = (IEntity) context;
            var component = entity.GetComponent(ComponentType);

            if (component is IAnimationProperties properties)
            {
                properties.SetAnimatableProperty(Property, value);
            }
            else
            {
                AnimationHelper.SetAnimatableProperty(component, Property, value);
            }
        }

        public override IDeepClone DeepClone()
        {
            return new AnimationTrackComponentProperty
            {
                Property = Property,
                ComponentType = IDeepClone.CloneValue(ComponentType),
                InterpolationMode = IDeepClone.CloneValue(InterpolationMode),
                KeyFrames = IDeepClone.CloneValue(KeyFrames)!
            };
        }
    }
}
