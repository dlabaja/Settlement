namespace System.Runtime.CompilerServices
{
    // Requied keyword pro pole
    public class RequiredMemberAttribute : Attribute { }
    
    // Required keyword pro konstruktory
    public class CompilerFeatureRequiredAttribute : Attribute 
    {
        public CompilerFeatureRequiredAttribute(string name) { }
    }
}
