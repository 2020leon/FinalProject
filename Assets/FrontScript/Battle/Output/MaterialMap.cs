using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class MaterialMap : MonoBehaviour
    {
        [Serializable]
        public struct Pair
        {
            public string Name;
            public Material Material;
        }

        [SerializeField]
        private List<Pair> materials;
        private Dictionary<string, Material> dictionary;

        private void Start()
        {
            dictionary = ToDictionary();
        }

        private Dictionary<string, Material> ToDictionary()
        {
            Dictionary<string, Material> target = new Dictionary<string, Material>();
            foreach (var pair in materials)
            {
                target.Add(pair.Name, pair.Material);
            }

            return target;
        }

        public Material this[string Name]
        {
            get
            {
                return dictionary[Name];
            }
        }
    }
}