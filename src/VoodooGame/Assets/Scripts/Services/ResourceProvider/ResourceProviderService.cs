using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Services.ResourceProvider
{
    public class ResourceProviderService : IResourceProviderService
    {
        public TResource LoadResource<TResource>(ResourceInfo resourceInfo) where TResource : Object, IResource
        {
            return Resources.Load<TResource>(resourceInfo.Path);
        }

        public IEnumerable<TResource> LoadResources<TResource>(ResourceInfo resourceInfo)
            where TResource : Object, IResource
        {
            return Resources.LoadAll<TResource>(resourceInfo.Path);
        }
    }
}
