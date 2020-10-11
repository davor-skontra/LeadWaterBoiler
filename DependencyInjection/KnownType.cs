using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyInjection.Attributes;
using UnityEngine;
using Utilities;

namespace DependencyInjection
{
    public class KnownType
    {
        private readonly List<FieldInfo> _childComponentFields = new List<FieldInfo>();

        private readonly List<FieldInfo> _childComponentsFields = new List<FieldInfo>();

        //Single components
        private readonly List<FieldInfo> _ownComponentFields = new List<FieldInfo>();

        // Component arrays
        private readonly List<FieldInfo> _ownComponentsFields = new List<FieldInfo>();
        private readonly List<FieldInfo> _parentComponentFields = new List<FieldInfo>();
        private readonly List<FieldInfo> _parentComponentsFields = new List<FieldInfo>();

        // Services
        private readonly List<FieldInfo> _serviceFields = new List<FieldInfo>();

        public KnownType(Type type)
        {
            var fields = type.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic
                                    | BindingFlags.Static | BindingFlags.Instance
            );

            foreach (var field in fields)
            {
                var attributes = field
                    .GetCustomAttributes()
                    .ToArray();

                // Single components
                if (HasAttribute<InjectComponentAttribute>(attributes))
                {
                    _ownComponentFields.Add(field);
                }

                if (HasAttribute<InjectParentComponentAttribute>(attributes))
                {
                    _parentComponentFields.Add(field);
                }

                if (HasAttribute<InjectChildComponentAttribute>(attributes))
                {
                    _childComponentFields.Add(field);
                }

                // Component arrays
                if (HasAttribute<InjectComponentsAttribute>(attributes))
                {
                    _ownComponentsFields.Add(field);
                }

                if (HasAttribute<InjectParentComponentsAttribute>(attributes))
                {
                    _parentComponentsFields.Add(field);
                }

                if (HasAttribute<InjectChildComponentsAttribute>(attributes))
                {
                    _childComponentsFields.Add(field);
                }

                //Attributes
                if (HasAttribute<InjectAttribute>(attributes))
                {
                    _serviceFields.Add(field);
                }
            }

            bool HasAttribute<TAttribute>(Attribute[] attributes) where TAttribute : Attribute
            {
                foreach (var ownedAttribute in attributes)
                {
                    if (ownedAttribute is TAttribute)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void Resolve(object target)
        {
            foreach (var field in _serviceFields)
            {
                field.SetValue(target, IocContainer.Services.ResolveAnonymous(field.FieldType));
            }
        }

        public void Resolve<TMonoBehaviour>(TMonoBehaviour monoBehaviour) where TMonoBehaviour : MonoBehaviour
        {
            // Single component
            foreach (var field in _ownComponentFields)
            {
                field.SetValue(monoBehaviour, monoBehaviour.GetComponent(field.FieldType));
            }

            foreach (var field in _parentComponentFields)
            {
                field.SetValue(monoBehaviour, monoBehaviour.GetComponentInParent(field.FieldType));
            }

            foreach (var field in _childComponentFields)
            {
                field.SetValue(monoBehaviour, monoBehaviour.GetComponentInChildren(field.FieldType));
            }

            // Component arrays
            foreach (var field in _ownComponentsFields)
            {
                var elementType = field.FieldType.GetElementType();
                SetValueWithFieldConversion(elementType, field, monoBehaviour.GetComponents);
            }

            foreach (var field in _parentComponentsFields)
            {
                var elementType = field.FieldType.GetElementType();
                SetValueWithFieldConversion(elementType, field, monoBehaviour.GetComponentsInParent);
            }

            foreach (var field in _childComponentsFields)
            {
                var elementType = field.FieldType.GetElementType();
                SetValueWithFieldConversion(elementType, field, monoBehaviour.GetComponentsInChildren);
            }

            foreach (var field in _serviceFields)
            {
                field.SetValue(monoBehaviour, IocContainer.Services.ResolveAnonymous(field.FieldType));
            }

            void SetValueWithFieldConversion(Type elementType, FieldInfo field, Func<Type, object[]> getter)
            {
                var finalValue = getter(elementType).Convert(elementType);
                field.SetValue(monoBehaviour, finalValue);
            }
        }
    }
}