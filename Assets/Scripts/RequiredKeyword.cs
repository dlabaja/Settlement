namespace System.Runtime.CompilerServices
{
    // Tento atribut musí existovat, aby C# 11 'required' fungovalo
    public class RequiredMemberAttribute : Attribute { }
    
    // Často k tomu budeš potřebovat i tento, pokud začneš používat konstruktory s required
    public class CompilerFeatureRequiredAttribute : Attribute 
    {
        public CompilerFeatureRequiredAttribute(string name) { }
    }
}
