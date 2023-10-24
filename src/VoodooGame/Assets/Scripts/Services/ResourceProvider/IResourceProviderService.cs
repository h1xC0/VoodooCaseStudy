using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Services.ResourceProvider
{
    public interface IResourceProviderService
    {
        TResource LoadResource<TResource>(ResourceInfo resourceInfo) where TResource : Object, IResource;
        IEnumerable<TResource> LoadResources<TResource>(ResourceInfo resourceInfo) where TResource : Object, IResource;

    }
}