using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.ResourceProvider
{
    public class ResourceProviderService : IResourceProviderService
    {
        public TResource LoadResource<TResource>(ResourceInfo resourceInfo) where TResource : Object, IResource
        {
            var resource = Resources.Load<TResource>(resourceInfo.Path);
            if (resource != null)
            {
                return resource;
            }
                
            throw new NullReferenceException($"{typeof(TResource)} resource wasn't found");
        }

        public IEnumerable<TResource> LoadResources<TResource>(ResourceInfo resourceInfo)
            where TResource : Object, IResource
        {
            var resources = Resources.LoadAll<TResource>(resourceInfo.Path);

            if (resources is { Length: > 0 })
            {
                return resources;
            }

            throw new NullReferenceException($"{typeof(TResource)} resources weren't found");
        }
    }
}
