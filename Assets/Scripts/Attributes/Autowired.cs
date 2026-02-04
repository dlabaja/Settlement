using JetBrains.Annotations;
using Reflex.Attributes;
using System;

namespace Attributes
{
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class AutowiredAttribute : InjectAttribute
    {
    }
}
