using K5BZI_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace K5BZI_Services.Utilities
{
    public class PropertyCompareResult
    {
        public string Name { get; private set; }
        public object NewValue { get; private set; }

        public PropertyCompareResult(string name, object newValue)
        {
            Name = name;
            NewValue = newValue;
        }
    }

    public class EntityUtility : BaseUtility, IEntityUtility
    {
        public void UpdateChangedProperties<T>(T newObject, T oldObject)
        {
            var properties = typeof(T).GetProperties();
            var result = new List<PropertyCompareResult>();

            foreach (var propertyInfo in properties)
            {
                var property = properties.FirstOrDefault(_ => _.Name == propertyInfo.Name);

                if (property == null || propertyInfo.Name == "Item" || propertyInfo.Name == "Id")
                {
                    continue;
                }

                try
                {
                    var oldValue = property.GetValue(oldObject);
                    var newValue = property.GetValue(newObject);

                    if (!object.Equals(oldValue, newValue))
                    {
                        var campareResult = new PropertyCompareResult(property.Name, newValue);

                        UpdateValue(campareResult, newObject, oldObject);
                    }
                }
                catch (Exception ex)
                {
                    var objectType = typeof(T).ToString();
                    var errorMsg = $"Error updating properties. Type: {objectType}, PropertyName: {propertyInfo.Name}. Details: {ex.Message}";

                    //Logger.Error(errorMsg);

                    throw new Exception(errorMsg);
                }
            }
        }

        private void UpdateValue<T>(PropertyCompareResult resultItem, T newObject, T oldObject)
        {
            var newObjectype = newObject.GetType();
            var oldObjectType = oldObject.GetType();
            var propertyInfo = oldObjectType.GetProperty(resultItem.Name);
            var propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyInfo.PropertyType) ?
                Nullable.GetUnderlyingType(propertyInfo.PropertyType) :
                propertyInfo.PropertyType;

            try
            {
                //Returns an System.Object with the specified System.Type and whose value is
                //equivalent to the specified object.
                var resultValue = Convert.ChangeType(resultItem.NewValue, targetType);

                propertyInfo.SetValue(oldObject, resultValue, null);
            }
            catch (ArgumentException)
            { }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}