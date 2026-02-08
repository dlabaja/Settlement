using JetBrains.Annotations;

namespace Delegates;

public delegate void ObjectChanged<in T>([CanBeNull] T newObject, [CanBeNull] T oldObject);
public delegate void ObjectChangedNullable<in T>(T newObject, T oldObject);
